using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Database.Dopeul;
using Stump.Core.Reflection;
using Stump.DofusProtocol.Enums;
using Stump.Server.BaseServer.Database;
using Stump.Server.WorldServer.Database.Llavero;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Npcs;
using Stump.Server.WorldServer.Game.Items;
using Stump.Server.WorldServer.Game.Llavero;
using Stump.Server.WorldServer.Game.Maps.Cells;

namespace Stump.Server.WorldServer.Database.Npcs.Replies
{
    [Discriminator("Llavero", typeof(NpcReply), typeof(NpcReplyRecord))]
    internal class LlaveroReply : NpcReply
    {
        public LlaveroReply(NpcReplyRecord record) : base(record)
        {
        }

        public LlaveroReply()
        {
            Record.Type = "Llavero";
        }

        private bool m_mustRefreshPosition;
        private ObjectPosition m_position;

        public int LlaveId
        {
            get
            {
                var result = 0;
                try
                {
                    result = Record.GetParameter<int>(0u);
                }
                catch { }
                return result;
            }
            set { Record.SetParameter(0u, value); }
        }

        public int Map_Id
        {
            get
            {
                return Record.GetParameter<int>(1);
            }
            set
            {
                Record.SetParameter(1, value);
                m_mustRefreshPosition = true;
            }
        }

        /// <summary>
        /// Parameter 1
        /// </summary>
        public int Cell_Id
        {
            get
            {
                return Record.GetParameter<int>(2);
            }
            set
            {
                Record.SetParameter(2, value);
                m_mustRefreshPosition = true;
            }
        }

        /// <summary>
        /// Parameter 2
        /// </summary>
        public DirectionsEnum Direction
        {
            get
            {
                return (DirectionsEnum)Record.GetParameter<int>(3);
            }
            set
            {
                Record.SetParameter(3, (int)value);
                m_mustRefreshPosition = true;
            }
        }

        private void RefreshPosition()
        {
            var map = Game.World.Instance.GetMap(Map_Id);

            if (map == null)
                throw new Exception(string.Format("Cannot load SkillTeleport id={0}, map {1} isn't found", Id, Map_Id));

            var cell = map.Cells[Cell_Id];

            m_position = new ObjectPosition(map, cell, Direction);
        }

        public ObjectPosition GetPosition()
        {
            if (m_position == null || m_mustRefreshPosition)
                RefreshPosition();

            m_mustRefreshPosition = false;

            return m_position;
        }

        public override bool Execute(Npc npc, Character character)
        {
            LlaveroRecord EditLlavero = null;

            var compareTime = DateTime.Now;
            character.LlaveroCollecction.Load(character.Id);
            foreach (var llavero in character.LlaveroCollecction.Llavero.Where(llavero => llavero.LlaveId == LlaveId))
            {
                EditLlavero = llavero;
                compareTime = llavero.Time;
                break;
            }
            if (compareTime <= DateTime.Now)
            {
                var llaveroId = new LlaveroCollection();
                llaveroId.Load(character.Id);
                foreach (var llavero in llaveroId.Llavero.Where(llavero => llavero.LlaveId == LlaveId))
                {
                    EditLlavero = llavero;
                    compareTime = llavero.Time;
                    break;
                }
                if (!(compareTime <= DateTime.Now))
                {
                    switch (character.Account.Lang)
                    {
                        case "es":
                            character.SendServerMessage(
                        $"Ya has gastado el llavero en esta mazmorra!, Tienes que esperar <b>{compareTime.Subtract(DateTime.Now).Days} días, {compareTime.Subtract(DateTime.Now).Hours} Horas</b>",
                        Color.Red);
                            break;
                        case "fr":
                            character.SendServerMessage(
                         $"Ya has gastado el llavero en esta mazmorra!, Tienes que esperar <b>{compareTime.Subtract(DateTime.Now).Days} días, {compareTime.Subtract(DateTime.Now).Hours} Horas</b>",
                        Color.Red);
                            break;
                        default:
                            character.SendServerMessage(
                         $"Ya has gastado el llavero en esta mazmorra!, Tienes que esperar <b>{compareTime.Subtract(DateTime.Now).Days} días, {compareTime.Subtract(DateTime.Now).Hours} Horas</b>",
                        Color.Red);
                            break;
                    }
                    character.LeaveDialog();
                    return false;
                }

                character.AddMazmorraTerminada(LlaveId);
                character.SendServerMessage($"Has utilizado el llavero para entrar a la mazmorra.");
                character.Teleport(GetPosition());
                character.RefreshActor();

                if (EditLlavero != null)
                {
                    EditLlavero.Id = character.Id;
                    EditLlavero.IsUpdated = true;
                    EditLlavero.Time = DateTime.Now.AddDays(7);
                }
                else
                {
                    Singleton<ItemManager>.Instance.CreateLlavero(character, LlaveId);
                    character.AddMazmorraTerminada(LlaveId);
                    character.SendServerMessage($"Has utilizado el llavero para entrar a la mazmorra.");
                    character.Teleport(GetPosition());
                    character.RefreshActor();
                }
                character.SaveLater();
                return true;
            }
            switch (character.Account.Lang)
            {
                case "es":
                    character.SendServerMessage(
                $"Ya has gastado el llavero en esta mazmorra!, Tienes que esperar <b>{compareTime.Subtract(DateTime.Now).Days} días, {compareTime.Subtract(DateTime.Now).Hours} Horas</b>",
                Color.Red);
                    break;
                case "fr":
                    character.SendServerMessage(
                $"Ya has gastado el llavero en esta mazmorra!, Tienes que esperar <b>{compareTime.Subtract(DateTime.Now).Days} días, {compareTime.Subtract(DateTime.Now).Hours} Horas</b>",
                Color.Red);
                    break;
                default:
                    character.SendServerMessage(
                $"Ya has gastado el llavero en esta mazmorra!, Tienes que esperar <b>{compareTime.Subtract(DateTime.Now).Days} días, {compareTime.Subtract(DateTime.Now).Hours} Horas</b>",
                Color.Red);
                    break;
            }
            character.LeaveDialog();
            return false;
        }
    }
}
