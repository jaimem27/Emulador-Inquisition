using System;
using System.Globalization;
using System.Linq;
using MongoDB.Bson;
using Stump.Core.Extensions;
using Stump.Core.Reflection;
using Stump.DofusProtocol.Enums;
using Stump.DofusProtocol.Enums.Custom;
using Stump.DofusProtocol.Messages;
using Stump.DofusProtocol.Types;
using Stump.Server.BaseServer.Logging;
using Stump.Server.BaseServer.Network;
using Stump.Server.WorldServer.Core.Network;
using Stump.Server.WorldServer.Game;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using Stump.Server.WorldServer.Game.Alliances;
using Stump.Server.WorldServer.Game.Dialogs.Alliances;
using Stump.Server.WorldServer.Game.Guilds;
using Stump.Server.WorldServer.Handlers.Prism;
using Stump.Server.WorldServer.Handlers.TaxCollector;

namespace Stump.Server.WorldServer.Handlers.Alliances
{
    public class AllianceHandler : WorldHandlerContainer
    {
        private AllianceHandler()
        {
        }

        [WorldHandler(AllianceCreationValidMessage.Id)]
        public static void HandleAllianceCreationValidMessage(WorldClient client, AllianceCreationValidMessage message)
        {
            var allianceCreationPanel = client.Character.Dialog as AllianceCreationPanel;
            allianceCreationPanel?.CreateAlliance(message.AllianceName, message.AllianceTag, message.AllianceEmblem);
        }

        internal static void SendAllianceInvitedMessage(WorldClient client, Character source)
        {
            client.Send(new AllianceInvitedMessage((ulong)source.Id, source.Name,
                source.Guild.Alliance.GetBasicNamedAllianceInformations()));
        }

        internal static void SendAllianceInvitationStateRecrutedMessage(WorldClient client,
            GuildInvitationStateEnum gUILD_INVITATION_SENT)
        {
            client.Send(new AllianceInvitationStateRecrutedMessage((sbyte)gUILD_INVITATION_SENT));
        }

        [WorldHandler(AllianceInvitationAnswerMessage.Id)]
        public static void HandleAllianceInvitationAnswerMessage(WorldClient client,
            AllianceInvitationAnswerMessage message)
        {
            var request = client.Character.RequestBox as AllianceInvitationRequest;

            if (request == null)
                return;

            if (client.Character == request.Source && !message.Accept)
                request.Cancel();
            else if (client.Character == request.Target)
            {
                if (message.Accept)
                    request.Accept();
                else
                    request.Deny();
            }
        }

        [WorldHandler(AllianceInsiderInfoRequestMessage.Id)]
        public static void HandleAllianceInsiderInfoRequestMessage(WorldClient client,
            AllianceInsiderInfoRequestMessage message)
        {
            if (client.Character.Guild?.Alliance != null)
                SendAllianceInsiderInfoMessage(client, client.Character.Guild.Alliance);
        }

        [WorldHandler(AllianceInvitationMessage.Id)]
        public static void HandleAllianceInvitationMessage(WorldClient client, AllianceInvitationMessage message)
        {
            Console.WriteLine(message.TargetId);
            if (client.Character.Guild?.Alliance != null)
            {
                if (!client.Character.GuildMember.HasRight(GuildRightsBitEnum.GUILD_RIGHT_INVITE_NEW_MEMBERS)
                ) // ALLIANCE_RIGHT_RECRUIT_GUILDS = 8
                {
                    client.Character.SendInformationMessage(TextInformationTypeEnum.TEXT_INFORMATION_ERROR,
                        207); //TODO: Explore messages to send correctly ids
                }
                else
                {
                    var character = Singleton<World>.Instance.GetCharacter((int)message.TargetId);
                    if (character == null)
                    {
                        client.Character.SendInformationMessage(TextInformationTypeEnum.TEXT_INFORMATION_ERROR, 208);
                    }
                    else
                    {
                        if (character.Guild == null || character.Guild.Alliance != null ||
                            !character.GuildMember.IsBoss)
                        {
                            client.Character.SendInformationMessage(TextInformationTypeEnum.TEXT_INFORMATION_ERROR,
                                206);
                        }
                        else
                        {
                            //   alliancevar = character.Guild.Alliance;
                            if (character.IsBusy())
                            {
                                client.Character.SendInformationMessage(TextInformationTypeEnum.TEXT_INFORMATION_ERROR,
                                    209);
                            }
                            else
                            {
                                AllianceInvitationRequest guildInvitationRequest =
                                    new AllianceInvitationRequest(client.Character, character);
                                guildInvitationRequest.Open();
                            }
                        }
                    }
                }
            }
        }

        [WorldHandler(AllianceFactsRequestMessage.Id)]
        public static void HandleAllianceFactsRequestMessage(WorldClient client, AllianceFactsRequestMessage message)
        {
            var alliance = Singleton<AllianceManager>.Instance.TryGetAlliance((int)message.AllianceId);
            if (alliance != null)
                SendAllianceFactsMessage(client, alliance);
        }

        [WorldHandler(SetEnableAVARequestMessage.Id)]
        public static void HandleSetEnableAVARequestMessage(WorldClient client, SetEnableAVARequestMessage message)
        {
            //if combat zone TextInfo | type 1 | Id 339
            if (client.Character.SubArea.HasPrism)
            {
                if (client.Character.SubArea.Prism.State == PrismStateEnum.PRISM_STATE_VULNERABLE)
                {
                    client.Character.SendInformationMessage(TextInformationTypeEnum.TEXT_INFORMATION_ERROR, 339);
                    return;
                }
            }

            if (client.Character.Guild?.Alliance == null)
                return;

            client.Character.AvAActived = message.Enable;

            //TODO
        }

        [WorldHandler(AllianceMotdSetRequestMessage.Id)]
        public static void HandleAllianceMotdSetRequestMessage(WorldClient client,
            AllianceMotdSetRequestMessage message)
        {
            if (client.Character.GuildMember == null)
            {
                SendAllianceMotdSetErrorMessage(client);
                return;
            }

            client.Character.Guild?.Alliance.UpdateMotd(client.Character.GuildMember, message.Content);
            client.Character.Guild?.Alliance.Save(WorldServer.Instance.DBAccessor.Database);
        }

        [WorldHandler(AllianceBulletinSetRequestMessage.Id)]
        public static void HandleAllianceBulletinSetRequestMessage(WorldClient client,
            AllianceBulletinSetRequestMessage message)
        {
            if (client.Character.GuildMember == null)
            {
                SendAllianceBulletinSetErrorMessage(client, SocialNoticeErrorEnum.SOCIAL_NOTICE_UNKNOWN_ERROR);
                return;
            }

            if (client.Character.GuildMember.RankId > 1)
            {
                SendAllianceBulletinSetErrorMessage(client, SocialNoticeErrorEnum.SOCIAL_NOTICE_INVALID_RIGHTS);
                return;
            }

            client.Character.Guild.Alliance.UpdateBulletin(client.Character.GuildMember, message.Content,
                message.NotifyMembers);
            client.Character.Guild?.Alliance.Save(WorldServer.Instance.DBAccessor.Database);
        }

        [WorldHandler(GuildFactsRequestMessage.Id)]
        public static void HandleGuildFactsRequestMessage(WorldClient client, GuildFactsRequestMessage message)
        {
            var guild = Singleton<GuildManager>.Instance.TryGetGuild((int)message.guildId);
            if (guild != null)
            {
                SendGuildFactsMessage(client, guild);
            }
        }

        public static void SendAllianceBulletinMessage(IPacketReceiver client, Alliance alliance)
        {
            try
            {
                client.Send(new AllianceBulletinMessage(alliance.BulletinContent,
                    alliance.BulletinDate.GetUnixTimeStamp(), (ulong)alliance.BulletinMember.Id,
                    alliance.BulletinMember.Name, alliance.LastNotifiedDate.GetUnixTimeStamp()));
            }
            catch
            {
            }
        }

        public static void SendAllianceBulletinSetErrorMessage(IPacketReceiver client, SocialNoticeErrorEnum error)
        {
            client.Send(new AllianceBulletinSetErrorMessage((sbyte)error));
        }

        public static void SendAllianceMotdSetErrorMessage(IPacketReceiver client)
        {
            client.Send(new AllianceMotdSetErrorMessage(0));
        }

        public static void SendAllianceMotdMessage(IPacketReceiver client, Alliance alliance)
        {
            client.Send(new AllianceMotdMessage(alliance.MotdContent, alliance.MotdDate.GetUnixTimeStamp(),
                (ulong?)alliance.MotdMember?.Id ?? 0, alliance.MotdMember?.Name ?? "Unknown"));
        }

        public static void SendAllianceCreationStartedMessage(IPacketReceiver client)
        {
            client.Send(new AllianceCreationStartedMessage());
        }

        public static void SendAllianceCreationResultMessage(IPacketReceiver client,
            SocialGroupCreationResultEnum result)
        {
            client.Send(new AllianceCreationResultMessage((sbyte)result));
        }

        public static void SendAllianceInsiderInfoMessage(IPacketReceiver client, Alliance alliance)
        {
            client.Send(new AllianceInsiderInfoMessage(
                alliance.GetAllianceFactSheetInformations(),
                alliance.GetGuildsInformations().ToArray(),
                alliance.GetPrismsInformations().ToArray()));
            foreach (var a in alliance.m_guilds)
                TaxCollectorHandler.SendTaxCollectorListMessage(client, a.Value);
        }

        public static void SendGuildFactsMessage(IPacketReceiver client, Guild guild)
        {
            client.Send(guild.GetGuildFactsMessage());
        }

        public static void SendAllianceJoinedMessage(IPacketReceiver client, Alliance alliance, bool test = false)
        {
            client.Send(new AllianceJoinedMessage(
                new AllianceInformations((uint)alliance.Id, alliance.Tag, alliance.Name,
                    alliance.Emblem.GetNetworkGuildEmblem()),
                test, (uint)alliance.Boss.Id));
        }

        public static void SendAllianceMembershipMessage(IPacketReceiver client, Alliance alliance, bool test = false)
        {
            client.Send(
                new AllianceMembershipMessage(alliance.GetAllianceInformations(), test, (uint)alliance.Boss.Id));
        }

        public static void SendAllianceFactsMessage(IPacketReceiver client, Alliance alliance)
        {
            client.Send(new AllianceFactsMessage(
                alliance.GetAllianceFactSheetInformations(),
                alliance.GetGuildsInAllianceInformations().ToArray(),
                alliance.Prisms.Select(x => (ushort)x.SubArea.Id).ToArray(),
                (uint)alliance.Boss.Boss.Id,
                alliance.Boss.Boss.Name));
        }

        internal static void SendAllianceInvitationStateRecruterMessage(WorldClient client, Character target,
            GuildInvitationStateEnum gUILD_INVITATION_SENT)
        {
            client.Send(new AllianceInvitationStateRecruterMessage(target.Name, (sbyte)gUILD_INVITATION_SENT));
        }

        [WorldHandler(AllianceChangeGuildRightsMessage.Id)]
        public static void HandleAllianceChangeGuildRightsMessage(WorldClient client,
            AllianceChangeGuildRightsMessage message)
        {
            if (client.Character.Guild == null)
                return;
            if (client.Character.GuildMember == null)
            {
                return;
            }

            if (client.Character.Guild.Alliance == null)
                return;
            var alliance = Singleton<AllianceManager>.Instance.TryGetAlliance((int)client.Character.Guild.Alliance.Id);
            if (alliance == null)
                return;
            if (alliance.Boss != client.Character.Guild)
                return;
            var target = alliance.GetGuildById((uint)message.GuildId);
            if (target == null)
                return;
            if (client.Character.GuildMember.RankId > 1 || message.Rights > 1) // idk message..rights????
                return;

            alliance.SetBoss(target); //(client.Character.Guild?.Alliance.Clients
            //SendAllianceInsiderInfoMessage(client, client.Character.Guild?.Alliance);
            //SendAllianceMembershipMessage(client, client.Character.Guild?.Alliance, false);

            SendAllianceInsiderInfoMessage(client.Character.Guild?.Alliance.Clients, client.Character.Guild?.Alliance);
            client.Character.Guild?.Alliance.Save(WorldServer.Instance.DBAccessor.Database);
            //SendAllianceFactsMessage(client, client.Character.Guild?.Alliance);
            //SendAllianceJoinedMessage(client, alliance,true);
        }

        [WorldHandler(AllianceKickRequestMessage.Id)]
        public static void HandleAllianceKickRequestMessage(WorldClient client, AllianceKickRequestMessage message)
        {
            if (client.Character.Guild == null)
                return;
            if (client.Character.GuildMember == null)
            {
                return;
            }

            if (client.Character.Guild.Alliance == null)
                return;
            var alliance = Singleton<AllianceManager>.Instance.TryGetAlliance((int)client.Character.Guild.Alliance.Id);
            if (alliance == null)
                return;

            var target = alliance.GetGuildById((uint)message.KickedId);
            if (target == null)
                return;
            if (alliance.Boss != client.Character.Guild)
                if (target != client.Character.Guild)
                    return;
            if (alliance.Boss.Boss != client.Character.GuildMember) // if are alliance boss
            {
                if (target.Boss != client.Character.GuildMember) // also if are boss guild
                {
                    return;
                }
                else
                {
                    alliance.Clients.Send(new AllianceGuildLeavingMessage(false, (uint)target.Id));
                }
            }
            else if (alliance.Boss != target)
            {
                alliance.Clients.Send(new AllianceGuildLeavingMessage(true, (uint)target.Id));
            }
            else
            {
                alliance.Clients.Send(new AllianceGuildLeavingMessage(false, (uint)target.Id));
            }

            alliance.KickGuild(target);

            SendAllianceInsiderInfoMessage(alliance.Clients, alliance);
            target.Save(WorldServer.Instance.DBAccessor.Database);
            alliance.Save(WorldServer.Instance.DBAccessor.Database);
            var document = new BsonDocument
            {
                {"allianceId", alliance.Id}, {"allianceName", alliance.Name}, {"guildId", target.Id},
                {"guildName", target.Name}, {"removedbyId", client.Character.Id},
                {"removedbyname", client.Character.Name},

                {"Date", DateTime.Now.ToString(CultureInfo.InvariantCulture)}
            };
            MongoLogger.Instance.Insert("AllianceKickGuild", document);
        }

        //AllianceLeftMessage
    }
}