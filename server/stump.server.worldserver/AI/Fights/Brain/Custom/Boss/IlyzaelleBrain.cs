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
    [BrainIdentifier((int)MonsterIdEnum.ILYZAELLE)]
    public class IlyzaelleBrain : Brain
    {
        public IlyzaelleBrain(AIFighter fighter)
            : base(fighter)
        {
            fighter.Fight.FightStarted += Fight_FightStarted;
        }

        private void Fight_FightStarted(IFight obj)
        {
            Fighter.Stats[PlayerFields.SummonLimit].Additional = 9999;
            Fighter.Stats[PlayerFields.CriticalDamageReduction].Additional = 250;
            Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.POSSESSION, 1), Fighter.Cell);
            //Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.POSSESSION_9047, 1), Fighter.Cell);
            //Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.POSSESSION_9049, 1), Fighter.Cell);
        }
    }

    [BrainIdentifier(4972)]
    public class Cranima : Brain
    {

        public Cranima(AIFighter fighter)
            : base(fighter)
        {
            fighter.Fight.FightStarted += Fight_FightStarted;
            fighter.Fight.TurnStarted += OnTurnStarted;
        }

        private void OnTurnStarted(IFight obj, FightActor actor)
        {
            if (Fighter == actor)
            {
                if (!Fighter.HasState((int)SpellIdEnum.FRUSTRATION_9075))
                {
                    Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.FRUSTRATION_9075, 1), Fighter.Cell);

                }

            }
        }

        private void Fight_FightStarted(IFight obj)
        {
            //Fighter.Stats[PlayerFields.SummonLimit].Additional = 9999;
            Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.FRUSTRATION_9075, 1), Fighter.Cell);
            //Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.BACK_TO_BUBBLES_6870, 1), Fighter.Cell);

            //Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.CALL_OF_THE_DEEP_SEA_6808, 1), Fighter.Cell); // Invo
            //Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.CALL_OF_THE_DEEP_SEA_6812, 1), Fighter.Cell); //
            // //

        }



    }

    [BrainIdentifier(4969)]
    [BrainIdentifier(4970)]
    [BrainIdentifier(4971)]
    [BrainIdentifier(4968)]
    public class IllyBichos : Brain
    {
        public IllyBichos(AIFighter fighter)
            : base(fighter)
        {
            fighter.Fight.FightStarted += Fight_FightStarted;
            fighter.Fight.TurnStarted += OnTurnStarted;
        }

        private void OnTurnStarted(IFight obj, FightActor actor)
        {
            if (Fighter == actor)
            {
                if (!Fighter.HasState((int)SpellIdEnum.SOUL_BLAZER_9078))
                {
                    Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.SOUL_BLAZER_9078, 1), Fighter.Cell);

                }

            }
        }


        private void Fight_FightStarted(IFight obj)
        {
            //Fighter.Stats[PlayerFields.SummonLimit].Additional = 9999;
            Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.SOUL_BLAZER_9078, 1), Fighter.Cell);
            //Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.GHOULISH_MARCH_8890, 1), Fighter.Cell);

        }
    }

}