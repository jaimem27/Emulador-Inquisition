using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stump.DofusProtocol.Enums;
using Stump.Server.WorldServer.Game.Effects;
using Stump.Server.WorldServer.Game.Effects.Handlers.Spells.Armor;
using Stump.Server.WorldServer.Game.Effects.Handlers.Spells.Buffs;
using Stump.Server.WorldServer.Game.Effects.Handlers.Spells.Others;
using Stump.Server.WorldServer.Game.Effects.Handlers.Spells.States;
using Stump.Server.WorldServer.Game.Effects.Instances;
using Stump.Server.WorldServer.Game.Fights;
using Stump.Server.WorldServer.Game.Fights.Buffs;

namespace Stump.Server.WorldServer.Game.Spells.Casts.Sacrie {
    [SpellCastHandler (SpellIdEnum.ROBUST_9946)]
    public class Robusto : DefaultSpellCastHandler {
        public Robusto (SpellCastInformations cast) : base (cast) { }

        public override void Execute () {
            if (!m_initialized)
                Initialize ();
            Caster.RemoveSpellBuffs((int)SpellIdEnum.ROBUST_9946);

            foreach (var handler in Handlers) {
                if (Caster.HasState(626))
                    handler.Apply();
                if (Caster.HasState(627))
                    handler.Apply();
                if (Caster.HasState(628))
                    handler.Apply();
                if (Caster.HasState(629))
                    handler.Apply();
                if (Caster.HasState(630))
                    handler.Apply();
                if (Caster.HasState(631))
                    handler.Apply();
                if (Caster.HasState(632))
                    handler.Apply();
                if (Caster.HasState(633))
                    handler.Apply();
                if (Caster.HasState(634))
                    handler.Apply();
                if (Caster.HasState(635))
                    handler.Apply();
            }
        }
    }

    [SpellCastHandler (SpellIdEnum.BLOODTHIRSTY_9944)]
    public class Sanguinario : DefaultSpellCastHandler {
        public Sanguinario (SpellCastInformations cast) : base (cast) { }

        public override void Execute () {
            if (!m_initialized)
                Initialize ();

            Caster.RemoveSpellBuffs((int)SpellIdEnum.BLOODTHIRSTY_9944);

            foreach (var handler in Handlers) {
                if (Caster.HasState(616))
                    handler.Apply();
                if (Caster.HasState(617))
                    handler.Apply();
                if (Caster.HasState(618))
                    handler.Apply();
                if (Caster.HasState(619))
                    handler.Apply();
                if (Caster.HasState(620))
                    handler.Apply();
                if (Caster.HasState(621))
                    handler.Apply();
                if (Caster.HasState(622))
                    handler.Apply();
                if (Caster.HasState(623))
                    handler.Apply();
                if (Caster.HasState(624))
                    handler.Apply();
                if (Caster.HasState(625))
                    handler.Apply();
            }
        }
    }

    [SpellCastHandler(SpellIdEnum.COAGULATION_10002)]
    public class Coagulation : DefaultSpellCastHandler
    {
        public Coagulation(SpellCastInformations cast) : base(cast) { }

        public override void Execute()
        {
            if (!m_initialized)
                Initialize();

            var h = Handlers;
            short shieldAmount = 0; 
            var quantity = (short)(Caster.Level * 1.5);
            if (Caster.HasState(626))
                shieldAmount = (short)(quantity);
            if (Caster.HasState(627))
                shieldAmount = (short)(quantity * 2);
            if (Caster.HasState(628))
                shieldAmount = (short)(quantity * 3);
            if (Caster.HasState(629))
                shieldAmount = (short)(quantity * 4);
            if (Caster.HasState(630))
                shieldAmount = (short)(quantity * 5);

            var effect = new EffectDice(EffectsEnum.Effect_AddShieldPercent, shieldAmount, shieldAmount, shieldAmount);
            //var buff = new GiveShieldPercent(effect, Caster, this, Caster.Cell, false);
            //buff.Duration = 2;
            

            //buff.Apply();

            var actorBuffId = Caster.PopNextBuffId();
            var handler = EffectManager.Instance.GetSpellEffectHandler(effect, Caster, this, TargetedCell, Critical);

            var buff2 = new StatBuff(actorBuffId, Caster, Caster, handler, Spell, shieldAmount, PlayerFields.Shield, false, FightDispellableEnum.DISPELLABLE_BY_DEATH, Caster);
            buff2.Duration = 2;
            buff2.Value = shieldAmount;
            buff2.Dice.Value = shieldAmount;
            buff2.Dice.DiceNum = shieldAmount;

            try
            {
                foreach ( var item in Caster.GetBuffs()){
               
                    if (item.Effect.EffectId == EffectsEnum.Effect_AddShieldPercent)
                        Caster.RemoveBuff(item);
                }
            }
            catch { }

            if (shieldAmount > 0)
                Caster.AddBuff(buff2);
            //var actorBuffId = Caster.PopNextBuffId();

            //var addStateHandler = new GiveShieldPercent(new EffectDice(EffectsEnum.Effect_AddShieldPercent, shieldAmount, shieldAmount, shieldAmount), Caster, this, Caster.Cell, false);

            //var actorBuff = new StateBuff(actorBuffId, Caster, Caster, addStateHandler,
            //    Spell, FightDispellableEnum.DISPELLABLE_BY_DEATH, SpellManager.Instance.GetSpellState((uint)SpellStatesEnum.COAGULATION_10002))
            //{
            //    Duration = 2
            //};

            //Caster.AddBuff(actorBuff, true);
            base.Execute();


        }
    }
    
    [SpellCastHandler(SpellIdEnum.MOTIVATING_PAIN)]
    public class DolorMotivador : DefaultSpellCastHandler
    {
        public DolorMotivador(SpellCastInformations cast) : base(cast) { }

        public override void Execute()
        {
            if (!m_initialized)
                Initialize();

           
            base.Execute();
        }
    }
    
    [SpellCastHandler(SpellIdEnum.SUFFERING_EVOLUTION)]
    public class SufferingEvolution : DefaultSpellCastHandler
    {
        public SufferingEvolution(SpellCastInformations cast) : base(cast) { }

        public override void Execute()
        {
            if (!m_initialized)
                Initialize();

            /*var handlers = Handlers.OrderBy(x => x.Priority);
            foreach (var hadler in handlers.OrderBy(x => x.Priority))
            {
                if ((hadler is CastSpellEffect) && hadler.Dice.DiceNum == 9944)
                    hadler.Apply();
                if ((hadler is CastSpellEffect) && hadler.Dice.DiceNum == 9946)
                    hadler.Apply();
                //if (!(hadler is CastSpellEffect))
                    //hadler.Apply();
            }*/
            base.Execute();
        }
    }

    [SpellCastHandler(SpellIdEnum.PERFUSION_10001)]
    public class Perfusion : DefaultSpellCastHandler
    {
        public Perfusion(SpellCastInformations cast) : base(cast) { }

        public override void Execute()
        {
            if (!m_initialized)
                Initialize();

            var h = Handlers;
            
            base.Execute();
        }
    }
}