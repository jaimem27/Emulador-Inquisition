using Stump.Core.Reflection;
using Stump.DofusProtocol.Enums;
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
    [Discriminator("ZanaSiete", typeof(NpcReply), new System.Type[] { typeof(NpcReplyRecord) })]
    class ZanaSiete : NpcReply
    {
        public ZanaSiete(NpcReplyRecord record)
            : base(record)
        {
        }
        public override bool Execute(Npc npc, Character character)
        {
            var faseAnterior = Singleton<ItemManager>.Instance.TryGetTemplate(17000); //Humo shamánico
            var faseCompletada = character.Inventory.TryGetItem(faseAnterior);

            if (faseCompletada != null)
            {
                //Inicio Quest (item para poder dropear requisitos)
                var itemUnoQuest = Singleton<ItemManager>.Instance.TryGetTemplate(17035); //Lanza goblin
                var itemUno = character.Inventory.TryGetItem(itemUnoQuest);
                character.Inventory.AddItem(itemUnoQuest);


                //Comprobar requisitos
                var itemDosQuest = Singleton<ItemManager>.Instance.TryGetTemplate(17062); //Bolsa repleta
                var itemDos = character.Inventory.TryGetItem(itemDosQuest);
                var itemTresQuest = Singleton<ItemManager>.Instance.TryGetTemplate(17074); //Concha de Remordimientos
                var itemTres = character.Inventory.TryGetItem(itemTresQuest);

                if (itemDos != null && itemTres != null)
                {

                    character.Inventory.RemoveItem(itemDos, (int)itemDos.Stack);
                    character.Inventory.RemoveItem(itemUno, (int)itemUno.Stack);
                    character.Inventory.RemoveItem(itemTres, (int)itemTres.Stack);

                    character.SendServerMessage("Has terminado la Etapa 7 de 7.");
                    character.SendServerMessage("Has conseguido 1 189 944 puntos de experencia.");
                    character.AddExperience(1189944);
                    character.SendServerMessage("Has conseguido 7 980 Kamas.");
                    character.Inventory.AddKamas(7980);

                    character.Inventory.RemoveItem(faseCompletada, (int)faseCompletada.Stack);
                    character.SendServerMessage("Has obtenido el Ornamento y titulo de wabbits, además de tres emotes nuevos y otro título por tu esfuerzo.");
                    character.AddOrnament(55);
                    character.AddTitle(79);
                    character.AddTitle(145);
                    character.AddEmote((EmotesEnum)93);
                    character.AddEmote((EmotesEnum)101);
                    character.AddEmote((EmotesEnum)50);

                    var itemFinal = Singleton<ItemManager>.Instance.TryGetTemplate(16890); // Marca de Meriana
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
                character.SendServerMessage("No has completado la sexta fase.");
                return false;
            }

        }

    }
}