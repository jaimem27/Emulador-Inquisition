using Stump.DofusProtocol.Enums;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using Stump.Server.WorldServer.Handlers.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using NLog;

namespace Stump.Server.WorldServer.Game.Exchanges.Bank
{
    public class GuildBankDialog : IExchange
    {
        public GuildBankDialog(Character character)
        {
            Customer = new GuildBankCustomer(character, this);
        }

        public Character Character => Customer.Character;

        public GuildBankCustomer Customer
        {
            get;
            set;
        }

        public ExchangeTypeEnum ExchangeType => ExchangeTypeEnum.BANK;

        public DialogTypeEnum DialogType => DialogTypeEnum.DIALOG_EXCHANGE;

        public void Open()
        {
            ulong kamas = Character.Inventory.Kamas;
            ulong kamasbank = Character.GuildBank.Kamas;
            var impuesto = Character.Bank.GetAccessPrice();

            if ((int)kamasbank >= impuesto)
            {
                Character.Guild.BankInUse = 1;
                Character.Guild.BankInUseBy = Character.Name;
                Character.GuildBank.SubKamas((ulong)impuesto);
                Character.SendServerMessage("Has abierto el banco gremial de <b>" + Character.Guild.Name + "</b>, has pagado " + impuesto + " kamas de impuesto de tu banco gremial.");
                Character.GuildBank.SubKamas((ulong)impuesto);
                InventoryHandler.SendExchangeStartedWithStorageMessage(Character.Client, ExchangeType, int.MaxValue);
                InventoryHandler.SendGuildStorageInventoryContentMessage(Character.Client, Customer.Character.GuildBank);
                Character.SetDialoger(Customer);
            }
            else if ((int)kamas >= impuesto)
            {
                Character.Guild.BankInUse = 1;
                Character.Guild.BankInUseBy = Character.Name;
                Character.Inventory.SubKamas((ulong)impuesto);
                Character.SendServerMessage("Has abierto el banco gremial de <b>" + Character.Guild.Name + "</b>, has pagado " + impuesto + " kamas de impuesto de tu inventario.");
                Character.Inventory.SubKamas((ulong)impuesto);
                InventoryHandler.SendExchangeStartedWithStorageMessage(Character.Client, ExchangeType, int.MaxValue);
                InventoryHandler.SendGuildStorageInventoryContentMessage(Character.Client, Customer.Character.GuildBank);
                Character.SetDialoger(Customer);
            }
            else
            {
                Character.SendServerMessage("No hay suficientes kamas en tu inventario o en el banco gremial para abrirlo. Kamas necesarias : " + impuesto);
            }
        }



        public void Close()
        {
            InventoryHandler.SendExchangeLeaveMessage(Character.Client, DialogType, false);
            var database = WorldServer.Instance.DBAccessor.Database;
            Character.SaveLater();

            foreach (var guildmembers in Character.Guild.Members.Where(c => c.IsConnected == true))
            {
                guildmembers.Character.ReloadBank();
            }

            Character.Guild.BankInUse = 0;
            Character.ResetDialog();
        }
    }
}