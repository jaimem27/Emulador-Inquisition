using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using Stump.DofusProtocol.Enums;
using Stump.ORM;
using Stump.ORM.SubSonic.SQLGeneration.Schema;
using Stump.Server.AuthServer.IPC;
using Stump.Server.BaseServer.IPC;

namespace Stump.Server.AuthServer.Database
{
    public class WorldServerRelator
    {
        public static string FetchQuery = "SELECT * FROM worlds";
    }

    [TableName("worlds")]
    public partial class WorldServer : IAutoGeneratedRecord
    {
        public WorldServer()
        {
            Status = ServerStatusEnum.OFFLINE;
        }

        [PrimaryKey("Id", false)]
        public int Id
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public ushort Port
        {
            get;
            set;
        }

        public string Address
        {
            get;
            set;
        }

        public bool RequireSubscription
        {
            get;
            set;
        }

        public int Completion
        {
            get;
            set;
        }

        public bool ServerSelectable
        {
            get;
            set;
        }

        public int CharCapacity
        {
            get;
            set;
        }

        public int? CharsCount
        {
            get;
            set;
        }

        public RoleEnum RequiredRole
        {
            get;
            set;
        }


        #region Status

        public ServerStatusEnum Status
        {
            get;
            set;
        }

        public bool Connected
        {
            get { return Status == ServerStatusEnum.ONLINE; }
        }

        [Ignore]
        public IPCClient IPCClient
        {
            get;
            set;
        }

        [Ignore]
        public DateTime LastPing
        {
            get;
            set;
        }

        public void SetOnline(IPCClient client)
        {
            Status = ServerStatusEnum.ONLINE;
            LastPing = DateTime.Now;
            IPCClient = client;
        }

        public void SetSaving()
        {
            Status = ServerStatusEnum.SAVING;
            CharsCount = 0;
            IPCClient = null;
        }

        public void SetSavingEnd()
        {
            Status = ServerStatusEnum.ONLINE;
            CharsCount = 0;
            IPCClient = null;
        }

        public void SetOffline()
        {
            Status = ServerStatusEnum.OFFLINE;
            CharsCount = 0;
            IPCClient = null;
        }

        #endregion

        public override string ToString()
        {
            return string.Format("{0}({1})", Name, Id);
        }
    }
}