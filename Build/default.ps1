properties {
    $BaseDirectory = Resolve-Path ..     
    $ArtifactsDirectory = "$BaseDirectory\Artifacts\"     
	$LibDir = "$BaseDirectory\Lib"
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
		Write-Host "Including " $file
		$content = Get-Content $file | Out-String
		
		$content = $content.replace('%semver%', $script:Semver)
		$content = $content.replace('%commitdate%', $script:CommitDate) 
		$content = $content.replace('![](/assets', '![](assets') 

		if ($content -match "---(.|\n)*title\: (.+)") {
			$title = $Matches[2]
		}

		$content = ($content -replace "---(.|\n)+---\n*", "")

		if ($title) {
			$content = "<h1>$title</h1>\r\n" + $content;
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
	
	(Get-Content "$BaseDirectory\_pages\Cheatsheet.md").replace('%semver%', $script:Semver).replace('%commitdate%', $script:CommitDate) |  Add-Content $outfile
		
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