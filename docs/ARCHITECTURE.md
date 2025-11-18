# YTDL-Cosmic - Architecture Documentation

## Design Philosophy

**Vertical Slice Architecture** - Each feature slice is self-contained and can operate independently.

## Project Structure

```
YTDL-Cosmic/
├── src/
│   ├── Console/              # CLI Application Slice
│   │   ├── Program.cs        # Entry point, arg parsing, orchestration
│   │   └── *.csproj          # Project file
│   │
│   ├── GUI/                  # WPF GUI Application Slice
│   │   ├── MainWindow.xaml   # UI layout & styling
│   │   ├── MainWindow.xaml.cs # UI logic & event handlers
│   │   ├── CosmicParticles.xaml.cs # Particle effect system
│   │   ├── App.xaml          # Application resources
│   │   ├── Assets/           # Images, icons
│   │   └── *.csproj          # Project file (links to Core)
│   │
│   └── Core/                 # Shared Business Logic
│       ├── AudioDownloader.cs # YouTube download & conversion
│       └── Config.cs         # Configuration & path management
│
├── scripts/                  # Automation Scripts
│   ├── Launch-CosmicGUI.bat
│   ├── Launch-CosmicGUI-Hidden.vbs
│   ├── Create-Desktop-Shortcut.ps1
│   └── Create-Icon-From-Background.ps1
│
├── docs/                     # Documentation
├── Tools/                    # External executables (yt-dlp, ffmpeg)
└── Downloads/                # Output directory
```

## Core Components

### 1. AudioDownloader (`src/Core/AudioDownloader.cs`)

**Responsibility:** Download and convert YouTube audio

**Key Methods:**
- `DownloadAudioAsync(url, format)` - Main download logic
- `IsValidYouTubeUrl(url)` - URL validation

**Dependencies:**
- YoutubeDLSharp library
- Config class for paths

**Design:**
- Async/await for non-blocking operations
- Progress reporting via callbacks
- Comprehensive error handling

### 2. Config (`src/Core/Config.cs`)

**Responsibility:** Centralized configuration & path management

**Key Features:**
- Smart tool path resolution (checks both bin & project root)
- Downloads folder management
- Tool validation
- Filename sanitization

**Design Pattern:** Static utility class with computed properties

**Path Resolution Logic:**
```
1. Check BaseDirectory/Tools/ (for GUI in bin folder)
2. Fallback to ProjectRoot/Tools/ (for Console)
3. Return first found, or invalid if neither exists
```

### 3. Console App (`src/Console/Program.cs`)

**Responsibility:** CLI interface for YouTube downloads

**Flow:**
```
1. Parse arguments (URL, format)
2. Validate inputs
3. Check tool dependencies
4. Create AudioDownloader
5. Execute download
6. Report results
```

**Error Handling:**
- Exit code 0 = success
- Exit code 1 = failure (with message)

### 4. GUI App (`src/GUI/`)

**Architecture:** WPF with code-behind (simple, effective)

**Key Components:**

**MainWindow.xaml:**
- XAML markup for UI structure
- Custom styles & templates
- Resource definitions (colors, brushes, effects)

**MainWindow.xaml.cs:**
- Event handlers (click, text changed)
- UI state management
- Async download orchestration
- Particle system integration

**CosmicParticles.xaml.cs:**
- Custom animation system
- Canvas-based particle rendering
- Star burst & floating dust effects

**Design Decisions:**
- Code-behind (not MVVM) - appropriate for single-window app
- Direct coupling to Core - acceptable for small app
- Async UI updates via Dispatcher

## Data Flow

### Console Download Flow
```
User → Program.cs → AudioDownloader → yt-dlp → FFmpeg → File
              ↓
           Config (tool paths)
```

### GUI Download Flow
```
User → MainWindow → AudioDownloader → yt-dlp → FFmpeg → File
         ↓              ↓
    Particles      Config (tool paths)
    (visual feedback)
```

## Design Patterns

### 1. Vertical Slice Architecture
Each application slice (Console, GUI) is independent but shares Core logic.

**Benefits:**
- Easy to understand
- Clear boundaries
- Independent deployment
- Minimal coupling

### 2. Static Configuration
`Config` class uses static properties with lazy evaluation.

**Benefits:**
- Simple access from anywhere
- No DI container needed
- Computed properties for dynamic paths

### 3. Async/Await
All download operations are async.

**Benefits:**
- Non-blocking UI
- Cancellable operations
- Progress reporting

### 4. Dependency Injection (Minimal)
Only where needed - AudioDownloader injected into UI logic.

**Philosophy:** Don't over-engineer simple apps.

## Extension Points

### Adding New Format
1. Update `AudioDownloader.DownloadAudioAsync()` - add format case
2. Update UI/CLI - add format option
3. Update Config - add quality settings if needed

### Adding New Feature Slice
1. Create new folder in `src/`
2. Reference Core classes
3. Build independently

### Adding Playlist Support
1. Add method in `AudioDownloader` - `DownloadPlaylistAsync()`
2. Parse playlist URLs
3. Iterate and download

## Testing Strategy

**Current:** Manual testing (functional)

**Future:**
- Unit tests for Core logic
- Integration tests for download flow
- UI automation tests for GUI

## Performance Considerations

### Download Speed
- Limited by network & YouTube servers
- yt-dlp handles optimization
- FFmpeg conversion is fast

### GUI Responsiveness
- Async operations keep UI responsive
- Particle system is lightweight
- Progress updates throttled

### Memory
- Streams handled by yt-dlp
- No large buffers in memory
- Particles cleaned up after animation

## Security Considerations

1. **Input Validation:** URLs validated before processing
2. **Path Sanitization:** Filenames cleaned of invalid chars
3. **Tool Validation:** Checks for required executables before use
4. **No Network Code:** Delegates to yt-dlp (battle-tested)

## Future Architecture Improvements

1. **Dependency Injection:** If app grows
2. **MVVM Pattern:** If GUI becomes complex
3. **Plugin System:** For extensibility
4. **Config File:** For user preferences
5. **Logging Framework:** For troubleshooting

---

**Philosophy:** Keep it simple. Don't over-engineer. Optimize when needed.

