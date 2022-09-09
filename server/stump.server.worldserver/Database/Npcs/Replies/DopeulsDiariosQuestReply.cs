using Stump.Core.Reflection;
using Stump.DofusProtocol.Enums;
using Stump.Server.BaseServer.Database;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Npcs;
using Stump.Server.WorldServer.Game.Items;
using System;

namespace Stump.Server.WorldServer.Database.Npcs.Replies
{
    [Discriminator("DopeulQuest", typeof(NpcReply), new Type[] { typeof(NpcReplyRecord) })]
    public class DopeulsDiariosQuestReply : NpcReply
    {
        public DopeulsDiariosQuestReply(NpcReplyRecord record) : base(record)
        {
        }

        public override bool Execute(Npc npc, Character character)
        {

            var itemCraneoFeka = Singleton<ItemManager>.Instance.TryGetTemplate(9077);
            var itemCraneoFekaDisponible = character.Inventory.TryGetItem(itemCraneoFeka);

            var itemCraneoOsamoda = Singleton<ItemManager>.Instance.TryGetTemplate(9078);
            var itemCraneoOsamodaDisponible = character.Inventory.TryGetItem(itemCraneoOsamoda);

            var itemCraneoAnutrof = Singleton<ItemManager>.Instance.TryGetTemplate(9079);
            var itemCraneoAnutrofDisponible = character.Inventory.TryGetItem(itemCraneoAnutrof);

            var itemCraneoSram = Singleton<ItemManager>.Instance.TryGetTemplate(9080);
            var itemCraneoSramDisponible = character.Inventory.TryGetItem(itemCraneoSram);

            var itemCraneoXelor = Singleton<ItemManager>.Instance.TryGetTemplate(9081);
            var itemCraneoXelorDisponible = character.Inventory.TryGetItem(itemCraneoXelor);

            var itemCraneoZurka = Singleton<ItemManager>.Instance.TryGetTemplate(9082);
            var itemCraneoZurkaDisponible = character.Inventory.TryGetItem(itemCraneoZurka);

            var itemCraneoEni = Singleton<ItemManager>.Instance.TryGetTemplate(9083);
            var itemCraneoEniDisponible = character.Inventory.TryGetItem(itemCraneoEni);

            var itemCraneoYopuka = Singleton<ItemManager>.Instance.TryGetTemplate(9084);
            var itemCraneoYopukaDisponible = character.Inventory.TryGetItem(itemCraneoYopuka);

            var itemCraneoCra = Singleton<ItemManager>.Instance.TryGetTemplate(9085);
            var itemCraneoCraDisponible = character.Inventory.TryGetItem(itemCraneoCra);

            var itemCraneoSadi = Singleton<ItemManager>.Instance.TryGetTemplate(9086);
            var itemCraneoSadiDisponible = character.Inventory.TryGetItem(itemCraneoSadi);

            var itemCraneoSacro = Singleton<ItemManager>.Instance.TryGetTemplate(9087);
            var itemCraneoSacroDisponible = character.Inventory.TryGetItem(itemCraneoSacro);

            var itemCraneoPandawa = Singleton<ItemManager>.Instance.TryGetTemplate(9088);
            var itemCraneoPandawaDisponible = character.Inventory.TryGetItem(itemCraneoPandawa);

            var itemCraneoTymador = Singleton<ItemManager>.Instance.TryGetTemplate(12242);
            var itemCraneoTymadorDisponible = character.Inventory.TryGetItem(itemCraneoTymador);

            var itemCraneoZobal = Singleton<ItemManager>.Instance.TryGetTemplate(12243);
            var itemCraneoZobalDisponible = character.Inventory.TryGetItem(itemCraneoZobal);

            var itemCraneoSteamer = Singleton<ItemManager>.Instance.TryGetTemplate(13274);
            var itemCraneoSteamerDisponible = character.Inventory.TryGetItem(itemCraneoSteamer);

            var itemCraneoSela = Singleton<ItemManager>.Instance.TryGetTemplate(16051);
            var itemCraneoSelaDisponible = character.Inventory.TryGetItem(itemCraneoSela);

            var itemCraneoHiper = Singleton<ItemManager>.Instance.TryGetTemplate(17441);
            var itemCraneoHiperDisponible = character.Inventory.TryGetItem(itemCraneoHiper);

            var itemCraneoUgi = Singleton<ItemManager>.Instance.TryGetTemplate(18618);
            var itemCraneoUgiDisponible = character.Inventory.TryGetItem(itemCraneoUgi);

            if (itemCraneoFekaDisponible != null && itemCraneoOsamodaDisponible != null && itemCraneoAnutrofDisponible != null && itemCraneoSramDisponible != null &&
                itemCraneoXelorDisponible != null && itemCraneoZurkaDisponible != null && itemCraneoCraDisponible != null && itemCraneoEniDisponible != null &&
                itemCraneoYopukaDisponible != null && itemCraneoSadiDisponible != null && itemCraneoSacroDisponible != null && itemCraneoPandawaDisponible != null &&
                itemCraneoTymadorDisponible != null && itemCraneoZobalDisponible != null && itemCraneoSteamerDisponible != null && itemCraneoSelaDisponible != null &&
                itemCraneoHiperDisponible != null && itemCraneoUgiDisponible != null &&
                itemCraneoFekaDisponible.Stack > 0 && itemCraneoOsamodaDisponible.Stack > 0 && itemCraneoAnutrofDisponible.Stack > 0 && itemCraneoSramDisponible.Stack > 0 &&
                itemCraneoXelorDisponible.Stack > 0 && itemCraneoZurkaDisponible.Stack > 0 && itemCraneoCraDisponible.Stack > 0 && itemCraneoEniDisponible.Stack > 0  &&
                itemCraneoYopukaDisponible.Stack > 0 && itemCraneoSadiDisponible.Stack > 0 && itemCraneoSacroDisponible.Stack > 0 && itemCraneoPandawaDisponible.Stack > 0 &&
                itemCraneoTymadorDisponible.Stack > 0 && itemCraneoZobalDisponible.Stack > 0 && itemCraneoSteamerDisponible.Stack > 0 && itemCraneoSelaDisponible.Stack > 0)
            {

                character.Inventory.RemoveItem(itemCraneoFekaDisponible, 1);
                character.Inventory.RemoveItem(itemCraneoOsamodaDisponible, 1);
                character.Inventory.RemoveItem(itemCraneoAnutrofDisponible, 1);
                character.Inventory.RemoveItem(itemCraneoSramDisponible, 1);

                character.Inventory.RemoveItem(itemCraneoXelorDisponible, 1);
                character.Inventory.RemoveItem(itemCraneoZurkaDisponible, 1);
                character.Inventory.RemoveItem(itemCraneoCraDisponible, 1);
                character.Inventory.RemoveItem(itemCraneoEniDisponible, 1);

                character.Inventory.RemoveItem(itemCraneoYopukaDisponible, 1);
                character.Inventory.RemoveItem(itemCraneoSadiDisponible, 1);
                character.Inventory.RemoveItem(itemCraneoSacroDisponible, 1);
                character.Inventory.RemoveItem(itemCraneoPandawaDisponible, 1);

                character.Inventory.RemoveItem(itemCraneoTymadorDisponible, 1);
                character.Inventory.RemoveItem(itemCraneoZobalDisponible, 1);
                character.Inventory.RemoveItem(itemCraneoSteamerDisponible, 1);
                character.Inventory.RemoveItem(itemCraneoSelaDisponible, 1);


                character.SendServerMessage("Misión diária de las Pruebas Dopeuls superada.");
                var exp = 2450 * character.Level;
                character.AddExperience(exp);
                var dop = 10 * (character.Level / 10);
                var doplones = ItemManager.Instance.CreatePlayerItem(character, 13052, dop, true);
                character.Inventory.AddItem(doplones);
                character.SendServerMessage("Has conseguido "+ exp +" puntos de experencia.");
                character.SendServerMessage("Has ganado "+dop+" doplones.");

            }
            else
            {
                character.SendServerMessage("No has superado las pruebas de los Doppeuls.");
                return false;
            }

            return true;
        }
    }
}
