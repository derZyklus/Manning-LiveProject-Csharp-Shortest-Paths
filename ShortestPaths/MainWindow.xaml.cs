using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Input.Manipulations;
using Microsoft.Win32;

namespace ShortestPaths;

/// <summary>
///     Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    #region Properties

    private Network MyNetwork = new();

    #endregion

    public MainWindow()
    {
        InitializeComponent();
    }

    private void CommandBinding_OnCanExecute(object sender, CanExecuteRoutedEventArgs e)
    {
        e.CanExecute = true;
    }

    private void CommandBinding_OnExecuted(object sender, ExecutedRoutedEventArgs e)
    {
        try
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.DefaultExt = ".net";
            openFileDialog.Filter = "Network Files|*.net|All Files|*.*";

            if (openFileDialog.ShowDialog() == true) MyNetwork.ReadFromFile(openFileDialog.FileName);

            DrawNetwork();
        }
        catch (Exception exception)
        {
            MessageBox.Show(exception.Message);
            MyNetwork = new Network();
        }
    }

    private void DrawNetwork()
    {
        // Remove old drawing
        MainCanvas.Children.Clear();

        // Draw the new network
        MyNetwork.Draw(MainCanvas);
    }

    private void MenuItem_OnClick(object sender, RoutedEventArgs e)
    {
        Close();
    }

    private Network BuildGridNetwork(string filename, double width, double height, int numRows, int numCols)
    {
        var network = new Network();

        double margin = 10;

        var columnIncrement = (width - margin * 2) / numCols;
        var rowIncrement = (height - margin * 2) / numRows;

        var nodes = new Node[numRows, numCols];
        var nodeIndex = 0;

        for (var rowIndex = 0; rowIndex < numRows; rowIndex++)
        for (var columnIndex = 0; columnIndex < numCols; columnIndex++)
        {
            var label = (++nodeIndex).ToString();

            var cx = margin + columnIndex * columnIncrement;
            var cy = margin + rowIndex * rowIncrement;
            nodes[rowIndex, columnIndex] = new Node(network, new Point(cx, cy), label);
        }

        for (int rowIndex = 0; rowIndex < numRows; rowIndex++)
        {
            for (int columnIndex = 0; columnIndex < numCols-1; columnIndex++)
            {
                MakeRandomizedLink(network, nodes[rowIndex, columnIndex], nodes[rowIndex, columnIndex+1]);
            }
        }

        for (int rowIndex = 0; rowIndex < numRows-1; rowIndex++)
        {
            for (int columnIndex = 0; columnIndex < numCols; columnIndex++)
            {
                MakeRandomizedLink(network, nodes[rowIndex, columnIndex], nodes[rowIndex+1, columnIndex]);
            }
        }

        return network;
    }

    private void OnClickMakeTestNetworks(object sender, RoutedEventArgs e)
    {
        var test = BuildGridNetwork("hallo.net", 800, 600, 6, 10);

        test.Draw(MainCanvas);
    }

    private double Distance(Point p1, Point p2)
    {
        var vector = p1 - p2;
        return vector.Length;
    }

    private readonly Random Rand = new Random();

    void MakeRandomizedLink(Network network, Node node1, Node node2)
    {
        double dist = Distance(node1.Center, node2.Center);

        double cost12 = Math.Round(dist * (Rand.Next(10, 12)/10));
        Link link12 = new Link(network, node1, node2, cost12);

        double cost21 = Math.Round(dist * (Rand.Next(10, 12) / 10));
        Link link21 = new Link(network, node2, node1, cost21);
    }
}