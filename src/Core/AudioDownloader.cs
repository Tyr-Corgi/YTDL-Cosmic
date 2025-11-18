using System;
using System.IO;
using System.Threading.Tasks;
using YoutubeDLSharp;
using YoutubeDLSharp.Options;

namespace YouTubeAudioDownloader
{
    /// <summary>
    /// Progress information for playlist downloads
    /// </summary>
    public class PlaylistProgress
    {
        public int CurrentIndex { get; set; }
        public int TotalCount { get; set; }
        public string CurrentTitle { get; set; } = string.Empty;
        public double OverallProgress { get; set; }
        public string CurrentStatus { get; set; } = string.Empty;
    }

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
                OutputFolder = Config.DownloadsPath,
                OutputFileTemplate = "%(title)s.%(ext)s"
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
                   url.Contains("youtube.com/embed/") ||
                   url.Contains("youtube.com/playlist?list=");
        }

        /// <summary>
        /// Checks if the URL is a YouTube playlist
        /// </summary>
        public static bool IsPlaylistUrl(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
                return false;

            // Check for playlist indicators
            return url.Contains("list=") || url.Contains("/playlist?");
        }

        /// <summary>
        /// Downloads all audio files from a YouTube playlist
        /// </summary>
        /// <param name="url">YouTube playlist URL</param>
        /// <param name="format">Output format (mp3 or flac)</param>
        /// <param name="progress">Progress reporter for playlist download</param>
        /// <returns>Number of successfully downloaded files</returns>
        public async Task<int> DownloadPlaylistAsync(string url, string format, IProgress<PlaylistProgress>? progress = null)
        {
            try
            {
                Console.WriteLine($"Starting playlist download from: {url}");
                Console.WriteLine($"Target format: {format.ToUpper()}");
                Console.WriteLine("Processing playlist...\n");

                // Report initial progress
                progress?.Report(new PlaylistProgress
                {
                    CurrentIndex = 0,
                    TotalCount = 0,
                    CurrentTitle = "Fetching playlist...",
                    OverallProgress = 0,
                    CurrentStatus = "Starting playlist download..."
                });

                // Get playlist info to get individual video URLs
                Console.WriteLine("=== FETCHING PLAYLIST INFO ===");
                var infoResult = await _youtubeDL.RunVideoDataFetch(url);
                
                if (!infoResult.Success || infoResult.Data == null || infoResult.Data.Entries == null)
                {
                    Console.WriteLine("Failed to fetch playlist info");
                    progress?.Report(new PlaylistProgress
                    {
                        CurrentIndex = 0,
                        TotalCount = 0,
                        CurrentTitle = "Error",
                        OverallProgress = 0,
                        CurrentStatus = "Failed to fetch playlist information"
                    });
                    return 0;
                }

                var videos = infoResult.Data.Entries;
                int totalVideos = videos.Length;
                int successCount = 0;
                
                Console.WriteLine($"Found {totalVideos} videos in playlist");
                Console.WriteLine($"Playlist title: {infoResult.Data.Title}");
                
                // Create playlist subfolder
                string playlistName = Config.SanitizeFileName(infoResult.Data.Title ?? "Playlist");
                string playlistFolder = Path.Combine(Config.DownloadsPath, playlistName);
                Directory.CreateDirectory(playlistFolder);
                
                Console.WriteLine($"Downloading to: {playlistFolder}");

                // Download each video individually
                for (int i = 0; i < totalVideos; i++)
                {
                    var video = videos[i];
                    int currentIndex = i + 1;
                    
                    try
                    {
                        string videoUrl = video.Url ?? $"https://www.youtube.com/watch?v={video.ID}";
                        string videoTitle = Config.SanitizeFileName(video.Title ?? $"Video_{currentIndex}");
                        
                        Console.WriteLine($"\n=== Downloading {currentIndex}/{totalVideos}: {videoTitle} ===");
                        
                        progress?.Report(new PlaylistProgress
                        {
                            CurrentIndex = currentIndex,
                            TotalCount = totalVideos,
                            CurrentTitle = videoTitle,
                            OverallProgress = (currentIndex * 100.0) / totalVideos,
                            CurrentStatus = $"Downloading {currentIndex} of {totalVideos}: {videoTitle}"
                        });

                        // Temporarily change output folder to playlist subfolder
                        string originalFolder = _youtubeDL.OutputFolder;
                        _youtubeDL.OutputFolder = playlistFolder;
                        
                        try
                        {
                            // Download this individual video
                            var videoResult = await _youtubeDL.RunAudioDownload(
                                videoUrl,
                                format == "mp3" ? AudioConversionFormat.Mp3 : AudioConversionFormat.Flac
                            );

                            if (videoResult.Success)
                            {
                                successCount++;
                                Console.WriteLine($"✓ Downloaded to: {playlistFolder}\\{videoTitle}");
                            }
                            else
                            {
                                Console.WriteLine($"✗ Failed: {videoTitle}");
                                if (videoResult.ErrorOutput != null && videoResult.ErrorOutput.Length > 0)
                                {
                                    Console.WriteLine($"   Error: {videoResult.ErrorOutput[0]}");
                                }
                            }
                        }
                        finally
                        {
                            // Restore original output folder
                            _youtubeDL.OutputFolder = originalFolder;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"✗ Error downloading video {currentIndex}: {ex.Message}");
                    }
                }

                Console.WriteLine($"\n=== DOWNLOAD COMPLETE ===");
                Console.WriteLine($"Successfully downloaded {successCount} of {totalVideos} videos");

                progress?.Report(new PlaylistProgress
                {
                    CurrentIndex = totalVideos,
                    TotalCount = totalVideos,
                    CurrentTitle = "Complete!",
                    OverallProgress = 100,
                    CurrentStatus = $"Downloaded {successCount} of {totalVideos} videos!"
                });

                return successCount;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n✗ An error occurred during playlist download: {ex.Message}");
                progress?.Report(new PlaylistProgress
                {
                    CurrentIndex = 0,
                    TotalCount = 0,
                    CurrentTitle = "Error",
                    OverallProgress = 0,
                    CurrentStatus = $"Error: {ex.Message}"
                });
                return 0;
            }
        }
    }
}

