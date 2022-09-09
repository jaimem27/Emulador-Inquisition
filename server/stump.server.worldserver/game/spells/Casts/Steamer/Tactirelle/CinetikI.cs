using Stump.DofusProtocol.Enums;
using Stump.Server.WorldServer.Game.Effects.Handlers.Spells;
using Stump.Server.WorldServer.Game.Fights;
using System.Collections.Generic;
using System.Linq;

namespace Stump.Server.WorldServer.Game.Spells.Casts
{
    [SpellCastHandler(3233)]
    public class CinetikIHandler : DefaultSpellCastHandler
    {
        public CinetikIHandler(SpellCastInformations cast)
            : base(cast)
        {
        }

        public object TemporaryBoostStateEffect { get; private set; }
        public override void Execute()
        {
            try
            {
                if (!this.m_initialized)
                {
                    this.Initialize();
                }
                List<SpellEffectHandler> effects = base.Handlers.ToList();
                //effects[0].Targets = SpellTargetType.ENEMY_ALL;
                //effects[1].Targets = SpellTargetType.ALL;
                Handlers = effects.ToArray();
                base.Execute();
            }
            catch (System.Exception)
            {
                
               
            }
           
        }
    }
}