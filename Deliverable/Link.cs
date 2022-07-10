using System;

namespace ShortestPaths;

internal class Link
{
    internal Network Network { get; private set; }
    internal Node FromNode { get; private set; }
    internal Node ToNode { get; private set; }
    internal double Cost { get; private set; }

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
}