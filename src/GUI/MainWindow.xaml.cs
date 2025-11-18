using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;
using YouTubeAudioDownloader;

namespace YouTubeAudioDownloader.GUI;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private AudioDownloader? _downloader;
    private bool _isDownloading = false;
    private CosmicParticles? _particles;
    private System.Windows.Threading.DispatcherTimer? _dustTimer;

    public MainWindow()
    {
        InitializeComponent();
        InitializeApp();
        Loaded += MainWindow_Loaded;
    }

    private void MainWindow_Loaded(object sender, RoutedEventArgs e)
    {
        // Initialize particle system
        _particles = new CosmicParticles(ParticleCanvas);
        
        // Create ambient floating dust particles
        StartAmbientParticles();
    }

    private void StartAmbientParticles()
    {
        // Timer to periodically spawn dust particles
        _dustTimer = new System.Windows.Threading.DispatcherTimer
        {
            Interval = TimeSpan.FromSeconds(2)
        };
        _dustTimer.Tick += (s, e) =>
        {
            if (_particles != null)
            {
                _particles.CreateFloatingDust(3);
            }
        };
        _dustTimer.Start();
    }

    private void InitializeApp()
    {
        // Validate required tools on startup
        if (!Config.ValidateTools(out string missingTool))
        {
            MessageBox.Show(
                $"Required tool not found: {missingTool}\n\n" +
                $"Expected location: {(missingTool == "yt-dlp.exe" ? Config.YtDlpPath : Config.FFmpegPath)}\n\n" +
                "Please ensure all required tools are in the Tools directory.",
                "Missing Dependencies",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
            Application.Current.Shutdown();
            return;
        }

        // Ensure downloads directory exists
        Config.EnsureDownloadsDirectory();

        // Initialize downloader
        _downloader = new AudioDownloader();
    }

    private void UrlTextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
    {
        // Validate URL and provide visual feedback
        string url = UrlTextBox.Text;
        
        if (string.IsNullOrWhiteSpace(url))
        {
            // Default state
            UrlTextBox.BorderBrush = new SolidColorBrush(Color.FromRgb(107, 45, 92)); // #6b2d5c
            UrlTextBox.Effect = null;
            return;
        }

        if (AudioDownloader.IsValidYouTubeUrl(url))
        {
            // Valid URL - Cyan glow
            UrlTextBox.BorderBrush = new SolidColorBrush(Color.FromRgb(0, 212, 255)); // #00d4ff
            var glow = new System.Windows.Media.Effects.DropShadowEffect
            {
                Color = Color.FromRgb(0, 212, 255),
                BlurRadius = 15,
                ShadowDepth = 0,
                Opacity = 0.7
            };
            UrlTextBox.Effect = glow;
        }
        else
        {
            // Invalid URL - Red glow
            UrlTextBox.BorderBrush = new SolidColorBrush(Color.FromRgb(255, 50, 50)); // Red
            var glow = new System.Windows.Media.Effects.DropShadowEffect
            {
                Color = Color.FromRgb(255, 50, 50),
                BlurRadius = 15,
                ShadowDepth = 0,
                Opacity = 0.7
            };
            UrlTextBox.Effect = glow;
        }
    }

    private async void DownloadButton_Click(object sender, RoutedEventArgs e)
    {
        if (_isDownloading || _downloader == null)
            return;

        string url = UrlTextBox.Text.Trim();
        string format = Mp3Radio.IsChecked == true ? "mp3" : "flac";

        // Validate URL
        if (string.IsNullOrWhiteSpace(url))
        {
            ShowCosmicMessage("Please enter a YouTube URL", "Input Required");
            return;
        }

        if (!AudioDownloader.IsValidYouTubeUrl(url))
        {
            ShowCosmicMessage("Please enter a valid YouTube URL", "Invalid URL");
            return;
        }

        // Start download
        _isDownloading = true;
        SetUIDownloadingState(true);
        
        try
        {
            UpdateStatus($"Starting download as {format.ToUpper()}...");
            
            // Animate progress bar appearance
            AnimateProgressPanel(true);
            
            var result = await _downloader.DownloadAudioAsync(url, format);

            if (result != null)
            {
                // Success!
                UpdateStatus("✨ Download completed successfully! ✨");
                AnimateSuccessGlow();
                
                // Cosmic celebration particles!
                if (_particles != null)
                {
                    var buttonPos = DownloadButton.TranslatePoint(
                        new Point(DownloadButton.ActualWidth / 2, DownloadButton.ActualHeight / 2), 
                        this);
                    _particles.CreateStarBurst(buttonPos, 30);
                }
                
                MessageBox.Show(
                    $"File saved to:\n{result}\n\nClick 'Open Downloads Folder' to view your file.",
                    "🌟 Download Complete!",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
                
                DownloadProgressBar.Value = 100;
            }
            else
            {
                // Failure
                UpdateStatus("❌ Download failed");
                ShowCosmicMessage("The download failed. Please check the URL and try again.", "Download Failed");
            }
        }
        catch (Exception ex)
        {
            UpdateStatus("❌ Error occurred");
            ShowCosmicMessage($"An error occurred:\n{ex.Message}", "Error");
        }
        finally
        {
            _isDownloading = false;
            SetUIDownloadingState(false);
            
            // Reset after a delay
            await Task.Delay(3000);
            ResetUI();
        }
    }

    private void OpenFolderButton_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            if (Directory.Exists(Config.DownloadsPath))
            {
                Process.Start("explorer.exe", Config.DownloadsPath);
            }
            else
            {
                ShowCosmicMessage("Downloads folder not found.", "Folder Not Found");
            }
        }
        catch (Exception ex)
        {
            ShowCosmicMessage($"Could not open folder:\n{ex.Message}", "Error");
        }
    }

    private void SetUIDownloadingState(bool isDownloading)
    {
        DownloadButton.IsEnabled = !isDownloading;
        UrlTextBox.IsEnabled = !isDownloading;
        Mp3Radio.IsEnabled = !isDownloading;
        FlacRadio.IsEnabled = !isDownloading;
        
        if (isDownloading)
        {
            DownloadButton.Content = "⏳ Downloading...";
            DownloadProgressBar.IsIndeterminate = true;
        }
        else
        {
            DownloadButton.Content = "✨ Download Audio ✨";
            DownloadProgressBar.IsIndeterminate = false;
        }
    }

    private void UpdateStatus(string message)
    {
        Dispatcher.Invoke(() =>
        {
            StatusText.Text = message;
        });
    }

    private void AnimateProgressPanel(bool show)
    {
        if (show)
        {
            ProgressPanel.Visibility = Visibility.Visible;
            var fadeIn = new DoubleAnimation(0, 1, TimeSpan.FromMilliseconds(500));
            ProgressPanel.BeginAnimation(OpacityProperty, fadeIn);
        }
        else
        {
            var fadeOut = new DoubleAnimation(1, 0, TimeSpan.FromMilliseconds(500));
            fadeOut.Completed += (s, e) => ProgressPanel.Visibility = Visibility.Collapsed;
            ProgressPanel.BeginAnimation(OpacityProperty, fadeOut);
        }
    }

    private void AnimateSuccessGlow()
    {
        // Pulse the download button with cosmic colors
        var colorAnimation = new ColorAnimation
        {
            From = Color.FromRgb(255, 107, 53), // Orange
            To = Color.FromRgb(0, 212, 255),    // Cyan
            Duration = TimeSpan.FromMilliseconds(500),
            AutoReverse = true,
            RepeatBehavior = new RepeatBehavior(3)
        };

        var glow = new System.Windows.Media.Effects.DropShadowEffect
        {
            Color = Color.FromRgb(255, 107, 53),
            BlurRadius = 30,
            ShadowDepth = 0,
            Opacity = 0.9
        };
        
        DownloadButton.Effect = glow;
    }

    private void ResetUI()
    {
        Dispatcher.Invoke(() =>
        {
            DownloadProgressBar.Value = 0;
            UpdateStatus("Ready to download...");
            AnimateProgressPanel(false);
            
            // Reset button glow
            var defaultGlow = new System.Windows.Media.Effects.DropShadowEffect
            {
                Color = Color.FromRgb(139, 90, 139),
                BlurRadius = 30,
                ShadowDepth = 0,
                Opacity = 0.8
            };
            DownloadButton.Effect = defaultGlow;
        });
    }

    private void ShowCosmicMessage(string message, string title)
    {
        MessageBox.Show(message, $"🌌 {title}", MessageBoxButton.OK, MessageBoxImage.Information);
    }
}
