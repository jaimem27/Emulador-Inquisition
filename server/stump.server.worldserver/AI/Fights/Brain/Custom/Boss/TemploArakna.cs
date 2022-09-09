using Stump.Core.Extensions;
using Stump.DofusProtocol.Enums;
using Stump.Server.WorldServer.AI.Fights.Actions;
using Stump.Server.WorldServer.Game.Actors.Fight;
using Stump.Server.WorldServer.Game.Fights;
using Stump.Server.WorldServer.Game.Spells;
using Stump.Server.WorldServer.Game.Spells.Casts;
using System.Linq;
using TreeSharp;
using static Stump.Server.WorldServer.Game.Actors.Fight.FightActor;

namespace Stump.Server.WorldServer.AI.Fights.Brain.Custom.Boss
{
    [BrainIdentifier(1195)]
    public class Qutan : Brain
    {
        public Qutan(AIFighter fighter)
            : base(fighter)
        {
            fighter.Fight.FightStarted += Fight_FightStarted;

        }

        private void Fight_FightStarted(IFight obj)
        {
            //Fighter.Stats[PlayerFields.SummonLimit].Additional = 9999;
            Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.QU_TAN_S_CURSE_1178, 1), Fighter.Cell);
            Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.QU_TAN_S_CURSE_3940, 1), Fighter.Cell);
        }

        public override void Play()
        {

            foreach (var spell in Fighter.Spells.Values)
            {
                var invo = Environment.GetFreeAdjacentCell();
                var target = Environment.GetNearestEnemy();

                if (spell.Id == 1099)
                {
                    Fighter.CastSpell(new Spell((int)SpellIdEnum.FOURTH_KRAKEN, 1), Fighter.Cell);
                    if (Fighter.HasState((int)SpellStatesEnum.ENCRE_TERTIAIRE_33))
                        Fighter.Summoner.CastAutoSpell(new Spell((int)SpellIdEnum.PEAT_VULNERABILITY, 1), Fighter.Cell);
                }

                if (spell.Id == 1102)
                    Fighter.CastSpell(new Spell((int)SpellIdEnum.TENTACULAR_PARALYSIS, 1), Fighter.Cell);


                var selector = new PrioritySelector();

                selector.AddChild(new Decorator(ctx => target == null, new DecoratorContinue(new RandomMove(Fighter))));
                selector.AddChild(new Decorator(ctx => spell == null, new DecoratorContinue(new FleeAction(Fighter))));

                if (target != null && spell != null)
                {
                    selector.AddChild(new PrioritySelector(
                        new Decorator(ctx => Fighter.CanCastSpell(spell, target.Cell) == SpellCastResult.OK,
                            new Sequence(
                                new SpellCastAction(Fighter, spell, target.Cell, true),
                                new Decorator(new MoveNearTo(Fighter, target)))),
                        new Sequence(
                            new MoveNearTo(Fighter, target),
                            new Decorator(
                                ctx => Fighter.CanCastSpell(spell, target.Cell) == SpellCastResult.OK,
                                new Sequence(
                                    new SpellCastAction(Fighter, spell, target.Cell, true))))));
                }

                foreach (var action in selector.Execute(this))
                {

                }

            }

        }


    }

    [BrainIdentifier(3513)]
    public class Impit : Brain
    {
        public Impit(AIFighter fighter)
            : base(fighter)
        {
            fighter.Fight.FightStarted += Fight_FightStarted;

        }

        private void Fight_FightStarted(IFight obj)
        {
            //Fighter.Stats[PlayerFields.SummonLimit].Additional = 9999;
            Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.HADDOCK, 1), Fighter.Cell);
            
        }
    }

    [BrainIdentifier(3515)]
    public class Juss : Brain
    {
        public Juss(AIFighter fighter)
            : base(fighter)
        {
            fighter.Fight.FightStarted += Fight_FightStarted;

        }

        private void Fight_FightStarted(IFight obj)
        {
            //Fighter.Stats[PlayerFields.SummonLimit].Additional = 9999;
            Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.INTYM_3944, 1), Fighter.Cell);
            Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.INTYM_3945, 1), Fighter.Cell);
        }
    }

    [BrainIdentifier(3516)]
    public class Bidoo : Brain
    {
        public Bidoo(AIFighter fighter)
            : base(fighter)
        {
            fighter.Fight.FightStarted += Fight_FightStarted;
            fighter.Fight.TurnStarted += OnTurnStarted;
        }

        private void OnTurnStarted(IFight obj, FightActor actor)
        {
            if (Fighter == actor)
            {
                if (!Fighter.HasState((int)SpellIdEnum.BODYBUILDING))
                {
                    Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.BODYBUILDING, 1), Fighter.Cell);

                }
            }
        }

        private void Fight_FightStarted(IFight obj)
        {
            //Fighter.Stats[PlayerFields.SummonLimit].Additional = 9999;
            Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.BODYBUILDING, 1), Fighter.Cell);
            
        }
    }

}