# Quick Start Guide

## Running the Application

### Option 1: Using dotnet run (Development)
```bash
dotnet run "https://www.youtube.com/watch?v=VIDEO_ID" mp3
```

### Option 2: Using the compiled executable (Production)
```bash
cd bin\Debug\net8.0
YouTubeAudioDownloader.exe "https://www.youtube.com/watch?v=VIDEO_ID" mp3
```

## Examples

### Download as MP3 (320kbps)
```bash
dotnet run "https://www.youtube.com/watch?v=jNQXAC9IVRw" mp3
```

### Download as FLAC (Lossless)
```bash
dotnet run "https://youtu.be/jNQXAC9IVRw" flac
```

### Short URL format also works
```bash
dotnet run "https://youtu.be/VIDEO_ID" mp3
```

## Output Location

Files are saved to:
- **Development**: `bin\Debug\net8.0\Downloads\`
- **Production**: `Downloads\` (next to the .exe)

## Building for Distribution

To create a standalone executable:

```bash
# Build in Release mode
dotnet build -c Release

# Or publish as self-contained
dotnet publish -c Release -r win-x64 --self-contained
```

The executable will be in:
- `bin\Release\net8.0\` (normal build)
- `bin\Release\net8.0\win-x64\publish\` (published)

## Troubleshooting

### "Missing tool" error
- Ensure `Tools\yt-dlp.exe` and `Tools\ffmpeg.exe` exist
- These should be copied automatically during build

### "Invalid URL" error
- Make sure the URL includes `youtube.com` or `youtu.be`
- Use quotes around the URL

### Download fails
- Check internet connection
- Verify the video is not private or age-restricted
- Try updating yt-dlp.exe to the latest version

## Tips

1. **Always use quotes** around URLs to avoid shell interpretation issues
2. **Check Downloads folder** for your files after completion
3. **FLAC files are larger** than MP3 but lossless quality
4. **File names** are automatically generated from video titles

