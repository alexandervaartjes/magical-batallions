﻿using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using magical_batallions.Images;


namespace magical_batallions
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void startButton_Click(object sender, RoutedEventArgs e)
        {
            start_menu gameWindow = new start_menu();
            gameWindow.Show();
            this.Close();
        }

        private void optionsButton_Click(object sender, RoutedEventArgs e)
        {
            // Logic for opening options goes here
            MessageBox.Show("Opening options...");
        }

        private void creditsButton_Click(object sender, RoutedEventArgs e)
        {
            // Logic for showing credits goes here
            MessageBox.Show("Showing credits...");
        }
    }
}