param(
    [Parameter(Mandatory = $true)]
    [string]$PublishDir
)

$indexPath = Join-Path $PublishDir "wwwroot\index.html"
if (-not (Test-Path $indexPath)) {
    Write-Host "FixBaseHref: '$indexPath' not found, skipping."
    exit 0
}

$content = Get-Content -Raw -Path $indexPath
$patched = $content -replace '<base href="/" />', '<base href="/hub/" />'

if ($patched -eq $content) {
    Write-Host "FixBaseHref: base href already patched or pattern not found in '$indexPath'."
} else {
    Set-Content -Path $indexPath -Value $patched -NoNewline
    Write-Host "FixBaseHref: patched base href to '/hub/' in '$indexPath'."
}

# nginx here serves plain files only (no gzip_static), so stale precompressed
# copies of index.html would carry the old base href and never get corrected.
foreach ($ext in @(".br", ".gz")) {
    $stale = "$indexPath$ext"
    if (Test-Path $stale) {
        Remove-Item $stale -Force
        Write-Host "FixBaseHref: removed stale $stale"
    }
}
