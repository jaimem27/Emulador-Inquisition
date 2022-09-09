using Stump.DofusProtocol.Enums;
using Stump.Server.WorldServer.Commands.Commands.Patterns;
using Stump.Server.WorldServer.Commands.Trigger;
using Stump.Server.WorldServer.Game.Dialogs.Alliances;
using System.Drawing;
using Stump.Core.Reflection;
using Stump.Server.BaseServer.Commands;
using Stump.Server.WorldServer.Game;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using Stump.Server.WorldServer.Game.Alliances;


namespace Stump.Server.WorldServer.Commands.Commands
{
    public class InvitarAlianza : TargetCommand
    {
        public InvitarAlianza()
        {
            base.Aliases = new string[]
            {
                "invitar"
            };
            RequiredRole = RoleEnum.Administrator;
            Description = "Invita al lider de un Gremio a formar parte de tu Alianza.";
            AddParameter("objetivo", "objetivo", "Lider del gremio", isOptional: false, converter: ParametersConverter.CharacterConverter);
        }

        public override void Execute(TriggerBase trigger)
        {
            var gameTrigger = trigger as GameTrigger;
            var player = gameTrigger.Character;
            Character objetivo;
            objetivo = trigger.Get<Character>("objetivo");
           
            if (objetivo == null)
            {
                player.SendServerMessage("Debes indicar el nombre del lider del Gremio que quieres invitar a la Alianza");
                return;
            }

            foreach (var target in GetTargets(trigger))
            {
                if (player.Guild?.Alliance != null)
                {
                    if (!player.GuildMember.HasRight(GuildRightsBitEnum.GUILD_RIGHT_INVITE_NEW_MEMBERS)) // ALLIANCE_RIGHT_RECRUIT_GUILDS = 8
                    {
                        player.SendServerMessage("No tienes los permisos necesarios para utilizar este comando.", Color.Crimson);
                        player.SendInformationMessage(TextInformationTypeEnum.TEXT_INFORMATION_ERROR, 207); //TODO: Explore messages to send correctly ids
                    }
                    else
                    {
                        var character = Singleton<World>.Instance.GetCharacter(objetivo.Id);
                        if (character == null)
                        {
                            player.SendServerMessage("El jugador indicado no se ha podido encontrar.", Color.Crimson);
                            player.SendInformationMessage(TextInformationTypeEnum.TEXT_INFORMATION_ERROR, 208);
                        }
                        else
                        {
                            if (character.Guild == null || character.Guild.Alliance != null || !character.GuildMember.IsBoss)
                            {
                                player.SendServerMessage("No estás en una Alianza o no eres el lider de una.", Color.Crimson);
                                player.SendInformationMessage(TextInformationTypeEnum.TEXT_INFORMATION_ERROR, 206);
                            }
                            else
                            {
                                //   alliancevar = character.Guild.Alliance;
                                if (character.IsBusy())
                                {
                                    player.SendInformationMessage(TextInformationTypeEnum.TEXT_INFORMATION_ERROR, 209);
                                }
                                else
                                {

                                    AllianceInvitationRequest guildInvitationRequest = new AllianceInvitationRequest(player, character);
                                    guildInvitationRequest.Open();
                                }
                            }
                        }
                    }
                }
                else
                {
                    player.SendServerMessage("Ha ocurrido un error, las causas pueden ser:<br>- No eres el lider de una Alianza.<br>- No tienes los permisos necesarios.<br>- El objetivo no fue encontrado.", Color.Crimson);
                }
            }
        }
    }

//    public class AllianceCreateCommand : InGameSubCommand
//    {
//        public AllianceCreateCommand () 
//{
//            base.Aliases = new string[] {
//                "crear"
//            };

//            base.RequiredRole = RoleEnum.Player;
//            ParentCommandType = typeof (AllianceCommand);
//        }

//        public override void Execute (GameTrigger trigger) {
//            var allianceCreationPanel = new AllianceCreationPanel (trigger.Character);
//            allianceCreationPanel.Open ();
//        }

   // }

    
}