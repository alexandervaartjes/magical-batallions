﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace magical_batallions.Images
{
    /// <summary>
    /// Interaction logic for start_menu.xaml
    /// </summary>
    public partial class start_menu : Window
    {
        public start_menu()
        {
            InitializeComponent();
        }
        private void New_Game_Click(object sender, RoutedEventArgs e)
        {
            Map gameWindow = new();
            gameWindow.Show();
            this.Close();
        }
    }
}
