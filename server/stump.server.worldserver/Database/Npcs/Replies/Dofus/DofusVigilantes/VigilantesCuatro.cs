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
    [Discriminator("VigilantesCuatro", typeof(NpcReply), new System.Type[] { typeof(NpcReplyRecord) })]
    class VigilantesCuatro : NpcReply
    {
        public VigilantesCuatro(NpcReplyRecord record)
            : base(record)
        {
        }
        public override bool Execute(Npc npc, Character character)
        {
            var misionTerminada = Singleton<ItemManager>.Instance.TryGetTemplate(17468); // Pipa de burbujas
            var dofusObtenido = character.Inventory.TryGetItem(misionTerminada);

            if (dofusObtenido != null)
            {
                var itemUnoQuest = Singleton<ItemManager>.Instance.TryGetTemplate(17470); //Pipa de burbujas
                var itemUno = character.Inventory.TryGetItem(itemUnoQuest);

                var itemDosQuest = Singleton<ItemManager>.Instance.TryGetTemplate(17554); //Lista del comandante Qu
                var itemDos = character.Inventory.TryGetItem(itemDosQuest);

                if (itemUno != null && itemUno.Stack >= 1 && itemDos != null && itemDos.Stack >= 1)
                {

                    character.Inventory.RemoveItem(dofusObtenido, (int)dofusObtenido.Stack);
                    character.Inventory.RemoveItem(itemDos, (int)itemDos.Stack);
                    character.Inventory.RemoveItem(itemUno, (int)itemUno.Stack);

                    character.SendServerMessage("Has terminado la Etapa 4 de 5.");
                    character.SendServerMessage("Has conseguido 5 000 000 puntos de experencia.");
                    character.AddExperience(5000000);
                    character.SendServerMessage("Has conseguido 43 980 Kamas.");
                    character.Inventory.AddKamas(43980);
                    character.AddTitle(164); 

                    var itemSiguienteEtapa = Singleton<ItemManager>.Instance.TryGetTemplate(17469); // Pipa de burbujas
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
                character.SendServerMessage("No has completado la tercera fase.");
                return false;
            }

        }

    }
}