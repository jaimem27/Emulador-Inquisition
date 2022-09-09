using Stump.DofusProtocol.Enums;
using Stump.Server.WorldServer.AI.Fights.Actions;
using Stump.Server.WorldServer.Game.Actors.Fight;
using Stump.Server.WorldServer.Game.Fights;
using Stump.Server.WorldServer.Game.Spells;
using System.Linq;
using TreeSharp;

namespace Stump.Server.WorldServer.AI.Fights.Brain.Custom.Boss
{
    [BrainIdentifier((int)MonsterIdEnum.DOPEUL_SACRIEUR_455)]
    public class DopeulSacro : Brain
    {
        public DopeulSacro(AIFighter fighter)
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

                var selector = new PrioritySelector();

                if (spell.Id == 8142) //Sacro
                    Fighter.CastSpell(new Spell((int)SpellIdEnum.DOPPLESQUE_BLOODTHIRSTY_PUNISHMENT, 6),Fighter.Cell);

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

    [BrainIdentifier((int)MonsterIdEnum.DOPEUL_HUPPERMAGE_4290)]
    public class DopeulHipper : Brain
    {
        public DopeulHipper(AIFighter fighter)
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

                var selector = new PrioritySelector();

                if (spell.Id == 5840) //Hipermago
                    Fighter.CastSpell(new Spell(5840, 6), Fighter.Cell);
                if (spell.Id == 5833)
                    Fighter.CastSpell(new Spell(5833, 6), Fighter.Cell);

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

    [BrainIdentifier((int)MonsterIdEnum.DOPEUL_ENIRIPSA_166)]
    public class DopeulEni : Brain
    {
        public DopeulEni(AIFighter fighter)
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

                if (spell.Id == 2094) //Invos
                    Fighter.CastSpell(new Spell(2094, 6), invo);
                if (spell.Id == 6686)
                    Fighter.CastSpell(new Spell(6686, 6), invo);

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

    [BrainIdentifier((int)MonsterIdEnum.DOPEUL_ROUBLARD_3111)]
    [BrainIdentifier((int)MonsterIdEnum.DOPEUL_XELOR_164)]
    public class DopeulNormales : Brain
    {
        public DopeulNormales(AIFighter fighter)
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

    [BrainIdentifier((int)MonsterIdEnum.DOPEUL_SADIDA_169)]
    public class DopeulSadida : Brain
    {
        public DopeulSadida(AIFighter fighter)
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

                if (spell.Id == 5676) //Sadida
                    Fighter.CastSpell(new Spell(5676, 6), invo);

                if (spell.Id == 2130) //Sadida
                    Fighter.CastSpell(new Spell(2130, 6), invo);
                if (spell.Id == 2132) //Sadida
                    Fighter.CastSpell(new Spell(2132, 6), invo);
                if (spell.Id == 2127) //Sadida
                    Fighter.CastSpell(new Spell(2127, 6), invo);
                if (spell.Id == 2133) //Sadida
                    Fighter.CastSpell(new Spell(2133, 6), invo);

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


    [BrainIdentifier((int)MonsterIdEnum.DOPEUL_OUGINAK_4777)]
    public class DopeulUgi : Brain
    {
        public DopeulUgi(AIFighter fighter)
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
                var selector = new PrioritySelector();

                    
                if (spell.Id == 8334) //Ugi
                    Fighter.CastSpell(new Spell(8334, 3), Fighter.Cell);
                if (spell.Id == 8331) //Ugi
                    Fighter.CastSpell(new Spell(8331, 3), Fighter.Cell);

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