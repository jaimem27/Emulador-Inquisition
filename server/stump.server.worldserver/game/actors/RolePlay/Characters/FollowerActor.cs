using System.Collections.Generic;
using System.Linq;
using Stump.DofusProtocol.Enums;
using Stump.DofusProtocol.Types;
using Stump.Server.WorldServer.Game.Actors.Interfaces;
using Stump.Server.WorldServer.Game.Actors.Look;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Monsters;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Npcs;
using Stump.Server.WorldServer.Game.Maps.Cells;
using Stump.Server.WorldServer.Game.Maps.Pathfinding;
using Stump.Server.WorldServer.Handlers.Context;

namespace Stump.Server.WorldServer.Game.Actors.RolePlay
{
    public class FollowerActor : NamedActor, IContextDependant
    {
        public int m_contextId;
        string m_name;
        short m_type;
        int m_follower;
        ActorLook m_look;

        public FollowerActor(int contextId, ObjectPosition position, Character caller, short type, int follower)
        {
            m_contextId = contextId;
            Position = position;
            m_type = type;
            m_follower = follower;
            if (m_type == 1 || m_type == 21)
                m_look = NpcManager.Instance.GetNpcTemplate(m_follower).Look;
            else if (m_type == 2)
                m_look = MonsterManager.Instance.GetTemplate(m_follower).EntityLook;

        }
        public override int Id
        {
            get
            {
                return m_contextId;
            }
        }

        public override string Name
        {
            get
            {
                return "Teste";
            }
        }
        public override ActorLook Look
        {
            get
            {
                return m_look ?? RefreshLook();
            }
        }
        #region Look

        public ActorLook RefreshLook()
        {
            m_look = new ActorLook
            { BonesID = (short)m_follower };

            return m_look;
        }

        #endregion
        #region Movement
        public override bool StartMove(Path movementPath)
        {
            if (!CanMove() || movementPath.IsEmpty())
                return false;

            Position = movementPath.EndPathPosition;
            var keys = movementPath.GetServerPathKeys();

            Map.ForEach(entry => ContextHandler.SendGameMapMovementMessage(entry.Client, keys, this));

            StopMove();

            return true;
        }

        #endregion

        #region Network

        #region HumanInformations

        //public virtual ActorRestrictionsInformations GetActorRestrictionsInformations()
        //{
        //    return new ActorRestrictionsInformations();
        //}

        //public virtual HumanInformations GetHumanInformations()
        //{
        //    return new HumanInformations(GetActorRestrictionsInformations(),
        //        Sex == SexTypeEnum.SEX_FEMALE,
        //        Enumerable.Empty<HumanOption>()); // todo
        //}

        #endregion

        #endregion
    }
}