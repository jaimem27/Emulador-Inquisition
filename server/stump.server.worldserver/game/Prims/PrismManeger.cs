using System;
using System.Collections.Generic;
using System.Linq;
using Stump.Core.Pool;
using Stump.Core.Reflection;
using Stump.Server.BaseServer;
using Stump.Server.BaseServer.Database;
using Stump.Server.BaseServer.Initialization;
using Stump.Server.WorldServer.Database;
using Stump.Server.WorldServer.Database.World;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using Stump.Server.WorldServer.Game.Items;
using Stump.Server.WorldServer.Game.Items.Player;

namespace Stump.Server.WorldServer.Game.Prisms {
    public class PrismManager : DataManager<PrismManager>, ISaveable {
        private readonly List<PrismNpc> m_activePrisms = new List<PrismNpc> ();
        // FIELDS
        private UniqueIdProvider m_idProvider;
        private Dictionary<int, WorldMapPrismRecord> m_prismSpawns;

        public void Save () {
            foreach (var current in
                    from prism in m_activePrisms
                select prism)
                current.Save ();
        }

        // PROPERTIES
        public List<PrismNpc> Prisms => m_activePrisms;

        // CONSTRUCTORS

        // METHODS
        [Initialization (InitializationPass.Eighth)]
        public override void Initialize () {
            m_prismSpawns = Database.Query<WorldMapPrismRecord> (WorldMapPrismRelator.FetchQuery)
                .ToDictionary (entry => entry.Id);

            m_idProvider = m_prismSpawns.Any () ?
                new UniqueIdProvider ((from item in m_prismSpawns select item.Value.Id).Max ()) :
                UniqueIdProvider.Default;

            Singleton<World>.Instance.RegisterSaveableInstance (this);
            Singleton<World>.Instance.SpawnPrisms ();
        }

        public void AddActivePrism (PrismNpc npc) {
            if (!m_activePrisms.Contains (npc))
                m_activePrisms.Add (npc);
        }

        public WorldMapPrismRecord[] GetPrismSpawns () {
            return m_prismSpawns.Values.ToArray ();
        }

        public WorldMapPrismRecord[] GetPrismSpawns (int allianceId) {
            return (
                from entry in m_prismSpawns.Values where entry.AllianceId == allianceId select entry).ToArray ();
        }

        public bool TryAddPrism (Character character, bool lazySave = true) {

            bool result = false;
            

            if ( /* character.SubArea.Record.Capturable && */ !character.SubArea.HasPrism && character.Map.Prism == null && character.SubArea.Id != 10 && !character.Map.IsDungeon()) //subarea 10 é a area do mapa de pvp, se ele ficar weakened todos ficam impedido de agredir :(
            {
                if (!character.SubArea.Record.Capturable) {

                    switch (character.Account.Lang) {
                        case "fr":
                            character.SendServerMessage ("La sous-zone ne peut pas être capturée!");
                            break;
                        case "es":
                            character.SendServerMessage ("¡Subarea no se puede capturar!");
                            break;
                        case "en":
                            character.SendServerMessage ("Subarea can not be captured!");
                            break;
                        default:
                            character.SendServerMessage ("Subarea não pode ser capturada!");
                            break;
                    }
                }

                var position = character.Position.Clone ();
                var npc = new PrismNpc (m_idProvider.Pop (), position.Map.GetNextContextualId (), position,
                    character.Guild.Alliance, character);
                if (lazySave)
                    ServerBase<WorldServer>.Instance.IOTaskPool.AddMessage (delegate {
                        Database.Insert (npc.Record);
                        m_prismSpawns.Add (npc.GlobalId, npc.Record);
                        AddActivePrism (npc);
                        npc.Map.Enter (npc);
                        character.Guild.Alliance.AddPrism (npc);
                        //character.Inventory.RemoveItem(itemTemplate, 1);

                    });
                else {
                    Database.Insert (npc.Record);
                    m_prismSpawns.Add (npc.GlobalId, npc.Record);
                    AddActivePrism (npc);
                    npc.Map.Enter (npc);
                    character.Guild.Alliance.AddPrism (npc);
                    
                }

                result = true;
            } else if (!character.SubArea.Record.Capturable) {
                switch (character.Account.Lang) {
                    case "fr":
                        character.SendServerMessage ("La sous-zone ne peut pas être capturée!");
                        break;
                    case "es":
                        character.SendServerMessage ("¡Subarea no se puede capturar!");
                        break;
                    case "en":
                        character.SendServerMessage ("Subarea can not be captured!");
                        break;
                    default:
                        character.SendServerMessage ("Subarea não pode ser capturada!");
                        break;
                }
            } else if (character.SubArea.HasPrism) {
                switch (character.Account.Lang) {
                    case "fr":
                        character.SendServerMessage ("La sous-zone contient déjà du prisme!");
                        break;
                    case "es":
                        character.SendServerMessage ("¡Subárea ya contiene prisma!");
                        break;
                    case "en":
                        character.SendServerMessage ("Subarea already contains prism!");
                        break;
                    default:
                        character.SendServerMessage ("Subarea já contem prisma!");
                        break;
                }
            } else if (character.Map.Prism != null) {

                switch (character.Account.Lang) {
                    case "fr":
                        character.SendServerMessage ("La carte contient déjà du prisme!");
                        break;
                    case "es":
                        character.SendServerMessage ("¡El mapa ya contiene prisma!");
                        break;
                    case "en":
                        character.SendServerMessage ("The map already contains prism!");
                        break;
                    default:
                        character.SendServerMessage ("O mapa já contém prisma!");
                        break;
                }
            }
            return result;
        }
    }
}