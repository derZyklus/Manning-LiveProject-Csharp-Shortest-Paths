using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ShortestPaths;

internal class Link
{
    #region Properties

    internal double Cost { get; }
    internal Node FromNode { get; }
    internal Network Network { get; }
    internal Node ToNode { get; }

    #endregion

    private const double Radius = 10;
    private const double Diameter = 2 * Radius;

    internal Link(Network network, Node fromNode, Node toNode, double cost)
    {
        Network = network ?? throw new ArgumentNullException(nameof(network));
        FromNode = fromNode ?? throw new ArgumentNullException(nameof(fromNode));
        ToNode = toNode ?? throw new ArgumentNullException(nameof(toNode));
        Cost = cost;

        Network.AddLink(this);
        FromNode.AddLink(this);
    }

    public override string ToString()
    {
        return $"{FromNode} --> {ToNode} ({Cost})";
    }

    internal void Draw(Canvas canvas)
    {
        canvas.DrawLine(FromNode.Center, ToNode.Center, Brushes.Crimson, 1);
    }

    internal void DrawLabel(Canvas canvas)
    {
        var dx = ToNode.Center.X - FromNode.Center.X;
        var dy = ToNode.Center.Y - FromNode.Center.Y;

        var angle = Math.Atan2(dx, dy) * 180 / Math.PI - 0;

        var x = 0.67 * FromNode.Center.X + 0.33 * ToNode.Center.X;
        var y = 0.67 * FromNode.Center.Y + 0.33 * ToNode.Center.Y;

        canvas.DrawEllipse(new Rect(x - Radius, y - Radius, Diameter, Diameter),
            Brushes.White, null, 0);
        canvas.DrawString(Cost.ToString(), Diameter, Diameter, new Point(x, y), angle, 12, Brushes.Black);
    }
}