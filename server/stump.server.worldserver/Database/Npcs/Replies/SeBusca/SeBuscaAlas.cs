using Stump.DofusProtocol.Enums;
using Stump.Server.BaseServer.Database;
using Stump.Core.Reflection;
using Stump.Server.WorldServer.Database.Npcs;
using Stump.Server.WorldServer.Database.Npcs.Replies;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Npcs;
using Stump.Server.WorldServer.Game;
using System.Drawing;
using Stump.Server.WorldServer.Game.Items;

namespace Database.Npcs.Replies
{
    [Discriminator("SeBuscaAlas", typeof(NpcReply), new System.Type[]
 {
  typeof(NpcReplyRecord)
 })]
    class SeBuscaAlas : NpcReply
    {
        public SeBuscaAlas(NpcReplyRecord record)
            : base(record)
        {
        }
        public override bool Execute(Npc npc, Character character)
        {
            bool entregado = false;           

            //Misiones de alineamiento para Bontas y Brakmar, únicamente.

            #region Sam Sagás
            var seBuscaItemSam = Singleton<ItemManager>.Instance.TryGetTemplate(14970);
            var Sam = character.Inventory.TryGetItem(seBuscaItemSam);

            if (Sam != null && character.AlignmentSide != AlignmentSideEnum.ALIGNMENT_NEUTRAL)
            {
                string ali = "Mercenarias";
                if (character.AlignmentSide == AlignmentSideEnum.ALIGNMENT_ANGEL)
                    ali = "Bontarianas";
                if (character.AlignmentSide == AlignmentSideEnum.ALIGNMENT_EVIL)
                    ali = "Brakmarias";

                string annonce = character.Name + " ha entregado a Sam Sagás a las autoridades " + ali + ".";

                character.SendServerMessage("Has ganado 245 kamas.");
                character.SendServerMessage("Has ganado 100000 puntos de experencia.");
                character.SendServerMessage("Has ganado 50 Chapas.");
                character.SendServerMessage("Has ganado 100 puntos de honor.");

                var chapeaus = ItemManager.Instance.CreatePlayerItem(character, 10275, 50, true);

                character.AddHonor(100);
                character.Inventory.AddKamas(245);
                character.Inventory.AddItem(chapeaus);
                character.AddExperience(100000);
                character.Inventory.RemoveItem(Sam, 1);
                Singleton<World>.Instance.SendAnnounce(annonce, Color.Yellow);
                entregado = true;
            }
            #endregion

            #region Maestro Plomo
            var seBuscaItemPlomo = Singleton<ItemManager>.Instance.TryGetTemplate(14971);
            var Plomo = character.Inventory.TryGetItem(seBuscaItemPlomo);

            if (Plomo != null && character.AlignmentSide != AlignmentSideEnum.ALIGNMENT_NEUTRAL)
            {
                string ali = "Mercenarias";
                if (character.AlignmentSide == AlignmentSideEnum.ALIGNMENT_ANGEL)
                    ali = "Bontarianas";
                if (character.AlignmentSide == AlignmentSideEnum.ALIGNMENT_EVIL)
                    ali = "Brakmarias";

                string annonce = character.Name + " ha entregado a Maestro Plomo a las autoridades " + ali + ".";

                character.SendServerMessage("Has ganado 544 kamas.");
                character.SendServerMessage("Has ganado 150000 puntos de experencia.");
                character.SendServerMessage("Has ganado 75 Chapas.");
                character.SendServerMessage("Has ganado 150 puntos de honor.");

                var chapeaus = ItemManager.Instance.CreatePlayerItem(character, 10275, 75, true);

                character.AddHonor(150);
                character.Inventory.AddKamas(544);
                character.Inventory.AddItem(chapeaus);
                character.AddExperience(150000);
                character.Inventory.RemoveItem(Plomo, 1);
                Singleton<World>.Instance.SendAnnounce(annonce, Color.Yellow);
                entregado = true;
            }
            #endregion

            #region Tocap Elotas
            var seBuscaItemElotas = Singleton<ItemManager>.Instance.TryGetTemplate(14972);
            var Elotas = character.Inventory.TryGetItem(seBuscaItemElotas);

            if (Elotas != null && character.AlignmentSide != AlignmentSideEnum.ALIGNMENT_NEUTRAL)
            {
                string ali = "Mercenarias";
                if (character.AlignmentSide == AlignmentSideEnum.ALIGNMENT_ANGEL)
                    ali = "Bontarianas";
                if (character.AlignmentSide == AlignmentSideEnum.ALIGNMENT_EVIL)
                    ali = "Brakmarias";

                string annonce = character.Name + " ha entregado a Tocap Elotas a las autoridades " + ali + ".";

                character.SendServerMessage("Has ganado 1642 kamas.");
                character.SendServerMessage("Has ganado 200000 puntos de experencia.");
                character.SendServerMessage("Has ganado 100 Chapas.");
                character.SendServerMessage("Has ganado 200 puntos de honor.");

                var chapeaus = ItemManager.Instance.CreatePlayerItem(character, 10275, 100, true);

                character.AddHonor(200);
                character.Inventory.AddKamas(1642);
                character.Inventory.AddItem(chapeaus);
                character.AddExperience(200000);
                character.Inventory.RemoveItem(Elotas, 1);
                Singleton<World>.Instance.SendAnnounce(annonce, Color.Yellow);
                entregado = true;
            }
            #endregion

            #region Buz Beib
            var seBuscaItemBuz = Singleton<ItemManager>.Instance.TryGetTemplate(14973);
            var Buz = character.Inventory.TryGetItem(seBuscaItemBuz);

            if (Buz != null && character.AlignmentSide != AlignmentSideEnum.ALIGNMENT_NEUTRAL)
            {
                string ali = "Mercenarias";
                if (character.AlignmentSide == AlignmentSideEnum.ALIGNMENT_ANGEL)
                    ali = "Bontarianas";
                if (character.AlignmentSide == AlignmentSideEnum.ALIGNMENT_EVIL)
                    ali = "Brakmarias";

                string annonce = character.Name + " ha entregado a Buz Beib a las autoridades " + ali + ".";

                character.SendServerMessage("Has ganado 3334 kamas.");
                character.SendServerMessage("Has ganado 250000 puntos de experencia.");
                character.SendServerMessage("Has ganado 125 Chapas.");
                character.SendServerMessage("Has ganado 250 puntos de honor.");

                var chapeaus = ItemManager.Instance.CreatePlayerItem(character, 10275, 125, true);

                character.AddHonor(250);
                character.Inventory.AddKamas(3334);
                character.Inventory.AddItem(chapeaus);
                character.AddExperience(250000);
                character.Inventory.RemoveItem(Buz, 1);
                Singleton<World>.Instance.SendAnnounce(annonce, Color.Yellow);
                entregado = true;
            }
            #endregion

            #region Nono el Wobot
            var seBuscaItemWobot = Singleton<ItemManager>.Instance.TryGetTemplate(14974);
            var Wobot = character.Inventory.TryGetItem(seBuscaItemWobot);

            if (Wobot != null && character.AlignmentSide != AlignmentSideEnum.ALIGNMENT_NEUTRAL)
            {
                string ali = "Mercenarias";
                if (character.AlignmentSide == AlignmentSideEnum.ALIGNMENT_ANGEL)
                    ali = "Bontarianas";
                if (character.AlignmentSide == AlignmentSideEnum.ALIGNMENT_EVIL)
                    ali = "Brakmarias";

                string annonce = character.Name + " ha entregado a Nono el Wobot a las autoridades " + ali + ".";

                character.SendServerMessage("Has ganado 5000 kamas.");
                character.SendServerMessage("Has ganado 300000 puntos de experencia.");
                character.SendServerMessage("Has ganado 150 Chapas.");
                character.SendServerMessage("Has ganado 300 puntos de honor.");

                var chapeaus = ItemManager.Instance.CreatePlayerItem(character, 10275, 150, true);

                character.AddHonor(300);
                character.Inventory.AddKamas(5000);
                character.Inventory.AddItem(chapeaus);
                character.AddExperience(300000);
                character.Inventory.RemoveItem(Wobot, 1);
                Singleton<World>.Instance.SendAnnounce(annonce, Color.Yellow);
                entregado = true;
            }
            #endregion

            #region Armada la Invencible
            var seBuscaItemArmada = Singleton<ItemManager>.Instance.TryGetTemplate(14978);
            var Armada = character.Inventory.TryGetItem(seBuscaItemArmada);

            if (Armada != null && character.AlignmentSide != AlignmentSideEnum.ALIGNMENT_NEUTRAL)
            {
                string ali = "Mercenarias";
                if (character.AlignmentSide == AlignmentSideEnum.ALIGNMENT_ANGEL)
                    ali = "Bontarianas";
                if (character.AlignmentSide == AlignmentSideEnum.ALIGNMENT_EVIL)
                    ali = "Brakmarias";

                string annonce = character.Name + " ha entregado a la Armada la Invencible a las autoridades " + ali + ".";

                character.SendServerMessage("Has ganado 16888 kamas.");
                character.Inventory.AddKamas(16888);

                character.SendServerMessage("Has ganado 350000 puntos de experencia.");
                character.AddExperience(350000);

                character.SendServerMessage("Has ganado 200 Chapas.");
                var chapeaus = ItemManager.Instance.CreatePlayerItem(character, 10275, 200, true);
                character.Inventory.AddItem(chapeaus);

                character.SendServerMessage("Has ganado 350 puntos de honor.");
                character.AddHonor(350);

                character.Inventory.RemoveItem(Armada, 1);
                Singleton<World>.Instance.SendAnnounce(annonce, Color.Yellow);
                entregado = true;
            }
            #endregion

            #region Dragopavona
            var seBuscaItemDragopavona = Singleton<ItemManager>.Instance.TryGetTemplate(14979);
            var Dragopavona = character.Inventory.TryGetItem(seBuscaItemDragopavona);

            if (Dragopavona != null && character.AlignmentSide != AlignmentSideEnum.ALIGNMENT_NEUTRAL)
            {
                string ali = "Mercenarias";
                if (character.AlignmentSide == AlignmentSideEnum.ALIGNMENT_ANGEL)
                    ali = "Bontarianas";
                if (character.AlignmentSide == AlignmentSideEnum.ALIGNMENT_EVIL)
                    ali = "Brakmarias";

                string annonce = character.Name + " ha entregado a Dragopavona a las autoridades " + ali + ".";

                character.SendServerMessage("Has ganado 25647 kamas.");
                character.Inventory.AddKamas(25647);

                character.SendServerMessage("Has ganado 400000 puntos de experencia.");
                character.AddExperience(400000);

                character.SendServerMessage("Has ganado 225 Chapas.");
                var chapeaus = ItemManager.Instance.CreatePlayerItem(character, 10275, 225, true);
                character.Inventory.AddItem(chapeaus);

                character.SendServerMessage("Has ganado 400 puntos de honor.");
                character.AddHonor(400);

                character.Inventory.RemoveItem(Dragopavona, 1);
                Singleton<World>.Instance.SendAnnounce(annonce, Color.Yellow);
                entregado = true;
            }
            #endregion

            #region Queba Sura
            var seBuscaItemQueba = Singleton<ItemManager>.Instance.TryGetTemplate(14980);
            var Queba = character.Inventory.TryGetItem(seBuscaItemQueba);

            if (Queba != null && character.AlignmentSide != AlignmentSideEnum.ALIGNMENT_NEUTRAL)
            {
                string ali = "Mercenarias";
                if (character.AlignmentSide == AlignmentSideEnum.ALIGNMENT_ANGEL)
                    ali = "Bontarianas";
                if (character.AlignmentSide == AlignmentSideEnum.ALIGNMENT_EVIL)
                    ali = "Brakmarias";

                string annonce = character.Name + " ha entregado a Queba Sura a las autoridades " + ali + ".";

                character.SendServerMessage("Has ganado 68102 kamas.");
                character.Inventory.AddKamas(68102);

                character.SendServerMessage("Has ganado 450000 puntos de experencia.");
                character.AddExperience(450000);

                character.SendServerMessage("Has ganado 250 Chapas.");
                var chapeaus = ItemManager.Instance.CreatePlayerItem(character, 10275, 250, true);
                character.Inventory.AddItem(chapeaus);

                character.SendServerMessage("Has ganado 450 puntos de honor.");
                character.AddHonor(450);

                character.Inventory.RemoveItem(Queba, 1);
                Singleton<World>.Instance.SendAnnounce(annonce, Color.Yellow);
                entregado = true;
            }
            #endregion

            #region Príncipe Embaucador
            var seBuscaItemEmbaucador = Singleton<ItemManager>.Instance.TryGetTemplate(14981);
            var Embaucador = character.Inventory.TryGetItem(seBuscaItemEmbaucador);

            if (Embaucador != null && character.AlignmentSide != AlignmentSideEnum.ALIGNMENT_NEUTRAL)
            {
                string ali = "Mercenarias";
                if (character.AlignmentSide == AlignmentSideEnum.ALIGNMENT_ANGEL)
                    ali = "Bontarianas";
                if (character.AlignmentSide == AlignmentSideEnum.ALIGNMENT_EVIL)
                    ali = "Brakmarias";

                string annonce = character.Name + " ha entregado a Príncipe Embaucador a las autoridades " + ali + ".";

                character.SendServerMessage("Has ganado 123411 kamas.");
                character.Inventory.AddKamas(123411);

                character.SendServerMessage("Has ganado 500000 puntos de experencia.");
                character.AddExperience(500000);

                character.SendServerMessage("Has ganado 275 Chapas.");
                var chapeaus = ItemManager.Instance.CreatePlayerItem(character, 10275, 275, true);
                character.Inventory.AddItem(chapeaus);

                character.SendServerMessage("Has ganado 500 puntos de honor.");
                character.AddHonor(500);

                character.Inventory.RemoveItem(Embaucador, 1);
                Singleton<World>.Instance.SendAnnounce(annonce, Color.Yellow);
                entregado = true;
            }
            #endregion

            #region Goblechaun
            var seBuscaItemGoblechaun = Singleton<ItemManager>.Instance.TryGetTemplate(14982);
            var Goblechaun = character.Inventory.TryGetItem(seBuscaItemGoblechaun);

            if (Goblechaun != null && character.AlignmentSide != AlignmentSideEnum.ALIGNMENT_NEUTRAL)
            {
                string ali = "Mercenarias";
                if (character.AlignmentSide == AlignmentSideEnum.ALIGNMENT_ANGEL)
                    ali = "Bontarianas";
                if (character.AlignmentSide == AlignmentSideEnum.ALIGNMENT_EVIL)
                    ali = "Brakmarias";
                string annonce = character.Name + " ha entregado a Goblechaun a las autoridades " + ali + ".";

                character.SendServerMessage("Has ganado 160000 kamas.");
                character.Inventory.AddKamas(160000);

                character.SendServerMessage("Has ganado 550000 puntos de experencia.");
                character.AddExperience(550000);

                character.SendServerMessage("Has ganado 300 Chapas.");
                var chapeaus = ItemManager.Instance.CreatePlayerItem(character, 10275, 300, true);
                character.Inventory.AddItem(chapeaus);

                character.SendServerMessage("Has ganado 550 puntos de honor.");
                character.AddHonor(550);

                character.Inventory.RemoveItem(Goblechaun, 1);
                Singleton<World>.Instance.SendAnnounce(annonce, Color.Yellow);
                entregado = true;
            }
            #endregion

            #region Vakakiwíe
            var seBuscaItemVakakiwie = Singleton<ItemManager>.Instance.TryGetTemplate(14983);
            var Vakakiwie = character.Inventory.TryGetItem(seBuscaItemVakakiwie);

            if (Vakakiwie != null && character.AlignmentSide != AlignmentSideEnum.ALIGNMENT_NEUTRAL)
            {
                string ali = "Mercenarias";
                if (character.AlignmentSide == AlignmentSideEnum.ALIGNMENT_ANGEL)
                    ali = "Bontarianas";
                if (character.AlignmentSide == AlignmentSideEnum.ALIGNMENT_EVIL)
                    ali = "Brakmarias";

                string annonce = character.Name + " ha entregado a Vakakiwíe a las autoridades " + ali + ".";

                character.SendServerMessage("Has ganado 197122 kamas.");
                character.Inventory.AddKamas(197122);

                character.SendServerMessage("Has ganado 600000 puntos de experencia.");
                character.AddExperience(600000);

                character.SendServerMessage("Has ganado 325 Chapas.");
                var chapeaus = ItemManager.Instance.CreatePlayerItem(character, 10275, 325, true);
                character.Inventory.AddItem(chapeaus);

                character.SendServerMessage("Has ganado 600 puntos de honor.");
                character.AddHonor(600);

                character.Inventory.RemoveItem(Vakakiwie, 1);
                Singleton<World>.Instance.SendAnnounce(annonce, Color.Yellow);
                entregado = true;
            }
            #endregion

            #region Yerar Depandur
            var seBuscaItemYerar = Singleton<ItemManager>.Instance.TryGetTemplate(14990);
            var Yerar = character.Inventory.TryGetItem(seBuscaItemYerar);

            if (Yerar != null && character.AlignmentSide != AlignmentSideEnum.ALIGNMENT_NEUTRAL)
            {
                string ali = "Mercenarias";
                if (character.AlignmentSide == AlignmentSideEnum.ALIGNMENT_ANGEL)
                    ali = "Bontarianas";
                if (character.AlignmentSide == AlignmentSideEnum.ALIGNMENT_EVIL)
                    ali = "Brakmarias";

                string annonce = character.Name + " ha entregado a Yerar Depandur a las autoridades " + ali + ".";

                character.SendServerMessage("Has ganado 236544 kamas.");
                character.Inventory.AddKamas(236544);

                character.SendServerMessage("Has ganado 650000 puntos de experencia.");
                character.AddExperience(650000);

                character.SendServerMessage("Has ganado 350 Chapas.");
                var chapeaus = ItemManager.Instance.CreatePlayerItem(character, 10275, 350, true);
                character.Inventory.AddItem(chapeaus);

                character.SendServerMessage("Has ganado 650 puntos de honor.");
                character.AddHonor(650);

                character.Inventory.RemoveItem(Yerar, 1);
                Singleton<World>.Instance.SendAnnounce(annonce, Color.Yellow);
                entregado = true;
            }
            #endregion

            #region Darma
            var seBuscaItemDarma = Singleton<ItemManager>.Instance.TryGetTemplate(14991);
            var Darma = character.Inventory.TryGetItem(seBuscaItemDarma);

            if (Darma != null && character.AlignmentSide != AlignmentSideEnum.ALIGNMENT_NEUTRAL)
            {
                string ali = "Mercenarias";
                if (character.AlignmentSide == AlignmentSideEnum.ALIGNMENT_ANGEL)
                    ali = "Bontarianas";
                if (character.AlignmentSide == AlignmentSideEnum.ALIGNMENT_EVIL)
                    ali = "Brakmarias";

                string annonce = character.Name + " ha entregado a Darma a las autoridades " + ali + ".";

                character.SendServerMessage("Has ganado 245000 kamas.");
                character.Inventory.AddKamas(245000);

                character.SendServerMessage("Has ganado 700000 puntos de experencia.");
                character.AddExperience(700000);

                character.SendServerMessage("Has ganado 375 Chapas.");
                var chapeaus = ItemManager.Instance.CreatePlayerItem(character, 10275, 375, true);
                character.Inventory.AddItem(chapeaus);

                character.SendServerMessage("Has ganado 700 puntos de honor.");
                character.AddHonor(700);

                character.Inventory.RemoveItem(Darma, 1);
                Singleton<World>.Instance.SendAnnounce(annonce, Color.Yellow);
                entregado = true;
            }
            #endregion

            #region Balimogli
            var seBuscaItemBalimogli = Singleton<ItemManager>.Instance.TryGetTemplate(14992);
            var Balimogli = character.Inventory.TryGetItem(seBuscaItemBalimogli);

            if (Balimogli != null && character.AlignmentSide != AlignmentSideEnum.ALIGNMENT_NEUTRAL)
            {
                string ali = "Mercenarias";
                if (character.AlignmentSide == AlignmentSideEnum.ALIGNMENT_ANGEL)
                    ali = "Bontarianas";
                if (character.AlignmentSide == AlignmentSideEnum.ALIGNMENT_EVIL)
                    ali = "Brakmarias";

                string annonce = character.Name + " ha entregado a Balimogli a las autoridades " + ali + ".";

                character.SendServerMessage("Has ganado 365555 kamas.");
                character.Inventory.AddKamas(365555);

                character.SendServerMessage("Has ganado 750000 puntos de experencia.");
                character.AddExperience(750000);

                character.SendServerMessage("Has ganado 400 Chapas.");
                var chapeaus = ItemManager.Instance.CreatePlayerItem(character, 10275, 400, true);
                character.Inventory.AddItem(chapeaus);

                character.SendServerMessage("Has ganado 750 puntos de honor.");
                character.AddHonor(750);

                character.Inventory.RemoveItem(Balimogli, 1);
                Singleton<World>.Instance.SendAnnounce(annonce, Color.Yellow);
                entregado = true;
            }
            #endregion

            #region Glangaf el Gris
            var seBuscaItemGlangaf = Singleton<ItemManager>.Instance.TryGetTemplate(14999);
            var Glangaf = character.Inventory.TryGetItem(seBuscaItemGlangaf);

            if (Glangaf != null && character.AlignmentSide != AlignmentSideEnum.ALIGNMENT_NEUTRAL)
            {
                string ali = "Mercenarias";
                if (character.AlignmentSide == AlignmentSideEnum.ALIGNMENT_ANGEL)
                    ali = "Bontarianas";
                if (character.AlignmentSide == AlignmentSideEnum.ALIGNMENT_EVIL)
                    ali = "Brakmarias";

                string annonce = character.Name + " ha entregado a Glangaf el Gris a las autoridades " + ali + ".";

                character.SendServerMessage("Has ganado 421389 kamas.");
                character.Inventory.AddKamas(421389);

                character.SendServerMessage("Has ganado 800000 puntos de experencia.");
                character.AddExperience(800000);

                character.SendServerMessage("Has ganado 425 Chapas.");
                var chapeaus = ItemManager.Instance.CreatePlayerItem(character, 10275, 425, true);
                character.Inventory.AddItem(chapeaus);

                character.SendServerMessage("Has ganado 800 puntos de honor.");
                character.AddHonor(800);

                character.Inventory.RemoveItem(Glangaf, 1);
                Singleton<World>.Instance.SendAnnounce(annonce, Color.Yellow);
                entregado = true;
            }
            #endregion

            #region Cáspar
            var seBuscaItemCaspar = Singleton<ItemManager>.Instance.TryGetTemplate(15000);
            var Caspar = character.Inventory.TryGetItem(seBuscaItemCaspar);

            if (Caspar != null && character.AlignmentSide != AlignmentSideEnum.ALIGNMENT_NEUTRAL)
            {
                string ali = "Mercenarias";
                if (character.AlignmentSide == AlignmentSideEnum.ALIGNMENT_ANGEL)
                    ali = "Bontarianas";
                if (character.AlignmentSide == AlignmentSideEnum.ALIGNMENT_EVIL)
                    ali = "Brakmarias";

                string annonce = character.Name + " ha entregado a Cáspar a las autoridades " + ali + ".";

                character.SendServerMessage("Has ganado 469857 kamas.");
                character.Inventory.AddKamas(469857);

                character.SendServerMessage("Has ganado 850000 puntos de experencia.");
                character.AddExperience(850000);

                character.SendServerMessage("Has ganado 450 Chapas.");
                var chapeaus = ItemManager.Instance.CreatePlayerItem(character, 10275, 450, true);
                character.Inventory.AddItem(chapeaus);

                character.SendServerMessage("Has ganado 850 puntos de honor.");
                character.AddHonor(850);

                character.Inventory.RemoveItem(Caspar, 1);
                Singleton<World>.Instance.SendAnnounce(annonce, Color.Yellow);
                entregado = true;
            }
            #endregion

            #region Carter Pillar
            var seBuscaItemCarter = Singleton<ItemManager>.Instance.TryGetTemplate(15001);
            var Carter = character.Inventory.TryGetItem(seBuscaItemCarter);

            if (Carter != null && character.AlignmentSide != AlignmentSideEnum.ALIGNMENT_NEUTRAL)
            {
                string ali = "Mercenarias";
                if (character.AlignmentSide == AlignmentSideEnum.ALIGNMENT_ANGEL)
                    ali = "Bontarianas";
                if (character.AlignmentSide == AlignmentSideEnum.ALIGNMENT_EVIL)
                    ali = "Brakmarias";

                string annonce = character.Name + " ha entregado a Carter Pillar a las autoridades " + ali + ".";

                character.SendServerMessage("Has ganado 499999 kamas.");
                character.Inventory.AddKamas(499999);

                character.SendServerMessage("Has ganado 900000 puntos de experencia.");
                character.AddExperience(850000);

                character.SendServerMessage("Has ganado 475 Chapas.");
                var chapeaus = ItemManager.Instance.CreatePlayerItem(character, 10275, 475, true);
                character.Inventory.AddItem(chapeaus);

                character.SendServerMessage("Has ganado 900 puntos de honor.");
                character.AddHonor(900);

                character.Inventory.RemoveItem(Carter, 1);
                Singleton<World>.Instance.SendAnnounce(annonce, Color.Yellow);
                entregado = true;
            }
            #endregion

            #region Sin Rostro
            var seBuscaItemSinRostro = Singleton<ItemManager>.Instance.TryGetTemplate(15002);
            var SinRostro = character.Inventory.TryGetItem(seBuscaItemSinRostro);

            if (SinRostro != null && character.AlignmentSide != AlignmentSideEnum.ALIGNMENT_NEUTRAL)
            {
                string ali = "Mercenarias";
                if (character.AlignmentSide == AlignmentSideEnum.ALIGNMENT_ANGEL)
                    ali = "Bontarianas";
                if (character.AlignmentSide == AlignmentSideEnum.ALIGNMENT_EVIL)
                    ali = "Brakmarias";

                string annonce = character.Name + " ha entregado a Sin Rostro a las autoridades " + ali + ".";

                character.SendServerMessage("Has ganado 536411 kamas.");
                character.Inventory.AddKamas(536411);

                character.SendServerMessage("Has ganado 950000 puntos de experencia.");
                character.AddExperience(950000);

                character.SendServerMessage("Has ganado 500 Chapas.");
                var chapeaus = ItemManager.Instance.CreatePlayerItem(character, 10275, 500, true);
                character.Inventory.AddItem(chapeaus);

                character.SendServerMessage("Has ganado 950 puntos de honor.");
                character.AddHonor(950);

                character.Inventory.RemoveItem(SinRostro, 1);
                Singleton<World>.Instance.SendAnnounce(annonce, Color.Yellow);
                entregado = true;
            }
            #endregion

            #region Ultratumbarrayder
            var seBuscaItemUltratumbarrayder = Singleton<ItemManager>.Instance.TryGetTemplate(15003);
            var Ultratumbarrayder = character.Inventory.TryGetItem(seBuscaItemUltratumbarrayder);

            if (Ultratumbarrayder != null && character.AlignmentSide != AlignmentSideEnum.ALIGNMENT_NEUTRAL)
            {
                string ali = "Mercenarias";
                if (character.AlignmentSide == AlignmentSideEnum.ALIGNMENT_ANGEL)
                    ali = "Bontarianas";
                if (character.AlignmentSide == AlignmentSideEnum.ALIGNMENT_EVIL)
                    ali = "Brakmarias";

                string annonce = character.Name + " ha entregado a Ultratumbarrayder a las autoridades " + ali + ".";

                character.SendServerMessage("Has ganado 596331 kamas.");
                character.Inventory.AddKamas(596331);

                character.SendServerMessage("Has ganado 1000000 puntos de experencia.");
                character.AddExperience(1000000);

                character.SendServerMessage("Has ganado 550 Chapas.");
                var chapeaus = ItemManager.Instance.CreatePlayerItem(character, 10275, 550, true);
                character.Inventory.AddItem(chapeaus);

                character.SendServerMessage("Has ganado 1000 puntos de honor.");
                character.AddHonor(1000);

                character.Inventory.RemoveItem(Ultratumbarrayder, 1);
                Singleton<World>.Instance.SendAnnounce(annonce, Color.Yellow);
                entregado = true;
            }
            #endregion


            if (!entregado)
            {
                character.SendServerMessage($"No tienes a ningún malechor para entregar a las autoridades.");
            }

            character.RefreshActor();

            return true;
        }



    }
}