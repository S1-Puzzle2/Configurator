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
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ZXing;

namespace PuzzleCreator
{
    /// <summary>
    /// Interaction logic for Print.xaml
    /// </summary>
    public partial class Print : Window
    {
        Parameters parameters;
        Dictionary<int, PuzzlePiece> images;
        BarcodeWriter bcw;
        
  
        public Print(Parameters para,LinkedList<Bitmap> bitmaps)
        {
            InitializeComponent();
            parameters = para;
            LinkedList<BitmapImage> imag = parameters.ConvertBitmapToImage(bitmaps);
            bcw = new BarcodeWriter();
            bcw.Format = BarcodeFormat.QR_CODE;
            images = para.ActualPuzzlepices;
            Image1.Source = imag.ElementAt(0);
            Image2.Source = imag.ElementAt(1);
            Image3.Source = imag.ElementAt(2);
            Image4.Source = imag.ElementAt(3);
            Image5.Source = imag.ElementAt(4);
            Image6.Source = imag.ElementAt(5);
            Image7.Source = imag.ElementAt(6);
            Image8.Source = imag.ElementAt(7);
            Image9.Source = imag.ElementAt(8);

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            print2();
        }

        public void print2()
        {
            PrintDialog pd = new PrintDialog();
            if (pd.ShowDialog() != true) return;

            // create a document
            FixedDocument document = new FixedDocument();
            document.DocumentPaginator.PageSize = new System.Windows.Size(pd.PrintableAreaWidth, pd.PrintableAreaHeight);
            var pageSize = new System.Windows.Size(8.26 * 96, 11.69 * 96);

            for (int i = 0; i < 9; i++)
            {
                Guid id = images[i+1].Id;
                System.Drawing.Bitmap result = bcw.Write(id.ToString());
                System.Drawing.Bitmap barcode = new System.Drawing.Bitmap(result);
                
                document.Pages.Add(createPageContent( images[i + 1].Image , parameters.Bitmap2BitmapImage(barcode),id.ToString() , pageSize));                
            }

            pd.PrintDocument(document.DocumentPaginator, "PuzzlePrint");

        }

        private PageContent createPageContent(BitmapImage puzzle, BitmapImage Qr, String Text, System.Windows.Size pageSize)
        {
            PrintPage pp = new PrintPage(parameters, parameters.BitmapImage2Bitmap(puzzle),parameters.BitmapImage2Bitmap( Qr), Text);
            FixedPage page = new FixedPage();
            page.Width = pageSize.Width;
            page.Height = pageSize.Height;

            TextBlock page1Text = new TextBlock();
            page1Text.Text = Text;
            page1Text.FontSize = pp.Description.FontSize;
            page1Text.Margin = pp.Description.Margin; 
            page.Children.Add(page1Text);


            System.Windows.Controls.Image img = new System.Windows.Controls.Image();
            img.Source = CreateResizedImage(pp.PuzzlePiece.Source.Clone(), 500, 500, 0);
            img.Stretch = Stretch.Uniform;
                  
            img.Margin = pp.PuzzlePiece.Margin;
            page.Children.Add(img);

            System.Windows.Controls.Image img2 = new System.Windows.Controls.Image();
            img2.Source = CreateResizedImage(pp.QR.Source.Clone(), 200, 200, 0);
            img2.Margin = pp.QR.Margin;
            page.Children.Add(img2);
 
            PageContent pageContent = new PageContent();
            ((IAddChild)pageContent).AddChild(page);

            return pageContent;
        }

        private static BitmapFrame CreateResizedImage(ImageSource source, int width, int height, int margin)
        {
            var rect = new Rect(margin, margin, width - margin * 2, height - margin * 2);

            var group = new DrawingGroup();
            RenderOptions.SetBitmapScalingMode(group, BitmapScalingMode.HighQuality);
            group.Children.Add(new ImageDrawing(source, rect));

            var drawingVisual = new DrawingVisual();
            using (var drawingContext = drawingVisual.RenderOpen())
                drawingContext.DrawDrawing(group);

            var resizedImage = new RenderTargetBitmap(
                width, height,         // Resized dimensions
                96, 96,                // Default DPI values
                PixelFormats.Default); // Default pixel format
            resizedImage.Render(drawingVisual);

            return BitmapFrame.Create(resizedImage);
        }


       
    }
}

