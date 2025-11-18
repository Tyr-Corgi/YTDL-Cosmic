using System;
using System.IO;
using System.Threading.Tasks;
using YoutubeDLSharp;
using YoutubeDLSharp.Options;

namespace YouTubeAudioDownloader
{
    /// <summary>
    /// Handles downloading and converting YouTube audio
    /// </summary>
    public class AudioDownloader
    {
        private readonly YoutubeDL _youtubeDL;

        public AudioDownloader()
        {
            _youtubeDL = new YoutubeDL
            {
                YoutubeDLPath = Config.YtDlpPath,
                FFmpegPath = Config.FFmpegPath,
                OutputFolder = Config.DownloadsPath
            };
        }

        /// <summary>
        /// Downloads audio from YouTube URL and converts to specified format
        /// </summary>
        /// <param name="url">YouTube URL</param>
        /// <param name="format">Output format (mp3 or flac)</param>
        /// <returns>Path to the downloaded file if successful, null otherwise</returns>
        public async Task<string?> DownloadAudioAsync(string url, string format)
        {
            try
            {
                Console.WriteLine($"Starting download from: {url}");
                Console.WriteLine($"Target format: {format.ToUpper()}");
                Console.WriteLine("Please wait...\n");

                // Configure download options
                var options = new OptionSet
                {
                    ExtractAudio = true,
                    AudioFormat = format == "mp3" ? AudioConversionFormat.Mp3 : AudioConversionFormat.Flac,
                    Output = Path.Combine(Config.DownloadsPath, "%(title)s.%(ext)s")
                };

                // Set audio quality based on format (for MP3)
                if (format == "mp3")
                {
                    options.AudioQuality = 0; // 0 = best quality
                }

                // Download and convert with progress reporting
                var result = await _youtubeDL.RunAudioDownload(
                    url,
                    format == "mp3" ? AudioConversionFormat.Mp3 : AudioConversionFormat.Flac,
                    progress: new Progress<DownloadProgress>(p =>
                    {
                        if (p.State == DownloadState.Downloading && p.Progress > 0)
                        {
                            Console.Write($"\rProgress: {p.Progress:F1}% | Speed: {p.DownloadSpeed} | ETA: {p.ETA}");
                        }
                    })
                );

                Console.WriteLine(); // New line after progress

                if (result.Success)
                {
                    Console.WriteLine("\n✓ Download completed successfully!");
                    Console.WriteLine($"File saved to: {result.Data}");
                    return result.Data;
                }
                else
                {
                    Console.WriteLine("\n✗ Download failed!");
                    foreach (var error in result.ErrorOutput)
                    {
                        Console.WriteLine($"Error: {error}");
                    }
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n✗ An error occurred: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Validates if the URL is a valid YouTube URL
        /// </summary>
        public static bool IsValidYouTubeUrl(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
                return false;

            // Check for common YouTube URL patterns
            return url.Contains("youtube.com/watch?v=") ||
                   url.Contains("youtu.be/") ||
                   url.Contains("youtube.com/shorts/") ||
                   url.Contains("youtube.com/embed/");
        }
    }
}

