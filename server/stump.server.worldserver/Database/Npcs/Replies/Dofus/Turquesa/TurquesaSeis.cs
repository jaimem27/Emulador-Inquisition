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
    [Discriminator("TurquesaSeis", typeof(NpcReply), new System.Type[] { typeof(NpcReplyRecord) })]
    class TurquesaSeis : NpcReply
    {
        public TurquesaSeis(NpcReplyRecord record)
            : base(record)
        {
        }
        public override bool Execute(Npc npc, Character character)
        {
            var faseAnterior = Singleton<ItemManager>.Instance.TryGetTemplate(18289);
            var faseCompletada = character.Inventory.TryGetItem(faseAnterior);

            if (faseCompletada != null)
            {

                //Comprobar requisitos
                var itemDosQuest = Singleton<ItemManager>.Instance.TryGetTemplate(18291);
                var itemDos = character.Inventory.TryGetItem(itemDosQuest);

                if (itemDos != null && itemDos.Stack >= 1)
                {

                    character.Inventory.RemoveItem(itemDos, (int)itemDos.Stack);

                    character.SendServerMessage("Has terminado la Etapa 6 de 6.");
                    character.SendServerMessage("Has conseguido 13 100 000 puntos de experencia.");
                    character.AddExperience(13100000);

                    var itemFinal = Singleton<ItemManager>.Instance.TryGetTemplate(18268);
                    character.Inventory.AddItem(itemFinal);

                    character.SendServerMessage("Has obtenido el Dofus Turquesa.");
                    var dofus = Singleton<ItemManager>.Instance.TryGetTemplate(739);
                    character.Inventory.AddItem(dofus);
                    string annonce = character.Name + " ha terminado completar el Dofus Turquesa!";
                    Singleton<World>.Instance.SendAnnounce(annonce, Color.Azure);
                    //character.AddOrnament(41);
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
                character.SendServerMessage("No has completado la quinta fase.");
                return false;
            }

        }

    }
}