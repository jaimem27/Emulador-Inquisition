using Stump.Core.Reflection;
using Stump.DofusProtocol.Enums;
using Stump.DofusProtocol.Enums.Custom;
using Stump.DofusProtocol.Messages;
using Stump.DofusProtocol.Types;
using Stump.Server.BaseServer.Commands;
using Stump.Server.BaseServer.Network;
using Stump.Server.WorldServer.Commands.Trigger;
using Stump.Server.WorldServer.Core.Network;
using Stump.Server.WorldServer.Database.Items.Templates;
using Stump.Server.WorldServer.Game;
using Stump.Server.WorldServer.Game.Actors.Fight;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Monsters;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Mounts;
using Stump.Server.WorldServer.Game.Dialogs.Interactives;
using Stump.Server.WorldServer.Game.Effects.Instances;
using Stump.Server.WorldServer.Game.Fights;
using Stump.Server.WorldServer.Game.Formulas;
using Stump.Server.WorldServer.Game.HavenBags;
using Stump.Server.WorldServer.Game.Items;
using Stump.Server.WorldServer.Game.Items.Player;
using Stump.Server.WorldServer.Game.Items.Player.Custom;
using Stump.Server.WorldServer.Game.Items.Player.Custom.LivingObjects;
using Stump.Server.WorldServer.Game.Maps;
using Stump.Server.WorldServer.Game.Maps.Cells;
using Stump.Server.WorldServer.Handlers.Context;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Stump.Server.WorldServer.Handlers.Inventory
{
    public partial class InventoryHandler : WorldHandlerContainer
    {
        [WorldHandler(ObjectSetPositionMessage.Id)]
        public static void HandleObjectSetPositionMessage(WorldClient client, ObjectSetPositionMessage message)
        {
            if (!Enum.IsDefined(typeof(CharacterInventoryPositionEnum), (int)message.Position))
            {
                return;
            }

            var item = client.Character.Inventory.TryGetItem((int)message.ObjectUID);

            if (item == null)
            {
                return;
            }

            client.Character.Inventory.MoveItem(item, (CharacterInventoryPositionEnum)message.Position);

        }

        [WorldHandler(ObjectDeleteMessage.Id)]
        public static void HandleObjectDeleteMessage(WorldClient client, ObjectDeleteMessage message)
        {
            var item = client.Character.Inventory.TryGetItem((int)message.ObjectUID);

            if (item == null)
            {
                return;
            }

            client.Character.Inventory.RemoveItem(item, (int)message.Quantity);
        }

        [WorldHandler(ObjectUseMessage.Id)]
        public static void HandleObjectUseMessage(WorldClient client, ObjectUseMessage message)
        {
            var item = client.Character.Inventory.TryGetItem((int)message.ObjectUID);

            if (item == null)
            {
                return;
            }

            client.Character.Inventory.UseItem(item);

            switch (item.Template.Id)
            {// ---------------- Retomar Mazmorra ------------------//
                case 793: 
                    RetomarMazmorar(client, item);
                    break;
                // ---------------- Pocima de Aumento XP y DROP ------------------//
                case 21167: //Pocima de Experencia
                    var bonusItem = Singleton<ItemManager>.Instance.TryGetTemplate(18365); //Buff
                    client.Character.Inventory.AddItem(bonusItem, 1);
                    client.Character.Inventory.RemoveItem(item, 1);
                    client.Character.SendServerMessage("Tu próximo combate tendrá un bonus de XP y Drop del 40%");
                    break;
                // ---------------- Caja de Leyendas (Torre) ------------------//
                case 20671: //Caja de reflejos oníricos
                    Leyendas(client, item);
                    break;
                // ---------------- Regalos de navidad (Evento) ------------------//
                case 8335: //Regalito Pequeño de Nawidad
                    Regalito(client, item);
                    break;
                case 8337: //Regalo de Nawidad
                    Regalo(client, item);
                    break;
                case 8340: //Increíble regalo de Nawidad
                    Regalazo(client, item);
                    break;
 
                // ---------------- Regalos eventos ------------------//
                case 10675: //Primer premio
                    var sombreroBun = Singleton<ItemManager>.Instance.TryGetTemplate(8533); //Sombrero
                    var capaBun = Singleton<ItemManager>.Instance.TryGetTemplate(8534); //Capa
                    var botasBun = Singleton<ItemManager>.Instance.TryGetTemplate(8535); //Botas
                    var cinturonBun = Singleton<ItemManager>.Instance.TryGetTemplate(8536); //Cinturon
                    var anilloBun = Singleton<ItemManager>.Instance.TryGetTemplate(8537); //Anillo
                    var amuletoBun = Singleton<ItemManager>.Instance.TryGetTemplate(8538); //Amuleto
                    var petSab = Singleton<ItemManager>.Instance.TryGetTemplate(1711); //Pet + Sab
                    var loteria3 = Singleton<ItemManager>.Instance.TryGetTemplate(7220); //Loteria
                    client.Character.Inventory.AddItem(sombreroBun, 1);
                    client.Character.Inventory.AddItem(capaBun, 1);
                    client.Character.Inventory.AddItem(botasBun, 1);
                    client.Character.Inventory.AddItem(anilloBun, 1);
                    client.Character.Inventory.AddItem(amuletoBun, 1);
                    client.Character.Inventory.AddItem(petSab, 1);
                    client.Character.Inventory.AddItem(loteria3, 3);
                    client.Character.Inventory.RemoveItem(item, 1);
                    break;
                case 13471: //Segundo premio
                    var petSab1 = Singleton<ItemManager>.Instance.TryGetTemplate(1711); //Pet + Sab
                    var loteria1 = Singleton<ItemManager>.Instance.TryGetTemplate(7220); //Loteria
                    client.Character.Inventory.AddItem(petSab1, 1);
                    client.Character.Inventory.AddItem(loteria1, 3);
                    client.Character.Inventory.RemoveItem(item, 1);
                    break;
                case 13625: //Tercer premio
                    var sombreroBun1 = Singleton<ItemManager>.Instance.TryGetTemplate(8533); //Sombrero
                    var capaBun1 = Singleton<ItemManager>.Instance.TryGetTemplate(8534); //Capa
                    var botasBun1 = Singleton<ItemManager>.Instance.TryGetTemplate(8535); //Botas
                    var cinturonBun1 = Singleton<ItemManager>.Instance.TryGetTemplate(8536); //Cinturon
                    var anilloBun1 = Singleton<ItemManager>.Instance.TryGetTemplate(8537); //Anillo
                    var amuletoBun1 = Singleton<ItemManager>.Instance.TryGetTemplate(8538); //Amuleto
                    var loteria2 = Singleton<ItemManager>.Instance.TryGetTemplate(7220); //Loteria
                    client.Character.Inventory.AddItem(sombreroBun1, 1);
                    client.Character.Inventory.AddItem(capaBun1, 1);
                    client.Character.Inventory.AddItem(botasBun1, 1);
                    client.Character.Inventory.AddItem(anilloBun1, 1);
                    client.Character.Inventory.AddItem(amuletoBun1, 1);
                    client.Character.Inventory.AddItem(loteria2, 3);
                    client.Character.Inventory.RemoveItem(item, 1);
                    break;
                // ---------------- pergas lvl ------------------//
                case 12332: //add level < 25
                    if (client.Character.Level < 25)
                    {
                        client.Character.LevelUp(1);
                        client.Character.Inventory.RemoveItem(item, 1);
                    }

                    break;
                case 12333://add level <50
                    if (client.Character.Level < 50)
                    {
                        client.Character.LevelUp(1);
                        client.Character.Inventory.RemoveItem(item, 1);
                    }
                    break;
                case 684://add kolichas
                    var itemTemplate = Singleton<ItemManager>.Instance.TryGetTemplate(12736);
                    client.Character.Inventory.AddItem(itemTemplate, 1000);
                    client.Character.Inventory.RemoveItem(item, 1);
                    break;
                // ---------------- Bolsitas ------------------//
                case 16819:
                    var itemTemplate1 = Singleton<ItemManager>.Instance.TryGetTemplate(2331); //Berenjena
                    client.Character.Inventory.AddItem(itemTemplate1, 10);
                    client.Character.Inventory.RemoveItem(item, 1);
                    break;
                case 16820:
                    var itemTemplate2 = Singleton<ItemManager>.Instance.TryGetTemplate(1984); //Cenizas Eternas
                    client.Character.Inventory.AddItem(itemTemplate2, 10);
                    client.Character.Inventory.RemoveItem(item, 1);
                    break;
                case 16821:
                    var itemTemplate3 = Singleton<ItemManager>.Instance.TryGetTemplate(1734); //Cerezas
                    client.Character.Inventory.AddItem(itemTemplate3, 10);
                    client.Character.Inventory.RemoveItem(item, 1);
                    break;
                case 16822:
                    var itemTemplate4 = Singleton<ItemManager>.Instance.TryGetTemplate(1736); //Limones
                    client.Character.Inventory.AddItem(itemTemplate4, 10);
                    client.Character.Inventory.RemoveItem(item, 1);
                    break;
                case 16825:
                    var itemTemplate5 = Singleton<ItemManager>.Instance.TryGetTemplate(1977); //Especias
                    client.Character.Inventory.AddItem(itemTemplate5, 10);
                    client.Character.Inventory.RemoveItem(item, 1);
                    break;
                case 16826:
                    var itemTemplate6 = Singleton<ItemManager>.Instance.TryGetTemplate(1974); //Enchalada (Lechuga e-e)
                    client.Character.Inventory.AddItem(itemTemplate6, 10);
                    client.Character.Inventory.RemoveItem(item, 1);
                    break;
                case 16827:
                    var itemTemplate7 = Singleton<ItemManager>.Instance.TryGetTemplate(1983); //Grasa Gelatinosa
                    client.Character.Inventory.AddItem(itemTemplate7, 10);
                    client.Character.Inventory.RemoveItem(item, 1);
                    break;
                case 16828:
                    var itemTemplate8 = Singleton<ItemManager>.Instance.TryGetTemplate(6671); //Alubias
                    client.Character.Inventory.AddItem(itemTemplate8, 10);
                    client.Character.Inventory.RemoveItem(item, 1);
                    break;
                case 16830:
                    var itemTemplate9 = Singleton<ItemManager>.Instance.TryGetTemplate(1978); //Pimienta
                    client.Character.Inventory.AddItem(itemTemplate9, 10);
                    client.Character.Inventory.RemoveItem(item, 1);
                    break;
                case 16831:
                    var itemTemplate10 = Singleton<ItemManager>.Instance.TryGetTemplate(1730); //Sal
                    client.Character.Inventory.AddItem(itemTemplate10, 10);
                    client.Character.Inventory.RemoveItem(item, 1);
                    break;
                case 16832:
                    var itemTemplate11 = Singleton<ItemManager>.Instance.TryGetTemplate(1975); //Cebolla
                    client.Character.Inventory.AddItem(itemTemplate11, 10);
                    client.Character.Inventory.RemoveItem(item, 1);
                    break;
                case 16833:
                    var itemTemplate12 = Singleton<ItemManager>.Instance.TryGetTemplate(519); //Polvos Magicos
                    client.Character.Inventory.AddItem(itemTemplate12, 10);
                    client.Character.Inventory.RemoveItem(item, 1);
                    break;
                case 16834:
                    var itemTemplate13 = Singleton<ItemManager>.Instance.TryGetTemplate(1986); //Polvo Temporal
                    client.Character.Inventory.AddItem(itemTemplate13, 10);
                    client.Character.Inventory.RemoveItem(item, 1);
                    break;
                case 16835:
                    var itemTemplate14 = Singleton<ItemManager>.Instance.TryGetTemplate(1985); //Resina
                    client.Character.Inventory.AddItem(itemTemplate14, 10);
                    client.Character.Inventory.RemoveItem(item, 1);
                    break;
                // ----------------- Tonel ---------------- //
                case 16823:
                    var itemTemplate15 = Singleton<ItemManager>.Instance.TryGetTemplate(1731); //Zumo Sabroso
                    client.Character.Inventory.AddItem(itemTemplate15, 15);
                    client.Character.Inventory.RemoveItem(item, 1);
                    break;
                case 16824:
                    var itemTemplate16 = Singleton<ItemManager>.Instance.TryGetTemplate(311); //Agua
                    client.Character.Inventory.AddItem(itemTemplate16, 15);
                    client.Character.Inventory.RemoveItem(item, 1);
                    break;
                case 16829:
                    var itemTemplate17 = Singleton<ItemManager>.Instance.TryGetTemplate(1973); //Aceite para freir
                    client.Character.Inventory.AddItem(itemTemplate17, 15);
                    client.Character.Inventory.RemoveItem(item, 1);
                    break;
                case 16836:
                    var itemTemplate18 = Singleton<ItemManager>.Instance.TryGetTemplate(2012); //Sangre de Scorbuto
                    client.Character.Inventory.AddItem(itemTemplate18, 15);
                    client.Character.Inventory.RemoveItem(item, 1);
                    break;

                // ----------------- Pociones ---------------- //
                case 6965://Pócima de ciudad: Bonta
                    tpPlayer(client.Character, 5506048, 359, DirectionsEnum.DIRECTION_EAST);
                    client.Character.Inventory.RemoveItem(item, 1);
                    break;
                case 6964://Pócima de ciudad: Brakmar
                    tpPlayer(client.Character, 13631488, 370, DirectionsEnum.DIRECTION_EAST);
                    client.Character.Inventory.RemoveItem(item, 1);
                    break;
                case 996: //Multigly
                    tpPlayer(client.Character, 98566657, 43, DirectionsEnum.DIRECTION_EAST);
                    client.Character.Inventory.RemoveItem(item, 1);
                    break;
                case 548: //pocima de recuerdo
                    var objectPosition = client.Character.GetSpawnPoint();
                    var NextMap = objectPosition.Map;
                    var Cell = objectPosition.Cell;
                    client.Character.Teleport(NextMap, Cell);
                    break;
                case 14485://mimobionte
                    client.Character.Inventory.Save();
                    client.Send(new ClientUIOpenedByObjectMessage(3, (uint)item.Guid));
                    break;
                case 15236://tesoro
                    tpPlayer(client.Character, 142088718, 424, DirectionsEnum.DIRECTION_EAST);
                    client.Character.Inventory.RemoveItem(item, 1);
                    break;
                case 2542://hogar
                    if (client.Character.HasEmote(EmotesEnum.EMOTE_GUILD))
                    {
                        var meh = client.Character.Guild.Id;
                        if (meh == 3 || meh == 5)
                        {
                            tpPlayer(client.Character, 85459457, 430, DirectionsEnum.DIRECTION_NORTH_EAST);
                            client.Character.Inventory.RemoveItem(item, 1);
                        }else if(meh == 5000) {
                            tpPlayer(client.Character, 59768832, 382, DirectionsEnum.DIRECTION_NORTH_EAST);
                            client.Character.Inventory.RemoveItem(item, 1);
                        }
                        else
                        {
                            client.Character.SendServerMessage("Tu gremio tiene que tener acceso a una base de operaciones para utilizarla.", Color.DarkOrange);
                        }

                    }
                    else
                    {
                        client.Character.SendServerMessage("Únete a un gremio para utilizarla.", Color.DarkOrange);
                    }
                    break;
                case 15163://diligencias
                    Dictionary<Map, int> diligencias = new Dictionary<Map, int>();

                    if (client.Character.Achievement.FinishedAchievements.Any(x => x.Id == 345)) //Astrub
                    {
                        diligencias.Add(World.Instance.GetMap(189531147), 171);
                    }
                    if (client.Character.Achievement.FinishedAchievements.Any(x => x.Id == 302)) //Bonta
                    {
                        diligencias.Add(World.Instance.GetMap(158859268), 228);
                    }
                    if (client.Character.Achievement.FinishedAchievements.Any(x => x.Id == 1603)) //Brakmar
                    {
                        diligencias.Add(World.Instance.GetMap(180355584), 214);
                    }
                    if (client.Character.Achievement.FinishedAchievements.Any(x => x.Id == 300)) //Cania
                    {
                        diligencias.Add(World.Instance.GetMap(167248904), 289);
                    }

                    if (diligencias.Count > 0)
                    {
                        DungsDialog diligenciaPortal = new DungsDialog(client.Character, diligencias);
                        diligenciaPortal.Open();
                        client.Character.Inventory.RemoveItem(item, 1);
                    }
                    else
                    {
                        client.Character.SendServerMessage("Primero tienes que explorar esas zonas para poder teletransportarte.", Color.DarkOrange);
                    }
                    break;
                case 15237://viajero
                    tpPlayer(client.Character, 130025985, 399, DirectionsEnum.DIRECTION_EAST);
                    client.Character.Inventory.RemoveItem(item, 1);
                    break;
                case 14447://alianzas
                    tpPlayer(client.Character, 115083777, 403, DirectionsEnum.DIRECTION_EAST);
                    client.Character.Inventory.RemoveItem(item, 1);
                    break;
                case 14488://Cocahowia
                    Dictionary<Map, int> perforadoras = new Dictionary<Map, int>();

                    if (client.Character.Achievement.FinishedAchievements.Any(x => x.Id == 244)) //Mililameda   
                    {
                        perforadoras.Add(World.Instance.GetMap(88081171), 256);
                    }
                    if (client.Character.Achievement.FinishedAchievements.Any(x => x.Id == 1037)) //Tunel de rokarton
                    {
                        perforadoras.Add(World.Instance.GetMap(117440514), 328);
                    }
                    if (client.Character.Achievement.FinishedAchievements.Any(x => x.Id == 277)) //Moon
                    {
                        perforadoras.Add(World.Instance.GetMap(156762624), 231);
                    }
                    if (client.Character.Achievement.FinishedAchievements.Any(x => x.Id == 257)) //Piedemonte de los Crujidores ,interior Mina
                    {
                        perforadoras.Add(World.Instance.GetMap(97259013), 506);
                    }
                    if (client.Character.Achievement.FinishedAchievements.Any(x => x.Id == 242)) //Bosque de amakna, interior mina
                    {
                        perforadoras.Add(World.Instance.GetMap(97259009), 243);
                    }
                    if (client.Character.Achievement.FinishedAchievements.Any(x => x.Id == 268)) //Territorio de los Porcos
                    {
                        perforadoras.Add(World.Instance.GetMap(30671113), 300);
                    }
                    if (client.Character.Achievement.FinishedAchievements.Any(x => x.Id == 336)) //Tierras Desoladas,interior mina
                    {
                        perforadoras.Add(World.Instance.GetMap(178784256), 163);
                    }
                    if (client.Character.Achievement.FinishedAchievements.Any(x => x.Id == 380)) //Vallekemata, interior mina
                    {
                        perforadoras.Add(World.Instance.GetMap(63700992), 383);
                    }
                    if (client.Character.Achievement.FinishedAchievements.Any(x => x.Id == 304)) // Dientes de piedra
                    {
                        perforadoras.Add(World.Instance.GetMap(126879233), 341);
                    }
                    if (client.Character.Achievement.FinishedAchievements.Any(x => x.Id == 300)) //Bahia de cania
                    {
                        perforadoras.Add(World.Instance.GetMap(167248904), 287);
                    }
                    if (client.Character.Achievement.FinishedAchievements.Any(x => x.Id == 398)) //Frigost, interior mina
                    {
                        perforadoras.Add(World.Instance.GetMap(57017859), 299);
                    }
                    if (client.Character.Achievement.FinishedAchievements.Any(x => x.Id == 384)) //Otomai
                    {
                        perforadoras.Add(World.Instance.GetMap(63964931), 352);
                    }
                    if (client.Character.Achievement.FinishedAchievements.Any(x => x.Id == 365)) //Pandala
                    {
                        perforadoras.Add(World.Instance.GetMap(14622), 161);
                    }

                    if (perforadoras.Count > 0)
                    {
                        DungsDialog perforadorasPortal = new DungsDialog(client.Character, perforadoras);
                        perforadorasPortal.Open();
                        client.Character.Inventory.RemoveItem(item, 1);
                    }
                    else
                    {
                        client.Character.SendServerMessage("Primero tienes que explorar esas zonas para poder teletransportarte.", Color.DarkOrange);
                    }
                    break;
                case 13442://Bibliotemplo
                    tpPlayer(client.Character, 84805890, 356, DirectionsEnum.DIRECTION_EAST);
                    client.Character.Inventory.RemoveItem(item, 1);
                    break;
                case 20221://Pócima de Meriana
                    tpPlayer(client.Character, 134403, 388, DirectionsEnum.DIRECTION_EAST);
                    client.Character.Inventory.RemoveItem(item, 1);
                    break;
                case 9035://Pócima de cercado de gremio
                    Dictionary<Map, int> salteadorillos = new Dictionary<Map, int>();

                    if (client.Character.Achievement.FinishedAchievements.Any(x => x.Id == 262)) //Rio Kawaii
                    {
                        salteadorillos.Add(World.Instance.GetMap(68551174), 314);
                    }
                    if (client.Character.Achievement.FinishedAchievements.Any(x => x.Id == 257)) //Piedemonte Crujidores
                    {
                        salteadorillos.Add(World.Instance.GetMap(88212751), 303);
                    }
                    if (client.Character.Achievement.FinishedAchievements.Any(x => x.Id == 247)) //Gelatinas
                    {
                        salteadorillos.Add(World.Instance.GetMap(88085511), 396);
                    }
                    if (client.Character.Achievement.FinishedAchievements.Any(x => x.Id == 246)) //Dragohuevos
                    {
                        salteadorillos.Add(World.Instance.GetMap(84411907), 442);
                    }
                    if (client.Character.Achievement.FinishedAchievements.Any(x => x.Id == 277)) //Moon
                    {
                        salteadorillos.Add(World.Instance.GetMap(156762112), 317);
                    }
                    if (client.Character.Achievement.FinishedAchievements.Any(x => x.Id == 272)) //Wabbit
                    {
                        salteadorillos.Add(World.Instance.GetMap(99746311), 465);
                    }
                    if (client.Character.Achievement.FinishedAchievements.Any(x => x.Id == 372)) //Pandala Aire
                    {
                        salteadorillos.Add(World.Instance.GetMap(9511), 426);
                    }
                    if (client.Character.Achievement.FinishedAchievements.Any(x => x.Id == 308)) //Campos de Cania
                    {
                        salteadorillos.Add(World.Instance.GetMap(142085634), 374);
                    }
                    if (client.Character.Achievement.FinishedAchievements.Any(x => x.Id == 382)) //Isla Minutauro
                    {
                        salteadorillos.Add(World.Instance.GetMap(152338), 412);
                    }
                    if (client.Character.Achievement.FinishedAchievements.Any(x => x.Id == 319)) //Brakmar
                    {
                        salteadorillos.Add(World.Instance.GetMap(172231680), 397);
                    }
                    if (client.Character.Achievement.FinishedAchievements.Any(x => x.Id == 390)) //Otomai
                    {
                        salteadorillos.Add(World.Instance.GetMap(160257), 383);
                    }
                    if (client.Character.Achievement.FinishedAchievements.Any(x => x.Id == 406)) //Frigost
                    {
                        salteadorillos.Add(World.Instance.GetMap(54168865), 397);
                    }

                    if (salteadorillos.Count > 0)
                    {
                        DungsDialog salteadorillosPortal = new DungsDialog(client.Character, salteadorillos);
                        salteadorillosPortal.Open();
                        client.Character.Inventory.RemoveItem(item, 1);
                    }
                    else
                    {
                        client.Character.SendServerMessage("Primero tienes que explorar esas zonas para poder teletransportarte.", Color.DarkOrange);
                    }
                    break;
                case 15413://Pócima de Apísafe
                    tpPlayer(client.Character, 153360, 341, DirectionsEnum.DIRECTION_EAST);
                    client.Character.Inventory.RemoveItem(item, 1);
                    break;
                case 14573://Pócima de Panzudo
                    tpPlayer(client.Character, 153878787, 514, DirectionsEnum.DIRECTION_EAST);
                    client.Character.Inventory.RemoveItem(item, 1);
                    break;
                case 10973://Pócima de destino desconocido
                    tpPlayer(client.Character, 147850241, 134, DirectionsEnum.DIRECTION_EAST);
                    client.Character.Inventory.RemoveItem(item, 1);
                    break;
                // ------------ Pergaminos de XP ----------- //
                case 679: //Blanco
                    client.Character.AddExperience(200);
                    break;
                case 678: //Marfil
                    client.Character.AddExperience(2000);
                    break;
                case 680: //Dorado
                    client.Character.AddExperience(20000);
                    break;
                // ----------------- Prisma ---------------- //
                case 14293:
                    client.Character.Inventory.RemoveItem(item, 1);
                    break;

                // -------------- Cofres Loteria ------------- //
                case 15270:
                    Random rnd = new Random();
                    var randomfinded = rnd.Next(0, 8);

                    switch (randomfinded)
                    {
                        case 1:
                            var cofreS = Singleton<ItemManager>.Instance.TryGetTemplate(15264); //Cofre solido
                            client.Character.Inventory.AddItem(cofreS, 1);
                            break;
                        case 2:
                            var cofreM = Singleton<ItemManager>.Instance.TryGetTemplate(15269); //Cofre magristral
                            client.Character.Inventory.AddItem(cofreM, 1);
                            break;
                        case 3:
                            var cofreI = Singleton<ItemManager>.Instance.TryGetTemplate(15267); //Cofre imponente
                            client.Character.Inventory.AddItem(cofreI, 1);
                            break;
                        case 4:
                            var cofreIns = Singleton<ItemManager>.Instance.TryGetTemplate(15248); //Cofre insignificante
                            client.Character.Inventory.AddItem(cofreIns, 1);
                            break;
                        case 5:
                            var cofreSor = Singleton<ItemManager>.Instance.TryGetTemplate(15262); //Cofre sorprendente
                            client.Character.Inventory.AddItem(cofreSor, 1);
                            break;
                        case 6:
                            var cofreP = Singleton<ItemManager>.Instance.TryGetTemplate(15265); //Cofre pesado
                            client.Character.Inventory.AddItem(cofreP, 1);
                            break;
                        case 7:
                            var cofreC = Singleton<ItemManager>.Instance.TryGetTemplate(15266); //Cofre chispeante
                            client.Character.Inventory.AddItem(cofreC, 1);
                            break;
                        case 8:
                            var cofreO = Singleton<ItemManager>.Instance.TryGetTemplate(15260); //Cofre oxidado
                            client.Character.Inventory.AddItem(cofreO, 1);
                            break;
                        default:
                            var cofreLoteria = Singleton<ItemManager>.Instance.TryGetTemplate(15270); //Cofre Loteria
                            client.Character.Inventory.AddItem(cofreLoteria, 1);
                            break;
                    }

                    client.Character.Inventory.RemoveItem(item, 1);
                    break;
                case 15264:    //Cofre solido               
                    var pergaPesac = Singleton<ItemManager>.Instance.TryGetTemplate(10405); //Pergamino de pescador
                    var pergaLeña = Singleton<ItemManager>.Instance.TryGetTemplate(695); //Pergamino de leñador
                    var pergaAlqi = Singleton<ItemManager>.Instance.TryGetTemplate(10402); //Pergamino de alquimista
                    var rosas = Singleton<ItemManager>.Instance.TryGetTemplate(15263); //rosas
                    client.Character.Inventory.AddItem(pergaPesac, 1);
                    client.Character.Inventory.AddItem(pergaLeña, 1);
                    client.Character.Inventory.AddItem(pergaAlqi, 1);
                    client.Character.Inventory.AddItem(rosas, 20);
                    client.Character.Inventory.AddKamas(1500);
                    break;
                case 15269:    //Cofre magristral               
                    var pergaMine = Singleton<ItemManager>.Instance.TryGetTemplate(10398); //Pergamino de minero
                    var pergaCampe = Singleton<ItemManager>.Instance.TryGetTemplate(10403); //Pergamino de campesino
                    var pergaCaza = Singleton<ItemManager>.Instance.TryGetTemplate(10404); //Pergamino de cazador
                    var rosas1 = Singleton<ItemManager>.Instance.TryGetTemplate(15263); //rosas
                    client.Character.Inventory.AddItem(pergaMine, 1);
                    client.Character.Inventory.AddItem(pergaCampe, 1);
                    client.Character.Inventory.AddItem(pergaCaza, 1);
                    client.Character.Inventory.AddItem(rosas1, 60);
                    client.Character.Inventory.AddKamas(2000);
                    break;
                case 15267:    //Cofre imponente               
                    var pergaZaMa = Singleton<ItemManager>.Instance.TryGetTemplate(10407); //Pergamino de zapateromago
                    var pergaSaMa = Singleton<ItemManager>.Instance.TryGetTemplate(10401); //Pergamino de sastremago
                    var rosas2 = Singleton<ItemManager>.Instance.TryGetTemplate(15263); //rosas
                    client.Character.Inventory.AddItem(pergaZaMa, 1);
                    client.Character.Inventory.AddItem(pergaSaMa, 1);
                    client.Character.Inventory.AddItem(rosas2, 80);
                    client.Character.Inventory.AddKamas(2500);
                    break;
                case 15248:    //Cofre insignificante               
                    var pergaBlan = Singleton<ItemManager>.Instance.TryGetTemplate(679); //Pergamino blanco
                    var pergaMarf = Singleton<ItemManager>.Instance.TryGetTemplate(678); //Pergamino marfil
                    var rosas3 = Singleton<ItemManager>.Instance.TryGetTemplate(15263); //rosas
                    client.Character.Inventory.AddItem(pergaBlan, 1);
                    client.Character.Inventory.AddItem(pergaMarf, 1);
                    client.Character.Inventory.AddItem(rosas3, 100);
                    client.Character.Inventory.AddKamas(3000);
                    break;
                case 15262:    //Cofre sorprendente               
                    var pergaEsMa = Singleton<ItemManager>.Instance.TryGetTemplate(10394); //Pergamino escultomago
                    var pergaFoMa = Singleton<ItemManager>.Instance.TryGetTemplate(10386); //Pergamino forjamago
                    var pergaJoMa = Singleton<ItemManager>.Instance.TryGetTemplate(10383); //Pergamino joyeromago
                    var rosas4 = Singleton<ItemManager>.Instance.TryGetTemplate(15263); //rosas
                    client.Character.Inventory.AddItem(pergaEsMa, 1);
                    client.Character.Inventory.AddItem(pergaFoMa, 1);
                    client.Character.Inventory.AddItem(pergaJoMa, 1);
                    client.Character.Inventory.AddItem(rosas4, 134);
                    client.Character.Inventory.AddKamas(3500);
                    break;
                case 15265:    //Cofre pesado               
                    var pergaJoye = Singleton<ItemManager>.Instance.TryGetTemplate(10382); //Pergamino joyero
                    var pergaMani = Singleton<ItemManager>.Instance.TryGetTemplate(10385); //Pergamino manitas
                    var pergaSas = Singleton<ItemManager>.Instance.TryGetTemplate(10400); //Pergamino sastre
                    var rosas5 = Singleton<ItemManager>.Instance.TryGetTemplate(15263); //rosas
                    client.Character.Inventory.AddItem(pergaJoye, 1);
                    client.Character.Inventory.AddItem(pergaMani, 1);
                    client.Character.Inventory.AddItem(pergaSas, 1);
                    client.Character.Inventory.AddItem(rosas5, 157);
                    client.Character.Inventory.AddKamas(4000);
                    break;
                case 15266:    //Cofre chispeante               
                    var pergaZapa = Singleton<ItemManager>.Instance.TryGetTemplate(879); //Pergamino zapatero
                    var pergaEscu = Singleton<ItemManager>.Instance.TryGetTemplate(715); //Pergamino escultor
                    var pergaFab = Singleton<ItemManager>.Instance.TryGetTemplate(10397); //Pergamino fabricante
                    var pergaHer = Singleton<ItemManager>.Instance.TryGetTemplate(713); //Pergamino herrero
                    var rosas6 = Singleton<ItemManager>.Instance.TryGetTemplate(15263); //rosas
                    client.Character.Inventory.AddItem(pergaZapa, 1);
                    client.Character.Inventory.AddItem(pergaEscu, 1);
                    client.Character.Inventory.AddItem(pergaFab, 1);
                    client.Character.Inventory.AddItem(pergaHer, 1);
                    client.Character.Inventory.AddItem(rosas6, 213);
                    client.Character.Inventory.AddKamas(4500);
                    break;
                case 15260:    //Cofre oxidado               
                    var pergaDora = Singleton<ItemManager>.Instance.TryGetTemplate(680); //Pergamino zapatero
                    var loteria = Singleton<ItemManager>.Instance.TryGetTemplate(7220); //Loteria
                    var rosas7 = Singleton<ItemManager>.Instance.TryGetTemplate(15263); //rosas
                    client.Character.Inventory.AddItem(pergaDora, 1);
                    client.Character.Inventory.AddItem(loteria, 1);
                    client.Character.Inventory.AddItem(rosas7, 367);
                    client.Character.Inventory.AddKamas(5000);
                    break;

                //--- Pocima de Hogar y Pocima de gremio --- //
                case 8883:// Teleport entre prismas //Pocima de gremio
                    if (client.Character.HasEmote(EmotesEnum.EMOTE_ALLIANCE))
                    {
                        Dictionary<Map, int> destinations = new Dictionary<Map, int>();
                        foreach (var prisma in World.Instance.GetSubAreas())
                        {
                            if (prisma.HasPrism && prisma.Prism.Alliance.Id == client.Character.Guild.Alliance.Id)
                            {
                                destinations.Add(World.Instance.GetMap(prisma.Prism.Map.Id), prisma.Prism.Cell.Id);
                            }
                        }

                        DungsDialog s = new DungsDialog(client.Character, destinations);
                        s.Open();
                        client.Character.Inventory.RemoveItem(item, 1);
                    }
                    else
                    {
                        client.Character.SendServerMessage("Únete primero a una Alianza.", Color.DarkOrange);
                    }
                    break;
                // ------- Pergaminos de XP de Oficio ------- //
                case 879:    //Zapatero               
                    client.Character.Jobs[15].Experience += 100;
                    client.Character.Inventory.RemoveItem(item, 1);
                    break;
                case 10402:    //Alquimista               
                    client.Character.Jobs[26].Experience += 100;
                    client.Character.Inventory.RemoveItem(item, 1);
                    break;
                case 10403:    //Campesino               
                    client.Character.Jobs[28].Experience += 100;
                    client.Character.Inventory.RemoveItem(item, 1);
                    break;
                case 10404:    //Cazador               
                    client.Character.Jobs[41].Experience += 100;
                    client.Character.Inventory.RemoveItem(item, 1);
                    break;
                case 10394:    //EscultoMago               
                    client.Character.Jobs[48].Experience += 100;
                    client.Character.Inventory.RemoveItem(item, 1);
                    break;
                case 715:    //Escultor               
                    client.Character.Jobs[13].Experience += 100;
                    client.Character.Inventory.RemoveItem(item, 1);
                    break;
                case 10397:    //Fabricante               
                    client.Character.Jobs[60].Experience += 100;
                    client.Character.Inventory.RemoveItem(item, 1);
                    break;
                case 10386:    //ForjaMago               
                    client.Character.Jobs[44].Experience += 100;
                    client.Character.Inventory.RemoveItem(item, 1);
                    break;
                case 713:    //Herrero               
                    client.Character.Jobs[11].Experience += 100;
                    client.Character.Inventory.RemoveItem(item, 1);
                    break;
                case 10382:    //Joyero               
                    client.Character.Jobs[16].Experience += 100;
                    client.Character.Inventory.RemoveItem(item, 1);
                    break;
                case 10383:    //JoyeroMago               
                    client.Character.Jobs[63].Experience += 100;
                    client.Character.Inventory.RemoveItem(item, 1);
                    break;
                case 695:    //Leñador               
                    client.Character.Jobs[2].Experience += 100;
                    client.Character.Inventory.RemoveItem(item, 1);
                    break;
                case 10385:    //Manitas               
                    client.Character.Jobs[65].Experience += 100;
                    client.Character.Inventory.RemoveItem(item, 1);
                    break;
                case 10398:    //Minero               
                    client.Character.Jobs[24].Experience += 100;
                    client.Character.Inventory.RemoveItem(item, 1);
                    break;
                case 10405:    //Pescador               
                    client.Character.Jobs[36].Experience += 100;
                    client.Character.Inventory.RemoveItem(item, 1);
                    break;
                case 10400:    //Sastre               
                    client.Character.Jobs[27].Experience += 100;
                    client.Character.Inventory.RemoveItem(item, 1);
                    break;
                case 10401:    //SastreMago            
                    client.Character.Jobs[64].Experience += 100;
                    client.Character.Inventory.RemoveItem(item, 1);
                    break;
                case 10407:    //ZapateroMago               
                    client.Character.Jobs[62].Experience += 100;
                    client.Character.Inventory.RemoveItem(item, 1);
                    break;
                // ----------------- Elixires ---------------- //
                case 10210://Aniripsa
                    tpPlayer(client.Character, 88083734, 451, DirectionsEnum.DIRECTION_EAST);
                    client.Character.Inventory.RemoveItem(item, 1);
                    break;
                case 10217://Anutrof
                    tpPlayer(client.Character, 185862661, 378, DirectionsEnum.DIRECTION_EAST);
                    client.Character.Inventory.RemoveItem(item, 1);
                    break;
                case 10216://Feca
                    tpPlayer(client.Character, 88086290, 341, DirectionsEnum.DIRECTION_EAST);
                    client.Character.Inventory.RemoveItem(item, 1);
                    break;
                case 17529://Hipermago
                    tpPlayer(client.Character, 163053568, 432, DirectionsEnum.DIRECTION_EAST);
                    client.Character.Inventory.RemoveItem(item, 1);
                    break;
                case 10208://Pandawa
                    tpPlayer(client.Character, 88082201, 468, DirectionsEnum.DIRECTION_EAST);
                    client.Character.Inventory.RemoveItem(item, 1);
                    break;
                case 10209://Sacro
                    tpPlayer(client.Character, 185861637, 257, DirectionsEnum.DIRECTION_EAST);
                    client.Character.Inventory.RemoveItem(item, 1);
                    break;
                case 10211://Sadida
                    tpPlayer(client.Character, 88212750, 370, DirectionsEnum.DIRECTION_EAST);
                    client.Character.Inventory.RemoveItem(item, 1);
                    break;
                case 10212://Ocra
                    tpPlayer(client.Character, 88212244, 384, DirectionsEnum.DIRECTION_EAST);
                    client.Character.Inventory.RemoveItem(item, 1);
                    break;
                case 10213://Yopuka
                    tpPlayer(client.Character, 88080660, 343, DirectionsEnum.DIRECTION_EAST);
                    client.Character.Inventory.RemoveItem(item, 1);
                    break;
                case 10214://Zurcarak
                    tpPlayer(client.Character, 185863684, 369, DirectionsEnum.DIRECTION_EAST);
                    client.Character.Inventory.RemoveItem(item, 1);
                    break;
                case 10215://Xelor
                    tpPlayer(client.Character, 88081686, 387, DirectionsEnum.DIRECTION_EAST);
                    client.Character.Inventory.RemoveItem(item, 1);
                    break;
                case 10218://Osamoda
                    tpPlayer(client.Character, 88084245, 386, DirectionsEnum.DIRECTION_EAST);
                    client.Character.Inventory.RemoveItem(item, 1);
                    break;
                case 10219://Sram
                    tpPlayer(client.Character, 88214295, 164, DirectionsEnum.DIRECTION_EAST);
                    client.Character.Inventory.RemoveItem(item, 1);
                    break;
                case 12156://Tymador
                    tpPlayer(client.Character, 82314240, 289, DirectionsEnum.DIRECTION_EAST);
                    client.Character.Inventory.RemoveItem(item, 1);
                    break;
                case 12228://Zobal
                    tpPlayer(client.Character, 69206274, 318, DirectionsEnum.DIRECTION_EAST);
                    client.Character.Inventory.RemoveItem(item, 1);
                    break;
                case 13278://Streamer
                    tpPlayer(client.Character, 95423492, 344, DirectionsEnum.DIRECTION_EAST);
                    client.Character.Inventory.RemoveItem(item, 1);
                    break;
                case 16135://Selatrop
                    tpPlayer(client.Character, 148637187, 471, DirectionsEnum.DIRECTION_EAST);
                    client.Character.Inventory.RemoveItem(item, 1);
                    break;
                case 18620://Uginak
                    tpPlayer(client.Character, 176555008, 295, DirectionsEnum.DIRECTION_EAST);
                    client.Character.Inventory.RemoveItem(item, 1);
                    break;
                // ----------------- Sacos de Recursos ---------------- //
                case 7941: //Saco de trigo
                    var saco = Singleton<ItemManager>.Instance.TryGetTemplate(289); //Trigo
                    client.Character.Inventory.AddItem(saco, 50);
                    client.Character.Inventory.RemoveItem(item, 1);
                    break;
                case 7942: //Saco de cevada
                    var saco1 = Singleton<ItemManager>.Instance.TryGetTemplate(400); //cevada
                    client.Character.Inventory.AddItem(saco1, 50);
                    client.Character.Inventory.RemoveItem(item, 1);
                    break;
                case 7943: //Saco de avena
                    var saco2 = Singleton<ItemManager>.Instance.TryGetTemplate(533); //avena
                    client.Character.Inventory.AddItem(saco2, 50);
                    client.Character.Inventory.RemoveItem(item, 1);
                    break;
                case 7944: //Saco de lupulo
                    var saco3 = Singleton<ItemManager>.Instance.TryGetTemplate(401); //lupulo
                    client.Character.Inventory.AddItem(saco3, 50);
                    client.Character.Inventory.RemoveItem(item, 1);
                    break;
                case 7945: //Saco de lino
                    var saco4 = Singleton<ItemManager>.Instance.TryGetTemplate(423); //lupulo
                    client.Character.Inventory.AddItem(saco4, 50);
                    client.Character.Inventory.RemoveItem(item, 1);
                    break;
                case 7946: //Saco de centeno
                    var saco5 = Singleton<ItemManager>.Instance.TryGetTemplate(532); //centeno
                    client.Character.Inventory.AddItem(saco5, 50);
                    client.Character.Inventory.RemoveItem(item, 1);
                    break;
                case 7947: //Saco de arroz
                    var saco6 = Singleton<ItemManager>.Instance.TryGetTemplate(7018); //arroz
                    client.Character.Inventory.AddItem(saco6, 50);
                    client.Character.Inventory.RemoveItem(item, 1);
                    break;
                case 7948: //Saco de malta
                    var saco7 = Singleton<ItemManager>.Instance.TryGetTemplate(405); //malta
                    client.Character.Inventory.AddItem(saco7, 50);
                    client.Character.Inventory.RemoveItem(item, 1);
                    break;
                case 7949: //Saco de cañamo
                    var saco8 = Singleton<ItemManager>.Instance.TryGetTemplate(425); //cañamo
                    client.Character.Inventory.AddItem(saco8, 50);
                    client.Character.Inventory.RemoveItem(item, 1);
                    break;
                case 7950: //Saco de fresno
                    var saco9 = Singleton<ItemManager>.Instance.TryGetTemplate(303); //fresno
                    client.Character.Inventory.AddItem(saco9, 50);
                    client.Character.Inventory.RemoveItem(item, 1);
                    break;
                case 7951: //Saco de castaño
                    var saco10 = Singleton<ItemManager>.Instance.TryGetTemplate(473); //castaño
                    client.Character.Inventory.AddItem(saco10, 50);
                    client.Character.Inventory.RemoveItem(item, 1);
                    break;
                case 7952: //Saco de nogal
                    var saco11 = Singleton<ItemManager>.Instance.TryGetTemplate(476); //nogal
                    client.Character.Inventory.AddItem(saco11, 50);
                    client.Character.Inventory.RemoveItem(item, 1);
                    break;
                case 7953: //Saco de roble
                    var saco12 = Singleton<ItemManager>.Instance.TryGetTemplate(460); //roble
                    client.Character.Inventory.AddItem(saco12, 50);
                    client.Character.Inventory.RemoveItem(item, 1);
                    break;
                case 7954: //Saco de bombu
                    var saco13 = Singleton<ItemManager>.Instance.TryGetTemplate(2358); //bombu
                    client.Character.Inventory.AddItem(saco13, 50);
                    client.Character.Inventory.RemoveItem(item, 1);
                    break;
                case 7955: //Saco de olivioleta
                    var saco14 = Singleton<ItemManager>.Instance.TryGetTemplate(2357); //olivioleta
                    client.Character.Inventory.AddItem(saco14, 50);
                    client.Character.Inventory.RemoveItem(item, 1);
                    break;
                case 7956: //Saco de arce
                    var saco15 = Singleton<ItemManager>.Instance.TryGetTemplate(471); //arce
                    client.Character.Inventory.AddItem(saco15, 50);
                    client.Character.Inventory.RemoveItem(item, 1);
                    break;
                case 7957: //Saco de tejo
                    var saco16 = Singleton<ItemManager>.Instance.TryGetTemplate(461); //tejo
                    client.Character.Inventory.AddItem(saco16, 50);
                    client.Character.Inventory.RemoveItem(item, 1);
                    break;
                case 7958: //Saco de bambu
                    var saco17 = Singleton<ItemManager>.Instance.TryGetTemplate(7013); //bambu
                    client.Character.Inventory.AddItem(saco17, 50);
                    client.Character.Inventory.RemoveItem(item, 1);
                    break;
                case 7959: //Saco de cerezo
                    var saco18 = Singleton<ItemManager>.Instance.TryGetTemplate(474); //cerezo
                    client.Character.Inventory.AddItem(saco18, 50);
                    client.Character.Inventory.RemoveItem(item, 1);
                    break;
                case 7960: //Saco de ebano
                    var saco19 = Singleton<ItemManager>.Instance.TryGetTemplate(449); //ebano
                    client.Character.Inventory.AddItem(saco19, 50);
                    client.Character.Inventory.RemoveItem(item, 1);
                    break;
                case 7961: //Saco de bambu oscuro
                    var saco20 = Singleton<ItemManager>.Instance.TryGetTemplate(7016); //bambu oscuro
                    client.Character.Inventory.AddItem(saco20, 50);
                    client.Character.Inventory.RemoveItem(item, 1);
                    break;
                case 7962: //Saco de olmo
                    var saco21 = Singleton<ItemManager>.Instance.TryGetTemplate(470); //olmo
                    client.Character.Inventory.AddItem(saco21, 50);
                    client.Character.Inventory.RemoveItem(item, 1);
                    break;
                case 7963: //Saco de bambu sagrado
                    var saco22 = Singleton<ItemManager>.Instance.TryGetTemplate(7014); //bambu sagrado
                    client.Character.Inventory.AddItem(saco22, 50);
                    client.Character.Inventory.RemoveItem(item, 1);
                    break;
                case 7964: //Saco de ortigas
                    var saco23 = Singleton<ItemManager>.Instance.TryGetTemplate(421); //ortigas
                    client.Character.Inventory.AddItem(saco23, 50);
                    client.Character.Inventory.RemoveItem(item, 1);
                    break;
                case 7965: //Saco de salvia
                    var saco24 = Singleton<ItemManager>.Instance.TryGetTemplate(428); //salvia
                    client.Character.Inventory.AddItem(saco24, 50);
                    client.Character.Inventory.RemoveItem(item, 1);
                    break;
                case 7966: //Saco de treboles
                    var saco25 = Singleton<ItemManager>.Instance.TryGetTemplate(395); //treboles
                    client.Character.Inventory.AddItem(saco25, 50);
                    client.Character.Inventory.RemoveItem(item, 1);
                    break;
                case 7967: //Saco de menta
                    var saco26 = Singleton<ItemManager>.Instance.TryGetTemplate(380); //menta
                    client.Character.Inventory.AddItem(saco26, 50);
                    client.Character.Inventory.RemoveItem(item, 1);
                    break;
                case 7968: //Saco de orquidea
                    var saco27 = Singleton<ItemManager>.Instance.TryGetTemplate(593); //orquidea
                    client.Character.Inventory.AddItem(saco27, 50);
                    client.Character.Inventory.RemoveItem(item, 1);
                    break;
                case 7969: //Saco de edelweiss
                    var saco28 = Singleton<ItemManager>.Instance.TryGetTemplate(594); //edelweiss
                    client.Character.Inventory.AddItem(saco28, 50);
                    client.Character.Inventory.RemoveItem(item, 1);
                    break;
                case 7970: //Saco de pandoja
                    var saco29 = Singleton<ItemManager>.Instance.TryGetTemplate(7059); //pandoja
                    client.Character.Inventory.AddItem(saco29, 50);
                    client.Character.Inventory.RemoveItem(item, 1);
                    break;
                case 7971: //Saco de hierro
                    var saco30 = Singleton<ItemManager>.Instance.TryGetTemplate(312); //hierro
                    client.Character.Inventory.AddItem(saco30, 50);
                    client.Character.Inventory.RemoveItem(item, 1);
                    break;
                case 7972: //Saco de cobre
                    var saco31 = Singleton<ItemManager>.Instance.TryGetTemplate(441); //cobre
                    client.Character.Inventory.AddItem(saco31, 50);
                    client.Character.Inventory.RemoveItem(item, 1);
                    break;
                case 7973: //Saco de bronce
                    var saco32 = Singleton<ItemManager>.Instance.TryGetTemplate(442); //bronce
                    client.Character.Inventory.AddItem(saco32, 50);
                    client.Character.Inventory.RemoveItem(item, 1);
                    break;
                case 7974: //Saco de kobalto
                    var saco33 = Singleton<ItemManager>.Instance.TryGetTemplate(443); //kobalto
                    client.Character.Inventory.AddItem(saco33, 50);
                    client.Character.Inventory.RemoveItem(item, 1);
                    break;
                case 7975: //Saco de manganeso
                    var saco34 = Singleton<ItemManager>.Instance.TryGetTemplate(445); //manganeso
                    client.Character.Inventory.AddItem(saco34, 50);
                    client.Character.Inventory.RemoveItem(item, 1);
                    break;
                case 7976: //Saco de estaño
                    var saco35 = Singleton<ItemManager>.Instance.TryGetTemplate(444); //estaño
                    client.Character.Inventory.AddItem(saco35, 50);
                    client.Character.Inventory.RemoveItem(item, 1);
                    break;
                case 7977: //Saco de silicato
                    var saco36 = Singleton<ItemManager>.Instance.TryGetTemplate(7032); //silicato
                    client.Character.Inventory.AddItem(saco36, 50);
                    client.Character.Inventory.RemoveItem(item, 1);
                    break;
                case 7978: //Saco de plata
                    var saco37 = Singleton<ItemManager>.Instance.TryGetTemplate(350); //plata
                    client.Character.Inventory.AddItem(saco37, 50);
                    client.Character.Inventory.RemoveItem(item, 1);
                    break;
                case 7979: //Saco de bauxita
                    var saco38 = Singleton<ItemManager>.Instance.TryGetTemplate(446); //bauxita
                    client.Character.Inventory.AddItem(saco38, 50);
                    client.Character.Inventory.RemoveItem(item, 1);
                    break;
                case 7980: //Saco de oro
                    var saco39 = Singleton<ItemManager>.Instance.TryGetTemplate(313); //oro
                    client.Character.Inventory.AddItem(saco39, 50);
                    client.Character.Inventory.RemoveItem(item, 1);
                    break;
                case 7981: //Saco de dolomita
                    var saco40 = Singleton<ItemManager>.Instance.TryGetTemplate(7033); //dolomita
                    client.Character.Inventory.AddItem(saco40, 50);
                    client.Character.Inventory.RemoveItem(item, 1);
                    break;
                case 7982: //Saco de gobios
                    var saco41 = Singleton<ItemManager>.Instance.TryGetTemplate(1782); //gobios
                    client.Character.Inventory.AddItem(saco41, 50);
                    client.Character.Inventory.RemoveItem(item, 1);
                    break;
                case 7983: //Saco de truchas
                    var saco42 = Singleton<ItemManager>.Instance.TryGetTemplate(1844); //truchas
                    client.Character.Inventory.AddItem(saco42, 50);
                    client.Character.Inventory.RemoveItem(item, 1);
                    break;
                case 7984: //Saco de pez gato
                    var saco43 = Singleton<ItemManager>.Instance.TryGetTemplate(603); //pez gato
                    client.Character.Inventory.AddItem(saco43, 50);
                    client.Character.Inventory.RemoveItem(item, 1);
                    break;
                case 7985: //Saco de bamgas
                    var saco44 = Singleton<ItemManager>.Instance.TryGetTemplate(598); //bamgas
                    client.Character.Inventory.AddItem(saco44, 50);
                    client.Character.Inventory.RemoveItem(item, 1);
                    break;
                case 7986: //Saco de crangejos
                    var saco45 = Singleton<ItemManager>.Instance.TryGetTemplate(1757); //crangejos
                    client.Character.Inventory.AddItem(saco45, 50);
                    client.Character.Inventory.RemoveItem(item, 1);
                    break;
                case 7987: //Saco de pescados empanados
                    var saco46 = Singleton<ItemManager>.Instance.TryGetTemplate(1750); //pescados empanados
                    client.Character.Inventory.AddItem(saco46, 50);
                    client.Character.Inventory.RemoveItem(item, 1);
                    break;
                case 7988: //Saco de lucios
                    var saco47 = Singleton<ItemManager>.Instance.TryGetTemplate(1847); //lucios
                    client.Character.Inventory.AddItem(saco47, 50);
                    client.Character.Inventory.RemoveItem(item, 1);
                    break;
                case 7989: //Saco de carpas
                    var saco48 = Singleton<ItemManager>.Instance.TryGetTemplate(1794); //carpas
                    client.Character.Inventory.AddItem(saco48, 50);
                    client.Character.Inventory.RemoveItem(item, 1);
                    break;
                case 7990: //Saco de sardinas
                    var saco49 = Singleton<ItemManager>.Instance.TryGetTemplate(1805); //sardinas
                    client.Character.Inventory.AddItem(saco49, 50);
                    client.Character.Inventory.RemoveItem(item, 1);
                    break;
                case 7991: //Saco de kralamares
                    var saco50 = Singleton<ItemManager>.Instance.TryGetTemplate(600); //kralamares
                    client.Character.Inventory.AddItem(saco50, 50);
                    client.Character.Inventory.RemoveItem(item, 1);
                    break;
                case 7992: //Saco de lubinas
                    var saco51 = Singleton<ItemManager>.Instance.TryGetTemplate(1779); //lubinas
                    client.Character.Inventory.AddItem(saco51, 50);
                    client.Character.Inventory.RemoveItem(item, 1);
                    break;
                case 7993: //Saco de rayas
                    var saco52 = Singleton<ItemManager>.Instance.TryGetTemplate(1784); //rayas
                    client.Character.Inventory.AddItem(saco52, 50);
                    client.Character.Inventory.RemoveItem(item, 1);
                    break;
                case 7994: //Saco de percas
                    var saco53 = Singleton<ItemManager>.Instance.TryGetTemplate(1801); //percas
                    client.Character.Inventory.AddItem(saco53, 50);
                    client.Character.Inventory.RemoveItem(item, 1);
                    break;
                case 7995: //Saco de tiburones
                    var saco54 = Singleton<ItemManager>.Instance.TryGetTemplate(602); //tiburones
                    client.Character.Inventory.AddItem(saco54, 50);
                    client.Character.Inventory.RemoveItem(item, 1);
                    break;
                case 7996: //Saco de kalipto
                    var saco55 = Singleton<ItemManager>.Instance.TryGetTemplate(7925); //kalipto
                    client.Character.Inventory.AddItem(saco55, 50);
                    client.Character.Inventory.RemoveItem(item, 1);
                    break;
                case 8081: //Saco de carpe
                    var saco56 = Singleton<ItemManager>.Instance.TryGetTemplate(472); //carpe
                    client.Character.Inventory.AddItem(saco56, 50);
                    client.Character.Inventory.RemoveItem(item, 1);
                    break;
                case 11103: //Saco de campanillas
                    var saco57 = Singleton<ItemManager>.Instance.TryGetTemplate(11102); //campanillas
                    client.Character.Inventory.AddItem(saco57, 50);
                    client.Character.Inventory.RemoveItem(item, 1);
                    break;
                case 11111: //Saco de inverluzas
                    var saco58 = Singleton<ItemManager>.Instance.TryGetTemplate(11106); //inverluzas
                    client.Character.Inventory.AddItem(saco58, 50);
                    client.Character.Inventory.RemoveItem(item, 1);
                    break;
                case 11112: //Saco de alamo
                    var saco59 = Singleton<ItemManager>.Instance.TryGetTemplate(11107); //alamo
                    client.Character.Inventory.AddItem(saco59, 50);
                    client.Character.Inventory.RemoveItem(item, 1);
                    break;
                case 11113: //Saco de frostizz
                    var saco60 = Singleton<ItemManager>.Instance.TryGetTemplate(11109); //frostizz
                    client.Character.Inventory.AddItem(saco60, 50);
                    client.Character.Inventory.RemoveItem(item, 1);
                    break;
                case 11114: //Saco de obsidiana
                    var saco61 = Singleton<ItemManager>.Instance.TryGetTemplate(11110); //obsidiana
                    client.Character.Inventory.AddItem(saco61, 50);
                    client.Character.Inventory.RemoveItem(item, 1);
                    break;
                case 16528: //Saco de ginseng
                    var saco62 = Singleton<ItemManager>.Instance.TryGetTemplate(16385); //ginseng
                    client.Character.Inventory.AddItem(saco62, 50);
                    client.Character.Inventory.RemoveItem(item, 1);
                    break;
                case 16529: //Saco de belladona
                    var saco63 = Singleton<ItemManager>.Instance.TryGetTemplate(16387); //belladona
                    client.Character.Inventory.AddItem(saco63, 50);
                    client.Character.Inventory.RemoveItem(item, 1);
                    break;
                case 16530: //Saco de mandragora
                    var saco64 = Singleton<ItemManager>.Instance.TryGetTemplate(16389); //mandragora
                    client.Character.Inventory.AddItem(saco64, 50);
                    client.Character.Inventory.RemoveItem(item, 1);
                    break;
                case 16531: //Saco de avellano
                    var saco65 = Singleton<ItemManager>.Instance.TryGetTemplate(16488); //avellano
                    client.Character.Inventory.AddItem(saco65, 50);
                    client.Character.Inventory.RemoveItem(item, 1);
                    break;
                case 16532: //Saco de Maiz
                    var saco66 = Singleton<ItemManager>.Instance.TryGetTemplate(16454); //Maiz
                    client.Character.Inventory.AddItem(saco66, 50);
                    client.Character.Inventory.RemoveItem(item, 1);
                    break;
                case 16533: //Saco de Mijo
                    var saco67 = Singleton<ItemManager>.Instance.TryGetTemplate(16456); //Mijo
                    client.Character.Inventory.AddItem(saco67, 50);
                    client.Character.Inventory.RemoveItem(item, 1);
                    break;
                case 16534: //Saco de anguilas
                    var saco68 = Singleton<ItemManager>.Instance.TryGetTemplate(16461); //anguilas
                    client.Character.Inventory.AddItem(saco68, 50);
                    client.Character.Inventory.RemoveItem(item, 1);
                    break;
                case 16535: //Saco de doradas
                    var saco69 = Singleton<ItemManager>.Instance.TryGetTemplate(16463); //doradas
                    client.Character.Inventory.AddItem(saco69, 50);
                    client.Character.Inventory.RemoveItem(item, 1);
                    break;
                case 16536: //Saco de rapes
                    var saco70 = Singleton<ItemManager>.Instance.TryGetTemplate(16465); //rapes
                    client.Character.Inventory.AddItem(saco70, 50);
                    client.Character.Inventory.RemoveItem(item, 1);
                    break;
                case 16537: //Saco de bacaladillas
                    var saco71 = Singleton<ItemManager>.Instance.TryGetTemplate(16467); //bacaladillas
                    client.Character.Inventory.AddItem(saco71, 50);
                    client.Character.Inventory.RemoveItem(item, 1);
                    break;
                case 16538: //Saco de tenca
                    var saco72 = Singleton<ItemManager>.Instance.TryGetTemplate(16469); //tenca
                    client.Character.Inventory.AddItem(saco72, 50);
                    client.Character.Inventory.RemoveItem(item, 1);
                    break;
                case 16539: //Saco de espada
                    var saco73 = Singleton<ItemManager>.Instance.TryGetTemplate(16471); //espada
                    client.Character.Inventory.AddItem(saco73, 50);
                    client.Character.Inventory.RemoveItem(item, 1);
                    break;
                case 18058: //Saco de acuacia
                    var saco74 = Singleton<ItemManager>.Instance.TryGetTemplate(17991); //acuacia
                    client.Character.Inventory.AddItem(saco74, 50);
                    client.Character.Inventory.RemoveItem(item, 1);
                    break;
                case 18059: //Saco de salikronias
                    var saco75 = Singleton<ItemManager>.Instance.TryGetTemplate(17992); //salikronias
                    client.Character.Inventory.AddItem(saco75, 50);
                    client.Character.Inventory.RemoveItem(item, 1);
                    break;
                case 18060: //Saco de quisnoa
                    var saco76 = Singleton<ItemManager>.Instance.TryGetTemplate(17993); //quisnoa
                    client.Character.Inventory.AddItem(saco76, 50);
                    client.Character.Inventory.RemoveItem(item, 1);
                    break;
                case 18061: //Saco de lapas
                    var saco77 = Singleton<ItemManager>.Instance.TryGetTemplate(17994); //lapas
                    client.Character.Inventory.AddItem(saco77, 50);
                    client.Character.Inventory.RemoveItem(item, 1);
                    break;
                case 18062: //Saco de sepiolita
                    var saco78 = Singleton<ItemManager>.Instance.TryGetTemplate(17995); //sepiolita
                    client.Character.Inventory.AddItem(saco78, 50);
                    client.Character.Inventory.RemoveItem(item, 1);
                    break;
                case 20706: //Saco de reflejos oniricos
                    var saco79 = Singleton<ItemManager>.Instance.TryGetTemplate(20292); //reflejos oniricos
                    client.Character.Inventory.AddItem(saco79, 1000);
                    client.Character.Inventory.RemoveItem(item, 1);
                    break;


            }
        }

        private static void RetomarMazmorar(WorldClient client, BasePlayerItem item)
        {

            if (client.Character.GetRetomarMazmorra() != 0)
            {
                if (!client.Character.Map.IsDungeon())
                {
                    client.Character.SendServerMessage("Acabas de retomar la mazmorra donde moristes.");
                    tpPlayer(client.Character, client.Character.GetRetomarMazmorra(),
                        client.Character.Map.GetRandomFreeCell().Id, DirectionsEnum.DIRECTION_EAST);
                }
                else
                {
                    client.Character.SendServerMessage("No puedes retomar una mazmorra si estás dentro de ella.");
                }
            }
            else
            {
                client.Character.SendServerMessage("No puedes retomar ninguna mazmorra");
            }
        }

        private static void Leyendas(WorldClient client, BasePlayerItem item)
        {
            Random rnd = new Random();
            var randomfinded = rnd.Next(1, 13);
            int id;

            switch (randomfinded)
            {
                case 1:
                    id = 20649; //Leyenda de Bram Barbamundo
                    break;
                case 2:
                    id = 20659; //Leyenda de Buhodorado
                    break;
                case 3:
                    id = 20650; //Leyenda de Cocobur
                    break;
                case 4:
                    id = 20654; //Leyenda de Dodge
                    break;
                case 5:
                    id = 20655; //Leyenda de Fallanster
                    break;
                case 6:
                    id = 20661; //Leyenda de Ganymed
                    break;
                case 7:
                    id = 20660; //Leyenda de Jahas Jurgen
                    break;
                case 8:
                    id = 20652; //Leyenda de Señoria Jhessica
                    break;
                case 9:
                    id = 20653; //Leyenda de Mil Leguas
                    break;
                case 10:
                    id = 20658; //Leyenda de Rykke Errel
                    break;
                case 11:
                    id = 20651; //Leyenda del Burlador de la Muerte
                    break;
                case 12:
                    id = 20656; //Leyenda del Culo Plateado
                    break;
                default:
                    id = 20649; //Leyenda de Bram Barbamundo
                    break;
            }

            var leyenda = Singleton<ItemManager>.Instance.TryGetTemplate(id);
            client.Character.Inventory.AddItem(leyenda, 1);
            World.Instance.SendAnnounce("<b>" + client.Character.Name + "</b> a encontrado en la Torre : "+leyenda.Name, Color.Gold);
            client.Character.Inventory.RemoveItem(item, 1);
        }

        private static void Regalito(WorldClient client, BasePlayerItem item)
        {
            Random rnd = new Random();
            var randomfinded = rnd.Next(0, 55);

            switch (randomfinded)
            {
                case 1:
                    var regalo = Singleton<ItemManager>.Instance.TryGetTemplate(798); //Pergamino pequeño de agilidad
                    client.Character.Inventory.AddItem(regalo, 1);
                    break;
                case 2:
                    var regalo2 = Singleton<ItemManager>.Instance.TryGetTemplate(1672); //Pelos de darit
                    client.Character.Inventory.AddItem(regalo2, 1);
                    break;
                case 3:
                    var regalo3 = Singleton<ItemManager>.Instance.TryGetTemplate(1690); //Pelos de minilubu
                    client.Character.Inventory.AddItem(regalo3, 1);
                    break;
                case 4:
                    var regalo4 = Singleton<ItemManager>.Instance.TryGetTemplate(20918); //Llave Wabiddictos
                    client.Character.Inventory.AddItem(regalo4, 1);
                    break;
                case 5:
                    var regalo5 = Singleton<ItemManager>.Instance.TryGetTemplate(652); //Tejido jabali
                    client.Character.Inventory.AddItem(regalo5, 1);
                    break;
                case 6:
                    var regalo6 = Singleton<ItemManager>.Instance.TryGetTemplate(886); //Cuero jalato real
                    client.Character.Inventory.AddItem(regalo6, 1);
                    break;
                case 7:
                    var regalo7 = Singleton<ItemManager>.Instance.TryGetTemplate(14464); //Llave del Castillo Wabbit
                    client.Character.Inventory.AddItem(regalo7, 1);
                    break;
                case 8:
                    var regalo8 = Singleton<ItemManager>.Instance.TryGetTemplate(9687); //Piedra de alma 100
                    client.Character.Inventory.AddItem(regalo8, 1);
                    break;
                case 9:
                    var regalo9 = Singleton<ItemManager>.Instance.TryGetTemplate(2275); //Cuero puerkazo
                    client.Character.Inventory.AddItem(regalo9, 1);
                    break;
                case 10:
                    var regalo10 = Singleton<ItemManager>.Instance.TryGetTemplate(9248); //Llave del enclaje blops
                    client.Character.Inventory.AddItem(regalo10, 1);
                    break;
                case 11:
                    var regalo11 = Singleton<ItemManager>.Instance.TryGetTemplate(2553); //Plomo pesado
                    client.Character.Inventory.AddItem(regalo11, 1);
                    break;
                case 12:
                    var regalo12 = Singleton<ItemManager>.Instance.TryGetTemplate(580); //Pocima de energia
                    client.Character.Inventory.AddItem(regalo12, 1);
                    break;
                case 13:
                    var regalo13 = Singleton<ItemManager>.Instance.TryGetTemplate(1770); //Cacho coco
                    client.Character.Inventory.AddItem(regalo13, 1);
                    break;
                case 14:
                    var regalo14 = Singleton<ItemManager>.Instance.TryGetTemplate(683); //Pergamino pequeño de fuerza
                    client.Character.Inventory.AddItem(regalo14, 1);
                    break;
                case 15:
                    var regalo15 = Singleton<ItemManager>.Instance.TryGetTemplate(1775); //Cacho guinda
                    client.Character.Inventory.AddItem(regalo15, 1);
                    break;
                case 16:
                    var regalo16 = Singleton<ItemManager>.Instance.TryGetTemplate(7310); //Llave de la caverna de los bulbos
                    client.Character.Inventory.AddItem(regalo16, 1);
                    break;
                case 17:
                    var regalo17 = Singleton<ItemManager>.Instance.TryGetTemplate(9686); //Piedra de alma 50
                    client.Character.Inventory.AddItem(regalo17, 1);
                    break;
                case 18:
                    var regalo18 = Singleton<ItemManager>.Instance.TryGetTemplate(11798); //Llave del Pueblo Kanibola
                    client.Character.Inventory.AddItem(regalo18, 1);
                    break;
                case 19:
                    client.Character.Energy -= 300;
                    client.Character.SendServerMessage("Baia, perdistes 300 de energía. Culpa del gato.", Color.OrangeRed);
                    break;
                case 20:
                    var regalo20 = Singleton<ItemManager>.Instance.TryGetTemplate(1777); //Cacho indigo
                    client.Character.Inventory.AddItem(regalo20, 1);
                    break;
                case 21:
                    var regalo21 = Singleton<ItemManager>.Instance.TryGetTemplate(13052); //Doplones
                    client.Character.Inventory.AddItem(regalo21, 15);
                    break;
                case 22:
                    var regalo22 = Singleton<ItemManager>.Instance.TryGetTemplate(1773); //Cacho reinieta
                    client.Character.Inventory.AddItem(regalo22, 1);
                    break;
                case 23:
                    var regalo23 = Singleton<ItemManager>.Instance.TryGetTemplate(7272); //Corteza magica bulbambu
                    client.Character.Inventory.AddItem(regalo23, 1);
                    break;
                case 24:
                    var regalo24 = Singleton<ItemManager>.Instance.TryGetTemplate(1569); //Llave de los Herreros
                    client.Character.Inventory.AddItem(regalo24, 1);
                    break;
                case 25:
                    var regalo25 = Singleton<ItemManager>.Instance.TryGetTemplate(7270); //Corteza magica bulbifor
                    client.Character.Inventory.AddItem(regalo25, 1);
                    break;
                case 26:
                    var regalo26 = Singleton<ItemManager>.Instance.TryGetTemplate(8917); //Llave de la Gruta Grut'Hesqua
                    client.Character.Inventory.AddItem(regalo26, 1);
                    break;
                case 27:
                    var regalo27 = Singleton<ItemManager>.Instance.TryGetTemplate(7271); //Corteza magica bulbomatorral
                    client.Character.Inventory.AddItem(regalo27, 1);
                    break;
                case 28:
                    var regalo28 = Singleton<ItemManager>.Instance.TryGetTemplate(8135); //Llave de la mazmorra bwork
                    client.Character.Inventory.AddItem(regalo28, 1);
                    break;
                case 29:
                    var regalo29 = Singleton<ItemManager>.Instance.TryGetTemplate(7222); //Agua akwadala
                    client.Character.Inventory.AddItem(regalo29, 1);
                    break;
                case 30:
                    var regalo30 = Singleton<ItemManager>.Instance.TryGetTemplate(536); //Pan de copos de avena
                    client.Character.Inventory.AddItem(regalo30, 1);
                    break;
                case 31:
                    var regalo31 = Singleton<ItemManager>.Instance.TryGetTemplate(1728); //Miaumiau
                    client.Character.Inventory.AddItem(regalo31, 1);
                    break;
                case 32:
                    var regalo32 = Singleton<ItemManager>.Instance.TryGetTemplate(802); //Pergamino pequeño de sabiduria
                    client.Character.Inventory.AddItem(regalo32, 1);
                    break;
                case 33:
                    client.Character.SendServerMessage("Mala suerte, Shine te quito unos cuantos regalos por mirar donde no debías. :D", Color.OrangeRed);
                    client.Character.Inventory.RemoveItem(item, 5);
                    break;
                case 34:
                    var regalo34 = Singleton<ItemManager>.Instance.TryGetTemplate(7518); //Fontasma
                    client.Character.Inventory.AddItem(regalo34, 1);
                    break;
                case 35:
                    var regalo35 = Singleton<ItemManager>.Instance.TryGetTemplate(536); //Pan de copos de avena
                    client.Character.Inventory.AddItem(regalo35, 1);
                    break;
                case 36:
                    var regalo36 = Singleton<ItemManager>.Instance.TryGetTemplate(8151); //Boluto
                    client.Character.Inventory.AddItem(regalo36, 1);
                    break;
                case 37:
                    var regalo37 = Singleton<ItemManager>.Instance.TryGetTemplate(6902); //Pluma amarilla
                    client.Character.Inventory.AddItem(regalo37, 1);
                    break;
                case 38:
                    var regalo38 = Singleton<ItemManager>.Instance.TryGetTemplate(6897); //Pluma azul
                    client.Character.Inventory.AddItem(regalo38, 1);
                    break;
                case 39:
                    var regalo39 = Singleton<ItemManager>.Instance.TryGetTemplate(536); //Pan de copos de avena
                    client.Character.Inventory.AddItem(regalo39, 1);
                    break;
                case 40:
                    var regalo40 = Singleton<ItemManager>.Instance.TryGetTemplate(806); //Pergamino pequeño de vitalidad
                    client.Character.Inventory.AddItem(regalo40, 1);
                    break;
                case 41:
                    var regalo41 = Singleton<ItemManager>.Instance.TryGetTemplate(6900); //Pluma roja
                    client.Character.Inventory.AddItem(regalo41, 1);
                    break;
                case 42:
                    var regalo52 = Singleton<ItemManager>.Instance.TryGetTemplate(580); //Pocima de energia
                    client.Character.Inventory.AddItem(regalo52, 1);
                    break;
                case 43:
                    var regalo53 = Singleton<ItemManager>.Instance.TryGetTemplate(6903); //Pluma rosa
                    client.Character.Inventory.AddItem(regalo53, 1);
                    break;
                case 44:
                    var regalo54 = Singleton<ItemManager>.Instance.TryGetTemplate(6899); //Pluma verde
                    client.Character.Inventory.AddItem(regalo54, 1);
                    break;
                case 45:
                    var regalo55 = Singleton<ItemManager>.Instance.TryGetTemplate(12017); //Llave kwokan
                    client.Character.Inventory.AddItem(regalo55, 1);
                    break;
                case 46:
                    var regalo56 = Singleton<ItemManager>.Instance.TryGetTemplate(6898); //Pluma violeta
                    client.Character.Inventory.AddItem(regalo56, 1);
                    break;
                case 47:
                    var regalo57 = Singleton<ItemManager>.Instance.TryGetTemplate(287); //Semilla sesamo
                    client.Character.Inventory.AddItem(regalo57, 1);
                    break;
                case 48:
                    var regalo58 = Singleton<ItemManager>.Instance.TryGetTemplate(7423); //Huevo dorado
                    client.Character.Inventory.AddItem(regalo58, 1);
                    break;
                case 49:
                    var regalo59 = Singleton<ItemManager>.Instance.TryGetTemplate(686); //Pergamino pequeño de inteligencia
                    client.Character.Inventory.AddItem(regalo59, 1);
                    break;
                case 50:
                    var regalo60 = Singleton<ItemManager>.Instance.TryGetTemplate(1689); //Tinde naranja
                    client.Character.Inventory.AddItem(regalo60, 1);
                    break;
                case 51:
                    var regalo61 = Singleton<ItemManager>.Instance.TryGetTemplate(1687); //Tinde verde
                    client.Character.Inventory.AddItem(regalo61, 1);
                    break;
                case 52:
                    var regalo62 = Singleton<ItemManager>.Instance.TryGetTemplate(1692); //Tinde negro
                    client.Character.Inventory.AddItem(regalo62, 1);
                    break;
                case 53:
                    var regalo63 = Singleton<ItemManager>.Instance.TryGetTemplate(580); //Pocima de energia
                    client.Character.Inventory.AddItem(regalo63, 1);
                    break;
                case 54:
                    var regalo64 = Singleton<ItemManager>.Instance.TryGetTemplate(1575); //Gremiolagema
                    client.Character.Inventory.AddItem(regalo64, 1);
                    break;
                case 55:
                    var regalo65 = Singleton<ItemManager>.Instance.TryGetTemplate(809); //Pergamino pequeño de suerte
                    client.Character.Inventory.AddItem(regalo65, 1);
                    break;
                default:
                    client.Character.SendServerMessage("Mala suerte, el regalo estaba vacío. D:", Color.OrangeRed);
                    break;
            }

            client.Character.Inventory.RemoveItem(item, 1);

        }

        private static void Regalo(WorldClient client, BasePlayerItem item)
        {
            Random rnd = new Random();
            var randomfinded = rnd.Next(0, 55);

            switch (randomfinded)
            {
                case 1:
                    var regalo1 = Singleton<ItemManager>.Instance.TryGetTemplate(965); //Piedra Dopel
                    client.Character.Inventory.AddItem(regalo1, 1);
                    break;
                case 2:
                    var regalo2 = Singleton<ItemManager>.Instance.TryGetTemplate(2551); //Oreja fux
                    client.Character.Inventory.AddItem(regalo2, 1);
                    break;
                case 3:
                    tpPlayer(client.Character, 84411907, 456, DirectionsEnum.DIRECTION_EAST); // TP Dragohuevo
                    client.Character.SendServerMessage("Creo que he algún elfo ha metido un portal de Selatrop donde no tocaba...", Color.OrangeRed);
                    break;
                case 4:
                    var regalo4 = Singleton<ItemManager>.Instance.TryGetTemplate(680); //Pergamino dorado
                    client.Character.Inventory.AddItem(regalo4, 1);
                    break;
                case 5:
                    client.Character.SendServerMessage("Mala suerte, Shine te quito unos cuantos regalos por mirar donde no debías. :D", Color.OrangeRed);
                    client.Character.Inventory.RemoveItem(item, 6);
                    break;
                case 6:
                    var regalo6 = Singleton<ItemManager>.Instance.TryGetTemplate(9688); //Piedra de Alma 150
                    client.Character.Inventory.AddItem(regalo6, 1);
                    break;
                case 7:
                    var regalo7 = Singleton<ItemManager>.Instance.TryGetTemplate(1890); //Pelo de vetorano 
                    client.Character.Inventory.AddItem(regalo7, 1);
                    break;
                case 8:
                    var regalo8 = Singleton<ItemManager>.Instance.TryGetTemplate(2542); //hogar
                    client.Character.Inventory.AddItem(regalo8, 1);
                    break;
                case 9:
                    var regalo9 = Singleton<ItemManager>.Instance.TryGetTemplate(18366); //arena fina
                    client.Character.Inventory.AddItem(regalo9, 1);
                    break;
                case 10:
                    tpPlayer(client.Character, 160257, 383, DirectionsEnum.DIRECTION_EAST); // TP Otomai
                    client.Character.SendServerMessage("Creo que he algún elfo ha metido un portal de Selatrop donde no tocaba...", Color.OrangeRed);
                    break;
                case 11:
                    client.Character.Energy -= 600;
                    client.Character.SendServerMessage("Baia, perdistes 600 de energía. A mí no me mires, solo soy tú.", Color.OrangeRed);
                    break;
                case 12:
                    var regalo12 = Singleton<ItemManager>.Instance.TryGetTemplate(7523); //Miaumiau pelirojo
                    client.Character.Inventory.AddItem(regalo12, 1);
                    break;
                case 13:
                    var regalo13 = Singleton<ItemManager>.Instance.TryGetTemplate(12735); //Llave Daigorobo
                    client.Character.Inventory.AddItem(regalo13, 1);
                    break;
                case 14:
                    var regalo14 = Singleton<ItemManager>.Instance.TryGetTemplate(11014); //Murcielago palido
                    client.Character.Inventory.AddItem(regalo14, 1);
                    break;
                case 15:
                    var regalo15 = Singleton<ItemManager>.Instance.TryGetTemplate(2504); //lengua cruji
                    client.Character.Inventory.AddItem(regalo15, 1);
                    break;
                case 16:
                    var regalo16 = Singleton<ItemManager>.Instance.TryGetTemplate(8801); //Estropajo piel
                    client.Character.Inventory.AddItem(regalo16, 1);
                    break;
                case 17:
                    var regalo17 = Singleton<ItemManager>.Instance.TryGetTemplate(757); //gelatina azul
                    client.Character.Inventory.AddItem(regalo17, 1);
                    break;
                case 18:
                    var regalo18 = Singleton<ItemManager>.Instance.TryGetTemplate(14488); //cacahowia
                    client.Character.Inventory.AddItem(regalo18, 1);
                    break;
                case 19:
                    var regalo19 = Singleton<ItemManager>.Instance.TryGetTemplate(368); //gelatina fresa
                    client.Character.Inventory.AddItem(regalo19, 1);
                    break;
                case 20:
                    var regalo20 = Singleton<ItemManager>.Instance.TryGetTemplate(369); //gelatina menta
                    client.Character.Inventory.AddItem(regalo20, 1);
                    break;
                case 21:
                    var regalo21 = Singleton<ItemManager>.Instance.TryGetTemplate(13052); //Doplones
                    client.Character.Inventory.AddItem(regalo21, 32);
                    break;
                case 22:
                    var regalo22 = Singleton<ItemManager>.Instance.TryGetTemplate(434); //corteza abrak
                    client.Character.Inventory.AddItem(regalo22, 1);
                    break;
                case 23:
                    var regalo23 = Singleton<ItemManager>.Instance.TryGetTemplate(435); //raiz abrak
                    client.Character.Inventory.AddItem(regalo23, 1);
                    break;
                case 24:
                    var regalo24 = Singleton<ItemManager>.Instance.TryGetTemplate(12073); //Llave Laboratorio
                    client.Character.Inventory.AddItem(regalo24, 1);
                    break;
                case 25:
                    var regalo25 = Singleton<ItemManager>.Instance.TryGetTemplate(20646); //Sombrero aparencia nawi
                    client.Character.Inventory.AddItem(regalo25, 1);
                    break;
                case 26:
                    var regalo26 = Singleton<ItemManager>.Instance.TryGetTemplate(8883); //Pocima hogar gremio
                    client.Character.Inventory.AddItem(regalo26, 1);
                    break;
                case 27:
                    var regalo27 = Singleton<ItemManager>.Instance.TryGetTemplate(548); //recuerdo
                    client.Character.Inventory.AddItem(regalo27, 1);
                    break;
                case 28:
                    var regalo28 = Singleton<ItemManager>.Instance.TryGetTemplate(7927); //Llave picos rocosos
                    client.Character.Inventory.AddItem(regalo28, 1);
                    break;
                case 29:
                    var regalo29 = Singleton<ItemManager>.Instance.TryGetTemplate(10988); //Sombrero aparencia escarcha
                    client.Character.Inventory.AddItem(regalo29, 1);
                    break;
                case 30:
                    var regalo30 = Singleton<ItemManager>.Instance.TryGetTemplate(15163); //Diligencias
                    client.Character.Inventory.AddItem(regalo30, 1);
                    break;
                case 31:
                    tpPlayer(client.Character, 9511, 426, DirectionsEnum.DIRECTION_EAST); // Pandala Aire
                    client.Character.SendServerMessage("Creo que he algún elfo ha metido un portal de Selatrop donde no tocaba...", Color.OrangeRed);
                    break;
                case 32:
                    client.Character.SendServerMessage("Mala suerte, Shine te quito unos cuantos regalos por mirar donde no debías. :D", Color.OrangeRed);
                    client.Character.Inventory.RemoveItem(item, 6);
                    break;
                case 33:
                    var regalo33 = Singleton<ItemManager>.Instance.TryGetTemplate(10989); //Capa aparencia escarcha
                    client.Character.Inventory.AddItem(regalo33, 1);
                    break;
                case 34:
                    var regalo34 = Singleton<ItemManager>.Instance.TryGetTemplate(14465); //Llave Wey Wabbit
                    client.Character.Inventory.AddItem(regalo34, 1);
                    break;
                case 35:
                    var regalo35 = Singleton<ItemManager>.Instance.TryGetTemplate(548); //recuerdo
                    client.Character.Inventory.AddItem(regalo35, 1);
                    break;
                case 36:
                    var regalo36 = Singleton<ItemManager>.Instance.TryGetTemplate(20299); //Sombrero aparencia nawi 2
                    client.Character.Inventory.AddItem(regalo36, 1);
                    break;
                case 37:
                    var regalo37 = Singleton<ItemManager>.Instance.TryGetTemplate(15236); //Tesoro
                    client.Character.Inventory.AddItem(regalo37, 1);
                    break;
                case 38:
                    var regalo38 = Singleton<ItemManager>.Instance.TryGetTemplate(2034); //Baston caramelo
                    client.Character.Inventory.AddItem(regalo38, 1);
                    break;
                case 39:
                    var regalo39 = Singleton<ItemManager>.Instance.TryGetTemplate(12312); //Cruzarse de brazos
                    client.Character.Inventory.AddItem(regalo39, 1);
                    break;
                case 40:
                    var regalo40 = Singleton<ItemManager>.Instance.TryGetTemplate(12328); //dar saltitos
                    client.Character.Inventory.AddItem(regalo40, 1);
                    break;
                case 41:
                    var regalo41 = Singleton<ItemManager>.Instance.TryGetTemplate(6965); //Bonta
                    client.Character.Inventory.AddItem(regalo41, 1);
                    break;
                case 42:
                    var regalo52 = Singleton<ItemManager>.Instance.TryGetTemplate(12283); //Estar congelado
                    client.Character.Inventory.AddItem(regalo52, 1);
                    break;
                case 43:
                    var regalo53 = Singleton<ItemManager>.Instance.TryGetTemplate(6964); //Brakmar
                    client.Character.Inventory.AddItem(regalo53, 1);
                    break;
                case 44:
                    var regalo54 = Singleton<ItemManager>.Instance.TryGetTemplate(13422); //frotarse las manos
                    client.Character.Inventory.AddItem(regalo54, 1);
                    break;
                case 45:
                    var regalo55 = Singleton<ItemManager>.Instance.TryGetTemplate(680); //Pergamino dorado
                    client.Character.Inventory.AddItem(regalo55, 1);
                    break;
                case 46:
                    var regalo56 = Singleton<ItemManager>.Instance.TryGetTemplate(18421); //Llave cementerio mastodontes
                    client.Character.Inventory.AddItem(regalo56, 1);
                    break;
                case 47:
                    var regalo57 = Singleton<ItemManager>.Instance.TryGetTemplate(13368); //tener frio
                    client.Character.Inventory.AddItem(regalo57, 1);
                    break;
                case 48:
                    var regalo58 = Singleton<ItemManager>.Instance.TryGetTemplate(13442); //bibliotemplo
                    client.Character.Inventory.AddItem(regalo58, 1);
                    break;
                case 49:
                    var regalo59 = Singleton<ItemManager>.Instance.TryGetTemplate(7288); //bambuto dorado
                    client.Character.Inventory.AddItem(regalo59, 1);
                    break;
                case 50:
                    var regalo60 = Singleton<ItemManager>.Instance.TryGetTemplate(548); //recuerdo
                    client.Character.Inventory.AddItem(regalo60, 1);
                    break;
                case 51:
                    var regalo61 = Singleton<ItemManager>.Instance.TryGetTemplate(14447); //Alianza
                    client.Character.Inventory.AddItem(regalo61, 1);
                    break;
                case 52:
                    var regalo62 = Singleton<ItemManager>.Instance.TryGetTemplate(10973); //desconocido tp
                    client.Character.Inventory.AddItem(regalo62, 1);
                    break;
                case 53:
                    var regalo63 = Singleton<ItemManager>.Instance.TryGetTemplate(13052); //Llave picos rocosos
                    client.Character.Inventory.AddItem(regalo63, 1);
                    break;
                case 54:
                    client.Character.Energy -= 600;
                    client.Character.SendServerMessage("Baia, perdistes 600 de energía. A mí no me mires, solo soy tú.", Color.OrangeRed);
                    break;
                case 55:
                    tpPlayer(client.Character, 99746311, 466, DirectionsEnum.DIRECTION_EAST); // Wabbits TP
                    client.Character.SendServerMessage("Creo que he algún elfo ha metido un portal de Selatrop donde no tocaba...", Color.OrangeRed);
                    break;
                default:
                    client.Character.SendServerMessage("Mala suerte, el regalo estaba vacío. D:", Color.OrangeRed);
                    break;
            }

            client.Character.Inventory.RemoveItem(item, 1);

        }

        private static void Regalazo(WorldClient client, BasePlayerItem item)
        {
            Random rnd = new Random();
            var randomfinded = rnd.Next(0, 55);

            switch (randomfinded)
            {
                case 1:
                    var regalo1 = Singleton<ItemManager>.Instance.TryGetTemplate(20323); //mascota apa 5
                    client.Character.Inventory.AddItem(regalo1, 1);
                    break;
                case 2:
                    client.Character.SendServerMessage("Mala suerte, Shine te quito unos cuantos regalos por mirar donde no debías. :D", Color.OrangeRed);
                    client.Character.Inventory.RemoveItem(item, 9);
                    break;
                case 3:
                    var regalo3 = Singleton<ItemManager>.Instance.TryGetTemplate(9234); //sombrero obje
                    client.Character.Inventory.AddItem(regalo3, 1);
                    break;
                case 4:
                    var regalo4 = Singleton<ItemManager>.Instance.TryGetTemplate(13052); //Doplones
                    client.Character.Inventory.AddItem(regalo4, 45);
                    break;
                case 5:
                    var regalo5 = Singleton<ItemManager>.Instance.TryGetTemplate(9451); //capa cristalina
                    client.Character.Inventory.AddItem(regalo5, 1);
                    break;
                case 6:
                    var regalo6 = Singleton<ItemManager>.Instance.TryGetTemplate(805); //Potente sab
                    client.Character.Inventory.AddItem(regalo6, 1);
                    break;
                case 7:
                    var regalo7 = Singleton<ItemManager>.Instance.TryGetTemplate(20322); //mascota apa 4
                    client.Character.Inventory.AddItem(regalo7, 1);
                    break;
                case 8:
                    var regalo8 = Singleton<ItemManager>.Instance.TryGetTemplate(7225); //artefacto fuegodala
                    client.Character.Inventory.AddItem(regalo8, 1);
                    break;
                case 9:
                    var regalo9 = Singleton<ItemManager>.Instance.TryGetTemplate(817); //Pergamino Invocacion araka
                    client.Character.Inventory.AddItem(regalo9, 1);
                    break;
                case 10:
                    client.Character.SendServerMessage("Mala suerte, Shine te quito unos cuantos regalos por mirar donde no debías. :D", Color.OrangeRed);
                    client.Character.Inventory.RemoveItem(item, 9);
                    break;
                case 11:
                    client.Character.Energy -= 1000;
                    client.Character.SendServerMessage("El paquete acaba de explotar, ligeramente, si.", Color.OrangeRed);
                    break;
                case 12:
                    var regalo12 = Singleton<ItemManager>.Instance.TryGetTemplate(7291); //Raiz magica de bambuto
                    client.Character.Inventory.AddItem(regalo12, 1);
                    break;
                case 13:
                    var regalo13 = Singleton<ItemManager>.Instance.TryGetTemplate(9233); //capa objeviva
                    client.Character.Inventory.AddItem(regalo13, 1);
                    break;
                case 14:
                    client.Character.SendServerMessage("Mala suerte, Shine te quito unos cuantos regalos por mirar donde no debías. :D", Color.OrangeRed);
                    client.Character.Inventory.RemoveItem(item, 9);
                    break;
                case 15:
                    var regalo15 = Singleton<ItemManager>.Instance.TryGetTemplate(7385); //Tejido fantasma takuni
                    client.Character.Inventory.AddItem(regalo15, 1);
                    break;
                case 16:
                    var regalo16 = Singleton<ItemManager>.Instance.TryGetTemplate(718); //Pergamino llamilla
                    client.Character.Inventory.AddItem(regalo16, 1);
                    break;
                case 17:
                    var regalo17 = Singleton<ItemManager>.Instance.TryGetTemplate(7393); //Tejido fantasma pandora
                    client.Character.Inventory.AddItem(regalo17, 1);
                    break;
                case 18:
                    var regalo18 = Singleton<ItemManager>.Instance.TryGetTemplate(6963); //Mitobolas
                    client.Character.Inventory.AddItem(regalo18, 1);
                    break;
                case 19:
                    var regalo19 = Singleton<ItemManager>.Instance.TryGetTemplate(797); //Potente fuerza
                    client.Character.Inventory.AddItem(regalo19, 1);
                    break;
                case 20:
                    var regalo20 = Singleton<ItemManager>.Instance.TryGetTemplate(583); //tejido pandikaze ghost
                    client.Character.Inventory.AddItem(regalo20, 1);
                    break;
                case 21:
                    var regalo21 = Singleton<ItemManager>.Instance.TryGetTemplate(20326); //mascota apa 3
                    client.Character.Inventory.AddItem(regalo21, 1);
                    break;
                case 22:
                    var regalo22 = Singleton<ItemManager>.Instance.TryGetTemplate(9418); //tocado crista
                    client.Character.Inventory.AddItem(regalo22, 1);
                    break;
                case 23:
                    var regalo23 = Singleton<ItemManager>.Instance.TryGetTemplate(20321); //mascota apa 3
                    client.Character.Inventory.AddItem(regalo23, 1);
                    break;
                case 24:
                    var regalo24 = Singleton<ItemManager>.Instance.TryGetTemplate(6805); //bracalete bwork
                    client.Character.Inventory.AddItem(regalo24, 1);
                    break;
                case 25:
                    var regalo25 = Singleton<ItemManager>.Instance.TryGetTemplate(6804); //amuleto bwork
                    client.Character.Inventory.AddItem(regalo25, 1);
                    break;
                case 26:
                    var regalo26 = Singleton<ItemManager>.Instance.TryGetTemplate(814); //Potente suerte
                    client.Character.Inventory.AddItem(regalo26, 1);
                    break;
                case 27:
                    var regalo27 = Singleton<ItemManager>.Instance.TryGetTemplate(6807); //Botas bwork
                    client.Character.Inventory.AddItem(regalo27, 1);
                    break;
                case 28:
                    var regalo28 = Singleton<ItemManager>.Instance.TryGetTemplate(6813); //casco bwork
                    client.Character.Inventory.AddItem(regalo28, 1);
                    break;
                case 29:
                    var regalo29 = Singleton<ItemManager>.Instance.TryGetTemplate(6812); //calconcillo bwork
                    client.Character.Inventory.AddItem(regalo29, 1);
                    break;
                case 30:
                    var regalo30 = Singleton<ItemManager>.Instance.TryGetTemplate(6811); //capa bwork
                    client.Character.Inventory.AddItem(regalo30, 1);
                    break;
                case 31:
                    var regalo31 = Singleton<ItemManager>.Instance.TryGetTemplate(6799); //Hoja bwork
                    client.Character.Inventory.AddItem(regalo31, 1);
                    break;
                case 32:
                    var regalo32 = Singleton<ItemManager>.Instance.TryGetTemplate(8082); //tejido sigelak
                    client.Character.Inventory.AddItem(regalo32, 1);
                    break;
                case 33:
                    var regalo33 = Singleton<ItemManager>.Instance.TryGetTemplate(8066); //pelo warko violeta
                    client.Character.Inventory.AddItem(regalo33, 1);
                    break;
                case 34:
                    var regalo34 = Singleton<ItemManager>.Instance.TryGetTemplate(8064); //koala sanginario recurso
                    client.Character.Inventory.AddItem(regalo34, 1);
                    break;
                case 35:
                    var regalo35 = Singleton<ItemManager>.Instance.TryGetTemplate(801); //Potente agi
                    client.Character.Inventory.AddItem(regalo35, 1);
                    break;
                case 36:
                    client.Character.Energy -= 1000;
                    client.Character.SendServerMessage("El paquete acaba de explotar, ligeramente, si.", Color.OrangeRed);
                    break;
                case 37:
                    var regalo37 = Singleton<ItemManager>.Instance.TryGetTemplate(11117); //lana jalamut
                    client.Character.Inventory.AddItem(regalo37, 1);
                    break;
                case 38:
                    var regalo38 = Singleton<ItemManager>.Instance.TryGetTemplate(11136); //lana jalamut leg
                    client.Character.Inventory.AddItem(regalo38, 1);
                    break;
                case 39:
                    var regalo39 = Singleton<ItemManager>.Instance.TryGetTemplate(11475); //lana jalamut
                    client.Character.Inventory.AddItem(regalo39, 1);
                    break;
                case 40:
                    var regalo40 = Singleton<ItemManager>.Instance.TryGetTemplate(12151); //llave nawidad
                    client.Character.Inventory.AddItem(regalo40, 1);
                    break;
                case 41:
                    var regalo41 = Singleton<ItemManager>.Instance.TryGetTemplate(7908); //llave tranki
                    client.Character.Inventory.AddItem(regalo41, 1);
                    break;
                case 42:
                    var regalo42 = Singleton<ItemManager>.Instance.TryGetTemplate(817); //Potente int
                    client.Character.Inventory.AddItem(regalo42, 1);
                    break;
                case 43:
                    var regalo43 = Singleton<ItemManager>.Instance.TryGetTemplate(8156); //llave max
                    client.Character.Inventory.AddItem(regalo43, 1);
                    break;
                case 44:
                    var regalo44 = Singleton<ItemManager>.Instance.TryGetTemplate(7557); //llave ances
                    client.Character.Inventory.AddItem(regalo44, 1);
                    break;
                case 45:
                    client.Character.SendServerMessage("Mala suerte, Shine te quito unos cuantos regalos por mirar donde no debías. :D", Color.OrangeRed);
                    client.Character.Inventory.RemoveItem(item, 9);
                    break;
                case 46:
                    client.Character.Energy -= 1000;
                    client.Character.SendServerMessage("El paquete acaba de explotar, ligeramente, si.", Color.OrangeRed);
                    break;
                case 47:
                    var regalo47 = Singleton<ItemManager>.Instance.TryGetTemplate(810); //Potente vit
                    client.Character.Inventory.AddItem(regalo47, 1);
                    break;
                case 48:
                    var regalo58 = Singleton<ItemManager>.Instance.TryGetTemplate(20324); //mascota apa 2
                    client.Character.Inventory.AddItem(regalo58, 1);
                    break;
                case 49:
                    var regalo49 = Singleton<ItemManager>.Instance.TryGetTemplate(447); //carbon
                    client.Character.Inventory.AddItem(regalo49, 1);
                    break;
                case 50:
                    var regalo50 = Singleton<ItemManager>.Instance.TryGetTemplate(20125); //galleta
                    client.Character.Inventory.AddItem(regalo50, 1);
                    break;
                case 51:
                    var regalo61 = Singleton<ItemManager>.Instance.TryGetTemplate(20325); //mascota apa 1
                    client.Character.Inventory.AddItem(regalo61, 1);
                    break;
                case 52:
                    var regalo52 = Singleton<ItemManager>.Instance.TryGetTemplate(8756); //pelusa kilibr
                    client.Character.Inventory.AddItem(regalo52, 1);
                    break;
                case 53:
                    var regalo54 = Singleton<ItemManager>.Instance.TryGetTemplate(8800); //tejido zoth
                    client.Character.Inventory.AddItem(regalo54, 1);
                    break;
                case 54:
                    client.Character.SendServerMessage("Mala suerte, Shine te quito unos cuantos regalos por mirar donde no debías. :D", Color.OrangeRed);
                    client.Character.Inventory.RemoveItem(item, 9);
                    break;
                case 55:
                    client.Character.Energy -= 1000;
                    client.Character.SendServerMessage("El paquete acaba de explotar, ligeramente, si.", Color.OrangeRed);
                    break;
                default:
                    client.Character.SendServerMessage("Mala suerte, el regalo estaba vacío. D:", Color.OrangeRed);
                    break;
            }

            client.Character.Inventory.RemoveItem(item, 1);

        }

        [WorldHandler(ObjectUseMultipleMessage.Id)]
        public static void HandleObjectUseMultipleMessage(WorldClient client, ObjectUseMultipleMessage message)
        {
            var item = client.Character.Inventory.TryGetItem((int)message.ObjectUID);

            if (item == null)
            {
                return;
            }

            client.Character.Inventory.UseItem(item, (int)message.Quantity);
        }

        [WorldHandler(ObjectUseOnCellMessage.Id)]
        public static void HandleObjectUseOnCellMessage(WorldClient client, ObjectUseOnCellMessage message)
        {
            var item = client.Character.Inventory.TryGetItem((int)message.ObjectUID);

            if (item == null)
            {
                return;
            }

            var cell = client.Character.Map.GetCell(message.Cells);

            if (cell == null)
            {
                return;
            }

            client.Character.Inventory.UseItem(item, cell);
        }

        [WorldHandler(ObjectUseOnCharacterMessage.Id)]
        public static void HandleObjectUseOnCharacterMessage(WorldClient client, ObjectUseOnCharacterMessage message)
        {
            var item = client.Character.Inventory.TryGetItem((int)message.ObjectUID);

            if (item == null)
            {
                return;
            }

            if (!item.Template.Targetable)
            {
                return;
            }

            var character = client.Character.Map.GetActor<Character>((int)message.CharacterId);

            if (character == null)
            {
                return;
            }

            client.Character.Inventory.UseItem(item, character);
        }

        [WorldHandler(ObjectFeedMessage.Id)]
        public static void HandleObjectFeedMessage(WorldClient client, ObjectFeedMessage message)
        {
            if (client.Character.IsInFight())
            {
                return;
            }

            var item = client.Character.Inventory.TryGetItem((int)message.ObjectUID);
            var food = client.Character.Inventory.TryGetItem((int)message.Meal.First().ObjectUID);

            if (item == null || food == null)
            {
                return;
            }

            if (food.Stack < message.Meal.First().Quantity)
            {
                message.Meal.First().Quantity = (ushort)food.Stack;
            }

            if (item.Stack > 1)
                item.Owner.Inventory.CutItem(item, (int)item.Stack - 1);

            var i = 0;
            while (i < message.Meal.First().Quantity && item.Feed(food))
            {
                i++;
            }

            client.Character.Inventory.RemoveItem(food, i);
        }

        [WorldHandler(LivingObjectChangeSkinRequestMessage.Id)]
        public static void HandleLivingObjectChangeSkinRequestMessage(WorldClient client, LivingObjectChangeSkinRequestMessage message)
        {
            if (client.Character.IsInFight())
            {
                return;
            }

            var item = client.Character.Inventory.TryGetItem((int)message.LivingUID);

            if (!(item is CommonLivingObject))
            {
                return;
            } ((CommonLivingObject)item).SelectedLevel = (short)message.SkinId;
        }

        [WorldHandler(LivingObjectDissociateMessage.Id)]
        public static void HandleLivingObjectDissociateMessage(WorldClient client, LivingObjectDissociateMessage message)
        {
            if (client.Character.IsInFight())
            {
                return;
            }

            var item = client.Character.Inventory.TryGetItem((int)message.LivingUID);

            (item as BoundLivingObjectItem)?.Dissociate();
        }

        [WorldHandler(ObjectDropMessage.Id)]
        public static void HandleObjectDropMessage(WorldClient client, ObjectDropMessage message)
        {
            if (client.Character.IsInFight() || client.Character.IsInExchange())
            {
                return;
            }

            client.Character.DropItem((int)message.ObjectUID, (int)message.Quantity);
        }

        [WorldHandler(MimicryObjectFeedAndAssociateRequestMessage.Id)]
        public static void HandleMimicryObjectFeedAndAssociateRequestMessage(WorldClient client, MimicryObjectFeedAndAssociateRequestMessage message)
        {
            if (client.Character.IsInFight())
            {
                return;
            }

            var character = client.Character;

            var host = character.Inventory.TryGetItem((int)message.HostUID);
            var food = character.Inventory.TryGetItem((int)message.FoodUID);
            var mimisymbic = character.Inventory.TryGetItem(ItemIdEnum.MIMIBIOTE_14485);

            if (host == null || food == null)
            {
                SendMimicryObjectErrorMessage(client, host == null ? MimicryErrorEnum.NO_VALID_HOST : MimicryErrorEnum.NO_VALID_FOOD);
                return;
            }

            if (mimisymbic == null)
            {
                SendMimicryObjectErrorMessage(client, MimicryErrorEnum.NO_VALID_MIMICRY);
                return;
            }

            if (host.Effects.Any(x => x.EffectId == EffectsEnum.Effect_LivingObjectId || x.EffectId == EffectsEnum.Effect_Appearance || x.EffectId == EffectsEnum.Effect_Apparence_Wrapper)
                || !host.Template.Type.Mimickable)
            {
                SendMimicryObjectErrorMessage(client, MimicryErrorEnum.NO_VALID_HOST);
                return;
            }

            if (food.Effects.Any(x => x.EffectId == EffectsEnum.Effect_LivingObjectId || x.EffectId == EffectsEnum.Effect_Appearance || x.EffectId == EffectsEnum.Effect_Apparence_Wrapper)
                || !food.Template.Type.Mimickable)
            {
                SendMimicryObjectErrorMessage(client, MimicryErrorEnum.NO_VALID_FOOD);
                return;
            }

            if (food.Template.Id == host.Template.Id)
            {
                SendMimicryObjectErrorMessage(client, MimicryErrorEnum.SAME_SKIN);
                return;
            }

            if (food.Template.Level > host.Template.Level)
            {
                SendMimicryObjectErrorMessage(client, MimicryErrorEnum.FOOD_LEVEL);
                return;
            }

            if (food.Template.TypeId != host.Template.TypeId)
            {
                SendMimicryObjectErrorMessage(client, MimicryErrorEnum.FOOD_TYPE);
                return;
            }

            var modifiedItem = ItemManager.Instance.CreatePlayerItem(character, host);
            modifiedItem.Effects.Add(new EffectInteger(EffectsEnum.Effect_Appearance, (short)food.Template.Id));
            modifiedItem.Stack = 1;

            if (message.Preview)
            {
                SendMimicryObjectPreviewMessage(client, modifiedItem);
            }
            else
            {
                character.Inventory.UnStackItem(food, 1);
                character.Inventory.UnStackItem(mimisymbic, 1);
                character.Inventory.UnStackItem(host, 1);
                character.Inventory.AddItem(modifiedItem);

                SendMimicryObjectAssociatedMessage(client, modifiedItem);
            }
        }

        [WorldHandler(MimicryObjectEraseRequestMessage.Id)]
        public static void HandleMimicryObjectEraseRequestMessage(WorldClient client, MimicryObjectEraseRequestMessage message)
        {
            if (client.Character.IsInFight())
            {
                return;
            }

            var host = client.Character.Inventory.TryGetItem((int)message.HostUID);

            if (host == null)
            {
                return;
            }

            host.Effects.RemoveAll(x => x.EffectId == EffectsEnum.Effect_Appearance);
            host.Invalidate();

            client.Character.Inventory.RefreshItem(host);
            client.Character.UpdateLook();

            SendInventoryWeightMessage(client);
        }

        [WorldHandler(WrapperObjectDissociateRequestMessage.Id)]
        public static void HandleWrapperObjectDissociateRequestMessage(WorldClient client, WrapperObjectDissociateRequestMessage message)
        {
            if (client.Character.IsInFight() || client.Character.IsInExchange())
            {
                return;
            }

            var host = client.Character.Inventory.TryGetItem((int)message.HostUID);

            if (host == null)
            {
                return;
            }

            var apparenceWrapper = host.Effects.FirstOrDefault(x => x.EffectId == EffectsEnum.Effect_Apparence_Wrapper) as EffectInteger;

            if (apparenceWrapper == null)
            {
                return;
            }

            var wrapperItemTemplate = ItemManager.Instance.TryGetTemplate(apparenceWrapper.Value);

            host.Effects.RemoveAll(x => x.EffectId == EffectsEnum.Effect_Apparence_Wrapper);

            host.Invalidate();
            client.Character.Inventory.RefreshItem(host);
            host.OnObjectModified();

            var wrapperItem = ItemManager.Instance.CreatePlayerItem(client.Character, wrapperItemTemplate, 1);

            client.Character.Inventory.AddItem(wrapperItem);
            client.Character.UpdateLook();

            SendInventoryWeightMessage(client);
        }

        public static void SendWrapperObjectAssociatedMessage(IPacketReceiver client, BasePlayerItem host)
        {
            client.Send(new WrapperObjectAssociatedMessage((uint)host.Guid));
        }

        public static void SendMimicryObjectAssociatedMessage(IPacketReceiver client, BasePlayerItem host)
        {
            client.Send(new MimicryObjectAssociatedMessage((uint)host.Guid));
        }

        public static void SendMimicryObjectPreviewMessage(IPacketReceiver client, BasePlayerItem host)
        {
            client.Send(new MimicryObjectPreviewMessage(host.GetObjectItem()));
        }

        public static void SendMimicryObjectErrorMessage(IPacketReceiver client, MimicryErrorEnum error)
        {
            client.Send(new MimicryObjectErrorMessage((sbyte)ObjectErrorEnum.SYMBIOTIC_OBJECT_ERROR, (sbyte)error, true));
        }

        public static void SendGameRolePlayPlayerLifeStatusMessage(IPacketReceiver client)
        {
            client.Send(new GameRolePlayPlayerLifeStatusMessage());
        }

        public static void SendInventoryContentMessage(WorldClient client)
        {
            var itemPrices = new Dictionary<ushort, ulong>();
            foreach (var item in client.Character.Inventory)
                if (!itemPrices.ContainsKey(item.GetObjectItem().ObjectGID))
                    itemPrices.Add(item.GetObjectItem().ObjectGID, PriceFormulas.getItemPrice(item.Template.Id));
            client.Send(new ObjectAveragePricesMessage(itemPrices.Keys.ToArray(), itemPrices.Values.ToArray()));
            client.Send(new InventoryContentMessage(
                client.Character.Inventory.Select(entry => entry.GetObjectItem()).ToArray(),
                client.Character.Inventory.Kamas));
        }
        

        public static void SendInventoryContentAndPresetMessage(WorldClient client)
        {
            var itemPrices = new Dictionary<ushort, ulong>();
            foreach (var item in client.Character.Inventory)
                if (!itemPrices.ContainsKey(item.GetObjectItem().ObjectGID))
                    itemPrices.Add(item.GetObjectItem().ObjectGID, PriceFormulas.getItemPrice(item.Template.Id));
            client.Send(new ObjectAveragePricesMessage(itemPrices.Keys.ToArray(), itemPrices.Values.ToArray()));
            client.Send(new InventoryContentMessage(
                client.Character.Inventory.Select(entry => entry.GetObjectItem()).ToArray(),
                client.Character.Inventory.Kamas));
        }

        public static void SendInventoryWeightMessage(WorldClient client)
        {
            client.Send(new InventoryWeightMessage((uint)client.Character.Inventory.Weight,
                                                   client.Character.Inventory.WeightTotal));
        }

        public static void SendExchangeKamaModifiedMessage(IPacketReceiver client, bool remote, ulong kamasAmount)
        {
            client.Send(new ExchangeKamaModifiedMessage(remote, kamasAmount));
        }

        public static void SendObjectAddedMessage(IPacketReceiver client, IItem addedItem)
        {
            client.Send(new ObjectAddedMessage(addedItem.GetObjectItem(), 0));
            client.Send(new ObjectAveragePricesMessage(new[] { addedItem.GetObjectItem().ObjectGID },
                new ulong[] { PriceFormulas.getItemPrice(addedItem.Template.Id) }));
        }

        public static void SendObjectsAddedMessage(IPacketReceiver client, IEnumerable<ObjectItem> addeditems)
        {
            client.Send(new ObjectsAddedMessage(addeditems.ToArray()));
        }

        public static void SendObjectsQuantityMessage(IPacketReceiver client, IEnumerable<ObjectItemQuantity> itemQuantity)
        {
            client.Send(new ObjectsQuantityMessage(itemQuantity.ToArray()));
        }

        public static void SendObjectDeletedMessage(IPacketReceiver client, int guid)
        {
            client.Send(new ObjectDeletedMessage((uint)guid));
        }

        public static void SendObjectsDeletedMessage(IPacketReceiver client, IEnumerable<int> guids)
        {
            client.Send(new ObjectsDeletedMessage(guids.Select(entry => (uint)entry).ToArray()));
        }

        public static void SendObjectModifiedMessage(IPacketReceiver client, IItem item)
        {
            client.Send(new ObjectModifiedMessage(item.GetObjectItem()));
        }

        public static void SendObjectMovementMessage(IPacketReceiver client, BasePlayerItem movedItem)
        {
            client.Send(new ObjectMovementMessage((uint)movedItem.Guid, (sbyte)movedItem.Position));
        }

        public static void SendObjectQuantityMessage(IPacketReceiver client, BasePlayerItem item)
        {
            client.Send(new ObjectQuantityMessage((uint)item.Guid, (uint)item.Stack, 0));
        }

        public static void SendObjectErrorMessage(IPacketReceiver client, ObjectErrorEnum error)
        {
            client.Send(new ObjectErrorMessage((sbyte)error));
        }

        public static void SendSetUpdateMessage(WorldClient client, ItemSetTemplate itemSet)
        {
            client.Send(new SetUpdateMessage((ushort)itemSet.Id,
                client.Character.Inventory.GetItemSetEquipped(itemSet).Select(entry => (ushort)entry.Template.Id).ToArray(),
                client.Character.Inventory.GetItemSetEffects(itemSet).Select(entry => entry.GetObjectEffect()).ToArray()));
        }

        public static void SendExchangeShopStockMovementUpdatedMessage(IPacketReceiver client, MerchantItem item)
        {
            client.Send(new ExchangeShopStockMovementUpdatedMessage(item.GetObjectItemToSell()));
        }

        public static void SendExchangeShopStockMovementRemovedMessage(IPacketReceiver client, MerchantItem item)
        {
            client.Send(new ExchangeShopStockMovementRemovedMessage((uint)item.Guid));
        }

        public static void SendObtainedItemMessage(IPacketReceiver client, ItemTemplate item, int count)
        {
            client.Send(new ObtainedItemMessage((ushort)item.Id, (uint)count));
        }

        public static void SendObtainedItemWithBonusMessage(IPacketReceiver client, ItemTemplate item, int count, int bonus)
        {
            client.Send(new ObtainedItemWithBonusMessage((ushort)item.Id, (uint)count, (uint)bonus));
        }

        public static void SendExchangeObjectPutInBagMessage(IPacketReceiver client, bool remote, IItem item)
        {
            client.Send(new ExchangeObjectPutInBagMessage(remote, item.GetObjectItem()));
        }

        public static void SendExchangeObjectModifiedInBagMessage(IPacketReceiver client, bool remote, IItem item)
        {
            client.Send(new ExchangeObjectModifiedInBagMessage(remote, item.GetObjectItem()));
        }

        public static void SendExchangeObjectRemovedFromBagMessage(IPacketReceiver client, bool remote, int guid)
        {
            client.Send(new ExchangeObjectRemovedFromBagMessage(remote, (uint)guid));
        }

        public static void tpPlayer(Character player, int mapId, short cellId, DirectionsEnum playerDirection)
        {
            player.Teleport(new ObjectPosition(Singleton<World>.Instance.GetMap(mapId), cellId, playerDirection));
        }
    }
}