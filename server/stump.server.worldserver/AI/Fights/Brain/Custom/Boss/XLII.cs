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
    [BrainIdentifier(3849)]
    public class XLLII : Brain
    {
        public XLLII(AIFighter fighter)
            : base(fighter)
        {
            fighter.Fight.FightStarted += Fight_FightStarted;

        }

        private void Fight_FightStarted(IFight obj)
        {
            Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.CALL_MY_BUFF_5102, 1), Fighter.Cell);
            Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.CALL_MY_BUFF_5103, 1), Fighter.Cell);
            Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.CALL_MY_BUFF_5105, 1), Fighter.Cell);
            Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.CALL_MY_BUFF_5106, 1), Fighter.Cell);
        }
    }

    [BrainIdentifier(3850)]
    public class Invo : Brain
    {
        public Invo(AIFighter fighter)
            : base(fighter)
        {
            fighter.Fight.TurnStarted += OnTurnStarted;

        }

        private void OnTurnStarted(IFight obj, FightActor actor)
        {
            if (Fighter == actor)
            {

                //Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.ZEITGEIST_5104, 1), Fighter.Cell);

            }
            
        }
    }


    [BrainIdentifier(3844)]
    [BrainIdentifier(3843)]
    [BrainIdentifier(3842)]
    [BrainIdentifier(3841)]
    [BrainIdentifier(3840)]
    public class BichosOleada : Brain
    {
        public BichosOleada(AIFighter fighter)
            : base(fighter)
        {
            fighter.Fight.FightStarted += Fight_FightStarted;
            fighter.Fight.TurnStarted += OnTurnStarted;
        }

        private void OnTurnStarted(IFight obj, FightActor actor)
        {
            if (Fighter == actor)
            {
                if (!Fighter.HasState((int)SpellIdEnum.CTRL_ALT_RAGE))
                {
                    Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.CTRL_ALT_RAGE, 1), Fighter.Cell);

                }
            }
        }

        private void Fight_FightStarted(IFight obj)
        {
            Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.CTRL_ALT_RAGE, 1), Fighter.Cell); // Efecto pasivo.
            //Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.CTRL_ALT_RAGE_5034, 1), Fighter.Cell);
            //Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.CTRL_ALT_RAGE_5037, 1), Fighter.Cell);
            //Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.CTRL_ALT_RAGE_5038, 1), Fighter.Cell);
            //Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.CTRL_ALT_RAGE_5041, 1), Fighter.Cell);
            //Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.CTRL_ALT_RAGE_5044, 1), Fighter.Cell);
            //Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.CTRL_ALT_RAGE_5047, 1), Fighter.Cell);
        }
    }

}