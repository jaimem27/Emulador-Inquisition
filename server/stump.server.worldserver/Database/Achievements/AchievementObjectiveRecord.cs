﻿using Stump.Core.Reflection;
using Stump.DofusProtocol.D2oClasses.Tools.D2o;
using Stump.DofusProtocol.Types;
using Stump.ORM;
using Stump.ORM.SubSonic.SQLGeneration.Schema;
using Stump.Server.WorldServer.Game.Achievements;
using Stump.Server.WorldServer.Game.Achievements.Criterions;

namespace Stump.Server.WorldServer.Database.Achievements
{
    [D2OClass("AchievementObjective", "com.ankamagames.dofus.datacenter.quest"),
     TableName("achievements_objectives")]
    public class AchievementObjectiveRecord : IAutoGeneratedRecord, ISaveIntercepter, IAssignedByD2O
    {
        // FIELDS
        private AchievementTemplate m_achievement;

        // PROPERTIES
        [PrimaryKey("Id", false)]
        public uint Id { get; set; }

        public uint AchievementId { get; set; }

        [Ignore]
        public AchievementTemplate Achievement => m_achievement ??
                                                  (m_achievement =
                                                      Singleton<AchievementManager>.Instance.TryGetAchievement(
                                                          AchievementId));

        public uint Order { get; set; }

        public uint NameId { get; set; }

        public string Criterion { get; set; }

        [Ignore]
        public AbstractCriterion AbstractCriterion { get; private set; }

        public virtual void AssignFields(object d2oObject)
        {
            var item = (DofusProtocol.D2oClasses.AchievementObjective)d2oObject;
            Id = item.id;
            AchievementId = item.achievementId;
            Order = item.order;
            NameId = item.nameId;
            Criterion = item.criterion;
        }

        public void BeforeSave(bool insert)
        {
        }

        // CONSTRUCTORS

        // METHODS
        public void Initialize()
        {
            AbstractCriterion = AbstractCriterion.CreateCriterion(this);
            AbstractCriterion?.AddUsefullness(Achievement);
        }

        public AchievementObjective GetAchievementObjective(PlayerAchievement player)
        {
            return AbstractCriterion == null
                ? new AchievementObjective((ushort)Id, 0)
                : AbstractCriterion.GetAchievementObjective(this, player);
        }

        public AchievementStartedObjective GetAchievementStartedObjective(PlayerAchievement player)
        {
            if (AbstractCriterion == null)
            {
                return new AchievementStartedObjective(Id, 1, 0);
            }
            return new AchievementStartedObjective(Id, AbstractCriterion.MaxValue,
                AbstractCriterion.GetPlayerValue(player));
        }
    }
}