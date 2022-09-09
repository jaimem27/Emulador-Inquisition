using Stump.Core.Extensions;
using Stump.Core.Reflection;
using Stump.DofusProtocol.Enums;
using Stump.Server.WorldServer.AI.Fights.Actions;
using Stump.Server.WorldServer.Game.Actors.Fight;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Monsters;
using Stump.Server.WorldServer.Game.Fights;
using Stump.Server.WorldServer.Game.Maps.Cells;
using Stump.Server.WorldServer.Game.Spells;
using Stump.Server.WorldServer.Game.Spells.Casts;
using System.Linq;
using TreeSharp;
using static Stump.Server.WorldServer.Game.Actors.Fight.FightActor;

namespace Stump.Server.WorldServer.AI.Fights.Brain.Custom.Boss
{
    [BrainIdentifier(4860)]
    public class Judini : Brain
    {
        public Judini(AIFighter fighter)
            : base(fighter)
        {
            fighter.Fight.FightStarted += Fight_FightStarted;
        }

        private void Fight_FightStarted(IFight obj)
        {
            Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.TAKE_MY_PLACE_8630, 1), Fighter.Cell);
            Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.TAKE_MY_PLACE_8631, 1), Fighter.Cell);
        }

        public override void Play()
        {

            foreach (var spell in Fighter.Spells.Values)
            {
                
                var target = Environment.GetNearestEnemy();

                var selector = new PrioritySelector();

                if (spell.Id == 8634)
                {
                    Fighter.CastSpell(new Spell(8634, 1), target.Cell);

                }


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