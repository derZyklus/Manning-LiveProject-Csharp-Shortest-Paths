using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace ShortestPaths;

internal class Network
{
    #region Properties

    internal List<Link> Links { get; private set; }

    internal List<Node> Nodes { get; private set; }

    #endregion

    private const double Margin = 10;

    internal Network()
    {
        Clear();
    }

    internal Network(string filename)
    {
        ReadFromFile(filename);
    }

    internal void Clear()
    {
        Nodes = new List<Node>();
        Links = new List<Link>();
    }

    private bool drawLabels;

    internal void AddNode(Node node)
    {
        drawLabels = Nodes.Count < 100 ? true : false;
        node.Index = Nodes.Count;
        Nodes.Add(node);
    }

    internal void AddLink(Link link)
    {
        Links.Add(link);
    }

    internal string Serialization()
    {
        var sw = new StringWriter();
        sw.WriteLine($"{Nodes.Count} # Num nodes.");
        sw.WriteLine($"{Links.Count} # Num links.");

        sw.WriteLine("# Nodes.");
        foreach (var node in Nodes) sw.WriteLine($"{node.Center.X},{node.Center.Y},{node.Text}");

        sw.WriteLine("# Links.");
        foreach (var link in Links) sw.WriteLine($"{link.FromNode.Index},{link.ToNode.Index},{link.Cost}");

        return sw.ToString();
    }

    internal void SaveIntoFile(string filename)
    {
        var networkToSave = Serialization();
        File.WriteAllText(filename, networkToSave);
    }

    private string ReadNextLine(StringReader stringReader)
    {
        while (true)
        {
            var fullLine = stringReader.ReadLine();
            if (fullLine == null)
                return null;

            var cleanLine = fullLine.Split('#', StringSplitOptions.TrimEntries)[0];
            if (cleanLine.Length > 0)
                return cleanLine;
        }
    }

    internal void Deserialize(string networkToLoad)
    {
        Clear();

        using var stringReader = new StringReader(networkToLoad);
        var numberOfNodes = int.Parse(ReadNextLine(stringReader));
        var numberOfLinks = int.Parse(ReadNextLine(stringReader));

        for (var i = 0; i < numberOfNodes; i++)
        {
            var line = ReadNextLine(stringReader);
            var lineElements = line.Split(',');

            var x = double.Parse(lineElements[0]);
            var y = double.Parse(lineElements[1]);
            var text = lineElements[2].Trim();

            _ = new Node(this, new Point(x, y), text);
        }

        for (var i = 0; i < numberOfLinks; i++)
        {
            var line = ReadNextLine(stringReader);
            var lineElements = line.Split(',');

            var indexFromNode = int.Parse(lineElements[0]);
            var indexToNode = int.Parse(lineElements[1]);
            var cost = double.Parse(lineElements[2]);

            _ = new Link(this, Nodes[indexFromNode], Nodes[indexToNode], cost);
        }
    }

    internal void ReadFromFile(string filename)
    {
        var fileContent = File.ReadAllText(filename);
        Deserialize(fileContent);
    }

    internal void Draw(Canvas canvas)
    {
        var bounds = GetBounds();
        canvas.Width = bounds.Width + Margin;
        canvas.Height = bounds.Height + Margin;

        foreach (var link in Links) link.Draw(canvas);

        if (drawLabels)
        {
            foreach (var link in Links) link.DrawLabel(canvas);
        }
        

        foreach (var node in Nodes) node.Draw(canvas, drawLabels);
    }

    internal Rect GetBounds()
    {
        double xmax = 0;
        double ymax = 0;

        foreach (var node in Nodes)
        {
            if (node.Center.X > xmax)
                xmax = node.Center.X;
            if (node.Center.Y > ymax)
                ymax = node.Center.Y;
        }

        return new Rect(new Size(xmax, ymax));
    }
}