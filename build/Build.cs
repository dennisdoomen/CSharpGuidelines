using System;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.RegularExpressions;
using Nuke.Common;
using Nuke.Common.IO;
using Nuke.Common.Tooling;
using Serilog;

class Build : NukeBuild
{
    public static int Main() => Execute<Build>(x => x.Default);

    AbsolutePath ArtifactsDirectory => RootDirectory / "Artifacts";
    AbsolutePath LibDirectory => RootDirectory / "Lib";

    const string DefaultRulePrefix = "AV";

    string semVer = string.Empty;
    string commitDate = string.Empty;

    Target Clean => _ => _
        .Executes(() =>
        {
            ArtifactsDirectory.CreateOrCleanDirectory();
        });

    Target ExtractVersionsFromGit => _ => _
        .DependsOn(Clean)
        .Executes(() =>
        {
            var gitVersionExe = LibDirectory / "GitVersion.exe";
            var process = ProcessTasks.StartProcess(gitVersionExe, workingDirectory: RootDirectory);
            process.AssertZeroExitCode();

            var json = string.Join(Environment.NewLine, process.Output.Select(x => x.Text));
            using var doc = JsonDocument.Parse(json);
            semVer = doc.RootElement.GetProperty("SemVer").GetString() ?? string.Empty;
            commitDate = DateTime.Parse(
                doc.RootElement.GetProperty("CommitDate").GetString() ?? string.Empty
            ).ToString("MMMM d, yyyy");
        });

    Target Compile => _ => _
        .DependsOn(ExtractVersionsFromGit)
        .Executes(() =>
        {
            var guidelineFiles = new AbsolutePath[]
            {
                RootDirectory / "_pages"    / "0000_CoverAndStyles.md",
                RootDirectory / "_includes" / "0001_Introduction.md",
                RootDirectory / "_pages"    / "1000_ClassDesignGuidelines.md",
                RootDirectory / "_pages"    / "1100_MemberDesignGuidelines.md",
                RootDirectory / "_pages"    / "1200_MiscellaneousDesignGuidelines.md",
                RootDirectory / "_pages"    / "1500_MaintainabilityGuidelines.md",
                RootDirectory / "_pages"    / "1700_NamingGuidelines.md",
                RootDirectory / "_pages"    / "1800_PerformanceGuidelines.md",
                RootDirectory / "_pages"    / "2200_FrameworkGuidelines.md",
                RootDirectory / "_pages"    / "2300_DocumentationGuidelines.md",
                RootDirectory / "_pages"    / "2400_LayoutGuidelines.md",
                RootDirectory / "_pages"    / "9999_ResourcesAndLinks.md",
            };

            var outputDir = ArtifactsDirectory / "Guidelines";
            outputDir.CreateOrCleanDirectory();

            var outfile = outputDir / "CSharpCodingGuidelines.md";

            foreach (var file in guidelineFiles)
            {
                var rawContent = File.ReadAllText(file)
                    .Replace("%semver%", semVer)
                    .Replace("%commitdate%", commitDate)
                    .Replace("![](/assets", "![](assets");

                var title    = ExtractFrontmatterField(rawContent, "title");
                var category = ExtractFrontmatterField(rawContent, "rule_category");

                rawContent = RemoveFrontmatter(rawContent);

                string content;
                if (string.IsNullOrEmpty(category))
                {
                    Log.Information("Including {File}", (string)file);
                    content = rawContent;
                }
                else
                {
                    Log.Information("Including rules of category {Category}", category);
                    content = string.Empty;

                    foreach (var ruleFile in (RootDirectory / "_rules").GetFiles().OrderBy(f => (string)f))
                    {
                        var rule = File.ReadAllText(ruleFile);
                        if (!Regex.IsMatch(rule, $@"---(.|\n)*rule_category\: {Regex.Escape(category)}"))
                            continue;

                        var ruleTitle    = ExtractFrontmatterField(rule, "title");
                        var ruleSeverity = ExtractFrontmatterField(rule, "severity");
                        var ruleId       = ExtractFrontmatterField(rule, "rule_id");
                        var ruleIdPrefix = ExtractFrontmatterField(rule, "custom_prefix") is { Length: > 0 } cp
                            ? cp
                            : "{{ site.default_rule_prefix }}";

                        content += $"<div id=\"{ruleIdPrefix}{ruleId}\"></div>### {ruleTitle} ({ruleIdPrefix}{ruleId}) <img src=\"assets/images/{ruleSeverity}.png\" />\n";
                        content += RemoveFrontmatter(rule);
                        content += "\n";
                    }
                }

                content = content.Replace("{{ site.default_rule_prefix }}", DefaultRulePrefix);
                content = Regex.Replace(content, @"\(\/.+?(#\w+)\)", "($1)");

                if (!string.IsNullOrEmpty(title))
                    content = $"<h1>{title}</h1>\n" + content;

                File.AppendAllText(outfile, content);
            }

            (RootDirectory / "assets" / "css" / "Guidelines.css")
                .Copy(outputDir / "style.css", ExistsPolicy.FileOverwrite);
            (RootDirectory / "assets" / "images")
                .Copy(outputDir / "assets" / "images", ExistsPolicy.MergeAndOverwrite);
        });

    Target CompileCheatsheet => _ => _
        .DependsOn(ExtractVersionsFromGit)
        .Executes(() =>
        {
            var outputDir = ArtifactsDirectory / "Cheatsheet";
            outputDir.CreateOrCleanDirectory();

            var content = File.ReadAllText(RootDirectory / "_pages" / "Cheatsheet.md")
                .Replace("%semver%", semVer)
                .Replace("%commitdate%", commitDate)
                .Replace("{{ site.default_rule_prefix }}", DefaultRulePrefix);

            File.WriteAllText(outputDir / "Cheatsheet.md", content);

            (RootDirectory / "assets" / "css" / "CheatSheet.css")
                .Copy(outputDir / "style.css", ExistsPolicy.FileOverwrite);
            (RootDirectory / "assets" / "images")
                .Copy(outputDir / "assets" / "images", ExistsPolicy.MergeAndOverwrite);
        });

    Target BuildHtml => _ => _
        .DependsOn(Compile, CompileCheatsheet)
        .Executes(() =>
        {
            var pandoc = LibDirectory / "Pandoc" / "pandoc.exe";

            ProcessTasks.StartProcess(
                pandoc,
                $"CSharpCodingGuidelines.md -f markdown_phpextra -s -o \"{ArtifactsDirectory / "CSharpCodingGuidelines.htm"}\" --self-contained",
                ArtifactsDirectory / "Guidelines"
            ).AssertZeroExitCode();

            ProcessTasks.StartProcess(
                pandoc,
                $"Cheatsheet.md -f markdown+markdown_in_html_blocks -s -o \"{ArtifactsDirectory / "CSharpCodingGuidelinesCheatsheet.htm"}\" --self-contained",
                ArtifactsDirectory / "Cheatsheet"
            ).AssertZeroExitCode();
        });

    Target Default => _ => _
        .DependsOn(BuildHtml);

    static string ExtractFrontmatterField(string content, string fieldName)
    {
        var match = Regex.Match(content, $@"---(.|\n)*{Regex.Escape(fieldName)}\: (.+)");
        return match.Success ? match.Groups[2].Value.Trim() : string.Empty;
    }

    static string RemoveFrontmatter(string content) =>
        Regex.Replace(content, @"---\r?\n(.|\r?\n)+?---\r?\n", string.Empty);
}
