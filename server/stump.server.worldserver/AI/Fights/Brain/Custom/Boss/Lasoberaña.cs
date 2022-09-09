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
    [BrainIdentifier(3996)]
    public class Lasoberaña : Brain
    {
        public Lasoberaña(AIFighter fighter)
            : base(fighter)
        {
            fighter.Fight.FightStarted += Fight_FightStarted;
        }

        private void Fight_FightStarted(IFight obj)
        {
            Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.QUEEN_S_CALLING, 1), Fighter.Cell);
        }
    }


    [BrainIdentifier(3997)]
    public class Huevo : Brain
    {
        
        public Huevo(AIFighter fighter)
            : base(fighter)
        {
            fighter.Fight.FightStarted += Fight_FightStarted;
            Fighter.GetAlive += OnGetAlive;
        }



        private void Fight_FightStarted(IFight obj)
        {
            Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.ACCELERATED_INCUBATION_5515, 1), Fighter.Cell);
            Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.ACCELERATED_INCUBATION_5616, 1), Fighter.Cell);
        }

        private void OnGetAlive(FightActor fighter)
        {
            if (fighter != Fighter)
                return;

            if (fighter is SummonedMonster && (fighter as SummonedMonster).Monster.MonsterId == 3997) 
            {
                Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.ACCELERATED_INCUBATION_5515, 1), Fighter.Cell);
                Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.ACCELERATED_INCUBATION_5616, 1), Fighter.Cell);

            }

            //if(turnos > 2)
            //{
            //    var grade = Singleton<MonsterManager>.Instance.GetMonsterGrade((int)3998, 1);
                
            //    var monster = new Monster(grade, new MonsterGroup(0, fighter.Position));                

            //    Fighter.Fight.DefendersTeam.AddFighter(new MonsterFighter(fighter.Fight.DefendersTeam,monster));
            //    fighter.Die();
                
            //}

        }

    }

    [BrainIdentifier(13997)]
    public class Arapex : Brain
    {
        public Arapex(AIFighter fighter)
            : base(fighter)
        {
        }


        public override void Play()
        {

            foreach (var spell in Fighter.Spells.Values)
            {
                var invo = Environment.GetFreeAdjacentCell();
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