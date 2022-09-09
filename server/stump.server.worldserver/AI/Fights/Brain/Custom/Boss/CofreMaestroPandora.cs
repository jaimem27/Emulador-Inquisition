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
    [BrainIdentifier((int)MonsterIdEnum.BOUATE_DE_PANDORE)]
    public class CofreMaestroPandora : Brain
    {
        public CofreMaestroPandora(AIFighter fighter)
            : base(fighter)
        {
            fighter.Fight.FightStarted += Fight_FightStarted;
            Fighter.GetAlive += OnGetAlive;
        }

        private void Fight_FightStarted(IFight obj)
        {
        }

        private void OnGetAlive(FightActor fighter)
        {
            if (fighter != Fighter)
                return;

            Fighter.Stats[PlayerFields.AirResistPercent].Base = 100;
            Fighter.Stats[PlayerFields.NeutralResistPercent].Base = 100;
            Fighter.Stats[PlayerFields.FireResistPercent].Base = 100;
            Fighter.Stats[PlayerFields.WaterResistPercent].Base = 100;
            Fighter.Stats[PlayerFields.EarthResistPercent].Base = 100;
        }

        public override void Play()
        {
            foreach (var spell in Fighter.Spells.Values)
            {
                var target = Environment.GetNearestEnemy();
                var invo = Environment.GetFreeAdjacentCell();
                var selector = new PrioritySelector();

                if (spell.Id == 570)
                    Fighter.CastAutoSpell(new Spell(570, 1), invo);

                    //if(spell.Id == 570)
                    //{
                    //    var random = new Random();
                    //    var inv = random.Next(0, 10);

                    //    switch (inv)
                    //    {
                    //        case 1:
                    //            Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.SUMMONING_))
                    //            break;
                    //        case 2:
                    //            break;
                    //        case 3:
                    //            break;
                    //        case 4:
                    //            break;
                    //        case 5:
                    //            break;
                    //        case 6:
                    //            break;
                    //        case 7:
                    //            break;
                    //        case 8:
                    //            break;
                    //        case 9:
                    //            break;
                    //        case 10:
                    //            break;
                    //        default:
                    //            break;
                    //    }


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