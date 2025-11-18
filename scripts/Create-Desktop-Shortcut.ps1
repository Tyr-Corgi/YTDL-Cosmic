# PowerShell script to create desktop shortcut for Cosmic GUI

$WScriptShell = New-Object -ComObject WScript.Shell
$Desktop = [System.Environment]::GetFolderPath('Desktop')
$ShortcutPath = Join-Path $Desktop "YouTube Audio Downloader - Cosmic.lnk"

$Shortcut = $WScriptShell.CreateShortcut($ShortcutPath)
$Shortcut.TargetPath = Join-Path $PSScriptRoot "Launch-CosmicGUI-Hidden.vbs"
$Shortcut.WorkingDirectory = $PSScriptRoot
$Shortcut.Description = "YouTube Audio Downloader - Cosmic Edition"

# Set custom icon if it exists
$IconPath = Join-Path $PSScriptRoot "nebula-icon.ico"
if (Test-Path $IconPath) {
    $Shortcut.IconLocation = $IconPath
    Write-Host "Custom nebula icon applied!" -ForegroundColor Magenta
} else {
    Write-Host "No custom icon found. To add one, see HOW_TO_ADD_CUSTOM_ICON.md" -ForegroundColor Yellow
}

$Shortcut.Save()

Write-Host "Desktop shortcut created successfully!" -ForegroundColor Green
Write-Host "Location: $ShortcutPath" -ForegroundColor Cyan
Write-Host ""
Write-Host "You can now double-click the shortcut on your desktop to launch the Cosmic GUI!" -ForegroundColor Yellow

