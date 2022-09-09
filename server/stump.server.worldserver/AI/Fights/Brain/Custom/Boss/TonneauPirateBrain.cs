using Stump.DofusProtocol.Enums;
using Stump.Server.WorldServer.Game.Actors.Fight;
using Stump.Server.WorldServer.Game.Effects.Handlers.Spells.Summon;
using Stump.Server.WorldServer.Game.Fights;
using Stump.Server.WorldServer.Game.Spells;
using System;

namespace Stump.Server.WorldServer.AI.Fights.Brain.Custom.Boss
{
    [BrainIdentifier((int)MonsterIdEnum.TONNEAU_PIRATE)]
    public class TonneauPirateBrain : Brain
    {
        public TonneauPirateBrain(AIFighter fighter)
            : base(fighter)
        {
            fighter.Fight.TurnStarted += OnTurnStarted;
        }

        private void OnTurnStarted(IFight obj, FightActor actor)
        {
            if (actor != Fighter || actor.AP < 4)
                return;

            if(Fighter.IsSummoned())
                Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.TREACHEROUS_RHUM, (short)(Fighter as SummonedMonster).MonsterGrade.GradeId), Fighter.Cell);
            else
                Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.TREACHEROUS_RHUM, (short)(Fighter as MonsterFighter).MonsterGrade.GradeId), Fighter.Cell);
        }
    }

    [BrainIdentifier((int)MonsterIdEnum.NAKUNBRA)]
    public class Notyebra : Brain
    {
        public Notyebra(AIFighter fighter)
            : base(fighter)
        {
            fighter.Fight.FightStarted += Fight_FightStarted;
        }

        private void Fight_FightStarted(IFight obj)
        {
            //Fighter.Stats[PlayerFields.SummonLimit].Additional = 9999;
            Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.PREPARE_TO_BOARD, 1), Fighter.Cell);
        }
    }

}