//using Stump.Core.Extensions;
//using Stump.DofusProtocol.Enums;
//using Stump.Server.WorldServer.AI.Fights.Actions;
//using Stump.Server.WorldServer.Game.Actors.Fight;
//using Stump.Server.WorldServer.Game.Fights;
//using Stump.Server.WorldServer.Game.Spells;
//using Stump.Server.WorldServer.Game.Spells.Casts;
//using System.Linq;
//using TreeSharp;
//using static Stump.Server.WorldServer.Game.Actors.Fight.FightActor;

//namespace Stump.Server.WorldServer.AI.Fights.Brain.Custom.Boss
//{
//    [BrainIdentifier((int)MonsterIdEnum.MAITRE_DES_PANTINS)]
//    public class MarionetaMaster : Brain
//    {

//        public MarionetaMaster(AIFighter fighter)
//            : base(fighter)
//        {
//            fighter.Fight.FightStarted += Fight_FightStarted;
//            fighter.TurnPassed += Fight_EndTurn;
//        }

//        private void Fight_FightStarted(IFight obj)
//        {
//            Fighter.Stats[PlayerFields.SummonLimit].Additional = 9999;
//            //Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.SCRIPTING_3874, 1), Fighter.Cell);
//            Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.SCRIPTING_3875, 1), Fighter.Cell);
//        }

//        private void Fight_EndTurn(FightActor fighter)
//        {
            
//            fighter.CastAutoSpell(new Spell((int)SpellIdEnum.BUNRAKU_3962, 1), Fighter.Cell);
            
//        }

//        public int Acto()
//        {

//            if (Fighter.HasState((int)SpellStatesEnum.ACTE_1_159))
//            {
//                return 1;
                
//            }
                
//            if (Fighter.HasState((int)SpellStatesEnum.ACTE_2_160))
//            {
//                return 2;
 
//            }
//            if (Fighter.HasState((int)SpellStatesEnum.ACTE_3_161))
//            {
//                return 3;

//            }
//            if (Fighter.HasState((int)SpellStatesEnum.ACTE_4_162))
//            {
//                return 4;
                
//            }
//            if (Fighter.HasState((int)SpellStatesEnum.ACTE_5_163))
//            {
//                return 5;
                
//            }

//            Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.SCRIPTING_3874, 1), Fighter.Cell);

//            return 6;
//        }


//        public override void Play()
//        {

//            foreach (var spell in Fighter.Spells.Values)
//            {
//                if (spell.Id == 3881)
//                {                   

//                    switch (Acto())
//                    {
//                        case 1:
//                            Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.SUMMONING_OF_WHITE_PUPPET, 1), Fighter.Cell);
//                            break;
//                        case 2:
//                            Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.SUMMONING_OF_BLUE_PUPPET, 1), Fighter.Cell);
//                            break;
//                        case 3:
//                            Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.SUMMONING_OF_RED_PUPPET, 1), Fighter.Cell);
//                            break;
//                        case 4:
//                            Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.SUMMONING_OF_GREEN_PUPPET, 1), Fighter.Cell);
//                            break;
//                        case 5:
//                            Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.SUMMONING_OF_GREY_PUPPET, 1), Fighter.Cell);
//                            break;
//                        default:
//                            Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.SUMMONING_OF_WHITE_PUPPET, 1), Fighter.Cell);
//                            break;
//                    }

//                }

//                var target = Environment.GetNearestEnemy();

//                if (spell.Id == 3871)
//                {
//                    if(!Fighter.HasState((int)SpellIdEnum.SCRIPTING_3875))
//                        Fighter.CastSpell(new Spell(3871, 1), Fighter.Cell);

//                }


//                var selector = new PrioritySelector();

//                selector.AddChild(new Decorator(ctx => target == null, new DecoratorContinue(new RandomMove(Fighter))));
//                selector.AddChild(new Decorator(ctx => spell == null, new DecoratorContinue(new FleeAction(Fighter))));

//                if (target != null && spell != null)
//                {
//                    selector.AddChild(new PrioritySelector(
//                        new Decorator(ctx => Fighter.CanCastSpell(spell, target.Cell) == SpellCastResult.OK,
//                            new Sequence(
//                                new SpellCastAction(Fighter, spell, target.Cell, true),
//                                new Decorator(new MoveNearTo(Fighter, target)))),
//                        new Sequence(
//                            new MoveNearTo(Fighter, target),
//                            new Decorator(
//                                ctx => Fighter.CanCastSpell(spell, target.Cell) == SpellCastResult.OK,
//                                new Sequence(
//                                    new SpellCastAction(Fighter, spell, target.Cell, true))))));
//                }

//                foreach (var action in selector.Execute(this))
//                {

//                }

//            }

//        }

//    }

//    [BrainIdentifier(3522)]
//    public class Marioneta : Brain
//    {

//        public Marioneta(AIFighter fighter)
//            : base(fighter)
//        {
//            fighter.Fight.TurnStarted += OnTurnStarted;
//        }

//        private void OnTurnStarted(IFight obj, FightActor actor)
//        {
//            if (Fighter != null)
//            {
//                Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.PERFORMANCE_3872, 1), Fighter.Cell);
//                Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.PERFORMANCE_3873, 1), Fighter.Cell);
//            }
//        }

//    }

//    [BrainIdentifier(3477)]
//    public class MarionetaBlanca : Brain
//    {

//        public MarionetaBlanca(AIFighter fighter)
//            : base(fighter)
//        {
//            fighter.Fight.TurnStarted += OnTurnStarted;
//        }

//        private void OnTurnStarted(IFight obj, FightActor actor)
//        {
//            if (Fighter != null)
//            {
//                Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.PERFORMANCE_3873, 2), Fighter.Cell);
//                Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.PERFORMANCE_3872, 1), Fighter.Cell);
//            }
//        }

//    }

//    [BrainIdentifier(3478)]
//    public class MarionetaAzul : Brain
//    {

//        public MarionetaAzul(AIFighter fighter)
//            : base(fighter)
//        {
//            fighter.Fight.TurnStarted += OnTurnStarted;
//        }

//        private void OnTurnStarted(IFight obj, FightActor actor)
//        {
//            if (Fighter != null)
//            {
//                Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.PERFORMANCE_3873, 3), Fighter.Cell);
//                Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.PERFORMANCE_3872, 2), Fighter.Cell);
//            }
//        }

//    }

//    [BrainIdentifier(3479)]
//    public class MarionetaRoja : Brain
//    {

//        public MarionetaRoja(AIFighter fighter)
//            : base(fighter)
//        {
//            fighter.Fight.TurnStarted += OnTurnStarted;
//        }

//        private void OnTurnStarted(IFight obj, FightActor actor)
//        {
//            if (Fighter != null)
//            {
//                Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.PERFORMANCE_3873, 4), Fighter.Cell);
//                Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.PERFORMANCE_3872, 3), Fighter.Cell);
//            }
//        }

//    }

//    [BrainIdentifier(3480)]
//    public class MarionetaVerde : Brain
//    {

//        public MarionetaVerde(AIFighter fighter)
//            : base(fighter)
//        {
//            fighter.Fight.TurnStarted += OnTurnStarted;
//        }

//        private void OnTurnStarted(IFight obj, FightActor actor)
//        {
//            if (Fighter != null)
//            {
//                Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.PERFORMANCE_3873, 5), Fighter.Cell);
//                Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.PERFORMANCE_3872, 4), Fighter.Cell);
//            }
//        }

//    }

//    [BrainIdentifier(3481)]
//    public class MarionetaGris : Brain
//    {

//        public MarionetaGris(AIFighter fighter)
//            : base(fighter)
//        {
//            fighter.Fight.TurnStarted += OnTurnStarted;
//        }

//        private void OnTurnStarted(IFight obj, FightActor actor)
//        {
//            if (Fighter != null)
//            {
//                Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.PERFORMANCE_3873, 6), Fighter.Cell);
//                Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.PERFORMANCE_3872, 5), Fighter.Cell);
//            }
//        }

//    }


//    [BrainIdentifier(3482)]
//    public class Dramak : Brain
//    {

//        public Dramak(AIFighter fighter)
//            : base(fighter)
//        {
//            fighter.Fight.FightStarted += Fight_FightStarted;
//        }

//        private void Fight_FightStarted(IFight obj)
//        {
//            //Fighter.Stats[PlayerFields.SummonLimit].Additional = 9999;
//            Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.LIVING_SHOW, 1), Fighter.Cell);
//            //Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.SCRIPTING_3875, 1), Fighter.Cell);
//        }

//    }

//}