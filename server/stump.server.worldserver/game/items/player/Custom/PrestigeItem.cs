using System.Collections.Generic;
using System.Linq;
using Stump.Server.WorldServer.Database.Items;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using Stump.Server.WorldServer.Game.Effects;
using Stump.Server.WorldServer.Game.Effects.Handlers.Items;
using Stump.Server.WorldServer.Game.Effects.Instances;

namespace Stump.Server.WorldServer.Game.Items.Player.Custom
{
    [ItemId(PrestigeManager.ItemForBonus)]
    public sealed class PrestigeItem : BasePlayerItem
    {
        public PrestigeItem(Character owner, PlayerItemRecord record)
            : base(owner, record)
        {
            IsTemporarily = true;
            var effects = PrestigeManager.Instance.GetPrestigeEffectsViajero(Owner.PrestigeRank);
            Effects = new List<EffectBase>(effects);
        }

        public override bool OnAddItem()
        {
            ApplyBonus(ItemEffectHandler.HandlerOperation.APPLY);
            return base.OnAddItem();
        }

        private void ApplyBonus(ItemEffectHandler.HandlerOperation operation)
        {
            foreach (var handler in Effects.Select(effect => EffectManager.Instance.GetItemEffectHandler(effect, Owner, this)))
            {
                handler.Operation = operation;

                handler.Apply();
            }
        }

        public void UpdateEffectsViajero()
        {
            ApplyBonus(ItemEffectHandler.HandlerOperation.UNAPPLY);
            var effects = PrestigeManager.Instance.GetPrestigeEffectsViajero(Owner.PrestigeRank);

            Effects = new List<EffectBase>(effects);
            ApplyBonus(ItemEffectHandler.HandlerOperation.APPLY);
        }

        public void UpdateEffectsValeroso()
        {
            ApplyBonus(ItemEffectHandler.HandlerOperation.UNAPPLY);
            var effects = PrestigeManager.Instance.GetPrestigeEffectsValeroso(Owner.PrestigeRank);

            Effects = new List<EffectBase>(effects);
            ApplyBonus(ItemEffectHandler.HandlerOperation.APPLY);
        }

        public void UpdateEffectsCorazon()
        {
            ApplyBonus(ItemEffectHandler.HandlerOperation.UNAPPLY);
            var effects = PrestigeManager.Instance.GetPrestigeEffectsCorazon(Owner.PrestigeRank);

            Effects = new List<EffectBase>(effects);
            ApplyBonus(ItemEffectHandler.HandlerOperation.APPLY);
        }

        public void UpdateEffectsMajestuoso()
        {
            ApplyBonus(ItemEffectHandler.HandlerOperation.UNAPPLY);
            var effects = PrestigeManager.Instance.GetPrestigeEffectsMajestuoso(Owner.PrestigeRank);

            Effects = new List<EffectBase>(effects);
            ApplyBonus(ItemEffectHandler.HandlerOperation.APPLY);
        }

        public void UpdateEffectsRocoso()
        {
            ApplyBonus(ItemEffectHandler.HandlerOperation.UNAPPLY);
            var effects = PrestigeManager.Instance.GetPrestigeEffectsRocoso(Owner.PrestigeRank);

            Effects = new List<EffectBase>(effects);
            ApplyBonus(ItemEffectHandler.HandlerOperation.APPLY);
        }

        public void UpdateEffectsTramposo()
        {
            ApplyBonus(ItemEffectHandler.HandlerOperation.UNAPPLY);
            var effects = PrestigeManager.Instance.GetPrestigeEffectsTramposo(Owner.PrestigeRank);

            Effects = new List<EffectBase>(effects);
            ApplyBonus(ItemEffectHandler.HandlerOperation.APPLY);
        }

        public override bool OnRemoveItem()
        {
            ApplyBonus(ItemEffectHandler.HandlerOperation.UNAPPLY);
            return base.OnRemoveItem();
        }

        public override bool IsLinkedToPlayer()
        {
            return true;
        }

        public override bool IsLinkedToAccount()
        {
            return true;
        }
    }
}