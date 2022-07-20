using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ShortestPaths;

internal class Node
{
    private const double LARGE_RADIUS = 10;

    private const double SMALL_RADIUS = 3;
    //private const double Radius = 10;
    //private const double Diameter = 2 * Radius;

    internal Node(Network network, Point center, string text)
    {
        if (network == null)
            throw new ArgumentNullException(nameof(network));
        Network = network;
        Center = center;
        Text = text;
        Links = new List<Link>();
        Index = -1;

        Network.AddNode(this);
    }

    public override string ToString()
    {
        return $"[{Text}]";
    }

    internal void AddLink(Link link)
    {
        if (link != null)
            Links.Add(link);
    }

    internal void Draw(Canvas canvas, bool drawLabels)
    {
        var radius = drawLabels ? LARGE_RADIUS : SMALL_RADIUS;
        var bounds = new Rect(new Point(Center.X - radius, Center.Y - radius),
            new Size(radius * 2, radius * 2));

        canvas.DrawEllipse(bounds, Brushes.White, Brushes.Black, 1);
        if (drawLabels) canvas.DrawString(Text, radius * 2, radius * 2, Center, 0, 12, Brushes.Blue);
    }

    #region Properties

    internal Point Center { get; }

    internal int Index { get; set; }
    internal List<Link> Links { get; }
    internal Network Network { get; }
    internal string Text { get; }

    #endregion
}