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
    [Discriminator("PurpuraCuatro", typeof(NpcReply), new System.Type[] { typeof(NpcReplyRecord) })]
    class PurpuraCuatro : NpcReply
    {
        public PurpuraCuatro(NpcReplyRecord record)
            : base(record)
        {
        }
        public override bool Execute(Npc npc, Character character)
        {
            var faseAnterior = Singleton<ItemManager>.Instance.TryGetTemplate(18259);
            var faseCompletada = character.Inventory.TryGetItem(faseAnterior);

            if (faseCompletada != null)
            {

                var itemDosQuest = Singleton<ItemManager>.Instance.TryGetTemplate(15391);
                var itemDos = character.Inventory.TryGetItem(itemDosQuest);

                if (itemDos != null && itemDos.Stack >= 1)
                {

                    character.Inventory.RemoveItem(itemDos, (int)itemDos.Stack);

                    character.SendServerMessage("Has terminado la Etapa 4 de 5.");
                    character.SendServerMessage("Has conseguido 7 370 000 puntos de experencia.");
                    character.AddExperience(7370000);
                    character.SendServerMessage("Has conseguido 25 980 Kamas.");
                    character.Inventory.AddKamas(25980);

                    var itemFinal = Singleton<ItemManager>.Instance.TryGetTemplate(18261);
                    character.Inventory.AddItem(itemFinal);
                    character.Inventory.RemoveItem(faseCompletada);

                    character.RefreshActor();
                }
                else
                {
                    character.SendServerMessage("No cumples con el requisito.");
                    return false;
                }

                return true;
            }
            else
            {
                character.SendServerMessage("No has completado la tercera fase.");
                return false;
            }

        }

    }
}