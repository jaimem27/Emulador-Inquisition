using System.Linq;
using Stump.Core.Reflection;
using Stump.DofusProtocol.Enums;
using Stump.Server.WorldServer.Database.World;
using Stump.Server.WorldServer.Game.Actors.Fight;
using Stump.Server.WorldServer.Game.Effects;
using Stump.Server.WorldServer.Game.Effects.Handlers.Spells;
using Stump.Server.WorldServer.Game.Effects.Handlers.Spells.Move;
using Stump.Server.WorldServer.Game.Effects.Handlers.Spells.States;
using Stump.Server.WorldServer.Game.Effects.Instances;
using Stump.Server.WorldServer.Game.Fights;
using Stump.Server.WorldServer.Game.Fights.Buffs;

namespace Stump.Server.WorldServer.Game.Spells.Casts.Roublard
{
    [SpellCastHandler(SpellIdEnum.REMISSION_2809)]
    public class Remision : DefaultSpellCastHandler
    {
        public Remision(SpellCastInformations cast) : base(cast)
        {
        }
        private void RepelsTo (TriggerBuff buff, FightActor trigerrer, BuffTriggerType trigger, object token) 
        {
            EffectDice repel = new EffectDice ((short) EffectsEnum.Effect_PushBack, buff.EffectHandler.Dice.Value, buff.EffectHandler.Dice.DiceFace, buff.EffectHandler.Dice.DiceFace, new EffectBase ());
            SpellEffectHandler spellEffectHandler = Singleton<EffectManager>.Instance.GetSpellEffectHandler (repel, buff.Caster, this, base.CastCell, Critical);
            spellEffectHandler.Apply ();
        }

        public override void Execute()
        {
            var hadlers = Handlers;
            var targer = Handlers[1].GetAffectedActors().First(); // Error

            if (targer == null)
                return;
            Handlers[0].AddTriggerBuff(targer, BuffTriggerType.OnDamagedInCloseRange, RepelsTo);
            base.Execute();
        }
    }
}