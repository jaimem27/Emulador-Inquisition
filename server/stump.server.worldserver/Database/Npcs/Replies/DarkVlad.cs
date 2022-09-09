using System;
using System.Drawing;
using System.Linq;
using Database.Dopeul;
using Stump.Core.Reflection;
using Stump.DofusProtocol.Enums;
using Stump.DofusProtocol.Messages;
using Stump.Server.BaseServer.Database;
using Stump.Server.WorldServer.Game.Actors.Fight;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Monsters;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Npcs;
using Stump.Server.WorldServer.Game.Fights;
using Stump.Server.WorldServer.Game.Maps.Cells;
using Stump.Server.WorldServer.Handlers.Context;

namespace Stump.Server.WorldServer.Database.Npcs.Replies
{
    [Discriminator("DV", typeof(NpcReply), typeof(NpcReplyRecord))]
    internal class DarkVlad : NpcReply
    {
        public DarkVlad(NpcReplyRecord record) : base(record)
        {
        }

        public override bool Execute(Npc npc, Character character)
        {
            var DeleteDopeul = new DopeulRecord();
            var compareTime = DateTime.Now;
            foreach (var dopeul in character.DoppleCollection.Dopeul.Where(dopeul => dopeul.DopeulId == 3578))
            {
                DeleteDopeul = dopeul;
                compareTime = dopeul.Time;
                break;
            }
            if (!(compareTime <= DateTime.Now))
            {
                switch (character.Account.Lang)
                {
                    default:
                        character.SendServerMessage(
                    $"No puedes iniciar una pelea contra el, debes esperar <b>{compareTime.Subtract(DateTime.Now).Hours} horas, {compareTime.Subtract(DateTime.Now).Minutes} minutos</b>",
                    Color.PaleVioletRed);
                        break;
                }
                character.LeaveDialog();
                return false;
            }
            else if (compareTime <= DateTime.Now)
            {
                if (DeleteDopeul != null)
                {
                    DeleteDopeul.Ip = character.Client.IP;
                    DeleteDopeul.IsUpdated = true;
                    DeleteDopeul.Time = DateTime.Now.AddHours(24);
                }

                var grade = Singleton<MonsterManager>.Instance.GetMonsterGrade(3578, 5);
                var position = new ObjectPosition(character.Map, character.Cell, (DirectionsEnum)5);
                var monster = new Monster(grade, new MonsterGroup(0, position));

                var fight = Singleton<FightManager>.Instance.CreateFightDarkVlad(character.Map);
                fight.ChallengersTeam.AddFighter(character.CreateFighter(fight.ChallengersTeam));
                fight.DefendersTeam.AddFighter(new MonsterFighter(fight.DefendersTeam, monster));
                fight.StartPlacement();

                ContextHandler.HandleGameFightJoinRequestMessage(character.Client,
                    new GameFightJoinRequestMessage(character.Fighter.Id, (ushort)fight.Id));
                character.SaveLater();
                return true;
            }
            switch (character.Account.Lang)
            {
                default:
                    character.SendServerMessage(
                $"No puedes iniciar una pelea contra el, debes esperar <b>{compareTime.Subtract(DateTime.Now).Hours} horas, {compareTime.Subtract(DateTime.Now).Minutes} minutos</b>",
                Color.PaleVioletRed);
                    break;
            }
            character.LeaveDialog();
            return false;
        }
    }
}