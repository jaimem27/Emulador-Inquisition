using System.Linq;
using Stump.Core.Mathematics;
using Stump.DofusProtocol.Enums;
using Stump.Server.WorldServer.Database.Items;
using Stump.Server.WorldServer.Database.Spells;
using Stump.Server.WorldServer.Game.Actors.Fight;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using Stump.Server.WorldServer.Game.Effects.Handlers.Items;
using Stump.Server.WorldServer.Game.Effects.Instances;
using Stump.Server.WorldServer.Game.Fights;
using Stump.Server.WorldServer.Game.Jobs;
using Stump.Server.WorldServer.Game.Spells;

namespace Stump.Server.WorldServer.Game.Items.Player.Custom
{
    [ItemHasEffect(EffectsEnum.Effect_669)]
    public sealed class IncarnationWeapon : BasePlayerItem
    {
        private EffectInteger m_incarnationEffect;

        public IncarnationWeapon(Character owner, PlayerItemRecord record)
            : base(owner, record)
        {
            m_incarnationEffect = Effects.OfType<EffectInteger>().FirstOrDefault(x => x.EffectId == EffectsEnum.Effect_669);

            if (m_incarnationEffect == null)
            {
                return;
            }

            if (record.Template.Effects.OfType<EffectInteger>().Any(x => x.EffectId == EffectsEnum.Effect_669))
                if (m_incarnationEffect.Value == Template.Effects.OfType<EffectDice>().FirstOrDefault(x => x.EffectId == EffectsEnum.Effect_669).DiceNum)
                {
                    //Effects.Add( new EffectInteger(EffectsEnum.Effect_669,1));
                    var record_inca = IncarnationManager.Instance.GetCustomIncarnationRecordByItem(Template.Id);
                    if (record_inca != null)
                    {
                        Effects.OfType<EffectInteger>().FirstOrDefault(x => x.EffectId == EffectsEnum.Effect_669).Value = (short)record_inca.Id;
                        Owner.Inventory.ApplyItemEffects(this, true, ItemEffectHandler.HandlerOperation.APPLY);
                    }
                }


            //if (IsEquiped())
            //  OnEquipItem(IsEquiped());

        }


        public override bool OnEquipItem(bool unequip)
        {
            if (unequip)
            {
                IncarnationManager.Instance.UnApplyCustomIncarnation(Owner);
            }
            else
            {

                //var record = IncarnationManager.Instance.GetCustomIncarnationRecordByItem(Template.Id);
                //if (record != null)
                //  IncarnationManager.Instance.ApplyCustomIncarnation(Owner, record);
                CustomIncarnationRecord record = null;
                if (Effects.OfType<EffectInteger>().FirstOrDefault(x => x.EffectId == EffectsEnum.Effect_669).Value != Template.Effects.OfType<EffectDice>().FirstOrDefault(x => x.EffectId == EffectsEnum.Effect_669).DiceNum)
                    record = IncarnationManager.Instance.GetCustomIncarnationRecord(Effects.OfType<EffectInteger>().FirstOrDefault(x => x.EffectId == EffectsEnum.Effect_669).Value);
                if (record != null)
                    IncarnationManager.Instance.ApplyCustomIncarnation(Owner, record);

            }
            return base.OnEquipItem(unequip);
        }



    }
}