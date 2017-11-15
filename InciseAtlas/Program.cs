using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AtlasTools;
using System.Windows.Forms;

namespace InciseAtlas
{
    class Program
    {
        static void Main(string[] args)
        {
            args =
                @"E:/project/Branch/ClientWorkBranch/NewUI_V4/Assets/Resources/Res/Atlas_V4/AtlasCommon.png E:/project/Branch/ClientWorkBranch/NewUI_V4/Assets/Resources/Res/Atlas_V4/AtlasFont.png E:/project/Branch/ClientWorkBranch/NewUI_V4/Assets/Resources/Res/Atlas_V4/AtlasHeadIcon.png E:/project/Branch/ClientWorkBranch/NewUI_V4/Assets/AtlasTex_V4/"
                    .Split(' ');
            for (int i = 0, length = args.Length; i < length; i++)
            {
                args[i] = args[i].Replace("/", "\\");
            }
            if (args.Length < 2)
            {
                MessageBox.Show("参数配置错误");
                return;
            }
            //设置最后一个参数为保存目录
            string saveDir = args[args.Length - 1];

            if (!Directory.Exists(saveDir))
            {
                Directory.CreateDirectory(saveDir);
            }
            for (int i = 0, length = args.Length - 1; i < length; i++)
            {
                string[] strTemp = args[i].Split('\\');
                string fileName = strTemp[strTemp.Length - 1];
                fileName = fileName.Replace(".png", string.Empty);
                AtlasFacade facade = new AtlasFacade();
                facade.Process(args[i]);
                Console.WriteLine("处理Atlas：" + args[i]);

                facade.Save(saveDir + fileName);
            }
        }
    }
}
