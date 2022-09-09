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
    [Discriminator("SeBuscaPicahari", typeof(NpcReply), new System.Type[]
 {
  typeof(NpcReplyRecord)
 })]
    class SeBuscaPicahari : NpcReply
    {
        public SeBuscaPicahari(NpcReplyRecord record)
            : base(record)
        {
        }
        public override bool Execute(Npc npc, Character character)
        {
            bool entregado = false;

            #region Turnado
            var seBuscaItemTurnado = Singleton<ItemManager>.Instance.TryGetTemplate(15007);
            var Turnado = character.Inventory.TryGetItem(seBuscaItemTurnado);

            if (Turnado != null)
            {

                string annonce = character.Name + " ha entregado a Turnado a las autoridades de Picahari.";

                character.SendServerMessage("Has ganado 64000 kamas.");
                character.SendServerMessage("Has ganado 400000 puntos de experencia.");
                character.SendServerMessage("Has ganado 600 doplones.");

                var doplones = ItemManager.Instance.CreatePlayerItem(character, 13052, 600, true);

                character.Inventory.AddKamas(64000);
                character.Inventory.AddItem(doplones);
                character.AddExperience(400000);
                character.Inventory.RemoveItem(Turnado, 1);
                Singleton<World>.Instance.SendAnnounce(annonce, Color.Red);
                entregado = true;
            }
            #endregion

            #region Juanca Mole
            var seBuscaItemJuanca = Singleton<ItemManager>.Instance.TryGetTemplate(15400);
            var Juanca = character.Inventory.TryGetItem(seBuscaItemJuanca);

            if (Juanca != null)
            {

                string annonce = character.Name + " ha entregado a Juanca Mole a las autoridades de Picahari.";

                character.SendServerMessage("Has ganado 80000 kamas.");
                character.SendServerMessage("Has ganado 650000 puntos de experencia.");
                character.SendServerMessage("Has ganado 1080 doplones.");

                var doplones = ItemManager.Instance.CreatePlayerItem(character, 13052, 1080, true);

                character.Inventory.AddKamas(80000);
                character.Inventory.AddItem(doplones);
                character.AddExperience(650000);
                character.Inventory.RemoveItem(Juanca, 1);
                Singleton<World>.Instance.SendAnnounce(annonce, Color.Red);
                entregado = true;
            }
            #endregion

            #region Sa'Kais'Hulud
            var seBuscaItemHulud = Singleton<ItemManager>.Instance.TryGetTemplate(15417);
            var Hulud = character.Inventory.TryGetItem(seBuscaItemHulud);

            if (Hulud != null)
            {

                string annonce = character.Name + " ha entregado a Sa'Kais'Hulud a las autoridades de Picahari.";

                character.SendServerMessage("Has ganado 410000 kamas.");
                character.SendServerMessage("Has ganado 850000 puntos de experencia.");
                character.SendServerMessage("Has ganado 1560 doplones.");

                var doplones = ItemManager.Instance.CreatePlayerItem(character, 13052, 1560, true);

                character.Inventory.AddKamas(410000);
                character.Inventory.AddItem(doplones);
                character.AddExperience(850000);
                character.Inventory.RemoveItem(Hulud, 1);
                Singleton<World>.Instance.SendAnnounce(annonce, Color.Red);
                entregado = true;
            }
            #endregion

            #region Jepricornio
            var seBuscaItemJepricornio = Singleton<ItemManager>.Instance.TryGetTemplate(15418);
            var Jepricornio = character.Inventory.TryGetItem(seBuscaItemJepricornio);

            if (Jepricornio != null)
            {

                string annonce = character.Name + " ha entregado a Jepricornio a las autoridades de Picahari.";

                character.SendServerMessage("Has ganado 598000 kamas.");
                character.SendServerMessage("Has ganado 1000000 puntos de experencia.");
                character.SendServerMessage("Has ganado 2040 doplones.");

                var doplones = ItemManager.Instance.CreatePlayerItem(character, 13052, 2040, true);

                character.Inventory.AddKamas(598000);
                character.Inventory.AddItem(doplones);
                character.AddExperience(1000000);
                character.Inventory.RemoveItem(Jepricornio, 1);
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