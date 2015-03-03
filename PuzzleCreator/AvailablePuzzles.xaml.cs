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
using System.Windows.Shapes;

namespace PuzzleCreator
{
    /// <summary>
    /// Interaction logic for AvailablePuzzles.xaml
    /// </summary>
    public partial class AvailablePuzzles : Window
    {
        public Parameters parameters;
        public AvailablePuzzles(Parameters par, IEnumerable<Puzzle> puzzleList)
        {
            InitializeComponent();
            parameters = par;
            ListBox.ItemsSource = puzzleList;

        }

        private void TextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (ListBox.SelectedItem != null)
            {
                parameters.actualPuzzleName = ((Puzzle)ListBox.SelectedItem).name;
                parameters.actualPuzzleID = ((Puzzle)ListBox.SelectedItem).id;
                parameters._networkController.setPuzzle(parameters.actualPuzzleID );
                parameters._networkController.getPuzzleItemList(parameters.actualPuzzleName);
                MessageBox.Show(ListBox.SelectedItem.ToString() + " wurde als aktuelles Puzzle ausgewählt");
            }
        }
    }
}
