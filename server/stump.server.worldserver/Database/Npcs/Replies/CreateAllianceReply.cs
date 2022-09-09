using Stump.Server.BaseServer.Database;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Npcs;
using Stump.Server.WorldServer.Game.Dialogs.Alliances;

namespace Stump.Server.WorldServer.Database.Npcs.Replies {
    [Discriminator ("AllianceCreation", typeof (NpcReply), typeof (NpcReplyRecord))]
    public class CreateAllianceReply : NpcReply {
        public CreateAllianceReply (NpcReplyRecord record) : base (record) {

        }
        public override bool CanExecute (Npc npc, Character character) {
            if (character.Guild != null) {
                return true;
            }

            switch (character.Account.Lang) {
                case "fr":
                    character.SendServerMessage ("Vous n'avez pas de guilde. Veuillez être dans une guilde pour pouvoir créer une alliance.", System.Drawing.Color.Red);
                    break;
                case "es":
                    character.SendServerMessage ("No tienes un gremio Por favor, estar en un gremio para crear una alianza.", System.Drawing.Color.Red);
                    break;
                case "en":
                    character.SendServerMessage ("You do not have a guild. Please be in a guild to create an alliance.", System.Drawing.Color.Red);
                    break;
                default:
                    character.SendServerMessage ("Você não tem uma guilda. Por favor, esteja em uma guilda para criar uma aliança.", System.Drawing.Color.Red);
                    break;
            }
            return false;
        }
        public override bool Execute (Npc npc, Character character) {
            try {
                AllianceCreationPanel panel = new AllianceCreationPanel (character);
                panel.Open ();
                return true;
            } catch {
                return false;
            }
        }
    }
}