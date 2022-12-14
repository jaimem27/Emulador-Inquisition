using Stump.DofusProtocol.Enums;
using Stump.Server.WorldServer.AI.Fights.Actions;
using Stump.Server.WorldServer.Game.Actors.Fight;
using Stump.Server.WorldServer.Game.Fights;
using Stump.Server.WorldServer.Game.Spells;
using System.Linq;
using TreeSharp;

namespace Stump.Server.WorldServer.AI.Fights.Brain.Custom.Boss
{
    [BrainIdentifier(3400)]
    public class DoctorEggob : Brain
    {
        public DoctorEggob(AIFighter fighter)
           : base(fighter)
        {
            fighter.Fight.FightStarted += Fight_FightStarted;
        }

        private void Fight_FightStarted(IFight obj)
        {
            Fighter.CastAutoSpell(new Spell(4193, 1), Fighter.Cell);
            //Fighter.CastAutoSpell(new Spell(12105, 1), Fighter.Cell);
        }


    }

    [BrainIdentifier(3399)]
    public class HuevoMortal : Brain
    {
        public HuevoMortal(AIFighter fighter)
           : base(fighter)
        {
            fighter.Fight.TurnStarted += OnTurnStarted;
        }

        private void OnTurnStarted(IFight obj, FightActor actor)
        {
            if (Fighter.IsSummoned())
                Fighter.CastAutoSpell(new Spell(3549, 1), Fighter.Cell);
            //Fighter.CastAutoSpell(new Spell(12105, 1), Fighter.Cell);
        }


    }

    [BrainIdentifier(3398)]
    public class CosoInestable : Brain
    {
        public CosoInestable(AIFighter fighter)
           : base(fighter)
        {
            fighter.Fight.TurnStarted += OnTurnStarted;
        }

        private void OnTurnStarted(IFight obj, FightActor actor)
        {
            if (Fighter.IsSummoned())
                Fighter.CastAutoSpell(new Spell(3546, 1), Fighter.Cell);
            //Fighter.CastAutoSpell(new Spell(12105, 1), Fighter.Cell);
        }


    }

}


