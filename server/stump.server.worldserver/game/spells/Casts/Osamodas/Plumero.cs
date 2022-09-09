using System.Linq;
using Stump.DofusProtocol.Enums;
using Stump.Server.WorldServer.Game.Actors.Fight;
using Stump.Server.WorldServer.Game.Fights;
using Stump.Server.WorldServer.Handlers.Actions;

namespace Stump.Server.WorldServer.Game.Spells.Casts.Osamodas
{
    [SpellCastHandler(SpellIdEnum.DUSTER_33)]
    public class Plumero : DefaultSpellCastHandler
    {
        public Plumero(SpellCastInformations cast)
            : base(cast)
        {
        }

        public override bool Initialize()
        {
            var countTofus = 0;

            foreach (var fighter in Fight.Fighters)
            {
                if (!fighter.IsAlive())
                    break;
                if ((fighter is SummonedMonster) && ((fighter as SummonedMonster)?.Monster.MonsterId == 4561 ||
                    (fighter as SummonedMonster)?.Monster.MonsterId == 4562 ||
                    (fighter as SummonedMonster)?.Monster.MonsterId == 5131))
                    countTofus++;
            }

            double  incrementDamage = 0.0;
            if (countTofus > 0)
            {
                incrementDamage = ((countTofus * 5) / 100.0);
            }
            if (base.Initialize())
            {
                Handlers[0].Dice.DiceNum = 27;
                Handlers[0].Dice.DiceFace = 29;
                if (Critical)
                {
                    Handlers[0].Dice.DiceNum = 32;
                    Handlers[0].Dice.DiceFace = 34;
                }
                Handlers[0].Priority = 0;
                Handlers[0].Dice.DiceNum += (int)(incrementDamage * Handlers[0].Dice.DiceNum);
                Handlers[0].Dice.DiceFace += (int)(incrementDamage * Handlers[0].Dice.DiceFace);
                return true;
            }

            return false;
        }
    }
}