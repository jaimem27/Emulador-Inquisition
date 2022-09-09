using Database.Dopeul;
using Stump.Server.BaseServer.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using Stump.Server.WorldServer.Database.Llavero;

namespace Stump.Server.WorldServer.Game.Llavero
{
    public class LlaveroCollection : DataManager<LlaveroCollection>
    {
        public List<LlaveroRecord> Llavero { get; set; }

        public void Save(ORM.Database database)
        {
            foreach (var llavero in Llavero)
            {
                if (llavero.IsUpdated && !llavero.IsNew)
                {
                    database.Update(llavero);
                    llavero.IsUpdated = false;
                }

                if (llavero.IsNew)
                {
                    LlaveroManager.Instance.AddRecord(llavero);
                    database.Insert(llavero);
                    llavero.IsNew = false;
                }
            }
        }

        public void Load(int id)
        {
            Llavero = FindByOwner(id);
        }


        public List<LlaveroRecord> FindByOwner(int ownerId)
        {
            return LlaveroManager.Instance.GetCharacterRecords(ownerId);
        }

        internal void Load(Character character)
        {
            Llavero = FindByOwner(character.Id);
        }
    }
}
