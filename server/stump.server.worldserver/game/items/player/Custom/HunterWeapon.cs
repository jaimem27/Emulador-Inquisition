using System.Linq;
using Stump.Core.Mathematics;
using Stump.DofusProtocol.Enums;
using Stump.Server.WorldServer.Database.Items;
using Stump.Server.WorldServer.Game.Actors.Fight;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using Stump.Server.WorldServer.Game.Effects.Instances;
using Stump.Server.WorldServer.Game.Fights;
using Stump.Server.WorldServer.Game.Jobs;

namespace Stump.Server.WorldServer.Game.Items.Player.Custom
{
    [ItemHasEffect(EffectsEnum.Effect_795)]
    public sealed class HunterWeapon : BasePlayerItem
    {
        private EffectInteger m_hunterweaponEffect;

        public HunterWeapon(Character owner, PlayerItemRecord record)
            : base(owner, record)
        {
            m_hunterweaponEffect = Effects.OfType<EffectInteger>().FirstOrDefault(x => x.EffectId == EffectsEnum.Effect_795);

            if (m_hunterweaponEffect == null)
            {
                return;
            }

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
                //if (Owner.Fighter.Team.Fighters.Any(x => x.Loot.Items.Any(y => y.Key == (int)ItemIdEnum.PIERRE_DAME_PLEINE_7010)))
                //    return;

                double mob_stars = (double)Owner.Fighter.Fight.AgeBonus <= 0 ? 1 : Owner.Fighter.Fight.AgeBonus;
                double hunter_level = (double)Owner.Jobs[41].Level;
                //1% até 60% pela mob stars
                //1% até 20% pelo job level 
                //double coef_job_hunter = 0.00167;
                //double coef_mob_stars = 0.1;

                //double tax_drop = ((coef_job_hunter * hunter_level)+1)*(coef_mob_stars * mob_stars);
                //double tax_drop = ((coef_job_hunter * hunter_level) +1) *( (coef_mob_stars * mob_stars)+1);
                foreach (var monster in fightPvM.DefendersTeam.Fighters.OfType<MonsterFighter>().Select(x => x.Monster).Where(x => HunterManager.Instance.DropExist(x.Template.Id)))
                {
                    int drop_monster = HunterManager.Instance.Level(monster.Template.Id);
                    //double drop_coef = hunter_level > drop_monster ? 0.2 : ((double)hunter_level / (double)drop_monster) * 0.2;
                    var rand = new CryptoRandom();
                    double number_rand = rand.NextDouble();
                    bool drop = false;
                    while (number_rand <= (0.15d * (mob_stars + 2)) + (0.15d * (hunter_level)) + ((drop_monster + ((hunter_level) - drop_monster)) * 0.2d))// (((tax_drop * drop_coef)+ tax_drop))-1)                    
                    {
                        drop = true;
                        number_rand = number_rand + (0.15d * mob_stars) + (0.15d * hunter_level) + ((drop_monster + (hunter_level - drop_monster)) * 0.2d);
                        var itemid = HunterManager.Instance.ItemId(monster.Template.Id);
                        var hunter_item = ItemManager.Instance.CreatePlayerItem(Owner, itemid, 1);
                        Owner.Inventory.AddItem(hunter_item);
                        // display purpose
                        Owner.Fighter.Loot.AddItem(new DroppedItem(hunter_item.Template.Id, 1) { IgnoreGeneration = true });
                        //var xp = (int)(((((tax_drop * drop_coef) + tax_drop)-1d)/100d)* drop_monster);
                        var coefexp = JobManager.Instance.GetHarvestJobXp(drop_monster);
                        var xp = ((hunter_level + (drop_monster - hunter_level) * 0.002d) * ((0.05d * drop_monster) * coefexp) + (0.05d * drop_monster) * coefexp) * (mob_stars / 1000d) + ((hunter_level + (drop_monster - hunter_level) * 0.002d) * ((0.05d * drop_monster) * coefexp) + (0.05d * drop_monster) * coefexp);
                        Owner.Jobs[41].Experience += (long)xp;
                    }
                    if (drop == false)
                    {
                        var coefexp = JobManager.Instance.GetHarvestJobXp(drop_monster);
                        var xp = ((hunter_level + (drop_monster - hunter_level) * 0.002d) * ((0.05d * drop_monster) * coefexp) + (0.05d * drop_monster) * coefexp) * (mob_stars / 1000d) + ((hunter_level + (drop_monster - hunter_level) * 0.002d) * ((0.05d * drop_monster) * coefexp) + (0.05d * drop_monster) * coefexp);
                        Owner.Jobs[41].Experience += (long)xp;
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