using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace YouTubeAudioDownloader.GUI
{
    /// <summary>
    /// Creates animated cosmic particles (stars, dust) for visual effects
    /// </summary>
    public class CosmicParticles
    {
        private readonly Canvas _canvas;
        private readonly Random _random = new Random();

        public CosmicParticles(Canvas canvas)
        {
            _canvas = canvas;
        }

        /// <summary>
        /// Creates a burst of star particles
        /// </summary>
        public void CreateStarBurst(Point origin, int particleCount = 20)
        {
            for (int i = 0; i < particleCount; i++)
            {
                CreateStar(origin);
            }
        }

        private void CreateStar(Point origin)
        {
            // Create star ellipse
            var star = new Ellipse
            {
                Width = _random.Next(2, 6),
                Height = _random.Next(2, 6),
                Fill = GetRandomCosmicColor(),
                Opacity = 0
            };

            // Position at origin
            Canvas.SetLeft(star, origin.X);
            Canvas.SetTop(star, origin.Y);
            
            _canvas.Children.Add(star);

            // Random direction and distance
            double angle = _random.NextDouble() * Math.PI * 2;
            double distance = _random.Next(50, 150);
            double targetX = origin.X + Math.Cos(angle) * distance;
            double targetY = origin.Y + Math.Sin(angle) * distance;

            // Animate position
            var moveX = new DoubleAnimation
            {
                From = origin.X,
                To = targetX,
                Duration = TimeSpan.FromMilliseconds(_random.Next(800, 1500)),
                EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseOut }
            };

            var moveY = new DoubleAnimation
            {
                From = origin.Y,
                To = targetY,
                Duration = moveX.Duration,
                EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseOut }
            };

            // Fade in then out
            var fade = new DoubleAnimation
            {
                From = 0,
                To = 1,
                Duration = TimeSpan.FromMilliseconds(300)
            };
            
            var fadeOut = new DoubleAnimation
            {
                From = 1,
                To = 0,
                BeginTime = TimeSpan.FromMilliseconds(500),
                Duration = TimeSpan.FromMilliseconds(700)
            };

            fadeOut.Completed += (s, e) => _canvas.Children.Remove(star);

            star.BeginAnimation(Canvas.LeftProperty, moveX);
            star.BeginAnimation(Canvas.TopProperty, moveY);
            star.BeginAnimation(UIElement.OpacityProperty, fade);
            star.BeginAnimation(UIElement.OpacityProperty, fadeOut);
        }

        /// <summary>
        /// Creates floating dust particles
        /// </summary>
        public void CreateFloatingDust(int count = 10)
        {
            for (int i = 0; i < count; i++)
            {
                CreateDustParticle();
            }
        }

        private void CreateDustParticle()
        {
            var dust = new Ellipse
            {
                Width = _random.Next(1, 3),
                Height = _random.Next(1, 3),
                Fill = new SolidColorBrush(Color.FromArgb(150, 255, 255, 255)),
                Opacity = 0
            };

            double startX = _random.NextDouble() * _canvas.ActualWidth;
            double startY = _canvas.ActualHeight + 10;

            Canvas.SetLeft(dust, startX);
            Canvas.SetTop(dust, startY);
            
            _canvas.Children.Add(dust);

            // Animate upward drift
            var moveY = new DoubleAnimation
            {
                From = startY,
                To = -10,
                Duration = TimeSpan.FromMilliseconds(_random.Next(3000, 5000))
            };

            var fade = new DoubleAnimation
            {
                From = 0,
                To = 0.6,
                Duration = TimeSpan.FromMilliseconds(500)
            };

            moveY.Completed += (s, e) => _canvas.Children.Remove(dust);

            dust.BeginAnimation(Canvas.TopProperty, moveY);
            dust.BeginAnimation(UIElement.OpacityProperty, fade);
        }

        private Brush GetRandomCosmicColor()
        {
            var colors = new[]
            {
                Color.FromRgb(0, 212, 255),    // Cyan
                Color.FromRgb(255, 107, 53),   // Orange
                Color.FromRgb(139, 90, 139),   // Purple
                Color.FromRgb(247, 183, 49),   // Gold
                Color.FromRgb(255, 255, 255)   // White
            };

            return new SolidColorBrush(colors[_random.Next(colors.Length)]);
        }
    }
}

