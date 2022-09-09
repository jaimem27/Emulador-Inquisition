using Stump.DofusProtocol.Enums;
using Stump.DofusProtocol.Messages;
using Stump.Server.WorldServer.Database.Items;
using Stump.Server.WorldServer.Database.World;
using Stump.Server.WorldServer.Game.Accounts;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using Stump.Server.WorldServer.Game.Effects.Instances;
using System.Drawing;

namespace Stump.Server.WorldServer.Game.Items.Player.Custom
{
    [ItemId(ItemIdEnum.POTION_DE_KERUBIM_13624)]
    public class VipPotion : BasePlayerItem
    {
        public VipPotion(Character owner, PlayerItemRecord record)
            : base(owner, record)
        {
        }

        public override uint UseItem(int amount = 1, Cell targetCell = null, Character target = null)
        {
            if(Owner.WorldAccount.Vip)
            {
                Owner.SendServerMessage("Ya eres VIP, no puedes serlo dos veces :D ...", Color.OrangeRed);
                return 0;
            }

            Owner.WorldAccount.Vip = true;
            AccountManager.Instance.UpdateVip(Owner.WorldAccount);

            Owner.AddOrnament(39);
            Owner.AddTitle(350);

            var item = Owner.Inventory.TryGetItem(ItemManager.Instance.TryGetTemplate(16147));
            if (Owner.WorldAccount.Vip && !Owner.Inventory.HasItem(ItemManager.Instance.TryGetTemplate(16147)))
            {
                item = Owner.Inventory.AddItem(ItemManager.Instance.TryGetTemplate(16147), 1);
                item.Effects.Add(new EffectInteger(EffectsEnum.Effect_NonExchangeable_981, 1));
                item.Invalidate();
                Owner.Inventory.RefreshItem(item);
            }

            if (item != null)
                Owner.Shortcuts.AddItemShortcut(19, item);

            World.Instance.SendAnnounce("<b>" + Owner.Name + "</b> ahora eres VIP, disfrutalo :D!", Color.AliceBlue);

            return 1;
        }
    }
}
