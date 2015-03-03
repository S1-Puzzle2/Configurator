using System;
using System.Collections.Generic;
using System.Globalization;
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

namespace PuzzleCreator
{
    /// <summary>
    /// Interaction logic for ServerConnection.xaml
    /// </summary>
    public partial class ServerConnection : Window
    {
        public ServerConnection(String host = "172.16.50.140", int port = 4711)
        {
            InitializeComponent();

            this.DataContext = this;

            Host = host;
            Port = port;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        public void SetStatus(String con)
        {
        }

        private void setStatus(String con)
        {
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
        }

        public String Host { get; private set; }

        public int Port { get; private set; }
    }
}
