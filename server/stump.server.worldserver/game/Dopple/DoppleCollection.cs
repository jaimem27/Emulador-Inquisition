using Database.Dopeul;
using Stump.Server.BaseServer.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stump.Server.WorldServer.Game.Dopeul;
using System.Threading.Tasks;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;

namespace Stump.Server.WorldServer.Game.Dopeul
{
    public class DopeulCollection : DataManager<DopeulCollection>
    {
        public List<DopeulRecord> Dopeul { get; set; }

        public void Save(ORM.Database database)
        {
            foreach (var dopeul in Dopeul)
            {
                if (dopeul.IsUpdated && !dopeul.IsNew)
                {
                    database.Update(dopeul);
                    dopeul.IsUpdated = false;
                }

                if (dopeul.IsNew)
                {
                    DoppleManager.Instance.AddRecord(dopeul);
                    database.Insert(dopeul);
                    dopeul.IsNew = false;
                }
            }
        }

        public void Load(int id)
        {
            Dopeul = FindByOwner(id);
        }

        public void Load(string ip)
        {
            Dopeul = FindByOwner(ip);
        }

        public List<DopeulRecord> FindByOwner(int ownerId)
        {
            return DoppleManager.Instance.GetCharacterRecords(ownerId);
        }

        public List<DopeulRecord> FindByOwner(string ip)
        {
            return DoppleManager.Instance.GetCharacterRecords(ip);
        }

        internal void Load(Character character)
        {
            Dopeul = FindByOwner(character.Id);
        }
    }
}
