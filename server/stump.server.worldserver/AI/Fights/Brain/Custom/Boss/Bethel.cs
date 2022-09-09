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
    [BrainIdentifier(5110)]
    public class Bethel : Brain
    {

        public Bethel(AIFighter fighter)
            : base(fighter)
        {
            fighter.Fight.FightStarted += Fight_FightStarted;
        }

        private void Fight_FightStarted(IFight obj)
        {

            //Fighter.Stats[PlayerFields.SummonLimit].Additional = 9999;
            //Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.UNSPEAKABLE_CONTRACT_9542, 1), Fighter.Cell); //
            


        }

    }

    [BrainIdentifier(5121)]
    public class Monolito : Brain
    {

        public Monolito(AIFighter fighter)
            : base(fighter)
        {
            fighter.Fight.TurnStarted += OnTurnStarted;
        }

        private void OnTurnStarted(IFight obj, FightActor actor)
        {
            //Fighter.Stats[PlayerFields.SummonLimit].Additional = 9999;
            //Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.HIEROGLYPHS, 1), Fighter.Cell);
            //Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.BACK_TO_BUBBLES_6870, 1), Fighter.Cell);

            //Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.CALL_OF_THE_DEEP_SEA_6808, 1), Fighter.Cell); // Invo
            //Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.CALL_OF_THE_DEEP_SEA_6812, 1), Fighter.Cell); //
            // //

        }
    }

    [BrainIdentifier(5122)]
    public class DragonProfundidades : Brain
    {

        public DragonProfundidades(AIFighter fighter)
            : base(fighter)
        {
            fighter.Fight.TurnStarted += OnTurnStarted;
        }

        private void OnTurnStarted(IFight obj, FightActor actor)
        {
            //Fighter.Stats[PlayerFields.SummonLimit].Additional = 9999;
            //Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.FRIGHTENING_PRESENCE, 1), Fighter.Cell);
            //Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.BACK_TO_BUBBLES_6870, 1), Fighter.Cell);

            //Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.CALL_OF_THE_DEEP_SEA_6808, 1), Fighter.Cell); // Invo
            //Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.CALL_OF_THE_DEEP_SEA_6812, 1), Fighter.Cell); //
            // //

        }
    }

    [BrainIdentifier(5115)]
    [BrainIdentifier(5113)]
    [BrainIdentifier(5112)]
    [BrainIdentifier(5114)]
    [BrainIdentifier(5111)]
    public class BethelBichos : Brain
    {
        public BethelBichos(AIFighter fighter)
            : base(fighter)
        {
            fighter.Fight.FightStarted += Fight_FightStarted;
            fighter.Fight.TurnStarted += OnTurnStarted;
        }

        private void OnTurnStarted(IFight obj, FightActor actor)
        {
            if (Fighter == actor)
            {
                if (!Fighter.HasState((int)SpellIdEnum.AQUAZOMB_9450))
                {
                    //Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.AQUAZOMB_9450, 1), Fighter.Cell);

                }

            }
        }


        private void Fight_FightStarted(IFight obj)
        {
            //Fighter.Stats[PlayerFields.SummonLimit].Additional = 9999;
            //Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.AQUAZOMB_9450, 1), Fighter.Cell);
            //Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.GHOULISH_MARCH_8890, 1), Fighter.Cell);

        }
    }

}