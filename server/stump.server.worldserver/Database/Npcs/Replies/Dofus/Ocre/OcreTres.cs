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
    [Discriminator("OcreTres", typeof(NpcReply), new System.Type[] { typeof(NpcReplyRecord) })]
    class OcreTres : NpcReply
    {
        public OcreTres(NpcReplyRecord record)
            : base(record)
        {
        }
        public override bool Execute(Npc npc, Character character)
        {
            var faseAnterior = Singleton<ItemManager>.Instance.TryGetTemplate(18243);
            var faseCompletada = character.Inventory.TryGetItem(faseAnterior);

            if (faseCompletada != null)
            {
                //Inicio Quest (item para poder dropear requisitos)
                var itemUnoQuest = Singleton<ItemManager>.Instance.TryGetTemplate(18244);
                var itemUno = character.Inventory.TryGetItem(itemUnoQuest);
                character.Inventory.AddItem(itemUnoQuest);


                //Comprobar requisitos
                var itemDosQuest = Singleton<ItemManager>.Instance.TryGetTemplate(9720);
                var itemDos = character.Inventory.TryGetItem(itemDosQuest);

                if (itemDos != null && itemDos.Stack >= 1)
                {

                    character.Inventory.RemoveItem(itemDos, (int)itemDos.Stack);
                    character.Inventory.RemoveItem(itemUno, (int)itemUno.Stack);

                    character.SendServerMessage("Has terminado la Etapa 3 de 3.");
                    character.SendServerMessage("Has conseguido 23 100 000 puntos de experencia.");
                    character.AddExperience(23100000);

                    var itemFinal = Singleton<ItemManager>.Instance.TryGetTemplate(18231);
                    character.Inventory.AddItem(itemFinal);

                    character.SendServerMessage("Has obtenido el Dofus Ocre.");
                    var dofus = Singleton<ItemManager>.Instance.TryGetTemplate(7754);
                    character.Inventory.AddItem(dofus);
                    string annonce = character.Name + " ha terminado completar el Dofus Ocre!";
                    Singleton<World>.Instance.SendAnnounce(annonce, Color.Azure);
                    character.AddOrnament(41);
                    character.Inventory.RemoveItem(faseCompletada);

                    character.RefreshActor();

                }
                else
                {
                    character.SendServerMessage("No cumples con los requisitos para seguir.");
                    return false;
                }

                return true;
            }
            else
            {
                character.SendServerMessage("No has completado la segunda fase.");
                return false;
            }

        }

    }
}