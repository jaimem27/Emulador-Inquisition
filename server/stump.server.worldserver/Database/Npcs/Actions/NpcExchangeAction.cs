using System;
using Stump.DofusProtocol.Enums;
using Stump.Server.BaseServer.Database;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Npcs;
using Stump.Server.WorldServer.Game.Exchanges;

namespace Stump.Server.WorldServer.Database.Npcs.Actions
{
    [Discriminator(Discriminator, typeof(NpcActionDatabase), new System.Type[]
    {
        typeof(NpcExchangeAction)
    })]
    public class NpcExchangeAction : NpcActionDatabase
    {
        public const string Discriminator = "Trade";
        public override NpcActionTypeEnum[] ActionType => new NpcActionTypeEnum[] { NpcActionTypeEnum.ACTION_EXCHANGE };
        public int Kamas
        {
            get
            {
                return base.Record.GetParameter<int>(0u, false);
            }
            set
            {
                base.Record.SetParameter<int>(0u, value);
            }
        }
        public int ItemIdToReceive
        {
            get
            {
                return base.Record.GetParameter<int>(1u, false);
            }
            set
            {
                base.Record.SetParameter<int>(1u, value);
            }
        }
        public int ItemIdToGive
        {
            get
            {
                return base.Record.GetParameter<int>(2u, false);
            }
            set
            {
                base.Record.SetParameter<int>(2u, value);
            }
        }
        public double RateItem
        {
            get
            {
                return base.Record.GetParameter<double>(3u, false);
            }
            set
            {
                base.Record.SetParameter<double>(3u, value);
            }
        }

        public string Itens
        {
            get
            {
                return base.Record.GetParameter<string>(4u, true);
            }
            set
            {
                base.Record.SetParameter<string>(4u, value);
            }
        }

        public NpcExchangeAction(NpcActionRecord record)
            : base(record)
        {
        }

        public override void Execute(Npc npc, Character character)
        {
            NpcTrade npcDialog = new NpcTrade(character, npc, Kamas, ItemIdToGive, ItemIdToReceive, RateItem, Itens);
            npcDialog.Open();
        }
    }
}
