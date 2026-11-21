[CmdletBinding()]
param()

$ErrorActionPreference = 'Stop'
$repositoryRoot = [System.IO.Path]::GetFullPath((Join-Path $PSScriptRoot '..'))

if (-not (Test-Path -LiteralPath (Join-Path $repositoryRoot '.git'))) {
    throw 'The script must run from a Git checkout of FRMS QR Generator.'
}

$candidateFiles = @(
    git -C $repositoryRoot ls-files --cached --others --exclude-standard |
        Where-Object { Test-Path -LiteralPath (Join-Path $repositoryRoot $_) }
)
if ($LASTEXITCODE -ne 0) {
    throw 'Unable to enumerate repository files.'
}

$failures = [System.Collections.Generic.List[object]]::new()

function Add-Failure {
    param(
        [Parameter(Mandatory)] [string] $Category,
        [Parameter(Mandatory)]
        [AllowEmptyCollection()]
        [string[]] $Paths
    )

    $normalizedPaths = @($Paths | Where-Object { $_ } | Sort-Object -Unique)
    if ($normalizedPaths.Count -gt 0) {
        $failures.Add([pscustomobject]@{
            Category = $Category
            Paths = $normalizedPaths
        })
    }
}

$requiredFiles = @(
    'README.md',
    'LICENSE',
    'CHANGELOG.md',
    'CONTRIBUTING.md',
    'SECURITY.md',
    'THIRD_PARTY_NOTICES.md',
    'docs/architecture.md',
    'docs/security.md',
    'docs/screenshots/main-window.png',
    'assets/app-icon.png',
    'FRMS-QRGenerator/app.ico'
)
Add-Failure -Category 'Missing publication documentation' -Paths @(
    $requiredFiles | Where-Object {
        -not (Test-Path -LiteralPath (Join-Path $repositoryRoot $_))
    }
)

$sensitiveExtensions = @('.key', '.pem', '.pvk', '.pfx', '.p12', '.crt', '.cer', '.snk')
Add-Failure -Category 'Keys or certificates' -Paths @(
    $candidateFiles | Where-Object {
        $sensitiveExtensions -contains [System.IO.Path]::GetExtension($_).ToLowerInvariant()
    }
)

$generatedBinaryExtensions = @('.dll', '.exe', '.msi', '.cab', '.pdb', '.nupkg')
Add-Failure -Category 'Generated or vendored binaries' -Paths @(
    $candidateFiles | Where-Object {
        $generatedBinaryExtensions -contains [System.IO.Path]::GetExtension($_).ToLowerInvariant()
    }
)

$textExtensions = @('.cs', '.config', '.xml', '.json', '.csproj', '.props', '.ps1', '.md', '.yml', '.yaml')
$sensitivePatterns = @(
    '(?i)(?:password|passwd|pwd)\s*=\s*["''][^"'']+["'']',
    '(?i)(?:api[_-]?key|client[_-]?secret)\s*[:=]\s*["''][^"'']+["'']',
    '(?i)[A-Z]:\\(?:Users|IndronF)\\'
)
$sourceFindings = [System.Collections.Generic.List[string]]::new()

foreach ($relativePath in $candidateFiles) {
    if ($textExtensions -notcontains [System.IO.Path]::GetExtension($relativePath).ToLowerInvariant()) {
        continue
    }

    $absolutePath = Join-Path $repositoryRoot $relativePath
    $fileInfo = Get-Item -LiteralPath $absolutePath
    if ($fileInfo.Length -gt 2MB) {
        $sourceFindings.Add("$relativePath [not scanned: file exceeds 2 MiB]")
        continue
    }

    $content = [System.IO.File]::ReadAllText($absolutePath)
    foreach ($pattern in $sensitivePatterns) {
        if ([System.Text.RegularExpressions.Regex]::IsMatch($content, $pattern)) {
            $sourceFindings.Add($relativePath)
            break
        }
    }
}
Add-Failure -Category 'Potential secrets or private local paths' -Paths @($sourceFindings)

if ($failures.Count -eq 0) {
    Write-Host 'Publication readiness checks passed.' -ForegroundColor Green
    exit 0
}

Write-Host "Publication readiness checks failed in $($failures.Count) categories." -ForegroundColor Red
foreach ($failure in $failures) {
    Write-Host "`n[$($failure.Category)] $($failure.Paths.Count) item(s)" -ForegroundColor Yellow
    foreach ($path in @($failure.Paths | Select-Object -First 20)) {
        Write-Host " - $path"
    }
    if ($failure.Paths.Count -gt 20) {
        Write-Host " - ... and $($failure.Paths.Count - 20) more"
    }
}

exit 1
