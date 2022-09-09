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
    [BrainIdentifier(2967)]
    public class Tengu : Brain
    {
        public Tengu(AIFighter fighter)
            : base(fighter)
        {
            fighter.Fight.FightStarted += Fight_FightStarted;
            fighter.Fight.TurnStarted += OnTurnStarted;
        }

        private void OnTurnStarted(IFight obj, FightActor actor)
        {

                if (Fighter.HasState((int)SpellIdEnum.FURIBUND) && Fighter.HasState((int)SpellIdEnum.LIGHTNING))
                {
                    Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.FROZEN_FLEECE, 1), Fighter.Cell);
                    //Fighter.RemoveSpellBuffs((int)SpellIdEnum.CROQUETTE);
                    //actor.CastAutoSpell(new Spell((int)SpellIdEnum.FULGURATION,1), actor.Cell);
                    //Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.LIGHTNING_FIST_5367, 1), Fighter.Cell);
                }

            
        }

        private void Fight_FightStarted(IFight obj)
        {
            Fighter.Stats[PlayerFields.SummonLimit].Additional = 9999;
            //Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.CROQUETTE, 1), Fighter.Cell);
        }
    }

    [BrainIdentifier(3234)]
    public class Fuji : Brain
    {
        public Fuji(AIFighter fighter)
            : base(fighter)
        {
            fighter.Fight.FightStarted += Fight_FightStarted;

        }

        private void Fight_FightStarted(IFight obj)
        {
            //Fighter.Stats[PlayerFields.SummonLimit].Additional = 9999;
            Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.MATERNAL_INSTINCT, 1), Fighter.Cell);
        }
    }

    [BrainIdentifier(2891)]
    public class Yomi : Brain
    {
        public Yomi(AIFighter fighter)
            : base(fighter)
        {
            fighter.Fight.FightStarted += Fight_FightStarted;
            fighter.Fight.TurnStarted += OnTurnStarted;
            fighter.Dead += OnGetDead;

        }

        private void OnTurnStarted(IFight obj, FightActor actor)
        {
            if (Fighter == actor)
            {
                if (!Fighter.HasState((int)SpellIdEnum.LIGHTNING))
                {
                    Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.LIGHTNING, 1), Fighter.Cell);
                    //Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.FULGURATION, 1), Fighter.Cell);
                    //Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.LIGHTNING_FIST_5367, 1), Fighter.Cell);
                }

            }
        }

        private void OnGetDead(FightActor fighter, FightActor fighter2)
        {
            if (Fighter == fighter)
            {
                if (fighter.IsSummoned())
                {
                    Fighter.Summoner.CastAutoSpell(new Spell((int)SpellIdEnum.FULGURATION, 1), Fighter.Cell);
                }
            }

        }

        private void Fight_FightStarted(IFight obj)
        {
            //Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.FULGURATION, 1), Fighter.Cell);
            //Fighter.Stats[PlayerFields.SummonLimit].Additional = 9999;
            Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.LIGHTNING, 1), Fighter.Cell);
            //Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.LIGHTNING_FIST_5367, 1), Fighter.Cell);
        }
    }

    [BrainIdentifier(2888)]
    public class Yokai : Brain
    {
        public Yokai(AIFighter fighter)
            : base(fighter)
        {
            fighter.Fight.FightStarted += Fight_FightStarted;
            fighter.Fight.TurnStarted += OnTurnStarted;
            
        }

        private void OnTurnStarted(IFight obj, FightActor actor)
        {
            if (Fighter == actor)
            {
                if (!Fighter.HasState((int)SpellIdEnum.FURIBUND))
                {
                    Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.FURIBUND, 1), Fighter.Cell);
                }

            }
        }

        

        private void Fight_FightStarted(IFight obj)
        {
            //Fighter.Stats[PlayerFields.SummonLimit].Additional = 9999;
            Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.FURIBUND, 1), Fighter.Cell);
        }
    }

}