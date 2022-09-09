using System;
using System.Collections.Generic;
using Stump.Core.Reflection;
using Stump.DofusProtocol.Enums;
using Stump.Server.WorldServer.Game.Actors.Fight;
using Stump.Server.WorldServer.Game.Effects;
using Stump.Server.WorldServer.Game.Effects.Handlers.Spells;
using Stump.Server.WorldServer.Game.Effects.Handlers.Spells.Damage;
using Stump.Server.WorldServer.Game.Fights;
using Stump.Server.WorldServer.Game.Fights.Buffs;

namespace Stump.Server.WorldServer.Game.Spells.Casts.Osamodas
{
    [SpellCastHandler (SpellIdEnum.REKOP_114)]
    [SpellCastHandler (SpellIdEnum.DOPPLESQUE_REKOP)]
    public class Rekop : DefaultSpellCastHandler {
        public Rekop (SpellCastInformations cast) : base (cast) { }
        public int CastRound {
            get;
            set;
        }

        private void RekoPa (TriggerBuff buff, FightActor trigerrer, BuffTriggerType trigger, object token) {
            buff.EffectHandler.Apply ();
        }
        public override void Execute () {

            Handlers[0].Apply ();

            Handlers[1].Apply ();

            Handlers[2].Apply ();

            Handlers[3].Apply ();

            Handlers[4].Apply ();

            //Handlers[5].Duration = 0;
            //Handlers[5].Effect.Duration = 0;
            //Handlers[5].Apply ();

            //Handlers[6].Duration = 0;
            //Handlers[6].Effect.Duration = 0;
            //Handlers[6].Apply ();

            //Handlers[7].Duration = 0;
            //Handlers[7].Effect.Duration = 0;
            //Handlers[7].Apply ();

            //Handlers[8].Duration = 0;
            //Handlers[8].Effect.Duration = 0;
            //Handlers[8].Apply ();
        }
    }
    
    [SpellCastHandler(SpellIdEnum.TRICKERY_9340)]
    public class RekopVariantCastHandler : DefaultSpellCastHandler
    {
        public RekopVariantCastHandler(SpellCastInformations cast)
            : base(cast)
        {
        }

        public override bool Initialize()
        {
            var list = Critical ? SpellLevel.CriticalEffects : SpellLevel.Effects;
            var list2 = new List<SpellEffectHandler>();
            foreach (var current in list)
            {
                var spellEffectHandler = Singleton<EffectManager>.Instance.GetSpellEffectHandler(current, Caster, this, TargetedCell, Critical);
                if (MarkTrigger != null)
                    spellEffectHandler.MarkTrigger = MarkTrigger;
                list2.Add(spellEffectHandler);
            }
            Handlers = list2.ToArray();
            return true;
        }

        public override void Execute()
        {
            if (!m_initialized)
                Initialize();

            var Target = Fight.GetOneFighter(TargetedCell);

            var directDamage = Handlers[0] as DirectDamage;
            var directDamage2 = Handlers[1] as DirectDamage;
            var directDamage3 = Handlers[2] as DirectDamage;
            var directDamage4 = Handlers[3] as DirectDamage;
           
            var castRound = new Random().Next(0, 4);
            switch (castRound)
            {
                case 0:
                    Handlers[4].Duration = 0;
                    Handlers[4].Effect.Duration = 0;
                    Handlers[4].Apply();
                    directDamage.Apply();
                    break;
                case 1:
                    Handlers[6].Duration = 0;
                    Handlers[6].Effect.Duration = 0;
                    Handlers[6].Apply();
                    directDamage2.Apply();
                    Caster.RegainAP(1);
                    break;
                case 2:
                    Handlers[7].Duration = 0;
                    Handlers[7].Effect.Duration = 0;
                    Handlers[7].Apply();
                    directDamage3.Apply();
                    Caster.RegainAP(2);
                    break;
                case 3:
                    Handlers[5].Duration = 0;
                    Handlers[5].Effect.Duration = 0;
                    Handlers[5].Apply();
                    directDamage4.Apply();
                    Caster.RegainAP(3);
                    break;
            }
        }
    }
    
    [SpellCastHandler (SpellIdEnum.FELINE_SPIRIT_108)]
    public class EspirituFelino : DefaultSpellCastHandler {
        public EspirituFelino (SpellCastInformations cast) : base (cast) { }

        public override void Execute() {
            if (!m_initialized)
                Initialize();

            var s = Handlers;

            Handlers[0].Apply();
            Handlers[1].Effect.EffectId = EffectsEnum.Effect_SymetricPointTeleport;
            //Handlers[1].Effect.TargetMask = "c,*e7,*e8";
            Handlers[1].Apply();
        }
    }
}