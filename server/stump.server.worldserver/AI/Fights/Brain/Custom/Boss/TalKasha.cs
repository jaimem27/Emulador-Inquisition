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
    [BrainIdentifier(4744)]
    public class TalKasha : Brain
    {
        int turnos = 1;
        public TalKasha(AIFighter fighter)
            : base(fighter)
        {
            fighter.Fight.FightStarted += Fight_FightStarted;
            fighter.Fight.TurnStarted += OnTurnStarted;
        }

        private void Fight_FightStarted(IFight obj)
        {

            //Fighter.Stats[PlayerFields.SummonLimit].Additional = 9999;
            Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.CURSE_OF_THE_PYRAMID, 1), Fighter.Cell); // Glifo
            Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.CURSE_OF_THE_PYRAMID_7868, 1), Fighter.Cell); // Invu
            Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.CURSE_OF_THE_PYRAMID_7869, 1), Fighter.Cell); // ?? Resucita
            Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.CURSE_OF_THE_PYRAMID_8082, 1), Fighter.Cell); //
            Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.CURSE_OF_THE_PYRAMID_8139, 1), Fighter.Cell); //



        }

        private void OnTurnStarted(IFight obj, FightActor actor)
        {
            if (Fighter == actor)
            {
                if (turnos == 1 || turnos % 2 == 0)
                {
                    Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.CURSE_OF_THE_PYRAMID, 1), Fighter.Cell);
                    Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.CURSE_OF_THE_PYRAMID_7869, 1), Fighter.Cell); // ?? Resucita
                    Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.CURSE_OF_THE_PYRAMID_8082, 1), Fighter.Cell); //
                    Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.CURSE_OF_THE_PYRAMID_8139, 1), Fighter.Cell); //

                }

            }
            turnos++;
        }

    }

    [BrainIdentifier(4739)]
    public class Chacanubis : Brain
    {

        public Chacanubis(AIFighter fighter)
            : base(fighter)
        {
            fighter.Fight.FightStarted += Fight_FightStarted;
            fighter.Fight.TurnStarted += OnTurnStarted;
        }

        private void Fight_FightStarted(IFight obj)
        {
            //Fighter.Stats[PlayerFields.SummonLimit].Additional = 9999;
            Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.ARCHAIC_POWER, 1), Fighter.Cell);
            Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.ARCHAIC_POWER_7826, 1), Fighter.Cell);

            //Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.CALL_OF_THE_DEEP_SEA_6808, 1), Fighter.Cell); // Invo
            //Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.CALL_OF_THE_DEEP_SEA_6812, 1), Fighter.Cell); //
            // //

        }

        private void OnTurnStarted(IFight obj, FightActor actor)
        {
            if (Fighter == actor)
            {
                if (!Fighter.HasState((int)SpellIdEnum.ARCHAIC_POWER_7826))
                {
                    Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.ARCHAIC_POWER_7826, 1), Fighter.Cell);

                }

            }
        }

    }

    [BrainIdentifier(4743)]
    public class Malignotep : Brain
    {
        public Malignotep(AIFighter fighter)
            : base(fighter)
        {
            fighter.Fight.FightStarted += Fight_FightStarted;
            fighter.Fight.TurnStarted += OnTurnStarted;
        }

        private void OnTurnStarted(IFight obj, FightActor actor)
        {
            if (Fighter == actor)
            {
                if (!Fighter.HasState((int)SpellIdEnum.CURSIFICATION_8049))
                {
                    Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.CURSIFICATION_8049, 1), Fighter.Cell);

                }

            }
        }


        private void Fight_FightStarted(IFight obj)
        {
            //Fighter.Stats[PlayerFields.SummonLimit].Additional = 9999;
            Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.CURSIFICATION_8049, 1), Fighter.Cell);
            //Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.MADNESS_6724, 1), Fighter.Cell);

        }
    }

    [BrainIdentifier(4741)]
    public class Momistico : Brain
    {

        public Momistico(AIFighter fighter)
            : base(fighter)
        {
            fighter.Fight.FightStarted += Fight_FightStarted;
            fighter.Fight.TurnStarted += OnTurnStarted;
        }

        private void OnTurnStarted(IFight obj, FightActor actor)
        {
            if (Fighter == actor)
            {
                if (!Fighter.HasState((int)SpellIdEnum.HUPPERMAGE_HERITAGE_7833))
                {
                    Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.HUPPERMAGE_HERITAGE_7833, 1), Fighter.Cell);

                }

            }
        }

        private void Fight_FightStarted(IFight obj)
        {
            //Fighter.Stats[PlayerFields.SummonLimit].Additional = 9999;
            Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.HUPPERMAGE_HERITAGE_7833, 1), Fighter.Cell);

            //Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.CALL_OF_THE_DEEP_SEA_6808, 1), Fighter.Cell); // Invo
            //Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.CALL_OF_THE_DEEP_SEA_6812, 1), Fighter.Cell); //
            // //

        }

    }

    [BrainIdentifier(4742)]
    public class Ryakon : Brain
    {

        public Ryakon(AIFighter fighter)
            : base(fighter)
        {
            fighter.Fight.FightStarted += Fight_FightStarted;
            fighter.Fight.TurnStarted += OnTurnStarted;
        }

        private void OnTurnStarted(IFight obj, FightActor actor)
        {
            if (Fighter == actor)
            {
                if (!Fighter.HasState((int)SpellIdEnum.CURSED_RHYTHM))
                {
                    Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.CURSED_RHYTHM, 1), Fighter.Cell);

                }

            }
        }

        private void Fight_FightStarted(IFight obj)
        {
            //Fighter.Stats[PlayerFields.SummonLimit].Additional = 9999;
            Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.CURSED_RHYTHM, 1), Fighter.Cell);

            //Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.CALL_OF_THE_DEEP_SEA_6808, 1), Fighter.Cell); // Invo
            //Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.CALL_OF_THE_DEEP_SEA_6812, 1), Fighter.Cell); //
            // //

        }

    }

    [BrainIdentifier(4740)]
    public class Venduso : Brain
    {

        public Venduso(AIFighter fighter)
            : base(fighter)
        {
            fighter.Fight.FightStarted += Fight_FightStarted;
            fighter.Fight.TurnStarted += OnTurnStarted;
        }

        private void OnTurnStarted(IFight obj, FightActor actor)
        {
            if (Fighter == actor)
            {
                if (!Fighter.HasState((int)SpellIdEnum.EROSIVE_SNOT_7829))
                {
                    Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.EROSIVE_SNOT_7829, 1), Fighter.Cell);

                }

            }
        }

        private void Fight_FightStarted(IFight obj)
        {
            //Fighter.Stats[PlayerFields.SummonLimit].Additional = 9999;
            Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.EROSIVE_SNOT_7829, 1), Fighter.Cell);

            //Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.CALL_OF_THE_DEEP_SEA_6808, 1), Fighter.Cell); // Invo
            //Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.CALL_OF_THE_DEEP_SEA_6812, 1), Fighter.Cell); //
            // //

        }

    }

}