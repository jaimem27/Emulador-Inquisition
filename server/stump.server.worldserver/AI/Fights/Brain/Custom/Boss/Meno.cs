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
    [BrainIdentifier(4460)]
    public class Meno : Brain
    {
        int turnos = 1;
        public Meno(AIFighter fighter)
            : base(fighter)
        {
            fighter.Fight.FightStarted += Fight_FightStarted;
            fighter.Fight.TurnStarted += OnTurnStarted;

        }

        private void Fight_FightStarted(IFight obj)
        {
            Fighter.Stats[PlayerFields.SummonLimit].Additional = 9999;
            Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.CALL_OF_THE_DEEP_SEA, 1), Fighter.Cell); //Invul a los bichos
            Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.CALL_OF_THE_DEEP_SEA_6793, 1), Fighter.Cell); // Buffo Glifo
            Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.CALL_OF_THE_DEEP_SEA_6795, 1), Fighter.Cell); // 100% daños incurable
            
            //Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.CALL_OF_THE_DEEP_SEA_6808, 1), Fighter.Cell); // Invo
            //Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.CALL_OF_THE_DEEP_SEA_6812, 1), Fighter.Cell); //
            // //

        }

        private void OnTurnStarted(IFight obj, FightActor actor)
        {
            if(Fighter == actor && (turnos % 3 == 0 || turnos == 1)){
                Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.CALL_OF_THE_DEEP_SEA_6808, 1), Fighter.Cell);
                //Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.CALL_OF_THE_DEEP_SEA_6812, 1), Fighter.Cell);
                //Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.CALL_OF_THE_DEEP_SEA_6813, 1), Fighter.Cell);
            }
            turnos++;
        }

    }

    [BrainIdentifier(4456)]
    [BrainIdentifier(4457)]
    [BrainIdentifier(4459)]
    [BrainIdentifier(4458)]
    [BrainIdentifier(4455)]
    public class MenoBichos : Brain
    {
        public MenoBichos(AIFighter fighter)
            : base(fighter)
        {
            fighter.Fight.FightStarted += Fight_FightStarted;
            fighter.Fight.TurnStarted += OnTurnStarted;
        }

        private void OnTurnStarted(IFight obj, FightActor actor)
        {
            if (Fighter == actor)
            {
                if (!Fighter.HasState((int)SpellIdEnum.UNSTABLE_MUTATION_6758))
                {
                    Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.UNSTABLE_MUTATION_6758, 1), Fighter.Cell);

                }

            }
        }


        private void Fight_FightStarted(IFight obj)
        {
            //Fighter.Stats[PlayerFields.SummonLimit].Additional = 9999;
            Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.UNSTABLE_MUTATION_6758, 1), Fighter.Cell);
            //Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.UNSTABLE_MUTATION_6759, 1), Fighter.Cell);

        }
    }


}