using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace TerraNarration.Common.Players
{
  public class WorkingNarratorPlayer : ModPlayer
  {
    // 设置一个冷却计数器，防止文字重叠
    private int textCooldown = 0;

    public override void PostUpdate()
    {
      if (textCooldown > 0)
        textCooldown--;

      // 检测玩家是否正在使用稿子（挖矿中）
      if (Player.HeldItem.pick > 0 && Player.itemAnimation > 0 && textCooldown == 0)
      {
        // 5% 的概率在挖矿时触发吐槽
        if (true || Main.rand.NextBool(20))
        {
          var data = Player.GetModPlayer<QuoteDataPlayer>();
          string quote = Main.rand.Next(data.Quotes.MiningQuotes);
          ShowNarrative(quote, Color.LightGray);
          textCooldown = 300; // 5秒冷却
        }
      }

      // 检测玩家是否正在使用斧子
      if (Player.HeldItem.axe > 0 && Player.itemAnimation > 0 && textCooldown == 0)
      {
        if (true || Main.rand.NextBool(20))
        {
          var data = Player.GetModPlayer<QuoteDataPlayer>();
          string quote = Main.rand.Next(data.Quotes.AxingQuotes);
          ShowNarrative(quote, Color.LightGray);
          textCooldown = 300;
        }
      }
    }

    // 核心方法：生成浮动文字
    private void ShowNarrative(string text, Color color)
    {
      // CombatText.NewText 会在指定位置生成浮动文字，类似伤害数字
      // Main.LocalPlayer.Entity.Center 是玩家中心位置
      CombatText.NewText(Player.getRect(), color, text, false, false);
    }
  }
}
