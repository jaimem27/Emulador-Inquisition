using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stump.DofusProtocol.Enums;
using Stump.Server.WorldServer.Game.Effects;
using Stump.Server.WorldServer.Game.Effects.Handlers.Spells.Buffs;
using Stump.Server.WorldServer.Game.Effects.Instances;
using Stump.Server.WorldServer.Game.Fights;

namespace Stump.Server.WorldServer.Game.Spells.Casts.Sacrie {
    [SpellCastHandler (SpellIdEnum.VIOLENCE_9633)]
    public class Violence : DefaultSpellCastHandler {
        public Violence (SpellCastInformations cast) : base (cast) { }

        public override void Execute () {
            if (!m_initialized)
                Initialize ();

            foreach (var handler in Handlers) {
                handler.Apply ();
            }
        }
    }
}