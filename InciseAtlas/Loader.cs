using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
namespace AtlasTools
{
    class Loader
    {
        public System.Drawing.Bitmap test;
        public Bitmap Image { get; private set; }
        public string Config { get; private set; }

        public string ResourceDir { get; private set; }

        public void Load(string imageFile)
        {
            ResourceDir = Path.GetDirectoryName(imageFile);
            Image = new Bitmap(imageFile);

            string fileName = Path.GetFileNameWithoutExtension(imageFile);
            string configFile = ResourceDir + "\\" + fileName + ".txt";
            StreamReader sr = new StreamReader(configFile);
            Config = sr.ReadToEnd();
            sr.Close();
            sr.Dispose();
        }

    }
}
