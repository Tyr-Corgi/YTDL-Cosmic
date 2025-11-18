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

        // Paths to external tools - try local first (for GUI), then project root (for console)
        public static string YtDlpPath => GetToolPath("yt-dlp.exe");
        public static string FFmpegPath => GetToolPath("ffmpeg.exe");

        // Output directory for downloaded files (in project root)
        public static string DownloadsPath => Path.Combine(ProjectRoot, "Downloads");

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

