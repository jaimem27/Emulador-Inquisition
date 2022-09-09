using System.Linq;
using Stump.DofusProtocol.Enums;
using Stump.Server.WorldServer.Database.Items;
using Stump.Server.WorldServer.Game.Actors.Look;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;

namespace Stump.Server.WorldServer.Game.Items.Player.Custom {
    [ItemId (ItemIdEnum.BOUCLIER_DE_GUILDE_13240)]
    public class GuildShieldItem : BasePlayerItem {
        public GuildShieldItem (Character owner, PlayerItemRecord record) : base (owner, record) { }

        public override ActorLook UpdateItemSkin (ActorLook characterLook) {
            if (IsEquiped ()) {
                if (Owner.Guild != null) {
                    if (Owner.Guild.Alliance?.Id == null || Owner.Guild.Alliance?.Id < 1) {
                        characterLook.AddSkin (1730); //New ApparenceId                     

                        characterLook.AddSkin ((short) (Owner.Guild.Emblem.Template.SkinId)); //Emblem Skin                          
                        characterLook.AddColor (7, Owner.Guild.Emblem.BackgroundColor);
                        characterLook.AddColor (8, Owner.Guild.Emblem.SymbolColor);

                    } else {
                        characterLook.AddSkin (1730); //New ApparenceId

                        //  characterLook.AddSkin((short)Owner.Guild.Emblem.Template.SkinId); //Emblem Skin
                        characterLook.AddSkin ((short) (Owner.Guild.Alliance.Emblem.SymbolShape + 2569));

                        characterLook.AddSkin ((short) Owner.Guild.Emblem.Template.SkinId);
                        characterLook.AddColor (7, Owner.Guild.Emblem.BackgroundColor);
                        characterLook.AddColor (8, Owner.Guild.Emblem.SymbolColor);

                        characterLook.AddColor (9, Owner.Guild.Alliance.Emblem.BackgroundColor);
                        characterLook.AddColor (10, Owner.Guild.Alliance.Emblem.SymbolColor);

                    }
                }
            } else {
                characterLook.RemoveSkin (1730); //New ApparenceId

                if (Owner.Guild != null) {
                    if (Owner.Guild.Alliance?.Id == null || Owner.Guild.Alliance?.Id < 1) {

                        if (!Owner.Inventory.Any (entry => entry.Template.Id == 18525 && entry.IsEquiped ())) {
                            characterLook.RemoveSkin ((short) Owner.Guild.Emblem.Template.SkinId); //Emblem Skin

                            characterLook.RemoveColor (7);
                            characterLook.RemoveColor (8);
                        }
                    } else {

                        characterLook.RemoveSkin ((short) (Owner.Guild.Alliance.Emblem.SymbolShape + 2569)); //Emblem Skin
                        if (!Owner.Inventory.Any (entry => entry.Template.Id == 18525 && entry.IsEquiped ())) {
                            characterLook.RemoveSkin ((short) Owner.Guild.Emblem.Template.SkinId);
                            characterLook.RemoveColor (7);
                            characterLook.RemoveColor (8);
                        }
                        characterLook.RemoveColor (9);
                        characterLook.RemoveColor (10);
                    }
                }
            }
            return base.UpdateItemSkin (characterLook);
        }
    }

}