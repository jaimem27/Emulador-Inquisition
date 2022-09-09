using Stump.Core.Extensions;
using Stump.DofusProtocol.Enums;
using Stump.Server.WorldServer.Game.Actors.Fight;
using Stump.Server.WorldServer.Game.Fights;
using Stump.Server.WorldServer.Game.Spells;
using Stump.Server.WorldServer.Game.Spells.Casts;
using System.Linq;
using static Stump.Server.WorldServer.Game.Actors.Fight.FightActor;

namespace Stump.Server.WorldServer.AI.Fights.Brain.Custom.Boss
{
    [BrainIdentifier(5664)]
    public class Crunchidor : Brain
    {
        public Crunchidor(AIFighter fighter)
            : base(fighter)
        {
            fighter.Fight.FightStarted += Fight_FightStarted;
            
        }

        private void Fight_FightStarted(IFight obj)
        {
            //Fighter.Stats[PlayerFields.SummonLimit].Additional = 9999;
            //Fighter.CastAutoSpell(new Spell(12290, 1), Fighter.Cell);
        }
    }

    [BrainIdentifier(5665)]
    public class CrunchidorInvo : Brain
    {
        public CrunchidorInvo(AIFighter fighter)
            : base(fighter)
        {
            fighter.GetAlive += OnGetAlive;
        }

        private void OnGetAlive(FightActor fighter)
        {
            Fighter.CastAutoSpell(new Spell(12297, 1), Fighter.Cell);
        }
    }

}