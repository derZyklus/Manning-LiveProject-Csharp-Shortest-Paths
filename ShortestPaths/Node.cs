using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ShortestPaths;

internal class Node
{
    #region Properties

    internal Point Center { get; }

    internal int Index { get; set; }
    internal List<Link> Links { get; }
    internal Network Network { get; }
    internal string Text { get; }

    #endregion

    private const double Radius = 10;
    private const double Diameter = 2 * Radius;

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

    internal void Draw(Canvas canvas)
    {
        var bounds = new Rect(new Point(Center.X - Radius, Center.Y - Radius),
            new Size(Diameter, Diameter));

        canvas.DrawEllipse(bounds, Brushes.White, Brushes.Black, 1);
        canvas.DrawString(Text, Diameter, Diameter, Center, 0, 12, Brushes.Blue);
    }
}