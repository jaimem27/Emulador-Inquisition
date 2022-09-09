using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using NLog;
using Quartz;
using Stump.Core.Extensions;
using Stump.Core.Threading;
using Stump.DofusProtocol.Enums;
using Stump.DofusProtocol.Messages;
using Stump.DofusProtocol.Types;
using Stump.Server.WorldServer.Game.Actors.Look;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Monsters;
using Stump.Server.WorldServer.Game.Alliances;
using Stump.Server.WorldServer.Game.Maps;
using Stump.Server.WorldServer.Handlers.Inventory;
using Stump.Server.WorldServer.Handlers.Prism;

namespace Stump.Server.WorldServer.Game.Prisms {
    public class PrismVulneranility : IJob {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger ();
        private static Task _queueRefresherTask;
        private static Task _allianceRefresherTask;
        private const int _milliRefresh = 120000; //2 min = 120000
        private const int _updateAllianceTime = 10000; //10 seconds
        private DateTime _refreshTime;
        private DateTime _refreshAllianceTime;
        public PrismNpc Prism { get; set; }
        public SubArea SubArea => Prism.SubArea;

        public Timer EndTimer { get; private set; }
        public bool IsEnded { get; private set; }

        public ConcurrentDictionary<int, Character> _queueCharacters = new ConcurrentDictionary<int, Character> ();
        public ConcurrentDictionary<int, AllianceKoH> _fightingAlliances = new ConcurrentDictionary<int, AllianceKoH> ();
        public ConcurrentDictionary<string, bool> _ips = new ConcurrentDictionary<string, bool> ();

        public void Execute (IJobExecutionContext context) {
            try {
                JobDataMap dataMap = context.JobDetail.JobDataMap;

                var prism = (PrismNpc) dataMap["prism"];
                if (prism == null) {
                    logger.Error ("prism it's null, cannot begin KoH");
                    return; //TODO Manage!
                }
                Prism = prism;
                Initialize ();
            } catch (Exception ex) {
                logger.Error ($"Error on Execute KoH | Log : {ex.Message}"); //my bad don't use a lot catch 
            }
        }

        private void Initialize () {
            //TODO manage when user leave Alliance, so leave KoH too
            SubArea.Enter += OnPlayerEnter;
            SubArea.Exit += OnExit;
            //Add players already on area to queue
            _refreshTime = DateTime.Now;
            _queueRefresherTask = Task.Factory.StartNewDelayed (_milliRefresh, AddCharactersToFight);
            _refreshAllianceTime = DateTime.Now;
            _allianceRefresherTask = Task.Factory.StartNewDelayed (_updateAllianceTime, CheckTimeBattle);
            //Manage Maps, pjs quantity by alliance, time
            logger.Info ($"[The Prism {Prism.Id} on SubArea {Prism.SubArea.Record.Name} at {_refreshTime} o'clock it's on KoH]");
            _fightingAlliances.TryAdd (Prism.Alliance.Id, new AllianceKoH (Prism.Alliance));
            foreach (var map in SubArea.GetMaps ())
                foreach (var actor in map.GetActors<Character> ())
                    OnPlayerEnter (actor);
            EndTimer = new Timer (120 * 60 * 1000); // 2 hours
            EndTimer.Elapsed += OnEndKoH;
            //TextInformationMessage | type = 2 | Id = 89 | element 834 (subareaid) //La zona «<b>$subarea%1</b>» es vulnerable. Puedes intentar conquistarla.
            // 41 | La %1 que lleva al centro de la ciudad del %2 está siendo atacada.
            // 86 | Vuestra zona «<b>$subarea%1</b>» es vulnerable. Puedes defenderla.
            // 87 | ¡El enemigo está atacando la zona: <b>$area%1</b>! ¡¡Protegedla!!
            // 88 | La zona <b>$area%1</b> se ha defendido correctamente ¡Acabad con los últimos enemigos y la victoria será vuestra!
            // 89 | La zona «<b>$subarea%1</b>» es vulnerable. Puedes intentar conquistarla.
            // 90 | ¡Tus aliados han abierto el centro de la zona <b>$area%1</b>! ¡Ahora puedes intentar capturar ese lugar!
            // 91 | La alianza {alliance,%2::%3} ha capturado la zona «<b>$subarea%1</b>».
            // 92 | La alianza {alliance,%2::%3} ha defendido la zona «<b>$subarea%1</b>».
            // 93 | Tras vuestra victoria, se han repartido %1 pepitas entre los grupos de monstruos de la zona.
            foreach (var alliance in AllianceManager.Instance.GetAlliances ()) {
                if (alliance.Id == Prism.Alliance.Id)
                    alliance.SendInformationMessage (TextInformationTypeEnum.TEXT_INFORMATION_PVP, 86, Prism.SubArea.Id);
                else
                    alliance.SendInformationMessage (TextInformationTypeEnum.TEXT_INFORMATION_PVP, 89, Prism.SubArea.Id);
            }
        }

        private void CheckTimeBattle () {
            if (IsEnded)
                return;
            var hasWinner = false;
            if (DateTime.Now.Subtract (_refreshAllianceTime).Minutes > 1) {
                var winnerPoints = 0;
                var winnerMapCount = _fightingAlliances.Values.Max (x => x.GetTotalMaps ()); //more maps so win
                var countMaps = _fightingAlliances.Values.Count (x => x.GetTotalMaps () == winnerMapCount);
                AllianceKoH winner = null;
                foreach (var alliance in _fightingAlliances.Values) {
                    var value = 0;
                    if (countMaps == 1 && alliance.GetTotalMaps () == winnerMapCount)
                        value += 200; //bonus to have more maps on KoH = max character level
                    value += alliance.GetTotalScore ();
                    if (value > winnerPoints)
                        winner = alliance;
                }
                if (winner != null)
                    winner.AddTime ();
                _refreshAllianceTime = DateTime.Now;

                if (winner.Time >= 30)
                    EndKoh (winner.Alliance);
                else
                    hasWinner = true;

            }
            if (!hasWinner) {
                SendKoh ();
                _allianceRefresherTask = Task.Factory.StartNewDelayed (_updateAllianceTime, CheckTimeBattle);
            }
        }

        private void OnEndKoH (object sender, ElapsedEventArgs e) {
            //End KoH if time happend the owner alliance win the territory!
            //pepite repartition
            EndKoh (Prism.Alliance);
        }

        /// <summary>
        /// And Alliance get the 30 min and win the territory
        /// </summary>
        private void EndKoh (Alliance winner) {
            IsEnded = true;
            if (winner.Id == Prism.Alliance.Id) {
                PrismAllianceWin ();
            } else {
                var nuggetCount = Prism.GetNuggetCount () / 2;
                //todo: make possible change prism etc... something like prism.CanBeChanged = true and on tryAddPrism check this condition and overwite the record to make owner the new alliance
                SendLooserext (winner, Prism.SubArea.Id);
                winner.SendInformationMessage (TextInformationTypeEnum.TEXT_INFORMATION_PVP, 93, nuggetCount);
                DistributeNuggets (nuggetCount, winner.Id);
            }
            foreach (var alliance in _fightingAlliances.Values.Select (x => x.GetCharacters ()))
                alliance.ForEach (x => UnBindActions (x));
            SubArea.Enter -= OnPlayerEnter;
            SubArea.Exit -= OnExit;
            try {
                _queueCharacters = null;
                _fightingAlliances = null;
                _ips = null;
                _queueRefresherTask = null;
                EndTimer.Dispose ();
                GC.Collect ();
                GC.WaitForPendingFinalizers (); // TODO: if like implement IDisposable, I'm lazy ¬¬
            } catch (Exception ex) {
                logger.Error ($"[Failed until trying dispose | log: {ex.Message}");
            }
            /*
             Si los defensores  ganan, el 100 % de las pepitas son distribuidas sobre los monstruos de la zona. Si son los agresores quienes ganan, 
             el 50 % de las pepitas son distribuidas sobre los monstruos de la zona (y el 50 % de las pepitas son destruidas).
             */
        }

        public void PrismAllianceWin () {
            //PrismListUpdate = own zone x 2 | first state = 0 | after state = 1
            //Monsters on Zone = HasAVARewardToken = true!
            var nuggetCount = Prism.GetNuggetCount ();
            Prism.IsWeakened = false;
            Prism.IsSabotaged = false;
            Prism.NuggetBeginDate = DateTime.Now;
            SendUpdatePrism (Prism.Alliance, Prism);
            SendWinText (Prism.Alliance, Prism.SubArea.Id);
            // 93 | Tras vuestra victoria, se han repartido %1 pepitas entre los grupos de monstruos de la zona.
            Prism.Alliance.SendInformationMessage (TextInformationTypeEnum.TEXT_INFORMATION_PVP, 93, nuggetCount); //todo: pepite count and divide by monsters on area!
            DistributeNuggets (nuggetCount, Prism.Alliance.Id);
        }

        public void DistributeNuggets (uint nuggetCount, int allianceId) {
            //get monstergroup on subarea
            //get totalsum count of monsters
            //divide nuggetcount by SumofMonsters
            // Si un miembro de otra alianza se enfrenta con un grupo de monstruos que posee pepitas,
            // automáticamente serán vueltas a distribuir sobre otro grupo de monstruos presente en la zona.
            var monsters = new List<MonsterGroup> ();
            Prism.SubArea.GetMaps ().ForEach (x => monsters.AddRange (x.GetActors<MonsterGroup> ()));
            var sum = (double) monsters.Sum (x => x.Count ());
            var totalByMonster = nuggetCount / sum;
            foreach (var group in monsters) {
                Console.WriteLine ("spawn de pepitas");

                //if (group.AvAReward == null)
                //{
                //var totalNuggets = (int)Math.Floor(totalByMonster * group.Count());
                //var reward = Redis.RedisInstance.Instance.StoreMonsterSummonAvA(group.Map.Id, totalNuggets, allianceId);
                //    group.AvAReward = reward;
                //}
            }
        }

        public void SendUpdatePrism (Alliance alliance, PrismNpc prism) {
            PrismHandler.SendPrismsListUpdateMessage (alliance.Clients, prism, alliance.Id == prism.Alliance.Id);
        }

        public void SendWinText (Alliance alliance, int subAreaId) {
            foreach (var alliance2 in AllianceManager.Instance.GetAlliances ()) {
                alliance2.SendInformationMessage (TextInformationTypeEnum.TEXT_INFORMATION_PVP, 92, subAreaId, alliance.Id, alliance.Tag);
                if (alliance.Id == alliance2.Id)
                    alliance2.Clients.Send (new NotificationByServerMessage (36, new string[] { alliance.Tag }, false));
            }
        }

        public void SendLooserext (Alliance alliance, int subAreaId) {
            foreach (var alliance2 in AllianceManager.Instance.GetAlliances ()) {
                alliance2.SendInformationMessage (TextInformationTypeEnum.TEXT_INFORMATION_PVP, 91, subAreaId, alliance.Id, alliance.Tag);
                if (alliance.Id == alliance2.Id)
                    alliance2.Clients.Send (new NotificationByServerMessage (36, new string[] { alliance.Tag }, false));
            }
        }

        private void OnLogOut (Character character) {
            OnExit (character);
        }

        private void OnKingOfHill (Character character) {
            //Todo, hum too heavy calculate every time change map, but also... get alliance it's heavy too...
            //SendKoh(character.Client);
            AllianceKoH allianceKoH;
            if (_fightingAlliances.TryGetValue (character.Guild.Alliance.Id, out allianceKoH))
                character.Client.Send (GetMapByAllianceKoH (character.Map.Id, allianceKoH));
        }
        private void OnDied (Character character) {
            //all die logic here!
            AllianceKoH allianceKoH;
            if (_fightingAlliances.TryGetValue (character.Guild.Alliance.Id, out allianceKoH)) {
                if (allianceKoH.HasCharacter (character)) {
                    if (allianceKoH.AddBanned (character)) {
                        var grave = character.GetGrave ();
                        character.CustomLook = ActorLook.Parse ("{" + grave + "}");
                        character.CustomLookActivated = true;
                        character.AvaState = AggressableStatusEnum.AvA_ENABLED_NON_AGGRESSABLE;
                        PrismHandler.SendUpdateSelfAgressableStatusMessage (character.Client, (sbyte) AggressableStatusEnum.AvA_ENABLED_NON_AGGRESSABLE, 0);
                        //   character.CantMove = true;
                        InventoryHandler.SendGameRolePlayPlayerLifeStatusMessage (character.Client, (sbyte) PlayerLifeStatusEnum.STATUS_TOMBSTONE, 0);
                    }
                }
            }
        }

        private void OnKoHRevive (Character character) {
            AllianceKoH allianceKoH;
            if (_fightingAlliances.TryGetValue (character.Guild.Alliance.Id, out allianceKoH)) {
                if (allianceKoH.HasCharacter (character)) {
                    if (allianceKoH.RemoveBanned (character)) {
                        PrismHandler.SendPrismsListUpdateMessage (character.Client, Prism, Prism.Alliance.Id == character.Guild.Alliance.Id);
                        PrismHandler.SendUpdateSelfAgressableStatusMessage (character.Client, (sbyte) AggressableStatusEnum.AvA_ENABLED_AGGRESSABLE, 0);
                        character.AvaState = AggressableStatusEnum.AvA_ENABLED_AGGRESSABLE;
                        InventoryHandler.SendGameRolePlayPlayerLifeStatusMessage (character.Client, (sbyte) PlayerLifeStatusEnum.STATUS_ALIVE_AND_KICKING, 0);
                        character.CustomLookActivated = false;
                        character.Map.Refresh (character);
                    }
                }
            }
        }

        public void BindActions (Character character) {
            character.KingOfHill += OnKingOfHill;
            character.LoggedOut += OnLogOut;
            character.Died += OnDied;
            character.KoHRevive += OnKoHRevive;
        }

        public void UnBindActions (Character character) {
            character.KingOfHill -= OnKingOfHill;
            character.LoggedOut -= OnLogOut;
            character.Died -= OnDied;
            character.KoHRevive -= OnKoHRevive;
        }

        private void OnExit (Character character) {
            AllianceKoH value;
            Character valueChar;
            UnBindActions (character);
            if (_queueCharacters.TryRemove (character.Id, out valueChar)) {
                character.KingOfHill -= OnKingOfHill;
                bool dummy;
                _ips.TryRemove (character.Client.IP, out dummy);
                return;
            }
            if (character.Guild?.Alliance?.Id != null)
                if (_fightingAlliances.TryGetValue (character.Guild.Alliance.Id, out value)) {
                    if (value.RemoveCharacter (character)) {
                        character.KingOfHill -= OnKingOfHill;
                        bool dummy;
                        _ips.TryRemove (character.Client.IP, out dummy);
                    } else
                        logger.Error ($"Already deleted {character} how it's possible?");
                }
        }

        private void OnPlayerEnter (Character character) {
            if (IsEnded)
                return;
            //TODO : Died player
            /*
                //bonesId of the grave for each breeds
                case 1:
                    result = 2384;
                    break;
                case 2:
                    result = 2380;
                    break;
                case 3:
                    result = 2373;
                    break;
                case 4:
                    result = 2376;
                    break;
                case 5:
                    result = 2386;
                    break;
                case 6:
                    result = 2378;
                    break;
                case 7:
                    result = 2383;
                    break;
                case 8:
                    result = 2374;
                    break;
                case 9:
                    result = 2372;
                    break;
                case 10:
                    result = 2381;
                    break;
                case 11:
                    result = 2379;
                    break;
                case 12:
                    result = 2375;
                    break;
                case 13:
                    result = 2382;
                    break;
                case 14:
                    result = 2377;
                    break;
                case 15:
                    result = 2385;
                    break;
                case 16:
                    result = 3091;
                    break;
             */
            if (character.Level < 50)
                return;
            if (_ips.ContainsKey (character.Client.IP)) //activate later, can't do something cause ip :p 
                return;
            if (!character.AvAActived || character.Guild?.Alliance == null)
                return;
            if (_queueCharacters.ContainsKey (character.Id)) {
                logger.Error ($"Already added {character} on KoH!");
                return;
            }

            AllianceKoH allianceKoH;
            if (!_fightingAlliances.TryGetValue (character.Guild.Alliance.Id, out allianceKoH)) {
                if (_fightingAlliances.Count == 5) // 5 = Max alliances by KoH
                {
                    // TODO: manage AvA_ENABLED_NON_AGGRESSABLE
                    PrismHandler.SendPrismsListUpdateMessage (character.Client, Prism, Prism.Alliance.Id == character.Guild.Alliance.Id);
                    PrismHandler.SendUpdateSelfAgressableStatusMessage (character.Client, (sbyte) AggressableStatusEnum.AvA_DISQUALIFIED, 0);
                    character.AvaState = AggressableStatusEnum.AvA_DISQUALIFIED;
                    return;
                }
                allianceKoH = new AllianceKoH (character.Guild.Alliance);
                _fightingAlliances.TryAdd (character.Guild.Alliance.Id, allianceKoH);
            } else {
                if (allianceKoH.IsBanned (character)) {
                    PrismHandler.SendPrismsListUpdateMessage (character.Client, Prism, Prism.Alliance.Id == character.Guild.Alliance.Id);
                    PrismHandler.SendUpdateSelfAgressableStatusMessage (character.Client, (sbyte) AggressableStatusEnum.AvA_DISQUALIFIED, 0);
                    character.AvaState = AggressableStatusEnum.AvA_DISQUALIFIED;
                    return;
                }
            }

            _queueCharacters.TryAdd (character.Id, character);
            _ips.TryAdd (character.Client.IP, true);
            var time = 0;

            var date = DateTime.Now.Subtract (_refreshTime);
            if (date.TotalMinutes < 2)
                time = DateTime.Now.AddMilliseconds (date.TotalMilliseconds).GetUnixTimeStamp (); //test            
            else
                time = DateTime.Now.AddMinutes (2).GetUnixTimeStamp (); //test

            PrismHandler.SendPrismsListUpdateMessage (character.Client, Prism, Prism.Alliance.Id == character.Guild.Alliance.Id);
            PrismHandler.SendUpdateSelfAgressableStatusMessage (character.Client, (sbyte) AggressableStatusEnum.AvA_PREQUALIFIED_AGGRESSABLE, time);
            character.AvaState = AggressableStatusEnum.AvA_PREQUALIFIED_AGGRESSABLE;
            character.Client.Send (GetMapByAllianceKoH (character.Map.Id, allianceKoH));
            BindActions (character);
        }

        private void AddCharactersToFight () {
            try {
                if (IsEnded)
                    return;
                foreach (var key in _queueCharacters.Keys) {
                    Character character;
                    if (_queueCharacters.TryRemove (key, out character)) {
                        AllianceKoH allianceKoH;
                        if (_fightingAlliances.TryGetValue (character.Guild.Alliance.Id, out allianceKoH)) {
                            if (allianceKoH.AddCharacter (character)) {
                                PrismHandler.SendUpdateSelfAgressableStatusMessage (character.Client, (sbyte) AggressableStatusEnum.AvA_ENABLED_AGGRESSABLE, 0);
                                character.AvaState = AggressableStatusEnum.AvA_ENABLED_AGGRESSABLE;
                            }
                        } else {
                            allianceKoH = new AllianceKoH (character.Guild.Alliance);
                            allianceKoH.AddCharacter (character);
                            _fightingAlliances.TryAdd (character.Guild.Alliance.Id, allianceKoH);
                            PrismHandler.SendUpdateSelfAgressableStatusMessage (character.Client, (sbyte) AggressableStatusEnum.AvA_ENABLED_AGGRESSABLE, 0);
                            character.AvaState = AggressableStatusEnum.AvA_ENABLED_AGGRESSABLE;
                        }
                    }
                }
                SendKoh ();

            } catch (Exception ex) {
                logger.Error (ex.Message); //add logger
            } finally {
                _refreshTime = DateTime.Now;
                _queueRefresherTask = Task.Factory.StartNewDelayed (_milliRefresh, AddCharactersToFight);
            }
        }

        //TODO Don't show panel when enter map, just when open map!
        public KohUpdateMessage GetKoh (IEnumerable<Alliance> alliances, IEnumerable<short> allianceNbMembers,
            IEnumerable<int> allianceRoundWeigth, IEnumerable<sbyte> allianceMatchScore, IEnumerable<BasicAllianceInformations> winningMapList, uint allianceMapWinnerScore, uint allianceMapMyAllianceScore) {

            return new KohUpdateMessage (alliances.Select (x => x.GetAllianceInformations ()).ToArray(), allianceNbMembers.Select (x => (ushort) x).ToArray(), allianceRoundWeigth.Select (x => (uint) x).ToArray(),
                allianceMatchScore.Select (x => (byte) x).ToArray(), winningMapList.ToArray(), (uint) allianceMapWinnerScore, (uint) allianceMapMyAllianceScore, 0);
        }

        public KohUpdateMessage GetMapByAllianceKoH (int mapId, AllianceKoH allianceChar, IEnumerable<Alliance> alliances,
            IEnumerable<short> totalKoh, IEnumerable<int> totalMap, IEnumerable<sbyte> totalTime) {
            var fightAlliances = _fightingAlliances.Values;
            AllianceKoH winnerMap = null;
            var winnerMapScore = 0;
            var myScore = allianceChar.GetPointsByMap (mapId);
            foreach (var alliance in fightAlliances) {
                var value = alliance.GetPointsByMap (mapId);
                if (value > winnerMapScore) {
                    winnerMapScore = value;
                    winnerMap = alliance;
                }
            }
            var winningMap = GetBasicAllianceInformations (winnerMap, winnerMapScore);

            List<BasicAllianceInformations> winningMapList = new List<BasicAllianceInformations> () {
                winningMap
            };
            return GetKoh (alliances, totalKoh, totalMap, totalTime, winningMapList, (uint) winnerMapScore, (uint) myScore);
        }

        public KohUpdateMessage GetMapByAllianceKoH (int mapId, AllianceKoH allianceChar) {
            var alliances = _fightingAlliances.Values.Select (x => x.Alliance);
            var totalKoh = _fightingAlliances.Values.Select (x => x.TotalOnKoH);
            var totalMap = _fightingAlliances.Values.Select (x => x.GetTotalMaps ());
            var totalTime = _fightingAlliances.Values.Select (x => x.GetTotalTime ());
            var fightAlliances = _fightingAlliances.Values;
            AllianceKoH winnerMap = null;
            var winnerMapScore = 0;
            var myScore = allianceChar.GetPointsByMap (mapId);
            foreach (var alliance in fightAlliances) {
                var value = alliance.GetPointsByMap (mapId);
                if (value > winnerMapScore) {
                    winnerMapScore = value;
                    winnerMap = alliance;
                }
            }

            var winningMap = GetBasicAllianceInformations (winnerMap, winnerMapScore);
            List<BasicAllianceInformations> winningMapList = new List<BasicAllianceInformations> () {
                winningMap
            };
            return GetKoh (alliances, totalKoh, totalMap, totalTime, winningMapList, (uint) winnerMapScore, (uint) myScore);
        }

        public BasicAllianceInformations GetBasicAllianceInformations (AllianceKoH winnerMap, int winnerMapScore) {
            if (winnerMap != null)
                return new BasicAllianceInformations ((uint) winnerMap.Alliance.Id, winnerMap.Alliance.Tag);
            else
                return new BasicAllianceInformations (0, "");
        }

        //fuck ankama and their fucking system of points by map
        /// <summary>
        /// Too heavy calculate every alliance and send, so simply get all values and send for all alliances
        /// </summary>
        public void SendKoh () {
            var alliances = _fightingAlliances.Values.Select (x => x.Alliance);
            var totalKoh = _fightingAlliances.Values.Select (x => x.TotalOnKoH);
            var totalMap = _fightingAlliances.Values.Select (x => x.GetTotalMaps ());
            var totalTime = _fightingAlliances.Values.Select (x => x.GetTotalTime ());
            var fightAlliances = _fightingAlliances.Values;
            var mapAlliance = new Dictionary<int, Dictionary<int, KohUpdateMessage>> (fightAlliances.Count);
            foreach (var alliance in fightAlliances) {
                mapAlliance.Add (alliance.Alliance.Id, new Dictionary<int, KohUpdateMessage> ());
                var mapList = mapAlliance[alliance.Alliance.Id];
                foreach (var character in alliance.GetCharacters ()) {
                    KohUpdateMessage value;
                    if (mapList.TryGetValue (character.Map.Id, out value))
                        character.Client.Send (value);
                    else {
                        var mapPoints = GetMapByAllianceKoH (character.Map.Id, alliance, alliances, totalKoh, totalMap, totalTime);
                        mapList.Add (character.Map.Id, mapPoints);
                        character.Client.Send (mapPoints);
                    }
                }
            }
        }
    }
}