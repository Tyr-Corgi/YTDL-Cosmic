# Usage Examples - YouTube Audio Downloader

## Console Application Examples

### Example 1: Download MP3 from Regular Video
```bash
# Command
dotnet run "https://www.youtube.com/watch?v=jNQXAC9IVRw" mp3

# Output
===========================================
   YouTube Audio Downloader (MP3/FLAC)
===========================================

Starting download from: https://www.youtube.com/watch?v=jNQXAC9IVRw
Target format: MP3
Please wait...

✓ Download completed successfully!
File saved to: C:\Mac\Home\Desktop\Repos\YT MP3\Downloads\Me at the zoo [jNQXAC9IVRw].mp3

===========================================
   Download completed successfully! ✓
===========================================
```

### Example 2: Download FLAC from Short URL
```bash
# Command
dotnet run "https://youtu.be/qtZ0hl-unM4" flac

# Output
===========================================
   YouTube Audio Downloader (MP3/FLAC)
===========================================

Starting download from: https://youtu.be/qtZ0hl-unM4
Target format: FLAC
Please wait...

✓ Download completed successfully!
File saved to: C:\Mac\Home\Desktop\Repos\YT MP3\Downloads\Pac-man theme remix - By Arsenic1987 [qtZ0hl-unM4].flac

===========================================
   Download completed successfully! ✓
===========================================
```

### Example 3: Invalid Format Error
```bash
# Command
dotnet run "https://www.youtube.com/watch?v=test" wav

# Output
===========================================
   YouTube Audio Downloader (MP3/FLAC)
===========================================

✗ Error: Invalid format specified.
  Supported formats: mp3, flac

Usage:
  YouTubeAudioDownloader.exe <YouTube_URL> <format>
...
```

### Example 4: Invalid URL Error
```bash
# Command
dotnet run "https://example.com" mp3

# Output
===========================================
   YouTube Audio Downloader (MP3/FLAC)
===========================================

✗ Error: Invalid YouTube URL.
  Please provide a valid YouTube video URL.

  Examples:
    https://www.youtube.com/watch?v=VIDEO_ID
    https://youtu.be/VIDEO_ID
```

### Example 5: No Arguments
```bash
# Command
dotnet run

# Output
===========================================
   YouTube Audio Downloader (MP3/FLAC)
===========================================

Usage:
  YouTubeAudioDownloader.exe <YouTube_URL> <format>

Arguments:
  YouTube_URL    A valid YouTube video URL
  format         Audio format: mp3 or flac

Examples:
  YouTubeAudioDownloader.exe "https://www.youtube.com/watch?v=dQw4w9WgXcQ" mp3
  YouTubeAudioDownloader.exe "https://youtu.be/dQw4w9WgXcQ" flac

Output:
  Files are saved to the 'Downloads' folder
  MP3:  320 kbps (high quality)
  FLAC: Lossless compression
```

## GUI Application Examples

### Example 1: Basic MP3 Download

**Steps:**
1. Open GUI: `cd GUI && dotnet run`
2. Enter URL: `https://www.youtube.com/watch?v=jNQXAC9IVRw`
3. Select format: MP3 (default)
4. Click "✨ Download Audio ✨"
5. Watch progress bar
6. See star burst effect on completion

**Visual Flow:**
```
1. URL box turns CYAN (valid URL detected)
2. Click Download button
3. Button changes to "⏳ Downloading..."
4. Progress bar appears and animates
5. Status shows: "Starting download as MP3..."
6. On completion: Star burst particles explode!
7. Message box: "🌟 Download Complete!"
```

### Example 2: FLAC Download with Folder Access

**Steps:**
1. Enter URL: `https://youtu.be/qtZ0hl-unM4`
2. Select FLAC radio button
3. Click Download
4. Wait for completion
5. Click "📁 Open Downloads Folder"
6. File manager opens showing your FLAC file

**Result:**
```
Downloads/
  └── Pac-man theme remix - By Arsenic1987 [qtZ0hl-unM4].flac
```

### Example 3: Invalid URL Detection

**Steps:**
1. Type: `example.com`
2. Observe: URL box turns RED with red glow
3. Try to download: Error message appears
4. Fix URL to: `youtube.com/watch?v=...`
5. Observe: URL box turns CYAN with cyan glow
6. Now download works!

### Example 4: Multiple Downloads

**Steps:**
1. Download first video as MP3
2. Wait for star burst effect
3. Click OK on success message
4. Enter new URL
5. Download as FLAC
6. Click "Open Downloads Folder"
7. See both files:
   ```
   Downloads/
     ├── First video [ID1].mp3
     └── Second video [ID2].flac
   ```

## Real-World Use Cases

### Use Case 1: Music Lover
**Scenario:** Download your favorite songs in lossless quality

```bash
# Using GUI for best experience
cd GUI
dotnet run

# Download multiple songs as FLAC
1. https://youtube.com/watch?v=song1 → FLAC
2. https://youtube.com/watch?v=song2 → FLAC
3. https://youtube.com/watch?v=song3 → FLAC

# Result: High-quality music library
```

### Use Case 2: Podcast Archiver
**Scenario:** Save podcast episodes for offline listening

```bash
# Using CLI for speed
dotnet run "https://youtube.com/watch?v=podcast1" mp3
dotnet run "https://youtube.com/watch?v=podcast2" mp3
dotnet run "https://youtube.com/watch?v=podcast3" mp3

# Result: MP3 files ready for your phone
```

### Use Case 3: Audio Production
**Scenario:** Extract audio from video for editing

```bash
# Use FLAC for editing (lossless)
dotnet run "https://youtube.com/watch?v=video-id" flac

# Edit in your DAW
# Export as MP3 when done
```

### Use Case 4: Educational Content
**Scenario:** Save lectures for offline study

```bash
# Using CLI in batch script
for /f "delims=" %%i in (lecture_urls.txt) do (
    YouTubeAudioDownloader.exe "%%i" mp3
)

# Result: All lectures as MP3 files
```

## Tips and Tricks

### Tip 1: Batch Processing (CLI)
Create a batch file `download.bat`:
```batch
@echo off
cd "C:\Mac\Home\Desktop\Repos\YT MP3"
dotnet run "https://youtube.com/watch?v=video1" mp3
dotnet run "https://youtube.com/watch?v=video2" mp3
dotnet run "https://youtube.com/watch?v=video3" mp3
echo All downloads complete!
pause
```

### Tip 2: Quality Comparison
Download the same video in both formats:
```bash
dotnet run "https://youtube.com/watch?v=VIDEO_ID" mp3
dotnet run "https://youtube.com/watch?v=VIDEO_ID" flac

# Compare file sizes and quality
```

### Tip 3: Organize Downloads
After downloading, organize by type:
```
Downloads/
  ├── Music/
  │   ├── Song1.flac
  │   └── Song2.flac
  ├── Podcasts/
  │   ├── Episode1.mp3
  │   └── Episode2.mp3
  └── Lectures/
      ├── Lecture1.mp3
      └── Lecture2.mp3
```

### Tip 4: GUI Shortcuts
- Tab key: Navigate between controls
- Enter: Submit when URL box is focused
- Click anywhere in textbox to paste

### Tip 5: File Naming
Files are named automatically:
- Video title (sanitized)
- Video ID in brackets
- Extension (.mp3 or .flac)

Example: `Rick Astley - Never Gonna Give You Up [dQw4w9WgXcQ].mp3`

## Common Workflows

### Workflow 1: Quick Download (GUI)
```
1. Open GUI
2. Paste URL (Ctrl+V)
3. Click Download
4. Done!
```
⏱️ Time: ~10 seconds (+ download time)

### Workflow 2: Bulk Download (CLI)
```
1. Open command prompt
2. Navigate to project
3. Run multiple commands
4. All files in Downloads/
```
⏱️ Time: ~5 seconds per video

### Workflow 3: Quality Check
```
1. Download as MP3
2. Download as FLAC
3. Compare in audio player
4. Keep preferred format
5. Delete other file
```

### Workflow 4: Mobile Transfer
```
1. Download as MP3 (smaller files)
2. Connect phone
3. Copy from Downloads/ to phone
4. Enjoy on the go!
```

## Troubleshooting Examples

### Problem: Download Fails
**Solution:**
```bash
# Check URL
✗ https://example.com  # Wrong
✓ https://youtube.com/watch?v=VIDEO_ID  # Correct

# Check format
✗ wav  # Not supported
✓ mp3  # Supported
✓ flac  # Supported
```

### Problem: File Not Found
**Solution:**
```bash
# Check Downloads folder
cd Downloads
dir

# Or use GUI "Open Downloads Folder" button
```

### Problem: Slow Download
**Reason:** Large video or slow connection
**Monitor:** Watch progress in GUI for real-time updates

### Problem: Tools Missing
**Solution:**
```bash
# Check Tools folder
dir Tools

# Should see:
# - yt-dlp.exe
# - ffmpeg.exe
```

## Advanced Examples

### Example 1: Verify Download
```powershell
# After download, check file
cd Downloads
dir "*.mp3"  # List all MP3 files
dir "*.flac" # List all FLAC files
```

### Example 2: Script Integration
```powershell
# PowerShell script
$urls = @(
    "https://youtube.com/watch?v=video1",
    "https://youtube.com/watch?v=video2"
)

foreach ($url in $urls) {
    & dotnet run $url mp3
}
```

### Example 3: Format Conversion
```bash
# Download as FLAC
dotnet run "URL" flac

# Later convert to MP3 using FFmpeg
ffmpeg -i "song.flac" -b:a 320k "song.mp3"
```

## Performance Examples

### File Sizes (Real Tests)
| Duration | MP3 (320kbps) | FLAC (Lossless) |
|----------|---------------|-----------------|
| 19 sec   | ~120 KB       | ~1.4 MB         |
| 3 min    | ~7 MB         | ~27 MB          |
| 10 min   | ~23 MB        | ~90 MB          |

### Download Times (Approximate)
| Duration | Download Time* |
|----------|----------------|
| < 1 min  | 5-10 sec       |
| 3-5 min  | 15-30 sec      |
| 10+ min  | 45-90 sec      |

*Depends on internet speed and YouTube server

---

**Happy downloading! 🎵✨**

