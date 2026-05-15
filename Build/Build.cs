using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using Nuke.Common;
using Nuke.Common.IO;
using Nuke.Common.Tooling;
using Serilog;

class Build : NukeBuild
{
    public static int Main() => Execute<Build>(x => x.BuildHtml);

    const string DefaultRulePrefix = "AV";
    const string PandocVersion = "3.9.0.2";

    AbsolutePath ArtifactsDirectory => RootDirectory / "Artifacts";

    string? cachedPandocPath;

    string semVer = "0.0.0";
    string commitDate = DateTime.Now.ToString("MMMM d, yyyy");

    Target Clean => _ => _
        .Executes(() =>
        {
            ArtifactsDirectory.CreateOrCleanDirectory();
        });

    Target ExtractVersionsFromGit => _ => _
        .Executes(() =>
        {
            var process = ProcessTasks.StartProcess("dotnet", "tool run dotnet-gitversion", workingDirectory: RootDirectory);
            process.AssertZeroExitCode();

            var json = string.Join("\n", process.Output.Select(o => o.Text));
            using var doc = JsonDocument.Parse(json);

            semVer = doc.RootElement.GetProperty("SemVer").GetString() ?? semVer;
            var rawDate = doc.RootElement.GetProperty("CommitDate").GetString();
            if (rawDate is not null)
                commitDate = DateTime.Parse(rawDate).ToString("MMMM d, yyyy");

            Log.Information("Version: {SemVer}, Date: {CommitDate}", semVer, commitDate);
        });

    Target Compile => _ => _
        .DependsOn(Clean, ExtractVersionsFromGit)
        .Executes(() =>
        {
            var guidelinesDir = ArtifactsDirectory / "Guidelines";
            guidelinesDir.CreateOrCleanDirectory();

            AbsolutePath[] pages =
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

            var output = new StringBuilder();

            foreach (var pageFile in pages)
            {
                var rawContent = File.ReadAllText(pageFile);
                rawContent = rawContent.Replace("%semver%", semVer);
                rawContent = rawContent.Replace("%commitdate%", commitDate);
                rawContent = rawContent.Replace("![](/assets", "![](assets");

                var title = ExtractFrontmatterField(rawContent, "title");
                var category = ExtractFrontmatterField(rawContent, "rule_category");

                rawContent = StripFrontmatter(rawContent);

                string content;

                if (string.IsNullOrEmpty(category))
                {
                    Log.Information("Including {File}", Path.GetFileName(pageFile));
                    content = rawContent;
                }
                else
                {
                    Log.Information("Including rules of category {Category}", category);
                    content = BuildCategorySection(category);
                }

                content = content.Replace("{{ site.default_rule_prefix }}", DefaultRulePrefix);
                content = Regex.Replace(content, @"\(\/.+?(#\w+)\)", "($1)");

                if (!string.IsNullOrEmpty(title))
                    content = $"<h1>{title}</h1>\n" + content;

                output.AppendLine(content);
            }

            File.WriteAllText(guidelinesDir / "CSharpCodingGuidelines.md", output.ToString());

            CopyFile(
                RootDirectory / "assets" / "css" / "Guidelines.css",
                guidelinesDir / "style.css");

            CopyDirectoryRecursively(
                RootDirectory / "assets" / "images",
                guidelinesDir / "Assets" / "Images");
        });

    Target CompileCheatsheet => _ => _
        .DependsOn(Clean, ExtractVersionsFromGit)
        .Executes(() =>
        {
            var cheatsheetDir = ArtifactsDirectory / "Cheatsheet";
            cheatsheetDir.CreateOrCleanDirectory();

            var content = File.ReadAllText(RootDirectory / "_pages" / "Cheatsheet.md");
            content = content.Replace("%semver%", semVer);
            content = content.Replace("%commitdate%", commitDate);
            content = content.Replace("{{ site.default_rule_prefix }}", DefaultRulePrefix);
            File.WriteAllText(cheatsheetDir / "Cheatsheet.md", content);

            CopyFile(
                RootDirectory / "assets" / "css" / "CheatSheet.css",
                cheatsheetDir / "style.css");

            CopyDirectoryRecursively(
                RootDirectory / "assets" / "images",
                cheatsheetDir / "Assets" / "Images");
        });

    Target BuildHtml => _ => _
        .DependsOn(Compile, CompileCheatsheet)
        .Executes(() =>
        {
            var pandoc = ResolvePandoc();

            ProcessTasks.StartProcess(
                    pandoc,
                    "CSharpCodingGuidelines.md -f markdown_phpextra -s -o ../CSharpCodingGuidelines.htm --embed-resources --standalone",
                    workingDirectory: ArtifactsDirectory / "Guidelines")
                .AssertZeroExitCode();

            ProcessTasks.StartProcess(
                    pandoc,
                    "Cheatsheet.md -f markdown+markdown_in_html_blocks -s -o ../CSharpCodingGuidelinesCheatsheet.htm --embed-resources --standalone",
                    workingDirectory: ArtifactsDirectory / "Cheatsheet")
                .AssertZeroExitCode();
        });

    Target LaunchWebsite => _ => _
        .Executes(() =>
        {
            EnsureRubyInstalled();

            ProcessTasks.StartProcess("gem", "install bundler", workingDirectory: RootDirectory)
                .AssertZeroExitCode();

            ProcessTasks.StartProcess("bundle", "install", workingDirectory: RootDirectory)
                .AssertZeroExitCode();

            (RootDirectory / "_site").CreateOrCleanDirectory();

            ProcessTasks.StartProcess("bundle", "exec jekyll serve --incremental", workingDirectory: RootDirectory)
                .WaitForExit();
        });

    string ResolvePandoc()
    {
        if (cachedPandocPath is not null)
            return cachedPandocPath;

        var exeName = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? "pandoc.exe" : "pandoc";
        var cacheDir = RootDirectory / ".nuke" / "temp" / "tools" / "pandoc" / PandocVersion;

        var existing = Directory.Exists(cacheDir)
            ? Directory.GetFiles(cacheDir, exeName, SearchOption.AllDirectories).FirstOrDefault()
            : null;

        if (existing is not null)
        {
            cachedPandocPath = existing;
            return cachedPandocPath;
        }

        var (assetName, _) = GetPandocAsset();
        var downloadUrl = $"https://github.com/jgm/pandoc/releases/download/{PandocVersion}/{assetName}";

        Log.Information("Downloading Pandoc {Version} from {Url}...", PandocVersion, downloadUrl);

        cacheDir.CreateOrCleanDirectory();
        var archivePath = cacheDir / assetName;

        using (var client = new HttpClient { Timeout = TimeSpan.FromMinutes(5) })
        {
            var bytes = client.GetByteArrayAsync(downloadUrl).GetAwaiter().GetResult();
            File.WriteAllBytes(archivePath, bytes);
        }

        if (assetName.EndsWith(".zip"))
            ZipFile.ExtractToDirectory(archivePath, cacheDir);
        else
            ProcessTasks.StartProcess("tar", $"-xzf \"{archivePath}\" -C \"{cacheDir}\"").AssertZeroExitCode();

        File.Delete(archivePath);

        cachedPandocPath = Directory.GetFiles(cacheDir, exeName, SearchOption.AllDirectories).First();

        if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            ProcessTasks.StartProcess("chmod", $"+x \"{cachedPandocPath}\"").AssertZeroExitCode();

        return cachedPandocPath;
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

    string BuildCategorySection(string category)
    {
        var ruleFiles = Directory
            .GetFiles(RootDirectory / "_rules", "*.md")
            .OrderBy(f => f);

        var content = new StringBuilder();

        foreach (var ruleFile in ruleFiles)
        {
            var rule = File.ReadAllText(ruleFile);

            if (!Regex.IsMatch(rule, $@"---(.|\n)*rule_category\:\s*{Regex.Escape(category)}", RegexOptions.Singleline))
                continue;

            var ruleTitle = ExtractFrontmatterField(rule, "title");
            var ruleSeverity = ExtractFrontmatterField(rule, "severity");
            var ruleId = ExtractFrontmatterField(rule, "rule_id");
            var customPrefix = ExtractFrontmatterField(rule, "custom_prefix");
            var ruleIdPrefix = string.IsNullOrEmpty(customPrefix)
                ? "{{ site.default_rule_prefix }}"
                : customPrefix;

            var severityImg = string.IsNullOrEmpty(ruleSeverity)
                ? ""
                : $" <img src=\"assets/images/{ruleSeverity}.png\" />";

            content.AppendLine($"<div id=\"{ruleIdPrefix}{ruleId}\"></div>### {ruleTitle} ({ruleIdPrefix}{ruleId}){severityImg}");
            content.AppendLine();
            content.AppendLine(StripFrontmatter(rule));
        }

        return content.ToString();
    }

    static void EnsureRubyInstalled()
    {
        try
        {
            var check = ProcessTasks.StartProcess("ruby", "--version");
            check.WaitForExit();

            if (check.ExitCode == 0)
            {
                Log.Information("Ruby found: {Version}", string.Join("", check.Output.Select(o => o.Text)));
                return;
            }
        }
        catch
        {
            // Not found — fall through to install
        }

        Log.Information("Ruby 3.3 not found. Installing...");

        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            ProcessTasks.StartProcess(
                    "winget",
                    "install RubyInstallerTeam.RubyWithDevKit.3.3 --silent --accept-package-agreements --accept-source-agreements")
                .AssertZeroExitCode();
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            ProcessTasks.StartProcess("sudo", "apt-get install -y ruby-full ruby-bundler build-essential")
                .AssertZeroExitCode();
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
        {
            ProcessTasks.StartProcess("brew", "install ruby@3.3")
                .AssertZeroExitCode();
        }
        else
        {
            Assert.Fail("Unsupported OS. Install Ruby 3.3 manually and re-run.");
        }
    }

    static string StripFrontmatter(string content) =>
        Regex.Replace(content, @"---\r?\n(.|\r?\n)+?---\r?\n", "");

    static string ExtractFrontmatterField(string content, string fieldName)
    {
        var match = Regex.Match(content, $@"---(.|\n)*?{Regex.Escape(fieldName)}\:\s*(.+)", RegexOptions.Singleline);
        return match.Success ? match.Groups[2].Value.Trim() : string.Empty;
    }

    static void CopyFile(string source, string target)
    {
        Directory.CreateDirectory(Path.GetDirectoryName(target)!);
        File.Copy(source, target, overwrite: true);
    }

    static void CopyDirectoryRecursively(string source, string target)
    {
        Directory.CreateDirectory(target);

        foreach (var file in Directory.GetFiles(source))
            File.Copy(file, Path.Combine(target, Path.GetFileName(file)), overwrite: true);

        foreach (var dir in Directory.GetDirectories(source))
            CopyDirectoryRecursively(dir, Path.Combine(target, Path.GetFileName(dir)));
    }
}
