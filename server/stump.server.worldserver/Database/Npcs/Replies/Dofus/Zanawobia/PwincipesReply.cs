using Stump.DofusProtocol.Enums;
using Stump.Server.BaseServer.Database;
using Stump.Core.Reflection;
using Stump.Server.WorldServer.Database.Npcs;
using Stump.Server.WorldServer.Database.Npcs.Replies;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Npcs;
using Stump.Server.WorldServer.Game;
using Stump.Server.WorldServer.Game.Fights.Teams;
using Stump.Server.WorldServer.Game.Maps.Cells;
using Stump.Server.WorldServer.Game.Fights;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Monsters;
using Stump.Server.WorldServer.Game.Actors.Fight;
using Stump.Core.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stump.Server.WorldServer.Database.Monsters;
using System.Drawing;
using Stump.Server.WorldServer.Game.Items.Player;
using Stump.Server.WorldServer.Game.Items;
using Stump.Server.WorldServer.Handlers.Context;
using Stump.DofusProtocol.Messages;

namespace Database.Npcs.Replies
{
    [Discriminator("Pwincipes", typeof(NpcReply), new System.Type[]
 {
  typeof(NpcReplyRecord)
 })]
    class PwincipesReply : NpcReply
    {
        public PwincipesReply(NpcReplyRecord record)
            : base(record)
        {
        }

        public override bool Execute(Npc npc, Character character)
        {

            var itemUnoQuest = Singleton<ItemManager>.Instance.TryGetTemplate(17001); 
            var itemUno = character.Inventory.TryGetItem(itemUnoQuest);
            if(itemUno != null)
            {
                var grade = Singleton<MonsterManager>.Instance.GetMonsterGrade(3470, 5);
                var position = new ObjectPosition(character.Map, character.Cell, (DirectionsEnum)5);
                var monster = new Monster(grade, new MonsterGroup(0, position));

                var grade1 = Singleton<MonsterManager>.Instance.GetMonsterGrade(3471, 5);
                var monster1 = new Monster(grade1, new MonsterGroup(0, position));

                var grade2 = Singleton<MonsterManager>.Instance.GetMonsterGrade(3472, 5);
                var monster2 = new Monster(grade2, new MonsterGroup(0, position));


                var fight = Singleton<FightManager>.Instance.CreatePvMFight(character.Map);
                fight.ChallengersTeam.AddFighter(character.CreateFighter(fight.ChallengersTeam));
                fight.DefendersTeam.AddFighter(new MonsterFighter(fight.DefendersTeam, monster));
                fight.DefendersTeam.AddFighter(new MonsterFighter(fight.DefendersTeam, monster1));
                fight.DefendersTeam.AddFighter(new MonsterFighter(fight.DefendersTeam, monster2));
                fight.StartPlacement();

                //fight.HideBlades();
                //fight.StartFighting();

                ContextHandler.HandleGameFightJoinRequestMessage(character.Client,
                new GameFightJoinRequestMessage(character.Fighter.Id, (ushort)fight.Id));
                character.SaveLater();
                character.RefreshActor();
            }
            else
            {
                character.SendServerMessage("No cumples los requisitos");
                character.SaveLater();
                character.RefreshActor();
            }
            
            
            return true;
        }

    }
}