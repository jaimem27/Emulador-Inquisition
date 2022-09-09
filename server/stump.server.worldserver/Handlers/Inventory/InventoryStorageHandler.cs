using Stump.DofusProtocol.Messages;
using Stump.Server.BaseServer.Network;
using Stump.Server.WorldServer.Core.Network;
using Stump.Server.WorldServer.Game.Actors.RolePlay.TaxCollectors;
using Stump.Server.WorldServer.Game.Formulas;
using Stump.Server.WorldServer.Game.Items;
using Stump.Server.WorldServer.Game.Items.Player;
using System.Collections.Generic;
using System.Linq;

namespace Stump.Server.WorldServer.Handlers.Inventory
{
    public partial class InventoryHandler : WorldHandlerContainer
    {
        public static void SendStorageInventoryContentMessage(IPacketReceiver client, TaxCollectorNpc taxCollector)
        {
            client.Send(taxCollector.GetStorageInventoryContent());
        }

        public static void SendStorageInventoryContentMessage(IPacketReceiver client, Bank bank)
        {
            var itemPrices = new Dictionary<ushort, ulong>();
            foreach (var item in bank)
                if (!itemPrices.ContainsKey(item.GetObjectItem().ObjectGID))
                    itemPrices.Add(item.GetObjectItem().ObjectGID, PriceFormulas.getItemPrice(item.Template.Id));
            foreach (var item in ((WorldClient)client).Character.Inventory)
                if (!itemPrices.ContainsKey(item.GetObjectItem().ObjectGID))
                    itemPrices.Add(item.GetObjectItem().ObjectGID, PriceFormulas.getItemPrice(item.Template.Id));
            client.Send(new ObjectAveragePricesMessage(itemPrices.Keys.ToArray(), itemPrices.Values.ToArray()));
            client.Send(new StorageInventoryContentMessage(bank.Select(x => x.GetObjectItem()).ToArray(), bank.Kamas));
        }

        public static void SendStorageKamasUpdateMessage(IPacketReceiver client, ulong kamas)
        {
            client.Send(new StorageKamasUpdateMessage(kamas));
        }

        public static void SendStorageObjectRemoveMessage(IPacketReceiver client, IItem item)
        {
            client.Send(new StorageObjectRemoveMessage((uint)item.Guid));
        }

        public static void SendStorageObjectUpdateMessage(IPacketReceiver client, IItem item)
        {
            client.Send(new StorageObjectUpdateMessage(item.GetObjectItem()));
        }

        public static void SendStorageObjectsRemoveMessage(IPacketReceiver client, IEnumerable<int> guids)
        {
            client.Send(new StorageObjectsRemoveMessage(guids.Select(x => (uint)x).ToArray()));
        }

        public static void SendStorageObjectsUpdateMessage(IPacketReceiver client, IEnumerable<IItem> items)
        {
            client.Send(new StorageObjectsUpdateMessage(items.Select(x => x.GetObjectItem()).ToArray()));
        }
    }
}