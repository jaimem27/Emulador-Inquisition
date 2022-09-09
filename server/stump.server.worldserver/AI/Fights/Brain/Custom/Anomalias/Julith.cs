using Stump.DofusProtocol.Enums;
using Stump.Server.WorldServer.AI.Fights.Actions;
using Stump.Server.WorldServer.Game.Actors.Fight;
using Stump.Server.WorldServer.Game.Fights;
using Stump.Server.WorldServer.Game.Spells;
using System.Linq;
using TreeSharp;

namespace Stump.Server.WorldServer.AI.Fights.Brain.Custom.Boss
{
    [BrainIdentifier(5663)]
    public class Julith : Brain
    {
        public Julith(AIFighter fighter)
           : base(fighter)
        {
            fighter.Fight.FightStarted += Fight_FightStarted;
            fighter.Fight.TurnStarted += OnTurnStarted;
        }

        int turnos = 0;

        private void Fight_FightStarted(IFight obj)
        {
            Fighter.CastAutoSpell(new Spell(12356, 1), Fighter.Cell);
            Fighter.CastAutoSpell(new Spell(11966, 1), Fighter.Cell);
        }

        private void OnTurnStarted(IFight obj, FightActor actor)
        {

          
            if (turnos > 10)
            {
                actor.Stats[PlayerFields.AirResistPercent].Base = 0;
                actor.Stats[PlayerFields.WaterResistPercent].Base = 0;
                actor.Stats[PlayerFields.FireResistPercent].Base = 0;
                actor.Stats[PlayerFields.EarthResistPercent].Base = 0;
                actor.Stats[PlayerFields.NeutralResistPercent].Base = 0;
            }
            turnos++;

        }

        public override void Play()
        {
            foreach (var spell in Fighter.Spells.Values)
            {
                var target = Environment.GetNearestEnemy();

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
