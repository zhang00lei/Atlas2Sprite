using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AtlasTools
{
    class Parser
    {
        public SpriteInfo[] SpriteInfos { get; private set; }

        public void Parse(int width, int height, string Config)
        {
            List<SpriteInfo> list = new List<SpriteInfo>();
            string[] lines = Config.Split(new char[] { '\n' });
            foreach (string line in lines)
            {
                string[] parts = line.Split(new char[] { ' ' });
                SpriteInfo si = new SpriteInfo(
                    parts[0],
                    Convert.ToInt32(parts[1]),
                    Convert.ToInt32(parts[2]),
                    (int)(Convert.ToSingle(parts[3]) * width),
                    (int)(Convert.ToSingle(parts[4]) * height)
                  );
                list.Add(si);
            }
            SpriteInfos = list.ToArray();

        }
        public void Parse(string Config)
        {
            string jsonConfig = File.ReadAllText(Config);
            JToken jo = JObject.Parse(jsonConfig);
            JToken arr = jo["frames"];
            List<SpriteInfo> list = new List<SpriteInfo>();
            foreach (JProperty baseJToken in arr)
            {
                JToken temp = baseJToken.First;
                SpriteInfo si = new SpriteInfo(
                    baseJToken.Name,
                    int.Parse(temp["frame"]["x"].Value<string>()),
                    int.Parse(temp["frame"]["y"].Value<string>()),
                    int.Parse(temp["frame"]["w"].Value<string>()),
                    int.Parse(temp["frame"]["h"].Value<string>())
                );
                list.Add(si);
            }
            SpriteInfos = list.ToArray();

            /*

            //for (int i = 0, length = jsonStr.Children().Count(); i < length; i++)
            //{
            //   IJEnumerable<JToken> test= jsonStr.Children()[i];
            //    test.
            //}
            //jsonStr[0].Values();
            //jsonStr.Values().Count();

            //List<SpriteInfo> list = new List<SpriteInfo>();
            string pattern = @"name+[\s\S]+?(paddingBottom:.{1,2})";
            Regex regex = new Regex(pattern, RegexOptions.Multiline);
            if (regex.IsMatch(Config))
            {
                MatchCollection matches = regex.Matches(Config);
                Dictionary<string, string> sprites = new Dictionary<string, string>();
                foreach (var match in matches)
                {
                    var SpriteStr = match.ToString();
                    string[] strs = SpriteStr.Split(new char[] { '\n' });

                    foreach (var st in strs)
                    {//vs2017 打包winform
                        string[] m_st = st.Trim().Split(new char[] { ':' });
                        if (sprites.ContainsKey(m_st[0]))
                        {
                            sprites[m_st[0]] = m_st[1].Replace("\'", "");
                        }
                        else
                        {
                            sprites.Add(m_st[0], m_st[1].Replace("\'", ""));
                        }

                    }
                    SpriteInfo si = new SpriteInfo(
                        sprites["name"].Trim(),
                        Convert.ToInt32(sprites["x"]),
                        Convert.ToInt32(sprites["y"]),
                        Convert.ToInt32(sprites["width"]),
                        Convert.ToInt32(sprites["height"])
                    );
                    list.Add(si);
                }
                SpriteInfos = list.ToArray();
            }
            */
        }
    }
}
