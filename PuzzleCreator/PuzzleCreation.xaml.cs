using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
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
using System.IO;

namespace PuzzleCreator
{
    /// <summary>
    /// Interaction logic for PuzzleCreation.xaml
    /// </summary>
    public partial class PuzzleCreation : Window
    {

        Parameters parameters;
        public PuzzleCreation(Parameters para)
        {
            InitializeComponent();
            parameters = para;
        }


        Bitmap img = null;


        private void button1_Click(object sender, RoutedEventArgs e)
        {

            // Create OpenFileDialog
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            // Set filter for file extension and default file extension
            dlg.DefaultExt = ".png";
            dlg.Filter = "Images (.png)|*.png";

            // Display OpenFileDialog by calling ShowDialog method
            Nullable<bool> result = dlg.ShowDialog();

            // Get the selected file name and display in a TextBox
            if (result == true)
            {
                // Open document
                string filename = dlg.FileName;
                FileNameTextBox.Text = filename;
                img = (Bitmap)System.Drawing.Image.FromFile(filename);
                ImageSource imageSource = new BitmapImage(new Uri(filename));

                PuzzleSource.Source = imageSource;
            }

        }

        private bool checkInput()
        {

            if (String.IsNullOrWhiteSpace(PuzzleName.Text))
            {
                MessageBox.Show("Please type in a name");
                return false;
            }

            if (img == null)
            {
                MessageBox.Show("Please choose a picture");
                return false;
            }

            return true;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            if (checkInput())
            {

                LinkedList<BitmapImage> puzzleItems = new LinkedList<BitmapImage>();
                int pixelWidth = (int)img.Width / parameters.PuzzleHorizontalCount;
                int pixelHeigth = (int)img.Height / parameters.PuzzleVerticalCount;

                Bitmap src = (Bitmap)img.Clone();
                Bitmap temp = new Bitmap(pixelWidth, pixelHeigth);

                for (int y = 0; y < parameters.PuzzleVerticalCount; y++)
                {
                    for (int x = 0; x < parameters.PuzzleHorizontalCount; x++)
                    {
                        System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(temp);
                        System.Drawing.Rectangle pos = new
                            System.Drawing.Rectangle(x * pixelWidth, y * pixelHeigth, pixelWidth, pixelHeigth);
                        System.Drawing.Rectangle rect = new
                            System.Drawing.Rectangle(0, 0, pixelWidth, pixelHeigth);
                        g.DrawImage(src, rect, pos, GraphicsUnit.Pixel);

                        puzzleItems.AddLast(parameters.Bitmap2BitmapImage(temp));
                    }
                }

                Dictionary<int, PuzzlePiece> puzzle = new Dictionary<int, PuzzlePiece>();

                for (int i = 0; i < puzzleItems.Count; i++)
                {
                    Guid id = Guid.NewGuid();
                    puzzle.Add((i + 1), new PuzzlePiece(id, puzzleItems.ElementAt(i)));
                }

                parameters.ActualPuzzlepices = puzzle;
                var newWindow = new PuzzlePieces(puzzleItems);
                newWindow.Show();
                parameters._networkController.sendImagesToServer(puzzleItems, PuzzleName.Text);
            }

        }
    }
}

