using Stump.DofusProtocol.Enums;
using Stump.Server.WorldServer.AI.Fights.Actions;
using Stump.Server.WorldServer.Game.Actors.Fight;
using Stump.Server.WorldServer.Game.Fights;
using Stump.Server.WorldServer.Game.Spells;
using System.Linq;
using TreeSharp;

namespace Stump.Server.WorldServer.AI.Fights.Brain.Custom.Boss
{
    [BrainIdentifier(5077)]
    public class Balimogli : Brain
    {
        public Balimogli(AIFighter fighter)
           : base(fighter)
        {
            fighter.Fight.FightStarted += Fight_FightStarted;
        }

        private void Fight_FightStarted(IFight obj)
        {
            Fighter.CastAutoSpell(new Spell(8988, 1), Fighter.Cell);
            //Fighter.CastAutoSpell(new Spell(12105, 1), Fighter.Cell);
        }


    }

    [BrainIdentifier(5078)]
    public class BalimogliKanu : Brain
    {
        public BalimogliKanu(AIFighter fighter)
           : base(fighter)
        {
            fighter.Fight.TurnStarted += OnTurnStarted;
        }

        private void OnTurnStarted(IFight obj, FightActor actor)
        {
            if (Fighter.IsSummoned())
                Fighter.CastAutoSpell(new Spell(8992, 1), Fighter.Cell);
            //Fighter.CastAutoSpell(new Spell(12105, 1), Fighter.Cell);
        }


    }

    [BrainIdentifier(5079)]
    public class BalimogliOso : Brain
    {
        public BalimogliOso(AIFighter fighter)
           : base(fighter)
        {
            fighter.Fight.TurnStarted += OnTurnStarted;
        }

        private void OnTurnStarted(IFight obj, FightActor actor)
        {
            if (Fighter.IsSummoned())
                Fighter.CastAutoSpell(new Spell(8994, 1), Fighter.Cell);
            //Fighter.CastAutoSpell(new Spell(12105, 1), Fighter.Cell);
        }


    }

    [BrainIdentifier(5080)]
    public class BalimogliLubu : Brain
    {
        public BalimogliLubu(AIFighter fighter)
           : base(fighter)
        {
            fighter.Fight.TurnStarted += OnTurnStarted;
        }

        private void OnTurnStarted(IFight obj, FightActor actor)
        {
            if (Fighter.IsSummoned())
                Fighter.CastAutoSpell(new Spell(8992, 1), Fighter.Cell);
            //Fighter.CastAutoSpell(new Spell(12105, 1), Fighter.Cell);
        }


    }

}

