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
    [Discriminator("EsmeraldaCuatro", typeof(NpcReply), new System.Type[] { typeof(NpcReplyRecord) })]
    class Esmeralda4 : NpcReply
    {
        public Esmeralda4(NpcReplyRecord record)
            : base(record)
        {
        }
        public override bool Execute(Npc npc, Character character)
        {

            var muestra = Singleton<ItemManager>.Instance.TryGetTemplate(15597);
            var item = character.Inventory.TryGetItem(muestra);

            if (item != null)
            {
                var misionAnterior = Singleton<ItemManager>.Instance.TryGetTemplate(15596);
                var Anterior = character.Inventory.TryGetItem(misionAnterior);
                character.Inventory.RemoveItem(Anterior, (int)Anterior.Stack);

                character.Inventory.RemoveItem(item, (int)item.Stack);

                var itemDosQuest = Singleton<ItemManager>.Instance.TryGetTemplate(15599);
                character.Inventory.AddItem(itemDosQuest);

                character.SendServerMessage("Has completado la fase 5 de 5 de la misión.");
                character.SendServerMessage("Has obtenido el Dofus Esmeralda.");
                var dofus = Singleton<ItemManager>.Instance.TryGetTemplate(737);
                character.Inventory.AddItem(dofus,1);
                character.SendServerMessage("Has conseguido 3082320 puntos de experencia.");
                character.AddExperience(3082320);
                character.SendServerMessage("Has conseguido 16780 Kamas.");
                character.Inventory.AddKamas(16780);


                string annonce = character.Name + " ha terminado la misión del Dofus Esmeralda!";
                Singleton<World>.Instance.SendAnnounce(annonce, Color.Azure);
                character.RefreshActor();
            }
            else
            {
                character.SendServerMessage("No tienes la la espada de Dark Vlad.");
            }


            character.RefreshActor();


            return true;
        }

    }
}