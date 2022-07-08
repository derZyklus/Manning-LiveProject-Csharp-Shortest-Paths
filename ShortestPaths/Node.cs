using System;
using System.Collections.Generic;
using System.Windows;

namespace ShortestPaths;

public class Node
{
    public Node(Network network, Point center, string text)
    {
        if (network == null)
            throw new ArgumentNullException(nameof(network));
        Center = center;
        Text = text;
        Links = new List<Link>();
        Index = -1;
    }

    public int Index { get; set; }
    public Network Network { get; private set; }
    public Point Center { get; }
    public string Text { get; }
    public List<Link> Links { get; }

    public override string ToString()
    {
        return $"[{Text}]";
    }

    public void AddLink(Link link)
    {
        if (link != null)
            Links.Add(link);
    }
}