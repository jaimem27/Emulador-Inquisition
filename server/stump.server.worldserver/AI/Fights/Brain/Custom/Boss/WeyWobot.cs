using Stump.DofusProtocol.Enums;
using Stump.Server.WorldServer.AI.Fights.Actions;
using Stump.Server.WorldServer.Game.Actors.Fight;
using Stump.Server.WorldServer.Game.Effects.Handlers.Spells.Summon;
using Stump.Server.WorldServer.Game.Fights;
using Stump.Server.WorldServer.Game.Spells;
using System.Linq;
using TreeSharp;

namespace Stump.Server.WorldServer.AI.Fights.Brain.Custom.Boss
{
    [BrainIdentifier((int)MonsterIdEnum.WA_WOBOT)]
    public class WeyWabbit : Brain
    {

        public WeyWabbit(AIFighter fighter)
           : base(fighter)
        {

        }

        public override void Play()
        {

            foreach (var spell in Fighter.Spells.Values)
            {

                var target = Environment.GetNearestEnemy();

                var selector = new PrioritySelector();

                if (spell.Id == 3791)
                {
                    Fighter.CastSpell(new Spell(3791, 21085), Fighter.Cell);
                    

                }

                if (spell.Id == 3789)
                {
                    Fighter.CastSpell(new Spell(3789, 21083), target.Cell);

                }

                if(spell.Id == 3824)
                    Fighter.CastSpell(new Spell((int)SpellIdEnum.COOPERATION, 21084), target.Cell);

                //if (spell.Id == 3790)
                //{
                //    if (target.Id == 3458 && target is SummonedMonster)
                //        Fighter.CastSpell(new Spell((int)SpellIdEnum., 21084), target.Cell);

                //}


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