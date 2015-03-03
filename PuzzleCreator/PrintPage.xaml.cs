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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PuzzleCreator
{
    /// <summary>
    /// Interaction logic for PrintPage.xaml
    /// </summary>
    public partial class PrintPage : Page
    {

        Parameters parameters;
        public PrintPage(Parameters para)
        {
            InitializeComponent();
            parameters = para;
        }

        public PrintPage(Parameters para,Bitmap puzzle, Bitmap qr, String text)
        {
            InitializeComponent();
            parameters = para;

            PuzzlePiece.Source =parameters.Bitmap2BitmapImage( puzzle);
            QR.Source = parameters.Bitmap2BitmapImage(qr);
            Description.Text = text;

            PuzzlePiece2.Source = parameters.Bitmap2BitmapImage(puzzle);
            QR2.Source = parameters.Bitmap2BitmapImage(qr);
            Description2.Text = text;

        }
        

    }
}
