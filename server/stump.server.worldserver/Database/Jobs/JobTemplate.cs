﻿using System;
using System.Collections.Generic;
using Stump.Core.IO;
using Stump.DofusProtocol.D2oClasses.Tools.D2o;
using Stump.ORM;
using Stump.ORM.SubSonic.SQLGeneration.Schema;
using Stump.Server.WorldServer.Database.I18n;
using Stump.Server.WorldServer.Database.Interactives;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using Stump.Server.WorldServer.Game.Jobs;
using Job = Stump.DofusProtocol.D2oClasses.Job;

namespace Stump.Server.WorldServer.Database.Jobs
{
    public class JobTemplateRelator
    {
        public static string FetchQuery = "SELECT * FROM jobs_templates";
    }

    [TableName("jobs_templates")]
    [D2OClass("Job", "com.ankamagames.dofus.datacenter.jobs")]
    public class JobTemplate : ISaveIntercepter, IAssignedByD2O, IAutoGeneratedRecord
    {
        private string m_name;
        private InteractiveSkillTemplate[] m_skills;
        private List<Character> m_availableCrafters = new List<Character>();
        public event Action<JobTemplate, Character> CrafterSubscribed;
        public event Action<JobTemplate, Character> CrafterUnSubscribed;
        public event Action<JobTemplate, Character> CrafterRefreshed;

        [PrimaryKey("Id", false)]
        public int Id
        {
            get;
            set;
        }

        public uint NameId
        {
            get;
            set;
        }

        public string Name
        {
            get { return m_name ?? (m_name = TextManager.Instance.GetText(NameId)); }
        }

        public int IconId
        {
            get;
            set;
        }

        [Ignore]
        public InteractiveSkillTemplate[] Skills => m_skills ?? (m_skills = JobManager.Instance.GetJobSkills(Id));

        public int HarvestedCountMax
        {
            get;
            set;
        }

        public IReadOnlyCollection<Character> AvailableCrafters => m_availableCrafters.AsReadOnly();

        public bool AddOrRemoveAvailableCrafter(Character character)
        {
            if (m_availableCrafters.Contains(character))
            {
                RemoveAvaiableCrafter(character);
                return false;
            }
            else
            {
                AddAvailableCrafter(character);
                return true;
            }
        }

        public void AddAvailableCrafter(Character character)
        {
            m_availableCrafters.Add(character);
            OnCrafterSubscribed(character);
        }

        public void RemoveAvaiableCrafter(Character character)
        {
            m_availableCrafters.Remove(character);
            OnCrafterUnSubscribed(character);
        }

        public void RefreshCrafter(Character character)
        {
            if (m_availableCrafters.Contains(character))
                OnCrafterRefreshed(character);
        }
        

        #region IAssignedByD2O Members

        public void AssignFields(object d2oObject)
        {
            var job = (Job) d2oObject;
            Id = job.id;
            NameId = job.nameId;
            IconId = job.iconId;
        }

        #endregion

        #region ISaveIntercepter Members

        public void BeforeSave(bool insert)
        {
        }

        #endregion

        protected virtual void OnCrafterSubscribed(Character character)
        {
            var evnt = CrafterSubscribed;
            if (evnt != null)
                evnt(this, character);
        }

        protected virtual void OnCrafterUnSubscribed(Character character)
        {
            var evnt = CrafterUnSubscribed;
            if (evnt != null)
                evnt(this, character);
        }

        protected virtual void OnCrafterRefreshed(Character character)
        {
            var evnt = CrafterRefreshed;
            if (evnt != null)
                evnt(this, character);
        }
    }
}