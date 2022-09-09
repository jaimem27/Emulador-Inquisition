using Stump.Core.Attributes;
using Stump.Core.Timers;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using Stump.Server.WorldServer.Game.Maps.Cells;
using Stump.Server.WorldServer.Game.Notifications;
using Stump.Server.WorldServer.Game.Parties;
using Stump.Server.WorldServer.Handlers.Context;
using System;

namespace Stump.Server.WorldServer.Game.Dungs
{
    public class DungInvitation : Notification
    {
        [Variable] public static int DisplayTime = 60;
        private TimedTimerEntry m_timer;

        public DungInvitation(ObjectPosition position, Character character, Character target, ushort dugeonid)
        {
            m_position = position;
            m_character = character;
            m_target = target;
            m_dugeonid = dugeonid;
            target.Party.MemberRemoved -= OnPartyMemberRemoved;
        }


        public ObjectPosition m_position
        {
            get;
            private set;
        }

        public Character m_character
        {
            get;
            private set;
        }
        public Character m_target
        {
            get;
            private set;
        }
        public ushort m_dugeonid
        {
            get;
            private set;
        }

        public override void Display()
        {
            //TextInformationMessag// maybe
            // TeleportBuddiesRequestedMessage ??
            m_target.DungPopup = this;
            ContextHandler.SendTeleportToBuddyOfferMessage(m_target.Client, m_dugeonid, (ulong)m_character.Id, DisplayTime);


            try
            {
                m_timer = m_target.Area.CallDelayed(DisplayTime * 1000, Deny);
            }
            catch (Exception ex)
            {
                Deny();
            }

        }

        public void Accept()
        {
            //Cancel(false);
            Cancel();
            //ContextHandler.SendTeleportToBuddyAnswerMessage(m_character.Client, m_dugeonid, (ulong)m_target.Id,true);
            m_target.Teleport(m_position);
        }

        public void Deny()
        {
            Cancel();
            //ContextHandler.SendTeleportToBuddyAnswerMessage(m_character.Client, m_dugeonid, (ulong)m_target.Id, false);
        }

        public void Cancel(bool disposePopup = true)
        {
            if (m_timer != null)
                m_timer.Dispose();
            ContextHandler.SendTeleportToBuddyCloseMessage(m_target.Client, m_dugeonid, (ulong)m_target.Id);

            if (disposePopup)
                m_target.DungPopup = null;
            // send something ?
        }
        private void OnPartyMemberRemoved(Party party, Character member, bool kicked)
        {
            Deny();
        }
    }
}