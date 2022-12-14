using Stump.DofusProtocol.D2oClasses;
using Stump.ORM;
using Stump.ORM.SubSonic.SQLGeneration.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stump.Server.WorldServer.Database.Dungeons
{
    public class DungeonRelator
    {
        public static string FetchQuery =
            "SELECT * FROM dungeons";
    }

    [TableName("dungeons")]
    public class DungeonRecord : IAutoGeneratedRecord
    {
        [PrimaryKey("Id")]
        public int Id
        {
            get;
            set;
        }

        public uint NameId
        {
            get;
            set;
        }

        public int OptimalPlayerLevel
        {
            get;
            set;
        }

        public string MapsIdsCSV
        {
            get;
            set;
        }

        public int EntranceMapId
        {
            get;
            set;
        }

        public int ExitMapId
        {
            get;
            set;
        }
    }
}
