# Session Summary: Playlist & Folder Picker Features

## Project Status
YouTube Audio Downloader - Cosmic Edition (WPF GUI + Console app) with Pillars of Creation nebula theme.

## Completed Features in This Session

### 1. Custom Download Folder Picker
**Location:** `src/GUI/MainWindow.xaml` + `src/GUI/MainWindow.xaml.cs`
- Added folder picker UI with Browse button (cosmic themed)
- Uses `System.Windows.Forms.FolderBrowserDialog`
- Path persists during app session
- `Config.cs` updated with `SetCustomDownloadPath()` method
- Default: Project root `Downloads/` folder

### 2. Playlist Batch Download
**Location:** `src/Core/AudioDownloader.cs`
- Auto-detects playlist URLs (checks for `list=` parameter)
- Downloads each video individually (not bulk) due to yt-dlp JS runtime limitation
- Creates playlist subfolder: `[DownloadPath]/[PlaylistName]/[SongTitle].ext`
- Progress tracking: Shows "Downloading X of Y: [Song Title]"
- Counts successful vs failed downloads

**Key Implementation Details:**
```csharp
// In DownloadPlaylistAsync():
1. Fetch playlist info using RunVideoDataFetch() - gets all video URLs and titles
2. Create sanitized playlist subfolder
3. Loop through each video:
   - Temporarily set _youtubeDL.OutputFolder to playlist subfolder
   - Call RunAudioDownload() for individual video
   - Restore original OutputFolder
   - Report progress to UI
```

### 3. UI Layout Fixes
**File:** `src/GUI/MainWindow.xaml`
- Fixed overlapping elements (Download button & playlist progress text were both on Grid.Row 10)
- Added proper grid rows with spacing:
  - Row 10: Download Button
  - Row 12: Playlist Progress Text
  - Row 14: Progress Bar
  - Row 15: Open Folder Button

### 4. Fixed Console Project
**File:** `src/Console/YouTubeAudioDownloader.csproj`
- Updated to reference `src/Core/` files after vertical slice restructure
- Added links to `AudioDownloader.cs` and `Config.cs`
- Fixed Tools path: `../../Tools/`

## Key Technical Issues Resolved

### Issue 1: Playlist Download Not Starting
**Problem:** `RunVideoDownload` with YesPlaylist option was getting stuck in Error state
**Root Cause:** yt-dlp requires JavaScript runtime (Node.js) for bulk playlist downloads
**Solution:** Download videos one-by-one using individual `RunAudioDownload()` calls

### Issue 2: Files Not Appearing in Playlist Folder
**Problem:** Files downloaded to wrong location (or not at all)
**Root Cause:** `OutputFolder` wasn't being set for individual downloads
**Solution:** Temporarily change `_youtubeDL.OutputFolder` before each download, restore after

### Issue 3: Namespace Ambiguities (WPF vs Windows Forms)
**Files:** `App.xaml.cs`, `CosmicParticles.xaml.cs`, `MainWindow.xaml.cs`
**Solution:** Added type aliases:
```csharp
using WpfApplication = System.Windows.Application;
using WpfMessageBox = System.Windows.MessageBox;
using WpfColor = System.Windows.Media.Color;
using WpfPoint = System.Windows.Point;
using WpfBrush = System.Windows.Media.Brush;
```

## Current File Structure
```
YT MP3/
├── src/
│   ├── Core/
│   │   ├── AudioDownloader.cs    (Playlist support added)
│   │   └── Config.cs              (Custom path support)
│   ├── Console/
│   │   ├── Program.cs
│   │   └── YouTubeAudioDownloader.csproj (Fixed)
│   └── GUI/
│       ├── MainWindow.xaml        (Folder picker + layout fixes)
│       ├── MainWindow.xaml.cs     (Playlist logic)
│       ├── CosmicParticles.xaml.cs
│       ├── App.xaml.cs
│       └── YouTubeAudioDownloader.GUI.csproj (Windows Forms support)
├── Tools/
│   ├── yt-dlp.exe
│   └── ffmpeg.exe
├── scripts/
│   ├── Launch-CosmicGUI-Hidden.vbs
│   ├── Launch-CosmicGUI.bat
│   ├── Create-Desktop-Shortcut.ps1
│   └── nebula-icon.ico
└── Downloads/                     (Default download location)
```

## Known Limitations
1. Playlist download uses individual downloads (slower but more reliable)
2. No JavaScript runtime installed (required for some yt-dlp features)
3. Custom folder path resets on app restart (not persisted to disk)

## Testing Status
- ✅ Single video download works
- ✅ Playlist detection works (62 videos found)
- ✅ Folder picker works
- ⏳ Playlist download in progress (files should go to `X:\Music\[PlaylistName]/`)

## Next Steps if Issues Persist
1. Check console output for download errors
2. Verify files in: `[CustomFolder]/[PlaylistName]/` directory
3. Check if yt-dlp needs updating for specific video formats
4. Consider adding Node.js as optional dependency for faster playlist downloads

## Git Repository
- Remote: `git@github.com:Tyr-Corgi/YTDL-Cosmic.git`
- Branch: `main`
- Last commit: Fixed file paths after vertical slice restructure + added icon

