using System;
using System.Collections.Generic;
using System.IO;

namespace ShortestPaths;

public class Network
{
    public Network()
    {
        Clear();
    }

    public List<Node> Nodes { get; private set; }
    public List<Link> Links { get; private set; }

    public void Clear()
    {
        Nodes = new List<Node>();
        Links = new List<Link>();
    }

    public void AddNode(Node node)
    {
        node.Index = Nodes.Count;
        Nodes.Add(node);
    }

    public void AddLink(Link link)
    {
        Links.Add(link);
    }

    public string Serialization()
    {
        string result = string.Empty;

        StringWriter sw = new StringWriter();
        


        return sw.ToString();
    }
}