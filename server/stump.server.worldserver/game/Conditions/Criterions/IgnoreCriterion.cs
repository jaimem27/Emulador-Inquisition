using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;

namespace Stump.Server.WorldServer.Game.Conditions.Criterions {
    public class IgnoreCriterion : Criterion {
        public override bool Eval (Character character) {
            return true;
        }

        public override void Build () { }
    }
}