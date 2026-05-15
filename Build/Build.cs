using System;
using System.IO;
using System.Linq;
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

    AbsolutePath ArtifactsDirectory => RootDirectory / "Artifacts";
    AbsolutePath LibDirectory => RootDirectory / "Lib";

    // On Windows, prefer the bundled executables; on other platforms fall back to system-installed tools.
    string GitVersionTool =>
        RuntimeInformation.IsOSPlatform(OSPlatform.Windows) && (LibDirectory / "GitVersion.exe").FileExists()
            ? LibDirectory / "GitVersion.exe"
            : "gitversion";

    string PandocTool =>
        RuntimeInformation.IsOSPlatform(OSPlatform.Windows) && (LibDirectory / "Pandoc" / "pandoc.exe").FileExists()
            ? LibDirectory / "Pandoc" / "pandoc.exe"
            : "pandoc";

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
            var process = ProcessTasks.StartProcess(GitVersionTool, workingDirectory: RootDirectory);
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
            ProcessTasks.StartProcess(
                    PandocTool,
                    "CSharpCodingGuidelines.md -f markdown_phpextra -s -o ../CSharpCodingGuidelines.htm --self-contained",
                    workingDirectory: ArtifactsDirectory / "Guidelines")
                .AssertZeroExitCode();

            ProcessTasks.StartProcess(
                    PandocTool,
                    "Cheatsheet.md -f markdown+markdown_in_html_blocks -s -o ../CSharpCodingGuidelinesCheatsheet.htm --self-contained",
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
                var version = string.Join("", check.Output.Select(o => o.Text));
                Log.Information("Ruby found: {Version}", version);
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
