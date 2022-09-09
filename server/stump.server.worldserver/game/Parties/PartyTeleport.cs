using Stump.DofusProtocol.Messages;
using Stump.Server.WorldServer.Core.Network;
using Stump.Server.WorldServer.Game.Maps;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Stump.Server.WorldServer.Game.Parties
{
    public class PartyTeleport
    {
        public PartyTeleport(short dungeonId, int inviterId, IEnumerable<int> invalidIds, Party party, Map mapId, int cellId)
        {
            DungeonId = dungeonId;
            InviterId = inviterId;
            InvalidersIds = invalidIds;
            Party = party;
            Map = mapId;
            TeleportCellId = cellId;
        }

        public Party Party
        {
            get;
            set;
        }

        public short DungeonId
        {
            get;
            set;
        }

        public int InviterId
        {
            get;
            set;
        }

        public int TeleportCellId
        {
            get;
            set;
        }

        public Map Map
        {
            get;
            set;
        }

        public IEnumerable<int> InvalidersIds
        {
            get;
            set;
        }

        public void Open()
        {
            Party.Members.ToClients().Send(new TeleportBuddiesRequestedMessage(DungeonId, InviterId, InvalidersIds.Select(i => (long)i).ToList()));
            Party.Members.Where(x => x.Id != InviterId && !InvalidersIds.Contains(x.Id)).ToClients().Send(new TeleportToBuddyOfferMessage((ushort)DungeonId, (ulong)InviterId, 900));
            foreach (var member in Party.Members)
                member.LastTeleportParty = DateTime.Now + TimeSpan.FromMinutes(3);
        }

        public void Accept(WorldClient client)
        {
            if (!Party.Members.Contains(client.Character))
                return;
            if (client.Character.IsInFight())
                return;
            if (client.Character.IsInExchange())
                return;

            client.Character.Teleport(Map, Map.GetCell(TeleportCellId));
            client.Send(new TeleportToBuddyCloseMessage((ushort)DungeonId, (ulong)InviterId));

        }

        public void Deny(WorldClient client)
        {
            if (!Party.Members.Contains(client.Character))
                return;
            client.Send(new TeleportToBuddyCloseMessage((ushort)DungeonId, (ulong)InviterId));
        }
    }
}
