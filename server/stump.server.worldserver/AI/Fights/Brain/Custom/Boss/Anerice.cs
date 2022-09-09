using Stump.Core.Extensions;
using Stump.DofusProtocol.Enums;
using Stump.Server.WorldServer.AI.Fights.Actions;
using Stump.Server.WorldServer.Game.Actors.Fight;
using Stump.Server.WorldServer.Game.Fights;
using Stump.Server.WorldServer.Game.Spells;
using Stump.Server.WorldServer.Game.Spells.Casts;
using System;
using System.Linq;
using TreeSharp;
using static Stump.Server.WorldServer.Game.Actors.Fight.FightActor;

namespace Stump.Server.WorldServer.AI.Fights.Brain.Custom.Boss
{
    [BrainIdentifier(4882)]
    public class Anerice : Brain
    {

        public Anerice(AIFighter fighter)
            : base(fighter)
        {
            fighter.Fight.FightStarted += Fight_FightStarted;
        }

        private void Fight_FightStarted(IFight obj)
        {

            //Fighter.Stats[PlayerFields.SummonLimit].Additional = 9999;
            Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.VAMPIRIC_RESILIENCE, 1), Fighter.Cell); //
            //Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.EVIL_PACT_6709, 1), Fighter.Cell); //
            //Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.EVIL_PACT_6711, 1), Fighter.Cell);


        }

    }

    [BrainIdentifier(4877)]
    public class Ghuloso : Brain
    {

        public Ghuloso(AIFighter fighter)
            : base(fighter)
        {
            fighter.Fight.FightStarted += Fight_FightStarted;
        }

        private void Fight_FightStarted(IFight obj)
        {
            //Fighter.Stats[PlayerFields.SummonLimit].Additional = 9999;
            Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.GOULALA, 1), Fighter.Cell);
            //Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.BACK_TO_BUBBLES_6870, 1), Fighter.Cell);

            //Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.CALL_OF_THE_DEEP_SEA_6808, 1), Fighter.Cell); // Invo
            //Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.CALL_OF_THE_DEEP_SEA_6812, 1), Fighter.Cell); //
            // //

        }



    }

    [BrainIdentifier(4879)]
    [BrainIdentifier(4880)]
    [BrainIdentifier(4878)]
    [BrainIdentifier(4881)]
    public class GoulsBichos : Brain
    {
        public GoulsBichos(AIFighter fighter)
            : base(fighter)
        {
            fighter.Fight.FightStarted += Fight_FightStarted;
            fighter.Fight.TurnStarted += OnTurnStarted;
        }

        private void OnTurnStarted(IFight obj, FightActor actor)
        {
            if (Fighter == actor)
            {
                if (!Fighter.HasState((int)SpellIdEnum.GHOULISH_MARCH_8726))
                {
                    Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.GHOULISH_MARCH_8890, 1), Fighter.Cell);

                }

            }
        }


        private void Fight_FightStarted(IFight obj)
        {
            //Fighter.Stats[PlayerFields.SummonLimit].Additional = 9999;
            Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.GHOULISH_MARCH_8726, 1), Fighter.Cell);
            //Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.GHOULISH_MARCH_8890, 1), Fighter.Cell);

        }
    }

}