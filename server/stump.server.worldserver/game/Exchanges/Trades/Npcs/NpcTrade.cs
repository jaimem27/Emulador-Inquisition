using MongoDB.Bson;
using Stump.Core.Extensions;
using Stump.Core.IO;
using Stump.Core.Reflection;
using Stump.DofusProtocol.Enums;
using Stump.Server.BaseServer.Logging;
using Stump.Server.WorldServer.Database.Items.Templates;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Npcs;
using Stump.Server.WorldServer.Game.Exchanges.Trades;
using Stump.Server.WorldServer.Game.Exchanges.Trades.Npcs;
using Stump.Server.WorldServer.Game.Exchanges.Trades.Players;
using Stump.Server.WorldServer.Game.Items;
using Stump.Server.WorldServer.Game.Items.Player;
using Stump.Server.WorldServer.Handlers.Inventory;
using System.Collections.Generic;
using System.Linq;
namespace Stump.Server.WorldServer.Game.Exchanges
{
    public class NpcTrade : Trade<PlayerTrader, NpcTrader>
    {
        public int Kamas
        {
            get;
            set;
        }
        public int ItemIdToReceive
        {
            get;
            set;
        }
        public int ItemIdToGive
        {
            get;
            set;
        }
        public double RateItem
        {
            get;
            set;
        }

        public uint[][] Itens
        {
            get;
            set;
        }

        public override ExchangeTypeEnum ExchangeType
        {
            get
            {
                return ExchangeTypeEnum.NPC_TRADE;
            }
        }
        public NpcTrade(Character character, Npc npc, int kamas, int itemToGive, int itemToReceive, double rateItem, string itens)
        {
            Kamas = kamas;
            ItemIdToGive = itemToGive;
            ItemIdToReceive = itemToReceive;
            RateItem = rateItem;
            Itens = (itens != null) ? itens.FromCSV("|", x => x.FromCSV<uint>(";")) : null;
            base.FirstTrader = new PlayerTrader(character, this);
            base.SecondTrader = new NpcTrader(npc, this);
        }
        public override void Open()
        {
            base.Open();
            base.FirstTrader.Character.SetDialoger(base.FirstTrader);
            InventoryHandler.SendExchangeStartOkNpcTradeMessage(base.FirstTrader.Character.Client, this);
        }
        public override void Close()
        {
            base.Close();
            InventoryHandler.SendExchangeLeaveMessage(base.FirstTrader.Character.Client, DialogTypeEnum.DIALOG_EXCHANGE, base.FirstTrader.ReadyToApply);
            base.FirstTrader.Character.CloseDialog(this);
        }
        protected override void Apply()
        {
            if (base.FirstTrader.Items.All(delegate (TradeItem x)
            {
                BasePlayerItem basePlayerItem = base.FirstTrader.Character.Inventory.TryGetItem(x.Guid);
                return basePlayerItem != null && basePlayerItem.Stack >= x.Stack;
            }))
            {
                base.FirstTrader.Character.Inventory.SetKamas((ulong)((long)base.FirstTrader.Character.Inventory.Kamas + (long)((ulong)(base.SecondTrader.Kamas - base.FirstTrader.Kamas))));
                foreach (TradeItem current in base.FirstTrader.Items)
                {
                    BasePlayerItem item = base.FirstTrader.Character.Inventory.TryGetItem(current.Guid);
                    base.FirstTrader.Character.Inventory.RemoveItem(item, amount: (int)current.Stack, delete: true);
                }
                foreach (TradeItem current in base.SecondTrader.Items)
                {
                    base.FirstTrader.Character.Inventory.AddItem(current.Template, amount: (int)current.Stack);
                }
                InventoryHandler.SendInventoryWeightMessage(base.FirstTrader.Character.Client);
                BsonDocument document = new BsonDocument
                {

                    {
                        "NpcId",
                        base.SecondTrader.Npc.TemplateId
                    },

                    {
                        "PlayerId",
                        base.FirstTrader.Id
                    },

                    {
                        "NpcKamas",
                        (long)((ulong)base.SecondTrader.Kamas)
                    },

                    {
                        "PlayerItems",
                        base.FirstTrader.ItemsString
                    },

                    {
                        "NpcItems",
                        base.SecondTrader.ItemsString
                    },

                    {
                        "Date",
                        System.DateTime.Now.ToString(System.Globalization.CultureInfo.InvariantCulture)
                    }
                };
                Singleton<MongoLogger>.Instance.Insert("NpcTrade", document);
            }
        }
        protected override void OnTraderReadyStatusChanged(Trader trader, bool status)
        {
            base.OnTraderReadyStatusChanged(trader, status);
            InventoryHandler.SendExchangeIsReadyMessage(base.FirstTrader.Character.Client, trader, status);
            if (trader is PlayerTrader && status)
            {
                base.SecondTrader.ToggleReady(true);
            }
        }
        protected override void OnTraderItemMoved(Trader trader, TradeItem item, bool modified, int difference)
        {
            base.OnTraderItemMoved(trader, item, modified, difference);
            if (item.Stack == 0u)
            {
                if (trader is PlayerTrader)
                {

                    SecondTrader.SetKamas(0);

                }
                if (trader is PlayerTrader && ItemIdToReceive != 0 && ((RateItem != 0 && item.Template.Id == ItemIdToGive) || Itens != null))
                {
                    foreach (var @object in SecondTrader.Items)
                    {
                        SecondTrader.RemoveItem(@object.Template, @object.Stack);
                    }
                }
                else if (trader is PlayerTrader && ItemIdToReceive == 0 && RateItem == 0 && Itens != null)//mont system trade
                {
                    foreach (var @object in SecondTrader.Items.ToList())
                    {
                        SecondTrader.RemoveItem(@object.Template, @object.Stack);
                    }
                }

                InventoryHandler.SendExchangeObjectRemovedMessage(base.FirstTrader.Character.Client, trader != base.FirstTrader, item.Guid);
            }
            else
            {
                if (modified)
                {
                    InventoryHandler.SendExchangeObjectModifiedMessage(base.FirstTrader.Character.Client, trader != base.FirstTrader, item);
                }
                else
                {
                    InventoryHandler.SendExchangeObjectAddedMessage(base.FirstTrader.Character.Client, trader != base.FirstTrader, item);
                }

                if (trader is PlayerTrader && ItemIdToReceive != 0 && ((RateItem != 0 && item.Template.Id == ItemIdToGive) || Itens != null))
                {
                    foreach (var @object in SecondTrader.Items)
                    {
                        SecondTrader.RemoveItem(@object.Template, @object.Stack);
                    }
                    if (Itens != null)
                        for (int i = 0; i < Itens.Length; i++)
                        {
                            if (Itens[i][0] == item.Template.Id)
                                SecondTrader.AddItem(ItemManager.Instance.TryGetTemplate(ItemIdToReceive), (uint)(item.Stack / Itens[i][1])); ;
                        }
                    else
                        SecondTrader.AddItem(ItemManager.Instance.TryGetTemplate(ItemIdToReceive), (uint)(item.Stack / RateItem));
                }
                else if (trader is PlayerTrader && ItemIdToReceive == 0 && RateItem == 0 && Itens != null)//mont system trade
                {
                    foreach (var @object in SecondTrader.Items.ToList())
                    {
                        SecondTrader.RemoveItem(@object.Template, @object.Stack);
                    }
                    List<ItemTemplate> mount_sort = new List<ItemTemplate>();
                    for (int i = 0; i < Itens.Length; i++)
                    {
                        if (trader.Items.Count() <= 2)
                        {
                            //if (Itens[i][0] == item.Template.Id)
                            if (trader.Items.Any(x => x.Template.Id == Itens[i][0]))
                                //if (trader.Items.Any(x => x.Template.Id == Itens[i][1]))//& 1 stack?!
                                if (Itens[i][1] != Itens[i][0])
                                {
                                    if (Itens[i][1] == item.Template.Id)
                                        mount_sort.Add(ItemManager.Instance.TryGetTemplate((int)Itens[i][2]));

                                }
                                else
                                {
                                    if (trader.Items.Where(x => x.Template.Id == Itens[i][0]).Count() == 2)
                                        mount_sort.Add(ItemManager.Instance.TryGetTemplate((int)Itens[i][2]));
                                }
                            if (trader.Items.Any(x => x.Template.Id == Itens[i][1]))
                                if (Itens[i][0] != Itens[i][1])
                                {
                                    if (Itens[i][0] == item.Template.Id)
                                        mount_sort.Add(ItemManager.Instance.TryGetTemplate((int)Itens[i][2]));

                                }
                        }
                    }
                    if (mount_sort.Any())
                    {
                        SecondTrader.AddItem(mount_sort.Shuffle().FirstOrDefault(), 1);
                    }


                }

                if (trader is PlayerTrader)
                {
                    var items = FirstTrader.Items.FirstOrDefault(x => x.Template.Id == ItemIdToGive);
                    if (items != null)
                    {
                        SecondTrader.SetKamas((int)(Kamas * items.Stack));
                    }
                    else
                    {
                        SecondTrader.SetKamas(0);
                    }
                }
            }

        }
        protected override void OnTraderKamasChanged(Trader trader, ulong amount)
        {
            base.OnTraderKamasChanged(trader, amount);
            InventoryHandler.SendExchangeKamaModifiedMessage(base.FirstTrader.Character.Client, trader != base.FirstTrader, (ulong)amount);
        }
    }
}
