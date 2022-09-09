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
    [Discriminator("PurpuraDos", typeof(NpcReply), new System.Type[] { typeof(NpcReplyRecord) })]
    class PurpuraDos : NpcReply
    {
        public PurpuraDos(NpcReplyRecord record)
            : base(record)
        {
        }
        public override bool Execute(Npc npc, Character character)
        {
            var faseAnterior = Singleton<ItemManager>.Instance.TryGetTemplate(18249);
            var faseCompletada = character.Inventory.TryGetItem(faseAnterior);

            if (faseCompletada != null)
            {
                //Inicio Quest (item para poder dropear requisitos)
                var itemUnoQuest = Singleton<ItemManager>.Instance.TryGetTemplate(18250);
                var itemUno = character.Inventory.TryGetItem(itemUnoQuest);
                character.Inventory.AddItem(itemUnoQuest);


                //Comprobar requisitos
                var itemDosQuest = Singleton<ItemManager>.Instance.TryGetTemplate(18251);
                var itemDos = character.Inventory.TryGetItem(itemDosQuest);
                var itemTresQuest = Singleton<ItemManager>.Instance.TryGetTemplate(18252);
                var itemTres = character.Inventory.TryGetItem(itemTresQuest);

                if (itemDos != null && itemDos.Stack >= 1 && itemTres != null && itemTres.Stack >= 1)
                {

                    character.Inventory.RemoveItem(itemDos, (int)itemDos.Stack);
                    character.Inventory.RemoveItem(itemUno, (int)itemUno.Stack);
                    character.Inventory.RemoveItem(itemTres, (int)itemTres.Stack);

                    character.SendServerMessage("Has terminado la Etapa 2 de 5.");
                    character.SendServerMessage("Has conseguido 5 370 000 puntos de experencia.");
                    character.AddExperience(5370000);
                    character.SendServerMessage("Has conseguido 17 980 Kamas.");
                    character.Inventory.AddKamas(17980);

                    var itemFinal = Singleton<ItemManager>.Instance.TryGetTemplate(18253);
                    character.Inventory.AddItem(itemFinal);
                    character.Inventory.RemoveItem(faseCompletada);

                    character.RefreshActor();
                }
                else
                {
                    character.SendServerMessage("No cumples con las objetos necesarios.");
                    return false;
                }

                return true;
            }
            else
            {
                character.SendServerMessage("No has completado la primera fase.");
                return false;
            }

        }

    }
}