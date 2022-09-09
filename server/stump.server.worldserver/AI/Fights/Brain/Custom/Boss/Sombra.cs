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
    [BrainIdentifier(3446)]
    public class Sombra : Brain
    {
        public Sombra(AIFighter fighter)
            : base(fighter)
        {
            fighter.Fight.FightStarted += Fight_FightStarted;

        }

        private void Fight_FightStarted(IFight obj)
        {
            //Fighter.Stats[PlayerFields.SummonLimit].Additional = 9999;
            //Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.EX_NIHILO, 1), Fighter.Cell);
        }
    }

    [BrainIdentifier(3566)]
    public class Murcielago : Brain
    {
        public Murcielago(AIFighter fighter)
            : base(fighter)
        {
            fighter.Fight.FightStarted += Fight_FightStarted;

        }

        private void Fight_FightStarted(IFight obj)
        {
            //Fighter.Stats[PlayerFields.SummonLimit].Additional = 9999;
            Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.GOOD_VIBRATIONS_4118, 1), Fighter.Cell);
            Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.GOOD_VIBRATIONS_4143, 1), Fighter.Cell);
        }
    }

}