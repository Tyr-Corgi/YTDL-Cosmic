# 🌌 YouTube Audio Downloader - Cosmic GUI Edition

A stunning WPF graphical interface for the YouTube Audio Downloader with a breathtaking nebula theme inspired by the Pillars of Creation.

## ✨ Features

### Visual Design
- **Cosmic Nebula Background**: Beautiful space imagery with purple, blue, and orange nebula gradients
- **Glowing Effects**: All interactive elements feature cosmic glow effects
- **Particle System**: Floating stardust and celebration particle bursts
- **Smooth Animations**: Fade transitions and pulsing effects
- **Glass-morphism UI**: Semi-transparent, modern interface design

### Functionality
- **URL Validation**: Real-time visual feedback with color-coded glowing borders
  - 🟦 Cyan glow = Valid YouTube URL
  - 🟥 Red glow = Invalid URL
- **Format Selection**: Easy radio button selection for MP3 or FLAC
- **Progress Tracking**: Animated progress bar with status updates
- **One-Click Access**: Open Downloads folder button
- **Error Handling**: User-friendly error messages with cosmic styling

## 🚀 Running the GUI

### From Source
```bash
cd GUI
dotnet run
```

### Build Executable
```bash
cd GUI
dotnet build -c Release
```

The executable will be in: `GUI\bin\Release\net8.0-windows\YouTubeAudioDownloader.GUI.exe`

## 🎨 UI Elements

### Color Palette
- **Deep Space Black**: `#0a0e27`, `#1a1a2e`
- **Nebula Purple**: `#6b2d5c`, `#8b5a8b`
- **Cosmic Blue**: `#162447`, `#1f4068`
- **Stellar Orange**: `#ff6b35`, `#f7b731`
- **Bright Cyan**: `#00d4ff`
- **Soft White**: `#e0e1dd`

### Special Effects

**Cosmic Glow**
- All buttons and inputs have dynamic glow effects
- Glow color changes on hover and focus
- Validation states shown through color-coded glows

**Particle Effects**
- Ambient floating dust particles (continuous)
- Star burst celebration on successful download
- Particles fade in and drift across the screen

**Animations**
- Smooth fade-in/out transitions
- Pulsing button effects during download
- Color cycling on success

## 📋 How to Use

1. **Enter YouTube URL**
   - Paste or type a YouTube video URL
   - Watch for the cyan glow to confirm it's valid

2. **Select Format**
   - Choose MP3 (320kbps) for high quality
   - Choose FLAC for lossless quality

3. **Click Download**
   - Watch the cosmic progress bar
   - See the status updates
   - Enjoy the particle effects on completion!

4. **Access Your Files**
   - Click "Open Downloads Folder" button
   - Files are saved with the video title as filename

## 🛠️ Technical Details

### Architecture
- **Framework**: WPF (.NET 8.0-windows)
- **Language**: C# 12
- **Design Pattern**: Code-behind (simple, effective)
- **Shared Code**: Reuses `AudioDownloader.cs` and `Config.cs` from console app

### Project Structure
```
GUI/
├── MainWindow.xaml          # UI layout with cosmic theme
├── MainWindow.xaml.cs       # Event handlers and logic
├── CosmicParticles.xaml.cs  # Particle effect system
├── Assets/
│   └── nebula-background.jpg # Background image
└── YouTubeAudioDownloader.GUI.csproj
```

### Dependencies
- YoutubeDLSharp 1.1.2
- Shared: AudioDownloader, Config classes
- External: yt-dlp.exe, ffmpeg.exe

## 🌟 Features Showcase

### Real-Time URL Validation
The URL textbox changes color based on input:
- Empty: Default purple border
- Valid YouTube URL: Glowing cyan border
- Invalid URL: Glowing red border

### Download States
- **Ready**: Button glows with purple nebula gradient
- **Downloading**: Button shows "⏳ Downloading..." with indeterminate progress
- **Success**: Star burst particle effect + color pulsing
- **Error**: Red notification with helpful message

### Particle System
- **Ambient Dust**: Floats upward continuously (spawns every 2 seconds)
- **Star Burst**: 30 colorful particles explode from download button on success
- **Dynamic Colors**: Cyan, orange, purple, gold, and white particles

## 🎯 User Experience

### Intuitive Design
- Clear visual hierarchy
- Large, easy-to-click buttons
- Readable text with high contrast
- Status messages keep you informed

### Responsive Feedback
- Immediate visual response to interactions
- Hover effects on all interactive elements
- Disabled state styling during downloads

### Error Prevention
- URL validation before download
- Clear error messages
- Tools validation on startup

## 🔮 Future Enhancements

Possible additions:
- Drag-and-drop URL support
- Download queue/history
- Custom output location selector
- Playlist processing
- Audio preview player
- Multiple simultaneous downloads
- More particle effects and themes

## 🎨 Customization

Want to change the theme? Edit these in `MainWindow.xaml`:

**Background Image**: Line 129
```xml
<ImageBrush ImageSource="Assets/your-image.jpg" />
```

**Color Scheme**: Lines 18-45 (Resource definitions)
```xml
<DropShadowEffect Color="#YourColor" ... />
```

**Particle Colors**: `CosmicParticles.xaml.cs`, `GetRandomCosmicColor()` method

## 💡 Tips

1. **Best Results**: Use with high-resolution display for full visual impact
2. **Performance**: Particle effects are lightweight and won't slow downloads
3. **Accessibility**: All text has high contrast for readability

## 🌌 Credits

- **Nebula Background**: Space imagery (Unsplash)
- **Inspiration**: NASA's Pillars of Creation (Hubble/JWST)
- **Design**: Modern cosmic/space UI aesthetic
- **Core Functionality**: Shared with console application

---

**Enjoy your cosmic audio downloading experience!** ✨🎵🌟

