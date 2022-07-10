using System;
using System.Collections.Generic;
using System.Windows;

namespace ShortestPaths;

internal class Node
{
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

    internal int Index { get; set; }
    internal Network Network { get; private set; }
    internal Point Center { get; }
    internal string Text { get; }
    internal List<Link> Links { get; }

    public override string ToString()
    {
        return $"[{Text}]";
    }

    internal void AddLink(Link link)
    {
        if (link != null)
            Links.Add(link);
    }
}