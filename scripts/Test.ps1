[CmdletBinding()]
param()

$ErrorActionPreference = 'Stop'
$repositoryRoot = [System.IO.Path]::GetFullPath((Join-Path $PSScriptRoot '..'))
$solutionPath = Join-Path $repositoryRoot 'FRMS-QRGenerator.sln'
$dotnet = (Get-Command dotnet -ErrorAction Stop).Source

& $dotnet restore $solutionPath --locked-mode
if ($LASTEXITCODE -ne 0) {
    exit $LASTEXITCODE
}

& $dotnet test $solutionPath `
    --configuration Release `
    --no-restore `
    --nologo `
    --verbosity minimal

exit $LASTEXITCODE
