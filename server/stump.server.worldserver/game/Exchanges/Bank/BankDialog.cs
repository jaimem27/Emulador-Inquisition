using Stump.DofusProtocol.Enums;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using Stump.Server.WorldServer.Handlers.Inventory;

namespace Stump.Server.WorldServer.Game.Exchanges.Bank
{
    public class BankDialog : IExchange
    {
        public BankDialog(Character character)
        {
            Customer = new BankCustomer(character, this);
        }

        public Character Character => Customer.Character;

        public BankCustomer Customer
        {
            get;
            set;
        }

        public ExchangeTypeEnum ExchangeType => ExchangeTypeEnum.BANK;

        public DialogTypeEnum DialogType => DialogTypeEnum.DIALOG_EXCHANGE;

        public void Open()
        {
            
            ulong kamas = Character.Inventory.Kamas;
            ulong kamasbank = Character.Bank.Kamas;
            var impuesto = Character.Bank.GetAccessPrice();

            if((int)kamasbank >=  impuesto)
            {
                Character.Bank.SubKamas((ulong)impuesto);
                InventoryHandler.SendExchangeStartedWithStorageMessage(Character.Client, ExchangeType, int.MaxValue);
                InventoryHandler.SendStorageInventoryContentMessage(Character.Client, Customer.Character.Bank);
                Character.SetDialoger(Customer);
                Character.SendServerMessage("Has gastado " + impuesto + " kamas para abrir el banco.");
                Character.RefreshActor();
            }
            else if ((int)kamas >= impuesto)
            {
                Character.Inventory.SubKamas((ulong)impuesto);
                InventoryHandler.SendExchangeStartedWithStorageMessage(Character.Client, ExchangeType, int.MaxValue);
                InventoryHandler.SendStorageInventoryContentMessage(Character.Client, Customer.Character.Bank);
                Character.SetDialoger(Customer);
                Character.SendServerMessage("Has gastado " + impuesto + " kamas para abrir el banco.");
                Character.RefreshActor();
            }
            else
            {
                Character.SendServerMessage("No tiene suficientes kamas para abrir el banco. Kamas necesarias : " + impuesto);
            }
            Character.RefreshActor();
        }

        public void Close()
        {
            InventoryHandler.SendExchangeLeaveMessage(Character.Client, DialogType, false);
            Character.ResetDialog();
        }
    }
}