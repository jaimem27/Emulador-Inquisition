using Stump.DofusProtocol.Enums;
using Stump.Server.WorldServer.Database.Items;
using Stump.Server.WorldServer.Game.Actors.Look;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;

namespace Stump.Server.WorldServer.Game.Items.Player.Custom {
    [ItemId (ItemIdEnum.BOUCLIER_DALLIANCE_14396)]
    public class AllianceShieldItem : BasePlayerItem {
        public AllianceShieldItem (Character owner, PlayerItemRecord record) : base (owner, record) { }

        public override ActorLook UpdateItemSkin (ActorLook characterLook) {
            if (IsEquiped ()) {
                if (Owner.Guild != null) {
                    if (Owner.Guild?.Alliance?.Id != null && Owner.Guild?.Alliance?.Id > 0) {
                        characterLook.AddSkin (2552); //New ApparenceId
                        characterLook.AddSkin ((short) (Owner.Guild.Alliance.Emblem.SymbolShape + 2569));
                        characterLook.AddSkin ((short) Owner.Guild.Emblem.Template.SkinId);
                        characterLook.AddColor (7, Owner.Guild.Emblem.BackgroundColor);
                        characterLook.AddColor (8, Owner.Guild.Emblem.SymbolColor);
                        characterLook.AddColor (9, Owner.Guild.Alliance.Emblem.BackgroundColor);
                        characterLook.AddColor (10, Owner.Guild.Alliance.Emblem.SymbolColor);

                    }
                }
            } else {
                characterLook.RemoveSkin (2552); //New ApparenceId

                if (Owner.Guild != null) {
                    if (Owner.Guild?.Alliance?.Id != null && Owner.Guild?.Alliance?.Id > 0) {

                        characterLook.RemoveSkin ((short) (Owner.Guild.Alliance.Emblem.SymbolShape + 2569)); //Emblem Skin
                        characterLook.RemoveSkin ((short) Owner.Guild.Emblem.Template.SkinId);
                        characterLook.RemoveColor (7);
                        characterLook.RemoveColor (8);
                        characterLook.RemoveColor (9);
                        characterLook.RemoveColor (10);

                    }
                }
            }
            return base.UpdateItemSkin (characterLook);
        }
    }
}