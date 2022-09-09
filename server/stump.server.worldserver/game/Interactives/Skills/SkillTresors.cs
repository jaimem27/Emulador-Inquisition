using Stump.Core.Reflection;
using Stump.DofusProtocol.Enums;
using Stump.DofusProtocol.Messages;
using Stump.DofusProtocol.Types;
using Stump.Server.BaseServer.Database;
using Stump.Server.WorldServer.Database.Interactives;
using Stump.Server.WorldServer.Game.Actors.Fight;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Monsters;
using Stump.Server.WorldServer.Game.Diverse;
using Stump.Server.WorldServer.Game.Exchanges.Bank;
using Stump.Server.WorldServer.Game.Fights;
using Stump.Server.WorldServer.Game.Maps.Cells;
using Stump.Server.WorldServer.Handlers.Context;
using System;
using System.Collections.Generic;

namespace Stump.Server.WorldServer.Game.Interactives.Skills
{
    [Discriminator("Tresors", typeof(Skill), typeof(int), typeof(InteractiveCustomSkillRecord), typeof(InteractiveObject))]
    public class SkillTresors : CustomSkill
    {
        public SkillTresors(int id, InteractiveCustomSkillRecord record, InteractiveObject interactiveObject)
            : base(id, record, interactiveObject)
        {
        }

        int? m_Level;

        /// <summary>
        /// Parameter 0
        /// </summary>
        public int Level
        {
            get
            {
                return m_Level ?? (m_Level = Record.GetParameter<int>(0)).Value;
            }
            set
            {
                Record.SetParameter(0, value);
                m_Level = value;
            }
        }

        public override int StartExecute(Character character)
        {
            if (character.IsBusy())
                return -1;

            if (!Record.AreConditionsFilled(character))
            {
                character.SendInformationMessage(TextInformationTypeEnum.TEXT_INFORMATION_ERROR, 1);
                return -1;
            }

            #region Start chasse
            if (character.Record.TreasureSearch == 0)
            {
                #region Nombre aléatoire
                //Astrub + Bonta + Brakmar
                Random rndNumbers = new Random();
                Random rndNumbers_2 = new Random();
                //Astrub + Bonta + Brakmar
                int rndNumber = 0;
                int rndNumber_2 = 0;

                for (int nbr = 1; nbr < 9; nbr++)
                {
                    //Astrub + Bonta + Brakmar
                    rndNumber = rndNumbers.Next(9);
                    rndNumber_2 = rndNumbers_2.Next(20);
                }
                #endregion

                #region Téléportation Astrub + Bonta + Brakmar
                //Astrub
                if (rndNumber == 1)
                {
                    character.Teleport(new ObjectPosition(Singleton<World>.Instance.GetMap(188744199), 235));
                    character.Record.TreasureSearch = 1;
                    character.DisplayNotification("Ahora estás buscando un tesoro.", NotificationEnum.INFORMATION);
                    character.SendServerMessage("El tesoro está en algún lugar de <b> Astrub </b>.\n" +
                        "Utiliza el comando <b>.tesoro</b> para buscar un tesoro en el mapa donde te encuentras.\n" +
                        "Utiliza el comando <b>.parar</b> para terminar la busqueda.");

                    if (!character.HasEmote(EmotesEnum.COMPLETE_A_QUEST))
                    {
                        character.AddEmote(EmotesEnum.COMPLETE_A_QUEST);
                    }
                    character.PlayEmote(EmotesEnum.COMPLETE_A_QUEST);
                }
                else if (rndNumber == 2)
                {
                    character.Teleport(new ObjectPosition(Singleton<World>.Instance.GetMap(191105026), 370));
                    character.Record.TreasureSearch = 1;
                    character.DisplayNotification("Ahora estás buscando un tesoro.", NotificationEnum.INFORMATION);
                    character.SendServerMessage("El tesoro está en algún lugar de <b> Astrub </b>.\n" +
                        "Utiliza el comando <b>.tesoro</b> para buscar un tesoro en el mapa donde te encuentras.\n" +
                        "Utiliza el comando <b>.parar</b> para terminar la busqueda.");

                    if (!character.HasEmote(EmotesEnum.COMPLETE_A_QUEST))
                    {
                        character.AddEmote(EmotesEnum.COMPLETE_A_QUEST);
                    }
                    character.PlayEmote(EmotesEnum.COMPLETE_A_QUEST);
                }
                else if (rndNumber == 3)
                {
                    character.Teleport(new ObjectPosition(Singleton<World>.Instance.GetMap(191102976), 253));
                    character.Record.TreasureSearch = 1;
                    character.DisplayNotification("Ahora estás buscando un tesoro.", NotificationEnum.INFORMATION);
                    character.SendServerMessage("El tesoro está en algún lugar de <b> Astrub </b>.\n" +
                        "Utiliza el comando <b>.tesoro</b> para buscar un tesoro en el mapa donde te encuentras.\n" +
                        "Utiliza el comando <b>.parar</b> para terminar la busqueda.");

                    if (!character.HasEmote(EmotesEnum.COMPLETE_A_QUEST))
                    {
                        character.AddEmote(EmotesEnum.COMPLETE_A_QUEST);
                    }
                    character.PlayEmote(EmotesEnum.COMPLETE_A_QUEST);
                }
                //Bonta
                else if (rndNumber == 4)
                {
                    character.Teleport(new ObjectPosition(Singleton<World>.Instance.GetMap(145204), 299));
                    character.Record.TreasureSearch = 2;
                    character.DisplayNotification("Ahora estás buscando un tesoro.", NotificationEnum.INFORMATION);
                    character.SendServerMessage("El tesoro está en algún lugar de <b> Bonta </b>.\n" +
                        "Utiliza el comando <b>.tesoro</b> para buscar un tesoro en el mapa donde te encuentras.\n" +
                        "Utiliza el comando <b>.parar</b> para terminar la busqueda.");

                    if (!character.HasEmote(EmotesEnum.COMPLETE_A_QUEST))
                    {
                        character.AddEmote(EmotesEnum.COMPLETE_A_QUEST);
                    }
                    character.PlayEmote(EmotesEnum.COMPLETE_A_QUEST);
                }
                else if (rndNumber == 5)
                {
                    character.Teleport(new ObjectPosition(Singleton<World>.Instance.GetMap(148792), 482));
                    character.Record.TreasureSearch = 2;
                    character.DisplayNotification("Ahora estás buscando un tesoro.", NotificationEnum.INFORMATION);
                    character.SendServerMessage("El tesoro está en algún lugar de <b> Bonta </b>.\n" +
                        "Utiliza el comando <b>.tesoro</b> para buscar un tesoro en el mapa donde te encuentras.\n" +
                        "Utiliza el comando <b>.parar</b> para terminar la busqueda.");

                    if (!character.HasEmote(EmotesEnum.COMPLETE_A_QUEST))
                    {
                        character.AddEmote(EmotesEnum.COMPLETE_A_QUEST);
                    }
                    character.PlayEmote(EmotesEnum.COMPLETE_A_QUEST);
                }
                else if (rndNumber == 6)
                {
                    character.Teleport(new ObjectPosition(Singleton<World>.Instance.GetMap(145212), 276));
                    character.Record.TreasureSearch = 2;
                    character.DisplayNotification("Ahora estás buscando un tesoro.", NotificationEnum.INFORMATION);
                    character.SendServerMessage("El tesoro está en algún lugar de <b> Bonta </b>.\n" +
                        "Utiliza el comando <b>.tesoro</b> para buscar un tesoro en el mapa donde te encuentras.\n" +
                        "Utiliza el comando <b>.parar</b> para terminar la busqueda.");

                    if (!character.HasEmote(EmotesEnum.COMPLETE_A_QUEST))
                    {
                        character.AddEmote(EmotesEnum.COMPLETE_A_QUEST);
                    }
                    character.PlayEmote(EmotesEnum.COMPLETE_A_QUEST);
                }
                //Brakmar
                else if (rndNumber == 7)
                {
                    character.Teleport(new ObjectPosition(Singleton<World>.Instance.GetMap(145952), 455));
                    character.Record.TreasureSearch = 3;
                    character.DisplayNotification("Ahora estás buscando un tesoro.", NotificationEnum.INFORMATION);
                    character.SendServerMessage("El tesoro está en algún lugar de <b> Brakmar </b>.\n" +
                        "Utiliza el comando <b>.tesoro</b> para buscar un tesoro en el mapa donde te encuentras.\n" +
                        "Utiliza el comando <b>.parar</b> para terminar la busqueda.");

                    if (!character.HasEmote(EmotesEnum.COMPLETE_A_QUEST))
                    {
                        character.AddEmote(EmotesEnum.COMPLETE_A_QUEST);
                    }
                    character.PlayEmote(EmotesEnum.COMPLETE_A_QUEST);
                }
                else if (rndNumber == 8)
                {
                    character.Teleport(new ObjectPosition(Singleton<World>.Instance.GetMap(141348), 233));
                    character.Record.TreasureSearch = 3;
                    character.DisplayNotification("Ahora estás buscando un tesoro.", NotificationEnum.INFORMATION);
                    character.SendServerMessage("El tesoro está en algún lugar de <b> Brakmar </b>.\n" +
                        "Utiliza el comando <b>.tesoro</b> para buscar un tesoro en el mapa donde te encuentras.\n" +
                        "Utiliza el comando <b>.parar</b> para terminar la busqueda.");

                    if (!character.HasEmote(EmotesEnum.COMPLETE_A_QUEST))
                    {
                        character.AddEmote(EmotesEnum.COMPLETE_A_QUEST);
                    }
                    character.PlayEmote(EmotesEnum.COMPLETE_A_QUEST);
                }
                else if (rndNumber == 9)
                {
                    character.Teleport(new ObjectPosition(Singleton<World>.Instance.GetMap(145447), 184));
                    character.Record.TreasureSearch = 3;
                    character.DisplayNotification("Ahora estás buscando un tesoro.", NotificationEnum.INFORMATION);
                    character.SendServerMessage("El tesoro está en algún lugar de <b> Brakmar </b>.\n" +
                        "Utiliza el comando <b>.tesoro</b> para buscar un tesoro en el mapa donde te encuentras.\n" +
                        "Utiliza el comando <b>.parar</b> para terminar la busqueda.");

                    if (!character.HasEmote(EmotesEnum.COMPLETE_A_QUEST))
                    {
                        character.AddEmote(EmotesEnum.COMPLETE_A_QUEST);
                    }
                    character.PlayEmote(EmotesEnum.COMPLETE_A_QUEST);
                }
                #endregion

                #region 20 maps différentes Astrub
                if (character.Record.TreasureSearch == 1)
                {
                    if (rndNumber_2 == 1)
                    {
                        character.Record.TreasureMapCoffre = 189531143;
                        character.Record.TreasureIndice = 1;
                    }
                    if (rndNumber_2 == 2)
                    {
                        character.Record.TreasureMapCoffre = 188746759;
                        character.Record.TreasureIndice = 2;
                    }
                    if (rndNumber_2 == 3)
                    {
                        character.Record.TreasureMapCoffre = 189792777;
                        character.Record.TreasureIndice = 3;
                    }
                    if (rndNumber_2 == 4)
                    {
                        character.Record.TreasureMapCoffre = 191102980;
                        character.Record.TreasureIndice = 4;
                    }
                    if (rndNumber_2 == 5)
                    {
                        character.Record.TreasureMapCoffre = 188745223;
                        character.Record.TreasureIndice = 5;
                    }
                    if (rndNumber_2 == 6)
                    {
                        character.Record.TreasureMapCoffre = 188745729;
                        character.Record.TreasureIndice = 6;
                    }
                    if (rndNumber_2 == 7)
                    {
                        character.Record.TreasureMapCoffre = 188743683;
                        character.Record.TreasureIndice = 7;
                    }
                    if (rndNumber_2 == 8)
                    {
                        character.Record.TreasureMapCoffre = 191104002;
                        character.Record.TreasureIndice = 8;
                    }
                    if (rndNumber_2 == 9)
                    {
                        character.Record.TreasureMapCoffre = 188744194;
                        character.Record.TreasureIndice = 9;
                    }
                    if (rndNumber_2 == 10)
                    {
                        character.Record.TreasureMapCoffre = 188743685;
                        character.Record.TreasureIndice = 10;
                    }
                    if (rndNumber_2 == 11)
                    {
                        character.Record.TreasureMapCoffre = 188744198;
                        character.Record.TreasureIndice = 11;
                    }
                    if (rndNumber_2 == 12)
                    {
                        character.Record.TreasureMapCoffre = 188746757;
                        character.Record.TreasureIndice = 12;
                    }
                    if (rndNumber_2 == 13)
                    {
                        character.Record.TreasureMapCoffre = 188746753;
                        character.Record.TreasureIndice = 13;
                    }
                    if (rndNumber_2 == 14)
                    {
                        character.Record.TreasureMapCoffre = 191105028;
                        character.Record.TreasureIndice = 14;
                    }
                    if (rndNumber_2 == 15)
                    {
                        character.Record.TreasureMapCoffre = 191102978;
                        character.Record.TreasureIndice = 15;
                    }
                    if (rndNumber_2 == 16)
                    {
                        character.Record.TreasureMapCoffre = 188745218;
                        character.Record.TreasureIndice = 16;
                    }
                    if (rndNumber_2 == 17)
                    {
                        character.Record.TreasureMapCoffre = 188745735;
                        character.Record.TreasureIndice = 17;
                    }
                    if (rndNumber_2 == 18)
                    {
                        character.Record.TreasureMapCoffre = 188744710;
                        character.Record.TreasureIndice = 18;
                    }
                    if (rndNumber_2 == 19)
                    {
                        character.Record.TreasureMapCoffre = 191106052;
                        character.Record.TreasureIndice = 19;
                    }
                    if (rndNumber_2 == 20)
                    {
                        character.Record.TreasureMapCoffre = 191104000;
                        character.Record.TreasureIndice = 20;
                    }
                }
                #endregion

                #region 20 maps différentes Bonta
                else if (character.Record.TreasureSearch == 2)
                {
                    if (rndNumber_2 == 1)
                    {
                        character.Record.TreasureMapCoffre = 150329;
                        character.Record.TreasureIndice = 21;
                    }
                    if (rndNumber_2 == 2)
                    {
                        character.Record.TreasureMapCoffre = 149306;
                        character.Record.TreasureIndice = 22;
                    }
                    if (rndNumber_2 == 3)
                    {
                        character.Record.TreasureMapCoffre = 148284;
                        character.Record.TreasureIndice = 23;
                    }
                    if (rndNumber_2 == 4)
                    {
                        character.Record.TreasureMapCoffre = 146236;
                        character.Record.TreasureIndice = 24;
                    }
                    if (rndNumber_2 == 5)
                    {
                        character.Record.TreasureMapCoffre = 145210;
                        character.Record.TreasureIndice = 25;
                    }
                    if (rndNumber_2 == 6)
                    {
                        character.Record.TreasureMapCoffre = 146745;
                        character.Record.TreasureIndice = 26;
                    }
                    if (rndNumber_2 == 7)
                    {
                        character.Record.TreasureMapCoffre = 148280;
                        character.Record.TreasureIndice = 27;
                    }
                    if (rndNumber_2 == 8)
                    {
                        character.Record.TreasureMapCoffre = 149302;
                        character.Record.TreasureIndice = 28;
                    }
                    if (rndNumber_2 == 9)
                    {
                        character.Record.TreasureMapCoffre = 147766;
                        character.Record.TreasureIndice = 29;
                    }
                    if (rndNumber_2 == 10)
                    {
                        character.Record.TreasureMapCoffre = 146230;
                        character.Record.TreasureIndice = 30;
                    }
                    if (rndNumber_2 == 11)
                    {
                        character.Record.TreasureMapCoffre = 145204;
                        character.Record.TreasureIndice = 31;
                    }
                    if (rndNumber_2 == 12)
                    {
                        character.Record.TreasureMapCoffre = 147252;
                        character.Record.TreasureIndice = 32;
                    }
                    if (rndNumber_2 == 13)
                    {
                        character.Record.TreasureMapCoffre = 149300;
                        character.Record.TreasureIndice = 33;
                    }
                    if (rndNumber_2 == 14)
                    {
                        character.Record.TreasureMapCoffre = 146740;
                        character.Record.TreasureIndice = 34;
                    }
                    if (rndNumber_2 == 15)
                    {
                        character.Record.TreasureMapCoffre = 144691;
                        character.Record.TreasureIndice = 35;
                    }
                    if (rndNumber_2 == 16)
                    {
                        character.Record.TreasureMapCoffre = 146738;
                        character.Record.TreasureIndice = 36;
                    }
                    if (rndNumber_2 == 17)
                    {
                        character.Record.TreasureMapCoffre = 147762;
                        character.Record.TreasureIndice = 37;
                    }
                    if (rndNumber_2 == 18)
                    {
                        character.Record.TreasureMapCoffre = 150326;
                        character.Record.TreasureIndice = 38;
                    }
                    if (rndNumber_2 == 19)
                    {
                        character.Record.TreasureMapCoffre = 146225;
                        character.Record.TreasureIndice = 39;
                    }
                    if (rndNumber_2 == 20)
                    {
                        character.Record.TreasureMapCoffre = 146749;
                        character.Record.TreasureIndice = 40;
                    }
                }

                #endregion

                #region 20 maps différentes Brâkmar
                else if (character.Record.TreasureSearch == 3)
                {
                    if (rndNumber_2 == 1)
                    {
                        character.Record.TreasureMapCoffre = 145439;
                        character.Record.TreasureIndice = 41;
                    }
                    if (rndNumber_2 == 2)
                    {
                        character.Record.TreasureMapCoffre = 143903;
                        character.Record.TreasureIndice = 42;
                    }
                    if (rndNumber_2 == 3)
                    {
                        character.Record.TreasureMapCoffre = 142879;
                        character.Record.TreasureIndice = 43;
                    }
                    if (rndNumber_2 == 4)
                    {
                        character.Record.TreasureMapCoffre = 141856;
                        character.Record.TreasureIndice = 44;
                    }
                    if (rndNumber_2 == 5)
                    {
                        character.Record.TreasureMapCoffre = 142880;
                        character.Record.TreasureIndice = 45;
                    }
                    if (rndNumber_2 == 6)
                    {
                        character.Record.TreasureMapCoffre = 144416;
                        character.Record.TreasureIndice = 46;
                    }
                    if (rndNumber_2 == 7)
                    {
                        character.Record.TreasureMapCoffre = 145441;
                        character.Record.TreasureIndice = 47;
                    }
                    if (rndNumber_2 == 8)
                    {
                        character.Record.TreasureMapCoffre = 141857;
                        character.Record.TreasureIndice = 48;
                    }
                    if (rndNumber_2 == 9)
                    {
                        character.Record.TreasureMapCoffre = 142370;
                        character.Record.TreasureIndice = 49;
                    }
                    if (rndNumber_2 == 10)
                    {
                        character.Record.TreasureMapCoffre = 143906;
                        character.Record.TreasureIndice = 50;
                    }
                    if (rndNumber_2 == 11)
                    {
                        character.Record.TreasureMapCoffre = 145442;
                        character.Record.TreasureIndice = 51;
                    }
                    if (rndNumber_2 == 12)
                    {
                        character.Record.TreasureMapCoffre = 146467;
                        character.Record.TreasureIndice = 52;
                    }
                    if (rndNumber_2 == 13)
                    {
                        character.Record.TreasureMapCoffre = 146980;
                        character.Record.TreasureIndice = 53;
                    }
                    if (rndNumber_2 == 14)
                    {
                        character.Record.TreasureMapCoffre = 144932;
                        character.Record.TreasureIndice = 54;
                    }
                    if (rndNumber_2 == 15)
                    {
                        character.Record.TreasureMapCoffre = 143396;
                        character.Record.TreasureIndice = 55;
                    }
                    if (rndNumber_2 == 16)
                    {
                        character.Record.TreasureMapCoffre = 141861;
                        character.Record.TreasureIndice = 56;
                    }
                    if (rndNumber_2 == 17)
                    {
                        character.Record.TreasureMapCoffre = 142886;
                        character.Record.TreasureIndice = 57;
                    }
                    if (rndNumber_2 == 18)
                    {
                        character.Record.TreasureMapCoffre = 143912;
                        character.Record.TreasureIndice = 58;
                    }
                    if (rndNumber_2 == 19)
                    {
                        character.Record.TreasureMapCoffre = 141352;
                        character.Record.TreasureIndice = 59;
                    }
                    if (rndNumber_2 == 20)
                    {
                        character.Record.TreasureMapCoffre = 142377;
                        character.Record.TreasureIndice = 60;
                    }
                }
                #endregion

                var timeChasse = DateTime.Now.AddMinutes(30);
                character.Record.TreasureTimeStart = timeChasse;

                DataManager.DefaultDatabase.Update(character.Record);
            }
            #endregion
            else
            {
                character.SendServerMessage("No puedes tener dos busquedas del mismo tipo al mismo tiempo. Debe renunciar al anterior antes de tomar uno nuevo.");
            }

            return base.StartExecute(character);
        }
    }
}