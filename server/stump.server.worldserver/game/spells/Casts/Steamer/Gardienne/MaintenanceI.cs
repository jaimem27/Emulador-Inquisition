using Stump.Server.WorldServer.Game.Effects.Handlers.Spells;
using Stump.Server.WorldServer.Game.Fights;
using System.Collections.Generic;
using System.Linq;

namespace Stump.Server.WorldServer.Game.Spells.Casts
{
    [SpellCastHandler(3241)]
    public class MaintenanceIHandler : DefaultSpellCastHandler
    {
        public MaintenanceIHandler(SpellCastInformations cast)
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
                effects.RemoveAt(1);
                effects.RemoveAt(2);
                Handlers = effects.ToArray();
                base.Execute();
            }
            catch (System.Exception)
            {
                
                
            }
          
        }
    }
}