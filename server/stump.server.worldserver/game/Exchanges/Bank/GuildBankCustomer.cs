using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using System.Collections.Generic;
using Stump.Server.WorldServer.Database.Items;

namespace Stump.Server.WorldServer.Game.Exchanges.Bank
{
    public class GuildBankCustomer : Exchanger
    {
        public GuildBankCustomer(Character character, GuildBankDialog dialog)
            : base(dialog)
        {
            Character = character;
        }

        public Character Character
        {
            get;
        }

        public override bool MoveItem(uint id, int quantity)
        {
            if (quantity > 0)
            {
                var item = Character.Inventory.TryGetItem((int)id);

                return item != null && Character.GuildBank.StoreItem(item, quantity, true) != null;
            }

            if (quantity >= 0)
                return false;

            var deleteItem = Character.GuildBank.TryGetItem((int)id);

            return Character.GuildBank.TakeItemBack(deleteItem, -quantity, true) != null;
        }

        public override bool SetKamas(long amount)
        {
            if (amount > 0)
                return Character.GuildBank.StoreKamas((ulong)amount);

            return amount < 0 && Character.GuildBank.TakeKamas(-amount);
        }

        public void MoveItems(bool store, IEnumerable<uint> guids, bool all, bool existing)
        {
            var guids_ = new List<int>();
            foreach (var id in guids)
            {
                guids_.Add((int)id);
            }
            if (store)
                Character.GuildBank.StoreItems(guids_, all, existing);
            else
                Character.GuildBank.TakeItemsBack(guids_, all, existing);
        }
    }
}