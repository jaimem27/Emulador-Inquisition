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
    [BrainIdentifier(3835)]
    public class Vortex : Brain
    {
        public Vortex(AIFighter fighter)
            : base(fighter)
        {
            fighter.Fight.FightStarted += Fight_FightStarted;
        }

        private void Fight_FightStarted(IFight obj)
        {
            //Fighter.Stats[PlayerFields.SummonLimit].Additional = 9999;
            //Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.VORTEXIPHAN, 1), Fighter.Cell);
            //Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.VORTEXIPHAN_5003, 1), Fighter.Cell);
            //Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.VORTEXIPHAN_5006, 1), Fighter.Cell);
            //Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.VORTEXIPHAN_5007, 1), Fighter.Cell);
            //Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.VORTEXIPHAN_5008, 1), Fighter.Cell);

        }

    }

    [BrainIdentifier((int)MonsterIdEnum.AURORAIRE_3833)]
    public class VortexInvo : Brain
    {
        public VortexInvo(AIFighter fighter)
            : base(fighter)
        {
            fighter.GetAlive += OnGetAlive;
        }

        private void OnGetAlive(FightActor fighter)
        {
            if (fighter != Fighter)
                return;

            Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.ZEITGEIST_4999, 1), Fighter.Cell);
            //Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.ZEITGEIST_5104, 1), Fighter.Cell);
            //Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.ZEITGEIST_5108, 1), Fighter.Cell);
        }
    }

    [BrainIdentifier(3837)]
    [BrainIdentifier(3836)]
    [BrainIdentifier(3839)]
    [BrainIdentifier(3838)]
    [BrainIdentifier(3834)]
    public class VortexBichos : Brain
    {
        public VortexBichos(AIFighter fighter)
            : base(fighter)
        {
            fighter.Fight.FightStarted += Fight_FightStarted;
            fighter.Fight.TurnStarted += OnTurnStarted;
        }

        private void OnTurnStarted(IFight obj, FightActor actor)
        {
            if (Fighter == actor)
            {
                if (!Fighter.HasState((int)SpellIdEnum.TELEPORTATION_GLYPH_5002))
                {
                    Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.TELEPORTATION_GLYPH_5002, 1), Fighter.Cell);
                    
                }

            }
        }


        private void Fight_FightStarted(IFight obj)
        {
            //Fighter.Stats[PlayerFields.SummonLimit].Additional = 9999;
            Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.TELEPORTATION_GLYPH_5002, 1), Fighter.Cell);
            
        }
    }


}