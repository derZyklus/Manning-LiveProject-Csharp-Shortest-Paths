using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace ShortestPaths;

internal class Link
{
    private const double Radius = 10;
    private const double Diameter = 2 * Radius;

    private bool isInPath;
    private bool isInTree;

    internal Link(Network network, Node fromNode, Node toNode, double cost)
    {
        Network = network ?? throw new ArgumentNullException(nameof(network));
        FromNode = fromNode ?? throw new ArgumentNullException(nameof(fromNode));
        ToNode = toNode ?? throw new ArgumentNullException(nameof(toNode));
        Cost = cost;

        IsInTree = false;
        IsInPath = false;

        Network.AddLink(this);
        FromNode.AddLink(this);
    }

    internal Line MyLine { get; private set; }

    internal bool IsInTree
    {
        get => isInTree;
         set
        {
            isInTree = value;
            SetLinkAppearance();
        }
    }

    internal bool IsInPath
    {
        get => isInPath;
         set
        {
            isInPath = value;
            SetLinkAppearance();
        }
    }

    public override string ToString()
    {
        return $"{FromNode} --> {ToNode} ({Cost})";
    }

    internal void Draw(Canvas canvas)
    {
        //canvas.DrawLine(FromNode.Center, ToNode.Center, Brushes.Green, 1);
        MyLine = canvas.DrawLine(FromNode.Center, ToNode.Center, Brushes.Green, 1);
    }

    internal void DrawLabel(Canvas canvas)
    {
        var dx = ToNode.Center.X - FromNode.Center.X;
        var dy = ToNode.Center.Y - FromNode.Center.Y;

        var angle = Math.Atan2(dx, dy) * 180 / Math.PI - 90;

        var x = 0.67 * FromNode.Center.X + 0.33 * ToNode.Center.X;
        var y = 0.67 * FromNode.Center.Y + 0.33 * ToNode.Center.Y;

        canvas.DrawEllipse(new Rect(x - Radius, y - Radius, Diameter, Diameter),
            Brushes.White, null, 0);
        canvas.DrawString(Cost.ToString(), Diameter, Diameter, new Point(x, y), angle, 12, Brushes.Black);
    }

    private void SetLinkAppearance()
    {
        if (MyLine == null) return;

        if (IsInPath)
        {
            MyLine.Stroke = Brushes.Red;
            MyLine.StrokeThickness = 6;
        }
        else if (IsInTree)
        {
            MyLine.Stroke = Brushes.Lime;
            MyLine.StrokeThickness = 6;
        }
        else
        {
            MyLine.Stroke = Brushes.Black;
            MyLine.StrokeThickness = 1;
        }
    }

    #region Properties

    internal double Cost { get; }
    internal Node FromNode { get; }
    internal Network Network { get; }
    internal Node ToNode { get; }

    #endregion
}