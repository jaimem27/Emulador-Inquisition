using Database.Dopeul;
using Stump.Server.BaseServer.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using Stump.Server.WorldServer.Database.Almanax;

namespace Stump.Server.WorldServer.Game.Almanax
{
    public class AlmanaxCollection : DataManager<AlmanaxCollection>
    {
        public List<AlmanaxRecord> Almanax { get; set; }

        public void Save(ORM.Database database)
        {
            foreach (var almanax in Almanax)
            {
                if (almanax.IsUpdated && !almanax.IsNew)
                {
                    database.Update(almanax);
                    almanax.IsUpdated = false;
                }

                if (almanax.IsNew)
                {
                    try
                    {
                        AlmanaxManager.Instance.AddRecord(almanax);
                        database.Insert(almanax);
                        almanax.IsNew = false;
                    }
                    catch { }
                    
                }
            }
        }

        public void Load(int id)
        {
            Almanax = FindByOwner(id);
        }

        public void Load(string ip)
        {
            Almanax = FindByOwner(ip);
        }

        public List<AlmanaxRecord> FindByOwner(int ownerId)
        {
            return AlmanaxManager.Instance.GetCharacterRecords(ownerId);
        }

        public List<AlmanaxRecord> FindByOwner(string ip)
        {
            return AlmanaxManager.Instance.GetCharacterRecords(ip);
        }

        internal void Load(Character character)
        {
            Almanax = FindByOwner(character.Id);
        }
    }
}
