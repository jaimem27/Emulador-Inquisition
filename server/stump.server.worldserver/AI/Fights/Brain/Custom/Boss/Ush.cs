using Stump.Core.Extensions;
using Stump.DofusProtocol.Enums;
using Stump.Server.WorldServer.Game.Actors.Fight;
using Stump.Server.WorldServer.Game.Fights;
using Stump.Server.WorldServer.Game.Spells;
using Stump.Server.WorldServer.Game.Spells.Casts;
using System;
using System.Linq;
using static Stump.Server.WorldServer.Game.Actors.Fight.FightActor;

namespace Stump.Server.WorldServer.AI.Fights.Brain.Custom.Boss
{
    [BrainIdentifier(4264)]
    public class Ush : Brain
    {
        public Ush(AIFighter fighter)
            : base(fighter)
        {
            fighter.Fight.FightStarted += Fight_FightStarted;
        }
        

        private void Fight_FightStarted(IFight obj)
        {
            //Fighter.Stats[PlayerFields.SummonLimit].Additional = 9999;
            //Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.RED_AND_BLACK, 1), Fighter.Cell);
            Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.RED_AND_BLACK_6239, 1), Fighter.Cell); //Reparticion de daños
            Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.RED_AND_BLACK_6264, 1), Fighter.Cell);;
            
        }
    }

    [BrainIdentifier(4299)]
    [BrainIdentifier(4296)]
    [BrainIdentifier(4297)]
    [BrainIdentifier(4300)]
    [BrainIdentifier(4298)]
    public class Miaugusto : Brain
    {
        public Miaugusto(AIFighter fighter)
            : base(fighter)
        {
            fighter.Fight.FightStarted += Fight_FightStarted;
            fighter.Fight.TurnStarted += OnTurnStarted;
        }

        private void OnTurnStarted(IFight obj, FightActor actor)
        {
            if (Fighter == actor)
            {
                if (!Fighter.HasState((int)SpellIdEnum.MOGGYMORPHOSIS_10265))
                {
                    Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.MOGGYMORPHOSIS_10265, 1), Fighter.Cell);
                    Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.MOGGYMORPHOSIS_10267, 1), Fighter.Cell);
                }
                    
            }
        }


        private void Fight_FightStarted(IFight obj)
        {
            //Fighter.Stats[PlayerFields.SummonLimit].Additional = 9999;
            Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.MOGGYMORPHOSIS_10265, 1), Fighter.Cell);
            Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.MOGGYMORPHOSIS_10267, 1), Fighter.Cell);
            //Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.MOGGYMORPHOSIS_10268, 1), Fighter.Cell);
            //Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.MOGGYMORPHOSIS_6604, 1), Fighter.Cell);
            //Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.MOGGYMORPHOSIS_6605, 1), Fighter.Cell);
            //Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.MOGGYMORPHOSIS_6606, 1), Fighter.Cell);
        }
    }

}