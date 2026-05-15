using System.Globalization;
using System.IO.Compression;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using Nuke.Common;
using Nuke.Common.CI.GitHubActions;
using Nuke.Common.IO;
using Nuke.Common.Tooling;
using Nuke.Common.Tools.GitVersion;
using Serilog;

class Build : NukeBuild
{
    public static int Main() => Execute<Build>(x => x.PublishRelease);

    const string DefaultRulePrefix = "AV";
    const string PandocVersion = "3.9.0.2";

    [GitVersion(NoFetch = true)]
    readonly GitVersion? GitVersion;

    AbsolutePath? _pandocPath;

    AbsolutePath ArtifactsDirectory => RootDirectory / "Artifacts";
    AbsolutePath GuidelinesDirectory => ArtifactsDirectory / "Guidelines";
    AbsolutePath CheatsheetDirectory => ArtifactsDirectory / "Cheatsheet";

    string SemVer => GitVersion?.SemVer ?? "0.0.0";

    string CommitDate => GitVersion?.CommitDate is { } rawDate
        ? DateTime.Parse(rawDate, CultureInfo.InvariantCulture).ToString("MMMM d, yyyy", CultureInfo.GetCultureInfo("en-US"))
        : DateTime.Now.ToString("MMMM d, yyyy", CultureInfo.GetCultureInfo("en-US"));

    static bool IsTagBuild => GitHubActions.Instance?.RefType == "tag";

    AbsolutePath[] GuidelinesPages =>
    [
        RootDirectory / "_pages" / "0000_CoverAndStyles.md",
        RootDirectory / "_includes" / "0001_Introduction.md",
        RootDirectory / "_pages" / "1000_ClassDesignGuidelines.md",
        RootDirectory / "_pages" / "1100_MemberDesignGuidelines.md",
        RootDirectory / "_pages" / "1200_MiscellaneousDesignGuidelines.md",
        RootDirectory / "_pages" / "1500_MaintainabilityGuidelines.md",
        RootDirectory / "_pages" / "1700_NamingGuidelines.md",
        RootDirectory / "_pages" / "1800_PerformanceGuidelines.md",
        RootDirectory / "_pages" / "2200_FrameworkGuidelines.md",
        RootDirectory / "_pages" / "2300_DocumentationGuidelines.md",
        RootDirectory / "_pages" / "2400_LayoutGuidelines.md",
        RootDirectory / "_pages" / "9999_ResourcesAndLinks.md",
    ];

    Target Clean => _ => _
        .Executes(() => ArtifactsDirectory.CreateOrCleanDirectory());

    Target Compile => _ => _
        .DependsOn(Clean)
        .Executes(() =>
        {
            GuidelinesDirectory.CreateOrCleanDirectory();
            var output = string.Join("\n", GuidelinesPages.Select(ProcessPage));
            (GuidelinesDirectory / "CSharpCodingGuidelines.md").WriteAllText(output);
            (RootDirectory / "assets" / "css" / "Guidelines.css").Copy(GuidelinesDirectory / "style.css", ExistsPolicy.FileOverwrite);
            (RootDirectory / "assets" / "images").CopyToDirectory(GuidelinesDirectory / "assets", ExistsPolicy.MergeAndOverwrite);
        });

    Target CompileCheatsheet => _ => _
        .DependsOn(Clean)
        .Executes(() =>
        {
            CheatsheetDirectory.CreateOrCleanDirectory();
            var content = ApplyTokenReplacements((RootDirectory / "_pages" / "Cheatsheet.md").ReadAllText());
            content = content.Replace("{{ site.default_rule_prefix }}", DefaultRulePrefix);
            (CheatsheetDirectory / "Cheatsheet.md").WriteAllText(content);
            (RootDirectory / "assets" / "css" / "CheatSheet.css").Copy(CheatsheetDirectory / "style.css", ExistsPolicy.FileOverwrite);
            (RootDirectory / "assets" / "images").CopyToDirectory(CheatsheetDirectory / "assets", ExistsPolicy.MergeAndOverwrite);
        });

    Target BuildHtml => _ => _
        .DependsOn(Compile, CompileCheatsheet)
        .Executes(() =>
        {
            var pandoc = ResolvePandoc();
            ProcessTasks.StartProcess(pandoc, "CSharpCodingGuidelines.md -f markdown_phpextra-implicit_figures -s -o ../CSharpCodingGuidelines.htm --embed-resources --standalone", workingDirectory: GuidelinesDirectory).AssertZeroExitCode();
            ProcessTasks.StartProcess(pandoc, "Cheatsheet.md -f markdown+markdown_in_html_blocks-implicit_figures -s -o ../CSharpCodingGuidelinesCheatsheet.htm --embed-resources --standalone", workingDirectory: CheatsheetDirectory).AssertZeroExitCode();
        });

    Target BuildPdf => _ => _
        .DependsOn(BuildHtml)
        .Executes(() =>
        {
            var chrome = ResolveChrome();
            ConvertToPdf(chrome, ArtifactsDirectory / "CSharpCodingGuidelines.htm", ArtifactsDirectory / "CSharpCodingGuidelines.pdf");
            ConvertToPdf(chrome, ArtifactsDirectory / "CSharpCodingGuidelinesCheatsheet.htm", ArtifactsDirectory / "CSharpCodingGuidelinesCheatsheet.pdf");
        });

    Target PublishRelease => _ => _
        .DependsOn(BuildPdf)
        .OnlyWhenStatic(() => IsTagBuild)
        .Executes(() =>
        {
            var tag = GitHubActions.Instance!.RefName;
            var repo = GitHubActions.Instance!.Repository;
            Log.Information("Publishing release {Tag} for {Repo}", tag, repo);
            ProcessTasks.StartProcess("gh", $"release create {tag} --repo {repo} --generate-notes --title {tag}", workingDirectory: RootDirectory).WaitForExit();
            foreach (var pdf in ArtifactsDirectory.GlobFiles("*.pdf"))
                ProcessTasks.StartProcess("gh", $"release upload {tag} \"{pdf}\" --repo {repo} --clobber", workingDirectory: RootDirectory).AssertZeroExitCode();
        });

    Target LaunchWebsite => _ => _
        .Executes(() =>
        {
            EnsureRubyInstalled();
            ProcessTasks.StartProcess("gem", "install bundler", workingDirectory: RootDirectory).AssertZeroExitCode();
            ProcessTasks.StartProcess("bundle", "install", workingDirectory: RootDirectory).AssertZeroExitCode();
            (RootDirectory / "_site").CreateOrCleanDirectory();
            ProcessTasks.StartProcess("bundle", "exec jekyll serve --incremental", workingDirectory: RootDirectory).WaitForExit();
        });

    string ProcessPage(AbsolutePath pageFile)
    {
        var rawContent = ApplyTokenReplacements(pageFile.ReadAllText());
        var title = ExtractFrontmatterField(rawContent, "title");
        var category = ExtractFrontmatterField(rawContent, "rule_category");
        rawContent = StripFrontmatter(rawContent);
        var content = string.IsNullOrEmpty(category) ? rawContent : BuildCategorySection(category);
        content = ApplySiteReplacements(content);
        return string.IsNullOrEmpty(title) ? content : $"<h1>{title}</h1>\n{content}";
    }

    string ApplyTokenReplacements(string content) =>
        content
            .Replace("%semver%", SemVer)
            .Replace("%commitdate%", CommitDate)
            .Replace("![](/assets", "![](assets");

    string BuildCategorySection(string category)
    {
        var ruleFiles = (RootDirectory / "_rules").GlobFiles("*.md").OrderBy(f => f.ToString());
        var content = new StringBuilder();
        foreach (var ruleFile in ruleFiles)
            AppendRuleIfInCategory(content, ruleFile, category);
        return content.ToString();
    }

    AbsolutePath ResolvePandoc()
    {
        if (_pandocPath is not null)
            return _pandocPath;
        var exeName = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? "pandoc.exe" : "pandoc";
        var cacheDir = RootDirectory / ".nuke" / "temp" / "tools" / "pandoc" / PandocVersion;
        _pandocPath = cacheDir.GlobFiles($"**/{exeName}").FirstOrDefault()
            ?? DownloadAndExtractPandoc(cacheDir, exeName);
        return _pandocPath;
    }

    static string ApplySiteReplacements(string content) =>
        Regex.Replace(
            content.Replace("{{ site.default_rule_prefix }}", DefaultRulePrefix),
            @"\(\/.+?(#\w+)\)", "($1)");

    static void AppendRuleIfInCategory(StringBuilder content, AbsolutePath ruleFile, string category)
    {
        var rule = ruleFile.ReadAllText();
        if (!Regex.IsMatch(rule, $@"---(.|\n)*rule_category\:\s*{Regex.Escape(category)}", RegexOptions.Singleline))
            return;
        content.AppendLine(FormatRule(rule));
    }

    static string FormatRule(string rule)
    {
        var ruleTitle = ExtractFrontmatterField(rule, "title");
        var ruleSeverity = ExtractFrontmatterField(rule, "severity");
        var ruleId = ExtractFrontmatterField(rule, "rule_id");
        var customPrefix = ExtractFrontmatterField(rule, "custom_prefix");
        var ruleIdPrefix = string.IsNullOrEmpty(customPrefix) ? "{{ site.default_rule_prefix }}" : customPrefix;
        var severityImg = string.IsNullOrEmpty(ruleSeverity) ? "" : $" <img src=\"assets/images/{ruleSeverity}.png\" />";
        return $"<div id=\"{ruleIdPrefix}{ruleId}\"></div>### {ruleTitle} ({ruleIdPrefix}{ruleId}){severityImg}\n\n{StripFrontmatter(rule)}";
    }

    static void ConvertToPdf(AbsolutePath chrome, AbsolutePath inputHtml, AbsolutePath outputPdf)
    {
        Log.Information("Converting {Input} → {Output}", inputHtml.Name, outputPdf.Name);
        var uri = new Uri(inputHtml.ToString()).AbsoluteUri;
        ProcessTasks
            .StartProcess(chrome, $"--headless --disable-gpu --disable-dev-shm-usage --no-sandbox --no-pdf-header-footer \"--print-to-pdf={outputPdf}\" \"{uri}\"")
            .AssertZeroExitCode();
    }

    static AbsolutePath ResolveChrome()
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            return FindWindowsChrome();
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            return FindLinuxChrome();
        if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            return FindMacChrome();
        throw new PlatformNotSupportedException("Unsupported OS for Chrome PDF generation.");
    }

    static AbsolutePath FindWindowsChrome()
    {
        AbsolutePath[] candidates =
        [
            (AbsolutePath)Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) / "Google" / "Chrome" / "Application" / "chrome.exe",
            (AbsolutePath)Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) / "Google" / "Chrome" / "Application" / "chrome.exe",
            (AbsolutePath)Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) / "Google" / "Chrome" / "Application" / "chrome.exe",
        ];
        return candidates.FirstOrDefault(c => c.FileExists())
            ?? throw new Exception("Google Chrome not found. Install from https://www.google.com/chrome/");
    }

    static AbsolutePath FindLinuxChrome()
    {
        foreach (var name in new[] { "google-chrome", "google-chrome-stable", "chromium-browser", "chromium" })
        {
            var which = ProcessTasks.StartProcess("which", name, logOutput: false);
            which.WaitForExit();
            if (which.ExitCode == 0)
                return (AbsolutePath)which.Output.First(o => o.Type == OutputType.Std).Text.Trim();
        }
        throw new Exception("Chrome/Chromium not found. Install with: sudo apt-get install google-chrome-stable");
    }

    static AbsolutePath FindMacChrome()
    {
        var path = (AbsolutePath)"/Applications/Google Chrome.app/Contents/MacOS/Google Chrome";
        return path.FileExists() ? path : throw new Exception("Google Chrome not found. Install from https://www.google.com/chrome/");
    }

    static AbsolutePath DownloadAndExtractPandoc(AbsolutePath cacheDir, string exeName)
    {
        var (assetName, _) = GetPandocAsset();
        cacheDir.CreateOrCleanDirectory();
        DownloadExtractAndCleanArchive(cacheDir, assetName);
        var path = cacheDir.GlobFiles($"**/{exeName}").First();
        MakeExecutable(path);
        return path;
    }

    static void DownloadExtractAndCleanArchive(AbsolutePath cacheDir, string assetName)
    {
        var archivePath = cacheDir / assetName;
        DownloadPandocArchive(assetName, archivePath);
        ExtractPandocArchive(archivePath, cacheDir);
        archivePath.DeleteFile();
    }

    static void DownloadPandocArchive(string assetName, AbsolutePath archivePath)
    {
        var downloadUrl = $"https://github.com/jgm/pandoc/releases/download/{PandocVersion}/{assetName}";
        Log.Information("Downloading Pandoc {Version} from {Url}...", PandocVersion, downloadUrl);
        HttpTasks.HttpDownloadFile(downloadUrl, archivePath, clientConfigurator: c =>
        {
            c.Timeout = TimeSpan.FromMinutes(5);
            return c;
        });
    }

    static void ExtractPandocArchive(AbsolutePath archivePath, AbsolutePath cacheDir)
    {
        if (Path.GetExtension(archivePath.ToString()).Equals(".zip", StringComparison.OrdinalIgnoreCase))
            ZipFile.ExtractToDirectory(archivePath, cacheDir);
        else
            ProcessTasks.StartProcess("tar", $"-xzf \"{archivePath}\" -C \"{cacheDir}\"").AssertZeroExitCode();
    }

    static void MakeExecutable(AbsolutePath path)
    {
        if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            ProcessTasks.StartProcess("chmod", $"+x \"{path}\"").AssertZeroExitCode();
    }

    static (string assetName, string exeName) GetPandocAsset()
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            return ($"pandoc-{PandocVersion}-windows-x86_64.zip", "pandoc.exe");
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            var arch = RuntimeInformation.ProcessArchitecture == Architecture.Arm64 ? "arm64" : "amd64";
            return ($"pandoc-{PandocVersion}-linux-{arch}.tar.gz", "pandoc");
        }
        if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
        {
            var arch = RuntimeInformation.ProcessArchitecture == Architecture.Arm64 ? "arm64" : "x86_64";
            return ($"pandoc-{PandocVersion}-{arch}-macOS.zip", "pandoc");
        }
        throw new PlatformNotSupportedException("Unsupported OS for Pandoc download.");
    }

    static void EnsureRubyInstalled()
    {
        if (IsRubyInstalled())
            return;
        Log.Information("Ruby 3.3 not found. Installing...");
        InstallRuby();
    }

    static bool IsRubyInstalled()
    {
        try
        {
            var check = ProcessTasks.StartProcess("ruby", "--version");
            check.WaitForExit();
            if (check.ExitCode != 0)
                return false;
            Log.Information("Ruby found: {Version}", string.Join("", check.Output.Select(o => o.Text)));
            return true;
        }
        catch
        {
            return false;
        }
    }

    static void InstallRuby()
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            ProcessTasks.StartProcess("winget", "install RubyInstallerTeam.RubyWithDevKit.3.3 --silent --accept-package-agreements --accept-source-agreements").AssertZeroExitCode();
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            ProcessTasks.StartProcess("sudo", "apt-get install -y ruby-full ruby-bundler build-essential").AssertZeroExitCode();
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            ProcessTasks.StartProcess("brew", "install ruby@3.3").AssertZeroExitCode();
        else
            Assert.Fail("Unsupported OS. Install Ruby 3.3 manually and re-run.");
    }

    static string StripFrontmatter(string content) =>
        Regex.Replace(content, @"---\r?\n(.|\r?\n)+?---\r?\n", "");

    static string ExtractFrontmatterField(string content, string fieldName)
    {
        var match = Regex.Match(content, $@"---(.|\n)*?{Regex.Escape(fieldName)}\:\s*([^\r\n]+)");
        return match.Success ? match.Groups[2].Value.Trim() : string.Empty;
    }
}
