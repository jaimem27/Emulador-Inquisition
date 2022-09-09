using Stump.DofusProtocol.Enums;
using Stump.Server.WorldServer.Game.Actors.Fight;
using Stump.Server.WorldServer.Game.Fights;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stump.DofusProtocol.Enums;
using Stump.Server.WorldServer.Game.Effects;
using Stump.Server.WorldServer.Game.Effects.Handlers.Spells.Damage;
using Stump.Server.WorldServer.Game.Actors.Fight;
using Stump.Server.WorldServer.Game.Effects.Instances;
using Stump.Server.WorldServer.Game.Effects.Handlers.Spells;
using Stump.Server.WorldServer.Game.Fights;
using Stump.Server.WorldServer.Game.Fights.Buffs;
using Stump.Core.Reflection;


namespace Stump.Server.WorldServer.Game.Spells.Casts.Ocra
{
    [SpellCastHandler (SpellIdEnum.FULMINATING_ARROW_9319)]
    public class FlechaFulminante : DefaultSpellCastHandler
    {
        public FlechaFulminante (SpellCastInformations cast) : base (cast) { }

        public override void Execute ()
        {
            if (!m_initialized)
                Initialize ();
            var oneFighter = Fight.GetOneFighter (TargetedCell);
            if (oneFighter is SummonedMonster && ((oneFighter as SummonedMonster).Monster.Template.Id == (int) MonsterIdEnum.BALISE_DE_RAPPEL ||    (oneFighter as SummonedMonster).Monster.Template.Id == (int) MonsterIdEnum.BALISE_TACTIQUE))
                Handlers[0].SetAffectedActor (oneFighter);
            else
                Handlers[0].SetAffectedActors (new FightActor[] { });

            var Target = Fight.GetOneFighter(TargetedCell);

            var Jugadores = Fight.GetAllFighters().ToList();

            var actors = Handlers[2].GetAffectedActors();
            var fulm = Caster.GetSpell(9319);
            var fulmi = Caster.GetSpell(9344);

            var calculo = 10 * actors.Count();


            /*if (actors.Count() >= 2)
            {
                var buff = new SpellBuff(Caster.PopNextBuffId(), Caster, Caster, Handlers[0], fulm, fulmi, (short)calculo, false, FightDispellableEnum.DISPELLABLE_BY_DEATH);
                buff.Duration = 3;
                Caster.AddBuff(buff);
            }*/

            /*foreach (var target in Jugadores)
            {
                if (Jugadores.Count() >= 2)
                {
                    var num = (Jugadores.Count() * 10) + 38;
                    var face = (Jugadores.Count() * 10) + 48;
                    Handlers[0].Dice.DiceNum = (short)num;
                    Handlers[0].Dice.DiceFace = (short)face;
                    //Fight.EndFight();
                }
                else
                {
                    Handlers[0].Dice.DiceNum = 38;
                    Handlers[0].Dice.DiceFace = 48;
                }
            }*/

            /*var objetivos = Handlers[0].GetAffectedActors();
            var celdas = Handlers[0].AffectedCells.Last();
            var num = (objetivos.Count() * 10) + 38;
            var face = (objetivos.Count() * 10) + 48;

            Handlers[0].Dice.DiceNum = (short)num;
            Handlers[0].Dice.DiceFace = (short)face;*/

            base.Execute ();
        }
    }



    [SpellCastHandler (SpellIdEnum.FULMINATING_ARROW_9344)]
    public class ActiveFlechaFulminante9344 : DefaultSpellCastHandler
    {
        public ActiveFlechaFulminante9344 (SpellCastInformations cast) : base (cast) { }

        public override void Execute ()
        {
            if (!m_initialized)
                Initialize ();
            var handlers = Handlers;

            var fulm = Caster.GetSpell(9344);
            var actors = Handlers[1].GetAffectedActors();

            var calcu = actors.Count() * 10;

            if (actors.Count() >= 1)
            {
                var buffo = new SpellBuff(Caster.PopNextBuffId(), Caster, Caster, Handlers[0], Spell, Spell, (short)calcu, false, FightDispellableEnum.DISPELLABLE_BY_DEATH);
                Caster.AddBuff(buffo);
            }

            base.Execute ();
        }
    }

    [SpellCastHandler (SpellIdEnum.FULMINATING_ARROW)]
    public class ActiveFlechaFulminante10117 : DefaultSpellCastHandler {
        public ActiveFlechaFulminante10117 (SpellCastInformations cast) : base (cast) { }

        public override void Execute ()
        {
            if (!m_initialized)
                Initialize ();
            var handlers = Handlers;
            base.Execute ();
        }
    }
}