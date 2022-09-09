using System;
using Stump.Core.Reflection;
using Stump.DofusProtocol.Enums;
using Stump.DofusProtocol.Messages;
using Stump.Server.BaseServer.Database;
using Stump.Server.WorldServer.Game.Actors.Fight;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Monsters;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Npcs;
using Stump.Server.WorldServer.Game.Fights;
using Stump.Server.WorldServer.Game.Items;
using Stump.Server.WorldServer.Game.Maps.Cells;
using Stump.Server.WorldServer.Handlers.Context;

namespace Stump.Server.WorldServer.Database.Npcs.Replies
{
    [Discriminator("OsoReply", typeof(NpcReply), typeof(NpcReplyRecord))]
    internal class OsoReply : NpcReply
    {
        public OsoReply(NpcReplyRecord record) : base(record)
        {
        }

        public override bool Execute(Npc npc, Character character)
        {
            var faseTerminada = Singleton<ItemManager>.Instance.TryGetTemplate(16138);
            var item = character.Inventory.TryGetItem(faseTerminada);

            if(item != null)
            {
                character.SendServerMessage("El Hombre Oso entiende que quieres comerte a su compañero, se ha enfadado y empieza a atacarte.");
                var position = new ObjectPosition(character.Map, character.Cell, (DirectionsEnum)3);

                var fight = Singleton<FightManager>.Instance.CreatePvMFight(character.Map);
                fight.ChallengersTeam.AddFighter(character.CreateFighter(fight.ChallengersTeam));

                var group = new MonsterGroup(0, position);


                var grade = Singleton<MonsterManager>.Instance.GetMonsterGrade(5270, 5);

                var monster = new Monster(grade, group);

                fight.DefendersTeam.AddFighter(new MonsterFighter(fight.DefendersTeam, monster));

                fight.StartPlacement();

                ContextHandler.HandleGameFightJoinRequestMessage(character.Client,
                new GameFightJoinRequestMessage(character.Fighter.Id, (ushort)fight.Id));
                character.SaveLater();
            }
            else
            {
                character.SendServerMessage("No cumples con los requisitos.");
            }
            

            return true;
        }
    }
}