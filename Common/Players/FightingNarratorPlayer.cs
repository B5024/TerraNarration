using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace TerraNarration.Common.Players
{
  public class FightingNarratorPlayer : ModPlayer
  {
    // 设置一个冷却计数器，防止文字重叠
    private int textCooldown = 0;

    // 低血量触发器
    private bool lowHealthTriggered = false;
    private readonly float lowHealthPercent = 0.3f;
    private readonly float normalHealthPercent = 0.4f;

    public override void PostUpdate()
    {
      if (textCooldown > 0)
      {
        textCooldown--;
      }

      // 低血量检测
      float healthPercent = (float)Player.statLife / Player.statLifeMax2;

      if (healthPercent <= lowHealthPercent)
      {
        // 标记未触发并冷却完毕
        if (!lowHealthTriggered && textCooldown <= 0)
        {
          var data = Player.GetModPlayer<QuoteDataPlayer>();
          string quote = Main.rand.Next(data.Quotes.LowHealthQuotes);

          ShowNarrative(quote, Color.Salmon);

          lowHealthTriggered = true; // 标记已触发
          textCooldown = 600;
        }
      }
      else if (healthPercent > normalHealthPercent)
      {
        // 重置低血量触发器
        lowHealthTriggered = false;
      }
    }

    // 当玩家被NPC击中时
    public override void OnHitByNPC(NPC npc, Player.HurtInfo hurtInfo)
    {
      if (textCooldown > 0)
        return;

      var data = Player.GetModPlayer<QuoteDataPlayer>();
      string quote = Main.rand.Next(data.Quotes.OnHitByNPCQuotes);

      ShowNarrative(quote, Color.Orange);
      textCooldown = 180;
    }

    // 当玩家被弹幕击中时
    public override void OnHitByProjectile(Projectile proj, Player.HurtInfo hurtInfo)
    {
      if (textCooldown > 0)
        return;

      var data = Player.GetModPlayer<QuoteDataPlayer>();
      string quote = Main.rand.Next(data.Quotes.OnHitByProjectileQuotes);

      ShowNarrative(quote, Color.Red);
      textCooldown = 180;
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
