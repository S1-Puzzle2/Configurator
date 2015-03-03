using System;
using System.Collections.Generic;
using System.Drawing;
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
    /// Interaction logic for PuzzlePieces.xaml
    /// </summary>
    public partial class PuzzlePieces : Window
    {
        public PuzzlePieces(LinkedList<BitmapImage> images)
        {
            InitializeComponent();

            Image1.Source = images.ElementAt(0);
            Image2.Source = images.ElementAt(1);
            Image3.Source = images.ElementAt(2);
            Image4.Source = images.ElementAt(3);
            Image5.Source = images.ElementAt(4);
            Image6.Source = images.ElementAt(5);
            Image7.Source = images.ElementAt(6);
            Image8.Source = images.ElementAt(7);
            Image9.Source = images.ElementAt(8);
        }
    }
}
