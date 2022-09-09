using System.Linq;
using Stump.Core.Mathematics;
using Stump.DofusProtocol.Enums;
using Stump.Server.WorldServer.Database.Items;
using Stump.Server.WorldServer.Game.Actors.Fight;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using Stump.Server.WorldServer.Game.Effects.Instances;
using Stump.Server.WorldServer.Game.Fights;
using Stump.Server.WorldServer.Game.Mounts;

namespace Stump.Server.WorldServer.Game.Items.Player.Custom
{
    [ItemId((ItemIdEnum)19933)]
    public sealed class CaptureVulk : BasePlayerItem
    {
        //private EffectInteger m_hunterweaponEffect;

        public CaptureVulk(Character owner, PlayerItemRecord record)
            : base(owner, record)
        {
            // m_hunterweaponEffect = Effects.OfType<EffectInteger>().FirstOrDefault(x => x.EffectId == EffectsEnum.Effect_795);

            //if (m_hunterweaponEffect == null)
            //{
            //return;
            //}

            if (IsEquiped())
                SubscribeEvents();

        }

        private void SubscribeEvents()
        {
            Owner.ContextChanged += OnContextChanged;
        }

        private void UnsubscribeEvents()
        {
            Owner.ContextChanged -= OnContextChanged;
        }

        private void OnContextChanged(Character character, bool infight)
        {
            if (infight)
                character.Fight.GeneratingResults += OnGeneratingResults;
        }

        private void OnGeneratingResults(IFight obj)
        {
            FightPvM fightPvM = null;
            if (Owner?.Fighter?.Fight != null && Owner.Fighter.Fight is FightPvM)
                fightPvM = Owner.Fighter.Fight as FightPvM;
            if (fightPvM != null && fightPvM == obj && Owner.Fighter.HasWin())
            {
                foreach (var monster in fightPvM.DefendersTeam.Fighters.OfType<MonsterFighter>().Select(x => x.Monster).Where(x => CaptureVulkManager.Instance.MountExist(x.Template.Id)))
                {
                    var itemid = CaptureVulkManager.Instance.ItemId(monster.Template.Id);
                    if (Owner.Fighter.Team.Fighters.Any(z => z.Loot.Items.Where(y => y.Key == itemid).Count() >= fightPvM.DefendersTeam.Fighters.OfType<MonsterFighter>().Select(x => x.Monster).Where(x => CaptureVulkManager.Instance.MountExist(x.Template.Id)).Count()))
                        return;
                    float drop_monster = CaptureVulkManager.Instance.TauxDrop(monster.Template.Id);

                    var rand = new CryptoRandom();
                    double number_rand = rand.NextDouble() * 100;
                    if (number_rand <= drop_monster)
                    {
                        if (Owner.Inventory.RemoveItem(this, 1) <= 0)
                            return;

                        var vulk_item = ItemManager.Instance.CreatePlayerItem(Owner, itemid, 4);
                        Owner.Inventory.AddItem(vulk_item);
                        // display purpose
                        Owner.Fighter.Loot.AddItem(new DroppedItem(vulk_item.Template.Id, 4) { IgnoreGeneration = true });
                    }

                }
            }
        }

        public override bool OnEquipItem(bool unequip)
        {
            if (!unequip)
                SubscribeEvents();
            else
                UnsubscribeEvents();

            return base.OnEquipItem(unequip);
        }

    }
}