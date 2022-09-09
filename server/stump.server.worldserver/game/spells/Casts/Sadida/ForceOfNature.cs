using Stump.DofusProtocol.Enums;
using Stump.Server.WorldServer.Game.Actors.Fight;
using Stump.Server.WorldServer.Game.Fights;
using Stump.Server.WorldServer.Game.Spells.Casts;

namespace Stump.Server.WorldServer.game.spells.Casts.Sadida
{
    [SpellCastHandler(SpellIdEnum.FORCE_OF_NATURE_9760)]
    public class ForceOfNature : DefaultSpellCastHandler
    {
        public ForceOfNature(SpellCastInformations cast) : base(cast)
        {
        }


        public override void Execute()
        {
            if (!base.Initialize())
                Initialize();

            var hadlers = Handlers;
            var daño = 0;

            foreach (var fighter in Caster.Fight.Fighters)
            {
                if ((fighter is SummonedMonster) && ((fighter as SummonedMonster)?.Monster.MonsterId == 282))
                    daño += 2;
            }
            Handlers[1].Dice.DiceFace += daño;
            Handlers[1].Dice.DiceNum += daño;
            
            Handlers[0].Apply();
            Handlers[1].Apply();
        }
    }

    [SpellCastHandler(SpellIdEnum.FORCE_OF_NATURE_INFECTION)]
    public class ForceOfNature_Infectado : DefaultSpellCastHandler
    {
        public ForceOfNature_Infectado(SpellCastInformations cast) : base(cast)
        {
        }


        public override void Execute()
        {
            if (!base.Initialize())
                Initialize();

            var hadlers = Handlers;
            var daño = 0;

            foreach (var fighter in Caster.Fight.Fighters)
            {
                if ((fighter is SummonedMonster) && ((fighter as SummonedMonster)?.Monster.MonsterId == 282))
                    daño += 1;
            }
            Handlers[0].Dice.DiceFace += daño;
            Handlers[0].Dice.DiceNum += daño;
            Handlers[0].Apply();
        }
    }
}