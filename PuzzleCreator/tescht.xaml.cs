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
    /// <summary>
    /// Interaction logic for tescht.xaml
    /// </summary>
    public partial class tescht : UserControl
    {
        public tescht()
        {
            InitializeComponent();
        }

         public tescht(ImageSource puzzle, ImageSource qr)
        {
            InitializeComponent();
            PuzzlePiece.Source = puzzle;
            QR.Source = qr;
          

        }
    }
}
