using NLog;
using Stump.DofusProtocol.Enums;
using Stump.Server.WorldServer.Database.Items;
using Stump.Server.WorldServer.Database.World;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using Stump.Server.WorldServer.Game.Conditions.Criterions;
using Stump.Server.WorldServer.Game.Effects.Instances;
using System.Linq;

namespace Stump.Server.WorldServer.Game.Items.Player.Custom
{
    [ItemHasEffect(EffectsEnum.Effect_193)]
    public class UseFollower : BasePlayerItem
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public UseFollower(Character owner, PlayerItemRecord record)
            : base(owner, record)
        {
        }

        public override uint UseItem(int amount = 1, Cell targetCell = null, Character target = null)
        {

            var criterion = Template.Effects.OfType<EffectDice>().FirstOrDefault(x => x.EffectId == EffectsEnum.Effect_193).DiceNum;

            if (criterion == 0)
            {
                return base.UseItem(amount, targetCell, target);
            }

            var boostItem = ItemManager.Instance.TryGetTemplate(criterion);

            if (boostItem == null)
            {
                logger.Error(string.Format("Follower {0} has boostItem {1} but it doesn't exist",
                    Template.Id, criterion));
                return 0;
            }

            Owner.Inventory.MoveItem(Owner.Inventory.AddItem(boostItem), CharacterInventoryPositionEnum.INVENTORY_POSITION_FOLLOWER);

            return 1;
        }
    }
}