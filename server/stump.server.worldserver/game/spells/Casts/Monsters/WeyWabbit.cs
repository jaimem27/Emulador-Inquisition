using Stump.Core.Extensions;
using Stump.Core.Reflection;
using Stump.DofusProtocol.Enums;
using Stump.Server.WorldServer.Game.Actors.Fight;
using Stump.Server.WorldServer.Game.Effects;
using Stump.Server.WorldServer.Game.Effects.Handlers.Spells;
using Stump.Server.WorldServer.Game.Effects.Instances;
using Stump.Server.WorldServer.Game.Fights;
using Stump.Server.WorldServer.Game.Fights.Buffs;
using System.Collections.Generic;
using System.Linq;

namespace Stump.Server.WorldServer.Game.Spells.Casts.Monsters
{
    [SpellCastHandler(SpellIdEnum.SUBSTITUTION)]
    public class WeyWabbit : DefaultSpellCastHandler
    {
        public WeyWabbit(SpellCastInformations cast)
            : base(cast)
        {
        }

        public override void Execute()
        {
            if (!m_initialized)
                Initialize();


            base.Execute();
        }

    }

    [SpellCastHandler(SpellIdEnum.TWANSMUTATION)]
    public class Twansmutacion : DefaultSpellCastHandler
    {
        public Twansmutacion(SpellCastInformations cast)
            : base(cast)
        {
        }

        public override void Execute()
        {
            if (!m_initialized)
                Initialize();



            base.Execute();
        }
    }

}
