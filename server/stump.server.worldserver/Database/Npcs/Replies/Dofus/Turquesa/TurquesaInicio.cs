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
    [Discriminator("TurquesaInicio", typeof(NpcReply), new System.Type[] { typeof(NpcReplyRecord) })]
    class TurquesaInicio : NpcReply
    {
        public TurquesaInicio(NpcReplyRecord record)
            : base(record)
        {
        }
        public override bool Execute(Npc npc, Character character)
        {
            var misionTerminada = Singleton<ItemManager>.Instance.TryGetTemplate(18268);
            var dofusObtenido = character.Inventory.TryGetItem(misionTerminada);

            if (dofusObtenido == null)
            {

                var itemDropReliquias = Singleton<ItemManager>.Instance.TryGetTemplate(18269);

                character.Inventory.AddItem(itemDropReliquias);

                var itemRequisitoUno = Singleton<ItemManager>.Instance.TryGetTemplate(18271);
                var itemUno = character.Inventory.TryGetItem(itemRequisitoUno);
                var itemRequisitoDos = Singleton<ItemManager>.Instance.TryGetTemplate(18272);
                var itemDos = character.Inventory.TryGetItem(itemRequisitoDos);

                if (itemUno != null && itemDos != null && itemUno.Stack >= 1 && itemDos.Stack >= 1 )
                {
                    var itemReliquia = character.Inventory.TryGetItem(itemDropReliquias);
                    character.Inventory.RemoveItem(itemReliquia, (int)itemReliquia.Stack);
                    character.Inventory.RemoveItem(itemUno, (int)itemUno.Stack);
                    character.Inventory.RemoveItem(itemDos, (int)itemDos.Stack);

                    var itemDosQuest = Singleton<ItemManager>.Instance.TryGetTemplate(18292);
                    character.Inventory.AddItem(itemDosQuest);


                    string annonce = character.Name + " ha iniciado la misión del Dofus Turquesa!";
                    Singleton<World>.Instance.SendAnnounce(annonce, Color.Azure);
                    character.SendServerMessage("Has terminado la etapa 1 de 6.");
                    character.SendServerMessage("Has conseguido 8 317 200 puntos de experencia.");
                    character.AddExperience(8317200);

                    character.RefreshActor();
                }
                else
                {
                    character.SendServerMessage("No tienes obtenido las pruebas del castigo.");
                    return false;
                }

                return true;
            }
            else
            {
                character.SendServerMessage("Ya has obtenido el Dofus Turquesa.");
                return false;
            }

        }

    }
}