properties {
    $BaseDirectory = Resolve-Path ..
    $ArtifactsDirectory = "$BaseDirectory\Artifacts\"
	$LibDir = "$BaseDirectory\Lib"
	$defaultRulePrefix = "AV"
}

task default -depends Clean, ExtractVersionsFromGit, Compile, CompileCheatsheet, BuildHtml

task Clean {
	if (Test-Path $ArtifactsDirectory) {
		Get-ChildItem $ArtifactsDirectory | ForEach { Remove-Item $_.FullName -Recurse -Force }
	}
}

task ExtractVersionsFromGit {

        $json = . "$LibDir\GitVersion.exe"

        if ($LASTEXITCODE -eq 0) {
            $version = (ConvertFrom-Json ($json -join "`n"));

            $script:SemVer = $version.SemVer;
            $script:CommitDate = ([datetime]$version.CommitDate).ToString("MMMM d, yyyy");
        }
        else {
            Write-Output $json -join "`n";
        }
}

task Compile {
	$files = @(
		"$BaseDirectory\_pages\0000_CoverAndStyles.md",
		"$BaseDirectory\_includes\0001_Introduction.md",
		"$BaseDirectory\_pages\1000_ClassDesignGuidelines.md",
		"$BaseDirectory\_pages\1100_MemberDesignGuidelines.md",
		"$BaseDirectory\_pages\1200_MiscellaneousDesignGuidelines.md",
		"$BaseDirectory\_pages\1500_MaintainabilityGuidelines.md",
		"$BaseDirectory\_pages\1700_NamingGuidelines.md",
		"$BaseDirectory\_pages\1800_PerformanceGuidelines.md",
		"$BaseDirectory\_pages\2200_FrameworkGuidelines.md",
		"$BaseDirectory\_pages\2300_DocumentationGuidelines.md",
		"$BaseDirectory\_pages\2400_LayoutGuidelines.md",
		"$BaseDirectory\_pages\9999_ResourcesAndLinks.md"
	)

	if (!(Test-Path -Path "$ArtifactsDirectory\Guidelines")) {
		New-Item -ItemType Directory -Force -Path "$ArtifactsDirectory\Guidelines"
	}

	$outfile = "$ArtifactsDirectory\Guidelines\CSharpCodingGuidelines.md"

	foreach ($file in $files) {
		$rawContent = Get-Content $file | Out-String

		$rawContent = $rawContent.replace('%semver%', $script:Semver)
		$rawContent = $rawContent.replace('%commitdate%', $script:CommitDate)
		$rawContent = $rawContent.replace('![](/assets', '![](assets')

		# Extract the title of the section from the Frontmatter block
		$title = ""
		if ($rawContent -match "---(.|\n)*title\: (.+)") {
			$title = $Matches[2].Trim()
		}
		# Extract the category from the Frontmatter block
		$category = ""
		if ($rawContent -match "---(.|\n)*rule_category\: (.+)") {
			$category = $Matches[2].Trim()
		}

		# Remove the entire Frontmatter block
		$rawContent = ($rawContent -replace '---\r?\n(.|\r?\n)+?---\r?\n', "")

		# Extract the category from the Frontmatter block
		if ([string]::IsNullOrEmpty($category)) {
			Write-Host "Including " $file
			$content = $rawContent
		} else {
			Write-Host "Including rules of category " $category
			$content = ""
			foreach ($ruleFile in (Get-ChildItem "$BaseDirectory\_rules\*")) {
				$rule = Get-Content $ruleFile.FullName | Out-String
				if ($rule -match "---(.|\n)*rule_category\: $category") {
					# Extract the title of the rule from the Frontmatter block
					$ruleTitle = ""
					if ($rule -match "---(.|\n)*title\: (.+)") {
						$ruleTitle = $Matches[2].Trim()
					}

					# Extract the severity of the rule from the Frontmatter block
					$ruleSeverity = ""
					if ($rule -match "---(.|\n)*severity\: (.+)") {
						$ruleSeverity = $Matches[2].Trim()
					}

					# Extract the id of the rule from the Frontmatter block
					$ruleId = ""
					if ($rule -match "---(.|\n)*rule_id\: (.+)") {
						$ruleId = $Matches[2].Trim()
					}

					# Extract the id prefix of the rule from the Frontmatter block
					$ruleIdPrefix = "{{ site.default_rule_prefix }}"
					if ($rule -match "---(.|\n)*custom_prefix\: (.+)") {
						$ruleIdPrefix = $Matches[2].Trim()
					}

					$content += "<div id=`"${ruleIdPrefix}${ruleId}`"></div>### $ruleTitle (${ruleIdPrefix}${ruleId}) <img src=`"assets/images/$ruleSeverity.png`" />`n"

					# Add rule content without Frontmatter
					$content += ($rule -replace '---\r?\n(.|\r?\n)+?---\r?\n', "")
					$content += "`n"
				}
			}
		}

		# Replace rule prefix variable
		$content =  ($content -replace '{{ site.default_rule_prefix }}', $defaultRulePrefix)

		# Replace cross-page relative links with local links (since everything becomes a single HTML)
		$content = ($content -replace '\(\/.+?(#\w+)\)', '($1)')

		if ($title) {
			$content = "<h1>$title</h1>`n" + $content;
		}

		Add-Content -Path $outfile $content
	}

	Copy-Item -Path "$BaseDirectory\Assets\css\guidelines.css" -Destination "$ArtifactsDirectory\Guidelines\style.css" -recurse -Force
	Copy-Item -Path "$BaseDirectory\Assets\Images\" -Destination "$ArtifactsDirectory\Guidelines\Assets\Images" -recurse -Force
}

task CompileCheatsheet {
	if (!(Test-Path -Path "$ArtifactsDirectory\Cheatsheet\")) {
		New-Item -ItemType Directory -Force -Path "$ArtifactsDirectory\Cheatsheet\"
	}

	$outfile = "$ArtifactsDirectory\Cheatsheet\Cheatsheet.md"

	$content = Get-Content "$BaseDirectory\_pages\Cheatsheet.md"
	$content = ($content -replace '%semver%', $script:Semver)
	$content = ($content -replace '%commitdate%', $script:CommitDate)
	$content = ($content -replace '{{ site.default_rule_prefix }}', $defaultRulePrefix)
	Add-Content $outfile $content

	Copy-Item -Path "$BaseDirectory\assets\css\CheatSheet.css" -Destination "$ArtifactsDirectory\Cheatsheet\style.css" -recurse -Force
	Copy-Item -Path "$BaseDirectory\assets\Images" -Destination "$ArtifactsDirectory\Cheatsheet\Assets\Images" -recurse -Force
}

task BuildHtml {

	$PreviousPwd = $PWD;

	try
	{
		Set-Location "$ArtifactsDirectory\Guidelines"

		$outfile = "$ArtifactsDirectory\CSharpCodingGuidelines.htm"

		if (Test-Path $outfile) {
			Remove-Item $outfile
		}

		& "$LibDir\Pandoc\pandoc.exe" CSharpCodingGuidelines.md -f markdown_phpextra -s -o $outfile --self-contained

		Set-Location "$ArtifactsDirectory\Cheatsheet\"

		$outfile = "$ArtifactsDirectory\CSharpCodingGuidelinesCheatsheet.htm"

		if (Test-Path $outfile) {
			Remove-Item $outfile
		}

		& "$LibDir\Pandoc\pandoc.exe" Cheatsheet.md -f markdown+markdown_in_html_blocks -s -o $outfile --self-contained
	}
	finally
	{
		Set-Location $PreviousPwd
	}
}