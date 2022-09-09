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
    [Discriminator("TurquesaTres", typeof(NpcReply), new System.Type[] { typeof(NpcReplyRecord) })]
    class TurquesaTres : NpcReply
    {
        public TurquesaTres(NpcReplyRecord record)
            : base(record)
        {
        }
        public override bool Execute(Npc npc, Character character)
        {
            var faseAnterior = Singleton<ItemManager>.Instance.TryGetTemplate(18275);
            var faseCompletada = character.Inventory.TryGetItem(faseAnterior);

            if (faseCompletada != null)
            {

                //Comprobar requisitos
                var itemUnoQuest = Singleton<ItemManager>.Instance.TryGetTemplate(18276);
                var itemUno = character.Inventory.TryGetItem(itemUnoQuest);
                var itemDosQuest = Singleton<ItemManager>.Instance.TryGetTemplate(18277);
                var itemDos = character.Inventory.TryGetItem(itemDosQuest);
                var itemTresQuest = Singleton<ItemManager>.Instance.TryGetTemplate(18278);
                var itemTres = character.Inventory.TryGetItem(itemTresQuest);

                if (itemUno != null && itemUno.Stack >= 1 && itemDos != null && itemDos.Stack >= 1 && itemTres != null && itemTres.Stack >= 1)
                {

                    character.Inventory.RemoveItem(itemDos, (int)itemDos.Stack);
                    character.Inventory.RemoveItem(itemUno, (int)itemUno.Stack);
                    character.Inventory.RemoveItem(itemTres, (int)itemTres.Stack);

                    character.SendServerMessage("Has terminado la Etapa 3 de 6.");
                    character.SendServerMessage("Has conseguido 8 476 867 puntos de experencia.");
                    character.AddExperience(8476867);


                    var itemFinal = Singleton<ItemManager>.Instance.TryGetTemplate(18280);
                    character.Inventory.AddItem(itemFinal);
                    character.Inventory.RemoveItem(faseCompletada);

                    character.RefreshActor();
                }
                else
                {
                    character.SendServerMessage("No cumples con las condiciones.");
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