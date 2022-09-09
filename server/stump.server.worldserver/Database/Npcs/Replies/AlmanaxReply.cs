
using Database.Dopeul;
using Stump.Core.Reflection;
using Stump.DofusProtocol.Enums;
using Stump.DofusProtocol.Messages;
using Stump.Server.BaseServer.Database;
using Stump.Server.WorldServer.Database.Almanax;
using Stump.Server.WorldServer.Database.Npcs;
using Stump.Server.WorldServer.Database.Npcs.Replies;
using Stump.Server.WorldServer.Game.Actors.Fight;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Monsters;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Npcs;
using Stump.Server.WorldServer.Game.Almanax;
using Stump.Server.WorldServer.Game.Dopeul;
using Stump.Server.WorldServer.Game.Fights;
using Stump.Server.WorldServer.Game.Items;
using Stump.Server.WorldServer.Game.Maps.Cells;
using Stump.Server.WorldServer.Handlers.Context;
using System;
using System.Drawing;
using System.Linq;

namespace Database.Npcs.Replies
{
    [Discriminator("Almanax", typeof(NpcReply), typeof(NpcReplyRecord))]
    internal class AlmanaxReplies : NpcReply
    {
        public AlmanaxReplies(NpcReplyRecord record) : base(record)
        {
        }

        int a = DateTime.Today.Day;
        public override bool Execute(Npc npc, Character character)
        {
            AlmanaxRecord EditAlmanax = null;
            
            var compareTime = DateTime.Now;
            character.AlmanaxCollection.Load(character.Id);
            foreach (var almanax in character.AlmanaxCollection.Almanax.Where(almanax => almanax.AlmanaxDia == a))
            {
                EditAlmanax = almanax;
                compareTime = almanax.Time;
                break;
            }
            if (compareTime <= DateTime.Now)
            {
                var almanaxIp = new AlmanaxCollection();
                almanaxIp.Load(character.Id);
                foreach (var almanax in almanaxIp.Almanax.Where(almanax => almanax.AlmanaxDia == a))
                {
                    EditAlmanax = almanax;
                    compareTime = almanax.Time;
                    break;
                }
                if (!(compareTime <= DateTime.Now))
                {
                    switch (character.Account.Lang)
                    {
                        case "es":
                            character.SendServerMessage(
                        $"Ya has realizado la ofrendra hoy!, Tienes que esperar <b>{compareTime.Subtract(DateTime.Now).Hours} horas, {compareTime.Subtract(DateTime.Now).Minutes} minutos</b>",
                        Color.Red);
                            break;
                        case "fr":
                            character.SendServerMessage(
                        $"Ya has realizado la ofrendra hoy!, Tienes que esperar <b>{compareTime.Subtract(DateTime.Now).Hours} horas, {compareTime.Subtract(DateTime.Now).Minutes} minutos</b>",
                        Color.Red);
                            break;
                        default:
                            character.SendServerMessage(
                        $"Ya has realizado la ofrendra hoy!, Tienes que esperar <b>{compareTime.Subtract(DateTime.Now).Hours} horas, {compareTime.Subtract(DateTime.Now).Minutes} minutos</b>",
                        Color.Red);
                            break;
                    }
                    character.LeaveDialog();
                    return false;
                }
                

                if (EditAlmanax != null)
                {
                    EditAlmanax.Id = character.Id;
                    EditAlmanax.IsUpdated = true;
                    EditAlmanax.Time = DateTime.Now.AddHours(24);
                }
                else
                {
                    var items = new AlmanaxItemCollection();
                    items.Load();

                    var s = items.Almanax[a];

                    var itemOfrenda = Singleton<ItemManager>.Instance.TryGetTemplate(s.itemId);
                    var ofrenda = character.Inventory.TryGetItem(itemOfrenda);
                    
                    if(ofrenda != null && ofrenda.Stack >= s.cantidad)
                    {
                        character.Inventory.RemoveItem(ofrenda, s.cantidad);

                        character.SendServerMessage("Has entregado la ofrenda a los Dioses. Vuelve en 24 horas para recibir otra bendición.");

                        Singleton<ItemManager>.Instance.CreateAlmanax(character, a);
                        character.AddAlmanaxRezos(1);
                        
                        var exp = character.Level * 10000;
                        character.AddExperience(exp);
                        character.SendServerMessage("Has ganado "+exp+" puntos de experencia.");

                        character.SendServerMessage("Días rezados en los santuarios : " + character.RezosAlmanax + ".");
                        var ficha = ItemManager.Instance.CreatePlayerItem(character, 13330, 1, true);
                        character.Inventory.AddItem(ficha);
                        character.SendServerMessage("Has ganado una Almanicha");

                        if (character.RezosAlmanax == 30)
                        {
                            var dofus = ItemManager.Instance.CreatePlayerItem(character, 13344, 1, true);


                            character.Inventory.AddItem(dofus);
                            character.SendServerMessage("Has ganado el Dolmanax por rezarle a los Dioses 30 veces.");
                        }

                        character.RefreshActor();

                    }
                    else
                    {
                        character.SendServerMessage("Para la ofrenda de hoy necesitas : "+itemOfrenda.Name.ToString()+"  y tienes que traer : "+s.cantidad);
                    }
                    
                }
                character.SaveLater();
                return true;
            }
            switch (character.Account.Lang)
            {
                case "es":
                    character.SendServerMessage(
                $"Ya has realizado la ofrendra hoy!, Tienes que esperar <b>{compareTime.Subtract(DateTime.Now).Hours} horas, {compareTime.Subtract(DateTime.Now).Minutes} minutos</b>",
                Color.Red);
                    break;
                case "fr":
                    character.SendServerMessage(
                $"Ya has realizado la ofrendra hoy!, Tienes que esperar <b>{compareTime.Subtract(DateTime.Now).Hours} horas, {compareTime.Subtract(DateTime.Now).Minutes} minutos</b>",
                Color.Red);
                    break;
                default:
                    character.SendServerMessage(
                $"Ya has realizado la ofrendra hoy!, Tienes que esperar <b>{compareTime.Subtract(DateTime.Now).Hours} horas, {compareTime.Subtract(DateTime.Now).Minutes} minutos</b>",
                Color.Red);
                    break;
            }
            character.LeaveDialog();
            return false;
        }
    }
}
