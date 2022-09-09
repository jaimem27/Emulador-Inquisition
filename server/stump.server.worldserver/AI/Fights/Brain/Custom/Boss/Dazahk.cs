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

    [BrainIdentifier(5319)]
    public class Dazahk : Brain
    {
        public Dazahk(AIFighter fighter)
            : base(fighter)
        {
            fighter.Fight.FightStarted += Fight_FightStarted;

        }

        private void Fight_FightStarted(IFight obj)
        {
            //Fighter.Stats[PlayerFields.SummonLimit].Additional = 9999;
            //Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.STUBBYGERNESS_10332, 1), Fighter.Cell); //Estado 1
            //Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.STUBBYGERNESS_10336, 1), Fighter.Cell);
            Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.STUBBYGERNESS_10346, 1), Fighter.Cell);
            //Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.STUBBYGERNESS_10347, 1), Fighter.Cell);
        }
    }

    [BrainIdentifier(5316)]
    [BrainIdentifier(5317)]
    [BrainIdentifier(5318)]
    [BrainIdentifier(5315)]
    [BrainIdentifier(5314)]
    public class BichosDazahk : Brain
    {
        public BichosDazahk(AIFighter fighter)
            : base(fighter)
        {
            fighter.Fight.FightStarted += Fight_FightStarted;

        }

        private void Fight_FightStarted(IFight obj)
        {
            //Fighter.Stats[PlayerFields.SummonLimit].Additional = 9999;
            //Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.STUBBYGERNESS_10332, 1), Fighter.Cell);
            //Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.STUBBYGERNESS_10336, 1), Fighter.Cell);
            Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.STUBBYGERNESS_10346, 1), Fighter.Cell);
            //Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.STUBBYGERNESS_10347, 1), Fighter.Cell);
        }
    }

}