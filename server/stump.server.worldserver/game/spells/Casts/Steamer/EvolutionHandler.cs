/*using Stump.Server.WorldServer.Game.Effects.Handlers.Spells;
using Stump.Server.WorldServer.Game.Fights;
using System.Collections.Generic;
using System.Linq;

namespace Stump.Server.WorldServer.Game.Spells.Casts
{
    [SpellCastHandler(3215)]
    public class Evolution : DefaultSpellCastHandler
    {
        public Evolution(SpellCastInformations cast)
            : base(cast)
        {
        }

        public object TemporaryBoostStateEffect { get; private set; }

        public override void Execute()
        {
            if (!this.m_initialized)
            {
                this.Initialize();
            }

            List<SpellEffectHandler> effects = base.Handlers.ToList();

            effects.RemoveAt(1);

            Handlers = effects.ToArray();

            base.Execute();
        }
    }
}*/