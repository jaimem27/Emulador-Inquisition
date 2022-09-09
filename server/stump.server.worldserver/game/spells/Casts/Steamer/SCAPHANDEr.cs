using System.Linq;
using Stump.Core.Reflection;
using Stump.DofusProtocol.Enums;
using Stump.DofusProtocol.Enums.Extensions;
using Stump.Server.WorldServer.Game.Actors.Fight;
using Stump.Server.WorldServer.Game.Effects;
using Stump.Server.WorldServer.Game.Effects.Handlers.Spells;
using Stump.Server.WorldServer.Game.Effects.Handlers.Spells.Buffs;
using Stump.Server.WorldServer.Game.Effects.Handlers.Spells.Debuffs;
using Stump.Server.WorldServer.Game.Effects.Handlers.Spells.Move;
using Stump.Server.WorldServer.Game.Effects.Instances;
using Stump.Server.WorldServer.Game.Fights;
using Stump.Server.WorldServer.Game.Fights.Buffs;

namespace Stump.Server.WorldServer.Game.Spells.Casts.Osamodas {
    [SpellCastHandler (SpellIdEnum.SCAPHANDER_3202)]
    public class SCAPHANDER : DefaultSpellCastHandler {
        public SCAPHANDER (SpellCastInformations cast) : base (cast) { }
        private void MPbuff (TriggerBuff buff, FightActor trigerrer, BuffTriggerType trigger, object token)
        {
            try
            {
                EffectDice subap = new EffectDice((short)EffectsEnum.Effect_AddMP_128, buff.EffectHandler.Dice.Value, (short)token, buff.EffectHandler.Dice.DiceFace, new EffectBase());
                SpellEffectHandler spellEffectHandler = Singleton<EffectManager>.Instance.GetSpellEffectHandler(subap, base.Caster, this, base.TargetedCell, Critical);
                spellEffectHandler.Apply();
            }
            catch { }

        }
        public override void Execute () {
            if (!m_initialized)
                Initialize ();
            var targer = Handlers[0].GetAffectedActors ().First ();

            Handlers[0].AddTriggerBuff (targer, BuffTriggerType.OnAPLost, MPbuff);
            foreach (var handler in Handlers)
                if (handler != Handlers[0])
                    handler.Apply ();

        }
    }

    [SpellCastHandler (SpellIdEnum.BOOMBOOM)]
    public class BOOMBOOM : DefaultSpellCastHandler {
        public BOOMBOOM (SpellCastInformations cast) : base (cast) { }
        public override bool Initialize () {
            if (base.Initialize ()) {

                Spell spellToCast = null;
                foreach (var target in Caster.OpposedTeam.Fighters.Where (x => x.HasState ((int) SpellStatesEnum.EMBUSCADE_130))) {
                    if (Caster.HasState ((int) SpellStatesEnum.TELLURIQUE_127))

                    {
                        spellToCast = new Spell ((int) SpellIdEnum.BOOBOOME, Spell.CurrentLevel);
                        Caster.CasttestSpell (spellToCast, target.Cell);

                        return true;

                    } else if (Caster.HasState ((int) SpellStatesEnum.AQUATIQUE_128)) {
                        spellToCast = new Spell ((int) SpellIdEnum.BWOOBWOOM, Spell.CurrentLevel);

                        Caster.CasttestSpell (spellToCast, target.Cell);
                        return true;

                    } else if (Caster.HasState ((int) SpellStatesEnum.ARDENT_129))

                    {
                        spellToCast = new Spell ((int) SpellIdEnum.BOOBOOMF, Spell.CurrentLevel);
                        Caster.CasttestSpell (spellToCast, target.Cell);
                        return true;
                    }
                }
                return true;
            }
            return false;
        }

    }

    [SpellCastHandler (SpellIdEnum.RESCUE_3244)]
    public class RESCUE : DefaultSpellCastHandler {
        public RESCUE (SpellCastInformations cast) : base (cast) { }
        public override bool Initialize () {
            if (base.Initialize ()) {

                var f = Fight.GetOneFighter (Map.GetCell (Handlers[0].AffectedCells[0].Id));
                if (f != null) {

                    //if (f.IsEnnemyWith(Caster)) dice = new EffectDice(EffectsEnum.Effect_AddAP_111, 1, 0, 0);
                    foreach (var state in f.GetBuffs (x => x is StateBuff && ((StateBuff) x).State.Id == (int) 131).ToArray ()) {
                        f.RemoveBuff (state);
                    }
                }

                return true;
            }
            return false;
        }

    }

}