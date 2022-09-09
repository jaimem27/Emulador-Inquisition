using Stump.DofusProtocol.Enums;
using Stump.Server.BaseServer.Database;
using Stump.Core.Reflection;
using Stump.Server.WorldServer.Database.Npcs;
using Stump.Server.WorldServer.Database.Npcs.Replies;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Npcs;
using Stump.Server.WorldServer.Game;
using Stump.Server.WorldServer.Game.Fights.Teams;
using Stump.Server.WorldServer.Game.Maps.Cells;
using Stump.Server.WorldServer.Game.Fights;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Monsters;
using Stump.Server.WorldServer.Game.Actors.Fight;
using Stump.Core.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stump.Server.WorldServer.Database.Monsters;
using System.Drawing;
using Stump.Server.WorldServer.Game.Items.Player;
using Stump.Server.WorldServer.Game.Items;
using Stump.Server.WorldServer.Handlers.Context;
using Stump.DofusProtocol.Messages;

namespace Database.Npcs.Replies
{
    [Discriminator("VigilantesCinco", typeof(NpcReply), new System.Type[] { typeof(NpcReplyRecord) })]
    class VigilantesCinco : NpcReply
    {
        public VigilantesCinco(NpcReplyRecord record)
            : base(record)
        {
        }
        public override bool Execute(Npc npc, Character character)
        {
            var misionTerminada = Singleton<ItemManager>.Instance.TryGetTemplate(17469); // Pipa de burbujas
            var dofusObtenido = character.Inventory.TryGetItem(misionTerminada);

            if (dofusObtenido != null)
            {
                var itemUnoQuest = Singleton<ItemManager>.Instance.TryGetTemplate(17555); //Ratón zascandil
                var itemUno = character.Inventory.TryGetItem(itemUnoQuest);

                if (itemUno != null && itemUno.Stack >= 1)
                {

                    character.Inventory.RemoveItem(dofusObtenido, (int)dofusObtenido.Stack);
                    character.Inventory.RemoveItem(itemUno, (int)itemUno.Stack);

                    character.SendServerMessage("Has terminado la Etapa 5 de 5.");
                    character.SendServerMessage("Has conseguido 15 000 000 puntos de experencia.");
                    character.AddExperience(15000000);
                    character.SendServerMessage("Has conseguido 43 980 Kamas.");
                    character.Inventory.AddKamas(43980);

                    var dofus = Singleton<ItemManager>.Instance.TryGetTemplate(16061);
                    character.Inventory.AddItem(dofus);
                    string annonce = character.Name + " ha terminado completar el Dofus de los Vigilantes";
                    Singleton<World>.Instance.SendAnnounce(annonce, Color.Azure);

                    var itemSiguienteEtapa = Singleton<ItemManager>.Instance.TryGetTemplate(17089); // Bendición goblin
                    character.Inventory.AddItem(itemSiguienteEtapa);
                    character.RefreshActor();
                }
                else
                {
                    character.SendServerMessage("No tienes los materiales necesarios.");
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