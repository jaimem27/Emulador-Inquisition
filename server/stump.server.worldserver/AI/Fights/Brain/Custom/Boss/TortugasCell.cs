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
    [BrainIdentifier((int)MonsterIdEnum.RAPHAELA)]
    [BrainIdentifier((int)MonsterIdEnum.DONATELLA)]
    [BrainIdentifier((int)MonsterIdEnum.LEONARDAWA)]
    [BrainIdentifier((int)MonsterIdEnum.MICHELANGELA)]
    public class TortugasCell : Brain
    {
        public TortugasCell(AIFighter fighter)
            : base(fighter)
        {
            fighter.Fight.FightStarted += Fight_FightStarted;

        }

        private void Fight_FightStarted(IFight obj)
        {
        }

        public override void Play()
        {
            foreach (var spell in Fighter.Spells.Values)
            {
                var target = Environment.GetNearestEnemy();
                var invo = Environment.GetFreeAdjacentCell();
                var selector = new PrioritySelector();

                selector.AddChild(new Decorator(ctx => target == null, new DecoratorContinue(new RandomMove(Fighter))));
                selector.AddChild(new Decorator(ctx => spell == null, new DecoratorContinue(new FleeAction(Fighter))));

                if (target != null && spell != null)
                {
                    selector.AddChild(new PrioritySelector(
                        new Decorator(ctx => Fighter.CanCastSpell(spell, Fighter.Cell) == SpellCastResult.OK,
                            new Sequence(
                                new SpellCastAction(Fighter, spell, Fighter.Cell, true),
                                new Decorator(new MoveNearTo(Fighter, target)))),
                        new Sequence(
                            new MoveNearTo(Fighter, target),
                            new Decorator(
                                ctx => Fighter.CanCastSpell(spell, Fighter.Cell) == SpellCastResult.OK,
                                new Sequence(
                                    new SpellCastAction(Fighter, spell, Fighter.Cell, true))))));
                }

                foreach (var action in selector.Execute(this))
                {

                }

            }
        }

    }

}