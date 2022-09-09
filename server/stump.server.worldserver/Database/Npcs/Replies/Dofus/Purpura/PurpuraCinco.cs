using Stump.Core.Reflection;
using Stump.Server.BaseServer.Database;
using Stump.Server.WorldServer.Database.Npcs;
using Stump.Server.WorldServer.Database.Npcs.Replies;
using Stump.Server.WorldServer.Game;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Npcs;
using Stump.Server.WorldServer.Game.Items;
using System.Drawing;

namespace Database.Npcs.Replies
{
    [Discriminator("PurpuraCinco", typeof(NpcReply), new System.Type[] { typeof(NpcReplyRecord) })]
    class PurpuraCinco : NpcReply
    {
        public PurpuraCinco(NpcReplyRecord record)
            : base(record)
        {
        }
        public override bool Execute(Npc npc, Character character)
        {
            var faseAnterior = Singleton<ItemManager>.Instance.TryGetTemplate(18261);
            var faseCompletada = character.Inventory.TryGetItem(faseAnterior);

            if (faseCompletada != null)
            {
                //Inicio Quest (item para poder dropear requisitos)
                var itemUnoQuest = Singleton<ItemManager>.Instance.TryGetTemplate(18262);
                var itemUno = character.Inventory.TryGetItem(itemUnoQuest);
                character.Inventory.AddItem(itemUnoQuest);


                //Comprobar requisitos
                var itemDosQuest = Singleton<ItemManager>.Instance.TryGetTemplate(18263);
                var itemDos = character.Inventory.TryGetItem(itemDosQuest);

                if (itemDos != null && itemDos.Stack >= 1)
                {

                    character.Inventory.RemoveItem(itemDos, (int)itemDos.Stack);
                    character.Inventory.RemoveItem(itemUno, (int)itemUno.Stack);

                    character.SendServerMessage("Has terminado la Etapa 5 de 5.");
                    character.SendServerMessage("Has conseguido 11 348 415 puntos de experencia.");
                    character.AddExperience(11348415);
                    character.SendServerMessage("Has conseguido 37 980 Kamas.");
                    character.Inventory.AddKamas(37980);

                    var itemFinal = Singleton<ItemManager>.Instance.TryGetTemplate(18245);
                    character.Inventory.AddItem(itemFinal);
                    character.Inventory.RemoveItem(faseCompletada);

                    character.SendServerMessage("Has obtenido el Dofus Purpura.");
                    var dofus = Singleton<ItemManager>.Instance.TryGetTemplate(694);
                    character.Inventory.AddItem(dofus);
                    string annonce = character.Name + " ha terminado completar el Dofus Purpura!";
                    Singleton<World>.Instance.SendAnnounce(annonce, Color.Azure);

                    character.RefreshActor();
                }
                else
                {
                    character.SendServerMessage("No cumples con el objeto necesario.");
                    return false;
                }

                return true;
            }
            else
            {
                character.SendServerMessage("No has completado la cuarta fase.");
                return false;
            }

        }

    }
}