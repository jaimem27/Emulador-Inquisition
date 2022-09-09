using Stump.DofusProtocol.Enums;
using Stump.Server.WorldServer.Game.Actors.Fight;
using Stump.Server.WorldServer.Game.Fights;
using System.Collections.Generic;
using System.Linq;

namespace Stump.Server.WorldServer.Game.Spells.Casts.Xelor
{
    [SpellCastHandler(SpellIdEnum.PENDULUM_9901)]
    public class Pendule : DefaultSpellCastHandler
    {
        public Pendule(SpellCastInformations cast)
            : base(cast)
        {
        }

        public override void Execute ()
        {
            if (!m_initialized)
                Initialize ();
            Handlers[0].Apply();
            ExecutionPlan.Delay(1000, () => { Handlers[3].Apply(); });
            Handlers[1].Apply();
            Handlers[2].Apply();
            Handlers[4].Apply();
        }
    }
}
