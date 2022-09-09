/*using Stump.DofusProtocol.Enums;
using Stump.DofusProtocol.Types;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Monsters;
using Stump.Server.WorldServer.Game.Actors.RolePlay.TaxCollectors;
using Stump.Server.WorldServer.Game.Fights.Teams;
using Stump.Server.WorldServer.Game.Items;
using Stump.Server.WorldServer.Game.Items.Player;
using Stump.Server.WorldServer.Game.Prisms;

namespace Stump.Server.WorldServer.Game.Fights.Results
{
    public class PrismProspectingResult : IFightResult, IExperienceResult
    {
        public PrismProspectingResult(PrismNpc prismNpc, IFight fight)
        {
            Prism = prismNpc;
            Fight = fight;
            Loot = new FightLoot();
        }

        public PrismNpc Prism
        {
            get;
        }

        public IFight Fight
        {
            get;
        }

        public bool Alive => true;

        public bool HasLeft => false;

        public int Id => Prism.GlobalId;

        public int Prospecting => 100;

        public int Wisdom => 0;

        public int Level => 1;

        public bool CanLoot(FightTeam team) => team is FightPlayerTeam;

        public FightLoot Loot
        {
            get;
        }

        public int Experience
        {
            get;
            set;
        }

        public FightOutcomeEnum Outcome => FightOutcomeEnum.RESULT_TAX;

        public FightResultListEntry GetFightResultListEntry() => new FightResultPrismListEntry((ushort)Outcome, 0, Loot.GetFightLoot(), Id, Alive,
                                    (byte)Prism.Alliance.GetGuildById((uint) Prism.Alliance.Record.Owner).Level, Prism.Alliance.GetGuildById((uint) Prism.Alliance.Record.Owner).GetBasicGuildInformations(), Experience);

        public void Apply()
        {
            foreach (var drop in Loot.Items.Values)
            {
                var template = ItemManager.Instance.TryGetTemplate(drop.ItemId);

                if (template.Effects.Count > 0)
                    for (var i = 0; i < drop.Amount; i++)
                    {
                        var item = ItemManager.Instance.CreatePrismItem(drop.ItemId, (int)Prism.Alliance.Record.Owner);
                        Prism.Bag.AddItem(item);
                       
                    }
                else
                {
                    var item = ItemManager.Instance.CreatePrismItem(Prism, drop.ItemId, (int)drop.Amount);
                    Prism.Bag.AddItem(item);
                }
            }
        }
       
    }
}*/