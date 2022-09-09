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
    [Discriminator("SeBuscaAmakna", typeof(NpcReply), new System.Type[]
 {
  typeof(NpcReplyRecord)
 })]
    class SeBuscaAmakna : NpcReply
    {
        public SeBuscaAmakna(NpcReplyRecord record)
            : base(record)
        {
        }
        public override bool Execute(Npc npc, Character character)
        {
            bool entregado = false;         

            #region Ali Grothor
            var seBuscaItemAli = Singleton<ItemManager>.Instance.TryGetTemplate(10817);
            var Ali = character.Inventory.TryGetItem(seBuscaItemAli);

            if (Ali != null)
            {

                string annonce = character.Name + " ha entregado a Ali Grothor a las autoridades.";

                character.SendServerMessage("Has ganado 321000 kamas.");
                character.SendServerMessage("Has ganado 650000 puntos de experencia.");
                character.SendServerMessage("Has ganado 1080 doplones.");

                var doplones = ItemManager.Instance.CreatePlayerItem(character, 13052, 1080, true);

                character.Inventory.AddKamas(321000);
                character.Inventory.AddItem(doplones);
                character.AddExperience(650000);
                character.Inventory.RemoveItem(Ali, 1);
                Singleton<World>.Instance.SendAnnounce(annonce, Color.Red);
                entregado = true;
            }
            #endregion

            #region Ceboyix
            var seBuscaItemCeboyix = Singleton<ItemManager>.Instance.TryGetTemplate(12121);
            var Ceboyix = character.Inventory.TryGetItem(seBuscaItemCeboyix);

            if (Ceboyix != null)
            {

                string annonce = character.Name + " ha entregado a Ceboyix a las autoridades.";

                character.SendServerMessage("Has ganado 2000 kamas.");
                character.SendServerMessage("Has ganado 150000 puntos de experencia.");
                character.SendServerMessage("Has ganado 180 doplones.");

                var doplones = ItemManager.Instance.CreatePlayerItem(character, 13052, 180, true);

                character.Inventory.AddKamas(2000);
                character.Inventory.AddItem(doplones);
                character.AddExperience(150000);
                character.Inventory.RemoveItem(Ceboyix, 1);
                Singleton<World>.Instance.SendAnnounce(annonce, Color.Red);
                entregado = true;
            }
            #endregion

            #region Boorro
            var seBuscaItemBoorro = Singleton<ItemManager>.Instance.TryGetTemplate(12122);
            var Boorro = character.Inventory.TryGetItem(seBuscaItemBoorro);

            if (Boorro != null)
            {

                string annonce = character.Name + " ha entregado a Boorro a las autoridades.";

                character.SendServerMessage("Has ganado 32000 kamas.");
                character.SendServerMessage("Has ganado 300000 puntos de experencia.");
                character.SendServerMessage("Has ganado 360 doplones.");

                var doplones = ItemManager.Instance.CreatePlayerItem(character, 13052, 360, true);

                character.Inventory.AddKamas(32000);
                character.Inventory.AddItem(doplones);
                character.AddExperience(300000);
                character.Inventory.RemoveItem(Boorro, 1);
                Singleton<World>.Instance.SendAnnounce(annonce, Color.Red);
                entregado = true;
            }
            #endregion

            #region Hedera Venenosa
            var seBuscaItemHedera = Singleton<ItemManager>.Instance.TryGetTemplate(12123);
            var Hedera = character.Inventory.TryGetItem(seBuscaItemHedera);

            if (Hedera != null)
            {

                string annonce = character.Name + " ha entregado a Hedera Venenosa a las autoridades.";

                character.SendServerMessage("Has ganado 62000 kamas.");
                character.SendServerMessage("Has ganado 700000 puntos de experencia.");
                character.SendServerMessage("Has ganado 1200 doplones.");

                var doplones = ItemManager.Instance.CreatePlayerItem(character, 13052, 1200, true);

                character.Inventory.AddKamas(62000);
                character.Inventory.AddItem(doplones);
                character.AddExperience(700000);
                character.Inventory.RemoveItem(Hedera, 1);
                Singleton<World>.Instance.SendAnnounce(annonce, Color.Red);
                entregado = true;
            }
            #endregion

            #region Hiperescampo
            var seBuscaItemHiperescampo = Singleton<ItemManager>.Instance.TryGetTemplate(12277);
            var Hiperescampo = character.Inventory.TryGetItem(seBuscaItemHiperescampo);

            if (Hiperescampo != null)
            {

                string annonce = character.Name + " ha entregado a Hiperescampo a las autoridades.";

                character.SendServerMessage("Has ganado 598000 kamas.");
                character.SendServerMessage("Has ganado 1000000 puntos de experencia.");
                character.SendServerMessage("Has ganado 2040 doplones.");

                var doplones = ItemManager.Instance.CreatePlayerItem(character, 13052, 2040, true);

                character.Inventory.AddKamas(598000);
                character.Inventory.AddItem(doplones);
                character.AddExperience(1000000);
                character.Inventory.RemoveItem(Hiperescampo, 1);
                Singleton<World>.Instance.SendAnnounce(annonce, Color.Red);
                entregado = true;
            }
            #endregion

            #region Tirana la Terrible
            var seBuscaItemTirana = Singleton<ItemManager>.Instance.TryGetTemplate(13267);
            var Tirana = character.Inventory.TryGetItem(seBuscaItemTirana);

            if (Tirana != null)
            {

                string annonce = character.Name + " ha entregado a Tirana la Terrible a las autoridades.";

                character.SendServerMessage("Has ganado 98000 kamas.");
                character.SendServerMessage("Has ganado 400000 puntos de experencia.");
                character.SendServerMessage("Has ganado 600 doplones.");

                var doplones = ItemManager.Instance.CreatePlayerItem(character, 13052, 600, true);

                character.Inventory.AddKamas(98000);
                character.Inventory.AddItem(doplones);
                character.AddExperience(400000);
                character.Inventory.RemoveItem(Tirana, 1);
                Singleton<World>.Instance.SendAnnounce(annonce, Color.Red);
                entregado = true;
            }
            #endregion

            #region Naganita
            var seBuscaItemNaganita = Singleton<ItemManager>.Instance.TryGetTemplate(13268);
            var Naganita = character.Inventory.TryGetItem(seBuscaItemNaganita);

            if (Naganita != null)
            {

                string annonce = character.Name + " ha entregado a Naganita a las autoridades.";

                character.SendServerMessage("Has ganado 131500 kamas.");
                character.SendServerMessage("Has ganado 500000 puntos de experencia.");
                character.SendServerMessage("Has ganado 660 doplones.");

                var doplones = ItemManager.Instance.CreatePlayerItem(character, 13052, 660, true);

                character.Inventory.AddKamas(131500);
                character.Inventory.AddItem(doplones);
                character.AddExperience(500000);
                character.Inventory.RemoveItem(Naganita, 1);
                Singleton<World>.Instance.SendAnnounce(annonce, Color.Red);
                entregado = true;
            }
            #endregion

            #region Nenúflor Deloto
            var seBuscaItemDeloto = Singleton<ItemManager>.Instance.TryGetTemplate(13269);
            var Deloto = character.Inventory.TryGetItem(seBuscaItemDeloto);

            if (Deloto != null)
            {

                string annonce = character.Name + " ha entregado a Nenúflor Deloto a las autoridades.";

                character.SendServerMessage("Has ganado 131500 kamas.");
                character.SendServerMessage("Has ganado 600000 puntos de experencia.");
                character.SendServerMessage("Has ganado 960 doplones.");

                var doplones = ItemManager.Instance.CreatePlayerItem(character, 13052, 960, true);

                character.Inventory.AddKamas(131500);
                character.Inventory.AddItem(doplones);
                character.AddExperience(600000);
                character.Inventory.RemoveItem(Deloto, 1);
                Singleton<World>.Instance.SendAnnounce(annonce, Color.Red);
                entregado = true;
            }
            #endregion

            #region Gemalo
            var seBuscaItemGemalo = Singleton<ItemManager>.Instance.TryGetTemplate(14578);
            var Gemalo = character.Inventory.TryGetItem(seBuscaItemGemalo);

            if (Gemalo != null)
            {

                string annonce = character.Name + " ha entregado a Gemalo a las autoridades.";

                character.SendServerMessage("Has ganado 269500 kamas.");
                character.SendServerMessage("Has ganado 700000 puntos de experencia.");
                character.SendServerMessage("Has ganado 1200 doplones.");

                var doplones = ItemManager.Instance.CreatePlayerItem(character, 13052, 1200, true);

                character.Inventory.AddKamas(269500);
                character.Inventory.AddItem(doplones);
                character.AddExperience(700000);
                character.Inventory.RemoveItem(Gemalo, 1);
                Singleton<World>.Instance.SendAnnounce(annonce, Color.Red);
                entregado = true;
            }
            #endregion

            #region Nataka Discodoro
            var seBuscaItemNataka = Singleton<ItemManager>.Instance.TryGetTemplate(14959);
            var Nataka = character.Inventory.TryGetItem(seBuscaItemNataka);

            if (Nataka != null)
            {

                string annonce = character.Name + " ha entregado a Nataka Discodoro a las autoridades.";

                character.SendServerMessage("Has ganado 431687 kamas.");
                character.SendServerMessage("Has ganado 800000 puntos de experencia.");
                character.SendServerMessage("Has ganado 1440 doplones.");

                var doplones = ItemManager.Instance.CreatePlayerItem(character, 13052, 1440, true);

                character.Inventory.AddKamas(431687);
                character.Inventory.AddItem(doplones);
                character.AddExperience(800000);
                character.Inventory.RemoveItem(Nataka, 1);
                Singleton<World>.Instance.SendAnnounce(annonce, Color.Red);
                entregado = true;
            }
            #endregion

            #region Guerrero de Kaos
            var seBuscaItemKaos = Singleton<ItemManager>.Instance.TryGetTemplate(14960);
            var Kaos = character.Inventory.TryGetItem(seBuscaItemKaos);

            if (Kaos != null)
            {

                string annonce = character.Name + " ha entregado a Guerrero de Kaos a las autoridades.";

                character.SendServerMessage("Has ganado 489466 kamas.");
                character.SendServerMessage("Has ganado 900000 puntos de experencia.");
                character.SendServerMessage("Has ganado 1680 doplones.");

                var doplones = ItemManager.Instance.CreatePlayerItem(character, 13052, 1680, true);

                character.Inventory.AddKamas(489466);
                character.Inventory.AddItem(doplones);
                character.AddExperience(900000);
                character.Inventory.RemoveItem(Kaos, 1);
                Singleton<World>.Instance.SendAnnounce(annonce, Color.Red);
                entregado = true;
            }
            #endregion

            #region Gran Kongoku
            var seBuscaItemKongoku = Singleton<ItemManager>.Instance.TryGetTemplate(14969);
            var Kongoku = character.Inventory.TryGetItem(seBuscaItemKongoku);

            if (Kongoku != null)
            {

                string annonce = character.Name + " ha entregado a Gran Kongoku a las autoridades.";

                character.SendServerMessage("Has ganado 598000 kamas.");
                character.SendServerMessage("Has ganado 1000000 puntos de experencia.");
                character.SendServerMessage("Has ganado 2040 doplones.");

                var doplones = ItemManager.Instance.CreatePlayerItem(character, 13052, 2040, true);

                character.Inventory.AddKamas(598000);
                character.Inventory.AddItem(doplones);
                character.AddExperience(1000000);
                character.Inventory.RemoveItem(Kongoku, 1);
                Singleton<World>.Instance.SendAnnounce(annonce, Color.Red);
                entregado = true;
            }
            #endregion

            #region Crustassius Cley
            var seBuscaItemCrustassius = Singleton<ItemManager>.Instance.TryGetTemplate(15004);
            var Crustassius = character.Inventory.TryGetItem(seBuscaItemCrustassius);

            if (Crustassius != null)
            {

                string annonce = character.Name + " ha entregado a Crustassius Cley a las autoridades.";

                character.SendServerMessage("Has ganado 598000 kamas.");
                character.SendServerMessage("Has ganado 1000000 puntos de experencia.");
                character.SendServerMessage("Has ganado 2040 doplones.");

                var doplones = ItemManager.Instance.CreatePlayerItem(character, 13052, 2040, true);

                character.Inventory.AddKamas(598000);
                character.Inventory.AddItem(doplones);
                character.AddExperience(1000000);
                character.Inventory.RemoveItem(Crustassius, 1);
                Singleton<World>.Instance.SendAnnounce(annonce, Color.Red);
                entregado = true;
            }
            #endregion

            #region Burbudazred
            var seBuscaItemBurbudazred = Singleton<ItemManager>.Instance.TryGetTemplate(15005);
            var Burbudazred = character.Inventory.TryGetItem(seBuscaItemBurbudazred);

            if (Burbudazred != null)
            {

                string annonce = character.Name + " ha entregado a Burbudazred a las autoridades.";

                character.SendServerMessage("Has ganado 598000 kamas.");
                character.SendServerMessage("Has ganado 1000000 puntos de experencia.");
                character.SendServerMessage("Has ganado 2040 doplones.");

                var doplones = ItemManager.Instance.CreatePlayerItem(character, 13052, 2040, true);

                character.Inventory.AddKamas(598000);
                character.Inventory.AddItem(doplones);
                character.AddExperience(1000000);
                character.Inventory.RemoveItem(Burbudazred, 1);
                Singleton<World>.Instance.SendAnnounce(annonce, Color.Red);
                entregado = true;
            }
            #endregion

            #region Takomako
            var seBuscaItemTakomako = Singleton<ItemManager>.Instance.TryGetTemplate(15006);
            var Takomako = character.Inventory.TryGetItem(seBuscaItemTakomako);

            if (Takomako != null)
            {

                string annonce = character.Name + " ha entregado a Takomako a las autoridades.";

                character.SendServerMessage("Has ganado 598000 kamas.");
                character.SendServerMessage("Has ganado 1000000 puntos de experencia.");
                character.SendServerMessage("Has ganado 2040 doplones.");

                var doplones = ItemManager.Instance.CreatePlayerItem(character, 13052, 2040, true);

                character.Inventory.AddKamas(598000);
                character.Inventory.AddItem(doplones);
                character.AddExperience(1000000);
                character.Inventory.RemoveItem(Takomako, 1);
                Singleton<World>.Instance.SendAnnounce(annonce, Color.Red);
                entregado = true;
            }
            #endregion

            #region Kosakepika
            var seBuscaItemKosakepika = Singleton<ItemManager>.Instance.TryGetTemplate(15419);
            var Kosakepika = character.Inventory.TryGetItem(seBuscaItemKosakepika);

            if (Kosakepika != null)
            {

                string annonce = character.Name + " ha entregado a Kosakepika a las autoridades.";

                character.SendServerMessage("Has ganado 53100 kamas.");
                character.SendServerMessage("Has ganado 500000 puntos de experencia.");
                character.SendServerMessage("Has ganado 720 doplones.");

                var doplones = ItemManager.Instance.CreatePlayerItem(character, 13052, 720, true);

                character.Inventory.AddKamas(53100);
                character.Inventory.AddItem(doplones);
                character.AddExperience(500000);
                character.Inventory.RemoveItem(Kosakepika, 1);
                Singleton<World>.Instance.SendAnnounce(annonce, Color.Red);
                entregado = true;
            }
            #endregion

            #region Simbadás
            var seBuscaItemSimbadas = Singleton<ItemManager>.Instance.TryGetTemplate(15420);
            var Simbadas = character.Inventory.TryGetItem(seBuscaItemSimbadas);

            if (Simbadas != null)
            {

                string annonce = character.Name + " ha entregado a Simbadás a las autoridades.";

                character.SendServerMessage("Has ganado 6000 kamas.");
                character.SendServerMessage("Has ganado 450000 puntos de experencia.");
                character.SendServerMessage("Has ganado 660 doplones.");

                var doplones = ItemManager.Instance.CreatePlayerItem(character, 13052, 660, true);

                character.Inventory.AddKamas(6000);
                character.Inventory.AddItem(doplones);
                character.AddExperience(450000);
                character.Inventory.RemoveItem(Simbadas, 1);
                Singleton<World>.Instance.SendAnnounce(annonce, Color.Red);
                entregado = true;
            }
            #endregion

            #region Carlita de La Guerfeld
            var seBuscaItemCarlita = Singleton<ItemManager>.Instance.TryGetTemplate(15421);
            var Carlita = character.Inventory.TryGetItem(seBuscaItemCarlita);

            if (Carlita != null)
            {

                string annonce = character.Name + " ha entregado a Carlita de La Guerfeld a las autoridades.";

                character.SendServerMessage("Has ganado 53100 kamas.");
                character.SendServerMessage("Has ganado 500000 puntos de experencia.");
                character.SendServerMessage("Has ganado 720 doplones.");

                var doplones = ItemManager.Instance.CreatePlayerItem(character, 13052, 720, true);

                character.Inventory.AddKamas(53100);
                character.Inventory.AddItem(doplones);
                character.AddExperience(500000);
                character.Inventory.RemoveItem(Carlita, 1);
                Singleton<World>.Instance.SendAnnounce(annonce, Color.Red);
                entregado = true;
            }
            #endregion

            #region Coldbru'Selas
            var seBuscaItemSelas = Singleton<ItemManager>.Instance.TryGetTemplate(15423);
            var Selas = character.Inventory.TryGetItem(seBuscaItemSelas);

            if (Selas != null)
            {

                string annonce = character.Name + " ha entregado a Coldbru'Selas a las autoridades.";

                character.SendServerMessage("Has ganado 410000 kamas.");
                character.SendServerMessage("Has ganado 900000 puntos de experencia.");
                character.SendServerMessage("Has ganado 1680 doplones.");

                var doplones = ItemManager.Instance.CreatePlayerItem(character, 13052, 1680, true);

                character.Inventory.AddKamas(410000);
                character.Inventory.AddItem(doplones);
                character.AddExperience(900000);
                character.Inventory.RemoveItem(Selas, 1);
                Singleton<World>.Instance.SendAnnounce(annonce, Color.Red);
                entregado = true;
            }
            #endregion

            #region Predagob
            var seBuscaItemPredagob = Singleton<ItemManager>.Instance.TryGetTemplate(15424);
            var Predagob = character.Inventory.TryGetItem(seBuscaItemPredagob);

            if (Predagob != null)
            {

                string annonce = character.Name + " ha entregado a Predagob a las autoridades.";

                character.SendServerMessage("Has ganado 598000 kamas.");
                character.SendServerMessage("Has ganado 950000 puntos de experencia.");
                character.SendServerMessage("Has ganado 2040 doplones.");

                var doplones = ItemManager.Instance.CreatePlayerItem(character, 13052, 2040, true);

                character.Inventory.AddKamas(598000);
                character.Inventory.AddItem(doplones);
                character.AddExperience(950000);
                character.Inventory.RemoveItem(Predagob, 1);
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