using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtlasTools
{
    class Cutter
    {
        public Bitmap[] Images { get; private set; }

        public void Cut(Bitmap sourceImage, SpriteInfo[] spriteInfos)
        {
            List<Bitmap> list = new List<Bitmap>();
            foreach (var info in spriteInfos)
            {
                Bitmap smallImage = new Bitmap(info.Width, info.Height);
                Graphics g = Graphics.FromImage(smallImage);
                g.DrawImage(sourceImage, new Rectangle(0, 0, info.Width, info.Height), new Rectangle(info.X, info.Y, info.Width, info.Height), GraphicsUnit.Pixel
                );
                g.Dispose();
                list.Add(smallImage);
            }
            Images = list.ToArray();
        }
    }
}
