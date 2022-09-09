using Stump.Core.Extensions;
using Stump.DofusProtocol.Enums;
using Stump.Server.WorldServer.Game.Actors.Fight;
using Stump.Server.WorldServer.Game.Fights;
using Stump.Server.WorldServer.Game.Fights.Buffs;
using Stump.Server.WorldServer.Game.Spells;
using Stump.Server.WorldServer.Game.Spells.Casts;
using System.Linq;
using static Stump.Server.WorldServer.Game.Actors.Fight.FightActor;

namespace Stump.Server.WorldServer.AI.Fights.Brain.Custom.Boss
{
    [BrainIdentifier(3828)]
    public class Ballena : Brain
    {
        public Ballena(AIFighter fighter)
            : base(fighter)
        {
            fighter.Fight.FightStarted += Fight_FightStarted;
            fighter.DamageReducted += OnDamageReducted;
        }

        private void OnDamageReducted(FightActor a, FightActor b, int daño)
        {
            

        }

        private void Fight_FightStarted(IFight obj)
        {
            //Fighter.Stats[PlayerFields.SummonLimit].Additional = 9999;
            //Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.INFECTIOUS_RESTORATION_4972, 1), Fighter.Cell);
            //Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.INFECTIOUS_RESTORATION_4974, 1), Fighter.Cell);
            
        }


    }

    [BrainIdentifier(3822)]
    public class BallenaInvo : Brain
    {
        public BallenaInvo(AIFighter fighter)
            : base(fighter)
        {
            fighter.Fight.TurnStarted += OnTurnStarted;
        }
        int turno = 0;

        private void OnTurnStarted(IFight obj, FightActor actor)
        {
            if (Fighter == actor && turno == 0)
            {
                Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.DECELLULARISATION, 1), Fighter.Cell);
                turno++;
            }

        }
    }

    [BrainIdentifier(3823)]
    public class Baciloloco : Brain
    {
        public Baciloloco(AIFighter fighter)
            : base(fighter)
        {
            fighter.Fight.FightStarted += Fight_FightStarted;

        }


        private void Fight_FightStarted(IFight obj)
        {
            //Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.CURATIVE_RESTORATION_4936, 1), Fighter.Cell); // Efecto pasivo.
            //Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.CURATIVE_RESTORATION_4937, 1), Fighter.Cell);
            //Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.CURATIVE_RESTORATION_4938, 1), Fighter.Cell);
            Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.CURATIVE_RESTORATION_5179, 1), Fighter.Cell);

        }
    }

    [BrainIdentifier(3825)]
    public class BacTerrible : Brain
    {
        public BacTerrible(AIFighter fighter)
            : base(fighter)
        {
            fighter.Fight.FightStarted += Fight_FightStarted;

        }


        private void Fight_FightStarted(IFight obj)
        {
            //Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.DISTANT_RESTORATION, 1), Fighter.Cell); // Efecto pasivo.
            //Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.DISTANT_RESTORATION_4946, 1), Fighter.Cell);
            //Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.DISTANT_RESTORATION_4947, 1), Fighter.Cell);


        }
    }

    [BrainIdentifier(3827)]
    public class Patogermen : Brain
    {
        public Patogermen(AIFighter fighter)
            : base(fighter)
        {
            fighter.Fight.FightStarted += Fight_FightStarted;

        }


        private void Fight_FightStarted(IFight obj)
        {
            Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.ELEMENTAL_RESTORATION, 1), Fighter.Cell); // Efecto pasivo.
            Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.ELEMENTAL_RESTORATION_4956, 1), Fighter.Cell);
            Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.ELEMENTAL_RESTORATION_4957, 1), Fighter.Cell);


        }
    }

    [BrainIdentifier(3824)]
    public class Pulginfecto : Brain
    {
        public Pulginfecto(AIFighter fighter)
            : base(fighter)
        {
            fighter.Fight.FightStarted += Fight_FightStarted;

        }


        private void Fight_FightStarted(IFight obj)
        {
            Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.PROXIMITY_RESTORATION, 1), Fighter.Cell); // Efecto pasivo.
            //Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.PROXIMITY_RESTORATION_4941, 1), Fighter.Cell);
            //Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.PROXIMITY_RESTORATION_4942, 1), Fighter.Cell);


        }
    }

    [BrainIdentifier(3826)]
    public class Virusca : Brain
    {
        public Virusca(AIFighter fighter)
            : base(fighter)
        {
            fighter.Fight.FightStarted += Fight_FightStarted;
        }


        private void Fight_FightStarted(IFight obj)
        {
            Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.MOBILE_RESTORATION, 1), Fighter.Cell); // Efecto pasivo.
            //Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.MOBILE_RESTORATION_4951, 1), Fighter.Cell);
            //Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.MOBILE_RESTORATION_4952, 1), Fighter.Cell);


        }
    }

}