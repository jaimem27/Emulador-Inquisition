using Stump.DofusProtocol.Enums;
using Stump.Server.WorldServer.Database.Monsters;
using Stump.Server.WorldServer.Game.Accounts;
using Stump.Server.WorldServer.Game.Actors.Fight;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using Stump.Server.WorldServer.Game.Diverse;
using Stump.Server.WorldServer.Game.Fights.Challenges;
using Stump.Server.WorldServer.Game.Fights.Results;
using Stump.Server.WorldServer.Game.Fights.Results.Data;
using Stump.Server.WorldServer.Game.Fights.Teams;
using Stump.Server.WorldServer.Game.Formulas;
using Stump.Server.WorldServer.Game.Items;
using Stump.Server.WorldServer.Game.Items.Player.Custom;
using Stump.Server.WorldServer.Game.Maps;
using Stump.Server.WorldServer.Handlers.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Stump.Server.WorldServer.Database.Items.Templates;
using Stump.Server.WorldServer.Game.Items.Player;
using System.Drawing;
using Stump.Server.BaseServer.Database;
using Stump.Server.WorldServer.Game.Alliances;
using Stump.Server.WorldServer.Game.Guilds;
using Stump.Server.WorldServer.Game.Achievements;
using Stump.Core.Reflection;
using Stump.Server.WorldServer.Game.Fights.Buffs;

namespace Stump.Server.WorldServer.Game.Fights
{
    public class FightPvM : Fight<FightMonsterTeam, FightPlayerTeam>
    {
        private bool m_ageBonusDefined;
        public static int[] d_salaFinal = {
            152834048,//Chadalid
            190318592,//Campos
            193729536,//Esponja
            121374211,//Jalato
            163582464,//Boostacho
            94114816,//Escara
            87033346,//Ronin
            96209922,//Batofu
            146670592,//Blatarrata
            17565953,//Bulbing
            104596995,//Bworka
            87296515,//Herrero
            5242886,//Coralador magistral
            64753664,//Kwokan
            96998917,//Shin larva
            166987778,//Blop Reinieta
            166986754,//Blop guinda
            166985730,//blop indigo
            166988802,//blop coco
            98567687,//Gelatinas
            157552640,//Kanibola
            116392450,//Wey Wabbit
            106960896,//Crujidor legendario
            176951296,//Nelwyn
            116654595,//Wey Wobot
            79434241,//Daigoboro
            174330368,//Manticoro
            149688320,//Abraknido Ancestral
            149165056,//Lasoberaña
            157028352,//Lechuko
            181669888,//Judini
            72361984,//Dragocerdo
            155718656,//Maxilubu
            107227136,//Trankitronko
            157290496,//Moon
            22809602,//Rasgabola
            27000836,//Rata blanca
            40110085,//Rata Negra
            27789316,//Crocabulia
            17309696,//Maestro Pandora
            107485184,//Skonk
            96209926,//Tofu Real
            55053312,//Jalamut
            18089988,//Tanuki San
            132911104,//Thor vestruz
            149427200,//Roble
            56102912,//Morsaguiño
            102760963,//Sprinter cell
            57154305,//Obsidiante
            21497858,//Kimbo
            56365056,//Roca negra
            125831683,//Kanigula
            175902208,//Tripas de anelion
            104333827,//Bworker
            182192129,//Ugah
            26740736,//Kralamar
            66326528,//Papa No-es
            62919680,//Cil
            61868036,//Tejosus
            62134792,//Golosotron
            57938689,//Smigol
            123212800,//Sombra
            179572736,//Razord
            110101506,//Syrlagh
            110363650,//Klim
            109840899,//Mizz
            109576707,//Nileza
            112203523,//Konde
            119277059,//Merkator
            140776448,//Ballena
            169873408,//Meno
            169349120,//Kutulu
            169611264,//Dentinea
            176164864,//Tal Kasha
            182718464,//anerice
            184686337,//Illyzaelle
            187437056,//Bethel
            187957512,//Solar
            195039232,//Dazak
            130286592,//Malesticof
            159125512,//Maestro cuerbok
            161743872,//Piojorojo
            34473474,//Minotauro
            143138823,//Fraktal
            136578048,//Eskarlata
            16516867,//Peki-Peki
            130548736,//Perfosil
            34472450,//ToT
            162004992,//Ush
            59515904,//Fuji
            59516162,//Tengu
            143917569,//Xlii
            136840192,//Toxoliath
            129500160,//Nidas
            137101312,//Reina
            143393281,//Vortex
            160564224,//Miauvizor
            89392130, //Tynrill
            66850816 //Santa
        };
        public static int[] d_logroUno = {
            115,//Chadalid
            116,//Girasol
            119,//Esponja
            121,//Jalato
            127,//Boostacho
            123,//Escara
            129,//Ronin
            125,//Batofu
            342,//Blatarrata
            141,//Bulbing
            131,//Bworka
            133,//Herrero
            137,//Coralador magistral
            139,//Kwokan
            135,//Shin larva
            153,//Blop Reinieta
            149,//Blop guinda
            151,//blop indigo
            147,//blop coco
            167,//gelatinas
            145,//kanibola
            143,//Wey Wabbit
            159,//Crujidor legendario
            155,//Nelwyn
            288,//Wey Wobbot
            169,//Daigorobo
            368,//Manticoro
            171,//Abraknido Ancestral
            345,//Lasoberaña
            348,//LeChuko
            383,//Judini
            173,//Dragocerdo
            177,//Maxilubu
            175,//Trankitronko
            179,//Moon
            181,//Rasgabola
            185,//Rata Blanca
            187,//Rata Negra
            199,//Crocabulia
            195,//Maestro Pandora
            201,//Skonk
            197,//Tofu Real
            193,//Jalamut
            203,//Tanuki San
            303,//Thor vestruz
            207,//Roble
            211,//Morsaguiño
            215,//Snifer cell
            221,//Obsidiante
            217,//Kimbo
            213,//Roca negra
            300,//Kanigula
            374,//Anelido
            229,//Bworker
            231,//Ougah
            227,//Kralamar
            247,//PapaNoes
            225,//Cil
            233,//Tejosus
            239,//Golosotron
            237,//Smigol
            297,//Sombra
            380,//Razord
            262,//Syrlagh
            259,//Klim
            260,//Mizz
            258,//Nileza
            263,//Conde
            295,//Merkator
            325,//Ballena
            359,//Meno
            361,//Kutulu
            364,//Dentinea
            377,//Talkasha
            387,//Anerice
            389,//Illyzaelle
            395,//Bethel
            392,//Solar
            398,//Dazah
            312,//Malesticof
            183,//Mestro cuerbok
            351,//Piojorojo
            191,//Minotauro
            338,//Fraktal
            321,//Eskarlata
            205,//Peki
            310,//Perfosil
            219,//ToT
            354,//Ush
            235,//Fuji
            223,//Tengu
            335,//XLII
            324,//Toxoliath
            306,//Nidas
            315,//Reina
            332,//Vortex
            356,//Miauvizor
            209,//Tynrill
            244 //Santa
        };
        public static int[] d_logroDos = {
            117,//Chadalid
            118,//Campos
            120,//Esponja
            122,//Jalato
            128,//Boostacho
            124,//Escara
            130,//Ronin
            126,//Batofu
            343,//Blatarrata
            142,//Bulbing
            132,//Bworka
            134,//Herrero
            138,//Coralador magistral
            140,//Kwokan
            136,//Shin larva
            154,//Blop Reinieta
            150,//Blop guinda
            152,//blop indigo
            148,//blop coco
            168,//gelatinas
            146,//kanibola
            144,//Wey Wabbit
            160,//Crujidor Legendario
            156,//Nelwyn
            289,//Wey Wobbot
            170,//Daigorobo
            369,//Manticoro
            172,//Abraknido Ancestral
            346,//Lasoberaña
            349,//LeChuko
            384,//Judini
            174,//Drago cerdo
            178,//Maxilubu
            176,//Trankitronko
            180,//Moon
            182,//Rasgabola
            186,//Rata Blanca
            188,//Rata Negra
            200,//Crocabulia
            196,//Maestro Pandora
            202,//Skonk
            198,//Tofu real
            194,//Jalamut
            204,//TanukiSan
            304,//Thoravestruz
            208,//Roble
            212,//Morsaguiño
            216,//Sprinter cell        
            222,//Obsidiante
            218,//Kimbo
            214,//Roca Negra
            301,//Kanigula
            375,//anelido
            230,//Bworker
            232,//Ougah
            228,//Kralamar
            248,//PapaNoEs
            226,//Cil
            234,//Tejosus
            240,//Golosotron
            238,//Smigol
            298,//Sombra
            381,//Razord
            266,//Syrlagh
            261,//Klim
            265,//Mizz
            264,//Nileza
            267,//Conde
            296,//Merkator
            326,//Ballena
            360,//Meno
            362,//Kutulu
            365,//Dentinea
            378,//Talkasha
            388,//Anerice
            390,//Illyzaelle
            396,//Bethel
            393,//Solar
            399,//Dazak
            313,//Malestixcof
            184,//Maestro Cuerbok
            352,//Piojorojo
            192,//Minotauro
            339,//Fraktal
            322,//Eskarlata
            206,//Peki
            309,//Perfosil
            220,//ToT
            355,//Ush
            236,//Fuji
            224,//Tengu
            336,//XLII
            319,//Toxoliath
            307,//Nidas
            316,//Reina
            333,//Vortex
            357,//Miauvuzor
            210,//Tynrill
            245//Santa
        };

        public FightPvM(int id, Map fightMap, FightMonsterTeam defendersTeam, FightPlayerTeam challengersTeam)
            : base(id, fightMap, defendersTeam, challengersTeam)
        {
        }

        public override void StartPlacement()
        {
            base.StartPlacement();

            m_placementTimer = Map.Area.CallDelayed(FightConfiguration.PlacementPhaseTime, StartFighting);
        }

        public override void StartFighting()
        {
            m_placementTimer.Dispose();
            if (PlayerTeam.Leader.Character.IsPartyLeader())
                ActiveIdols = PlayerTeam.Leader.Character.Party.IdolInventory.ComputeIdols(this).ToList();
            else
                ActiveIdols = PlayerTeam.Leader.Character.IdolInventory.ComputeIdols(this).ToList();

            base.StartFighting();
        }

        protected override void OnFightStarted()
        {
            base.OnFightStarted();

            if (!Map.AllowFightChallenges)
                return;

            initChallenge();

            if (Map.IsDungeon() || IsPvMArenaFight)
                initChallenge();

            void initChallenge()
            {
                var challenge = ChallengeManager.Instance.GetRandomChallenge(this);

                // no challenge found
                if (challenge == null)
                    return;

                challenge.Initialize();
                AddChallenge(challenge);

            }

            if (Map.IsDungeon())
                MostrarLogros();
            // VOTE
            //if(DateTime.Now < ChallengersTeam.Leader.Character.Account.SubscriptionEndDate.AddHours(3d))
            //{
            //    var challenge = ChallengeManager.Instance.GetChallenge(51, this);

            //    if (challenge == null)
            //        return;

            //    challenge.Initialize();
            //    AddChallenge(challenge);
            //}
        }

        private void MostrarLogros()
        {

            for (int i = 0; i < d_salaFinal.Length; i++)
            {
                if (Map.Id == d_salaFinal[i])
                {
                    var logroUno = ChallengeManager.Instance.GetChallenge(d_logroUno[i], this);
                    var logroDos = ChallengeManager.Instance.GetChallenge(d_logroDos[i], this);
                    //logroUno.Initialize();
                    //logroDos.Initialize();
                    if(logroUno != null)
                        AddChallenge(logroUno);
                    if (logroDos != null)
                        AddChallenge(logroDos);
                }
            }

        }

        protected override void OnFighterAdded(FightTeam team, FightActor actor)
        {
            base.OnFighterAdded(team, actor);

            if (!(team is FightMonsterTeam) || m_ageBonusDefined)
                return;

            if (team.Leader is MonsterFighter monsterFighter)
                AgeBonus = monsterFighter.Monster.Group.AgeBonus;

            m_ageBonusDefined = true;
        }

        public FightPlayerTeam PlayerTeam => Teams.FirstOrDefault(x => x.TeamType == TeamTypeEnum.TEAM_TYPE_PLAYER) as FightPlayerTeam;

        public FightMonsterTeam MonsterTeam => Teams.FirstOrDefault(x => x.TeamType == TeamTypeEnum.TEAM_TYPE_MONSTER) as FightMonsterTeam;

        public override FightTypeEnum FightType => FightTypeEnum.FIGHT_TYPE_PvM;

        public override bool IsPvP => false;

        public bool IsPvMArenaFight
        {
            get;
            set;
        }

        private uint LootPepitas(int prospec)
        {
            var listMonster = Fighters.Where(x => x is MonsterFighter);
            if (listMonster.Count() > 8)
            {
                for (int i = 0; i < listMonster.Count() - 8; i++)
                {
                    listMonster.ToList().RemoveAt(listMonster.Count() - i - 1);
                    i++;
                }
            }
            int levelTotal = listMonster.Select(x => (int)x.Level).Sum();

            var calc = (uint)(levelTotal / 5 * (prospec / 100f));

            if (levelTotal > 450)
                calc = (uint)(levelTotal / 4 * (prospec / 100f));

            if (levelTotal > 900)
                calc = (uint)(levelTotal / 3 * (prospec / 100f));

            if (calc <= 0)
                calc = 1;

            return calc;
        }

        private int LootOrbs(int prospec)
        {
            var listMonster = Fighters.Where(x => x is MonsterFighter);
            if (listMonster.Count() > 8)
            {
                for (int i = 0; i < listMonster.Count() - 8; i++)
                {
                    listMonster.ToList().RemoveAt(listMonster.Count() - i - 1);
                    i++;
                }
            }
            int levelTotal = listMonster.Select(x => (int)x.Level).Sum();

            var calc = (uint)(levelTotal / 2.5 * prospec / 120f);

            if (levelTotal > 900)
                calc = (uint)(levelTotal / 4 * prospec / 120f);

            if (levelTotal > 450)
                calc = (uint)(levelTotal / 3.5 * prospec / 120f);

            if (calc <= 0)
                calc = 1;

            return (int)calc * 100;

        }

        protected override List<IFightResult> GetResults()
        {
            var results = new List<IFightResult>();
            results.AddRange(GetFightersAndLeavers().Where(entry => entry.HasResult).Select(entry => entry.GetFightResult()));
            var prims = Map.SubArea.Maps.Select(x => x.Prism).Where(x => x != null).FirstOrDefault();
            if (Map.IsDungeon())
            {
                var taxCollectors = Map.TaxCollector;
                if (taxCollectors != null)
                    results.Add(new TaxCollectorProspectingResult(taxCollectors, this));

            }
            else
            {
                var taxCollectors = Map.SubArea.Maps.Select(x => x.TaxCollector).Where(x => x != null && x.CanGatherLoots()).FirstOrDefault();
                if (taxCollectors != null)
                {
                    results.Add(new TaxCollectorProspectingResult(taxCollectors, this));

                }

            }
            

            //var taxCollectors = Map.SubArea.Maps.Select(x => x.TaxCollector).Where(x => x != null && x.CanGatherLoots());
            //results.AddRange(taxCollectors.Select(x => new TaxCollectorProspectingResult(x, this)));


            foreach (var team in m_teams)
            {
                IEnumerable<FightActor> droppers = team.OpposedTeam.GetAllFighters(entry => entry.IsDead() && entry.CanDrop()).ToList();
                var looters = results.Where(x => x.CanLoot(team) && !(x is TaxCollectorProspectingResult)).OrderByDescending(entry => entry.Prospecting).
                    Concat(results.OfType<TaxCollectorProspectingResult>().Where(x => x.CanLoot(team)).OrderByDescending(x => x.Prospecting)); // tax collector loots at the end
                var teamPP = team.GetAllFighters<CharacterFighter>().Sum(entry => (entry.Stats[PlayerFields.Prospecting].Total >= 100) ? 100 : entry.Stats[PlayerFields.Prospecting].Total);
                var looterx = looters.ToList();
                var kamas = Winners == team ? droppers.Sum(entry => entry.GetDroppedKamas()) * team.GetAllFighters<CharacterFighter>().Count() : 0;
                var taxCollectors = Map.SubArea.Maps.Select(x => x.TaxCollector).Where(x => x != null && x.CanGatherLoots()).FirstOrDefault();
                foreach (var looter in looters)
                {

                    #region Ascension Boss
                    if (team == Winners && looter is FightPlayerResult)
                    {
                        foreach (var monster in this.DefendersTeam.GetAllFighters())
                        {
                            if (monster is MonsterFighter)
                            {
                                Actors.RolePlay.Characters.Character character = (looter as FightPlayerResult).Character;
                                if (AscensionEnum.GetAscensionFloorMap(character.GetAscensionStair())[0].Equals(character.Map.Id))
                                {
                                    foreach (var item in AscensionEnum.GetAscensionFloorLoots(character.GetAscensionStair()))
                                    {
                                        looter.Loot.AddItem(new DroppedItem(item[0], (uint)item[1])); // si oui on loot l'item
                                    }

                                    #region Ornamentos
                                    if (character.GetAscensionStair() == 10 && !character.HasOrnament(139)) //Soñador 1
                                    {
                                        character.AddOrnament(139);
                                    }

                                    if (character.GetAscensionStair() == 20 && !character.HasOrnament(140))//Soñador 2
                                    {
                                        character.AddOrnament(140);
                                    }

                                    if (character.GetAscensionStair() == 30 && !character.HasOrnament(141))//Soñador 3
                                    {
                                        character.AddOrnament(141);
                                    }

                                    if (character.GetAscensionStair() == 40 && !character.HasOrnament(142))//Soñador 4
                                    {
                                        character.AddOrnament(142);
                                    }

                                    if (character.GetAscensionStair() == 50 && !character.HasOrnament(143))//Soñador 5
                                    {
                                        character.AddOrnament(143);
                                    }

                                    if (character.GetAscensionStair() == 60 && !character.HasOrnament(144))//Soñador 6
                                    {
                                        character.AddOrnament(144);
                                    }

                                    if (character.GetAscensionStair() == 70 && !character.HasOrnament(145))//Soñador 7
                                    {
                                        character.AddOrnament(145);
                                    }

                                    if (character.GetAscensionStair() == 80 && !character.HasOrnament(146))//Soñador 8
                                    {
                                        character.AddOrnament(146);
                                    }

                                    if (character.GetAscensionStair() == 90 && !character.HasOrnament(147))//Soñador 9
                                    {
                                        character.AddOrnament(147);
                                    }

                                    #endregion

                                    if (character.GetAscensionStair() < 99)
                                    {
                                        character.SendServerMessage("Has conquistado el piso " + (character.AscensionStair + 1));
                                        character.AddAscensionStair(1);
                                        if (character.GetAscensionStair() == 99)
                                        {

                                            #region ornamentos
                                            if (!character.HasOrnament(148)) // Soñador 10
                                            {
                                                character.AddOrnament(148);
                                            }
                                            else
                                            {
                                                if (!character.HasOrnament(149))// Soñador 11
                                                {
                                                    character.AddOrnament(149);
                                                }
                                                else
                                                {
                                                    if (!character.HasOrnament(150))// Soñador 12
                                                    {
                                                        character.AddOrnament(150);
                                                    }
                                                    else
                                                    {
                                                        if (!character.HasOrnament(151))// Soñador 13
                                                        {
                                                            character.AddOrnament(151);
                                                        }
                                                    }
                                                }
                                            }
                                            #endregion

                                            character.SendServerMessage("Has completado la torre");
                                            World.Instance.SendAnnounce("<b>" + character.Name + "</b> a terminado todos los pisos de la Torre.", Color.Yellow);
                                        }
                                    }
                                    return results;
                                }
                            }
                        }
                    }
                    #endregion

                    #region Coffre au trésor
                    if (team == Winners && looter is FightPlayerResult)
                    {
                        Character character = (looter as FightPlayerResult).Character;

                        foreach (var monster in DefendersTeam.GetAllFighters())
                        {
                            if (monster is MonsterFighter)
                            {
                                var incrementXpTresor = false;
                                if (character.Record.TreasureSearch == 2 || character.Record.TreasureSearch == 3)
                                    incrementXpTresor = true;

                                //Coffre trésor lvl 20
                                if ((monster as MonsterFighter).Monster.Template.Id == 3724 && (monster as MonsterFighter).Monster.Grade.Level == 20)
                                {
                                    looter.Loot.AddItem(15263, 50 * (uint)(incrementXpTresor ? 2 : 1));//Rose des sables
                                    looter.Loot.AddItem(15265, 1 * (uint)(incrementXpTresor ? 2 : 1));//Coffre de ressources
                                    character.SendInformationMessage(TextInformationTypeEnum.TEXT_INFORMATION_MESSAGE, 21, 50, 15263);
                                    character.SendInformationMessage(TextInformationTypeEnum.TEXT_INFORMATION_MESSAGE, 21, 1, 15265);

                                    character.DisplayNotification("Felicidades, has vencido al tesoro.", NotificationEnum.INFORMATION);
                                    character.SendServerMessage("Búsqueda del tesoro finalizada, has recogido el tesoro.");

                                    character.RemoveEmote(EmotesEnum.COMPLETE_A_QUEST);

                                    character.Record.TreasureSearch = 0;
                                    character.Record.TreasureMapCoffre = null;
                                    character.Record.TreasureTimeStart = DateTime.Now;
                                    character.Record.TreasureIndice = null;

                                    DataManager.DefaultDatabase.Update(character.Record);
                                    return results;
                                }
                                //Coffre trésor lvl 40
                                if ((monster as MonsterFighter).Monster.Template.Id == 3724 && (monster as MonsterFighter).Monster.Grade.Level == 40)
                                {
                                    looter.Loot.AddItem(15263, 70 * (uint)(incrementXpTresor ? 2 : 1));//Rose des sables
                                    looter.Loot.AddItem(15265, 1 * (uint)(incrementXpTresor ? 2 : 1));//Coffre de ressources
                                    character.SendInformationMessage(TextInformationTypeEnum.TEXT_INFORMATION_MESSAGE, 21, 70, 15263);
                                    character.SendInformationMessage(TextInformationTypeEnum.TEXT_INFORMATION_MESSAGE, 21, 1, 15265);

                                    character.DisplayNotification("Felicidades, has vencido al tesoro.", NotificationEnum.INFORMATION);
                                    character.SendServerMessage("Búsqueda del tesoro finalizada, has recogido el tesoro.");

                                    character.RemoveEmote(EmotesEnum.COMPLETE_A_QUEST);

                                    character.Record.TreasureSearch = 0;
                                    character.Record.TreasureMapCoffre = null;
                                    character.Record.TreasureTimeStart = DateTime.Now;
                                    character.Record.TreasureIndice = null;

                                    DataManager.DefaultDatabase.Update(character.Record);
                                    return results;
                                }
                                //Coffre trésor lvl 60
                                if ((monster as MonsterFighter).Monster.Template.Id == 3724 && (monster as MonsterFighter).Monster.Grade.Level == 60)
                                {
                                    looter.Loot.AddItem(15263, 90 * (uint)(incrementXpTresor ? 2 : 1));//Rose des sables
                                    looter.Loot.AddItem(15265, 1 * (uint)(incrementXpTresor ? 2 : 1));//Coffre de ressources
                                    character.SendInformationMessage(TextInformationTypeEnum.TEXT_INFORMATION_MESSAGE, 21, 90, 15263);
                                    character.SendInformationMessage(TextInformationTypeEnum.TEXT_INFORMATION_MESSAGE, 21, 1, 15265);

                                    character.DisplayNotification("Felicidades, has vencido al tesoro.", NotificationEnum.INFORMATION);
                                    character.SendServerMessage("Búsqueda del tesoro finalizada, has recogido el tesoro.");

                                    character.RemoveEmote(EmotesEnum.COMPLETE_A_QUEST);

                                    character.Record.TreasureSearch = 0;
                                    character.Record.TreasureMapCoffre = null;
                                    character.Record.TreasureTimeStart = DateTime.Now;
                                    character.Record.TreasureIndice = null;

                                    DataManager.DefaultDatabase.Update(character.Record);
                                    return results;
                                }
                                //Coffre trésor lvl 80
                                if ((monster as MonsterFighter).Monster.Template.Id == 3724 && (monster as MonsterFighter).Monster.Grade.Level == 80)
                                {
                                    looter.Loot.AddItem(15263, 110 * (uint)(incrementXpTresor ? 2 : 1));//Rose des sables
                                    looter.Loot.AddItem(15265, 1 * (uint)(incrementXpTresor ? 2 : 1));//Coffre de ressources
                                    character.SendInformationMessage(TextInformationTypeEnum.TEXT_INFORMATION_MESSAGE, 21, 110, 15263);
                                    character.SendInformationMessage(TextInformationTypeEnum.TEXT_INFORMATION_MESSAGE, 21, 1, 15265);

                                    character.DisplayNotification("Felicidades, has vencido al tesoro.", NotificationEnum.INFORMATION);
                                    character.SendServerMessage("Búsqueda del tesoro finalizada, has recogido el tesoro.");

                                    character.RemoveEmote(EmotesEnum.COMPLETE_A_QUEST);

                                    character.Record.TreasureSearch = 0;
                                    character.Record.TreasureMapCoffre = null;
                                    character.Record.TreasureTimeStart = DateTime.Now;
                                    character.Record.TreasureIndice = null;

                                    DataManager.DefaultDatabase.Update(character.Record);
                                    return results;
                                }
                                //Coffre trésor lvl 100
                                if ((monster as MonsterFighter).Monster.Template.Id == 3724 && (monster as MonsterFighter).Monster.Grade.Level == 100)
                                {
                                    looter.Loot.AddItem(15263, 130 * (uint)(incrementXpTresor ? 2 : 1));//Rose des sables
                                    looter.Loot.AddItem(15265, 1 * (uint)(incrementXpTresor ? 2 : 1));//Coffre de ressources
                                    character.SendInformationMessage(TextInformationTypeEnum.TEXT_INFORMATION_MESSAGE, 21, 130, 15263);
                                    character.SendInformationMessage(TextInformationTypeEnum.TEXT_INFORMATION_MESSAGE, 21, 1, 15265);

                                    character.DisplayNotification("Felicidades, has vencido al tesoro.", NotificationEnum.INFORMATION);
                                    character.SendServerMessage("Búsqueda del tesoro finalizada, has recogido el tesoro.");

                                    character.RemoveEmote(EmotesEnum.COMPLETE_A_QUEST);

                                    character.Record.TreasureSearch = 0;
                                    character.Record.TreasureMapCoffre = null;
                                    character.Record.TreasureTimeStart = DateTime.Now;
                                    character.Record.TreasureIndice = null;

                                    DataManager.DefaultDatabase.Update(character.Record);
                                    return results;
                                }
                                //Coffre trésor lvl 120
                                if ((monster as MonsterFighter).Monster.Template.Id == 3724 && (monster as MonsterFighter).Monster.Grade.Level == 120)
                                {
                                    looter.Loot.AddItem(15263, 150 * (uint)(incrementXpTresor ? 2 : 1));//Rose des sables
                                    looter.Loot.AddItem(15265, 1 * (uint)(incrementXpTresor ? 2 : 1));//Coffre de ressources
                                    character.SendInformationMessage(TextInformationTypeEnum.TEXT_INFORMATION_MESSAGE, 21, 150, 15263);
                                    character.SendInformationMessage(TextInformationTypeEnum.TEXT_INFORMATION_MESSAGE, 21, 1, 15265);

                                    character.DisplayNotification("Felicidades, has vencido al tesoro.", NotificationEnum.INFORMATION);
                                    character.SendServerMessage("Búsqueda del tesoro finalizada, has recogido el tesoro.");

                                    character.RemoveEmote(EmotesEnum.COMPLETE_A_QUEST);

                                    character.Record.TreasureSearch = 0;
                                    character.Record.TreasureMapCoffre = null;
                                    character.Record.TreasureTimeStart = DateTime.Now;
                                    character.Record.TreasureIndice = null;

                                    DataManager.DefaultDatabase.Update(character.Record);
                                    return results;
                                }
                                //Coffre trésor lvl 140
                                if ((monster as MonsterFighter).Monster.Template.Id == 3724 && (monster as MonsterFighter).Monster.Grade.Level == 140)
                                {
                                    looter.Loot.AddItem(15263, 170 * (uint)(incrementXpTresor ? 2 : 1));//Rose des sables
                                    looter.Loot.AddItem(15265, 1 * (uint)(incrementXpTresor ? 2 : 1));//Coffre de ressources
                                    character.SendInformationMessage(TextInformationTypeEnum.TEXT_INFORMATION_MESSAGE, 21, 170, 15263);
                                    character.SendInformationMessage(TextInformationTypeEnum.TEXT_INFORMATION_MESSAGE, 21, 1, 15265);

                                    character.DisplayNotification("Felicidades, has vencido al tesoro.", NotificationEnum.INFORMATION);
                                    character.SendServerMessage("Búsqueda del tesoro finalizada, has recogido el tesoro.");

                                    character.RemoveEmote(EmotesEnum.COMPLETE_A_QUEST);

                                    character.Record.TreasureSearch = 0;
                                    character.Record.TreasureMapCoffre = null;
                                    character.Record.TreasureTimeStart = DateTime.Now;
                                    character.Record.TreasureIndice = null;

                                    DataManager.DefaultDatabase.Update(character.Record);
                                    return results;
                                }
                                //Coffre trésor lvl 160
                                if ((monster as MonsterFighter).Monster.Template.Id == 3724 && (monster as MonsterFighter).Monster.Grade.Level == 160)
                                {
                                    looter.Loot.AddItem(15263, 190 * (uint)(incrementXpTresor ? 2 : 1));//Rose des sables
                                    looter.Loot.AddItem(15265, 1 * (uint)(incrementXpTresor ? 2 : 1));//Coffre de ressources
                                    character.SendInformationMessage(TextInformationTypeEnum.TEXT_INFORMATION_MESSAGE, 21, 190, 15263);
                                    character.SendInformationMessage(TextInformationTypeEnum.TEXT_INFORMATION_MESSAGE, 21, 1, 15265);

                                    character.DisplayNotification("Felicidades, has vencido al tesoro.", NotificationEnum.INFORMATION);
                                    character.SendServerMessage("Búsqueda del tesoro finalizada, has recogido el tesoro.");

                                    character.RemoveEmote(EmotesEnum.COMPLETE_A_QUEST);

                                    character.Record.TreasureSearch = 0;
                                    character.Record.TreasureMapCoffre = null;
                                    character.Record.TreasureTimeStart = DateTime.Now;
                                    character.Record.TreasureIndice = null;

                                    DataManager.DefaultDatabase.Update(character.Record);
                                    return results;
                                }
                                //Coffre trésor lvl 180
                                if ((monster as MonsterFighter).Monster.Template.Id == 3724 && (monster as MonsterFighter).Monster.Grade.Level == 180)
                                {
                                    looter.Loot.AddItem(15263, 210 * (uint)(incrementXpTresor ? 2 : 1));//Rose des sables
                                    looter.Loot.AddItem(15265, 1 * (uint)(incrementXpTresor ? 2 : 1));//Coffre de ressources
                                    character.SendInformationMessage(TextInformationTypeEnum.TEXT_INFORMATION_MESSAGE, 21, 210, 15263);
                                    character.SendInformationMessage(TextInformationTypeEnum.TEXT_INFORMATION_MESSAGE, 21, 1, 15265);

                                    character.DisplayNotification("Felicidades, has vencido al tesoro.", NotificationEnum.INFORMATION);
                                    character.SendServerMessage("Búsqueda del tesoro finalizada, has recogido el tesoro.");

                                    character.RemoveEmote(EmotesEnum.COMPLETE_A_QUEST);

                                    character.Record.TreasureSearch = 0;
                                    character.Record.TreasureMapCoffre = null;
                                    character.Record.TreasureTimeStart = DateTime.Now;
                                    character.Record.TreasureIndice = null;

                                    DataManager.DefaultDatabase.Update(character.Record);
                                    return results;
                                }
                                //Coffre trésor lvl 200
                                if ((monster as MonsterFighter).Monster.Template.Id == 3724 && (monster as MonsterFighter).Monster.Grade.Level == 200)
                                {
                                    looter.Loot.AddItem(15263, 230 * (uint)(incrementXpTresor ? 2 : 1));//Rose des sables
                                    looter.Loot.AddItem(15265, 1 * (uint)(incrementXpTresor ? 2 : 1));//Coffre de ressources
                                    character.SendInformationMessage(TextInformationTypeEnum.TEXT_INFORMATION_MESSAGE, 21, 230, 15263);
                                    character.SendInformationMessage(TextInformationTypeEnum.TEXT_INFORMATION_MESSAGE, 21, 1, 15265);

                                    character.DisplayNotification("Felicidades, has vencido al tesoro.", NotificationEnum.INFORMATION);
                                    character.SendServerMessage("Búsqueda del tesoro finalizada, has recogido el tesoro.");

                                    character.RemoveEmote(EmotesEnum.COMPLETE_A_QUEST);

                                    character.Record.TreasureSearch = 0;
                                    character.Record.TreasureMapCoffre = null;
                                    character.Record.TreasureTimeStart = DateTime.Now;
                                    character.Record.TreasureIndice = null;

                                    DataManager.DefaultDatabase.Update(character.Record);
                                    return results;
                                }
                            }
                        }
                       
                    }
                    #endregion

                    //VIP
                    var character1 = World.Instance.GetCharacter(looter.Id);
                    var multiplicator = 1.0f;


                    if (character1 != null && character1.PvPEnabled)
                    {
                        multiplicator += 0.25f;

                    }

                    //Pocimas XPDROP
                    if (character1 is Character)
                    {
                        var bonusItem = Singleton<ItemManager>.Instance.TryGetTemplate(18365);
                        var buffo = character1.Inventory.TryGetItem(bonusItem);
                        if (buffo != null)
                        {

                            multiplicator += 0.4f;
                            character1.Inventory.RemoveItem(buffo, 1);
                        }
                    }

                    //By Shine
                    #region RetomarMazmorra
                    try
                    {
                        if (Map.IsDungeon())
                        {
                            character1.Record.RetomarMazmorra = Map.Id;
                        }
                        else
                        {
                            character1.Record.RetomarMazmorra = 0;
                        }
                    }
                    catch { }
                    #endregion

                    //Pepitas
                    if (prims != null)
                    {

                        var cosa = Map.SubArea.Prism.Alliance.Id;

                        if (character1 is Character)
                        {
                            if (character1.HasEmote(EmotesEnum.EMOTE_GUILD))
                            {
                                if (character1.HasEmote(EmotesEnum.EMOTE_ALLIANCE))
                                {
                                    var meh = character1.Guild.Alliance.Id;
                                    uint cantidad = LootPepitas(looter.Prospecting);
                                    if (cosa == meh)
                                    {
                                        looter.Loot.AddItem(new DroppedItem(14635, (cantidad + 5)));
                                        multiplicator += 0.35f;
                                    }
                                }

                            }
                        }

                        //var pepitas = prims.GetNuggetCount();
                        //var newItem = ItemManager.Instance.TryGetTemplate(14635);
                        //var baseitem = ItemManager.Instance.CreatePlayerItem(character1, newItem, 1);
                        //var itemprism = ItemManager.Instance.CreatePrismItem(baseitem, prims.Id);
                        //prims.Bag.DeleteBag();
                        //prims.Bag.AddItem(itemprism);
                        //prims.Bag.StackItem(itemprism, (int)pepitas + 10);


                    }
                    teamPP = (int)(teamPP * multiplicator);
                    //looter.Loot.Kamas = teamPP > 0 ? (int)(FightFormulas.AdjustDroppedKamas(looter, teamPP, kamas) * multiplicator) : 0;
                    if (team == Winners)
                    {

                        looter.Loot.Kamas = (long)(LootOrbs(looter.Prospecting));
                        foreach (var item in droppers.SelectMany(dropper => dropper.RollLoot(looter)))
                        {
                            Character charId = null;
                            if (character1 != null)
                            {
                                charId = World.Instance.GetCharacters(x => x.Client.IP == character1.Client.IP && x.IsIpDrop).FirstOrDefault();
                            }
                            if (charId != null)
                            {
                                var newLooter = looters.FirstOrDefault(y => y.Id == charId.Id);

                                if (newLooter != null)
                                {
                                    newLooter.Loot.AddItem(item);
                                }
                                else
                                {
                                    looter.Loot.AddItem(item);

                                }

                            }
                            else
                                looter.Loot.AddItem(item);


                        }

                        var personaje = World.Instance.GetCharacter(looter.Id);

                        //Vip -> Drop coins
                        if (personaje is Character)
                        {
                            if (personaje.WorldAccount.Vip == true)
                            {
                                multiplicator += 0.50f;
                                looter.Loot.AddItem(17019);
                            }
                            if (looter.Fight.Map.Area.Id == 48 && personaje.Achievement.FinishedAchievements.Any(x => x.Id == 156) && personaje.Achievement.FinishedAchievements.Any(x => x.Id == 160) && personaje.Achievement.FinishedAchievements.Any(x => x.Id == 138))//Kamas heladas
                                looter.Loot.AddItem(new DroppedItem(11196, 1));

                            if (looter.Fight.Map.Area.Id == 53 && personaje.Achievement.FinishedAchievements.Any(x => x.Id == 1096))//Oricor
                                looter.Loot.AddItem(new DroppedItem(15206, 1));

                            
                            //Reliquias tejossus
                            if (looter.Fight.Map.Id == 61998084)//Reliquia de tejaiis
                                looter.Loot.AddItem(new DroppedItem(11960, 1));
                            if (looter.Fight.Map.Id == 61998082)//Reliquia de tejonon
                                looter.Loot.AddItem(new DroppedItem(11961, 1));
                            if (looter.Fight.Map.Id == 61998338)//Reliquia de un tejebestia
                                looter.Loot.AddItem(new DroppedItem(11963, 1));
                            if (looter.Fight.Map.Id == 61998340)//Reliquia de un pyrojon
                                looter.Loot.AddItem(new DroppedItem(11964, 1));

                        }

                        #region ecailles
                        foreach (var monster in this.DefendersTeam.GetAllFighters())
                        {
                            if (monster is MonsterFighter)
                            {
                                

                                if (personaje is Character)
                                {
                                    var item1 = personaje.Inventory.TryGetItem(ItemManager.Instance.TryGetTemplate(793));
                                    if (!personaje.Inventory.HasItem(ItemManager.Instance.TryGetTemplate(793)))
                                    {
                                        item1 = personaje.Inventory.AddItem(ItemManager.Instance.TryGetTemplate(793), 1);
                                        item1.Effects.Clear();
                                        item1.Invalidate();
                                        personaje.Inventory.RefreshItem(item1);

                                    }
                                    //Termina una dung, retomar = 0
                                    if ((monster as MonsterFighter).Monster.Template.IsBoss)
                                    {
                                        character1.Record.RetomarMazmorra = 0;
                                    }

                                    //Semillas calabaza malditas99
                                    if ((monster as MonsterFighter).Monster.Template.RaceId == 99)
                                        looter.Loot.AddItem(new DroppedItem(13336, 2));

                                    #region escamas esmeralda
                                    var itemEscamas = Singleton<ItemManager>.Instance.TryGetTemplate(15593);
                                    var itemEscamasMision = personaje.Inventory.TryGetItem(itemEscamas);
                                    if ((monster as MonsterFighter).Monster.Template.Id == 377 && !SoulStoneFilled.FIGHT_MAPS.Contains(PlayerTeam.Fight.Map.Id) && itemEscamasMision != null)
                                    {
                                        looter.Loot.AddItem(new DroppedItem(14995, 1));
                                    }
                                    if ((monster as MonsterFighter).Monster.Template.Id == 375 && !SoulStoneFilled.FIGHT_MAPS.Contains(PlayerTeam.Fight.Map.Id) && itemEscamasMision != null)
                                    {
                                        looter.Loot.AddItem(new DroppedItem(14994, 1));
                                    }
                                    if ((monster as MonsterFighter).Monster.Template.Id == 374 && !SoulStoneFilled.FIGHT_MAPS.Contains(PlayerTeam.Fight.Map.Id) && itemEscamasMision != null)
                                    {
                                        looter.Loot.AddItem(new DroppedItem(14993, 1));
                                    }
                                    if ((monster as MonsterFighter).Monster.Template.Id == 3564)//Cuerno sombra
                                    {
                                        looter.Loot.AddItem(new DroppedItem(14928, 4));
                                    }
                                    #endregion

                                    #region LaberintoMinotauroro
                                    if ((monster as MonsterFighter).Monster.Template.Id == 832 && (monster as MonsterFighter).Monster.Group.SubArea.Id == 319)//Reliquia Deminobola
                                    {
                                        looter.Loot.AddItem(new DroppedItem(8306, 1));
                                    }
                                    if ((monster as MonsterFighter).Monster.Template.Id == 831 && (monster as MonsterFighter).Monster.Group.SubArea.Id == 319)//Reliquia Mominotauro
                                    {
                                        looter.Loot.AddItem(new DroppedItem(8305, 1));
                                    }

                                    #endregion

                                    #region SeBuscaDrop
                                    if ((monster as MonsterFighter).Monster.Template.Id == 463)//Beyota
                                    {
                                        looter.Loot.AddItem(new DroppedItem(19392, 1));
                                    }
                                    if ((monster as MonsterFighter).Monster.Template.Id == 460)//Tortacia
                                    {
                                        looter.Loot.AddItem(new DroppedItem(6798, 1));
                                    }
                                    if ((monster as MonsterFighter).Monster.Template.Id == 462)//Ogivol
                                    {
                                        looter.Loot.AddItem(new DroppedItem(6803, 1));
                                    }
                                    if ((monster as MonsterFighter).Monster.Template.Id == 464)//Brumen
                                    {
                                        looter.Loot.AddItem(new DroppedItem(6815, 1));
                                    }
                                    if ((monster as MonsterFighter).Monster.Template.Id == 481)//Qil
                                    {
                                        looter.Loot.AddItem(new DroppedItem(6816, 1));
                                    }
                                    if ((monster as MonsterFighter).Monster.Template.Id == 459)//Noai
                                    {
                                        looter.Loot.AddItem(new DroppedItem(6828, 1));
                                    }
                                    if ((monster as MonsterFighter).Monster.Template.Id == 554)//Marzelo
                                    {
                                        looter.Loot.AddItem(new DroppedItem(6829, 1));
                                    }
                                    if ((monster as MonsterFighter).Monster.Template.Id == 446)//Aermyn
                                    {
                                        looter.Loot.AddItem(new DroppedItem(6833, 1));
                                    }
                                    if ((monster as MonsterFighter).Monster.Template.Id == 552)//Musha
                                    {
                                        looter.Loot.AddItem(new DroppedItem(6835, 1));
                                    }
                                    if ((monster as MonsterFighter).Monster.Template.Id == 550)//Rok
                                    {
                                        looter.Loot.AddItem(new DroppedItem(6836, 1));
                                    }
                                    if ((monster as MonsterFighter).Monster.Template.Id == 555)//Zatoïshwan
                                    {
                                        looter.Loot.AddItem(new DroppedItem(6837, 1));
                                        looter.Loot.AddItem(new DroppedItem(17114, 1));
                                    }
                                    if ((monster as MonsterFighter).Monster.Template.Id == 2901)//Jalalut
                                    {
                                        looter.Loot.AddItem(new DroppedItem(6838, 1));
                                    }
                                    if ((monster as MonsterFighter).Monster.Template.Id == 2902)//Elpin Güino
                                    {
                                        looter.Loot.AddItem(new DroppedItem(6846, 1));
                                    }
                                    if ((monster as MonsterFighter).Monster.Template.Id == 2903)//Katigrú
                                    {
                                        looter.Loot.AddItem(new DroppedItem(6853, 1));
                                    }
                                    if ((monster as MonsterFighter).Monster.Template.Id == 2904)//Phantomeyt
                                    {
                                        looter.Loot.AddItem(new DroppedItem(6858, 1));
                                    }
                                    if ((monster as MonsterFighter).Monster.Template.Id == 2905)//Vengadora Enmascarada
                                    {
                                        looter.Loot.AddItem(new DroppedItem(8576, 1));
                                    }
                                    if ((monster as MonsterFighter).Monster.Template.Id == 2906)//YeCh'Ti
                                    {
                                        looter.Loot.AddItem(new DroppedItem(8588, 1));
                                    }
                                    if ((monster as MonsterFighter).Monster.Template.Id == 2970)//Fuji Gélifux
                                    {
                                        looter.Loot.AddItem(new DroppedItem(8590, 1));
                                    }
                                    if ((monster as MonsterFighter).Monster.Template.Id == 2908)//Dremoan
                                    {
                                        looter.Loot.AddItem(new DroppedItem(8595, 1));
                                        looter.Loot.AddItem(new DroppedItem(15485, 1));
                                    }
                                    if ((monster as MonsterFighter).Monster.Template.Id == 2909)//Tejash
                                    {
                                        looter.Loot.AddItem(new DroppedItem(8603, 1));
                                    }
                                    if ((monster as MonsterFighter).Monster.Template.Id == 2910)//Huini Golosote
                                    {
                                        looter.Loot.AddItem(new DroppedItem(8606, 1));
                                    }
                                    if ((monster as MonsterFighter).Monster.Template.Id == 3400)//doctor Eggob
                                    {
                                        looter.Loot.AddItem(new DroppedItem(8692, 1));
                                    }
                                    if ((monster as MonsterFighter).Monster.Template.Id == 3373)//Golosorak
                                    {
                                        looter.Loot.AddItem(new DroppedItem(8915, 1));
                                    }
                                    if ((monster as MonsterFighter).Monster.Template.Id == 3401)//Mecanimut
                                    {
                                        looter.Loot.AddItem(new DroppedItem(10812, 1));
                                    }
                                    if ((monster as MonsterFighter).Monster.Template.Id == 3413)//Psikopomzopato
                                    {
                                        looter.Loot.AddItem(new DroppedItem(10813, 1));
                                    }
                                    if ((monster as MonsterFighter).Monster.Template.Id == 3403)//Caballero de Hielo
                                    {
                                        looter.Loot.AddItem(new DroppedItem(10814, 1));
                                    }
                                    if ((monster as MonsterFighter).Monster.Template.Id == 3402)//Tentenhuevo
                                    {
                                        looter.Loot.AddItem(new DroppedItem(10815, 1));
                                    }
                                    if ((monster as MonsterFighter).Monster.Template.Id == 3416)//Conde Kontatrás
                                    {
                                        looter.Loot.AddItem(new DroppedItem(10816, 1));
                                    }
                                    if ((monster as MonsterFighter).Monster.Template.Id == 3524)//Ali Grothor
                                    {
                                        looter.Loot.AddItem(new DroppedItem(10817, 1));
                                        looter.Loot.AddItem(new DroppedItem(15541, 1));
                                    }
                                    if ((monster as MonsterFighter).Monster.Template.Id == 3525)//Ceboyix
                                    {
                                        looter.Loot.AddItem(new DroppedItem(12121, 1));
                                    }
                                    if ((monster as MonsterFighter).Monster.Template.Id == 3527)//Boorro
                                    {
                                        looter.Loot.AddItem(new DroppedItem(12122, 1));
                                    }
                                    if ((monster as MonsterFighter).Monster.Template.Id == 3528)//Hedera Venenosa
                                    {
                                        looter.Loot.AddItem(new DroppedItem(12123, 1));
                                    }
                                    if ((monster as MonsterFighter).Monster.Template.Id == 3533)//Hiperescampo
                                    {
                                        looter.Loot.AddItem(new DroppedItem(12277, 1));
                                        looter.Loot.AddItem(new DroppedItem(16008, 1));
                                    }
                                    if ((monster as MonsterFighter).Monster.Template.Id == 3670)//Maxicof
                                    {
                                        looter.Loot.AddItem(new DroppedItem(12191, 1));
                                    }
                                    if ((monster as MonsterFighter).Monster.Template.Id == 3669)//Agrisombra
                                    {
                                        looter.Loot.AddItem(new DroppedItem(12278, 1));
                                    }
                                    if ((monster as MonsterFighter).Monster.Template.Id == 3668)//Dambeldoro
                                    {
                                        looter.Loot.AddItem(new DroppedItem(12279, 1));
                                        looter.Loot.AddItem(new DroppedItem(17117, 1));
                                    }
                                    if ((monster as MonsterFighter).Monster.Template.Id == 3761)//Lonyon Plata
                                    {
                                        looter.Loot.AddItem(new DroppedItem(13054, 1));
                                        looter.Loot.AddItem(new DroppedItem(15551, 1));
                                    }
                                    if ((monster as MonsterFighter).Monster.Template.Id == 3760)//Mosquerosa
                                    {
                                        looter.Loot.AddItem(new DroppedItem(13055, 1));
                                    }
                                    if ((monster as MonsterFighter).Monster.Template.Id == 3762)//Panterrosa
                                    {
                                        looter.Loot.AddItem(new DroppedItem(13056, 1));
                                        looter.Loot.AddItem(new DroppedItem(16010, 1));
                                    }
                                    if ((monster as MonsterFighter).Monster.Template.Id == 3845)//Morblok
                                    {
                                        looter.Loot.AddItem(new DroppedItem(13192, 1));
                                    }
                                    if ((monster as MonsterFighter).Monster.Template.Id == 3848)//Uhno
                                    {
                                        looter.Loot.AddItem(new DroppedItem(13195, 1));
                                    }
                                    if ((monster as MonsterFighter).Monster.Template.Id == 3851)//Ciguña
                                    {
                                        looter.Loot.AddItem(new DroppedItem(13266, 1));
                                    }
                                    if ((monster as MonsterFighter).Monster.Template.Id == 4014)//Tirana la Terrible
                                    {
                                        looter.Loot.AddItem(new DroppedItem(13267, 1));
                                    }
                                    if ((monster as MonsterFighter).Monster.Template.Id == 4027)//Naganita
                                    {
                                        looter.Loot.AddItem(new DroppedItem(13268, 1));
                                    }
                                    if ((monster as MonsterFighter).Monster.Template.Id == 4041)//Nenúflor Deloto
                                    {
                                        looter.Loot.AddItem(new DroppedItem(13269, 1));
                                    }
                                    if ((monster as MonsterFighter).Monster.Template.Id == 4015)//Gemalo
                                    {
                                        looter.Loot.AddItem(new DroppedItem(14578, 1));
                                    }
                                    if ((monster as MonsterFighter).Monster.Template.Id == 4017)//Nataka Discodoro
                                    {
                                        looter.Loot.AddItem(new DroppedItem(14959, 1));
                                        looter.Loot.AddItem(new DroppedItem(17115, 1));
                                    }
                                    if ((monster as MonsterFighter).Monster.Template.Id == 4016)//Guerrero de Kaos
                                    {
                                        looter.Loot.AddItem(new DroppedItem(14960, 1));
                                        looter.Loot.AddItem(new DroppedItem(17116, 1));
                                    }
                                    if ((monster as MonsterFighter).Monster.Template.Id == 4028)//Gran Kongoku
                                    {
                                        looter.Loot.AddItem(new DroppedItem(14969, 1));
                                        looter.Loot.AddItem(new DroppedItem(17118, 1));
                                    }
                                    if ((monster as MonsterFighter).Monster.Template.Id == 4532)//Crustassius Cley
                                    {
                                        looter.Loot.AddItem(new DroppedItem(15004, 1));
                                    }
                                    if ((monster as MonsterFighter).Monster.Template.Id == 4506)//Burbudazred
                                    {
                                        looter.Loot.AddItem(new DroppedItem(15005, 1));
                                    }
                                    if ((monster as MonsterFighter).Monster.Template.Id == 4507)//Takomako
                                    {
                                        looter.Loot.AddItem(new DroppedItem(15006, 1));
                                    }
                                    if ((monster as MonsterFighter).Monster.Template.Id == 4618)//Turnado
                                    {
                                        looter.Loot.AddItem(new DroppedItem(15007, 1));
                                    }
                                    if ((monster as MonsterFighter).Monster.Template.Id == 4622)//Juanca Mole
                                    {
                                        looter.Loot.AddItem(new DroppedItem(15400, 1));
                                    }
                                    if ((monster as MonsterFighter).Monster.Template.Id == 4737)//Sa'Kais'Hulud
                                    {
                                        looter.Loot.AddItem(new DroppedItem(15417, 1));
                                    }
                                    if ((monster as MonsterFighter).Monster.Template.Id == 4734)//Jepricornio
                                    {
                                        looter.Loot.AddItem(new DroppedItem(15418, 1));
                                    }
                                    if ((monster as MonsterFighter).Monster.Template.Id == 4240)//Kosakepika
                                    {
                                        looter.Loot.AddItem(new DroppedItem(15419, 1));
                                    }
                                    if ((monster as MonsterFighter).Monster.Template.Id == 4814)//Simbadás
                                    {
                                        looter.Loot.AddItem(new DroppedItem(15420, 1));
                                    }
                                    if ((monster as MonsterFighter).Monster.Template.Id == 4815)//Carlita de La Guerfeld
                                    {
                                        looter.Loot.AddItem(new DroppedItem(15421, 1));
                                    }
                                    if ((monster as MonsterFighter).Monster.Template.Id == 4816)//Coldbru'Selas
                                    {
                                        looter.Loot.AddItem(new DroppedItem(15423, 1));
                                    }
                                    if ((monster as MonsterFighter).Monster.Template.Id == 4834)//Predagob
                                    {
                                        looter.Loot.AddItem(new DroppedItem(15424, 1));
                                    }
                                    if ((monster as MonsterFighter).Monster.Template.Id == 4266 && personaje.AlignmentSide != AlignmentSideEnum.ALIGNMENT_NEUTRAL)//Sam Sagás
                                    {
                                        looter.Loot.AddItem(new DroppedItem(14970, 1));
                                    }
                                    if ((monster as MonsterFighter).Monster.Template.Id == 4268 && personaje.AlignmentSide != AlignmentSideEnum.ALIGNMENT_NEUTRAL)//Maestro Plomo
                                    {
                                        looter.Loot.AddItem(new DroppedItem(14971, 1));
                                    }
                                    if ((monster as MonsterFighter).Monster.Template.Id == 4270 && personaje.AlignmentSide != AlignmentSideEnum.ALIGNMENT_NEUTRAL)//Tocap Elotas
                                    {
                                        looter.Loot.AddItem(new DroppedItem(14972, 1));
                                    }
                                    if ((monster as MonsterFighter).Monster.Template.Id == 4272 && personaje.AlignmentSide != AlignmentSideEnum.ALIGNMENT_NEUTRAL)//Buz Beib
                                    {
                                        looter.Loot.AddItem(new DroppedItem(14973, 1));
                                    }
                                    if ((monster as MonsterFighter).Monster.Template.Id == 4273 && personaje.AlignmentSide != AlignmentSideEnum.ALIGNMENT_NEUTRAL)//Nono el Wobot
                                    {
                                        looter.Loot.AddItem(new DroppedItem(14974, 1));
                                    }
                                    if ((monster as MonsterFighter).Monster.Template.Id == 4274 && personaje.AlignmentSide != AlignmentSideEnum.ALIGNMENT_NEUTRAL)//Armada la Invencible
                                    {
                                        looter.Loot.AddItem(new DroppedItem(14978, 1));
                                    }
                                    if ((monster as MonsterFighter).Monster.Template.Id == 4275 && personaje.AlignmentSide != AlignmentSideEnum.ALIGNMENT_NEUTRAL)//Dragopavona
                                    {
                                        looter.Loot.AddItem(new DroppedItem(14979, 1));
                                    }
                                    if ((monster as MonsterFighter).Monster.Template.Id == 4276 && personaje.AlignmentSide != AlignmentSideEnum.ALIGNMENT_NEUTRAL)//Queba Sura
                                    {
                                        looter.Loot.AddItem(new DroppedItem(14980, 1));
                                    }
                                    if ((monster as MonsterFighter).Monster.Template.Id == 4277 && personaje.AlignmentSide != AlignmentSideEnum.ALIGNMENT_NEUTRAL)//Príncipe Embaucador
                                    {
                                        looter.Loot.AddItem(new DroppedItem(14981, 1));
                                    }
                                    if ((monster as MonsterFighter).Monster.Template.Id == 5073 && personaje.AlignmentSide != AlignmentSideEnum.ALIGNMENT_NEUTRAL)//Goblechaun
                                    {
                                        looter.Loot.AddItem(new DroppedItem(14982, 1));
                                    }
                                    if ((monster as MonsterFighter).Monster.Template.Id == 5074 && personaje.AlignmentSide != AlignmentSideEnum.ALIGNMENT_NEUTRAL)//Vakakiwíe
                                    {
                                        looter.Loot.AddItem(new DroppedItem(14983, 1));
                                    }
                                    if ((monster as MonsterFighter).Monster.Template.Id == 4949 && personaje.AlignmentSide != AlignmentSideEnum.ALIGNMENT_NEUTRAL)//Yerar Depandur
                                    {
                                        looter.Loot.AddItem(new DroppedItem(14990, 1));
                                    }
                                    if ((monster as MonsterFighter).Monster.Template.Id == 5076 && personaje.AlignmentSide != AlignmentSideEnum.ALIGNMENT_NEUTRAL)//Darma
                                    {
                                        looter.Loot.AddItem(new DroppedItem(14991, 1));
                                    }
                                    if ((monster as MonsterFighter).Monster.Template.Id == 5077 && personaje.AlignmentSide != AlignmentSideEnum.ALIGNMENT_NEUTRAL)//Balimogli
                                    {
                                        looter.Loot.AddItem(new DroppedItem(14992, 1));
                                    }
                                    if ((monster as MonsterFighter).Monster.Template.Id == 5081 && personaje.AlignmentSide != AlignmentSideEnum.ALIGNMENT_NEUTRAL)//Glangaf el Gris
                                    {
                                        looter.Loot.AddItem(new DroppedItem(14999, 1));
                                    }
                                    if ((monster as MonsterFighter).Monster.Template.Id == 5083 && personaje.AlignmentSide != AlignmentSideEnum.ALIGNMENT_NEUTRAL)//Cáspar
                                    {
                                        looter.Loot.AddItem(new DroppedItem(15000, 1));
                                    }
                                    if ((monster as MonsterFighter).Monster.Template.Id == 5084 && personaje.AlignmentSide != AlignmentSideEnum.ALIGNMENT_NEUTRAL)//Carter Pillar
                                    {
                                        looter.Loot.AddItem(new DroppedItem(15001, 1));
                                    }
                                    if ((monster as MonsterFighter).Monster.Template.Id == 5086 && personaje.AlignmentSide != AlignmentSideEnum.ALIGNMENT_NEUTRAL)//Sin Rostro
                                    {
                                        looter.Loot.AddItem(new DroppedItem(15002, 1));
                                    }
                                    if ((monster as MonsterFighter).Monster.Template.Id == 4953 && personaje.AlignmentSide != AlignmentSideEnum.ALIGNMENT_NEUTRAL)//Ultratumbarrayder
                                    {
                                        looter.Loot.AddItem(new DroppedItem(15003, 1));
                                    }

                                    #endregion

                                    #region Misiones Dofus

                                    #region Dofus Esmeralda
                                    var itemInicioQuest = Singleton<ItemManager>.Instance.TryGetTemplate(15596);
                                    var itemUnoEsmeralda = personaje.Inventory.TryGetItem(itemInicioQuest);
                                    if ((monster as MonsterFighter).Monster.Template.Id == 3578 && itemUnoEsmeralda != null && !SoulStoneFilled.FIGHT_MAPS.Contains(PlayerTeam.Fight.Map.Id))
                                    {
                                        looter.Loot.AddItem(new DroppedItem(15597, 1));
                                        personaje.SendServerMessage("Has conseguido la espada de Dark Vlad.");
                                    }
                                    if ((monster as MonsterFighter).Monster.Template.Id == 3578 && !SoulStoneFilled.FIGHT_MAPS.Contains(PlayerTeam.Fight.Map.Id))
                                    {
                                        personaje.AddExperience(personaje.Level * 3500);
                                    }

                                    var itemInicio = Singleton<ItemManager>.Instance.TryGetTemplate(15425);
                                    var item4 = personaje.Inventory.TryGetItem(itemInicio);
                                    if (looter.Fight.Map.Area.Id == 6 && !SoulStoneFilled.FIGHT_MAPS.Contains(PlayerTeam.Fight.Map.Id) && item4 != null)
                                    {
                                        looter.Loot.AddItem(new DroppedItem(15427, 1));
                                    }

                                    var itemMuestra = Singleton<ItemManager>.Instance.TryGetTemplate(15426);
                                    var itemMuestraAA = personaje.Inventory.TryGetItem(itemMuestra);
                                    if ((monster as MonsterFighter).Monster.Template.Id == 173 && !SoulStoneFilled.FIGHT_MAPS.Contains(PlayerTeam.Fight.Map.Id) && itemMuestraAA != null)
                                    {
                                        looter.Loot.AddItem(new DroppedItem(15443, 1));
                                        personaje.SendServerMessage("Has conseguido la muestra del Abráknido Ancestral.");
                                    }
                                    if ((monster as MonsterFighter).Monster.Template.Id == 3996 && !SoulStoneFilled.FIGHT_MAPS.Contains(PlayerTeam.Fight.Map.Id) && itemMuestraAA != null)
                                    {
                                        looter.Loot.AddItem(new DroppedItem(15578, 1));
                                        personaje.SendServerMessage("Has conseguido la muestra de Lasoberaña.");
                                    }

                                    var itemMuestraMaxilubu = Singleton<ItemManager>.Instance.TryGetTemplate(15594);
                                    var itemMuestra2 = personaje.Inventory.TryGetItem(itemMuestraMaxilubu);
                                    if ((monster as MonsterFighter).Monster.Template.Id == 232 && !SoulStoneFilled.FIGHT_MAPS.Contains(PlayerTeam.Fight.Map.Id) && itemMuestra2 != null)
                                    {
                                        looter.Loot.AddItem(new DroppedItem(15595, 1));
                                        personaje.SendServerMessage("Has conseguido la muestra del Maxilubu.");
                                    }

                                    #endregion

                                    #region Dofawa

                                    var itemDofawa = Singleton<ItemManager>.Instance.TryGetTemplate(15602);
                                    var itemDofawaQuest = personaje.Inventory.TryGetItem(itemDofawa);
                                    if ((monster as MonsterFighter).Monster.Template.Id == 4051 && !SoulStoneFilled.FIGHT_MAPS.Contains(PlayerTeam.Fight.Map.Id) && itemDofawaQuest != null)
                                    {
                                        looter.Loot.AddItem(new DroppedItem(15603, 1));
                                        personaje.SendServerMessage("Has conseguido el alma de Chadalid.");
                                    }

                                    #endregion

                                    #region Dotruz

                                    var itemDotruz = Singleton<ItemManager>.Instance.TryGetTemplate(18264);
                                    var itemDotruzQuest = personaje.Inventory.TryGetItem(itemDotruz);
                                    if ((monster as MonsterFighter).Monster.Template.Id == 3618 && !SoulStoneFilled.FIGHT_MAPS.Contains(PlayerTeam.Fight.Map.Id) && itemDotruzQuest != null)
                                    {
                                        looter.Loot.AddItem(new DroppedItem(15317, 1));
                                    }

                                    #endregion

                                    #region Purpura

                                    var itemPurpura = Singleton<ItemManager>.Instance.TryGetTemplate(18246);
                                    var itemPurpuraQuest = personaje.Inventory.TryGetItem(itemPurpura);
                                    if ((monster as MonsterFighter).Monster.Template.Id == 121 && !SoulStoneFilled.FIGHT_MAPS.Contains(PlayerTeam.Fight.Map.Id) && itemPurpuraQuest != null)
                                    {
                                        looter.Loot.AddItem(new DroppedItem(18247, 1));
                                        personaje.SendServerMessage("Has conseguido un cuerno envuelto en llamas.");
                                    }
                                    if ((monster as MonsterFighter).Monster.Template.Id == 289 && !SoulStoneFilled.FIGHT_MAPS.Contains(PlayerTeam.Fight.Map.Id) && itemPurpuraQuest != null)
                                    {
                                        looter.Loot.AddItem(new DroppedItem(18248, 1));
                                        personaje.SendServerMessage("Has conseguido la Guadaña de la Muerte.");
                                    }

                                    var itemPurpura2 = Singleton<ItemManager>.Instance.TryGetTemplate(18250);
                                    var itemPurpuraQuest2 = personaje.Inventory.TryGetItem(itemPurpura2);
                                    if ((monster as MonsterFighter).Monster.Template.Id == 2854 && !SoulStoneFilled.FIGHT_MAPS.Contains(PlayerTeam.Fight.Map.Id) && itemPurpuraQuest2 != null)
                                    {
                                        looter.Loot.AddItem(new DroppedItem(18251, 1));
                                        personaje.SendServerMessage("Has conseguido un cuerno congelado.");
                                    }
                                    if ((monster as MonsterFighter).Monster.Template.Id == 3753 && !SoulStoneFilled.FIGHT_MAPS.Contains(PlayerTeam.Fight.Map.Id) && itemPurpuraQuest2 != null)
                                    {
                                        looter.Loot.AddItem(new DroppedItem(18252, 1));
                                        personaje.SendServerMessage("Has conseguido la Mascara de Lucha.");
                                    }

                                    var itemPurpura3 = Singleton<ItemManager>.Instance.TryGetTemplate(18254);
                                    var itemPurpuraQuest3 = personaje.Inventory.TryGetItem(itemPurpura3);
                                    if ((monster as MonsterFighter).Monster.Template.Id == 1188 && !SoulStoneFilled.FIGHT_MAPS.Contains(PlayerTeam.Fight.Map.Id) && itemPurpuraQuest3 != null)
                                    {
                                        looter.Loot.AddItem(new DroppedItem(18258, 1));
                                        personaje.SendServerMessage("Has conseguido un pedazito multicolor.");
                                    }

                                    var itemPurpura4 = Singleton<ItemManager>.Instance.TryGetTemplate(18262);
                                    var itemPurpuraQuest4 = personaje.Inventory.TryGetItem(itemPurpura4);
                                    if ((monster as MonsterFighter).Monster.Template.Id == 827 && !SoulStoneFilled.FIGHT_MAPS.Contains(PlayerTeam.Fight.Map.Id) && itemPurpuraQuest4 != null)
                                    {
                                        looter.Loot.AddItem(new DroppedItem(18263, 1));
                                        personaje.SendServerMessage("Has conseguido el corazón del verdadero guardián.");
                                    }

                                    #endregion

                                    #region DofusOcre
                                    var itemOcre = Singleton<ItemManager>.Instance.TryGetTemplate(18238);
                                    var itemOcreQuest = personaje.Inventory.TryGetItem(itemOcre);
                                    if ((monster as MonsterFighter).Monster.Template.Id == 1027 && !SoulStoneFilled.FIGHT_MAPS.Contains(PlayerTeam.Fight.Map.Id) && itemOcreQuest != null)
                                    {
                                        looter.Loot.AddItem(new DroppedItem(18232, 1));
                                        personaje.SendServerMessage("Has conseguido la reliquia del Coralador Magistral.");
                                    }
                                    if ((monster as MonsterFighter).Monster.Template.Id == 1051 && !SoulStoneFilled.FIGHT_MAPS.Contains(PlayerTeam.Fight.Map.Id) && itemOcreQuest != null)
                                    {
                                        looter.Loot.AddItem(new DroppedItem(18234, 1));
                                        personaje.SendServerMessage("Has conseguido la reliquia de Gurlo el Terrible.");
                                    }
                                    if ((monster as MonsterFighter).Monster.Template.Id == 1071 && !SoulStoneFilled.FIGHT_MAPS.Contains(PlayerTeam.Fight.Map.Id) && itemOcreQuest != null)
                                    {
                                        looter.Loot.AddItem(new DroppedItem(18235, 1));
                                        personaje.SendServerMessage("Has conseguido la reliquia de Silf el Rasgabola Mayor.");
                                    }
                                    if ((monster as MonsterFighter).Monster.Template.Id == 1086 && !SoulStoneFilled.FIGHT_MAPS.Contains(PlayerTeam.Fight.Map.Id) && itemOcreQuest != null)
                                    {
                                        looter.Loot.AddItem(new DroppedItem(18236, 1));
                                        personaje.SendServerMessage("Has conseguido la reliquia de Tynril.");
                                    }
                                    if ((monster as MonsterFighter).Monster.Template.Id == 1045 && !SoulStoneFilled.FIGHT_MAPS.Contains(PlayerTeam.Fight.Map.Id) && itemOcreQuest != null)
                                    {
                                        looter.Loot.AddItem(new DroppedItem(18237, 1));
                                        personaje.SendServerMessage("Has conseguido la reliquia del Kimbo.");
                                    }

                                    var itemOcreDos = Singleton<ItemManager>.Instance.TryGetTemplate(18239);
                                    var itemOcreQuestDos = personaje.Inventory.TryGetItem(itemOcreDos);
                                    if ((monster as MonsterFighter).Monster.Template.RaceId == 78 && itemOcreQuestDos != null)
                                    {
                                        looter.Loot.AddItem(new DroppedItem(18240, 1));
                                        personaje.SendServerMessage("Has embotellado el Alma de un monstruo del Origen.");
                                    }


                                    var itemOcreTres = Singleton<ItemManager>.Instance.TryGetTemplate(18244);
                                    var itemOcreQuestTres = personaje.Inventory.TryGetItem(itemOcreTres);
                                    if ((monster as MonsterFighter).Monster.Template.Id == 423 && !SoulStoneFilled.FIGHT_MAPS.Contains(PlayerTeam.Fight.Map.Id) && itemOcreQuestTres != null)
                                    {
                                        looter.Loot.AddItem(new DroppedItem(9720, 1));
                                        personaje.SendServerMessage("Has obtenido el Alma del Kralamar.");
                                    }

                                    #endregion

                                    #region Dofus Plateado

                                    var itemPlateado = Singleton<ItemManager>.Instance.TryGetTemplate(16138);
                                    var itemPlateadoQuest = personaje.Inventory.TryGetItem(itemPlateado);
                                    if ((monster as MonsterFighter).Monster.Template.Id == 5270 && !SoulStoneFilled.FIGHT_MAPS.Contains(PlayerTeam.Fight.Map.Id) && itemPlateadoQuest != null)
                                    {
                                        looter.Loot.AddItem(new DroppedItem(16888, 1));
                                        personaje.SendServerMessage("Has conseguido carne de Oso Asesino.");
                                    }

                                    #endregion

                                    #region Dofus Zanahowia

                                    var itemZanaMatarWabbits = Singleton<ItemManager>.Instance.TryGetTemplate(16892);
                                    var itemZanaMatarWabbitsQuest = personaje.Inventory.TryGetItem(itemZanaMatarWabbits);
                                    if ((monster as MonsterFighter).Monster.Template.Id == 64 && !SoulStoneFilled.FIGHT_MAPS.Contains(PlayerTeam.Fight.Map.Id) && itemZanaMatarWabbitsQuest != null)
                                    {
                                        looter.Loot.AddItem(new DroppedItem(16893, 1));
                                        personaje.SendServerMessage("Has contribuido en la defensa.");
                                    }
                                    if ((monster as MonsterFighter).Monster.Template.Id == 65 && !SoulStoneFilled.FIGHT_MAPS.Contains(PlayerTeam.Fight.Map.Id) && itemZanaMatarWabbitsQuest != null)
                                    {
                                        looter.Loot.AddItem(new DroppedItem(16893, 1));
                                        personaje.SendServerMessage("Has contribuido en la defensa.");
                                    }
                                    if ((monster as MonsterFighter).Monster.Template.Id == 96 && !SoulStoneFilled.FIGHT_MAPS.Contains(PlayerTeam.Fight.Map.Id) && itemZanaMatarWabbitsQuest != null)
                                    {
                                        looter.Loot.AddItem(new DroppedItem(16893, 1));
                                        personaje.SendServerMessage("Has contribuido en la defensa.");
                                    }
                                    if ((monster as MonsterFighter).Monster.Template.Id == 72 && !SoulStoneFilled.FIGHT_MAPS.Contains(PlayerTeam.Fight.Map.Id) && itemZanaMatarWabbitsQuest != null)
                                    {
                                        looter.Loot.AddItem(new DroppedItem(16893, 1));
                                        personaje.SendServerMessage("Has contribuido en la defensa.");
                                    }
                                    if ((monster as MonsterFighter).Monster.Template.Id == 68 && !SoulStoneFilled.FIGHT_MAPS.Contains(PlayerTeam.Fight.Map.Id) && itemZanaMatarWabbitsQuest != null)
                                    {
                                        looter.Loot.AddItem(new DroppedItem(16893, 1));
                                        personaje.SendServerMessage("Has contribuido en la defensa.");
                                    }

                                    var itemZanaMatarCapitanes = Singleton<ItemManager>.Instance.TryGetTemplate(16895);
                                    var itemZanaMatarCapitanesQuest = personaje.Inventory.TryGetItem(itemZanaMatarCapitanes);
                                    if (looter.Fight.Map.Id == 99746308 && !SoulStoneFilled.FIGHT_MAPS.Contains(PlayerTeam.Fight.Map.Id) && itemZanaMatarCapitanesQuest != null)
                                    {
                                        looter.Loot.AddItem(new DroppedItem(16897, 1));
                                        personaje.SendServerMessage("Has eliminado al segundo.");
                                    }
                                    if (looter.Fight.Map.Id == 99614722 && !SoulStoneFilled.FIGHT_MAPS.Contains(PlayerTeam.Fight.Map.Id) && itemZanaMatarCapitanesQuest != null)
                                    {
                                        looter.Loot.AddItem(new DroppedItem(16898, 1));
                                        personaje.SendServerMessage("Has eliminado al tercero.");
                                    }
                                    if (looter.Fight.Map.Id == 99746817 && !SoulStoneFilled.FIGHT_MAPS.Contains(PlayerTeam.Fight.Map.Id) && itemZanaMatarCapitanesQuest != null)
                                    {
                                        looter.Loot.AddItem(new DroppedItem(16899, 1));
                                        personaje.SendServerMessage("Has eliminado al cuarto.");
                                    }
                                    if (looter.Fight.Map.Id == 99615745 && !SoulStoneFilled.FIGHT_MAPS.Contains(PlayerTeam.Fight.Map.Id) && itemZanaMatarCapitanesQuest != null)
                                    {
                                        looter.Loot.AddItem(new DroppedItem(16896, 1));
                                        personaje.SendServerMessage("Has eliminado al primero.");
                                    }

                                    var itemZanaMatarWabbitGM = Singleton<ItemManager>.Instance.TryGetTemplate(16902);
                                    var itemZanaMatarWabbitGMQuest = personaje.Inventory.TryGetItem(itemZanaMatarWabbitGM);
                                    if ((monster as MonsterFighter).Monster.Template.Id == 182 && !SoulStoneFilled.FIGHT_MAPS.Contains(PlayerTeam.Fight.Map.Id) && itemZanaMatarWabbitGMQuest != null)
                                    {
                                        looter.Loot.AddItem(new DroppedItem(16903, 1));
                                        personaje.SendServerMessage("Has conseguido la reliquia del WabbitGM.");
                                    }

                                    var itemZanaMatarPrincipes = Singleton<ItemManager>.Instance.TryGetTemplate(17001);
                                    var itemZanaMatarPrincipesQuest = personaje.Inventory.TryGetItem(itemZanaMatarPrincipes);
                                    if ((monster as MonsterFighter).Monster.Template.Id == 179 && !SoulStoneFilled.FIGHT_MAPS.Contains(PlayerTeam.Fight.Map.Id) && itemZanaMatarPrincipesQuest != null)
                                    {
                                        looter.Loot.AddItem(new DroppedItem(16946, 1));
                                        personaje.SendServerMessage("Has conseguido huesos.");
                                    }
                                    if ((monster as MonsterFighter).Monster.Template.Id == 3469 && !SoulStoneFilled.FIGHT_MAPS.Contains(PlayerTeam.Fight.Map.Id) && itemZanaMatarPrincipesQuest != null)
                                    {
                                        looter.Loot.AddItem(new DroppedItem(16946, 1));
                                        personaje.SendServerMessage("Has conseguido huesos.");
                                    }

                                    if ((monster as MonsterFighter).Monster.Template.Id == 3470 && !SoulStoneFilled.FIGHT_MAPS.Contains(PlayerTeam.Fight.Map.Id) && itemZanaMatarPrincipesQuest != null)
                                    {
                                        looter.Loot.AddItem(new DroppedItem(16947, 1));
                                        personaje.SendServerMessage("Has conseguido quitarle la vela de la nigromancia a los pwincipes.");
                                    }

                                    var itemZanaMatarJefes = Singleton<ItemManager>.Instance.TryGetTemplate(17035);
                                    var itemZanaMatarJefesQuest = personaje.Inventory.TryGetItem(itemZanaMatarJefes);
                                    if ((monster as MonsterFighter).Monster.Template.Id == 180 && !SoulStoneFilled.FIGHT_MAPS.Contains(PlayerTeam.Fight.Map.Id) && itemZanaMatarJefesQuest != null)
                                    {
                                        looter.Loot.AddItem(new DroppedItem(17062, 1));
                                        personaje.SendServerMessage("Has conseguido la cabeza del Wey Wabbit.");
                                    }
                                    if ((monster as MonsterFighter).Monster.Template.Id == 3460 && !SoulStoneFilled.FIGHT_MAPS.Contains(PlayerTeam.Fight.Map.Id) && itemZanaMatarJefesQuest != null)
                                    {
                                        looter.Loot.AddItem(new DroppedItem(17074, 1));
                                        personaje.SendServerMessage("Has conseguido la cabeza del Wey Wobbot.");
                                    }
                                    #endregion

                                    #region Turquesa

                                    var itemTurquesa = Singleton<ItemManager>.Instance.TryGetTemplate(18269);
                                    var itemTurquesaQuest = personaje.Inventory.TryGetItem(itemTurquesa);
                                    if ((monster as MonsterFighter).Monster.Template.Id == 113 && !SoulStoneFilled.FIGHT_MAPS.Contains(PlayerTeam.Fight.Map.Id) && itemTurquesaQuest != null)
                                    {
                                        looter.Loot.AddItem(new DroppedItem(18271, 1));
                                        personaje.SendServerMessage("Has conseguido el castigo del Dragocerdo.");
                                    }
                                    if ((monster as MonsterFighter).Monster.Template.Id == 257 && !SoulStoneFilled.FIGHT_MAPS.Contains(PlayerTeam.Fight.Map.Id) && itemTurquesaQuest != null)
                                    {
                                        looter.Loot.AddItem(new DroppedItem(18272, 1));
                                        personaje.SendServerMessage("Has conseguido el castigo del Roble Blando.");
                                    }

                                    var itemTurquesa2 = Singleton<ItemManager>.Instance.TryGetTemplate(18292);
                                    var itemTurquesa2Quest = personaje.Inventory.TryGetItem(itemTurquesa2);
                                    if ((monster as MonsterFighter).Monster.Template.Id == 4175 && !SoulStoneFilled.FIGHT_MAPS.Contains(PlayerTeam.Fight.Map.Id) && itemTurquesa2Quest != null)
                                    {
                                        looter.Loot.AddItem(new DroppedItem(18273, 1));
                                        personaje.SendServerMessage("Has conseguido pasar la prueba.");
                                    }
                                    if ((monster as MonsterFighter).Monster.Template.Id == 4200 && !SoulStoneFilled.FIGHT_MAPS.Contains(PlayerTeam.Fight.Map.Id) && itemTurquesa2Quest != null)
                                    {
                                        looter.Loot.AddItem(new DroppedItem(18274, 1));
                                        personaje.SendServerMessage("Has conseguido pasar la prueba.");
                                    }

                                    var itemTurquesa3 = Singleton<ItemManager>.Instance.TryGetTemplate(18275);
                                    var itemTurquesa3Quest = personaje.Inventory.TryGetItem(itemTurquesa3);
                                    if ((monster as MonsterFighter).Monster.Template.Id == 605 && !SoulStoneFilled.FIGHT_MAPS.Contains(PlayerTeam.Fight.Map.Id) && itemTurquesa3Quest != null)
                                    {
                                        looter.Loot.AddItem(new DroppedItem(18276, 1));
                                        personaje.SendServerMessage("Has conseguido castigar al PekiPeki.");
                                    }
                                    if ((monster as MonsterFighter).Monster.Template.Id == 1086 && !SoulStoneFilled.FIGHT_MAPS.Contains(PlayerTeam.Fight.Map.Id) && itemTurquesa3Quest != null)
                                    {
                                        looter.Loot.AddItem(new DroppedItem(18277, 1));
                                        personaje.SendServerMessage("Has conseguido castigar al Tynril.");
                                    }
                                    if ((monster as MonsterFighter).Monster.Template.Id == 2848 && !SoulStoneFilled.FIGHT_MAPS.Contains(PlayerTeam.Fight.Map.Id) && itemTurquesa3Quest != null)
                                    {
                                        looter.Loot.AddItem(new DroppedItem(18278, 1));
                                        personaje.SendServerMessage("Has conseguido castigar al Morsagüino Real.");
                                    }


                                    var itemTurquesa4 = Singleton<ItemManager>.Instance.TryGetTemplate(18280);
                                    var itemTurquesa4Quest = personaje.Inventory.TryGetItem(itemTurquesa4);
                                    if ((monster as MonsterFighter).Monster.Template.Id == 943 && !SoulStoneFilled.FIGHT_MAPS.Contains(PlayerTeam.Fight.Map.Id) && itemTurquesa4Quest != null)
                                    {
                                        looter.Loot.AddItem(new DroppedItem(18281, 1));
                                        personaje.SendServerMessage("Has conseguido castigar a Sfinter Cell.");
                                    }
                                    if ((monster as MonsterFighter).Monster.Template.Id == 2877 && !SoulStoneFilled.FIGHT_MAPS.Contains(PlayerTeam.Fight.Map.Id) && itemTurquesa4Quest != null)
                                    {
                                        looter.Loot.AddItem(new DroppedItem(18282, 1));
                                        personaje.SendServerMessage("Has conseguido castigar a Ben el Ripata.");
                                    }
                                    if ((monster as MonsterFighter).Monster.Template.Id == 1045 && !SoulStoneFilled.FIGHT_MAPS.Contains(PlayerTeam.Fight.Map.Id) && itemTurquesa4Quest != null)
                                    {
                                        looter.Loot.AddItem(new DroppedItem(18283, 1));
                                        personaje.SendServerMessage("Has conseguido castigar a Kimbo.");
                                    }
                                    if ((monster as MonsterFighter).Monster.Template.Id == 2924 && !SoulStoneFilled.FIGHT_MAPS.Contains(PlayerTeam.Fight.Map.Id) && itemTurquesa4Quest != null)
                                    {
                                        looter.Loot.AddItem(new DroppedItem(18284, 1));
                                        personaje.SendServerMessage("Has conseguido castigar a Obsidiantre.");
                                    }

                                    var itemTurquesa5 = Singleton<ItemManager>.Instance.TryGetTemplate(18285);
                                    var itemTurquesa5Quest = personaje.Inventory.TryGetItem(itemTurquesa5);
                                    if ((monster as MonsterFighter).Monster.Template.Id == 3651 && !SoulStoneFilled.FIGHT_MAPS.Contains(PlayerTeam.Fight.Map.Id) && itemTurquesa5Quest != null)
                                    {
                                        looter.Loot.AddItem(new DroppedItem(18286, 1));
                                        personaje.SendServerMessage("Has conseguido castigar a Perfósil.");
                                    }
                                    if ((monster as MonsterFighter).Monster.Template.Id == 3556 && !SoulStoneFilled.FIGHT_MAPS.Contains(PlayerTeam.Fight.Map.Id) && itemTurquesa5Quest != null)
                                    {
                                        looter.Loot.AddItem(new DroppedItem(18287, 1));
                                        personaje.SendServerMessage("Has conseguido castigar a Kanígrula.");
                                    }
                                    if ((monster as MonsterFighter).Monster.Template.Id == 2968 && !SoulStoneFilled.FIGHT_MAPS.Contains(PlayerTeam.Fight.Map.Id) && itemTurquesa5Quest != null)
                                    {
                                        looter.Loot.AddItem(new DroppedItem(18288, 1));
                                        personaje.SendServerMessage("Has conseguido castigar a Cil.");
                                    }


                                    var itemTurquesa6 = Singleton<ItemManager>.Instance.TryGetTemplate(18289);
                                    var itemTurquesa6Quest = personaje.Inventory.TryGetItem(itemTurquesa6);
                                    if ((monster as MonsterFighter).Monster.Template.Id == 4065 && !SoulStoneFilled.FIGHT_MAPS.Contains(PlayerTeam.Fight.Map.Id) && itemTurquesa6Quest != null)
                                    {
                                        looter.Loot.AddItem(new DroppedItem(18291, 1));
                                        personaje.SendServerMessage("Has conseguido la prueba de los seguidores de Bolgrot.");
                                    }

                                    #endregion

                                    #endregion

                                    #region Evento GrasmeraGrozilla
                                    //Nivel 200
                                    if ((monster as MonsterFighter).Monster.Template.Id == 3143 && !SoulStoneFilled.FIGHT_MAPS.Contains(PlayerTeam.Fight.Map.Id))
                                    {
                                        looter.Loot.AddItem(new DroppedItem(15588, 20));
                                        if (!personaje.HasEmote((EmotesEnum.EMOTE_SALUT_VULKAIN)))
                                            personaje.AddEmote(EmotesEnum.EMOTE_SALUT_VULKAIN);

                                        if (!personaje.HasEmote((EmotesEnum.EMOTE_SUPER_HEROS)))
                                            personaje.AddEmote(EmotesEnum.EMOTE_SUPER_HEROS);
                                    }

                                    //Nivel 150
                                    if ((monster as MonsterFighter).Monster.Template.Id == 3296 && !SoulStoneFilled.FIGHT_MAPS.Contains(PlayerTeam.Fight.Map.Id))
                                    {
                                        looter.Loot.AddItem(new DroppedItem(15588, 10));
                                        if (!personaje.HasEmote((EmotesEnum.EMOTE_SALUT_VULKAIN)))
                                            personaje.AddEmote(EmotesEnum.EMOTE_SALUT_VULKAIN);

                                        if (!personaje.HasEmote((EmotesEnum.EMOTE_SUPER_HEROS)))
                                            personaje.AddEmote(EmotesEnum.EMOTE_SUPER_HEROS);
                                    }

                                    //Nivel 100
                                    if ((monster as MonsterFighter).Monster.Template.Id == 3297 && !SoulStoneFilled.FIGHT_MAPS.Contains(PlayerTeam.Fight.Map.Id))
                                    {
                                        looter.Loot.AddItem(new DroppedItem(15588, 3));
                                        if (!personaje.HasEmote((EmotesEnum.EMOTE_SALUT_VULKAIN)))
                                            personaje.AddEmote(EmotesEnum.EMOTE_SALUT_VULKAIN);

                                        if (!personaje.HasEmote((EmotesEnum.EMOTE_SUPER_HEROS)))
                                            personaje.AddEmote(EmotesEnum.EMOTE_SUPER_HEROS);
                                    }

                                    //Nivel 50
                                    if ((monster as MonsterFighter).Monster.Template.Id == 3298 && !SoulStoneFilled.FIGHT_MAPS.Contains(PlayerTeam.Fight.Map.Id))
                                    {
                                        looter.Loot.AddItem(new DroppedItem(15588, 1));
                                        if(!personaje.HasEmote((EmotesEnum.EMOTE_SALUT_VULKAIN)))
                                            personaje.AddEmote(EmotesEnum.EMOTE_SALUT_VULKAIN);

                                        if (!personaje.HasEmote((EmotesEnum.EMOTE_SUPER_HEROS)))
                                        personaje.AddEmote(EmotesEnum.EMOTE_SUPER_HEROS);
                                    }
                                    #endregion

                                }

                            }
                        }
                        #endregion

                    }
                    else
                    {

                    //    var personaje = World.Instance.GetCharacter(looter.Id);

                    //    personaje.Energy -= 1000;
                        
                    }
                    
                    if (looter is IExperienceResult)
                    {
                        var winXP = FightFormulas.CalculateWinExp(looter, team.GetAllFighters<CharacterFighter>(), droppers);
                        winXP = (long)(winXP * multiplicator);
                        var biggestwave = DefendersTeam.m_wavesFighters.OrderByDescending(x => x.WaveNumber).FirstOrDefault();
                        if (biggestwave != null)
                        {
                            winXP = FightFormulas.CalculateWinExp(looter, team.GetAllFighters<CharacterFighter>(), droppers, (biggestwave.WaveNumber + 1));
                            winXP = (long)(winXP * multiplicator);
                        }

                        (looter as IExperienceResult).AddEarnedExperience(team == Winners ? winXP : (long)Math.Round(winXP * 0.10));

                        if (FighterPlaying.Fight.DefendersTeam.Fighters.Any(x => x.Level >= 120))
                        {
                            if (looter is FightPlayerResult)
                            {
                                (looter as FightPlayerResult).Character.Record.WinPvm++;
                            }
                        }
                    }

                    character1.RefreshActor();
                }
                if (Winners == null || Draw || Winners == MonsterTeam)
                {
                    foreach(var looter in PlayerTeam.GetAllFightersWithLeavers())
                    {
                        var personaje = World.Instance.GetCharacter(looter.Id);
                        if(personaje is Character)
                        {
                            personaje.Energy -= 1000;
                            try
                            {
                                if (Map.IsDungeon())
                                {
                                    personaje.Record.RetomarMazmorra = Map.Id;
                                }
                                else
                                {
                                    personaje.Record.RetomarMazmorra = 0;
                                }
                            }
                            catch { }
                        }
                            
                    }
                    return results;
                }
                else if (DefendersTeam.Fighters.Any(x => x is MonsterFighter && (x as MonsterFighter).Monster.Nani))
                {
                    var NaniMonster = Map.NaniMonster;
                    if (NaniMonster == null) return results;

                    MonsterNaniManager.Instance.ResetSpawn(Map.NaniMonster);
                    Map.NaniMonster = null;

                    var characters = Winners.Fighters.OfType<CharacterFighter>();
                    if (characters.Count() < 1) return results;

                    if (Winners.TeamType == TeamTypeEnum.TEAM_TYPE_PLAYER) World.Instance.SendAnnounce("<b>" + string.Join(",", characters.Select(x => x.Name)) + "</b> ha conquistado : <b>" + NaniMonster.Template.Name + "</b>.");

                }

            }

            return results;
        }

        #region drop perso

        /*#region bowlton
        //if (team == Winners && looter is FightPlayerResult && this.DefendersTeam.GetAllFighters().Where(x => x is MonsterFighter).ToList().Exists(x => (x as MonsterFighter).Monster.Grade.Level > 20))
        //{
        //    Actors.RolePlay.Characters.Character character = (looter as FightPlayerResult).Character;

        //    MonsterFighter boss6 = this.DefendersTeam.GetAllFighters().Where(x => x is MonsterFighter).ToList().FirstOrDefault(x => (x as MonsterFighter).Monster.Grade.Level > 20) as MonsterFighter;

        //    if (boss6 != null)
        //    {
        //        uint bp6 = ((uint)new Stump.Core.Mathematics.CryptoRandom().Next((int)1, (int)Math.Ceiling(boss6.Level / 20 * 1.61)));

        //        looter.Loot.AddItem(new Items.DroppedItem(13026, bp6));
        //    }
        //}

        //if (team == Winners && looter is FightPlayerResult && this.DefendersTeam.GetAllFighters().Where(x => x is MonsterFighter).ToList().Exists(x => (x as MonsterFighter).Monster.Grade.Level > 20))
        //{
        //    Actors.RolePlay.Characters.Character character = (looter as FightPlayerResult).Character;

        //    MonsterFighter boss9 = this.DefendersTeam.GetAllFighters().Where(x => x is MonsterFighter).ToList().FirstOrDefault(x => (x as MonsterFighter).Monster.Grade.Level > 20) as MonsterFighter;

        //    if (boss9 != null)
        //    {
        //        uint bp9 = ((uint)new Stump.Core.Mathematics.CryptoRandom().Next((int)1, (int)Math.Ceiling(boss9.Level / 20 * 1.61)));

        //        looter.Loot.AddItem(new Items.DroppedItem(13023, bp9));
        //    }
        //}

        #endregion

        #region drop jeton perfection
        if (team == Winners && looter is FightPlayerResult && this.DefendersTeam.GetAllFighters().Where(x => x is MonsterFighter).ToList().Exists(x => (x as MonsterFighter).Monster.Template.IsBoss))
        {
            Character character = (looter as FightPlayerResult).Character;
            MonsterFighter boss2 = this.DefendersTeam.GetAllFighters().Where(x => x is MonsterFighter).ToList().FirstOrDefault(x => (x as MonsterFighter).Monster.Template.IsBoss) as MonsterFighter;
            MonsterFighter boss4 = this.DefendersTeam.GetAllFighters().Where(x => x is MonsterFighter).ToList().FirstOrDefault(x => (x as MonsterFighter).Monster.Grade.Level > 99) as MonsterFighter;

            if (boss2 != null && boss4 != null)
            {
                uint bp2 = ((uint)new Stump.Core.Mathematics.CryptoRandom().Next((int)1, (int)Math.Ceiling(boss2.Level / 30 * 1.61)));
                looter.Loot.AddItem(new Items.DroppedItem(16892, bp2));
            }
        }
        #endregion

        #region capturedemuldo
        if (team == Winners && looter is FightPlayerResult && this.DefendersTeam.GetAllFighters().Where(x => x is MonsterFighter).ToList().Exists(x => (x as MonsterFighter).Monster.Grade.Level > 175))
        {
            Actors.RolePlay.Characters.Character character = (looter as FightPlayerResult).Character;
            Items.Player.BasePlayerItem Filet = character.Inventory.TryGetItem(CharacterInventoryPositionEnum.ACCESSORY_POSITION_WEAPON);


            if (Filet != null && Filet.Template.Id == 17953 && character.Fighter.HasState((int)SpellStatesEnum.APPRIVOISEMENT_10) && character.Area.Id == 5)

            {
                MonsterFighter muldo1 = this.DefendersTeam.GetAllFighters().Where(x => x is MonsterFighter).ToList().FirstOrDefault(x => (x as MonsterFighter).Monster.Template.Id == 4434) as MonsterFighter;
                MonsterFighter muldo2 = this.DefendersTeam.GetAllFighters().Where(x => x is MonsterFighter).ToList().FirstOrDefault(x => (x as MonsterFighter).Monster.Template.Id == 4435) as MonsterFighter;
                MonsterFighter muldo3 = this.DefendersTeam.GetAllFighters().Where(x => x is MonsterFighter).ToList().FirstOrDefault(x => (x as MonsterFighter).Monster.Template.Id == 4436) as MonsterFighter;
                MonsterFighter muldo4 = this.DefendersTeam.GetAllFighters().Where(x => x is MonsterFighter).ToList().FirstOrDefault(x => (x as MonsterFighter).Monster.Template.Id == 4437) as MonsterFighter;
                MonsterFighter muldo5 = this.DefendersTeam.GetAllFighters().Where(x => x is MonsterFighter).ToList().FirstOrDefault(x => (x as MonsterFighter).Monster.Template.Id == 4438) as MonsterFighter;
                character.Inventory.RemoveItem(Filet, 1);
                if (muldo1 != null)

                {
                    looter.Loot.AddItem(new Items.DroppedItem(17957, 1));
                }

                if (muldo2 != null)

                {
                    looter.Loot.AddItem(new Items.DroppedItem(17956, 1));
                }

                if (muldo3 != null)

                {
                    looter.Loot.AddItem(new Items.DroppedItem(17958, 1));
                }

                if (muldo4 != null)

                {
                    looter.Loot.AddItem(new Items.DroppedItem(17959, 1));
                }

                if (muldo5 != null)

                {
                    looter.Loot.AddItem(new Items.DroppedItem(17960, 1));
                }

            }
        }
        #endregion

        #region drop rune
        if (team == Winners && looter is FightPlayerResult && this.DefendersTeam.GetAllFighters().Where(x => x is MonsterFighter).ToList().Exists(x => (x as MonsterFighter).Monster.Grade.Level > 20))
        {
            Character character = (looter as FightPlayerResult).Character;
            MonsterFighter boss4 = this.DefendersTeam.GetAllFighters().Where(x => x is MonsterFighter).ToList().FirstOrDefault(x => (x as MonsterFighter).Monster.Grade.Level > 20) as MonsterFighter;

            if (boss4 != null)
            {
                uint bp2 = ((uint)new Stump.Core.Mathematics.CryptoRandom().Next((int)1, (int)Math.Ceiling(boss4.Level / 9 * 8.61)));
                looter.Loot.AddItem(new Items.DroppedItem(276, bp2));
            }
        }
        #endregion

        #region DROP PB
        if (team == Winners && looter is FightPlayerResult && this.DefendersTeam.GetAllFighters().Where(x => x is MonsterFighter).ToList().Exists(x => (x as MonsterFighter).Monster.Grade.Level > 175))
        {
            Actors.RolePlay.Characters.Character character = (looter as FightPlayerResult).Character;
            Items.Player.BasePlayerItem BPsearcher = character.Inventory.TryGetItem(CharacterInventoryPositionEnum.ACCESSORY_POSITION_PETS);


            if (BPsearcher != null && BPsearcher.Template.Id == 10657)

            {
                MonsterFighter boss = this.DefendersTeam.GetAllFighters().Where(x => x is MonsterFighter).ToList().FirstOrDefault(x => (x as MonsterFighter).Monster.Grade.Level > 165) as MonsterFighter;
                MonsterFighter boss3 = this.DefendersTeam.GetAllFighters().Where(x => x is MonsterFighter).ToList().FirstOrDefault(x => (x as MonsterFighter).Monster.Template.IsBoss) as MonsterFighter;


                if (boss != null && boss3 != null)

                {
                    uint bp = ((uint)new Stump.Core.Mathematics.CryptoRandom().Next((int)1, (int)Math.Ceiling(boss.Level / 20 * 1.11)));
                    looter.Loot.AddItem(new Items.DroppedItem(7919, bp));

                }
            }
        }
        #endregion

        #region DROP TUTU
        if (team == Winners && looter is FightPlayerResult && this.DefendersTeam.GetAllFighters().Where(x => x is MonsterFighter).ToList().Exists(x => (x as MonsterFighter).Monster.Template.Id == 113))
        {
            Actors.RolePlay.Characters.Character character = (looter as FightPlayerResult).Character;
            Items.Player.BasePlayerItem Tutusearcher = character.Inventory.TryGetItem(CharacterInventoryPositionEnum.ACCESSORY_POSITION_PETS);


            if (Tutusearcher != null && Tutusearcher.Template.Id == 8153)

            {
                MonsterFighter boss10 = this.DefendersTeam.GetAllFighters().Where(x => x is MonsterFighter).ToList().FirstOrDefault(x => (x as MonsterFighter).Monster.Template.Id == 113) as MonsterFighter;

                if (boss10 != null)

                {
                    looter.Loot.AddItem(new Items.DroppedItem(2267, 1));
                }
            }
        }
        #endregion

        #region DROP dofus
        if (team == Winners && looter is FightPlayerResult && this.DefendersTeam.GetAllFighters().Where(x => x is MonsterFighter).ToList().Exists(x => (x as MonsterFighter).Monster.Template.Id == 2431))
        {

            looter.Loot.AddItem(new Items.DroppedItem(6980, 1));
        }

        if (team == Winners && looter is FightPlayerResult && this.DefendersTeam.GetAllFighters().Where(x => x is MonsterFighter).ToList().Exists(x => (x as MonsterFighter).Monster.Template.Id == 854))
        {

            looter.Loot.AddItem(new Items.DroppedItem(7044, 1));
        }
    }
    #endregion*/

        #endregion drop perso

        protected override void SendGameFightJoinMessage(CharacterFighter fighter)
        {
            ContextHandler.SendGameFightJoinMessage(fighter.Character.Client, true, true, IsStarted, IsStarted ? 0 : (int)GetPlacementTimeLeft().TotalMilliseconds / 100, FightType);
        }

        protected override bool CanCancelFight() => false;

        public override TimeSpan GetPlacementTimeLeft()
        {
            var timeleft = FightConfiguration.PlacementPhaseTime - (DateTime.Now - CreationTime).TotalMilliseconds;

            if (timeleft < 0)
                timeleft = 0;

            return TimeSpan.FromMilliseconds(timeleft);
        }

        protected override void OnDisposed()
        {
            if (m_placementTimer != null)
                m_placementTimer.Dispose();

            base.OnDisposed();
        }
    }
}