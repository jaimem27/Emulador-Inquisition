using Stump.DofusProtocol.Messages;
using Stump.DofusProtocol.Types;
using Stump.Server.BaseServer.Network;
using Stump.Server.WorldServer.Core.Network;
using Stump.Server.WorldServer.Game.Actors;
using Stump.Server.WorldServer.Game.Actors.Fight;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Monsters;
using Stump.Server.WorldServer.Game.Fights;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Stump.Server.WorldServer.Handlers.Context.RolePlay
{
    public partial class ContextRoleplayHandler : WorldHandlerContainer
    {
        [WorldHandler(GameRolePlayAttackMonsterRequestMessage.Id)]
        public static void HandleGameRolePlayAttackMonsterRequestMessage(WorldClient client, GameRolePlayAttackMonsterRequestMessage message)
        {
            var map = client.Character.Map;
            var monster = map.GetActor<MonsterGroup>(entry => entry.Id == message.MonsterGroupId);

            if (client.Character.Inventory.IsFull())
            {
                client.Character.SendServerMessage("Acci?n imposible, tienes el inventario lleno.", Color.DarkOrange);
                return;
            }

            if (monster != null && (monster.Position.Cell == client.Character.Position.Cell || monster.Position.Point.ManhattanDistanceTo(client.Character.Position.Cell) <= 2))
                monster.FightWith(client.Character);

            if(monster != null && client.Character.Map.Id == 107482112) //Sala 2 dung Skonk
                monster.FightWith(client.Character);

            if (monster != null && client.Character.Map.Id == 17302528) //Sala 1 - Mazmorra Maestro Pandora
                monster.FightWith(client.Character);

            if (monster != null && client.Character.Map.Id == 17304576) //Sala 2 - Mazmorra Maestro Pandora
                monster.FightWith(client.Character);

            if (monster != null && client.Character.Map.Id == 18091008) //Sala 3 - Mazmorra de los Kitsus
                monster.FightWith(client.Character);

            if (monster != null && client.Character.Map.Id == 61867008) //Sala 2 - Mazmorra Tejossus
                monster.FightWith(client.Character);

            if (monster != null && client.Character.Map.Id == 182324225) //Sala 4 - Mazmorra Ugah
                monster.FightWith(client.Character);

            if (monster != null && client.Character.Map.Id == 181666816) //Sala 2 - Carpa de los Magik Riktus
                monster.FightWith(client.Character);

            if (monster != null && client.Character.Map.Id == 27788292) //Sala 4 - Mazmorra de los dragohuevos
                monster.FightWith(client.Character);



            //A?adir mazmorras donde el moob es inacesible
        }

        [WorldHandler(GameFightPlacementSwapPositionsRequestMessage.Id)]
        public static void HandleGameFightPlacementSwapPositionsRequestMessage(WorldClient client, GameFightPlacementSwapPositionsRequestMessage message)
        {
            if (!client.Character.IsFighting())
                return;

            if (client.Character.Fighter.Position.Cell.Id != message.CellId)
            {
                var cell = client.Character.Fight.Map.Cells[message.CellId];
                var target = client.Character.Fighter.Team.GetOneFighter(cell);

                if (target == null)
                    return;
                if (target is CharacterFighter)
                {
                    if (client.Character.Fighter.IsTeamLeader())
                        client.Character.Fighter.SwapPrePlacement(target);
                    else
                    {
                        var swapRequest = new SwapRequest(client.Character, (target as CharacterFighter).Character);
                        swapRequest.Open();
                    }
                }
                else if (target is CompanionActor)
                {
                    client.Character.Fighter.SwapPrePlacement(target);
                }
            }
        }

        [WorldHandler(GameFightPlacementSwapPositionsAcceptMessage.Id)]
        public static void HandleGameFightPlacementSwapPositionsAcceptMessage(WorldClient client, GameFightPlacementSwapPositionsAcceptMessage message)
        {
            if (!client.Character.IsInRequest() || !(client.Character.RequestBox is SwapRequest))
                return;

            if (message.RequestId == client.Character.RequestBox.Source.Id)
                client.Character.RequestBox.Accept();
        }

        [WorldHandler(GameFightPlacementSwapPositionsCancelMessage.Id)]
        public static void HandleGameFightPlacementSwapPositionsCancelMessage(WorldClient client, GameFightPlacementSwapPositionsCancelMessage message)
        {
            if (!client.Character.IsInRequest() || !(client.Character.RequestBox is SwapRequest))
                return;

            if (message.RequestId == client.Character.RequestBox.Source.Id)
            {
                if (client.Character == client.Character.RequestBox.Source)
                    client.Character.RequestBox.Cancel();
                else
                    client.Character.RequestBox.Deny();
            }
        }

        public static void SendGameRolePlayPlayerFightFriendlyAnsweredMessage(IPacketReceiver client, Character replier,
                                                                              Character source, Character target,
                                                                              bool accepted)
        {
            client.Send(new GameRolePlayPlayerFightFriendlyAnsweredMessage((ushort)replier.Id,
                                                                           (ulong)source.Id,
                                                                           (ulong)target.Id,
                                                                           accepted));
        }

        public static void SendGameRolePlayPlayerFightFriendlyRequestedMessage(IPacketReceiver client, Character requester,
                                                                               Character source,
                                                                               Character target)
        {
            client.Send(new GameRolePlayPlayerFightFriendlyRequestedMessage((ushort)requester.Id, (ulong)source.Id,
                                                                            (ulong)target.Id));
        }

        public static void SendGameRolePlayArenaUpdatePlayerInfosMessage(WorldClient client, Character character)
        {
            var ranking = new ArenaRanking((ushort)character.ArenaRank_3vs3, (ushort)character.ArenaMaxRank_3vs3);
            var rankingLeague = new ArenaLeagueRanking((ushort)character.ArenaRank_3vs3, 0, 0, 0, 0);

            client.Send(new GameRolePlayArenaUpdatePlayerInfosAllQueuesMessage(new ArenaRankInfos(ranking, rankingLeague, (ushort)character.ArenaDailyMatchsWon_3vs3, (ushort)character.ArenaDailyMatchsCount_3vs3, 10), new ArenaRankInfos(), new ArenaRankInfos()));
        }

        public static void SendGameRolePlayAggressionMessage(IPacketReceiver client, Character challenger, Character defender)
        {
            client.Send(new GameRolePlayAggressionMessage((ulong)challenger.Id, (ulong)defender.Id));
        }

        public static void SendGameFightPlacementSwapPositionsMessage(IPacketReceiver client, IEnumerable<ContextActor> actors)
        {
            //client.Send(new GameFightPlacementSwapPositionsMessage(actors.Select(entry => entry.GetIdentifiedEntityDispositionInformations())));
        }

        public static void SendGameFightPlacementSwapPositionsOfferMessage(IPacketReceiver client, Character source, Character target)
        {
            client.Send(new GameFightPlacementSwapPositionsOfferMessage(source.Id, source.Fighter.Id, (ushort)source.Cell.Id, target.Fighter.Id, (ushort)target.Cell.Id));
        }

        public static void SendGameFightPlacementSwapPositionsCancelledMessage(IPacketReceiver client, Character source, Character canceller)
        {
            client.Send(new GameFightPlacementSwapPositionsCancelledMessage(source.Fighter.Id, canceller.Fighter.Id));
        }
    }
}