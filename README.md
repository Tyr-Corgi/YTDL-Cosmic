# YTDL-Cosmic 🌌
**YouTube Audio Downloader - Cosmic Edition**

A beautiful, nebula-themed YouTube audio downloader with both CLI and GUI interfaces.

## Features

- 🎵 **Download YouTube audio** in MP3 (320kbps) or FLAC (lossless)
- 🌌 **Stunning cosmic GUI** with Pillars of Creation theme
- ⚡ **Fast CLI** for power users
- ✨ **Particle effects** and smooth animations
- 🎨 **Real-time URL validation** with visual feedback

## Quick Start

### GUI (Recommended)
Double-click the desktop shortcut: **"YouTube Audio Downloader - Cosmic"**

Or run:
```bash
cd src/GUI
dotnet run
```

### CLI
```bash
cd src/Console
dotnet run "https://youtube.com/watch?v=VIDEO_ID" mp3
```

## Project Structure

```
YTDL-Cosmic/
├── src/
│   ├── Console/          # CLI application
│   ├── GUI/              # WPF cosmic-themed GUI
│   └── Core/             # Shared business logic
├── docs/                 # Documentation
├── scripts/              # Launcher scripts
├── Tools/                # yt-dlp & ffmpeg executables
└── Downloads/            # Output folder
```

## Architecture

**Vertical Slice Architecture:**
- `src/Console` - Self-contained console app slice
- `src/GUI` - Self-contained WPF GUI slice  
- `src/Core` - Shared domain logic (AudioDownloader, Config)

Each slice is independent and can be built/run separately.

## Requirements

- .NET 8.0 or higher
- Windows 10/11
- Internet connection

## Documentation

- [Quick Start Guide](docs/QUICKSTART.md)
- [GUI Documentation](docs/GUI_README.md)
- [Usage Examples](docs/USAGE_EXAMPLES.md)
- [Visual Guide](src/GUI/VISUAL_GUIDE.md)

## Tech Stack

- **Framework**: .NET 8.0 (C#)
- **GUI**: WPF with custom cosmic theme
- **Libraries**: YoutubeDLSharp
- **Tools**: yt-dlp, FFmpeg

## Downloads

Audio files save to: `Downloads/`
- MP3: 320kbps high quality
- FLAC: Lossless compression

## License

Open source project using:
- yt-dlp (Public Domain)
- FFmpeg (LGPL/GPL)
- YoutubeDLSharp (BSD-3-Clause)

---

**Enjoy your cosmic audio downloading experience!** 🎵✨🌌

