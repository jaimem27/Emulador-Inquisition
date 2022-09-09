using Stump.Core.Extensions;
using Stump.Core.Reflection;
using Stump.DofusProtocol.Enums;
using Stump.Server.WorldServer.AI.Fights.Actions;
using Stump.Server.WorldServer.Game.Actors.Fight;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Monsters;
using Stump.Server.WorldServer.Game.Fights;
using Stump.Server.WorldServer.Game.Fights.Buffs;
using Stump.Server.WorldServer.Game.Fights.Triggers;
using Stump.Server.WorldServer.Game.Maps.Cells;
using Stump.Server.WorldServer.Game.Spells;
using Stump.Server.WorldServer.Game.Spells.Casts;
using System;
using System.Linq;
using TreeSharp;
using static Stump.Server.WorldServer.Game.Actors.Fight.FightActor;

namespace Stump.Server.WorldServer.AI.Fights.Brain.Custom.Boss
{
    [BrainIdentifier((int)MonsterIdEnum.KRALAMOURE_GEANT)]
    public class Kralamar : Brain
    {
        public Kralamar(AIFighter fighter)
            : base(fighter)
        {
            fighter.Fight.FightStarted += Fight_FightStarted;
            fighter.TurnPassed += OnGetAlive;
        }

        private void Fight_FightStarted(IFight obj)
        {
            Fighter.Stats[PlayerFields.SummonLimit].Base = 4;
            Fighter.Stats[PlayerFields.Initiative].Base = 999999;

           
        }

        private void OnGetAlive(FightActor fighter)
        {
            
            

        }

        

    }

    [BrainIdentifier(1090)]
    public class UltimoTentaculo : Brain
    {
        public UltimoTentaculo(AIFighter fighter)
            : base(fighter)
        {
            Fighter.GetAlive += OnGetAlive;
            //Fighter.TurnPassed += OnGetAlive;
        }

        private void OnGetAlive(FightActor fighter)
        {
            if (fighter != null)
            {
                if (fighter.HasState((int)SpellStatesEnum.ENCRE_QUATERNAIRE_34))
                    fighter.Summoner.CastAutoSpell(new Spell((int)SpellIdEnum.PEAT_VULNERABILITY, 1), Fighter.Cell);

                if (fighter.HasState((int)SpellIdEnum.FOURTH_KRAKEN))
                    fighter.CastAutoSpell(new Spell((int)SpellIdEnum.PEAT_VULNERABILITY, 1), Fighter.Cell);
            }
            //if (fighter != null)
            
        }

        public override void Play()
        {

            foreach (var spell in Fighter.Spells.Values)
            {
                var invo = Environment.GetFreeAdjacentCell();
                var target = Environment.GetNearestEnemy();

                if (spell.Id == 1099)
                {
                    Fighter.CastSpell(new Spell((int)SpellIdEnum.FOURTH_KRAKEN, 1), Fighter.Cell);
                    if(Fighter.HasState((int)SpellStatesEnum.ENCRE_TERTIAIRE_33))
                        Fighter.Summoner.CastAutoSpell(new Spell((int)SpellIdEnum.PEAT_VULNERABILITY, 1), Fighter.Cell);
                }

                if (spell.Id == 1102)
                    Fighter.CastSpell(new Spell((int)SpellIdEnum.TENTACULAR_PARALYSIS, 1), Fighter.Cell);


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

    //[BrainIdentifier(2816)]
    //public class TotemAgua : Brain
    //{
    //    public TotemAgua(AIFighter fighter)
    //        : base(fighter)
    //    {
    //        Fighter.GetAlive += OnGetAlive;
    //        Fighter.Dead += OnGetDead;
    //    }

    //    private void OnGetDead(FightActor fighter, FightActor fighter2)
    //    {
    //        Fighter.Summoner.Summoner.Stats[PlayerFields.WaterResistPercent].Base = 0;
    //    }

    //    private void OnGetAlive(FightActor fighter)
    //    {
    //        if (fighter != Fighter)
    //            return;

    //        Fighter.Summoner.Summoner.Stats[PlayerFields.WaterResistPercent].Base = 200;
    //        var invo = Environment.GetFreeAdjacentCell();
    //        //Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.DARK_POWER_302, 1), Fighter.Cell);
    //        Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.WATER_TOTEM_6167, 4), invo);
    //    }



    //}

    //[BrainIdentifier(2815)]
    //public class TotemFuego : Brain
    //{
    //    public TotemFuego(AIFighter fighter)
    //        : base(fighter)
    //    {
    //        Fighter.GetAlive += OnGetAlive;
    //        Fighter.Dead += OnGetDead;
    //    }

    //    private void OnGetDead(FightActor fighter, FightActor fighter2)
    //    {
    //        Fighter.Summoner.Summoner.Stats[PlayerFields.FireResistPercent].Base = 0;
    //    }

    //    private void OnGetAlive(FightActor fighter)
    //    {
    //        if (fighter != Fighter)
    //            return;

    //        Fighter.Summoner.Summoner.Stats[PlayerFields.FireResistPercent].Base = 200;
    //        var invo = Environment.GetFreeAdjacentCell();
    //        //Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.DARK_POWER_302, 1), Fighter.Cell);
    //        Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.FIRE_TOTEM_6163, 4), invo);
    //    }



    //}

    //[BrainIdentifier(2818)]
    //public class TotemTierra : Brain
    //{
    //    public TotemTierra(AIFighter fighter)
    //        : base(fighter)
    //    {
    //        Fighter.GetAlive += OnGetAlive;
    //        Fighter.Dead += OnGetDead;
    //    }

    //    private void OnGetDead(FightActor fighter, FightActor fighter2)
    //    {
    //        Fighter.Summoner.Summoner.Stats[PlayerFields.EarthResistPercent].Base = 0;
    //    }

    //    private void OnGetAlive(FightActor fighter)
    //    {
    //        if (fighter != Fighter)
    //            return;

    //        Fighter.Summoner.Summoner.Stats[PlayerFields.EarthResistPercent].Base = 200;
    //        var invo = Environment.GetFreeAdjacentCell();
    //        //Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.DARK_POWER_302, 1), Fighter.Cell);
    //        Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.EARTH_TOTEM_6165, 4), invo);
    //    }



    //}


    //[BrainIdentifier((int)MonsterIdEnum.MOON)]
    //public class Moon : Brain
    //{
    //    public Moon(AIFighter fighter)
    //        : base(fighter)
    //    {
    //        fighter.Fight.FightStarted += Fight_FightStarted;


    //    }

    //    private void Fight_FightStarted(IFight obj)
    //    {
    //        Fighter.Stats[PlayerFields.SummonLimit].Additional = 9999;
    //        //Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.DARK_POWER_302, 1), Fighter.Cell);
    //        //Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.DARK_POWER_3276, 1), Fighter.Cell);

    //    }


    //}

}