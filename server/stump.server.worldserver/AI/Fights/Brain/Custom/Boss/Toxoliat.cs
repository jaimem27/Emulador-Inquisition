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
    [BrainIdentifier(3752)]
    public class Toxoliat : Brain
    {
        public Toxoliat(AIFighter fighter)
            : base(fighter)
        {
            fighter.Fight.FightStarted += Fight_FightStarted;

        }

        private void Fight_FightStarted(IFight obj)
        {
            //Fighter.Stats[PlayerFields.SummonLimit].Additional = 9999;
            Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.TOXMOSIS_4795, 1), Fighter.Cell);
            Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.TOXMOSIS_4796, 1), Fighter.Cell);
        }
    }

    [BrainIdentifier(3737)]
    public class Lucraneus : Brain
    {
        public Lucraneus(AIFighter fighter)
            : base(fighter)
        {
            fighter.Fight.FightStarted += Fight_FightStarted;
            fighter.Fight.TurnStarted += OnTurnStarted;
        }

        private void OnTurnStarted(IFight obj, FightActor actor)
        {
            if (Fighter == actor)
            {
                if (!Fighter.HasState((int)SpellIdEnum.SHIELDED_SHELL))
                {
                    Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.SHIELDED_SHELL, 1), Fighter.Cell);
                    Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.SHIELDED_SHELL_4744, 1), Fighter.Cell);
                    Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.SHIELDED_SHELL_4745, 1), Fighter.Cell);

                }
            }
        }

        private void Fight_FightStarted(IFight obj)
        {
            //Fighter.Stats[PlayerFields.SummonLimit].Additional = 9999;
            Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.SHIELDED_SHELL, 1), Fighter.Cell);
            Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.SHIELDED_SHELL_4744, 1), Fighter.Cell);
            Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.SHIELDED_SHELL_4745, 1), Fighter.Cell);
            //Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.TOXMOSIS_4796, 1), Fighter.Cell);
        }
    }

}