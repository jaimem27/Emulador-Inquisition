using System;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using Stump.Server.WorldServer.Game.Exchanges.Trades;
using Stump.Server.WorldServer.Game.Prisms;

namespace Stump.Server.WorldServer.Game.Exchanges.Prism {
    public class PrismTrader : Trader {
        public PrismTrader (PrismNpc taxCollector, Character character, AlliancePrismTrade taxCollectorTrade) : base (taxCollectorTrade) {
            PrismNpc = taxCollector;
            Character = character;
        }

        public override int Id => Character.Id;

        public PrismNpc PrismNpc { get; }

        public Character Character { get; }

        public bool SetKamas (int amount) {
            return false;
        }

        public override bool MoveItem(uint id, int quantity)
        {
            bool result;
            var newQuantity = Math.Abs(quantity) ;
            if (newQuantity <= 0 || (Character.Map.Prism == null || Character.Map.Prism != PrismNpc) ||
                (Character.Guild == null && Character.Map.Prism.Alliance.Id != Character.Guild?.Alliance?.Id)) {
                Character.SendInformationMessage (DofusProtocol.Enums.TextInformationTypeEnum.TEXT_INFORMATION_ERROR, 101);
                return false;
            }
            var taxCollectorItem = Character.Inventory.TryGetItem ((int) id);
            if (taxCollectorItem == null)
                result = false;
            else {
                PrismNpc.Bag.MoveToInventory (taxCollectorItem, Character, (uint) newQuantity);
                result = true;
            }
            return result;
        }
    }
}