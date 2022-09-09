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
    [BrainIdentifier(3648)]
    public class Nidas : Brain
    {
        public Nidas(AIFighter fighter)
            : base(fighter)
        {
            
            
        }

        int turno = 0;
        public override void Play()
        {

            foreach (var spell in Fighter.Spells.Values)
            {
                var invo = Environment.GetFreeAdjacentCell();
                var target = Environment.GetNearestEnemy();

                if (turno == 0)
                {
                    target.CastAutoSpell(new Spell(4478, 1), target.Cell);
                    turno++;
                }
                //Fighter.CastSpell(new Spell(4479, 1), target.Cell);
                //Fighter.CastSpell(new Spell(4480, 1), target.Cell);
                //Fighter.CastSpell(new Spell(4921, 1), target.Cell);

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


    [BrainIdentifier(3657)]
    public class Melenutrof : Brain
    {
        public Melenutrof(AIFighter fighter)
            : base(fighter)
        {
            fighter.Fight.FightStarted += Fight_FightStarted;
            fighter.Fight.TurnStarted += OnTurnStarted;

        }

        private void OnTurnStarted(IFight obj, FightActor actor)
        {
            if (Fighter == actor)
            {
                if (!Fighter.HasState((int)SpellIdEnum.AUSTERITY_4528))
                {
                    Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.AUSTERITY_4528, 1), Fighter.Cell);
                }

            }
        }

        private void Fight_FightStarted(IFight obj)
        {
            //Fighter.Stats[PlayerFields.SummonLimit].Additional = 9999;
            Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.AUSTERITY_4528, 1), Fighter.Cell);
            //Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.AUSTERITY_4529, 1), Fighter.Cell);
        }
    }

    [BrainIdentifier(3653)]
    public class Kamasterisk : Brain
    {
        public Kamasterisk(AIFighter fighter)
            : base(fighter)
        {
            fighter.Fight.FightStarted += Fight_FightStarted;
            fighter.Fight.TurnStarted += OnTurnStarted;

        }

        private void OnTurnStarted(IFight obj, FightActor actor)
        {
            if (Fighter == actor)
            {
                if (!Fighter.HasState((int)SpellIdEnum.BLISTRINBARNAK_4513))
                {
                    Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.BLISTRINBARNAK_4513, 1), Fighter.Cell);
                    //Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.BLISTRINBARNAK_4514, 1), Fighter.Cell);
                }

            }
        }

        private void Fight_FightStarted(IFight obj)
        {
            //Fighter.Stats[PlayerFields.SummonLimit].Additional = 9999;
            Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.BLISTRINBARNAK_4513, 1), Fighter.Cell);
            //Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.BLISTRINBARNAK_4514, 1), Fighter.Cell);
        }
    }

    [BrainIdentifier(3650)]
    public class NidasInvo : Brain
    {
        public NidasInvo(AIFighter fighter)
            : base(fighter)
        {
            fighter.Fight.FightStarted += Fight_FightStarted;
            fighter.Fight.TurnStarted += OnTurnStarted;
        }

        private void OnTurnStarted(IFight obj, FightActor actor)
        {
            if (Fighter == actor)
            {
                if (!Fighter.HasState((int)SpellIdEnum.POUCHIPINCH_4482))
                {
                    Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.POUCHIPINCH_4482, 1), Fighter.Cell);
                    //Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.POUCHIPINCH_4483, 1), Fighter.Cell);
                    //Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.POUCHIPINCH_4484, 1), Fighter.Cell);
                    //Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.POUCHIPINCH_4920, 1), Fighter.Cell);
                }

            }
        }

        private void Fight_FightStarted(IFight obj)
        {
            //Fighter.Stats[PlayerFields.SummonLimit].Additional = 9999;
            //Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.POUCHIPINCH_4482, 1), Fighter.Cell);
            //Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.POUCHIPINCH_4483, 1), Fighter.Cell);
            //Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.POUCHIPINCH_4484, 1), Fighter.Cell);
            //Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.POUCHIPINCH_4920, 1), Fighter.Cell);
        }
    }

}