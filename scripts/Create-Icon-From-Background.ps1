# PowerShell script to create an icon from the nebula background image

Add-Type -AssemblyName System.Drawing

try {
    # Load the nebula background image
    $sourceImage = "GUI\Assets\nebula-background.jpg"
    
    if (-not (Test-Path $sourceImage)) {
        Write-Host "Error: Nebula background image not found!" -ForegroundColor Red
        exit 1
    }
    
    Write-Host "Loading nebula image..." -ForegroundColor Cyan
    $image = [System.Drawing.Image]::FromFile((Resolve-Path $sourceImage))
    
    # Create a 256x256 bitmap for the icon
    $iconSize = 256
    $bitmap = New-Object System.Drawing.Bitmap $iconSize, $iconSize
    $graphics = [System.Drawing.Graphics]::FromImage($bitmap)
    
    # Enable high quality rendering
    $graphics.InterpolationMode = [System.Drawing.Drawing2D.InterpolationMode]::HighQualityBicubic
    $graphics.SmoothingMode = [System.Drawing.Drawing2D.SmoothingMode]::HighQuality
    $graphics.PixelOffsetMode = [System.Drawing.Drawing2D.PixelOffsetMode]::HighQuality
    
    Write-Host "Resizing image to icon size..." -ForegroundColor Cyan
    
    # Draw the image scaled to fit
    $graphics.DrawImage($image, 0, 0, $iconSize, $iconSize)
    
    # Save as PNG first (ICO creation is complex in pure PowerShell)
    $pngPath = "nebula-icon-temp.png"
    $bitmap.Save($pngPath, [System.Drawing.Imaging.ImageFormat]::Png)
    
    Write-Host "Saved temporary PNG file" -ForegroundColor Green
    
    # Now we need to convert PNG to ICO - this is the tricky part
    # We'll save as PNG and use it directly (shortcuts can use PNG in some cases)
    # Or we convert using a simple method
    
    # Create a simple ICO file (single size)
    $iconPath = "nebula-icon.ico"
    
    # Use the bitmap to create an icon
    $icon = [System.Drawing.Icon]::FromHandle($bitmap.GetHicon())
    $fileStream = [System.IO.FileStream]::new($iconPath, [System.IO.FileMode]::Create)
    $icon.Save($fileStream)
    $fileStream.Close()
    
    Write-Host "Icon created successfully: $iconPath" -ForegroundColor Green
    
    # Cleanup
    $graphics.Dispose()
    $bitmap.Dispose()
    $image.Dispose()
    $icon.Dispose()
    
    if (Test-Path $pngPath) {
        Remove-Item $pngPath -Force
    }
    
    Write-Host ""
    Write-Host "Nebula icon created! Now recreating desktop shortcut..." -ForegroundColor Yellow
    
    # Recreate the shortcut with the new icon
    & "$PSScriptRoot\Create-Desktop-Shortcut.ps1"
    
} catch {
    Write-Host "Error creating icon: $_" -ForegroundColor Red
    Write-Host "You may need to manually create the icon using an online converter." -ForegroundColor Yellow
}

