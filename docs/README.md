# YouTube Audio Downloader

A C# application that downloads audio from YouTube videos and converts them to MP3 or FLAC format.

**Available in TWO versions:**
- 🖥️ **Console Application** - Fast, lightweight command-line tool
- 🌌 **GUI Application** - Beautiful cosmic-themed graphical interface

## Features

### Both Versions
- Download audio from YouTube videos
- Support for two high-quality audio formats:
  - **MP3**: 320 kbps (high quality)
  - **FLAC**: Lossless compression
- Progress tracking during download
- Automatic file naming based on video title

### GUI Exclusive Features
- 🌌 Stunning nebula-themed interface (Pillars of Creation inspired)
- ✨ Cosmic particle effects and animations
- 🎨 Real-time URL validation with glowing visual feedback
- 🖱️ One-click folder access
- 💫 Smooth transitions and hover effects

## Requirements

- .NET 8.0 or higher
- Windows operating system
- Internet connection

## Installation

1. Clone or download this repository
2. The required tools (yt-dlp and FFmpeg) are included in the `Tools` folder
3. Build the project using `dotnet build` or open in Visual Studio

## Usage

### Console Application

Run the application from the command line with the following syntax:

```bash
YouTubeAudioDownloader.exe <YouTube_URL> <format>
```

### GUI Application

```bash
cd GUI
dotnet run
```

Or double-click `YouTubeAudioDownloader.GUI.exe` after building.

See [GUI_README.md](GUI_README.md) for detailed GUI documentation.

### Arguments

- `YouTube_URL`: A valid YouTube video URL (required)
- `format`: Audio format - either `mp3` or `flac` (required)

### Examples

Download as MP3:
```bash
YouTubeAudioDownloader.exe "https://www.youtube.com/watch?v=dQw4w9WgXcQ" mp3
```

Download as FLAC:
```bash
YouTubeAudioDownloader.exe "https://youtu.be/dQw4w9WgXcQ" flac
```

## Output

Downloaded files are automatically saved to the `Downloads` folder in the same directory as the executable. Files are named based on the video title.

## Technical Details

- **Language**: C# (.NET 8.0)
- **Core Library**: YoutubeDLSharp (wrapper for yt-dlp)
- **Audio Processing**: FFmpeg
- **Architecture**: Modular design with separate classes for configuration, downloading, and main program logic

## Project Structure

```
├── Program.cs           # Main entry point and CLI handling
├── AudioDownloader.cs   # YouTube download and conversion logic
├── Config.cs            # Configuration and path management
├── Tools/               # External executables (yt-dlp, FFmpeg)
└── Downloads/           # Output directory for downloaded files
```

## Future Enhancements

This application is designed to be easily extensible. Potential additions include:

- Graphical user interface (GUI)
- Batch processing for multiple URLs
- Playlist support
- Custom output directory selection
- Additional audio format support
- Quality selection options
- Download history tracking

## Legal Notice

Please ensure that downloading content from YouTube complies with YouTube's Terms of Service and local laws. This tool is intended for personal use with content you have the right to download.

## Troubleshooting

### Missing Tools Error
If you see an error about missing yt-dlp.exe or ffmpeg.exe, ensure these files are present in the `Tools` folder.

### Invalid URL Error
Make sure you're providing a complete YouTube URL, including the protocol (https://).

### Download Failures
- Check your internet connection
- Verify the YouTube video is accessible and not age-restricted
- Ensure you have write permissions to the Downloads folder

## License

This project uses open-source tools:
- yt-dlp: Public domain
- FFmpeg: LGPL/GPL licensed
- YoutubeDLSharp: BSD-3-Clause license

