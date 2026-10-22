using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace SecurityShield;

public partial class MainWindow : Window
{
    // ---- These remember the current state of the app ----
    private int score = 87;          // the Shield Score, 0 to 100
    private int xp = 2840;           // experience points earned
    private const int XpMax = 4000;  // xp needed for the next level
    private const double XpBarWidth = 300; // must match the XP bar width in the .xaml

    public MainWindow()
    {
        InitializeComponent();        // builds the screen from MainWindow.xaml
        UpdateScoreDisplay();         // draw the ring for the starting score
        UpdateXpDisplay();            // draw the xp bar for the starting xp
    }

    // ============ THE FIX BUTTONS ============
    // Each one hands its row, its button, and how much score/xp it's worth to ApplyFix.
    private void Fix1_Click(object sender, RoutedEventArgs e) => ApplyFix(Fix1Row, Fix1Btn, scoreGain: 4, xpGain: 8);
    private void Fix2_Click(object sender, RoutedEventArgs e) => ApplyFix(Fix2Row, Fix2Btn, scoreGain: 5, xpGain: 6);
    private void Fix3_Click(object sender, RoutedEventArgs e) => ApplyFix(Fix3Row, Fix3Btn, scoreGain: 4, xpGain: 5);

    private void ApplyFix(Border row, Button button, int scoreGain, int xpGain)
    {
        // Raise the score and xp, but never go above the maximum.
        score = Math.Min(100, score + scoreGain);
        xp = Math.Min(XpMax, xp + xpGain);

        // Mark this task as done and dim its row so it reads as finished.
        button.Content = "Done";
        button.IsEnabled = false;
        row.Opacity = 0.45;

        // Redraw the ring and xp bar to match the new numbers.
        UpdateScoreDisplay();
        UpdateXpDisplay();
    }

    // ============ THE "GAME READY" BUTTON ============
    private void GameReady_Click(object sender, RoutedEventArgs e)
    {
        GameReadyButton.IsEnabled = false;
        MessageBox.Show(
            "Game ready.\n\nMemory freed, scans paused, and notifications silenced for your session.",
            "Security Shield",
            MessageBoxButton.OK,
            MessageBoxImage.Information);
    }

    // ============ UPDATE THE XP BAR ============
    private void UpdateXpDisplay()
    {
        // The filled part's width is a fraction of the full bar.
        XpFill.Width = XpBarWidth * ((double)xp / XpMax);
        XpText.Text = $"{xp:N0} / {XpMax:N0} xp"; // N0 adds the thousands comma
    }

    // ============ UPDATE THE SCORE + RING ============
    private void UpdateScoreDisplay()
    {
        ScoreText.Text = score.ToString();

        // Pick the colour: green when healthy, amber when so-so, red when poor.
        Brush colour =
            score >= 90 ? (Brush)FindResource("Green") :
            score >= 50 ? (Brush)FindResource("Amber") :
                          new SolidColorBrush(Color.FromRgb(0xFF, 0x6B, 0x8B));

        ScoreArc.Stroke = colour;
        StatusText.Foreground = colour;
        StatusText.Text = score >= 100 ? "Fully protected" : "Protected";

        // Update the "fix N things" subtitle based on how far from 100 we are.
        SubtitleText.Text = score >= 100 ? "All clear - you're fully protected" : "Fix the items below to reach 100";

        // Redraw the arc so it fills to the current score.
        ScoreArc.Data = BuildArc(score);
    }

    // ============ DRAW THE SCORE ARC ============
    // Builds the curved line that fills the ring, from the top, going clockwise.
    private Geometry BuildArc(int percent)
    {
        const double cx = 92, cy = 92, r = 85; // centre and radius inside the 184x184 box

        // How far around the circle to sweep. Cap just under a full circle so it draws cleanly at 100.
        double sweep = 360.0 * percent / 100.0;
        if (sweep <= 0) return Geometry.Empty;
        if (sweep >= 360) sweep = 359.99;

        Point start = PointOnCircle(cx, cy, r, -90);          // top of the circle
        Point end = PointOnCircle(cx, cy, r, -90 + sweep);    // where the sweep ends
        bool isLargeArc = sweep > 180;

        var figure = new PathFigure { StartPoint = start, IsClosed = false };
        figure.Segments.Add(new ArcSegment
        {
            Point = end,
            Size = new Size(r, r),
            SweepDirection = SweepDirection.Clockwise,
            IsLargeArc = isLargeArc
        });

        var geometry = new PathGeometry();
        geometry.Figures.Add(figure);
        return geometry;
    }

    // Works out the x,y position of a point on a circle at a given angle.
    private static Point PointOnCircle(double cx, double cy, double r, double angleDegrees)
    {
        double radians = angleDegrees * Math.PI / 180.0;
        return new Point(cx + r * Math.Cos(radians), cy + r * Math.Sin(radians));
    }
}
