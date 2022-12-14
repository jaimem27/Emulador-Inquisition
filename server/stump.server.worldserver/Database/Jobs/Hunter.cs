using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stump.ORM;
using Stump.ORM.SubSonic.SQLGeneration.Schema;

//By Allan for Avalone adapted per saiki
namespace Stump.Server.WorldServer.Database.Jobs
{
    public class HunterRelator
    {
        public static string FetchQuery = "SELECT * FROM jobs_hunter";
    }

    [TableName("jobs_hunter")]
    public class HunterRecord : IAutoGeneratedRecord
    {
        [PrimaryKey("MonsterId")]
        public int MonsterId
        {
            get;
            set;
        }

        public int DropId
        {
            get;
            set;
        }

        public int Level
        {
            get;
            set;
        }

        public string Note
        {
            get;
            set;
        }

    }
}