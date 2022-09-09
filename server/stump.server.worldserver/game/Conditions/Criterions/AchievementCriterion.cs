using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stump.Server.WorldServer.Database.Achievements;
using Stump.Server.WorldServer.Game.Achievements;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;

namespace Stump.Server.WorldServer.Game.Conditions.Criterions
{
    public class AchievementCriterion : Criterion
    {
        public const string Identifier = "OA";

        public uint logro
        {
            set;
            get;
        }

        public override void Build()
        {
            int logroId;

            if (!int.TryParse(Literal, out logroId))
                throw new Exception(string.Format("Cannot build HasItemCriterion, {0} is not a valid item id", Literal));

            logro = (uint)logroId;
        }

        public override bool Eval(Character character)
        {
            return character.Achievement.FinishedAchievements.Any(entry => entry.Id == logro);
        }
    }
}
