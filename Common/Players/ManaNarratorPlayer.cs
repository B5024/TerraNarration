using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace TerraNarration.Common.Players
{
  public class ManaNarratorPlayer : ModPlayer
  {
    private int manaTextCooldown = 0;

    public override void PostUpdate()
    {
      if (manaTextCooldown > 0)
        manaTextCooldown--;
    }

    public override bool CanUseItem(Item item)
    {
      if (item.mana > 0 && Player.statMana < item.mana && manaTextCooldown <= 0)
      {
        var data = Player.GetModPlayer<QuoteDataPlayer>();

        if (data.Quotes != null && data.Quotes.LowManaQuotes.Count > 0)
        {
          // 只有法师武器且真的没魔力时才触发
          string quote = Main.rand.Next(data.Quotes.LowManaQuotes);
          CombatText.NewText(Player.getRect(), Color.LightSkyBlue, quote);

          manaTextCooldown = 120;
        }
      }
      return base.CanUseItem(item);
    }
  }
}
