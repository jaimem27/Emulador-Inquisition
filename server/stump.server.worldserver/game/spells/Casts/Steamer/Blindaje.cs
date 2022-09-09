using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stump.DofusProtocol.Enums;
using Stump.Server.WorldServer.Game.Actors.Fight;
using Stump.Server.WorldServer.Game.Effects;
using Stump.Server.WorldServer.Game.Effects.Handlers.Spells;
using Stump.Server.WorldServer.Game.Effects.Instances;
using Stump.Server.WorldServer.Game.Fights;
using Stump.Server.WorldServer.Game.Fights.Buffs;

namespace Stump.Server.WorldServer.Game.Spells.Casts.Steamer
{
    [SpellCastHandler(SpellIdEnum.ARMOUR_PLATING_5498)]
    [SpellCastHandler(SpellIdEnum.ARMOUR_PLATING_9858)]
    public class Blindaje : DefaultSpellCastHandler
    {
        public Blindaje(SpellCastInformations cast) : base(cast) { }

        public override void Execute()
        {
            if (!m_initialized)
                Initialize();


            List<SpellEffectHandler> effects = base.Handlers.ToList();


            Handlers = effects.ToArray();

            var Target = Fight.GetOneFighter(TargetedCell);
            if (Target != null)
            {

                var h = Handlers;
                short shieldAmount = (short)0;

                if (Target is SummonedTurret)
                {
                    shieldAmount = (short)(Caster.Level * 4.8);

                    if(Caster.Level > 200)
                    {
                        shieldAmount = (short)(200* 4.8);
                    }

                }else
                {
                    shieldAmount = (short)(Caster.Level * 2.4);

                    if (Caster.Level > 200)
                    {
                        shieldAmount = (short)(200 * 2.4);
                    }


                }

                var effect = new EffectDice(EffectsEnum.Effect_AddShieldPercent, shieldAmount, shieldAmount, shieldAmount);

                var actorBuffId = Caster.PopNextBuffId();
                var handler = EffectManager.Instance.GetSpellEffectHandler(effect, Caster, this, TargetedCell, Critical);

                var escudo = new StatBuff(actorBuffId, Target, Caster, handler, Spell, shieldAmount, PlayerFields.Shield, false, FightDispellableEnum.DISPELLABLE_BY_DEATH, Target);
                escudo.Duration = 1;


                escudo.Value = shieldAmount;
                escudo.Dice.Value = shieldAmount;
                escudo.Dice.DiceNum = shieldAmount;
                Target.AddBuff(escudo);

                base.Execute();
            }
        }
    }
}