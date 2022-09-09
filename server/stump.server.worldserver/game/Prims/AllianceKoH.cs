using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Stump.Server.WorldServer.Core.Network;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using Stump.Server.WorldServer.Game.Alliances;

namespace Stump.Server.WorldServer.Game.Prisms {
    public class AllianceKoH {
        private ConcurrentDictionary<int, Character> _characters = new ConcurrentDictionary<int, Character> ();
        private ConcurrentDictionary<int, Character> _bannedCharacters = new ConcurrentDictionary<int, Character> ();
        public WorldClientCollection Clients { get; } = new WorldClientCollection ();

        public Alliance Alliance { get; set; }
        public short TotalOnKoH { get; set; }
        public sbyte Time { get; set; }

        public AllianceKoH (Alliance alliance) {
            Alliance = alliance;
        }

        public List<Character> GetCharacters () {
            return _characters.Values.ToList ();
        }

        public bool HasCharacter (Character character) {
            return _characters.ContainsKey (character.Id);
        }

        public bool AddCharacter (Character character) {
            if (_characters.TryAdd (character.Id, character)) {
                Clients.Add (character.Client);
                TotalOnKoH += 1;
                return true;
            }
            return false;
        }

        public bool RemoveCharacter (Character character) {
            Character value;
            if (_characters.TryRemove (character.Id, out value)) {
                Clients.Remove (character.Client);
                TotalOnKoH -= 1;
                return true;
            }
            return false;
        }

        public List<Character> GetValidCharacters () {
            var result = new List<Character> ();
            foreach (var character in GetCharacters ()) {
                if (_bannedCharacters.ContainsKey (character.Id) || character.IsBusy () || character.IsFighting ())
                    continue;
                result.Add (character);
            }
            return result;
        }

        public int GetTotalScore () {
            int result = 0;
            foreach (var character in GetValidCharacters ())
                result += character.Level;
            return result;
        }

        public int GetTotalMaps () {
            var result = new List<int> ();
            foreach (var character in _characters.Values) {
                var mapId = character.Map.Id;
                if (!result.Contains (mapId))
                    result.Add (mapId);
            }
            return result.Count;
        }

        public sbyte GetTotalTime () {
            return Time;
        }

        public int GetPointsByMap (int map) {
            var result = 0;
            foreach (var character in GetValidCharacters ().Where (x => x.Map.Id == map))
                result += character.Level;
            return result;
        }

        internal void AddTime () {
            Time += 1;
        }

        public bool IsBanned (Character character) {
            return _bannedCharacters.ContainsKey (character.Id);
        }

        public bool AddBanned (Character character) {
            return _bannedCharacters.TryAdd (character.Id, character);
        }

        public bool RemoveBanned (Character character) {
            Character characterOut;
            return _bannedCharacters.TryRemove (character.Id, out characterOut);
        }
    }
}