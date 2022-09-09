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
    [Discriminator("SeBuscaDimensiones", typeof(NpcReply), new System.Type[]
 {
  typeof(NpcReplyRecord)
 })]
    class SeBuscaDimensiones : NpcReply
    {
        public SeBuscaDimensiones(NpcReplyRecord record)
            : base(record)
        {
        }
        public override bool Execute(Npc npc, Character character)
        {
            bool entregado = false;
         

            #region Maxicof
            var seBuscaItemMaxicof = Singleton<ItemManager>.Instance.TryGetTemplate(12191);
            var Maxicof = character.Inventory.TryGetItem(seBuscaItemMaxicof);

            if (Maxicof != null)
            {

                string annonce = character.Name + " ha entregado a Maxicof a las autoridades.";

                character.SendServerMessage("Has ganado 53100 kamas.");
                character.SendServerMessage("Has ganado 500000 puntos de experencia.");
                character.SendServerMessage("Has ganado 720 doplones.");

                var doplones = ItemManager.Instance.CreatePlayerItem(character, 13052, 720, true);

                character.Inventory.AddKamas(53100);
                character.Inventory.AddItem(doplones);
                character.AddExperience(500000);
                character.Inventory.RemoveItem(Maxicof, 1);
                Singleton<World>.Instance.SendAnnounce(annonce, Color.Red);
                entregado = true;
            }
            #endregion

            #region Agrisombra
            var seBuscaItemAgrisombra = Singleton<ItemManager>.Instance.TryGetTemplate(12278);
            var Agrisombra = character.Inventory.TryGetItem(seBuscaItemAgrisombra);

            if (Agrisombra != null)
            {

                string annonce = character.Name + " ha entregado a Agrisombra a las autoridades.";

                character.SendServerMessage("Has ganado 123100 kamas.");
                character.SendServerMessage("Has ganado 750000 puntos de experencia.");
                character.SendServerMessage("Has ganado 1320 doplones.");

                var doplones = ItemManager.Instance.CreatePlayerItem(character, 13052, 1320, true);

                character.Inventory.AddKamas(123100);
                character.Inventory.AddItem(doplones);
                character.AddExperience(750000);
                character.Inventory.RemoveItem(Agrisombra, 1);
                Singleton<World>.Instance.SendAnnounce(annonce, Color.Red);
                entregado = true;
            }
            #endregion

            #region Dambeldoro
            var seBuscaItemDambeldoro = Singleton<ItemManager>.Instance.TryGetTemplate(12279);
            var Dambeldoro = character.Inventory.TryGetItem(seBuscaItemDambeldoro);

            if (Dambeldoro != null)
            {

                string annonce = character.Name + " ha entregado a Dambeldoro a las autoridades.";

                character.SendServerMessage("Has ganado 598000 kamas.");
                character.SendServerMessage("Has ganado 1000000 puntos de experencia.");
                character.SendServerMessage("Has ganado 2040 doplones.");

                var doplones = ItemManager.Instance.CreatePlayerItem(character, 13052, 2040, true);

                character.Inventory.AddKamas(598000);
                character.Inventory.AddItem(doplones);
                character.AddExperience(1000000);
                character.Inventory.RemoveItem(Dambeldoro, 1);
                Singleton<World>.Instance.SendAnnounce(annonce, Color.Red);
                entregado = true;
            }
            #endregion

            #region Lonyon Plata
            var seBuscaItemLonyon = Singleton<ItemManager>.Instance.TryGetTemplate(13054);
            var Lonyon = character.Inventory.TryGetItem(seBuscaItemLonyon);

            if (Lonyon != null)
            {

                string annonce = character.Name + " ha entregado a Lonyon Plata a las autoridades.";

                character.SendServerMessage("Has ganado 598000 kamas.");
                character.SendServerMessage("Has ganado 1000000 puntos de experencia.");
                character.SendServerMessage("Has ganado 2040 doplones.");

                var doplones = ItemManager.Instance.CreatePlayerItem(character, 13052, 2040, true);

                character.Inventory.AddKamas(598000);
                character.Inventory.AddItem(doplones);
                character.AddExperience(1000000);
                character.Inventory.RemoveItem(Lonyon, 1);
                Singleton<World>.Instance.SendAnnounce(annonce, Color.Red);
                entregado = true;
            }
            #endregion

            #region Mosquerosa
            var seBuscaItemMosquerosa = Singleton<ItemManager>.Instance.TryGetTemplate(13055);
            var Mosquerosa = character.Inventory.TryGetItem(seBuscaItemMosquerosa);

            if (Mosquerosa != null)
            {

                string annonce = character.Name + " ha entregado a Mosquerosa a las autoridades.";

                character.SendServerMessage("Has ganado 358500 kamas.");
                character.SendServerMessage("Has ganado 8000000 puntos de experencia.");
                character.SendServerMessage("Has ganado 1680 doplones.");

                var doplones = ItemManager.Instance.CreatePlayerItem(character, 13052, 1680, true);

                character.Inventory.AddKamas(358500);
                character.Inventory.AddItem(doplones);
                character.AddExperience(800000);
                character.Inventory.RemoveItem(Mosquerosa, 1);
                Singleton<World>.Instance.SendAnnounce(annonce, Color.Red);
                entregado = true;
            }
            #endregion

            #region Panterrosa
            var seBuscaItemPanterrosa = Singleton<ItemManager>.Instance.TryGetTemplate(13056);
            var Panterrosa = character.Inventory.TryGetItem(seBuscaItemPanterrosa);

            if (Panterrosa != null)
            {

                string annonce = character.Name + " ha entregado a Panterrosa a las autoridades.";

                character.SendServerMessage("Has ganado 138500 kamas.");
                character.SendServerMessage("Has ganado 6500000 puntos de experencia.");
                character.SendServerMessage("Has ganado 1080 doplones.");

                var doplones = ItemManager.Instance.CreatePlayerItem(character, 13052, 1080, true);

                character.Inventory.AddKamas(138500);
                character.Inventory.AddItem(doplones);
                character.AddExperience(650000);
                character.Inventory.RemoveItem(Panterrosa, 1);
                Singleton<World>.Instance.SendAnnounce(annonce, Color.Red);
                entregado = true;
            }
            #endregion

            #region Morblok
            var seBuscaItemMorblok = Singleton<ItemManager>.Instance.TryGetTemplate(13192);
            var Morblok = character.Inventory.TryGetItem(seBuscaItemMorblok);

            if (Morblok != null)
            {

                string annonce = character.Name + " ha entregado a Morblok a las autoridades.";

                character.SendServerMessage("Has ganado 78500 kamas.");
                character.SendServerMessage("Has ganado 6000000 puntos de experencia.");
                character.SendServerMessage("Has ganado 960 doplones.");

                var doplones = ItemManager.Instance.CreatePlayerItem(character, 13052, 960, true);

                character.Inventory.AddKamas(78500);
                character.Inventory.AddItem(doplones);
                character.AddExperience(600000);
                character.Inventory.RemoveItem(Morblok, 1);
                Singleton<World>.Instance.SendAnnounce(annonce, Color.Red);
                entregado = true;
            }
            #endregion

            #region Uhno
            var seBuscaItemUhno = Singleton<ItemManager>.Instance.TryGetTemplate(13195);
            var Uhno = character.Inventory.TryGetItem(seBuscaItemUhno);

            if (Uhno != null)
            {

                string annonce = character.Name + " ha entregado a Uhno a las autoridades.";

                character.SendServerMessage("Has ganado 358500 kamas.");
                character.SendServerMessage("Has ganado 850000 puntos de experencia.");
                character.SendServerMessage("Has ganado 1560 doplones.");

                var doplones = ItemManager.Instance.CreatePlayerItem(character, 13052, 1560, true);

                character.Inventory.AddKamas(358500);
                character.Inventory.AddItem(doplones);
                character.AddExperience(850000);
                character.Inventory.RemoveItem(Uhno, 1);
                Singleton<World>.Instance.SendAnnounce(annonce, Color.Red);
                entregado = true;
            }
            #endregion

            #region Ciguña
            var seBuscaItemCiguna = Singleton<ItemManager>.Instance.TryGetTemplate(13266);
            var Ciguna = character.Inventory.TryGetItem(seBuscaItemCiguna);

            if (Ciguna != null)
            {

                string annonce = character.Name + " ha entregado a Ciguna a las autoridades.";

                character.SendServerMessage("Has ganado 598000 kamas.");
                character.SendServerMessage("Has ganado 1000000 puntos de experencia.");
                character.SendServerMessage("Has ganado 2040 doplones.");

                var doplones = ItemManager.Instance.CreatePlayerItem(character, 13052, 2040, true);

                character.Inventory.AddKamas(598000);
                character.Inventory.AddItem(doplones);
                character.AddExperience(1000000);
                character.Inventory.RemoveItem(Ciguna, 1);
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