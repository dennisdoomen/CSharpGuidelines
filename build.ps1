#!/usr/bin/env pwsh
[CmdletBinding()]
param(
    [Parameter(Position = 0, ValueFromRemainingArguments)] [string[]] $BuildArguments
)

Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'

$BuildProjectFile = "$PSScriptRoot/build/_build.csproj"

dotnet run --project $BuildProjectFile -- $BuildArguments
