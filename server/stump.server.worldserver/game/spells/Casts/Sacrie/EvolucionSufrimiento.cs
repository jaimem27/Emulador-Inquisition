//using Stump.DofusProtocol.Enums;
//using Stump.Server.WorldServer.Game.Effects;
//using Stump.Server.WorldServer.Game.Effects.Handlers.Spells.Buffs;
//using Stump.Server.WorldServer.Game.Effects.Instances;
//using Stump.Server.WorldServer.Game.Fights;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Stump.Server.WorldServer.Game.Spells.Casts.Sacrie
//{
//    [SpellCastHandler(SpellIdEnum.SUFFERING_EVOLUTION)]
//    public class EvolucionSufrimiento : DefaultSpellCastHandler
//    {
//        public EvolucionSufrimiento(SpellCastInformations cast)
//          : base(cast)
//        {
//        }

//        public override void Execute()
//        {
//            if (!m_initialized)
//                Initialize();

//            var m_buff = Handlers.OfType<StatsBuff>();
//            var fighter = Caster;
//            var test = this;

//            foreach (var handler in Handlers)
//            {
//                if (m_buff.Contains(handler))
//                    continue;
//                //if (handler.Dice.Value == 9946)
//                //    return;

//                //if (handler.Dice.DiceNum == 9944)
//                //    return;

//                handler.Apply();
//                //var boostedSpell = fighter.GetSpell(spell.Id);

//                //var dice = new EffectDice(spell.ByLevel[6].Effects[buffid].EffectId, spell.ByLevel[6].Effects[buffid].Value, spell.ByLevel[6].Effects[buffid].DiceNum, spell.ByLevel[6].Effects[buffid].DiceFace);

//                //var handlerr = EffectManager.Instance.GetSpellEffectHandler(dice, fighter, SpellManager.Instance.GetSpellCastHandler(fighter, new Spell(spell.Id, 6), fighter.Cell, false), fighter.Cell, false);
//                //fighter.RemoveSpellBuffs(spell.Id);
//            }

//        }
//    }
//}