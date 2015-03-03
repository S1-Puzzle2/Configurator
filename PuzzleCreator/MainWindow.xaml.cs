using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PuzzleCreator
{
  
    public partial class MainWindow : Window
    {
        public Parameters parameters;

        public MainWindow()
        {
            InitializeComponent();

            this.DataContext = this;

            parameters = new Parameters(this);
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            var newWindow = new ServerConnection();
            bool? dialogResult = newWindow.ShowDialog();

            parameters._networkController.Start();
            parameters._networkController.ip = newWindow.Host;
            parameters._networkController.port = newWindow.Port;
            parameters._networkController.openConn();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if(parameters._networkController.connected)
            {
                var newWindow = new PuzzleCreation(parameters);
                newWindow.Show();
            }
            else
            {
                MessageBox.Show("No connection to server");
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (parameters.actualPuzzleName != null)
            {
                if (parameters.ActualPuzzlepices.Count == 9)
                {


                    var newWindow = new Print(parameters, parameters.getActualPuzzleList());//Parameters.ActualPuzzlepices);
                    newWindow.Show();
                }
                else
                {      
                    MessageBox.Show("No puzzleitems received");
                }
            }
            else
            {
                MessageBox.Show("No puzzle selected");
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if (parameters._networkController.connected)
            {
                if (parameters.actualPuzzleName != null)
                {
                   // Parameters._networkController.setPuzzle(Parameters.actualPuzzleName);
                }
                else
                {
                    MessageBox.Show("No puzzle selected");
                }
            }
            else
            {
                MessageBox.Show("No server connected");
            }  
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            /* //Test
            LinkedList<Puzzle> puzzleList = new LinkedList<Puzzle>();
            puzzleList.AddLast(new Puzzle("hammergoodpuzzle", 1));
            puzzleList.AddLast(new Puzzle("test1", 2));
            puzzleList.AddLast(new Puzzle("test1", 3));
            var newWindow = new  AvailablePuzzles(parameters, puzzleList);
            newWindow.Show();
            */
            
            if (parameters._networkController.connected)
            {
                parameters._networkController.getPuzzleList();
            }
            else
            {
                MessageBox.Show("No server connected");
            }
        }

        public void ShowPuzzleListWindow(List<Puzzle> puzzleList)
        {
            //newWindow.Show();
            Dispatcher.BeginInvoke(new Action(() =>
            {
                var newWindow = new AvailablePuzzles(parameters, puzzleList);
                newWindow.Show();
            }));
        }
    }
}
