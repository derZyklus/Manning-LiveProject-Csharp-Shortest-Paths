using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace ShortestPaths;

internal class Node
{
    private const double LARGE_RADIUS = 10;

    private const double SMALL_RADIUS = 3;
    private bool isEndNode;

    private bool isStartNode;
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

        IsStartNode = false;
        IsEndNode = false;

        Network.AddNode(this);
    }

    internal bool IsStartNode
    {
        get => isStartNode;
        set
        {
            isStartNode = value;
            SetNodeAppearance();
        }
    }

    internal bool IsEndNode
    {
        get => isEndNode;
        set
        {
            isEndNode = value;
            SetNodeAppearance();
        }
    }

    internal Ellipse MyEllipse { get; private set; }
    internal Label MyLabel { get; private set; }

    internal double TotalCost { get; set; }
    internal bool IsInPath { get; set; }
    internal bool Visited { get; set; }

    internal Link ShortestPathLink { get; set; }

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

        MyEllipse = canvas.DrawEllipse(bounds, Brushes.White, Brushes.Black, 1);
        MyEllipse.Tag = this;
        MyEllipse.MouseDown += Network.Ellipse_MouseDown;

        if (drawLabels)
        {
            MyLabel = canvas.DrawString(Text, radius * 2, radius * 2, Center, 0, 12, Brushes.Blue);
            MyLabel.Tag = this;
            MyLabel.MouseDown += Network.Label_MouseDown;
        }
    }

    private void SetNodeAppearance()
    {
        if (MyEllipse == null)
            return;
        if (IsStartNode)
        {
            MyEllipse.Fill = Brushes.Pink;
            MyEllipse.Stroke = Brushes.Red;
            MyEllipse.StrokeThickness = 2;
        }
        else if (IsEndNode)
        {
            MyEllipse.Fill = Brushes.LightGreen;
            MyEllipse.Stroke = Brushes.Green;
            MyEllipse.StrokeThickness = 2;
        }
        else
        {
            MyEllipse.Fill = Brushes.White;
            MyEllipse.Stroke = Brushes.Black;
            MyEllipse.StrokeThickness = 1;
        }
    }

    #region Properties

    internal Point Center { get; }

    internal int Index { get; set; }
    internal List<Link> Links { get; }
    internal Network Network { get; }
    internal string Text { get; }

    #endregion
}