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
    [Discriminator("OcreInicio", typeof(NpcReply), new System.Type[] { typeof(NpcReplyRecord) })]
    class OcreInicio : NpcReply
    {
        public OcreInicio(NpcReplyRecord record)
            : base(record)
        {
        }
        public override bool Execute(Npc npc, Character character)
        {
            var misionTerminada = Singleton<ItemManager>.Instance.TryGetTemplate(18231);
            var dofusObtenido = character.Inventory.TryGetItem(misionTerminada);

            if (dofusObtenido == null)
            {
                
                var itemDropReliquias = Singleton<ItemManager>.Instance.TryGetTemplate(18238);
                
                character.Inventory.AddItem(itemDropReliquias);

                var itemRequisitoUno = Singleton<ItemManager>.Instance.TryGetTemplate(18232);
                var itemUno = character.Inventory.TryGetItem(itemRequisitoUno);
                var itemRequisitoDos = Singleton<ItemManager>.Instance.TryGetTemplate(18234);
                var itemDos = character.Inventory.TryGetItem(itemRequisitoDos);
                var itemRequisitoTres = Singleton<ItemManager>.Instance.TryGetTemplate(18235);
                var itemTres = character.Inventory.TryGetItem(itemRequisitoTres);
                var itemRequisitoCuatro = Singleton<ItemManager>.Instance.TryGetTemplate(18236);
                var itemCuatro = character.Inventory.TryGetItem(itemRequisitoCuatro);
                var itemRequisitoCinco = Singleton<ItemManager>.Instance.TryGetTemplate(18237);
                var itemCinco = character.Inventory.TryGetItem(itemRequisitoCinco);

                if (itemUno != null && itemDos != null && itemTres != null && itemCuatro != null && itemCinco != null && itemUno.Stack >= 1 && itemDos.Stack >= 1 && itemTres.Stack >= 1 && itemCuatro.Stack >= 1 && itemCinco.Stack >= 1)
                {
                    var itemReliquia = character.Inventory.TryGetItem(itemDropReliquias);
                    character.Inventory.RemoveItem(itemReliquia, (int)itemReliquia.Stack);
                    character.Inventory.RemoveItem(itemUno, (int)itemUno.Stack);
                    character.Inventory.RemoveItem(itemDos, (int)itemDos.Stack);
                    character.Inventory.RemoveItem(itemTres, (int)itemTres.Stack);
                    character.Inventory.RemoveItem(itemCuatro, (int)itemCuatro.Stack);
                    character.Inventory.RemoveItem(itemCinco, (int)itemCinco.Stack);

                    var itemDosQuest = Singleton<ItemManager>.Instance.TryGetTemplate(19437);
                    character.Inventory.AddItem(itemDosQuest);


                    string annonce = character.Name + " ha iniciado la misión del Dofus Ocre!";
                    Singleton<World>.Instance.SendAnnounce(annonce, Color.Azure);
                    character.SendServerMessage("Has terminado la etapa 1 de 3.");
                    character.SendServerMessage("Has conseguido 12 825 000 puntos de experencia.");
                    character.AddExperience(12825000);

                    character.RefreshActor();
                }
                else
                {
                    character.SendServerMessage("No tienes las reliquias.");
                    return false;
                }

                return true;
            }
            else
            {
                character.SendServerMessage("Ya has obtenido el Dofus Ocre.");
                return false;
            }

        }

    }
}