using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtlasTools
{
    class AtlasFacade
    {
        private Loader m_loader;
        private Parser m_parser;
        private Cutter m_cutter;
        public Bitmap[] images { get; private set; }

        public AtlasFacade()
        {
            m_loader = new Loader();
            m_parser = new Parser();
            m_cutter = new Cutter();
        }

        public bool Valid(string imageFile)
        {
            return imageFile.ToLower().EndsWith(".png");
        }

        public void Process(string imageFile)
        {
            //加载
            m_loader.Load(imageFile);
            //大图
            Bitmap soureImage = m_loader.Image;
            //配置
            string config = imageFile.Replace(".png", ".txt");
            //解析
            m_parser.Parse(config);
            SpriteInfo[] spriteInfos = m_parser.SpriteInfos;

            //切割
            m_cutter.Cut(soureImage, spriteInfos);
            images = m_cutter.Images;
        }

        public void Save(string path = null)
        {
            if (path == null)
                path = m_loader.ResourceDir;
            for (int i = 0; i < m_parser.SpriteInfos.Length; i++)
            {
                SpriteInfo info = m_parser.SpriteInfos[i];
                Bitmap image = images[i];
                string fileName = path + "\\" + info.Name + ".png";
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                image.Save(fileName);
            }
        }
    }
}