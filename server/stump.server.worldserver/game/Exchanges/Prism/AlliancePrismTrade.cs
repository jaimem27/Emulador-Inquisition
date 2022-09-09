using System.Linq;
using Stump.DofusProtocol.Enums;
using Stump.DofusProtocol.Messages;
using Stump.DofusProtocol.Types;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using Stump.Server.WorldServer.Game.Exchanges.Trades;
using Stump.Server.WorldServer.Game.Prisms;
using Stump.Server.WorldServer.Handlers.Inventory;

namespace Stump.Server.WorldServer.Game.Exchanges.Prism {
    public class AlliancePrismTrade : Trade<PrismTrader, EmptyTrader> {
        public AlliancePrismTrade (PrismNpc npc, Character character) {
            PrismNpc = npc;
            Character = character;
            FirstTrader = new PrismTrader (npc, character, this);
            SecondTrader = new EmptyTrader (npc.Id, this);
        }

        public PrismNpc PrismNpc { get; }

        public Character Character { get; }

        public override ExchangeTypeEnum ExchangeType => ExchangeTypeEnum.ALLIANCE_PRISM;

        public override void Open () {
            base.Open ();
            Character.SetDialoger (FirstTrader);
            PrismNpc.OnDialogOpened (this);

            var exchangeStartedWithStorageMessage = new ExchangeStartedWithStorageMessage ((sbyte) ExchangeType, 0);
            //  using (var exchangeStartedWithStorageMessage = new ExchangeStartedWithStorageMessage((sbyte)ExchangeType, 0))
            // {
            Character.Client.Send (exchangeStartedWithStorageMessage);
            //}

            var storageInventoryContentMessage = new StorageInventoryContentMessage (PrismNpc.Bag.GetObjectItems ().ToArray(), 0);
            Character.Client.Send (storageInventoryContentMessage);

        }

        public override void Close () {
            base.Close ();
            InventoryHandler.SendExchangeLeaveMessage (Character.Client, DialogType, false);
            Character.CloseDialog (this);
            PrismNpc.OnDialogClosed (this);
        }

        protected override void Apply () { }
    }
}