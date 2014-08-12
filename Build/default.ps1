properties {
    $BaseDirectory = Resolve-Path ..     
}

task default -depends Clean, Compile

task Clean {	
    TeamCity-Block "Removing build output from previous runs" {
		Get-ChildItem $BaseDirectory\Guidelines CSharp*.md | ForEach { Remove-Item $_.FullName }
    }
}

task Compile {
    TeamCity-Block "Compiling Markdown file from individual section" {
		$files = (dir $BaseDirectory\Guidelines\*.md)
		$outfile = "$BaseDirectory\Guidelines\CSharpCodingGuidelines.md"

		$files | %{
			Write-Host "Including " + $_.FullName
		    Get-Content $_.FullName | Add-Content $outfile
		}
		
		Write-Host "Opening the output file using its default program"
		& $outfile
    }
}