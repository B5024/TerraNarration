using Terraria;
using Terraria.ModLoader;
using System.IO;
using Newtonsoft.Json;

namespace TerraNarration.Common.Players
{
    public class QuoteDataPlayer : ModPlayer
    {
        public QuoteLibrary Quotes;
        public override void OnEnterWorld()
        {
            string path = Path.Combine(Main.SavePath, "ModConfigs", "MyQuotes.json");

            if (!File.Exists(path))
            {
                // 如果文件不存在则读取默认的文件
                Quotes = QuoteLibrary.Load(Mod);

                // 将默认文件写出方便修改
                string json = JsonConvert.SerializeObject(Quotes, Formatting.Indented);
                File.WriteAllText(path, json);
            }
            else
            {
                // 读取外部文件
                string content = File.ReadAllText(path);
                Quotes = JsonConvert.DeserializeObject<QuoteLibrary>(content);
            }
        }
    }
}