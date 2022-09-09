using System.Collections.Generic;
using System.Linq;
using Stump.DofusProtocol.Types;
using Stump.Server.WorldServer.Database.Items.Prism;
using Stump.Server.WorldServer.Database.Items.Templates;
using Stump.Server.WorldServer.Game.Effects.Instances;

namespace Stump.Server.WorldServer.Game.Items.Prism {
    public class PrismItem : PersistantItem<PrismNpcItemRecord> {
        public PrismItem (PrismNpcItemRecord record) : base (record) { }

        public PrismItem (int owner, int guid, ItemTemplate template, List<EffectBase> effects, uint stack) {
            Record = new PrismNpcItemRecord {
                Id = guid,
                OwnerId = owner,
                Template = template,
                Stack = stack,
                Effects = effects,
                IsNew = true,
            };
        }

        public override ObjectItem GetObjectItem () {
            return new ObjectItem (63, (ushort) Template.Id,
                (from x in Effects select x.GetObjectEffect ()).ToArray(), (uint) Guid, (uint) Stack);
        }
    }
}