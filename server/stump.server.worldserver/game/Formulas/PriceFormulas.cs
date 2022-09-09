using System;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Monsters;
using Stump.Server.WorldServer.Game.Items;

namespace Stump.Server.WorldServer.Game.Formulas
{
    public class PriceFormulas
    {
        public static uint getItemPrice(int itemId)
        {
            try
            {
                var itemLevel = ItemManager.Instance.TryGetTemplate(itemId).Level;
                var itemType = ItemManager.Instance.TryGetTemplate(itemId).TypeId;
                foreach (var drop in MonsterManager.Instance.GetDrops())
                    if (drop.Value.ItemId == itemId)
                    {
                        var monster = MonsterManager.Instance.GetTemplate(drop.Key);
                        if (monster != null)
                        {
                            var isBoss = monster.IsBoss;
                            var rate = drop.Value.DropRateForGrade1;
                            double priceRate = 1;
                            var bossRate = 1;
                            if (isBoss) bossRate = 10;

                            if (rate >= 75)
                                priceRate = 0.05;
                            else if (rate >= 50)
                                priceRate = 0.1;
                            else if (rate >= 20)
                                priceRate = 0.2;
                            else if (rate >= 10)
                                priceRate = 0.3;
                            else if (rate >= 5)
                                priceRate = 0.6;
                            else if (rate >= 2)
                                priceRate = 0.8;
                            else if (rate >= 1)
                                priceRate = 1;
                            else
                                priceRate = 10;

                            return (uint)Math.Floor(itemLevel * 200 * bossRate * priceRate);
                        }
                    }

                if (itemType <= 17) return itemLevel * itemLevel * itemLevel;

                if (itemType == 84) return itemLevel * itemLevel * itemLevel / 20;

                return itemLevel * 50;
            }
            catch (Exception e)
            {
                return 0;
            }
        }
    }
}