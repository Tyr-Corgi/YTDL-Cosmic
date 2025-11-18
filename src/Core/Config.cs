using System;
using System.IO;

namespace YouTubeAudioDownloader
{
    /// <summary>
    /// Configuration class for managing application settings and paths
    /// </summary>
    public static class Config
    {
        // Get the base directory where the application is running
        private static readonly string BaseDirectory = AppDomain.CurrentDomain.BaseDirectory;

        // Get the project root directory (where the .csproj file is)
        private static readonly string ProjectRoot = Path.GetFullPath(Path.Combine(BaseDirectory, "..", "..", ".."));

        // Custom download path set by user (null = use default)
        private static string? _customDownloadPath = null;

        // Paths to external tools - try local first (for GUI), then project root (for console)
        public static string YtDlpPath => GetToolPath("yt-dlp.exe");
        public static string FFmpegPath => GetToolPath("ffmpeg.exe");

        // Output directory for downloaded files (custom or default in project root)
        public static string DownloadsPath => _customDownloadPath ?? Path.Combine(ProjectRoot, "Downloads");

        private static string GetToolPath(string toolName)
        {
            // First check in the bin directory (where GUI copies them)
            string localPath = Path.Combine(BaseDirectory, "Tools", toolName);
            if (File.Exists(localPath))
                return localPath;

            // Fall back to project root (for console app)
            return Path.Combine(ProjectRoot, "Tools", toolName);
        }

        // Audio quality settings
        public const string Mp3Quality = "320";  // 320 kbps for MP3
        public const string FlacQuality = "best"; // Lossless for FLAC

        /// <summary>
        /// Validates that all required tools are present
        /// </summary>
        /// <returns>True if all tools are found, false otherwise</returns>
        public static bool ValidateTools(out string missingTool)
        {
            if (!File.Exists(YtDlpPath))
            {
                missingTool = "yt-dlp.exe";
                return false;
            }

            if (!File.Exists(FFmpegPath))
            {
                missingTool = "ffmpeg.exe";
                return false;
            }

            missingTool = string.Empty;
            return true;
        }

        /// <summary>
        /// Ensures the downloads directory exists
        /// </summary>
        public static void EnsureDownloadsDirectory()
        {
            if (!Directory.Exists(DownloadsPath))
            {
                Directory.CreateDirectory(DownloadsPath);
            }
        }

        /// <summary>
        /// Sets a custom download path chosen by the user
        /// </summary>
        /// <param name="path">Full path to the custom download folder</param>
        public static void SetCustomDownloadPath(string? path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                _customDownloadPath = null;
                return;
            }

            // Validate the path exists
            if (!Directory.Exists(path))
            {
                throw new DirectoryNotFoundException($"The specified path does not exist: {path}");
            }

            _customDownloadPath = path;
        }

        /// <summary>
        /// Gets the default download path (project root Downloads folder)
        /// </summary>
        public static string GetDefaultDownloadPath()
        {
            return Path.Combine(ProjectRoot, "Downloads");
        }

        /// <summary>
        /// Checks if a custom download path is currently set
        /// </summary>
        public static bool HasCustomDownloadPath()
        {
            return _customDownloadPath != null;
        }

        /// <summary>
        /// Sanitizes a filename by removing invalid characters
        /// </summary>
        public static string SanitizeFileName(string fileName)
        {
            var invalidChars = Path.GetInvalidFileNameChars();
            foreach (var c in invalidChars)
            {
                fileName = fileName.Replace(c, '_');
            }
            return fileName;
        }
    }
}

