using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;

namespace Stump.Server.WorldServer.Game.Conditions.Criterions {
    public class AllianceValidCriterion : Criterion {
        public const string Identifier = "Ow";

        public int Emote {
            get;
            set;
        }

        public override bool Eval (Character character) => character.Guild.Alliance != null;

        public override void Build () {

        }

        public override string ToString () => FormatToString (Identifier);
    }
}