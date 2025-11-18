# How to Add Custom Pillars of Creation Icon

## Option 1: Use an Online ICO Converter (Easiest)

1. **Find a Pillars of Creation image:**
   - Go to: https://www.nasa.gov/image-gallery/
   - Search for "Pillars of Creation"
   - Download a high-quality image

2. **Convert to ICO format:**
   - Go to: https://convertio.co/png-ico/
   - Or: https://www.icoconverter.com/
   - Upload your nebula image
   - Select size: 256x256 or 128x128
   - Download as `nebula-icon.ico`

3. **Save the icon:**
   - Save `nebula-icon.ico` in your project folder:
     `C:\Mac\Home\Desktop\Repos\YT MP3\nebula-icon.ico`

4. **Update the shortcut:**
   - Right-click the desktop shortcut
   - Select "Properties"
   - Click "Change Icon..."
   - Click "Browse..."
   - Navigate to: `C:\Mac\Home\Desktop\Repos\YT MP3\nebula-icon.ico`
   - Click OK, then Apply

## Option 2: Automatic with PowerShell Script

Once you have the `nebula-icon.ico` file in the project folder:

1. Run: `powershell -ExecutionPolicy Bypass -File Create-Desktop-Shortcut.ps1`
2. The script will automatically use the icon if it finds it

## Option 3: Use Windows Built-in Icons

Right-click shortcut → Properties → Change Icon → Browse to:
```
C:\Windows\System32\shell32.dll
```
Look through the icons for something space-themed (though limited options).

## Recommended Pillars of Creation Images

**NASA Official:**
- https://science.nasa.gov/mission/hubble/science/explore-the-night-sky/hubble-messier-catalog/messier-16/
- https://webbtelescope.org/contents/media/images/2022/052/01GF423GBQSK6ANC89NTFJW8VM

**Download, convert to ICO, and use!**

## What the Icon Does

- Makes your shortcut look professional
- Instantly recognizable on your desktop
- Matches the cosmic theme of the GUI 🌌

---

**Note:** Once you have the icon file, I can update the PowerShell script to automatically set it when creating the shortcut.

