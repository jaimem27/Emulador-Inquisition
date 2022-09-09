using Stump.Core.Reflection;
using Stump.DofusProtocol.Enums;
using Stump.Server.WorldServer.Game.Actors.Fight;
using Stump.Server.WorldServer.Game.Effects;
using Stump.Server.WorldServer.Game.Effects.Handlers.Spells;
using Stump.Server.WorldServer.Game.Effects.Instances;
using Stump.Server.WorldServer.Game.Fights;
using Stump.Server.WorldServer.Game.Fights.Buffs;
using System.Collections.Generic;
using System.Linq;

namespace Stump.Server.WorldServer.Game.Spells.Casts.Monsters
{
    [SpellCastHandler(SpellIdEnum.MASTERLY_CORALACTION)]
    public class CoralacionMagistral : DefaultSpellCastHandler
    {
        public CoralacionMagistral(SpellCastInformations cast)
            : base(cast)
        {
        }

        public override void Execute()
        {
            if (!m_initialized)
                Initialize();

            EffectDice dice = new EffectDice(EffectsEnum.Effect_AddMP_128, -3, 0, 0);
            EffectDice dice1 = new EffectDice(EffectsEnum.Effect_AddAP_111, 12, 0, 0);
            EffectDice dice2 = new EffectDice(EffectsEnum.Effect_IncreaseDamage_138, 400, 0, 0);

            var hand = EffectManager.Instance.GetSpellEffectHandler(dice, Caster, this, Caster.Cell, false);
            hand.DefaultDispellableStatus = FightDispellableEnum.DISPELLABLE; // tocheck
            
            hand.Duration = 4;
            hand.Apply();

            var hand1 = EffectManager.Instance.GetSpellEffectHandler(dice1, Caster, this, Caster.Cell, false);
            hand1.DefaultDispellableStatus = FightDispellableEnum.DISPELLABLE; // tocheck
            hand1.Duration = 4;
            hand1.Apply();

            var hand2 = EffectManager.Instance.GetSpellEffectHandler(dice2, Caster, this, Caster.Cell, false);
            hand2.DefaultDispellableStatus = FightDispellableEnum.DISPELLABLE; // tocheck
            hand2.Duration = 4;
            hand2.Apply();

            base.Execute();
        }
    }

    [SpellCastHandler(SpellIdEnum.MASTERLY_CORAL_ATTACK)]
    public class GolpeCoral : DefaultSpellCastHandler
    {
        public GolpeCoral(SpellCastInformations cast)
            : base(cast)
        {
        }

        public override void Execute()
        {
            if (!m_initialized)
                Initialize();

            //Caster.CastAutoSpell(new Spell(),Caster.Cell);

            //Handlers[0].Apply();

            base.Execute();
        }
    }

}
