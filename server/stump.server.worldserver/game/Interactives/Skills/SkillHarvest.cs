using System;
using Stump.Core.Attributes;
using Stump.DofusProtocol.Enums;
using Stump.Server.WorldServer.Database.Interactives;
using Stump.Server.WorldServer.Database.Items.Templates;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using Stump.Server.WorldServer.Game.Items;
using Stump.Server.WorldServer.Handlers.Inventory;
using Stump.Core.Mathematics;
using Stump.Core.Timers;
using Stump.DofusProtocol.Types;
using Stump.Server.WorldServer.Game.Jobs;
using Stump.Server.WorldServer.Game.Items.Player;
using Stump.Server.WorldServer.Commands.Trigger;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Monsters;
using Stump.Core.Reflection;
using Stump.Server.WorldServer.Game.Maps.Cells;
using Stump.Server.WorldServer.Game.Fights;
using Stump.Server.WorldServer.Game.Actors.Fight;
using Stump.Server.WorldServer.Handlers.Context;
using Stump.DofusProtocol.Messages;
using System.Linq;

namespace Stump.Server.WorldServer.Game.Interactives.Skills
{
    public class SkillHarvest : Skill, ISkillWithAgeBonus
    {
        [Variable(true)]
        public static int StarsBonusRate = 1440000000;

        [Variable(true)]
        public static short StarsBonusLimit = 200;

        public const short ClientStarsBonusLimit = 200;

        [Variable]
        public static int HarvestTime = 3000;

        [Variable]
        public static int RegrowTime = 40000;

        ItemTemplate m_harvestedItem;
        private TimedTimerEntry m_regrowTimer;

        public SkillHarvest(int id, InteractiveSkillTemplate skillTemplate, InteractiveObject interactiveObject)
            : base(id, skillTemplate, interactiveObject)
        {
            m_harvestedItem = ItemManager.Instance.TryGetTemplate(SkillTemplate.GatheredRessourceItem);
            CreationDate = DateTime.Now;

            if (m_harvestedItem == null)
                throw new Exception($"Harvested item {SkillTemplate.GatheredRessourceItem} doesn't exist");
        }

        public bool Harvested => HarvestedSince.HasValue && (DateTime.Now - HarvestedSince).Value.TotalMilliseconds < (RegrowTime * (m_harvestedItem.Level / 5));

        public void RecursosRaros(Character recolector, ItemTemplate recurso)
        {
            Random rnd = new Random();
            var randomfinded = rnd.Next(0, 100);

            
            var pp = (recolector.Stats[PlayerFields.Prospecting] / 50) ;
            
            if(pp >= randomfinded)
            {
                ItemTemplate raro = new ItemTemplate();

                switch (recurso.Id)
                {
                    //<--- Leñador de nivel 1 a 200 en orden---->
                    case 303: //Fresno
                        ProtectorRecursos(711, recolector);
                        break;
                    case 473: //Castaño
                        ProtectorRecursos(712, recolector);
                        break;
                    case 476: //Nogal
                        ProtectorRecursos(713, recolector);
                        break;
                    case 460: //Roble
                        ProtectorRecursos(714, recolector);
                        break;
                    case 2358: //Bombú
                        ProtectorRecursos(715, recolector);
                        break;
                    case 471: //Arce
                        ProtectorRecursos(717, recolector);
                        break;
                    case 2357: //Olivioleta
                        ProtectorRecursos(716, recolector);
                        break;
                    case 461: //Tejo
                        ProtectorRecursos(718, recolector);
                        break;
                    case 7013: //Bambú
                        ProtectorRecursos(719, recolector);
                        break;
                    case 474: //Cerezo silvestre
                        ProtectorRecursos(721, recolector);
                        break;
                    case 16488: //Avellano
                        ProtectorRecursos(4118, recolector);
                        break;
                    case 449: //Ébano
                        ProtectorRecursos(722, recolector);
                        break;
                    case 7925: //Kalipto
                        ProtectorRecursos(720, recolector);
                        break;
                    case 472: //Carpe
                        ProtectorRecursos(782, recolector);
                        break;
                    case 7016: //Bambú oscuro
                        ProtectorRecursos(723, recolector);
                        break;
                    case 470: //Olmo
                        ProtectorRecursos(724, recolector);
                        break;
                    case 7014: //Bambú sagrado
                        ProtectorRecursos(725, recolector);
                        break;
                    case 11107: //Álamo temblón
                        ProtectorRecursos(2842, recolector);
                        break;
                    case 17991: //Acuacia
                        ProtectorRecursos(4483, recolector);
                        break;
                    //<--- Alquimista de nivel 1 a 200 en orden---->
                    case 421: //Ortiga
                        ProtectorRecursos(704, recolector);
                        break;
                    case 428: //Salvia
                        ProtectorRecursos(705, recolector);
                        break;
                    case 395: //Trébol 5 hojas
                        ProtectorRecursos(706, recolector);
                        break;
                    case 380: //Menta salvaje
                        ProtectorRecursos(707, recolector);
                        break;
                    case 593: //Orquídea fresca
                        ProtectorRecursos(708, recolector);
                        break;
                    case 594: //Edelweiss
                        ProtectorRecursos(709, recolector);
                        break;
                    case 7059: //Semilla de pandoja
                        ProtectorRecursos(710, recolector);
                        break;
                    case 16385: //Ginseng
                        ProtectorRecursos(4115, recolector);
                        break;
                    case 16387: //Belladona
                        ProtectorRecursos(4116, recolector);
                        break;
                    case 16389: //Mandrágora
                        ProtectorRecursos(4117, recolector);
                        break;
                    case 11102: //Campanilla de invierno
                        ProtectorRecursos(2839, recolector);
                        break;
                    case 17992: //Salikrona
                        ProtectorRecursos(4488, recolector);
                        break;
                    //<--- Pescador de nivel 1 a 200 en orden---->
                    case 1782: //Gobio
                        ProtectorRecursos(726, recolector);
                        break;
                    case 598: //Bamga
                        ProtectorRecursos(729, recolector);
                        break;
                    case 1844: //Trucha
                        ProtectorRecursos(727, recolector);
                        break;
                    case 1757: //Cangrejo surimi
                        ProtectorRecursos(730, recolector);
                        break;
                    case 603: //Pez gatito
                        ProtectorRecursos(728, recolector);
                        break;
                    case 1750: //Pescado empanado
                        ProtectorRecursos(731, recolector);
                        break;
                    case 1794: //Carpa de Iem
                        ProtectorRecursos(733, recolector);
                        break;
                    case 1805: //Sardina brillante
                        ProtectorRecursos(734, recolector);
                        break;
                    case 1847: //Lucio
                        ProtectorRecursos(732, recolector);
                        break;
                    case 600: //Kralamar
                        ProtectorRecursos(735, recolector);
                        break;
                    case 16461: //Anguila
                        ProtectorRecursos(4183, recolector);
                        break;
                    case 16463: //Dorada exploradora
                        ProtectorRecursos(4184, recolector);
                        break;
                    case 1801: //Perca
                        ProtectorRecursos(738, recolector);
                        break;
                    case 1784: //Raya azul
                        ProtectorRecursos(737, recolector);
                        break;
                    case 16465: //Rape
                        ProtectorRecursos(4185, recolector);
                        break;
                    case 602: //Tiburón martillhoz'
                        ProtectorRecursos(739, recolector);
                        break;
                    case 1779: //Lubina Mericana
                        ProtectorRecursos(736, recolector);
                        break;
                    case 16467: //Bacaladilla
                        ProtectorRecursos(4186, recolector);
                        break;
                    case 16469: //Tenca
                        ProtectorRecursos(4187, recolector);
                        break;
                    case 16471: //Pez Espada
                        ProtectorRecursos(4188, recolector);
                        break;
                    case 11106: //Inverluza
                        ProtectorRecursos(2841, recolector);
                        break;
                    case 17994: //Lapas
                        ProtectorRecursos(4486, recolector);
                        break;
                    //<--- Campesino de nivel 1 a 200 en orden---->
                    case 289: //Trigo
                        ProtectorRecursos(684, recolector);
                        break;
                    case 400: //Cebada
                        ProtectorRecursos(685, recolector);
                        break;
                    case 533: //Avena
                        ProtectorRecursos(686, recolector);
                        break;
                    case 401: //Lúpulo
                        ProtectorRecursos(687, recolector);
                        break;
                    case 423: //Lino
                        ProtectorRecursos(688, recolector);
                        break;
                    case 532: //Centeno
                        ProtectorRecursos(689, recolector);
                        break;
                    case 7018: //Arroz
                        ProtectorRecursos(690, recolector);
                        break;
                    case 405: //Malta
                        ProtectorRecursos(691, recolector);
                        break;
                    case 425: //Cañamo
                        ProtectorRecursos(692, recolector);
                        break;
                    case 16454: //Maiz
                        ProtectorRecursos(4119, recolector);
                        break;
                    case 16456: //Mijo
                        ProtectorRecursos(4120, recolector);
                        break;
                    case 11109: //Frostizz
                        ProtectorRecursos(2843, recolector);
                        break;
                    case 17993: //Quisnoa
                        ProtectorRecursos(4484, recolector);
                        break;
                    //<--- Minero de nivel 1 a 200 en orden---->
                    case 312: //Hierro
                        ProtectorRecursos(693, recolector);
                        break;
                    case 441: //Cobre
                        ProtectorRecursos(694, recolector);
                        break;
                    case 442: //Broncekobalto
                        ProtectorRecursos(695, recolector);
                        break;
                    case 443: //Kobalto
                        ProtectorRecursos(696, recolector);
                        break;
                    case 445: //Manganeso
                        ProtectorRecursos(697, recolector);
                        break;
                    case 444: //Estaño
                        ProtectorRecursos(698, recolector);
                        break;
                    case 7032: //Silicato
                        ProtectorRecursos(699, recolector);
                        break;
                    case 350: //Plata
                        ProtectorRecursos(700, recolector);
                        break;
                    case 446: //Bauxita
                        ProtectorRecursos(701, recolector);
                        break;
                    case 313: //Oro
                        ProtectorRecursos(702, recolector);
                        break;
                    case 7033: //Dolomita
                        ProtectorRecursos(703, recolector);
                        break;
                    case 11110: //Obsidiana
                        ProtectorRecursos(2844, recolector);
                        break;
                    case 17995: //Sepiolita
                        ProtectorRecursos(4485, recolector);
                        break;


                    default:
                        //recolector.Inventory.AddItem(recurso, 1);
                        break;
                }
            }

        }

        public void ProtectorRecursos(int MonsterId, Character character)
        {
            
            var monsterGradeId = 1;
            while (monsterGradeId < 5)
            {
                var level = character.Level;

                if (level > 200)
                    level = 200;

                if (level > monsterGradeId * 20 + 10)
                    monsterGradeId++;
                else
                    break;
            }

            var grade = Singleton<MonsterManager>.Instance.GetMonsterGrade(MonsterId, monsterGradeId);
            var position = new ObjectPosition(character.Map, character.Cell, (DirectionsEnum)5);
            var monster = new Monster(grade, new MonsterGroup(0, position));

            var fight = Singleton<FightManager>.Instance.CreatePvMFight(character.Map);
            fight.ChallengersTeam.AddFighter(character.CreateFighter(fight.ChallengersTeam));
            fight.DefendersTeam.AddFighter(new MonsterFighter(fight.DefendersTeam, monster));
            fight.StartPlacement();

            fight.HideBlades();
            //fight.StartFighting();
            
            ContextHandler.HandleGameFightJoinRequestMessage(character.Client,
            new GameFightJoinRequestMessage(character.Fighter.Id, (ushort)fight.Id));
            character.SaveLater();

            character.DisplayNotification("Acaba de aparecer un <b>protector de los recursos</b>. Defiéndete !!", NotificationEnum.INFORMATION);

        }


        public DateTime CreationDate
        {
            get;
            private set;
        }

        public DateTime EnabledSince => HarvestedSince + TimeSpan.FromMilliseconds(RegrowTime) ?? CreationDate;

        public DateTime? HarvestedSince
        {
            get;
            private set;
        }


        public short AgeBonus
        {
            get
            {
                var bonus = (DateTime.Now - EnabledSince).TotalSeconds / (StarsBonusRate);

                if (bonus > StarsBonusLimit)
                    bonus = StarsBonusLimit;

                return (short)bonus;
            }
            set { HarvestedSince = DateTime.Now - TimeSpan.FromMilliseconds(RegrowTime) - TimeSpan.FromSeconds(value * StarsBonusRate); }
        }


        public override int GetDuration(Character character, bool forNetwork = false) => HarvestTime;

        public override bool IsEnabled(Character character)
            => base.IsEnabled(character) && !Harvested && character.Jobs[SkillTemplate.ParentJobId].Level >= SkillTemplate.LevelMin;

        public override int StartExecute(Character character)
        {
            InteractiveObject.SetInteractiveState(InteractiveStateEnum.STATE_ANIMATED);

            base.StartExecute(character);

            return GetDuration(character);
        }

        public override void EndExecute(Character character)
        {
            var count = RollHarvestedItemCount(character);
            var bonus = (int)Math.Floor(count * (AgeBonus / 100d));

            SetHarvested();

            InteractiveObject.SetInteractiveState(InteractiveStateEnum.STATE_ACTIVATED);

            if (character.Inventory.IsFull(m_harvestedItem, count))
            {
                //Votre inventaire est plein. Votre récolte est perdue...
                character.SendInformationMessage(TextInformationTypeEnum.TEXT_INFORMATION_MESSAGE, 144);

                base.EndExecute(character);
                return;
            }
            RecursosRaros(character, m_harvestedItem);
            character.Inventory.AddItem(m_harvestedItem, count + bonus);
            
            InventoryHandler.SendObtainedItemWithBonusMessage(character.Client, m_harvestedItem, count, bonus);

            if (SkillTemplate.ParentJobId != 1)
            {
                var xp = JobManager.Instance.GetHarvestJobXp((int)SkillTemplate.LevelMin);
                var multiplicador = 1.0f;
                if (character.WorldAccount.Vip)
                    multiplicador = 1.55f;

                character.Jobs[SkillTemplate.ParentJobId].Experience += (long)(xp * (long)Rates.JobXpRate * multiplicador);
            }

            character.OnHarvestItem(m_harvestedItem, count + bonus);

            base.EndExecute(character);
        }

        public void SetHarvested()
        {
            HarvestedSince = DateTime.Now;
            InteractiveObject.Map.Refresh(InteractiveObject);
            m_regrowTimer = InteractiveObject.Area.CallDelayed(RegrowTime, Regrow);
        }

        public void Regrow()
        {
            if (m_regrowTimer != null)
            {
                m_regrowTimer.Stop();
                m_regrowTimer = null;
            }

            InteractiveObject.Map.Refresh(InteractiveObject);
            InteractiveObject.SetInteractiveState(InteractiveStateEnum.STATE_NORMAL);
        }

        int RollHarvestedItemCount(Character character)
        {
            var job = character.Jobs[SkillTemplate.ParentJobId];
            var minMax = JobManager.Instance.GetHarvestItemMinMax(job.Template, job.Level, SkillTemplate);
            return new CryptoRandom().Next(minMax.First, minMax.Second + 1);
        }
    }
}