using Stump.DofusProtocol.Enums;
using Stump.Server.WorldServer.AI.Fights.Actions;
using Stump.Server.WorldServer.Game.Actors.Fight;
using Stump.Server.WorldServer.Game.Fights;
using Stump.Server.WorldServer.Game.Spells;
using System.Linq;
using TreeSharp;

namespace Stump.Server.WorldServer.AI.Fights.Brain.Custom.Boss
{
    [BrainIdentifier((int)MonsterIdEnum.RAT_NOIR)]
    public class RataNegra : Brain
    {

        public RataNegra(AIFighter fighter)
           : base(fighter)
        {
            fighter.Fight.TurnStarted += OnTurnStarted;
        }

        private void OnTurnStarted(IFight obj, FightActor actor)
        {
            var target = Environment.GetNearestEnemy();
            var selector = new PrioritySelector();

            selector.AddChild(new MoveNearTo(Fighter, target));

            foreach (var action in selector.Execute(this))
                {

                }
                //Fighter.MoveNearTo(Fighter, path.Cell);
        }

        //public override void Play()
        //{

        //    foreach (var spell in Fighter.Spells.Values)
        //    {
        //        var target = Environment.GetNearestEnemy();
        //        var selector = new PrioritySelector();



        //        selector.AddChild(new Decorator(ctx => target == null, new DecoratorContinue(new RandomMove(Fighter))));
        //        selector.AddChild(new Decorator(ctx => spell == null, new DecoratorContinue(new FleeAction(Fighter))));

        //        if (target != null && spell != null)
        //        {
        //            selector.AddChild(new PrioritySelector(
        //                new Decorator(ctx => Fighter.CanCastSpell(spell, target.Cell) == SpellCastResult.OK,
        //                    new Sequence(
        //                        new SpellCastAction(Fighter, spell, target.Cell, true),
        //                        new Decorator(new MoveNearTo(Fighter, target)))),
        //                new Sequence(
        //                    new MoveNearTo(Fighter, target),
        //                    new Decorator(
        //                        ctx => Fighter.CanCastSpell(spell, target.Cell) == SpellCastResult.OK,
        //                        new Sequence(
        //                            new SpellCastAction(Fighter, spell, target.Cell, true))))));
        //        }

        //        foreach (var action in selector.Execute(this))
        //        {

        //        }

        //    }

        //}
    }

}