using System;
using System.Windows;
using System.Windows.Input;
using Microsoft.Win32;

namespace ShortestPaths;

/// <summary>
///     Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    //private void Button1_OnClick(object sender, RoutedEventArgs e)
    //{
    //    var network = new Network();

    //    var a = new Node(network, new Point(20, 20), "A");
    //    var b = new Node(network, new Point(120, 120), "B");

    //    var link = new Link(network, a, b, 10);

    //    ValidateNetwork(network, "network1.txt");
    //}

    //private void Button2_OnClick(object sender, RoutedEventArgs e)
    //{
    //    Network network = new Network();

    //    Node a = new Node(network, new Point(20, 20), "A");
    //    Node b = new Node(network, new Point(120, 20), "B");
    //    Node c = new Node(network, new Point(20, 120), "C");
    //    Node d = new Node(network, new Point(120, 120), "D");

    //    Link link_a_b = new Link(network, a, b, 10);
    //    Link link_b_d = new Link(network, b, d, 15);
    //    Link link_a_c = new Link(network, a, c, 20);
    //    Link link_c_d = new Link(network, c, d, 25);

    //    ValidateNetwork(network, "network2.txt");
    //}

    //private void Button3_OnClick(object sender, RoutedEventArgs e)
    //{
    //    Network network = new Network();

    //    Node a = new Node(network, new Point(20, 20), "A");
    //    Node b = new Node(network, new Point(120, 20), "B");
    //    Node c = new Node(network, new Point(20, 120), "C");
    //    Node d = new Node(network, new Point(120, 120), "D");

    //    Link link_a_b = new Link(network, a, b, 10);
    //    Link link_b_d = new Link(network, b, d, 15);
    //    Link link_a_c = new Link(network, a, c, 20);
    //    Link link_c_d = new Link(network, c, d, 25);
    //    Link link_b_a = new Link(network, b, a, 11);
    //    Link link_d_b = new Link(network, d, b, 16);
    //    Link link_c_a = new Link(network, c, a, 21);
    //    Link link_d_c = new Link(network, d, c, 26);

    //    ValidateNetwork(network, "network3.txt");
    //}

    //private void ValidateNetwork(Network network, string filename)
    //{
    //    string serializationResult =network.Serialization();

    //    network.SaveIntoFile(filename);
    //    network.ReadFromFile(filename);

    //    string reloadedResult = network.Serialization();
    //    netTextBox.Text = reloadedResult;

    //    if (serializationResult == reloadedResult)
    //    {
    //        statusLabel.Content = "OK";
    //    }
    //    else
    //    {
    //        statusLabel.Content = "Serializations do not match";
    //    }
    //}
    private void CommandBinding_OnCanExecute(object sender, CanExecuteRoutedEventArgs e)
    {
        e.CanExecute = true;
    }


    private Network MyNetwork = new Network();

    private void CommandBinding_OnExecuted(object sender, ExecutedRoutedEventArgs e)
    {
        try
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.DefaultExt = ".net";
            openFileDialog.Filter = "Network Files|*.net|All Files|*.*";

            if (openFileDialog.ShowDialog() == true)
            {
                MyNetwork.ReadFromFile(openFileDialog.FileName);
            }

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
}