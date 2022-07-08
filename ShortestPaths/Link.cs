using System;

namespace ShortestPaths;

public class Link
{
    public Network Network { get; private set; }
    public Node FromNode { get; private set; }
    public Node ToNode { get; private set; }
    public double Cost { get; private set; }

    public Link(Network network, Node fromNode, Node toNode, double cost)
    {
        Network = network ?? throw new ArgumentNullException(nameof(network));
        FromNode = fromNode ?? throw new ArgumentNullException(nameof(fromNode));
        ToNode = toNode ?? throw new ArgumentNullException(nameof(toNode));
        Cost = cost;

        
    }

    public override string ToString()
    {
        return $"{FromNode} --> {ToNode} ({Cost})";
    }
}