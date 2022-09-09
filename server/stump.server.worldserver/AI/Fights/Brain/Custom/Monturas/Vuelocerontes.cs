using Stump.DofusProtocol.Enums;
using Stump.Server.WorldServer.AI.Fights.Actions;
using Stump.Server.WorldServer.Game.Actors.Fight;
using Stump.Server.WorldServer.Game.Fights;
using Stump.Server.WorldServer.Game.Spells;
using System.Linq;
using TreeSharp;

namespace Stump.Server.WorldServer.AI.Fights.Brain.Custom.Boss
{
    [BrainIdentifier(5308)]
    public class VuelocerontesOrquideo : Brain
    {
        private int turnos = 0;
        public VuelocerontesOrquideo(AIFighter fighter)
           : base(fighter)
        {
            fighter.Fight.FightStarted += Fight_FightStarted;
            fighter.Fight.TurnStarted += OnTurnStarted;
        }

        private void OnTurnStarted(IFight obj, FightActor actor)
        {
            turnos++;
            if (actor.HasState((int)SpellStatesEnum.APPRIVOISEMENT_10) || actor.HasState((int)SpellStatesEnum.APPRIVOISEMENT_CHEAT_427) || actor.HasState((int)SpellStatesEnum.APPRIVOISEMENT_10))
            {
                Fighter.RemoveAndDispellAllBuffs();
                Fighter.RemoveSpellBuffs(10321);
                
            }

            if(turnos > 25)
            {
                Fighter.Fight.EndFight();
                actor.Fight.EndFight();
            }

        }

        private void Fight_FightStarted(IFight obj)
        {
            Fighter.CastAutoSpell(new Spell(10321, 1), Fighter.Cell);
            //Fighter.CastAutoSpell(new Spell(12105, 1), Fighter.Cell);
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

    [BrainIdentifier(5311)]
    [BrainIdentifier(5313)]
    [BrainIdentifier(5309)]
    public class Vuelocerontes : Brain
    {
        private int turnos = 0;
        public Vuelocerontes(AIFighter fighter)
           : base(fighter)
        {
            fighter.Fight.FightStarted += Fight_FightStarted;
            fighter.Fight.TurnStarted += OnTurnStarted;
        }

        private void OnTurnStarted(IFight obj, FightActor actor)
        {
            turnos++;
            if (actor.HasState((int)SpellStatesEnum.APPRIVOISEMENT_10) || actor.HasState((int)SpellStatesEnum.APPRIVOISEMENT_CHEAT_427) || actor.HasState((int)SpellStatesEnum.APPRIVOISEMENT_10))
            {
                Fighter.RemoveAndDispellAllBuffs();
                Fighter.RemoveSpellBuffs(10321);
                
            }

            if(turnos > 25)
            {
                Fighter.Fight.EndFight();
                actor.Fight.EndFight();
            }

        }

        private void Fight_FightStarted(IFight obj)
        {
            Fighter.CastAutoSpell(new Spell(10326, 1), Fighter.Cell);
            //Fighter.CastAutoSpell(new Spell(12105, 1), Fighter.Cell);
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

    [BrainIdentifier(5310)]
    public class VuelocerontesInvo : Brain
    {
        public VuelocerontesInvo(AIFighter fighter)
            : base(fighter)
        {
            fighter.Fight.TurnStarted += OnTurnStarted;
        }

        private void OnTurnStarted(IFight obj, FightActor actor)
        {
            if (Fighter == actor)
                Fighter.CastAutoSpell(new Spell(10327, 1), Fighter.Cell);

        }
    }

}
