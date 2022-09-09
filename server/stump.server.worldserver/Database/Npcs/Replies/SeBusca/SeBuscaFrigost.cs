using Stump.DofusProtocol.Enums;
using Stump.Server.BaseServer.Database;
using Stump.Core.Reflection;
using Stump.Server.WorldServer.Database.Npcs;
using Stump.Server.WorldServer.Database.Npcs.Replies;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Npcs;
using Stump.Server.WorldServer.Game;
using System.Drawing;
using Stump.Server.WorldServer.Game.Items;

namespace Database.Npcs.Replies
{
    [Discriminator("SeBuscaFrigost", typeof(NpcReply), new System.Type[]
 {
  typeof(NpcReplyRecord)
 })]
    class SeBuscaFrigost : NpcReply
    {
        public SeBuscaFrigost(NpcReplyRecord record)
            : base(record)
        {
        }
        public override bool Execute(Npc npc, Character character)
        {
            bool entregado = false;

           
            #region Jalalut
            var seBuscaItemJalalut = Singleton<ItemManager>.Instance.TryGetTemplate(6838);
            var Jalalut = character.Inventory.TryGetItem(seBuscaItemJalalut);

            if (Jalalut != null)
            {

                string annonce = character.Name + " ha entregado a Jalalut a las autoridades.";

                character.SendServerMessage("Has ganado 230000 kamas.");
                character.SendServerMessage("Has ganado 600000 puntos de experencia.");
                character.SendServerMessage("Has ganado 960 doplones.");

                var doplones = ItemManager.Instance.CreatePlayerItem(character, 13052, 960, true);

                character.Inventory.AddKamas(230000);
                character.Inventory.AddItem(doplones);
                character.AddExperience(600000);
                character.Inventory.RemoveItem(Jalalut, 1);
                Singleton<World>.Instance.SendAnnounce(annonce, Color.Red);
                entregado = true;
            }
            #endregion

            #region Elpin Güino
            var seBuscaItemElpin = Singleton<ItemManager>.Instance.TryGetTemplate(6846);
            var Elpin = character.Inventory.TryGetItem(seBuscaItemElpin);

            if (Elpin != null)
            {

                string annonce = character.Name + " ha entregado a Elpin Güino a las autoridades.";

                character.SendServerMessage("Has ganado 260000 kamas.");
                character.SendServerMessage("Has ganado 650000 puntos de experencia.");
                character.SendServerMessage("Has ganado 1080 doplones.");

                var doplones = ItemManager.Instance.CreatePlayerItem(character, 13052, 1080, true);

                character.Inventory.AddKamas(260000);
                character.Inventory.AddItem(doplones);
                character.AddExperience(650000);
                character.Inventory.RemoveItem(Elpin, 1);
                Singleton<World>.Instance.SendAnnounce(annonce, Color.Red);
                entregado = true;
            }
            #endregion

            #region Katigrú
            var seBuscaItemKatigru = Singleton<ItemManager>.Instance.TryGetTemplate(6853);
            var Katigru = character.Inventory.TryGetItem(seBuscaItemKatigru);

            if (Katigru != null)
            {

                string annonce = character.Name + " ha entregado a Katigrú a las autoridades.";

                character.SendServerMessage("Has ganado 290000 kamas.");
                character.SendServerMessage("Has ganado 700000 puntos de experencia.");
                character.SendServerMessage("Has ganado 1200 doplones.");

                var doplones = ItemManager.Instance.CreatePlayerItem(character, 13052, 1200, true);

                character.Inventory.AddKamas(290000);
                character.Inventory.AddItem(doplones);
                character.AddExperience(700000);
                character.Inventory.RemoveItem(Katigru, 1);
                Singleton<World>.Instance.SendAnnounce(annonce, Color.Red);
                entregado = true;
            }
            #endregion

            #region Phantomeyt
            var seBuscaItemPhantomeyt = Singleton<ItemManager>.Instance.TryGetTemplate(6858);
            var Phantomeyt = character.Inventory.TryGetItem(seBuscaItemPhantomeyt);

            if (Phantomeyt != null)
            {

                string annonce = character.Name + " ha entregado a Phantomeyt a las autoridades.";

                character.SendServerMessage("Has ganado 320000 kamas.");
                character.SendServerMessage("Has ganado 750000 puntos de experencia.");
                character.SendServerMessage("Has ganado 1320 doplones.");

                var doplones = ItemManager.Instance.CreatePlayerItem(character, 13052, 1320, true);

                character.Inventory.AddKamas(320000);
                character.Inventory.AddItem(doplones);
                character.AddExperience(750000);
                character.Inventory.RemoveItem(Phantomeyt, 1);
                Singleton<World>.Instance.SendAnnounce(annonce, Color.Red);
                entregado = true;
            }
            #endregion

            #region Vengadora Enmascarada
            var seBuscaItemVengadora = Singleton<ItemManager>.Instance.TryGetTemplate(8576);
            var Vengadora = character.Inventory.TryGetItem(seBuscaItemVengadora);

            if (Vengadora != null)
            {

                string annonce = character.Name + " ha entregado a Vengadora Enmascarada a las autoridades.";

                character.SendServerMessage("Has ganado 350000 kamas.");
                character.SendServerMessage("Has ganado 800000 puntos de experencia.");
                character.SendServerMessage("Has ganado 1440 doplones.");

                var doplones = ItemManager.Instance.CreatePlayerItem(character, 13052, 1440, true);

                character.Inventory.AddKamas(350000);
                character.Inventory.AddItem(doplones);
                character.AddExperience(800000);
                character.Inventory.RemoveItem(Vengadora, 1);
                Singleton<World>.Instance.SendAnnounce(annonce, Color.Red);
                entregado = true;
            }
            #endregion

            #region YeCh'Ti
            var seBuscaItemYeCh = Singleton<ItemManager>.Instance.TryGetTemplate(8588);
            var YeCh = character.Inventory.TryGetItem(seBuscaItemYeCh);

            if (YeCh != null)
            {

                string annonce = character.Name + " ha entregado a YeCh'Ti a las autoridades.";

                character.SendServerMessage("Has ganado 380000 kamas.");
                character.SendServerMessage("Has ganado 850000 puntos de experencia.");
                character.SendServerMessage("Has ganado 1560 doplones.");

                var doplones = ItemManager.Instance.CreatePlayerItem(character, 13052, 1560, true);

                character.Inventory.AddKamas(380000);
                character.Inventory.AddItem(doplones);
                character.AddExperience(850000);
                character.Inventory.RemoveItem(YeCh, 1);
                Singleton<World>.Instance.SendAnnounce(annonce, Color.Red);
                entregado = true;
            }
            #endregion

            #region Fuji Gélifux
            var seBuscaItemFuji = Singleton<ItemManager>.Instance.TryGetTemplate(8590);
            var Fuji = character.Inventory.TryGetItem(seBuscaItemFuji);

            if (Fuji != null)
            {

                string annonce = character.Name + " ha entregado a Fuji Gélifux a las autoridades.";

                character.SendServerMessage("Has ganado 410000 kamas.");
                character.SendServerMessage("Has ganado 850000 puntos de experencia.");
                character.SendServerMessage("Has ganado 1560 doplones.");

                var doplones = ItemManager.Instance.CreatePlayerItem(character, 13052, 1560, true);

                character.Inventory.AddKamas(410000);
                character.Inventory.AddItem(doplones);
                character.AddExperience(850000);
                character.Inventory.RemoveItem(Fuji, 1);
                Singleton<World>.Instance.SendAnnounce(annonce, Color.Red);
                entregado = true;
            }
            #endregion

            #region Dremoan
            var seBuscaItemDremoan = Singleton<ItemManager>.Instance.TryGetTemplate(8595);
            var Dremoan = character.Inventory.TryGetItem(seBuscaItemDremoan);

            if (Dremoan != null)
            {

                string annonce = character.Name + " ha entregado a Dremoan a las autoridades.";

                character.SendServerMessage("Has ganado 410000 kamas.");
                character.SendServerMessage("Has ganado 900000 puntos de experencia.");
                character.SendServerMessage("Has ganado 1680 doplones.");

                var doplones = ItemManager.Instance.CreatePlayerItem(character, 13052, 1680, true);

                character.Inventory.AddKamas(410000);
                character.Inventory.AddItem(doplones);
                character.AddExperience(900000);
                character.Inventory.RemoveItem(Dremoan, 1);
                Singleton<World>.Instance.SendAnnounce(annonce, Color.Red);
                entregado = true;
            }
            #endregion

            #region Tejash
            var seBuscaItemTejash = Singleton<ItemManager>.Instance.TryGetTemplate(8603);
            var Tejash = character.Inventory.TryGetItem(seBuscaItemTejash);

            if (Tejash != null)
            {

                string annonce = character.Name + " ha entregado a Tejash a las autoridades.";

                character.SendServerMessage("Has ganado 410000 kamas.");
                character.SendServerMessage("Has ganado 950000 puntos de experencia.");
                character.SendServerMessage("Has ganado 1860 doplones.");

                var doplones = ItemManager.Instance.CreatePlayerItem(character, 13052, 1860, true);

                character.Inventory.AddKamas(410000);
                character.Inventory.AddItem(doplones);
                character.AddExperience(950000);
                character.Inventory.RemoveItem(Tejash, 1);
                Singleton<World>.Instance.SendAnnounce(annonce, Color.Red);
                entregado = true;
            }
            #endregion

            #region Huini Golosote
            var seBuscaItemHuini = Singleton<ItemManager>.Instance.TryGetTemplate(8606);
            var Huini = character.Inventory.TryGetItem(seBuscaItemHuini);

            if (Huini != null)
            {

                string annonce = character.Name + " ha entregado a Huini Golosote a las autoridades.";

                character.SendServerMessage("Has ganado 410000 kamas.");
                character.SendServerMessage("Has ganado 950000 puntos de experencia.");
                character.SendServerMessage("Has ganado 1860 doplones.");

                var doplones = ItemManager.Instance.CreatePlayerItem(character, 13052, 1860, true);

                character.Inventory.AddKamas(410000);
                character.Inventory.AddItem(doplones);
                character.AddExperience(950000);
                character.Inventory.RemoveItem(Huini, 1);
                Singleton<World>.Instance.SendAnnounce(annonce, Color.Red);
                entregado = true;
            }
            #endregion

            #region Doctor Eggob
            var seBuscaItemDoctor = Singleton<ItemManager>.Instance.TryGetTemplate(8692);
            var Doctor = character.Inventory.TryGetItem(seBuscaItemDoctor);

            if (Doctor != null)
            {

                string annonce = character.Name + " ha entregado a Doctor Eggob a las autoridades.";

                character.SendServerMessage("Has ganado 598000 kamas.");
                character.SendServerMessage("Has ganado 950000 puntos de experencia.");
                character.SendServerMessage("Has ganado 1860 doplones.");

                var doplones = ItemManager.Instance.CreatePlayerItem(character, 13052, 1860, true);

                character.Inventory.AddKamas(598000);
                character.Inventory.AddItem(doplones);
                character.AddExperience(950000);
                character.Inventory.RemoveItem(Doctor, 1);
                Singleton<World>.Instance.SendAnnounce(annonce, Color.Red);
                entregado = true;
            }
            #endregion

            #region Golosorak
            var seBuscaItemGolosorak = Singleton<ItemManager>.Instance.TryGetTemplate(8915);
            var Golosorak = character.Inventory.TryGetItem(seBuscaItemGolosorak);

            if (Golosorak != null)
            {

                string annonce = character.Name + " ha entregado a Golosorak a las autoridades.";

                character.SendServerMessage("Has ganado 598000 kamas.");
                character.SendServerMessage("Has ganado 950000 puntos de experencia.");
                character.SendServerMessage("Has ganado 2040 doplones.");

                var doplones = ItemManager.Instance.CreatePlayerItem(character, 13052, 2040, true);

                character.Inventory.AddKamas(598000);
                character.Inventory.AddItem(doplones);
                character.AddExperience(950000);
                character.Inventory.RemoveItem(Golosorak, 1);
                Singleton<World>.Instance.SendAnnounce(annonce, Color.Red);
                entregado = true;
            }
            #endregion

            #region Mecanimut
            var seBuscaItemMecanimut = Singleton<ItemManager>.Instance.TryGetTemplate(10812);
            var Mecanimut = character.Inventory.TryGetItem(seBuscaItemMecanimut);

            if (Mecanimut != null)
            {

                string annonce = character.Name + " ha entregado a Mecanimut a las autoridades.";

                character.SendServerMessage("Has ganado 598000 kamas.");
                character.SendServerMessage("Has ganado 950000 puntos de experencia.");
                character.SendServerMessage("Has ganado 2040 doplones.");

                var doplones = ItemManager.Instance.CreatePlayerItem(character, 13052, 2040, true);

                character.Inventory.AddKamas(598000);
                character.Inventory.AddItem(doplones);
                character.AddExperience(950000);
                character.Inventory.RemoveItem(Mecanimut, 1);
                Singleton<World>.Instance.SendAnnounce(annonce, Color.Red);
                entregado = true;
            }
            #endregion

            #region Psikopomzopato
            var seBuscaItemPsikopomzopato = Singleton<ItemManager>.Instance.TryGetTemplate(10813);
            var Psikopomzopato = character.Inventory.TryGetItem(seBuscaItemPsikopomzopato);

            if (Psikopomzopato != null)
            {

                string annonce = character.Name + " ha entregado a Psikopomzopato a las autoridades.";

                character.SendServerMessage("Has ganado 598000 kamas.");
                character.SendServerMessage("Has ganado 950000 puntos de experencia.");
                character.SendServerMessage("Has ganado 2040 doplones.");

                var doplones = ItemManager.Instance.CreatePlayerItem(character, 13052, 2040, true);

                character.Inventory.AddKamas(598000);
                character.Inventory.AddItem(doplones);
                character.AddExperience(950000);
                character.Inventory.RemoveItem(Psikopomzopato, 1);
                Singleton<World>.Instance.SendAnnounce(annonce, Color.Red);
                entregado = true;
            }
            #endregion

            #region Caballero de Hielo
            var seBuscaItemCaballero = Singleton<ItemManager>.Instance.TryGetTemplate(10814);
            var Caballero = character.Inventory.TryGetItem(seBuscaItemCaballero);

            if (Caballero != null)
            {

                string annonce = character.Name + " ha entregado al Caballero de Hielo a las autoridades.";

                character.SendServerMessage("Has ganado 598000 kamas.");
                character.SendServerMessage("Has ganado 950000 puntos de experencia.");
                character.SendServerMessage("Has ganado 2040 doplones.");

                var doplones = ItemManager.Instance.CreatePlayerItem(character, 13052, 2040, true);

                character.Inventory.AddKamas(598000);
                character.Inventory.AddItem(doplones);
                character.AddExperience(950000);
                character.Inventory.RemoveItem(Caballero, 1);
                Singleton<World>.Instance.SendAnnounce(annonce, Color.Red);
                entregado = true;
            }
            #endregion

            #region Tentenhuevo
            var seBuscaItemTentenhuevo = Singleton<ItemManager>.Instance.TryGetTemplate(10815);
            var Tentenhuevo = character.Inventory.TryGetItem(seBuscaItemTentenhuevo);

            if (Tentenhuevo != null)
            {

                string annonce = character.Name + " ha entregado a Tentenhuevo a las autoridades.";

                character.SendServerMessage("Has ganado 598000 kamas.");
                character.SendServerMessage("Has ganado 950000 puntos de experencia.");
                character.SendServerMessage("Has ganado 2040 doplones.");

                var doplones = ItemManager.Instance.CreatePlayerItem(character, 13052, 2040, true);

                character.Inventory.AddKamas(598000);
                character.Inventory.AddItem(doplones);
                character.AddExperience(950000);
                character.Inventory.RemoveItem(Tentenhuevo, 1);
                Singleton<World>.Instance.SendAnnounce(annonce, Color.Red);
                entregado = true;
            }
            #endregion

            #region Conde Kontatrás
            var seBuscaItemConde = Singleton<ItemManager>.Instance.TryGetTemplate(10816);
            var Conde = character.Inventory.TryGetItem(seBuscaItemConde);

            if (Conde != null)
            {

                string annonce = character.Name + " ha entregado al Conde Kontatrás a las autoridades.";

                character.SendServerMessage("Has ganado 750000 kamas.");
                character.SendServerMessage("Has ganado 1350000 puntos de experencia.");
                character.SendServerMessage("Has ganado 2040 doplones.");

                var doplones = ItemManager.Instance.CreatePlayerItem(character, 13052, 2040, true);

                character.Inventory.AddKamas(750000);
                character.Inventory.AddItem(doplones);
                character.AddExperience(1350000);
                character.Inventory.RemoveItem(Conde, 1);
                Singleton<World>.Instance.SendAnnounce(annonce, Color.Red);
                entregado = true;
            }
            #endregion


            if (!entregado)
            {
                character.SendServerMessage($"No tienes a ningún malechor para entregar a las autoridades.");
            }

            character.RefreshActor();

            return true;
        }



    }
}