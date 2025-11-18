using System;
using System.Threading.Tasks;

namespace YouTubeAudioDownloader
{
    class Program
    {
        static async Task<int> Main(string[] args)
        {
            Console.WriteLine("===========================================");
            Console.WriteLine("   YouTube Audio Downloader (MP3/FLAC)");
            Console.WriteLine("===========================================\n");

            // Validate command-line arguments
            if (args.Length != 2)
            {
                ShowUsage();
                return 1;
            }

            string url = args[0];
            string format = args[1].ToLower();

            // Validate format
            if (format != "mp3" && format != "flac")
            {
                Console.WriteLine("✗ Error: Invalid format specified.");
                Console.WriteLine("  Supported formats: mp3, flac\n");
                ShowUsage();
                return 1;
            }

            // Validate YouTube URL
            if (!AudioDownloader.IsValidYouTubeUrl(url))
            {
                Console.WriteLine("✗ Error: Invalid YouTube URL.");
                Console.WriteLine("  Please provide a valid YouTube video URL.\n");
                Console.WriteLine("  Examples:");
                Console.WriteLine("    https://www.youtube.com/watch?v=VIDEO_ID");
                Console.WriteLine("    https://youtu.be/VIDEO_ID");
                return 1;
            }

            // Validate required tools
            if (!Config.ValidateTools(out string missingTool))
            {
                Console.WriteLine($"✗ Error: Required tool not found: {missingTool}");
                Console.WriteLine($"  Expected location: {(missingTool == "yt-dlp.exe" ? Config.YtDlpPath : Config.FFmpegPath)}");
                Console.WriteLine("\nPlease ensure all required tools are in the Tools directory.");
                return 1;
            }

            // Ensure downloads directory exists
            Config.EnsureDownloadsDirectory();

            try
            {
                // Create downloader and start download
                var downloader = new AudioDownloader();
                var result = await downloader.DownloadAudioAsync(url, format);

                if (result != null)
                {
                    Console.WriteLine("\n===========================================");
                    Console.WriteLine("   Download completed successfully! ✓");
                    Console.WriteLine("===========================================");
                    return 0;
                }
                else
                {
                    Console.WriteLine("\n===========================================");
                    Console.WriteLine("   Download failed! ✗");
                    Console.WriteLine("===========================================");
                    return 1;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n✗ Fatal error: {ex.Message}");
                Console.WriteLine("\nStack trace:");
                Console.WriteLine(ex.StackTrace);
                return 1;
            }
        }

        /// <summary>
        /// Displays usage information
        /// </summary>
        private static void ShowUsage()
        {
            Console.WriteLine("Usage:");
            Console.WriteLine("  YouTubeAudioDownloader.exe <YouTube_URL> <format>\n");
            Console.WriteLine("Arguments:");
            Console.WriteLine("  YouTube_URL    A valid YouTube video URL");
            Console.WriteLine("  format         Audio format: mp3 or flac\n");
            Console.WriteLine("Examples:");
            Console.WriteLine("  YouTubeAudioDownloader.exe \"https://www.youtube.com/watch?v=dQw4w9WgXcQ\" mp3");
            Console.WriteLine("  YouTubeAudioDownloader.exe \"https://youtu.be/dQw4w9WgXcQ\" flac\n");
            Console.WriteLine("Output:");
            Console.WriteLine("  Files are saved to the 'Downloads' folder");
            Console.WriteLine("  MP3:  320 kbps (high quality)");
            Console.WriteLine("  FLAC: Lossless compression");
        }
    }
}
