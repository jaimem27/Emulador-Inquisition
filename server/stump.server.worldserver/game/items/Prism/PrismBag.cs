using System.Collections.Generic;
using System.Linq;
using Stump.Core.Reflection;
using Stump.DofusProtocol.Types;
using Stump.Server.BaseServer;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using Stump.Server.WorldServer.Game.Items.Player;
using Stump.Server.WorldServer.Game.Prisms;
using Stump.Server.WorldServer.Handlers.Prism;

namespace Stump.Server.WorldServer.Game.Items.Prism {
    public class PrismBag : PersistantItemsCollection<PrismItem> {
        public PrismBag (PrismNpc npc) {
            Owner = npc;
        }

        public PrismNpc Owner { get; set; }
        public bool IsDirty { get; private set; }

        protected override void OnItemStackChanged (PrismItem item, int difference) {
            IsDirty = true;
            base.OnItemStackChanged (item, difference);
        }
        protected override void OnItemAdded (PrismItem item, bool sendMessage = true) {
            IsDirty = true;
            base.OnItemAdded (item, sendMessage);
        }

        protected override void OnItemRemoved (PrismItem item, bool sendMessage = true) {
            IsDirty = true;
            base.OnItemRemoved (item, sendMessage);
        }
        public bool MoveToInventory (BasePlayerItem item, Character character, uint quantity) {
            bool result;
            if (quantity != 1)
                result = false;
            else {
                if (item.Template.Id != 14552)
                    return false;
                if (Items.Values.FirstOrDefault (x => x.Template.Id == 14552) != null)
                    return false;
                character.Inventory.RemoveItem (item, 1);
                var itemt = ItemManager.Instance.CreatePrismItem (item, Owner.Record.Id);
                AddItem (itemt);
                WorldServer.Instance.IOTaskPool.AddMessage (() => Save (WorldServer.Instance.DBAccessor.Database));
                PrismHandler.SendPrismsListUpdateMessage (character.Client, character.Map.Prism, true);
                result = true;
            }
            return result;
        }

        public void LoadRecord () {
            //if (ServerBase<WorldServer>.Instance.IsInitialized)
            //    ServerBase<WorldServer>.Instance.IOTaskPool.EnsureContext();
            var source = Singleton<ItemManager>.Instance.FindPrismItems (Owner.Record.Id);
            Items = (from entry in source select new PrismItem (entry)).ToDictionary (entry => entry.Guid);
        }

        public IEnumerable<ObjectItem> GetObjectItems () {
            return from x in Items select x.Value.GetObjectItem ();
        }

        public void DeleteBag (bool lazySave = true) {
            DeleteAll (false);
            if (lazySave)
                WorldServer.Instance.IOTaskPool.AddMessage (() => Save (WorldServer.Instance.DBAccessor.Database));
            else
                Save (WorldServer.Instance.DBAccessor.Database);
        }

        public override void Save (ORM.Database database) {
            if (ServerBase<WorldServer>.Instance.IsInitialized)
                ServerBase<WorldServer>.Instance.IOTaskPool.EnsureContext ();
            base.Save (database);
            IsDirty = false;
        }
    }
}