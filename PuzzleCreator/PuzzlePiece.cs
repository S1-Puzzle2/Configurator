using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace PuzzleCreator
{
    public class PuzzlePiece
    {
        Guid id;

        public Guid Id
        {
            get { return id; }
            set { id = value; }
        }

        BitmapImage image;

        public BitmapImage Image
        {
            get { return image; }
            set { image = value; }
        }

        public PuzzlePiece(Guid guid, BitmapImage img)
        {
            this.Id = guid;
            this.Image = img;
        }

    }
}
