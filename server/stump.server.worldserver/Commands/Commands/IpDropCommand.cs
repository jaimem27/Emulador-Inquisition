using Stump.DofusProtocol.Enums;
using Stump.Server.WorldServer.Commands.Commands.Patterns;
using Stump.Server.WorldServer.Commands.Trigger;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;

namespace Stump.Server.WorldServer.Commands.Commands
{
    class IpDropCommand : InGameCommand
    {
        public IpDropCommand()
        {
            base.Aliases = new[] { "ipdrop" };
            base.Description = "El personaje que activó este comando recibirá todas los recursos de todos tus personajes.";
            base.RequiredRole = RoleEnum.Player;
        }
        public override void Execute(GameTrigger trigger)
        {
            Character player = trigger.Character;
            if (!player.IsIpDrop)
            {
                player.IsIpDrop = true;
                player.SendServerMessage("Has activado el Drop por IP.");
            }
            else
            {
                player.IsIpDrop = false;
                player.SendServerMessage("Has desactivado el Drop por IP.");
            }
        }
    }
}
