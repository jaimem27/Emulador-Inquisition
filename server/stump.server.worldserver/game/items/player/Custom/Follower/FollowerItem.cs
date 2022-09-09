using System.Linq;
using Stump.DofusProtocol.D2oClasses;
using Stump.DofusProtocol.Enums;
using Stump.Server.WorldServer.Database.Items;
using Stump.Server.WorldServer.Game.Actors.Fight;
using Stump.Server.WorldServer.Game.Actors.RolePlay;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using Stump.Server.WorldServer.Game.Effects.Handlers.Items;
using Stump.Server.WorldServer.Game.Effects.Instances;
using Stump.Server.WorldServer.Game.Fights;
using Stump.Server.WorldServer.Game.Maps.Cells;

namespace Stump.Server.WorldServer.Game.Items.Player.Custom
{
    [ItemType(ItemTypeEnum.PERSONNAGE_SUIVEUR)]
    public class FollowerItem : BasePlayerItem
    {
        private bool m_removed;

        public FollowerItem(Character owner, PlayerItemRecord record)
            : base(owner, record)
        {
            int type = (Effects.FirstOrDefault(x => x.EffectId == EffectsEnum.Effect_148) as EffectInteger).Value;//Dicenum IF are 2 are Monster
            int follower = (Template.PossibleEffects.FirstOrDefault(x => x.EffectId == 148) as EffectInstanceDice).Value;
            foreach (var folling in owner.following.ToList())
            {

                folling.Map.Leave(folling);
                foreach (var item in Owner.Inventory.GetItems(x => x is FollowerItem && (x as FollowerItem).followerActor == folling))
                    Owner.Inventory.RemoveItem(item);
                Owner.following.Remove(folling);
            }
            AddFollower(owner, (short)type, follower);
            Owner.FightEnded += OnFightEnded;
            Owner.ContextChanged += OnContextChanged;
        }

        private void OnContextChanged(Character character, bool infight)
        {
            if (infight && character.Fight.IsPvP)
            {
                Owner.Inventory.ApplyItemEffects(this, force: ItemEffectHandler.HandlerOperation.UNAPPLY);
            }
        }

        public override bool OnEquipItem(bool unequip)
        {
            if (unequip && !m_removed)
                Owner.Inventory.RemoveItem(this);

            return base.OnEquipItem(unequip);
        }

        public override bool OnRemoveItem()
        {
            m_removed = true;
            Owner.FightEnded -= OnFightEnded;

            return base.OnRemoveItem();
        }

        private void OnFightEnded(Character character, CharacterFighter fighter)
        {
            var effect = Effects.OfType<EffectDice>().FirstOrDefault(x => x.EffectId == EffectsEnum.Effect_RemainingFights);

            if (effect == null)
                return;

            if (fighter.Fight.IsPvP)
            {
                Owner.Inventory.ApplyItemEffects(this, force: ItemEffectHandler.HandlerOperation.APPLY);
            }

            Invalidate();

            if (!fighter.Fight.IsPvP)
            {
                if (--effect.Value <= 0)
                {
                    Owner.following.Remove(followerActor);
                    followerActor.Map.Leave(followerActor);
                    Owner.Inventory.RemoveItem(this);
                }
                else
                    Owner.Inventory.RefreshItem(this);
            }
        }
        private FollowerActor followerActor
        {
            get;
            set;

        }
        private void AddFollower(Character target, short type, int follower)
        {
            var position_char = target.Position.Clone();
            var excludedCells = position_char.Map.GetActors<RolePlayActor>().Select(entry => entry.Cell.Id);
            var position = new ObjectPosition(position_char.Map, position_char.Point.GetAdjacentCells(true).Where(x => x.IsInMap()).OrderBy(x => x.ManhattanDistanceTo(position_char.Point)).Where(x => position_char.Map.Cells[x.CellId].Walkable && !excludedCells.Contains(x.CellId)).FirstOrDefault().CellId, position_char.Direction);


            followerActor = new FollowerActor(position.Map.GetNextContextualId(), position, target, type, follower);
            target.following.Add(followerActor);
            followerActor.Map.Enter(followerActor);

        }
    }
}