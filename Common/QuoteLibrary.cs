using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json; // TModLoader 自带 Newtonsoft
using Terraria.ModLoader;

namespace TerraNarration.Common
{
    public class QuoteLibrary
    {
        public List<string> OnHitByNPCQuotes { get; set; }
        public List<string> OnHitByProjectileQuotes { get; set; }
        public List<string> LowHealthQuotes { get; set; }
        public List<string> MiningQuotes { get; set; }
        public List<string> AxingQuotes { get; set; }

        public static QuoteLibrary Load(Mod mod)
        {
            // 从 Mod 的资源中获取文件流
            using Stream stream = mod.GetFileStream("quotes.json");
            using StreamReader reader = new StreamReader(stream, Encoding.UTF8);
            string json = reader.ReadToEnd();
            return JsonConvert.DeserializeObject<QuoteLibrary>(json);
        }
    }
}