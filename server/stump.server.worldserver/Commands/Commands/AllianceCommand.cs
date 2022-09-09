using Stump.DofusProtocol.Enums;
using Stump.Server.BaseServer.Commands;

namespace Stump.Server.WorldServer.Commands.Commands {
    public class AllianceCommand : SubCommandContainer {
        public AllianceCommand () {
            base.Aliases = new string[] {
                "alianza"
            };

            base.RequiredRole = RoleEnum.Administrator;
            base.Description = "La utilizacion de <b>.alianza crear</b> requiere de una <b>Alianzalogema</b>.<br><b>invitar</b> -<br>  -> invitar";
        }

    }
}