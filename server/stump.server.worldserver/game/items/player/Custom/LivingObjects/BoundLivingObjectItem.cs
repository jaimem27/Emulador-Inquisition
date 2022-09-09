using System;
using System.Collections.Generic;
using System.Linq;
using NLog;
using Stump.DofusProtocol.Enums;
using Stump.Server.WorldServer.Database.Items;
using Stump.Server.WorldServer.Database.Items.Templates;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using Stump.Server.WorldServer.Game.Effects.Instances;

namespace Stump.Server.WorldServer.Game.Items.Player.Custom.LivingObjects
{
    [ItemHasEffect(EffectsEnum.Effect_LivingObjectId)]
    public sealed class BoundLivingObjectItem : CommonLivingObject
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        private readonly ItemTemplate m_livingObjectTemplate;

        public BoundLivingObjectItem(Character owner, PlayerItemRecord record) : base(owner, record)
        {
            var idEffect = (EffectInteger)Effects.First(x => x.EffectId == EffectsEnum.Effect_LivingObjectId);
            m_livingObjectTemplate = ItemManager.Instance.TryGetTemplate(idEffect.Value);
            LivingObjectRecord = ItemManager.Instance.TryGetLivingObjectRecord(idEffect.Value);

            if (LivingObjectRecord == null || m_livingObjectTemplate == null)
                logger.Error("Living Object {0} has no template", Template.Id);

            Initialize();
        }

        public override bool CanFeed(BasePlayerItem item) => false;

        public override bool IsLinkedToAccount() => true;

        public override bool Feed(BasePlayerItem food)
        {
            if (food.Template.TypeId != LivingObjectRecord.ItemType)
                return false;

            var xp = (short)Math.Ceiling(food.Template.Level / 2d);
            Experience += xp;

            // Objetos Vivientes Maximo LvL 10
            if (
                    m_livingObjectTemplate.Id == 19524 ||   // Capa colorviva
                    m_livingObjectTemplate.Id == 19525 ||   // Escudo colorvivo
                    m_livingObjectTemplate.Id == 19526 ||   // Sombrero colorvivo
                    m_livingObjectTemplate.Id == 21794 ||   // Sombrero monstruvivo
                    m_livingObjectTemplate.Id == 23022 ||   // Tocado colorvivo
                    m_livingObjectTemplate.Id == 23023 ||   // Esclavina colorviva
                    m_livingObjectTemplate.Id == 23024 ||   // Adarga colorviva
                    m_livingObjectTemplate.Id == 26260 ||   // Máscara colorviva
                    m_livingObjectTemplate.Id == 26261 ||   // Manto colorvivo
                    m_livingObjectTemplate.Id == 26262      // Tarja colorviva
                )
            {
                if (Level > 10)
                    Experience = 126;
            }

            // Objetos Vivientes Maximo LvL 8
            if (
                    m_livingObjectTemplate.Id == 26062      // Casco cascanuevivo
                )
            {
                if (Level > 8)
                    Experience = 91;
            }

            // Objetos Vivientes Maximo LvL 5
            if (
                    m_livingObjectTemplate.Id == 26063 ||   // Capa cascanueviva
                    m_livingObjectTemplate.Id == 26064      // Escudo cascanuevivo
                )
            {
                if (Level > 5)
                    Experience = 46;
            }

            // Objetos Vivientes Maximo LvL 3
            if (
                    m_livingObjectTemplate.Id == 23659 ||   // Escudo samurivo
                    m_livingObjectTemplate.Id == 23660 ||   // Capa samuriva
                    m_livingObjectTemplate.Id == 23708 ||   // Casco samurivo
                    m_livingObjectTemplate.Id == 24992 ||   // Capa gloriosa
                    m_livingObjectTemplate.Id == 24993      // Capa gloriosa polikroma
                )
            {
                if (Level > 3)
                    Experience = 21;
            }

            Mood = 1;
            LastMeal = DateTime.Now;

            Owner.Inventory.RefreshItem(this);

            return true;
        }

        public void DissociateInCraf()
        {
            var effects = new List<EffectBase> { MoodEffect, ExperienceEffect, CategoryEffect, SelectedLevelEffect };
            var effectsLiving = new List<EffectBase> { MoodEffect, ExperienceEffect, CategoryEffect, SelectedLevelEffect };

            if (LastMealEffect != null)
            {
                effects.Add(LastMealEffect);
                effectsLiving.Add(LastMealEffect);
            }

            Effects.RemoveAll(effects.Contains);
            Effects.RemoveAll(x => x.EffectId == EffectsEnum.Effect_LivingObjectId);

            var newInstance = Owner.Inventory.RefreshItemInstance(this);

            newInstance.Invalidate();
            newInstance.OnObjectModified();

            Owner.UpdateLook();

            var livingObject = ItemManager.Instance.CreatePlayerItem(Owner, m_livingObjectTemplate, 1, effectsLiving);
            Owner.Inventory.AddItem(livingObject);
        }
        public void Dissociate()
        {
            if (Owner.IsInExchange())
                return;

            var effects = new List<EffectBase> { MoodEffect, ExperienceEffect, CategoryEffect, SelectedLevelEffect };
            var effectsLiving = new List<EffectBase> { MoodEffect, ExperienceEffect, CategoryEffect, SelectedLevelEffect };

            if (LastMealEffect != null)
            {
                effects.Add(LastMealEffect);
                effectsLiving.Add(LastMealEffect);
            }

            Effects.RemoveAll(effects.Contains);
            Effects.RemoveAll(x => x.EffectId == EffectsEnum.Effect_LivingObjectId);

            var newInstance = Owner.Inventory.RefreshItemInstance(this);

            newInstance.Invalidate();
            newInstance.OnObjectModified();

            Owner.UpdateLook();

            var livingObject = ItemManager.Instance.CreatePlayerItem(Owner, m_livingObjectTemplate, 1, effectsLiving);
            Owner.Inventory.AddItem(livingObject);
        }
    }
}