using System;
using System.Collections.Generic;
using System.Linq;
using Stump.DofusProtocol.Enums;
using Stump.Server.WorldServer.Game.Actors.Fight;
using Stump.Server.WorldServer.Game.Fights.Results;
using Stump.Server.WorldServer.Game.Fights.Teams;
using Stump.Server.WorldServer.Game.Maps;
using Stump.Server.WorldServer.Handlers.Context;

namespace Stump.Server.WorldServer.Game.Fights {
    public class FightAgressionKoH : FightAgression {
        public FightAgressionKoH (int id, Map fightMap, FightPlayerTeam blueTeam, FightPlayerTeam redTeam) : base (id, fightMap, blueTeam, redTeam) {
            m_placementTimer = Map.Area.CallDelayed (FightConfiguration.PlacementPhaseTime, StartFighting);
        }

        public override FightTypeEnum FightType => FightTypeEnum.FIGHT_TYPE_Koh;
        public override bool IsPvP => true;
        public override void StartPlacement () {
            base.StartPlacement ();
            m_placementTimer = Map.Area.CallDelayed (FightConfiguration.PlacementPhaseTime, StartFighting);
        }

        public override void StartFighting () {
            m_placementTimer.Dispose ();
            base.StartFighting ();
        }

        protected override List<IFightResult> GetResults () {
            var results = new List<IFightResult> ();
            return results;
        }

        protected override void SendGameFightJoinMessage (CharacterFighter fighter) {
            ContextHandler.SendGameFightJoinMessage (fighter.Character.Client, CanCancelFight (), !IsStarted,
                IsStarted, GetPlacementTimeLeft () / 100, FightType);
        }

        /* protected override void SendGameFightJoinMessage(FightSpectator spectator)
         {
             ContextHandler.SendGameFightJoinMessage(spectator.Character.Client, false, false, true, IsStarted,
                 GetPlacementTimeLeft() / 100, FightType);
         } */

#pragma warning disable CS0114 // 'FightAgressionKoH.GetPlacementTimeLeft()' oculta el miembro heredado 'FightAgression.GetPlacementTimeLeft()'. Para hacer que el miembro actual invalide esa implementaci�n, agregue la palabra clave override. Si no, agregue la palabra clave new.
        public int GetPlacementTimeLeft () {
#pragma warning restore CS0114 // 'FightAgressionKoH.GetPlacementTimeLeft()' oculta el miembro heredado 'FightAgression.GetPlacementTimeLeft()'. Para hacer que el miembro actual invalide esa implementaci�n, agregue la palabra clave override. Si no, agregue la palabra clave new.
            var num = FightConfiguration.PlacementPhaseTime - (DateTime.Now - CreationTime).TotalMilliseconds;
            if (num < 0.0) {
                num = 0.0;
            }
            return (int) num;
        }

        protected override bool CanCancelFight () {
            return false;
        }
    }
}