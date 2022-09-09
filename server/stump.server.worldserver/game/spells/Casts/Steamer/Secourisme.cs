//using Stump.Core.Threading;
//using Stump.DofusProtocol.Enums;
//using Stump.Server.WorldServer.Database.World;
//using Stump.Server.WorldServer.Game.Actors.Fight;
//using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
//using Stump.Server.WorldServer.Game.Effects.Handlers.Spells;
//using Stump.Server.WorldServer.Game.Effects.Instances;
//using Stump.Server.WorldServer.Game.Fights;
//using Stump.Server.WorldServer.Game.Fights.Buffs;
//using Stump.Server.WorldServer.Game.Maps.Cells;
//using System.Collections.Generic;
//using System.Linq;

//namespace Stump.Server.WorldServer.Game.Spells.Casts
//{
//    [SpellCastHandler(3219)]
//    public class Secourisme : DefaultSpellCastHandler
//    {
//        public Secourisme(SpellCastInformations cast)
//            : base(cast)
//        {
//        }

//        public object TemporaryBoostStateEffect { get; private set; }

//        public override void Execute()
//        {
//            if (!this.m_initialized)
//            {
//                this.Initialize();
//            }

//            List<SpellEffectHandler> effects = base.Handlers.ToList();


//            Handlers = effects.ToArray();

//            var Target = Fight.GetOneFighter(TargetedCell);
//            if (Target != null)
//            {
                
//                var Secourisme = SpellManager.Instance.GetSpellState(131);
//                var Buff = new StateBuff(Target.PopNextBuffId(), Target, Caster, new EffectDice(base.Handlers[1].Dice.EffectId, base.Handlers[1].Dice.Value, base.Handlers[1].Dice.DiceNum, base.Handlers[1].Dice.DiceFace), Spell, FightDispellableEnum.NOT_DISPELLABLE, Secourisme, Target);
//                Buff.Duration = 1;
//                Target.AddBuff(Buff);
//            }

//            base.Execute();


//        }
//    }
//}