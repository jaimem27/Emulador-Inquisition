using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using NLog;
using Stump.Core.Extensions;
using Stump.Core.Reflection;
using Stump.DofusProtocol.Enums;
using Stump.DofusProtocol.Messages;
using Stump.DofusProtocol.Types;
using Stump.Server.WorldServer.Core.Network;
using Stump.Server.WorldServer.Database.Alliances;
using Stump.Server.WorldServer.Game.Guilds;
using Stump.Server.WorldServer.Game.Prisms;
using Stump.Server.WorldServer.Handlers.Alliances;
using Stump.Server.WorldServer.Handlers.Basic;

namespace Stump.Server.WorldServer.Game.Alliances {
    public class Alliance {
        // FIELDS
        private static readonly Logger logger = LogManager.GetCurrentClassLogger ();
        public Dictionary<int, Guild> m_guilds = new Dictionary<int, Guild> ();
        private readonly object m_lock = new object ();

        private bool m_isDirty;

        // CONSTRUCTORS
        public Alliance (int id, string name, string tag) {
            Record = new AllianceRecord ();
            Id = id;
            Name = name;
            Tag = tag;
            Record.CreationDate = DateTime.Now;
            Emblem = new AllianceEmblem (Record) {
                BackgroundColor = Color.White,
                BackgroundShape = 1,
                SymbolColor = Color.Black,
                SymbolShape = 1
            };
            BulletinContent = "";
            BulletinDate = DateTime.Now;
            LastNotifiedDate = DateTime.Now;
            Prisms = new List<PrismNpc> ();
            Record.IsNew = true;
            IsDirty = true;
        }

        public Alliance (AllianceRecord record) {
            Record = record;
            Emblem = new AllianceEmblem (Record);
            BulletinDate = DateTime.Now;
            LastNotifiedDate = DateTime.Now;
            Prisms = new List<PrismNpc> ();
            foreach (var item in Singleton<GuildManager>.Instance.GetGuildsByAlliance (Record)) {
                if (item.Id == Record.Owner) {
                    item.SetAlliance (this);

                    m_guilds.Add (item.Id, item);

                    foreach (var guild in item.Clients)
                        if (!Clients.Contains (guild))
                            Clients.Add (guild);

                    if (Record.Owner == item.Id)
                        SetBoss (item);
                }
            }

            foreach (var item in Singleton<GuildManager>.Instance.GetGuildsByAlliance (Record)) {
                if (item.Id != Record.Owner) {
                    item.SetAlliance (this);

                    m_guilds.Add (item.Id, item);

                    foreach (var guild in  item.Clients)
                        if (!Clients.Contains (guild))
                            Clients.Add (guild);

                    if (Record.Owner == item.Id)
                        SetBoss (item);
                }
            }
            if (Boss == null) {
                if (m_guilds.Count == 0) { } else
                    SetBoss (m_guilds.First ().Value);
            }
        }

        // PROPERTIES
        public int Id {
            get { return Record.Id; }
            private set { Record.Id = value; }
        }

        public string Name {
            get { return Record.Name; }
            private set {
                Record.Name = value;
                IsDirty = true;
            }
        }

        public string Tag {
            get { return Record.Tag; }
            private set {
                Record.Tag = value;
                IsDirty = true;
            }
        }

        public AllianceRecord Record { get; }

        public Guild Boss { get; private set; }

        public AllianceEmblem Emblem { get; protected set; }

        public List<PrismNpc> Prisms { get; set; }
        WorldClientCollection m_clients = new WorldClientCollection ();

        private List<Actors.RolePlay.Characters.Character> m_Character = new List<Actors.RolePlay.Characters.Character> ();
        public WorldClientCollection Clients {

            get {

                foreach (var a in m_guilds) {
                    foreach (var b in a.Value.Clients)
                        if (!m_Character.Contains (b.Character)) {
                            m_Character.Add (b.Character);
                            m_clients.Add (b.Character.Client);
                        }

                }
                return m_clients;

            }

        }

        public bool IsDirty {
            get { return m_isDirty || Emblem.IsDirty; }
            set {
                m_isDirty = value;
                if (!value) {
                    Emblem.IsDirty = false;
                }
            }
        }
        public void UpdateMotd (Guilds.GuildMember member, string content) {
            try {
                MotdContent = content;
                MotdMember = member;
                MotdDate = DateTime.Now;

                AllianceHandler.SendAllianceMotdMessage (Clients, this);
            } catch { }

        }
        public void UpdateBulletin (Guilds.GuildMember member, string content, bool notify) {
            try {
                BulletinContent = content;
                BulletinMember = member;
                BulletinDate = DateTime.Now;

                if (notify)
                    LastNotifiedDate = DateTime.Now;

                AllianceHandler.SendAllianceBulletinMessage (Clients, this);
            } catch { }

        }
        public string MotdContent {
            get { return Record.MotdContent; }
            protected set {
                Record.MotdContent = value;
            }
        }

        public DateTime MotdDate {
            get { return Record.MotdDate; }
            set {
                Record.MotdDate = value;
            }
        }

        public Guilds.GuildMember MotdMember {
            get { return MembersTryGetId (Record.MotdMemberId); }
            protected set {
                Record.MotdMemberId = value.Id;
            }
        }

        public string BulletinContent {
            get { return Record.BulletinContent; }
            protected set {
                Record.BulletinContent = value;
            }
        }

        public DateTime BulletinDate {
            get { return Record.BulletinDate; }
            protected set {
                Record.BulletinDate = value;
            }
        }

        public DateTime LastNotifiedDate {
            get { return Record.LastNotifiedDate; }
            protected set {
                Record.LastNotifiedDate = value;
            }
        }

        public Guilds.GuildMember BulletinMember {
            get { return MembersTryGetId (Record.BulletinMemberId); }
            protected set {
                Record.BulletinMemberId = value.Id;
            }
        }
        public ushort Members {
            get { return (ushort) m_guilds.Sum (entry => entry.Value.Members.Count); }
        }
        public Guilds.GuildMember MembersTryGetId (int ID) {

            //var a = m_guilds.FirstOrDefault(entry => entry.Value.Members.FirstOrDefault(x => x.Id == ID).Id == ID);
            //  var b= a.Value.Members.FirstOrDefault(x => x.Id == ID);

            foreach (var c in m_guilds)
                foreach (var d in c.Value.Members)
                    if (d.Id == ID)
                        return d;
            return null;
        }

        // METHODS
        public void AddPrism (PrismNpc prism) {
            Prisms.Add (prism);
        }

        /// <summary>
        /// Add new guild to the alliance
        /// </summary>
        /// <param name="guild"></param>
        /// <returns></returns>
        public bool TryAddGuild (Guild guild) {
            bool result;
            lock (m_lock) {
                if (guild.Alliance != null) // TODO : more conditions
                    result = false;
                else {
                    m_guilds.Add (guild.Id, guild);
                    guild.SetAlliance (this);
                    foreach (var client in guild.Clients) {
                        if (!Clients.Contains (client))
                            Clients.Add (client);
                    }
                    if (m_guilds.Count == 1)
                        SetBoss (guild);
                    OnGuildAdded (guild);

                    result = true;
                }
            }
            return result;
        }

        public Guild GetGuildById (uint id) {
            return m_guilds.TryGetValue ((int) id, out var guild) ? guild : null;
        }

        public AllianceInformations GetAllianceInformations () {
            return new AllianceInformations ((uint) Id, Tag, Name, Emblem.GetNetworkGuildEmblem ());
        }

        public BasicNamedAllianceInformations GetBasicNamedAllianceInformations () {
            return new BasicNamedAllianceInformations ((uint) Id, Tag, Name);
        }

        public AllianceFactSheetInformations GetAllianceFactSheetInformations () {
            return new AllianceFactSheetInformations ((uint) Id, Tag, Name, Emblem.GetNetworkGuildEmblem (),
                Record.CreationDate.GetUnixTimeStamp ());
        }

        public AllianceVersatileInformations GetAllianceVersatileInformations () {
            return new AllianceVersatileInformations ((uint) Id, (ushort) m_guilds.Count, (ushort) Members, 0);
        }

        public IEnumerable<GuildInsiderFactSheetInformations> GetGuildsInformations () {

            return from guild in m_guilds
            select new GuildInsiderFactSheetInformations (
                (uint) guild.Value.Id,
                guild.Value.Level > 200 ? "° " + guild.Value.Name + " °" : guild.Value.Name,
                guild.Value.Level > 200 ? (byte) (guild.Value.Level - 200) : (byte) guild.Value.Level,
                guild.Value.Emblem.GetNetworkGuildEmblem (),
                (uint) guild.Value.Boss.Id,
                (ushort) guild.Value.Members.Count,
                guild.Value.Boss.Name,
                (ushort) guild.Value.Members.Count (entry => entry.IsConnected),
                (sbyte) guild.Value.TaxCollectors.Count,
                guild.Value.CreationDate.GetUnixTimeStamp ());
        }

        public IEnumerable<PrismSubareaEmptyInfo> GetPrismsInformations () {
            if (Prisms != null) {
                var list =
                    Prisms.Select (
                        prismRecord =>
                        new PrismGeolocalizedInformation ((ushort) prismRecord.SubArea.Id,
                            (uint) prismRecord.Alliance.Id, //TODO Send by original
                            (short) prismRecord.Map.Position.X, (short) prismRecord.Map.Position.Y,
                            prismRecord.Map.Id,
                            prismRecord.GetAllianceInsiderPrismInformation ()))
                    .ToList ();

                return list;
            }

            return new PrismSubareaEmptyInfo[0];
        }

        public IEnumerable<GuildInAllianceInformations> GetGuildsInAllianceInformations () {
            return from guild in m_guilds
            select new GuildInAllianceInformations (
                (uint) guild.Value.Id,
                guild.Value.Level > 200 ? "° " + guild.Value.Name + " °" : guild.Value.Name,
                guild.Value.Level > 200 ? (byte) (guild.Value.Level - 200) : (byte) guild.Value.Level,
                guild.Value.Emblem.GetNetworkGuildEmblem (),
                (byte) guild.Value.Members.Count, (int) new DateTime().Ticks);
        }

        protected virtual void OnGuildAdded (Guild guild) {
            foreach (var member in guild.Members)
                if (member.IsConnected) {
                    AllianceHandler.SendAllianceJoinedMessage (member.Character.Client, this);
                    if (this.BulletinContent != null)
                        AllianceHandler.SendAllianceBulletinMessage (member.Character.Client, this);

                    if (this.MotdContent != null)
                        AllianceHandler.SendAllianceMotdMessage (member.Character.Client, this);
                    member.Character.RefreshActor ();
                    member.Character.AddEmote (EmotesEnum.EMOTE_ALLIANCE);

                }
        }
        public bool KickGuild (Guild guild) {
            if (guild == null || !m_guilds.Values.Contains (guild))
                return false;
            m_guilds.Remove (guild.Id);
            IsDirty = true;
            guild.IsDirty = true;
            if (m_guilds.Count == 0) {
                AllianceManager.Instance.DeleteAlliance (this);
            } else if (guild == guild.Alliance.Boss) {
                var newBoss = m_guilds.OrderByDescending (x => x.Value.Experience).FirstOrDefault ();
                if (newBoss.Value != null) {
                    SetBoss (newBoss.Value);

                    // <b>%1</b> a remplacé <b>%2</b>  au poste de meneur de la guilde <b>%3</b>
                    BasicHandler.SendTextInformationMessage (Clients,
                        TextInformationTypeEnum.TEXT_INFORMATION_ERROR, 199, //TODO change guild to alliance code
                        newBoss.Value.Name, guild.Name, Name);
                }
            }
            var allianceold = guild.Alliance;
            guild.Alliance = null;
            foreach (var member in guild.Members)
                if (member.IsConnected) {
                    member.Character.RemoveEmote (EmotesEnum.EMOTE_ALLIANCE);

                    //member.Character.GuildMember = null;

                    // Vous avez quitté la guilde.
                    member.Character.SendInformationMessage (TextInformationTypeEnum.TEXT_INFORMATION_MESSAGE, 410, allianceold.Name);
                    member.Character.Client.Send (new AllianceLeftMessage ());

                    member.Character.RefreshActor ();

                }

            return true;
        }
        public void SetBoss (Guild guild) {

            Dictionary<int, Guild> temp = new Dictionary<int, Guild> ();

            temp.Add (guild.Id, guild);

            foreach (var a in m_guilds) {
                if (a.Value.Id != guild.Id) {
                    temp.Add (a.Value.Id, a.Value);
                }
            }

            m_guilds = temp;
            Boss = guild;

            if (Record.Owner != Boss.Id) {
                Record.Owner = Boss.Id;
                IsDirty = true;
            }

        }

        public void Save (ORM.Database database) {
            WorldServer.Instance.IOTaskPool.AddMessage (() => {
                if (Record.IsNew)
                    database.Insert (Record);
                else
                    database.Update (Record);
                IsDirty = false;
                Record.IsNew = false;
            });
        }

        public void SendPrismsInfoValidMessage () {
            Clients.Send (
                new PrismsInfoValidMessage (
                    Prisms.Where (x => x.IsFighting).Select (x => x.Fighter.GetPrismFightersInformation ()).ToArray()));
        }

        public void SendInformationMessage (TextInformationTypeEnum msgType, short msgId, params object[] parameters) {
            BasicHandler.SendTextInformationMessage (Clients, msgType, msgId, parameters);
        }
    }
}