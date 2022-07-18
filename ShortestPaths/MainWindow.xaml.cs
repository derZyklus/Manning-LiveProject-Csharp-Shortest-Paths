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
}