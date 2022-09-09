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
    [Discriminator("ZanaCinco", typeof(NpcReply), new System.Type[] { typeof(NpcReplyRecord) })]
    class ZanaCinco : NpcReply
    {
        public ZanaCinco(NpcReplyRecord record)
            : base(record)
        {
        }
        public override bool Execute(Npc npc, Character character)
        {
            var faseAnterior = Singleton<ItemManager>.Instance.TryGetTemplate(16901); //Bola de fucus
            var faseCompletada = character.Inventory.TryGetItem(faseAnterior);

            if (faseCompletada != null)
            {
                //Inicio Quest (item para poder dropear requisitos)
                var itemUnoQuest = Singleton<ItemManager>.Instance.TryGetTemplate(16902); //Bola de Nori
                var itemUno = character.Inventory.TryGetItem(itemUnoQuest);
                character.Inventory.AddItem(itemUnoQuest);


                //Comprobar requisitos
                var itemDosQuest = Singleton<ItemManager>.Instance.TryGetTemplate(16903); //Bola de kelpi
                var itemDos = character.Inventory.TryGetItem(itemDosQuest);

                if (itemDos != null)
                {

                    character.Inventory.RemoveItem(itemDos, (int)itemDos.Stack);
                    character.Inventory.RemoveItem(itemUno, (int)itemUno.Stack);

                    character.SendServerMessage("Has terminado la Etapa 5 de 7.");
                    character.SendServerMessage("Has conseguido 762 804 puntos de experencia.");
                    character.AddExperience(762804);
                    character.SendServerMessage("Has conseguido 4 760 Kamas.");
                    character.Inventory.AddKamas(4760);

                    character.Inventory.RemoveItem(faseCompletada, (int)faseCompletada.Stack);
                    //Obtencion del Dofus Zanahowia
                    var dofus = Singleton<ItemManager>.Instance.TryGetTemplate(972);
                    character.Inventory.AddItem(dofus);
                    string annonce = character.Name + " ha obtenido el Dofus Zanahowia!";
                    Singleton<World>.Instance.SendAnnounce(annonce, Color.Azure);

                    var itemFinal = Singleton<ItemManager>.Instance.TryGetTemplate(16904); // Bola de caulerpo
                    character.Inventory.AddItem(itemFinal);
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
                character.SendServerMessage("No has completado la cuarta fase.");
                return false;
            }

        }

    }
}