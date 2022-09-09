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
    [BrainIdentifier(3459)]
    public class WabbitSellador : Brain
    {
        public WabbitSellador(AIFighter fighter)
            : base(fighter)
        {
            fighter.Fight.FightStarted += Fight_FightStarted;
            Fighter.GetAlive += OnGetAlive;

        }

        private void Fight_FightStarted(IFight obj)
        {
            //Fighter.Stats[PlayerFields.SummonLimit].Additional = 9999;
            Fighter.CastAutoSpell(new Spell(3788, 21082), Fighter.Cell);
        }

        private void OnGetAlive(FightActor fighter)
        {
            if (fighter != Fighter)
                return;

            if (fighter is SummonedMonster && (fighter as SummonedMonster).Monster.MonsterId == 3459) Fighter.CastAutoSpell(new Spell(3788, 1), Fighter.Cell);
        }


    }
}
