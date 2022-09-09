using System;
using System.Collections.Generic;
using System.Linq;
using Stump.Core.Extensions;
using Stump.Core.Reflection;
using Stump.DofusProtocol.Enums;
using Stump.DofusProtocol.Messages;
using Stump.DofusProtocol.Types;
using Stump.Server.BaseServer;
using Stump.Server.WorldServer.Database.World;
using Stump.Server.WorldServer.Game.Actors.Fight;
using Stump.Server.WorldServer.Game.Actors.Interfaces;
using Stump.Server.WorldServer.Game.Actors.Look;
using Stump.Server.WorldServer.Game.Actors.RolePlay;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using Stump.Server.WorldServer.Game.Alliances;
using Stump.Server.WorldServer.Game.Dialogs;
using Stump.Server.WorldServer.Game.Dialogs.Prisms;
using Stump.Server.WorldServer.Game.Exchanges;
using Stump.Server.WorldServer.Game.Exchanges.Trades.Npcs;
using Stump.Server.WorldServer.Game.Fights.Teams;
using Stump.Server.WorldServer.Game.Guilds;
using Stump.Server.WorldServer.Game.Items.Prism;
using Stump.Server.WorldServer.Game.Maps.Cells;
using Stump.Server.WorldServer.Game.Maps.Pathfinding;
using Stump.Server.WorldServer.Handlers.Context;

namespace Stump.Server.WorldServer.Game.Prisms {
    public class PrismNpc : RolePlayActor, IAutoMovedEntity, IContextDependant, IInteractNpc {
        private readonly int m_contextId;
        // FIELDS
        private readonly List<IDialog> m_openedDialogs = new List<IDialog> ();
        private ActorLook m_look;

        // CONSTRUCTORS
        public PrismNpc (int globalId, int contextId, ObjectPosition position, Alliance alliance, Character character) {
            m_contextId = contextId;
            Position = position;
            Alliance = alliance;
            var time = DateTime.Now;
            Record = new WorldMapPrismRecord {
                Id = globalId,
                Map = Position.Map,
                Cell = Position.Cell.Id,
                AllianceId = alliance.Id,
                Date = time,
                NextDate = time.AddDays (2),
                IsSabotaged = false,
                IsDefated = false,
                NuggetBeginDate = time,
                LastTimeSlotModificationDate = DateTime.Now,
                LastTimeSlotModificationAuthorGuildId = character.Guild.Id,
                LastTimeSlotModificationAuthorId = character.Id,
                LastTimeSlotModificationAuthorName = character.Name
            };
            Position.Map.SubArea.HasPrism = true;
            IsDirty = true;
            Bag = new PrismBag (this);
            Bag.LoadRecord ();
            Position.Map.SubArea.Prism = this;
        }

        public PrismBag Bag { get; set; }

        public PrismNpc (WorldMapPrismRecord record, int contextId) {
            Record = record;
            m_contextId = contextId;
            if (!record.MapId.HasValue) {
                throw new Exception ("Prism's map not found");
            }
            Position = new ObjectPosition (record.Map, record.Map.Cells[Record.Cell], DirectionsEnum.DIRECTION_EAST);
            Alliance = Singleton<AllianceManager>.Instance.TryGetAlliance (Record.AllianceId);
            Bag = new PrismBag (this);
            Bag.LoadRecord ();
            Position.Map.SubArea.Prism = this;
        }

        public sealed override ObjectPosition Position {
            get { return base.Position; }
            protected set { base.Position = value; }
        }

        public int GlobalId {
            get { return Record.Id; }
            protected set { Record.Id = value; }
        }

        public WorldMapPrismRecord Record { get; }

        public bool IsSabotaged {
            get { return Record.IsSabotaged; }
            set { Record.IsSabotaged = value; }
        }

        public bool IsDefated {
            get { return Record.IsDefated; }
            protected set { Record.IsDefated = value; }
        }

        public bool IsWeakened {
            get { return Record.IsWeakened; }
            set { Record.IsWeakened = value; }
        }

        public DateTime Date {
            get { return Record.Date; }
            protected set { Record.Date = value; }
        }

        public DateTime NextDate {
            get { return Record.NextDate; }
            set { Record.NextDate = value; }
        }

        public DateTime NuggetBeginDate {
            get { return Record.NuggetBeginDate; }
            set { Record.NuggetBeginDate = value; }
        }

        public PrismStateEnum State => GetStatePrism ();

        public Alliance Alliance { get; }

        public override ActorLook Look => m_look ?? RefreshLook ();

        public bool IsBusy () => m_openedDialogs.Any (x => x is NpcTrade);

        public bool IsDirty { get; private set; }
        public DateTime NextMoveDate { get; set; }
        public DateTime LastMoveDate { get; private set; }

        public override bool StartMove (Path movementPath) {
            bool result;
            if (!CanMove () || movementPath.IsEmpty ())
                result = false;
            else {
                Position = movementPath.EndPathPosition;
                var keys = movementPath.GetServerPathKeys ();
                Map.ForEach (
                    delegate (Character entry) { ContextHandler.SendGameMapMovementMessage (entry.Client, keys, this); });
                StopMove ();
                LastMoveDate = DateTime.Now;
                result = true;
            }
            return result;
        }

        // PROPERTIES
        public override int Id => m_contextId;

        public void InteractWith (NpcActionTypeEnum actionType, Character dialoguer) {
            if (CanInteractWith (actionType, dialoguer)) {
                var infoDialog = new PrismInfoDialog (dialoguer, this);
                infoDialog.Open ();
            }
        }

        public bool CanInteractWith (NpcActionTypeEnum action, Character dialoguer) {
            return CanBeSee (dialoguer) && action == NpcActionTypeEnum.ACTION_TALK;
        }

        // METHODS
        public ActorLook RefreshLook () {
            m_look = new ActorLook {
                BonesID = 2211
            };

            if (Alliance?.Emblem != null) {

                m_look.AddSkin ((short) (Alliance.Emblem.SymbolShape + 2569));
                m_look.AddColor (1, System.Drawing.Color.FromArgb (16777215)); //write         
                m_look.AddColor (9, Alliance.Emblem.BackgroundColor);
                m_look.AddColor (10, Alliance.Emblem.SymbolColor);

            }

            return m_look;
        }

        public override GameContextActorInformations GetGameContextActorInformations (Character character) {

            if (character.Guild?.Alliance?.Id == Alliance.Id) {
                return new GameRolePlayPrismInformations (Id, Look.GetEntityLook (), GetEntityDispositionInformations (),
                    GetAllianceInsiderPrismInformation ());
            }

            return new GameRolePlayPrismInformations (Id, Look.GetEntityLook (), GetEntityDispositionInformations (),
                GetAlliancePrismInformation ());
        }

        public PrismInformation GetAlliancePrismInformation () {
            return new AlliancePrismInformation (
                (sbyte) PrismListenEnum.PRISM_LISTEN_ALL, //(?)
                (sbyte) GetStatePrism (),
                NextDate.GetUnixTimeStamp (),
                Date.GetUnixTimeStamp (),
                (uint) GetNuggetCount (),
                Alliance.GetAllianceInformations ());
        }

        public PrismInformation GetAllianceInsiderPrismInformation () {
            return new AllianceInsiderPrismInformation (
                (sbyte) PrismListenEnum.PRISM_LISTEN_ALL, //(?)
                (sbyte) GetStatePrism (),
                NextDate.GetUnixTimeStamp (),
                Date.GetUnixTimeStamp (),
                (uint) GetNuggetCount (),
                Record.LastTimeSlotModificationDate.GetUnixTimeStamp (),
                (uint) Record.LastTimeSlotModificationAuthorGuildId,
                (ulong) Record.LastTimeSlotModificationAuthorId,
                Record.LastTimeSlotModificationAuthorName,
                Bag.GetObjectItems ().ToArray()); //TODO Revisar.... Modulos
        }

        public void OnDialogOpened (IDialog dialog) {
            m_openedDialogs.Add (dialog);
        }

        public void OnDialogClosed (IDialog dialog) {
            m_openedDialogs.Remove (dialog);
        }

        public PrismStateEnum GetStatePrism () {
            //PRISM_STATE_ATTACKED = 2, //in queue to defend prism
            if (IsFighting &&
                (Fighter.Fight.State == Fights.FightState.NotStarted ||
                    Fighter.Fight.State == Fights.FightState.Placement))
                return PrismStateEnum.PRISM_STATE_ATTACKED;
            //PRISM_STATE_FIGHTING = 3, //fighting
            if (IsFighting && Fighter.Fight.State == Fights.FightState.Fighting)
                return PrismStateEnum.PRISM_STATE_FIGHTING;
            //PRISM_STATE_VULNERABLE = 5, //in KoH
            if (DateTime.Now.GetUnixTimeStamp () > NextDate.GetUnixTimeStamp () && (IsWeakened || IsSabotaged))
                return PrismStateEnum.PRISM_STATE_VULNERABLE;
            //PRISM_STATE_WEAKENED = 4, // Weakened, first fight defeat witing to next fight (?)
            if (IsWeakened)
                return PrismStateEnum.PRISM_STATE_WEAKENED;

            //PRISM_STATE_DEFEATED = 6, // first fight defeat witing to next fight
            if (IsDefated)
                return PrismStateEnum.PRISM_STATE_DEFEATED;
            //PRISM_STATE_SABOTAGED = 7, //Alliance Sabotage
            if (IsSabotaged)
                return PrismStateEnum.PRISM_STATE_SABOTAGED;
            //PRISM_STATE_NORMAL = 1, //After 2 days
            if (DateTime.Now.GetUnixTimeStamp () > NextDate.GetUnixTimeStamp ())
                return PrismStateEnum.PRISM_STATE_NORMAL;
            //PRISM_STATE_INVULNERABLE = 0, //Supose don't pass 2 days
            return PrismStateEnum.PRISM_STATE_INVULNERABLE;
        }

        public uint GetNuggetCount () {
            var result = 0u;
            var state = GetStatePrism ();
            var now = DateTime.Now;
            if (now.GetUnixTimeStamp () > NextDate.GetUnixTimeStamp () && (IsWeakened || IsSabotaged))
                result = (uint) (NextDate.Subtract (NuggetBeginDate).TotalHours /* * SubArea.Record.Level */ ); // just count max NextDate
            else
                result = (uint) (now.Subtract (NuggetBeginDate).TotalHours /* * SubArea.Record.Level */ );
            return result;
        }

        public void Save () {
            WorldServer.Instance.IOTaskPool.EnsureContext ();
            if (Bag.IsDirty)
                Bag.Save (GuildManager.Instance.Database);
            WorldServer.Instance.IOTaskPool.AddMessage (() => WorldServer.Instance.DBAccessor.Database.Update (Record));
        }

        public bool IsPrismOwner (Guild guild) {
            var contains = guild.Alliance?.Prisms.Contains (this);
            return contains.HasValue && contains.Value;
        }

        public void CloseAllDialogs () {
            var array = m_openedDialogs.ToArray ();
            foreach (var dialog in array)
                dialog.Close ();
            m_openedDialogs.Clear ();
        }

        public PrismFighter CreateFighter (FightTeam team) {
            if (IsFighting)
                throw new Exception ("Prism is already fighting !");
            Fighter = new PrismFighter (team, this);
            Map.Refresh (this);
            CloseAllDialogs ();
            return Fighter;
        }

        public PrismFighter Fighter { get; set; }

        public bool IsFighting => Fighter != null;

        public void RejoinMap () {
            if (IsFighting) {
                Fighter = null;
                Map.Refresh (this);
                var state = GetStatePrism ();
                if (state == PrismStateEnum.PRISM_STATE_NORMAL)
                    Alliance.SendInformationMessage (TextInformationTypeEnum.TEXT_INFORMATION_PVP, 92, SubArea.Id, Alliance.Id, Alliance.Tag);
                //AttacksCount++;
            }
        }
    }
}