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
    [Discriminator("OcreDos", typeof(NpcReply), new System.Type[] { typeof(NpcReplyRecord) })]
    class OcreDos : NpcReply
    {
        public OcreDos(NpcReplyRecord record)
            : base(record)
        {
        }
        public override bool Execute(Npc npc, Character character)
        {
            var faseAnterior = Singleton<ItemManager>.Instance.TryGetTemplate(19437); 
            var faseCompletada = character.Inventory.TryGetItem(faseAnterior);

            if (faseCompletada != null)
            {
                //Inicio Quest (item para poder dropear requisitos)
                var itemUnoQuest = Singleton<ItemManager>.Instance.TryGetTemplate(18239); 
                var itemUno = character.Inventory.TryGetItem(itemUnoQuest);
                character.Inventory.AddItem(itemUnoQuest);


                //Comprobar requisitos
                var itemDosQuest = Singleton<ItemManager>.Instance.TryGetTemplate(18240);
                var itemDos = character.Inventory.TryGetItem(itemDosQuest);

                if (itemDos != null && itemDos.Stack >= 150)
                {

                    character.Inventory.RemoveItem(itemDos, (int)itemDos.Stack);
                    character.Inventory.RemoveItem(itemUno, (int)itemUno.Stack);

                    character.SendServerMessage("Has terminado la Etapa 2 de 3.");
                    character.SendServerMessage("Has conseguido 39 370 000 puntos de experencia.");
                    character.AddExperience(39370000);
                    character.AddOrnament(40);

                    var itemFinal = Singleton<ItemManager>.Instance.TryGetTemplate(18243); 
                    character.Inventory.AddItem(itemFinal);
                    character.Inventory.RemoveItem(faseCompletada);

                    character.RefreshActor();
                }
                else
                {
                    character.SendServerMessage("No cumples con las almas necesarias.");
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