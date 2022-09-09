using System;
using System.Collections.Generic;
using System.Linq;
using Stump.Core.Reflection;
using Stump.DofusProtocol.Enums;
using Stump.DofusProtocol.Messages;
using Stump.DofusProtocol.Types;
using Stump.Server.BaseServer.Network;
using Stump.Server.WorldServer.Core.Network;
using Stump.Server.WorldServer.Database.Items.Templates;
using Stump.Server.WorldServer.Game;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using Stump.Server.WorldServer.Game.Alliances;
using Stump.Server.WorldServer.Game.Exchanges.Prism;
using Stump.Server.WorldServer.Game.Fights;
using Stump.Server.WorldServer.Game.Items;
using Stump.Server.WorldServer.Game.Items.Prism;
using Stump.Server.WorldServer.Game.Maps;
using Stump.Server.WorldServer.Game.Prisms;
using Stump.Server.WorldServer.Handlers.Context;

namespace Stump.Server.WorldServer.Handlers.Prism {
    public class PrismHandler : WorldHandlerContainer {
        //For captura prism
        //PrismListUpdate = state 5 and info
        //UpdateSelfAgressableStatusMessage | status = 23 | probationtime = now - counter time 2min I think or 1:30 idk
        //just send if its valid (not died, if died it's revived by potion)
        //get probation time ready all send  UpdateSelfAgressableStatusMessage | status = 20 | probationtime = 0
        //in this case server send REN still the zone (another zone that I don't defend) and send =
        //TextInformationMessage | type = 2 | Id = 89 | element 834 (no idea) //La zona «<b>$subarea%1</b>» es vulnerable. Puedes intentar conquistarla.
        //When exit zone PrismListUpdate = subareaid and alliance = 0
        //when death status = 21
        //every 2 min KohUpdateMessage (panel)

        //death message type = 0 | id = 34 | param = energy
        //RoleplayPlayerLifeStatus | state 1 | phoenixmapid = 0 ?
        //GameRolePlayFreeSoulRequest (go cementery)
        //RoleplayPlayerLifeStatus | state 2 | phoenixmapid = the nearest mapid ?
        //phoenix skill 211
        //Whe use RoleplayPlayerLifeStatus | state 0 | phoenixmapid = 0 ?
        //ShowActor normal

        //When town it's not from allie send TextInfo type 2 | id 408, also can't use zappi etc

        //If defend your alliance msgtype = 2 | id = 92 | params = subzoneId - AllianceId - TAG

        //Las pepitas se reparten por los spawn activos en la zona, siguientes no tendrán pepas!
        //En el templo de las alianzas elije si enterrador o resucitador!

        //If win zone Info tpye = 2 | Id 93 | quantity pepas
        //msgtype = 2 | id = 92 | params = subzoneId - AllianceId - TAG
        //NotificationByServerMessage | id 36 | param AllianceId - TAG | forceopen = false
        //PrismListUpdate = own zone x 2 | first state = 0 | after state = 1
        //Monsters on Zone = HasAVARewardToken = true!

        //private PrismHandler() { }

        [WorldHandler (PrismAttackRequestMessage.Id)]
        public static void HandlePrismAttackRequestMessage (WorldClient client, PrismAttackRequestMessage message) {

            var prism = client.Character.Map.Prism;
            if (prism != null) {

                var fighterRefusedReasonEnum = client.Character.CanAttack (prism);
                if (fighterRefusedReasonEnum != FighterRefusedReasonEnum.FIGHTER_ACCEPTED) {

                    ContextHandler.SendChallengeFightJoinRefusedMessage (client, client.Character, fighterRefusedReasonEnum);
                    return;
                }
                var fightPvT = Singleton<FightManager>.Instance.CreatePvMrFight (client.Character.Map);
                fightPvT.ChallengersTeam.AddFighter (client.Character.CreateFighter (fightPvT.ChallengersTeam));
                fightPvT.DefendersTeam.AddFighter (prism.CreateFighter (fightPvT.DefendersTeam));
                fightPvT.StartPlacement ();
            }

        }

        [WorldHandler (PrismSettingsRequestMessage.Id)]
        public static void HandlePrismSettingsRequestMessage (WorldClient client, PrismSettingsRequestMessage message) {
            //TODO KOH time
            if (client.Character.IsBusy ())
                return;
            var prism = client.Character.Map.Prism;
            if (client.Character.Guild?.Alliance != null && prism != null)
            {
                prism.Record.LastTimeSlotModificationDate = new DateTime();
                prism.Record.LastTimeSlotModificationAuthorId = client.Character.Id;
                prism.Record.LastTimeSlotModificationAuthorName = client.Character.Name;
                prism.Record.LastTimeSlotModificationAuthorGuildId = client.Character.Guild.Id;
                var date = message.StartDefenseTime & 0xff;
                //prism.Record.NextDate = DateTime.Parse(date.ToString());
                //prism.NextDate = DateTime.Parse(date.ToString());
                //prism.Save();
            }
        }

        [WorldHandler (PrismUseRequestMessage.Id)]
        public static void HandlePrismUseRequestMessage (WorldClient client, PrismUseRequestMessage message) {
            var prism = client.Character.Map.Prism;
            prism.Record.IsSabotaged = false;
            prism.IsSabotaged = false;
        }

        [WorldHandler (PrismSetSabotagedRequestMessage.Id)]
        public static void HandlePrismSetSabotagedRequestMessage (WorldClient client, PrismSetSabotagedRequestMessage message) {
            if (client.Character.IsBusy ())
                return;
            var prism = client.Character.Map.Prism;
            prism.Record.IsSabotaged = false;
            prism.IsSabotaged = false;
            if (client.Character.Guild?.Alliance != null && prism != null && prism.IsSabotaged)
            {
                client.Send(new PrismSetSabotagedRefusedMessage((ushort) client.Character.Map.SubArea.Id, (sbyte) PrismSetSabotagedRefusedReasonEnum.SABOTAGE_WRONG_STATE));
            }
            if (client.Character.Guild?.Alliance != null && prism != null  && !prism.IsSabotaged)
            {
                prism.Record.IsSabotaged = true;
                prism.IsSabotaged = true;
            }
            else 
            {
                client.Send(new PrismSetSabotagedRefusedMessage((ushort) client.Character.Map.SubArea.Id, (sbyte) PrismSetSabotagedRefusedReasonEnum.SABOTAGE_MEMBER_ACCOUNT_NEEDED));
            }
        }

        [WorldHandler (PrismModuleExchangeRequestMessage.Id)]
        public static void HandlePrismModuleExchangeRequestMessage (WorldClient client, PrismModuleExchangeRequestMessage message) {
            if (client.Character.IsBusy ())
                return;
            var prism = client.Character.Map.Prism;
            if (client.Character.Guild?.Alliance != null && prism != null)
            {
                var pepitas = prism.GetNuggetCount();
                var newItem = ItemManager.Instance.TryGetTemplate(14635);
                var baseitem = ItemManager.Instance.CreatePlayerItem(client.Character, newItem, 1);
                var itemprism = ItemManager.Instance.CreatePrismItem(baseitem, prism.Id);
                prism.Bag.DeleteBag();
                prism.Bag.AddItem(itemprism);
                prism.Bag.StackItem(itemprism, (int) pepitas);
                var alliancePrism = new AlliancePrismTrade (prism, client.Character);
                alliancePrism.Open ();
            }
        }

        [WorldHandler (PrismsListRegisterMessage.Id)]
        public static void HandlePrismsListRegisterMessage (WorldClient client, PrismsListRegisterMessage message) {
            SendPrismsListMessage (client);

            if (client.Character.Guild?.Alliance != null && message.Listen == 1)
                client.Send(new PrismsListMessage(client.Character.Guild?.Alliance.GetPrismsInformations().ToArray()));
            if (message.Listen == 2)
            {
                SendPrismsListMessage(client);
                client.Character.Guild?.Alliance?.SendPrismsInfoValidMessage();
            }
        }

        public static void SendPrismFightAttackerAddMessage (IPacketReceiver client, SubArea subarea, FightPvMr fight, Character fighter) {
            client.Send (new PrismFightAttackerAddMessage ((ushort) subarea.Id, (ushort) fight.Id,
                fighter.GetCharacterMinimalPlusLookInformations ()));
        }

        public static void SendPrismFightAttackerRemoveMessage (IPacketReceiver client, SubArea subarea, FightPvMr fight, Character fighter) {
            client.Send (new PrismFightAttackerRemoveMessage ((ushort) subarea.Id, (ushort) fight.Id,
                (ulong) fighter.Fighter.Id));
        }

        public static void SendPrismFightDefenderAddMessage (IPacketReceiver client, SubArea subarea, FightPvMr fight, Character fighter) {
            client.Send (new PrismFightDefenderAddMessage ((ushort) subarea.Id, (ushort) fight.Id,
                fighter.GetCharacterMinimalPlusLookInformations ()));
        }

        public static void SendPrismFightDefenderRemoveMessage (IPacketReceiver client, SubArea subarea, FightPvMr fight, Character fighter) {
            client.Send (new PrismFightDefenderLeaveMessage ((ushort) subarea.Id, (ushort) fight.Id,
                (ulong) fighter.Fighter.Id));
        }

        public static void SendPrismsListUpdateMessage (IPacketReceiver client, PrismNpc prism, bool ownPrism) {
            var list = new List<PrismSubareaEmptyInfo> {
                new PrismGeolocalizedInformation ((ushort) prism.SubArea.Id, (uint) prism.Alliance.Id, (short) prism.Map.Position.X,
                (short) prism.Map.Position.Y, prism.Map.Id, ownPrism ? prism.GetAllianceInsiderPrismInformation () : prism.GetAlliancePrismInformation ())
            };
            client.Send (new PrismsListUpdateMessage (list.ToArray()));
        }

        public static void SendKohUpdateMessage (IPacketReceiver client, IEnumerable<Alliance> alliances, IEnumerable<short> allianceNbMembers,
            IEnumerable<int> allianceRoundWeigth, IEnumerable<sbyte> allianceMatchScore) {
            List<BasicAllianceInformations> basicAllianceInformations = new List<BasicAllianceInformations> ();

            client.Send (new KohUpdateMessage (alliances.Select (x => x.GetAllianceInformations ()).ToArray(), allianceNbMembers.Select (x => (ushort) x).ToArray(), allianceRoundWeigth.Select (x => (uint) x).ToArray(), allianceMatchScore.Select (x => (byte) x).ToArray(), basicAllianceInformations.ToArray(), 0, 0, 0));
        }

        public static void SendKohUpdateMessage (IPacketReceiver client, IEnumerable<Alliance> alliances, IEnumerable<short> allianceNbMembers,
            IEnumerable<int> allianceRoundWeigth, IEnumerable<sbyte> allianceMatchScore, IEnumerable<BasicAllianceInformations> winningMapList, int allianceMapWinnerScore, int allianceMapMyAllianceScore) {
            client.Send (new KohUpdateMessage (alliances.Select (x => x.GetAllianceInformations ()).ToArray(), allianceNbMembers.Select (x => (ushort) x).ToArray(), allianceRoundWeigth.Select (x => (uint) x).ToArray(),
                allianceMatchScore.Select (x => (byte) x).ToArray(), winningMapList.ToArray(), (uint) allianceMapWinnerScore, (uint) allianceMapMyAllianceScore, 0));
        }

        public static void SendUpdateSelfAgressableStatusMessage (IPacketReceiver client, sbyte status, int time) {
            client.Send (new UpdateSelfAgressableStatusMessage (status, time));
        }

        public static void SendPrismsListMessage (WorldClient client) //TODO AllianceInsiderPrismInformation, this is send when actual client have same alliance of prism in area!
        {
            var prisms = Singleton<PrismManager>.Instance.GetPrismSpawns ();
            var subareas = Singleton<World>.Instance.GetSubAreas ().Where (x => x.Record.Capturable).ToList ();
            var list = subareas.Select (subarea => new PrismSubareaEmptyInfo ((ushort) subarea.Id, 0)).ToList ();
            foreach (var prism in prisms) {
                var subarea = list.FirstOrDefault (x => x.subAreaId == prism.Map.SubArea.Record.Id);
                if (subarea != null) {
                    if (client.Character.Guild?.Alliance != null && prism.AllianceId == client.Character.Guild?.Alliance.Id) {
                        var alliancePrism = client.Character.Guild?.Alliance?.Prisms?.FirstOrDefault (x => x.Record.Id == prism.Id);
                        if (alliancePrism != null)
                            list[list.IndexOf (subarea)] = new PrismGeolocalizedInformation ((ushort) alliancePrism.SubArea.Id,
                                (uint) alliancePrism.Alliance.Id, (short) alliancePrism.Map.Position.X,
                                (short) alliancePrism.Map.Position.Y, alliancePrism.Map.Id,
                                alliancePrism.GetAllianceInsiderPrismInformation ());
                    } else {
                        var alliance = Singleton<AllianceManager>.Instance.TryGetAlliance ((int) prism.AllianceId);
                        var alliancePrism = alliance.Prisms.FirstOrDefault (x => x.Record.Id == prism.Id);
                        if (alliancePrism != null)
                            list[list.IndexOf (subarea)] = new PrismGeolocalizedInformation ((ushort) alliancePrism.SubArea.Id,
                                (uint) alliancePrism.Alliance.Id, (short) alliancePrism.Map.Position.X,
                                (short) alliancePrism.Map.Position.Y, alliancePrism.Map.Id,
                                alliancePrism.GetAlliancePrismInformation ());
                    }

                }
            }
            client.Send (new PrismsListMessage (list.ToArray()));
        }
    }
}