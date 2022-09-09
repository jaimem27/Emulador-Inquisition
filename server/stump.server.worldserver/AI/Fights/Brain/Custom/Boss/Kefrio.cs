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
    [BrainIdentifier(2942)]
    public class Kefrio : Brain
    {
        public Kefrio(AIFighter fighter)
            : base(fighter)
        {
            fighter.Fight.FightStarted += Fight_FightStarted;
            fighter.Fight.TurnStarted += OnTurnStarted;
        }

        private void OnTurnStarted(IFight obj, FightActor actor)
        {
        }

        private void Fight_FightStarted(IFight obj)
        {
            Fighter.Stats[PlayerFields.SummonLimit].Additional = 9999;
            //Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.ROCK_HARD, 1), Fighter.Cell);
        }

        public override void Play()
        {

            foreach (var spell in Fighter.Spells.Values)
            {
                var invo = Environment.GetFreeAdjacentCell();
                var target = Environment.GetNearestEnemy();
                

                if (spell.Id == 2403)
                {
                    Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.PINGWINNERISYOU, 1), Environment.CellInformationProvider.Map.GetCell(234));
                                  
                }
                

                if (spell.Id == 2400)
                    Fighter.CastSpell(new Spell((int)SpellIdEnum.ETERNAL_WINTER, 1), target.Cell);

                if (spell.Id == 2402)
                    Fighter.CastSpell(new Spell((int)SpellIdEnum.ICEFIELD, 1), target.Cell);


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

    [BrainIdentifier(2941)]
    public class PalaBicho : Brain
    {
        public PalaBicho(AIFighter fighter)
            : base(fighter)
        {
            fighter.Dead += OnDead;

        }

        private void OnDead(FightActor actor, FightActor actor1)
        {

            if (actor.IsFriendlyWith(Fighter))
                Fighter.CastSpell(new Spell((int)SpellIdEnum.HOARFROST, 1), Fighter.Cell);


        }

    }


        [BrainIdentifier(2941)]
    public class InvoEsmigol : Brain
    {
        public InvoEsmigol(AIFighter fighter)
            : base(fighter)
        {
            //fighter.Fight.TurnStarted += OnTurnStarted;

        }


        public override void Play()
        {

            foreach (var spell in Fighter.Spells.Values)
            {
                var invo = Environment.GetFreeAdjacentCell();
                var target = Environment.GetNearestEnemy();

                if (spell.Id == 2409)
                    Fighter.CastSpell(new Spell((int)SpellIdEnum.GOBSTRUCTION, 1), Fighter.Cell);

                if (spell.Id == 2410)
                    Fighter.CastSpell(new Spell((int)SpellIdEnum.GOBTIMISATION, 1), Fighter.Cell);


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

}