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
    [Discriminator("SeBusca", typeof(NpcReply), new System.Type[]
 {
  typeof(NpcReplyRecord)
 })]
    class SeBuscaReply : NpcReply
    {
        public SeBuscaReply(NpcReplyRecord record)
            : base(record)
        {
        }
        public override bool Execute(Npc npc, Character character)
        {
            bool entregado = false;

            #region Peazo Beyota
            var seBuscaItemArdilla = Singleton<ItemManager>.Instance.TryGetTemplate(19392);
            var ardilla = character.Inventory.TryGetItem(seBuscaItemArdilla);

            if (ardilla != null)
            {

                string annonce = character.Name + " ha entregado a Peazo Beyota a las autoridades.";

                character.SendServerMessage("Has ganado 245 kamas.");
                character.SendServerMessage("Has ganado 100000 puntos de experencia.");
                character.SendServerMessage("Has ganado 120 doplones.");

                var doplones = ItemManager.Instance.CreatePlayerItem(character, 13052, 120, true);

                character.Inventory.AddKamas(245);
                character.Inventory.AddItem(doplones);
                character.AddExperience(100000);
                character.Inventory.RemoveItem(ardilla, 1);
                Singleton<World>.Instance.SendAnnounce(annonce, Color.Red);
                entregado = true;
            }
            #endregion

            #region Tortacia Leukocitina
            var seBuscaItemTortacia = Singleton<ItemManager>.Instance.TryGetTemplate(6798);
            var tortacia = character.Inventory.TryGetItem(seBuscaItemTortacia);

            if (tortacia != null)
            {

                string annonce = character.Name + " ha entregado a Tortacia Leukocitina a las autoridades.";

                character.SendServerMessage("Has ganado 2000 kamas.");
                character.SendServerMessage("Has ganado 150000 puntos de experencia.");
                character.SendServerMessage("Has ganado 180 doplones.");

                var doplones = ItemManager.Instance.CreatePlayerItem(character, 13052, 180, true);

                character.Inventory.AddKamas(2000);
                character.Inventory.AddItem(doplones);
                character.AddExperience(150000);
                character.Inventory.RemoveItem(tortacia, 1);
                Singleton<World>.Instance.SendAnnounce(annonce, Color.Red);
                entregado = true;
            }
            #endregion

            #region Ogivol Scarratero
            var seBuscaItemOgivol = Singleton<ItemManager>.Instance.TryGetTemplate(6803);
            var Ogivol = character.Inventory.TryGetItem(seBuscaItemOgivol);

            if (Ogivol != null)
            {

                string annonce = character.Name + " ha entregado a Ogivol Scarratero a las autoridades.";

                character.SendServerMessage("Has ganado 32000 kamas.");
                character.SendServerMessage("Has ganado 250000 puntos de experencia.");
                character.SendServerMessage("Has ganado 300 doplones.");

                var doplones = ItemManager.Instance.CreatePlayerItem(character, 13052, 300, true);

                character.Inventory.AddKamas(32000);
                character.Inventory.AddItem(doplones);
                character.AddExperience(250000);
                character.Inventory.RemoveItem(Ogivol, 1);
                Singleton<World>.Instance.SendAnnounce(annonce, Color.Red);
                entregado = true;
            }
            #endregion

            #region Brumen Tinctorias
            var seBuscaItemBrumen = Singleton<ItemManager>.Instance.TryGetTemplate(6815);
            var Brumen = character.Inventory.TryGetItem(seBuscaItemBrumen);

            if (Brumen != null)
            {

                string annonce = character.Name + " ha entregado a Brumen Tinctorias a las autoridades.";

                character.SendServerMessage("Has ganado 33600 kamas.");
                character.SendServerMessage("Has ganado 300000 puntos de experencia.");
                character.SendServerMessage("Has ganado 360 doplones.");

                var doplones = ItemManager.Instance.CreatePlayerItem(character, 13052, 360, true);

                character.Inventory.AddKamas(33600);
                character.Inventory.AddItem(doplones);
                character.AddExperience(300000);
                character.Inventory.RemoveItem(Brumen, 1);
                Singleton<World>.Instance.SendAnnounce(annonce, Color.Red);
                entregado = true;
            }
            #endregion

            #region Qil Bil
            var seBuscaItemQil = Singleton<ItemManager>.Instance.TryGetTemplate(6816);
            var Qil = character.Inventory.TryGetItem(seBuscaItemQil);

            if (Qil != null)
            {

                string annonce = character.Name + " ha entregado a Qil Bil a las autoridades.";

                character.SendServerMessage("Has ganado 19200 kamas.");
                character.SendServerMessage("Has ganado 350000 puntos de experencia.");
                character.SendServerMessage("Has ganado 480 doplones.");

                var doplones = ItemManager.Instance.CreatePlayerItem(character, 13052, 480, true);

                character.Inventory.AddKamas(19200);
                character.Inventory.AddItem(doplones);
                character.AddExperience(350000);
                character.Inventory.RemoveItem(Qil, 1);
                Singleton<World>.Instance.SendAnnounce(annonce, Color.Red);
                entregado = true;
            }
            #endregion

            #region Noai Aludem
            var seBuscaItemNoai = Singleton<ItemManager>.Instance.TryGetTemplate(6828);
            var Noai = character.Inventory.TryGetItem(seBuscaItemNoai);

            if (Noai != null)
            {

                string annonce = character.Name + " ha entregado a Noai Aludem a las autoridades.";

                character.SendServerMessage("Has ganado 80000 kamas.");
                character.SendServerMessage("Has ganado 650000 puntos de experencia.");
                character.SendServerMessage("Has ganado 1080 doplones.");

                var doplones = ItemManager.Instance.CreatePlayerItem(character, 13052, 1080, true);

                character.Inventory.AddKamas(80000);
                character.Inventory.AddItem(doplones);
                character.AddExperience(650000);
                character.Inventory.RemoveItem(Noai, 1);
                Singleton<World>.Instance.SendAnnounce(annonce, Color.Red);
                entregado = true;
            }
            #endregion

            #region Marzwelo, el Goblin
            var seBuscaItemMarzwelo = Singleton<ItemManager>.Instance.TryGetTemplate(6829);
            var Marzwelo = character.Inventory.TryGetItem(seBuscaItemMarzwelo);

            if (Marzwelo != null)
            {

                string annonce = character.Name + " ha entregado a Marzwelo, el Goblin a las autoridades.";

                character.SendServerMessage("Has ganado 25000 kamas.");
                character.SendServerMessage("Has ganado 300000 puntos de experencia.");
                character.SendServerMessage("Has ganado 360 doplones.");

                var doplones = ItemManager.Instance.CreatePlayerItem(character, 13052, 360, true);

                character.Inventory.AddKamas(25000);
                character.Inventory.AddItem(doplones);
                character.AddExperience(300000);
                character.Inventory.RemoveItem(Marzwelo, 1);
                Singleton<World>.Instance.SendAnnounce(annonce, Color.Red);
                entregado = true;
            }
            #endregion

            #region Aermyn 'Braco' Scalptaras
            var seBuscaItemAermyn = Singleton<ItemManager>.Instance.TryGetTemplate(6833);
            var Aermyn = character.Inventory.TryGetItem(seBuscaItemAermyn);

            if (Aermyn != null)
            {

                string annonce = character.Name + " ha entregado a Aermyn 'Braco' Scalptaras a las autoridades.";

                character.SendServerMessage("Has ganado 16000 kamas.");
                character.SendServerMessage("Has ganado 400000 puntos de experencia.");
                character.SendServerMessage("Has ganado 600 doplones.");

                var doplones = ItemManager.Instance.CreatePlayerItem(character, 13052, 600, true);

                character.Inventory.AddKamas(16000);
                character.Inventory.AddItem(doplones);
                character.AddExperience(400000);
                character.Inventory.RemoveItem(Aermyn, 1);
                Singleton<World>.Instance.SendAnnounce(annonce, Color.Red);
                entregado = true;
            }
            #endregion

            #region Musha, el Maldito
            var seBuscaItemMusha = Singleton<ItemManager>.Instance.TryGetTemplate(6835);
            var Musha = character.Inventory.TryGetItem(seBuscaItemMusha);

            if (Musha != null)
            {

                string annonce = character.Name + " ha entregado a Musha, el Maldito a las autoridades.";

                character.SendServerMessage("Has ganado 64000 kamas.");
                character.SendServerMessage("Has ganado 400000 puntos de experencia.");
                character.SendServerMessage("Has ganado 600 doplones.");

                var doplones = ItemManager.Instance.CreatePlayerItem(character, 13052, 600, true);

                character.Inventory.AddKamas(64000);
                character.Inventory.AddItem(doplones);
                character.AddExperience(400000);
                character.Inventory.RemoveItem(Musha, 1);
                Singleton<World>.Instance.SendAnnounce(annonce, Color.Red);
                entregado = true;
            }
            #endregion

            #region Rok Gnorok
            var seBuscaItemRok = Singleton<ItemManager>.Instance.TryGetTemplate(6836);
            var Rok = character.Inventory.TryGetItem(seBuscaItemRok);

            if (Rok != null)
            {

                string annonce = character.Name + " ha entregado a Rok Gnorok a las autoridades.";

                character.SendServerMessage("Has ganado 6000 kamas.");
                character.SendServerMessage("Has ganado 450000 puntos de experencia.");
                character.SendServerMessage("Has ganado 660 doplones.");

                var doplones = ItemManager.Instance.CreatePlayerItem(character, 13052, 660, true);

                character.Inventory.AddKamas(6000);
                character.Inventory.AddItem(doplones);
                character.AddExperience(450000);
                character.Inventory.RemoveItem(Rok, 1);
                Singleton<World>.Instance.SendAnnounce(annonce, Color.Red);
                entregado = true;
            }
            #endregion

            #region Zatoïshwan
            var seBuscaItemZato = Singleton<ItemManager>.Instance.TryGetTemplate(6837);
            var Zato = character.Inventory.TryGetItem(seBuscaItemZato);

            if (Zato != null)
            {

                string annonce = character.Name + " ha entregado a Zatoïshwan a las autoridades.";

                character.SendServerMessage("Has ganado 112500 kamas.");
                character.SendServerMessage("Has ganado 750000 puntos de experencia.");
                character.SendServerMessage("Has ganado 1320 doplones.");

                var doplones = ItemManager.Instance.CreatePlayerItem(character, 13052, 1320, true);

                character.Inventory.AddKamas(112500);
                character.Inventory.AddItem(doplones);
                character.AddExperience(750000);
                character.Inventory.RemoveItem(Zato, 1);
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