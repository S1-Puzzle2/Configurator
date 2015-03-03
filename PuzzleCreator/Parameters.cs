using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace PuzzleCreator
{
    public class Parameters
    {
        public  ServerConnection serverConnectionWindow;
        public  MainWindow mainWindow;
        public  NetworkController _networkController;
        public  int _puzzleHorizontalCount;
        public  Dictionary<int, PuzzlePiece> actualPuzzlepices;
        public  bool ConnectionActive;
        public  string actualPuzzleName;
        public  int actualPuzzleID;
        public  Dictionary<string, Dictionary<int, Guid>> puzzlePieces;
        public  bool allPartsReceived;
        public  bool puzzleCreated;

        public Parameters(MainWindow mW)
        {
            _networkController = new NetworkController(this);
            _puzzleHorizontalCount = 3;
            actualPuzzlepices = new Dictionary<int, PuzzlePiece>();
            ConnectionActive = false;
            puzzlePieces = new Dictionary<string, Dictionary<int, Guid>>();
            allPartsReceived = false;
            puzzleCreated = false;
            mainWindow = mW;
        }

        public  Dictionary<int, PuzzlePiece> ActualPuzzlepices
        {
            get { return actualPuzzlepices; }
            set { actualPuzzlepices = value; }
        }


        public  int PuzzleHorizontalCount
        {
            get { return _puzzleHorizontalCount; }
            private set { _puzzleHorizontalCount = value; }
        }
         int _puzzleVerticalCount = 3;

        public  int PuzzleVerticalCount
        {
            get { return _puzzleVerticalCount; }
            private set { _puzzleVerticalCount = value; }
        }

         void addNetworkController(NetworkController nc)
        {
            _networkController = nc;
        }

         Boolean isAlive()
        {
            if (_networkController != null)
            {
                return _networkController.connected;
            }
            return false;
        }

         public BitmapImage DeepCopy(BitmapImage bitmapImage)
         {
             return Bitmap2BitmapImage(BitmapImage2Bitmap(bitmapImage));
         }

         public LinkedList<Bitmap> getActualPuzzleList()
         {
             LinkedList<Bitmap> bitm = new LinkedList<Bitmap>();

             for (int i = 1; i <= 9; i++)
             {
                 bitm.AddLast(BitmapImage2Bitmap(ActualPuzzlepices[i].Image));
             }
             return bitm;
         }

         public LinkedList<BitmapImage> ConvertBitmapToImage(LinkedList<Bitmap> bitml)
         {
             LinkedList<BitmapImage> bitmi = new LinkedList<BitmapImage>();

             foreach (Bitmap bitm in bitml)
             {
                 bitmi.AddLast(Bitmap2BitmapImage(bitm));
             }

             return bitmi;
         }


        public  BitmapImage Bitmap2BitmapImage(Bitmap bitmap)
        {
            using (var memory = new MemoryStream())
            {
                bitmap.Save(memory, ImageFormat.Png);
                memory.Position = 0;

                var bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memory;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();

                return bitmapImage;
            }
        }

        public Bitmap BitmapImage2Bitmap(BitmapImage bitmapImage)
        {
            using (MemoryStream outStream = new MemoryStream())
            {
                BitmapEncoder enc = new BmpBitmapEncoder();
                enc.Frames.Add(BitmapFrame.Create(bitmapImage));
                enc.Save(outStream);
                System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(outStream);

                return new Bitmap(bitmap);
            }
        }

    }
}
