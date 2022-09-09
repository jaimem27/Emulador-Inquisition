using Stump.DofusProtocol.Enums;
using Stump.Server.WorldServer.Database.Items.Templates;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using Stump.Server.WorldServer.Game.Effects.Instances;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NLog;
using Stump.Core.Extensions;
using Stump.Server.WorldServer.Game.Items.Player;

namespace Stump.Server.WorldServer.Game.Effects.Handlers.Items
{
    [EffectHandler(EffectsEnum.Effect_ApCostReduce)] //Done
    public class ApCostEffectHandler : ItemEffectHandler
    {
        public ApCostEffectHandler(EffectBase effect, Character target, BasePlayerItem item)
            : base(effect, target, item)
        {
        }

        public ApCostEffectHandler(EffectBase effect, Character target, ItemSetTemplate itemSet, bool apply)
            : base(effect, target, itemSet, apply)
        {
        }

        protected override bool InternalApply()
        {
            var values = Effect.GetValues();
            object idSpellObject = values[0];
            short idSpellShort = Convert.ToInt16(idSpellObject.ToString());
            if (values == null) return false;
            if (Operation == HandlerOperation.APPLY)
                Target.ReduceSpellCost(idSpellShort, (uint) (values.Length > 2 ? (ushort)values[2] : 1));
            else
                Target.SpellCostDisable(idSpellShort, (short) (values.Length > 2 ? (ushort)values[2] : 1));

            return true;
        }
    }

    [EffectHandler(EffectsEnum.Effect_SpellObstaclesDisable)] //Done
    public class SpellObstaclesDisableEffectHandler : ItemEffectHandler
    {
        public SpellObstaclesDisableEffectHandler(EffectBase effect, Character target, BasePlayerItem item)
            : base(effect, target, item)
        {
        }

        public SpellObstaclesDisableEffectHandler(EffectBase effect, Character target, ItemSetTemplate itemSet, bool apply)
            : base(effect, target, itemSet, apply)
        {
        }

        protected override bool InternalApply()
        {

            var integerEffect = Effect.GenerateEffect(EffectGenerationContext.Item) as EffectInteger;

            if (integerEffect == null)
                return false;
            if (Operation == HandlerOperation.APPLY)
                Target.SpellObstaclesDisable((short)integerEffect.Value);
            else
                Target.SpellObstaclesEnable((short)integerEffect.Value);

            return true;
        }
    }

    [EffectHandler(EffectsEnum.Effect_LineCastDisable)] //Done
    public class LineCastDisableEffectHandler : ItemEffectHandler
    {
        public LineCastDisableEffectHandler(EffectBase effect, Character target, BasePlayerItem item)
            : base(effect, target, item)
        {
        }

        public LineCastDisableEffectHandler(EffectBase effect, Character target, ItemSetTemplate itemSet, bool apply)
            : base(effect, target, itemSet, apply)
        {
        }

        protected override bool InternalApply()
        {

            var integerEffect = Effect.GenerateEffect(EffectGenerationContext.Item) as EffectInteger;

            if (integerEffect == null)
                return false;


            if (Operation == HandlerOperation.APPLY)
                Target.LineCastDisable((short)integerEffect.Value);
            else
                Target.LineCastEnable((short)integerEffect.Value);

            return true;
        }
    }

    [EffectHandler(EffectsEnum.Effect_SpellRangeableEnable)] //Done
    public class SpellRangeableEnableEffectHandler : ItemEffectHandler
    {
        public SpellRangeableEnableEffectHandler(EffectBase effect, Character target, BasePlayerItem item)
            : base(effect, target, item)
        {
        }

        public SpellRangeableEnableEffectHandler(EffectBase effect, Character target, ItemSetTemplate itemSet, bool apply)
            : base(effect, target, itemSet, apply)
        {
        }

        protected override bool InternalApply()
        {

            var integerEffect = Effect.GenerateEffect(EffectGenerationContext.Item) as EffectInteger;

            if (integerEffect == null)
                return false;


            if (Operation == HandlerOperation.APPLY)
                Target.SpellRangeableEnable((short)integerEffect.Value);
            else
                Target.SpellRangeableDisable((short)integerEffect.Value);

            return true;
        }
    }

    [EffectHandler(EffectsEnum.Effect_SpellRangeIncrease)] //Done
    public class SpellRangeIncreaseEffectHandler : ItemEffectHandler
    {
        public SpellRangeIncreaseEffectHandler(EffectBase effect, Character target, BasePlayerItem item)
            : base(effect, target, item)
        {
        }

        public SpellRangeIncreaseEffectHandler(EffectBase effect, Character target, ItemSetTemplate itemSet, bool apply)
            : base(effect, target, itemSet, apply)
        {
        }

        protected override bool InternalApply()
        {
            var values = Effect.GetValues();
            object idSpellObject = values[0];
            short idSpellShort = Convert.ToInt16(idSpellObject.ToString());
            if (values == null) return false;
            if (Operation == HandlerOperation.APPLY)
                Target.IncreaseRange(idSpellShort, Convert.ToUInt32((values.Length > 2 ? values[2] : 1)));
            else
                Target.IncreaseRangeDisable(idSpellShort, Convert.ToInt16((values.Length > 2 ? values[2].ToString() : 1.ToString())));

            return true;
        }
    }

    [EffectHandler(EffectsEnum.Effect_SpellDelayReduce)] //Done
    public class SpellDelayReduceEffectHandler : ItemEffectHandler
    {
        public SpellDelayReduceEffectHandler(EffectBase effect, Character target, BasePlayerItem item)
            : base(effect, target, item)
        {
        }

        public SpellDelayReduceEffectHandler(EffectBase effect, Character target, ItemSetTemplate itemSet, bool apply)
            : base(effect, target, itemSet, apply)
        {
        }

        protected override bool InternalApply()
        {
            var values = Effect.GetValues();
            object idSpellObject = values[0];
            short idSpellShort = Convert.ToInt16(idSpellObject.ToString());
            if (values == null) return false;
            if (Operation == HandlerOperation.APPLY)
                Target.ReduceDelay(idSpellShort, (ushort)(values.Length > 2 ? (ushort)values[2] : 1));
            else
                Target.ReduceDelayDisable(idSpellShort);

            return true;
        }
    }

    [EffectHandler(EffectsEnum.Effect_SpellMaxCastBoost)] //Done
    public class SpellMaxCastBoostEffectHandler : ItemEffectHandler
    {
        public SpellMaxCastBoostEffectHandler(EffectBase effect, Character target, BasePlayerItem item)
            : base(effect, target, item)
        {
        }

        public SpellMaxCastBoostEffectHandler(EffectBase effect, Character target, ItemSetTemplate itemSet, bool apply)
            : base(effect, target, itemSet, apply)
        {
        }

        protected override bool InternalApply()
        {
            var values = Effect.GetValues();
            object idSpellObject = values[0];
            short idSpellShort = Convert.ToInt16(idSpellObject.ToString());
            if (values == null) return false;
            if (Operation == HandlerOperation.APPLY)
                Target.AddMaxCastPerTurn(idSpellShort, (uint)(values.Length > 2 ? (ushort)values[2] : 1));
            else
                Target.AddMaxCastPerTurnDisable(idSpellShort);

            return true;
        }
    }

    [EffectHandler(EffectsEnum.Effect_SpellMaxTargetCastBoost)] //Done
    public class SpellMaxCastPerTargetEffectHandler : ItemEffectHandler
    {
        public SpellMaxCastPerTargetEffectHandler(EffectBase effect, Character target, BasePlayerItem item)
            : base(effect, target, item)
        {
        }

        public SpellMaxCastPerTargetEffectHandler(EffectBase effect, Character target, ItemSetTemplate itemSet, bool apply)
            : base(effect, target, itemSet, apply)
        {
        }

        protected override bool InternalApply()
        {
            var values = Effect.GetValues();
            object idSpellObject = values[0];
            short idSpellShort = Convert.ToInt16(idSpellObject.ToString());
            if (values == null) return false;
            if (Operation == HandlerOperation.APPLY)
                Target.AddMaxCastPerTarget(idSpellShort, (uint)(values.Length > 2 ? (ushort)values[2] : 1));
            else
                Target.AddMaxCastPerTargetDisable(idSpellShort);

            return true;
        }
    }

    [EffectHandler(EffectsEnum.Effect_SpellDamageIncrease)] //Done
    public class SpellDamageIncreaseEffectHandler : ItemEffectHandler
    {
        public SpellDamageIncreaseEffectHandler(EffectBase effect, Character target, BasePlayerItem item)
            : base(effect, target, item)
        {
        }

        public SpellDamageIncreaseEffectHandler(EffectBase effect, Character target, ItemSetTemplate itemSet, bool apply)
            : base(effect, target, itemSet, apply)
        {
        }

        protected override bool InternalApply()
        {
            var values = Effect.GetValues();
            object idSpellObject = values[0];
            short idSpellShort = Convert.ToInt16(idSpellObject.ToString());
            if (values == null) return false;
            if (Operation == HandlerOperation.APPLY)
                Target.AddDamage(idSpellShort, (uint)(values.Length > 2 ? (ushort)values[2] : 1));
            else
                Target.AddDamageDisable(idSpellShort);

            return true;
        }
    }

    [EffectHandler(EffectsEnum.Effect_SpellHealIncrease)] //Done
    public class SpellHealIncreaseEffectHandler : ItemEffectHandler
    {
        public SpellHealIncreaseEffectHandler(EffectBase effect, Character target, BasePlayerItem item)
            : base(effect, target, item)
        {
        }

        public SpellHealIncreaseEffectHandler(EffectBase effect, Character target, ItemSetTemplate itemSet, bool apply)
            : base(effect, target, itemSet, apply)
        {
        }

        protected override bool InternalApply()
        {

            var values = Effect.GetValues();
            object idSpellObject = values[0];
            short idSpellShort = Convert.ToInt16(idSpellObject.ToString());
            if (values == null) return false;
            if (Operation == HandlerOperation.APPLY)
                Target.AddHeal(idSpellShort, (uint)(values.Length > 2 ? (ushort)values[2] : 1));
            else
                Target.AddHealDisable(idSpellShort);

            return true;
        }
    }

    [EffectHandler(EffectsEnum.Effect_SpellCriticalPercent)] //Done
    public class SpellCriticalPercentEffectHandler : ItemEffectHandler
    {
        public SpellCriticalPercentEffectHandler(EffectBase effect, Character target, BasePlayerItem item)
            : base(effect, target, item)
        {
        }

        public SpellCriticalPercentEffectHandler(EffectBase effect, Character target, ItemSetTemplate itemSet, bool apply)
            : base(effect, target, itemSet, apply)
        {
        }

        protected override bool InternalApply()
        {
            var values = Effect.GetValues();
            object idSpellObject = values[0];
            short idSpellShort = Convert.ToInt16(idSpellObject.ToString());
            if (values == null) return false;
            if (Operation == HandlerOperation.APPLY)
                Target.AddCritical(idSpellShort, (uint)(values.Length > 2 ? (ushort)values[2] : 1));
            else
                Target.AddCriticalDisable(idSpellShort);

            return true;
        }
    }
}
