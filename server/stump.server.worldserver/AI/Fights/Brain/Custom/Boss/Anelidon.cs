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
    [BrainIdentifier(4726)]
    public class Anedilon : Brain
    {
        public Anedilon(AIFighter fighter)
            : base(fighter)
        {
            fighter.Fight.FightStarted += Fight_FightStarted;

        }

        private void Fight_FightStarted(IFight obj)
        {
            //Fighter.Stats[PlayerFields.SummonLimit].Additional = 9999;
            Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.SLOW_DIGESTION_7367, 1), Fighter.Cell);
            Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.SLOW_DIGESTION_7368, 1), Fighter.Cell);
            Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.SLOW_DIGESTION_7375, 1), Fighter.Cell);
            Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.SLOW_DIGESTION_7376, 1), Fighter.Cell);
        }
    }

    [BrainIdentifier(4730)]
    public class Mordescualo : Brain
    {
        public Mordescualo(AIFighter fighter)
            : base(fighter)
        {
            fighter.Fight.FightStarted += Fight_FightStarted;

        }


        private void Fight_FightStarted(IFight obj)
        {
            Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.JAWS_OF_THE_SANDS_7845, 1), Fighter.Cell); // Efecto pasivo.
            Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.JAWS_OF_THE_SANDS_7846, 1), Fighter.Cell);

        }
    }

    [BrainIdentifier(4731)]
    public class Onisciclopo : Brain
    {
        public Onisciclopo(AIFighter fighter)
            : base(fighter)
        {
            fighter.Fight.FightStarted += Fight_FightStarted;

        }


        private void Fight_FightStarted(IFight obj)
        {
            Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.LASCCAR_7851, 1), Fighter.Cell); // Efecto pasivo.
            Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.LASCCAR_7852, 1), Fighter.Cell);
            Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.LASCCAR_7853, 1), Fighter.Cell);
            Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.LASCCAR_7854, 1), Fighter.Cell);

        }
    }

}