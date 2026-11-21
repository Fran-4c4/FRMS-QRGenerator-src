<#
.SYNOPSIS
Builds a distributable FRMS QR Generator package.

.PARAMETER Runtime
Target .NET runtime identifier. Defaults to win-x64. Invalid identifiers are
rejected by dotnet publish.

.PARAMETER Configuration
Build configuration. The closed domain is Release or Debug; Release is the
authoritative default.

.PARAMETER FrameworkDependent
Produces a smaller package that requires the matching .NET Desktop Runtime.
When omitted, the authoritative default is a self-contained single executable.
#>
[CmdletBinding()]
param(
    [Parameter()] [string] $Runtime = 'win-x64',
    [Parameter()] [ValidateSet('Release', 'Debug')] [string] $Configuration = 'Release',
    [Parameter()] [switch] $FrameworkDependent
)

$ErrorActionPreference = 'Stop'
$repositoryRoot = [System.IO.Path]::GetFullPath((Join-Path $PSScriptRoot '..'))
$projectPath = Join-Path $repositoryRoot 'FRMS-QRGenerator\FRMS-QRGenerator.csproj'
$outputPath = Join-Path $repositoryRoot "artifacts\publish\$Runtime"
$publishLockPath = Join-Path $repositoryRoot "artifacts\publish-locks\$Runtime.packages.lock.json"
$dotnet = (Get-Command dotnet -ErrorAction Stop).Source
$selfContained = -not $FrameworkDependent

$publishRoot = [System.IO.Path]::GetFullPath((Join-Path $repositoryRoot 'artifacts\publish'))
$resolvedOutputPath = [System.IO.Path]::GetFullPath($outputPath)
$requiredPrefix = $publishRoot.TrimEnd([System.IO.Path]::DirectorySeparatorChar) +
    [System.IO.Path]::DirectorySeparatorChar
if (-not $resolvedOutputPath.StartsWith(
        $requiredPrefix,
        [System.StringComparison]::OrdinalIgnoreCase)) {
    throw "Refusing to clean a publish path outside $publishRoot."
}

& (Join-Path $PSScriptRoot 'Test.ps1')
if ($LASTEXITCODE -ne 0) {
    exit $LASTEXITCODE
}

if (Test-Path -LiteralPath $resolvedOutputPath) {
    Remove-Item -LiteralPath $resolvedOutputPath -Recurse -Force
}

New-Item -ItemType Directory -Force -Path (Split-Path $publishLockPath) | Out-Null
& $dotnet restore $projectPath `
    --runtime $Runtime `
    --force-evaluate `
    /p:NuGetLockFilePath=$publishLockPath
if ($LASTEXITCODE -ne 0) {
    exit $LASTEXITCODE
}

& $dotnet publish $projectPath `
    --configuration $Configuration `
    --runtime $Runtime `
    --self-contained $selfContained.ToString().ToLowerInvariant() `
    --output $outputPath `
    --no-restore `
    --nologo `
    /p:NuGetLockFilePath=$publishLockPath `
    /p:PublishSingleFile=$selfContained `
    /p:IncludeNativeLibrariesForSelfExtract=$selfContained `
    /p:DebugSymbols=false `
    /p:DebugType=None `
    /p:PublishTrimmed=false

if ($LASTEXITCODE -ne 0) {
    exit $LASTEXITCODE
}

Write-Host "Package created at $outputPath" -ForegroundColor Green
