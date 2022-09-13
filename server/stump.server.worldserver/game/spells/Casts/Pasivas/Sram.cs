using Stump.DofusProtocol.Enums;
using Stump.Server.WorldServer.Game.Actors.Fight;
using Stump.Server.WorldServer.Game.Effects;
using Stump.Server.WorldServer.Game.Effects.Handlers.Spells;
using Stump.Server.WorldServer.Game.Effects.Instances;
using Stump.Server.WorldServer.Game.Fights;
using Stump.Server.WorldServer.Game.Items.Player.Custom;
using System.Collections.Generic;

namespace Stump.Server.WorldServer.Game.Spells.Casts
{
    [SpellCastHandler(12679)]
    public class SramPasiva : DefaultSpellCastHandler
    {
        public SramPasiva(SpellCastInformations cast)
            : base(cast)
        {
        }

        public override void Execute()
        {
            if (!base.Initialize())
                Initialize();


            EffectDice dice = new EffectDice(EffectsEnum.Effect_SpellCriticalPercent, 100, 100, 100);
            var hand = EffectManager.Instance.GetSpellEffectHandler(dice, Caster, this, Caster.Cell, false);

            EffectDice dice1 = new EffectDice(EffectsEnum.Effect_AddCriticalDamageBonus, 15, 15, 15);
            var hand1 = EffectManager.Instance.GetSpellEffectHandler(dice1, Caster, this, Caster.Cell, false);
            World.Instance.SendAnnounce("Pasiva a");
            hand1.Duration = 2;
            hand.Duration = 2;
            hand1.Apply();
            hand.Apply();


        }
    }

}