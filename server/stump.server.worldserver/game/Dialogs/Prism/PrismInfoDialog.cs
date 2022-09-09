using Stump.DofusProtocol.Enums;
using Stump.DofusProtocol.Messages;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using Stump.Server.WorldServer.Game.Prisms;
using Stump.Server.WorldServer.Handlers.Dialogs;

namespace Stump.Server.WorldServer.Game.Dialogs.Prisms {
    public class PrismInfoDialog : IDialog {
        public PrismInfoDialog (Character character, PrismNpc prism) {
            Prism = prism;
            Character = character;
        }

        public PrismNpc Prism { get; }
        public Character Character { get; }
        public DialogTypeEnum DialogType => DialogTypeEnum.DIALOG_DIALOG;

        public void Close () {
            Character.CloseDialog (this);
            Prism.OnDialogClosed (this);

            DialogHandler.SendLeaveDialogMessage (Character.Client, DialogType);
        }

        public void Open () {
            Character.SetDialog (this);
            Prism.OnDialogOpened (this);
            Character.Client.Send (new NpcDialogCreationMessage (Prism.Map.Id, Prism.Id));
            Character.Client.Send (new AlliancePrismDialogQuestionMessage ());
        }
    }
}