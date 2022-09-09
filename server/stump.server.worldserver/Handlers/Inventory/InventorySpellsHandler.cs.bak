using System;
using System.Collections.Generic;
using System.Linq;
using Stump.DofusProtocol.Messages;
using Stump.DofusProtocol.Types;
using Stump.Server.BaseServer.Network;
using Stump.Server.WorldServer.Core.Network;
using Stump.Server.WorldServer.Database.Shortcuts;
using Stump.Server.WorldServer.Game.Spells;

namespace Stump.Server.WorldServer.Handlers.Inventory
{
    public partial class InventoryHandler
    {
        public static void SendSpellListMessage(WorldClient client, bool previsualization)
        {
            client.Send(new SpellListMessage(previsualization,
                client.Character.Spells.GetSpells().Select(
                    entry => entry.GetSpellItem())));
        }
        public static void SendGameRolePlayPlayerLifeStatusMessage(IPacketReceiver client, sbyte state, int phxMap)
        {
            client.Send(new GameRolePlayPlayerLifeStatusMessage(state, phxMap));
        }
    }
}