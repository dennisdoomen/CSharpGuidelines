properties {
    $BaseDirectory = Resolve-Path ..     
    $ArtifactsDirectory = "$BaseDirectory\Artifacts\"     
	$LibDir = "$BaseDirectory\Lib"
	$SrcDir = "$BaseDirectory\Src\"
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
	$files = (dir $SrcDir\Guidelines\*.md)

	if (!(Test-Path -Path "$ArtifactsDirectory\Guidelines")) {
		New-Item -ItemType Directory -Force -Path "$ArtifactsDirectory\Guidelines"
	}

	$outfile = "$ArtifactsDirectory\Guidelines\CSharpCodingGuidelines.md"
		
	$files | %{
		Write-Host "Including " $_.FullName
		(Get-Content $_.FullName).replace('%semver%', $script:Semver).replace('%commitdate%', $script:CommitDate) |  Add-Content $outfile
	}
	
	Copy-Item -Path "$SrcDir\Guidelines\style.css" -Destination "$ArtifactsDirectory\Guidelines" -recurse -Force
	Copy-Item -Path "$SrcDir\Guidelines\Images" -Destination "$ArtifactsDirectory\Guidelines" -recurse -Force
}

task CompileCheatsheet {
	$files = (dir $SrcDir\Guidelines\*.md)

	if (!(Test-Path -Path "$ArtifactsDirectory\Cheatsheet\")) {
		New-Item -ItemType Directory -Force -Path "$ArtifactsDirectory\Cheatsheet\"
	}

	$outfile = "$ArtifactsDirectory\Cheatsheet\Cheatsheet.md"
	
	(Get-Content "$SrcDir\Cheatsheet\Cheatsheet.md").replace('%semver%', $script:Semver).replace('%commitdate%', $script:CommitDate) |  Add-Content $outfile
		
	Copy-Item -Path "$SrcDir\Cheatsheet\style.css" -Destination "$ArtifactsDirectory\Cheatsheet" -recurse -Force
	Copy-Item -Path "$SrcDir\Cheatsheet\Images" -Destination "$ArtifactsDirectory\Cheatsheet" -recurse -Force
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