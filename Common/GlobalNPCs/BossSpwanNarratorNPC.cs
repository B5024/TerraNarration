using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerraNarration.Common.GlobalNPCs
{
  public class BossSpawnNarratorNPC : GlobalNPC
  {
    public override void OnSpawn(NPC npc, Terraria.DataStructures.IEntitySource source)
    {
      // 检查生成的 NPC
      if (npc.type == NPCID.SkeletronHead)
      {
        TriggerBossQuote("SkeletronSpawnQuotes", Color.MediumPurple);
      }
      else if (npc.type == NPCID.EyeofCthulhu)
      {
        TriggerBossQuote("EyeOfCthulhuSpawnQuotes", Color.Cyan);
      }
    }

    private void TriggerBossQuote(string category, Color color)
    {
      Player player = Main.LocalPlayer;
      var data = player.GetModPlayer<Players.QuoteDataPlayer>();
      string quote = "";

      // 对于骷髅王有一句额外台词
      if (category == "SkeletronSpawnQuotes")
      {
        // 如果玩家手持星星炮,生成经典台词
        if (player.HeldItem.type == ItemID.StarCannon)
        {
          quote = Main.rand.Next(data.Quotes.SkeletronStarCannonQuotes);
        }
        else
        {
          quote = Main.rand.Next(data.Quotes.SkeletronSpawnQuotes);
        }
      }
      else
      {
        quote = Main.rand.Next(data.Quotes.EyeOfCthulhuSpawnQuotes);
      }

      CombatText.NewText(player.getRect(), color, quote, false);
    }
  }
}
