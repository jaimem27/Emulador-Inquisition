using Stump.Core.Reflection;
using Stump.Server.BaseServer.Database;
using Stump.Server.WorldServer.Database.Interactives;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using Stump.Server.WorldServer.Game.Alliances;
using Stump.Server.WorldServer.Game.Dialogs.Alliances;
using Stump.Server.WorldServer.Game.Items;

namespace Stump.Server.WorldServer.Game.Interactives.Skills {
    [Discriminator ("AllianceCreation", typeof (Skill), typeof (int), typeof (InteractiveCustomSkillRecord), typeof (InteractiveObject))]

    public class SkillAllianceCreation : CustomSkill {
        public SkillAllianceCreation (int id, InteractiveCustomSkillRecord record, InteractiveObject interactiveObject) : base (id, record, interactiveObject) { }
        public override bool IsEnabled (Character character) {
            var ItemPlayer = character.Inventory.TryGetItem (Singleton<ItemManager>.Instance.TryGetTemplate (AllianceManager.ALLIOGEMME_ID));
            if (ItemPlayer != null && character.Guild.Alliance == null)
                return true;
            return false;
        }
        public override int StartExecute (Character character) {

            var ItemPlayer = character.Inventory.TryGetItem (Singleton<ItemManager>.Instance.TryGetTemplate (AllianceManager.ALLIOGEMME_ID));
            if (ItemPlayer != null && character.Guild.Alliance == null) {
                var PanelAlliance = new AllianceCreationPanel (character);
                PanelAlliance.Open ();
            }
            return base.StartExecute (character);

        }

    }
}