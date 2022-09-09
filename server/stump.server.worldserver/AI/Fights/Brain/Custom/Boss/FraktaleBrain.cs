using Stump.Core.Extensions;
using Stump.DofusProtocol.Enums;
using Stump.Server.WorldServer.Game.Actors.Fight;
using Stump.Server.WorldServer.Game.Fights;
using Stump.Server.WorldServer.Game.Spells;
using Stump.Server.WorldServer.Game.Spells.Casts;
using System.Linq;
using static Stump.Server.WorldServer.Game.Actors.Fight.FightActor;

namespace Stump.Server.WorldServer.AI.Fights.Brain.Custom.Boss
{
    [BrainIdentifier((int)MonsterIdEnum.FRAKTALE)]
    public class FraktaleBrain : Brain
    {
        public FraktaleBrain(AIFighter fighter)
            : base(fighter)
        {
            fighter.GetAlive += OnGetAlive;
        }

        private void OnGetAlive(FightActor fighter)
        {
            if (fighter != Fighter)
                return;

            Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.FRAGMENTATION, 1), Fighter.Map.GetCell(283));
        }
    }

    [BrainIdentifier((int)MonsterIdEnum.AURORAIRE)]
    public class AuroraireBrain : Brain
    {
        public AuroraireBrain(AIFighter fighter)
            : base(fighter)
        {
            fighter.GetAlive += OnGetAlive;
        }

        private void OnGetAlive(FightActor fighter)
        {
            if (fighter != Fighter)
                return;

            Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.ZEITGEIST_5104, 1), Fighter.Cell);
        }
    }

    [BrainIdentifier(3856)]
    [BrainIdentifier(3854)]
    [BrainIdentifier(3857)]
    [BrainIdentifier(3855)]
    [BrainIdentifier(3858)]
    public class FraktalOleada : Brain
    {
        public FraktalOleada(AIFighter fighter)
            : base(fighter)
        {
            fighter.Fight.FightStarted += Fight_FightStarted;
            fighter.Fight.TurnStarted += OnTurnStarted;
        }

        private void OnTurnStarted(IFight obj, FightActor actor)
        {
            if (Fighter == actor)
            {
                if (!Fighter.HasState((int)SpellIdEnum.FRAGMENTATION))
                {
                    Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.FRAGMENTATION, 1), Fighter.Cell);

                }
            }
        }

        private void Fight_FightStarted(IFight obj)
        {
            Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.FRAGMENTATION, 1), Fighter.Cell); // Efecto pasivo.
            //Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.CTRL_ALT_RAGE_5034, 1), Fighter.Cell);
            //Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.CTRL_ALT_RAGE_5037, 1), Fighter.Cell);
            //Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.CTRL_ALT_RAGE_5038, 1), Fighter.Cell);
            //Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.CTRL_ALT_RAGE_5041, 1), Fighter.Cell);
            //Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.CTRL_ALT_RAGE_5044, 1), Fighter.Cell);
            //Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.CTRL_ALT_RAGE_5047, 1), Fighter.Cell);
        }
    }

}