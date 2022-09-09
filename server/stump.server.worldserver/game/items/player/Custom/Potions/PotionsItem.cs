﻿using Stump.DofusProtocol.Enums;
using Stump.DofusProtocol.Messages;
using Stump.Server.WorldServer.Database.Items;
using Stump.Server.WorldServer.Database.World;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Mounts;
using Stump.Server.WorldServer.Game.Effects.Instances;

namespace Stump.Server.WorldServer.Game.Items.Player.Custom
{
    [ItemId(ItemIdEnum.POTION_DE_CHANGEMENT_DE_NOM_10860)]
    public class NameChangePotion : BasePlayerItem
    {
        public NameChangePotion(Character owner, PlayerItemRecord record)
            : base(owner, record)
        {
        }

        public override uint UseItem(int amount = 1, Cell targetCell = null, Character target = null)
        {
            if ((Owner.Record.MandatoryChanges & (sbyte)CharacterRemodelingEnum.CHARACTER_REMODELING_NAME)
                == (sbyte)CharacterRemodelingEnum.CHARACTER_REMODELING_NAME)
            {
                Owner.SendInformationMessage(TextInformationTypeEnum.TEXT_INFORMATION_POPUP, 43);
                return 0;
            }

            Owner.Record.MandatoryChanges |= (sbyte)CharacterRemodelingEnum.CHARACTER_REMODELING_NAME;
            Owner.Record.PossibleChanges |= (sbyte)CharacterRemodelingEnum.CHARACTER_REMODELING_NAME;

            Owner.SendSystemMessage(41, false);

            return 1;
        }
    }

    [ItemId(ItemIdEnum.POTION_DE_CHANGEMENT_DES_COULEURS_10861)]
    public class ColourChangePotion : BasePlayerItem
    {
        public ColourChangePotion(Character owner, PlayerItemRecord record)
            : base(owner, record)
        {
        }

        public override uint UseItem(int amount = 1, Cell targetCell = null, Character target = null)
        {
            if ((Owner.Record.MandatoryChanges & (sbyte)CharacterRemodelingEnum.CHARACTER_REMODELING_COLORS)
                == (sbyte)CharacterRemodelingEnum.CHARACTER_REMODELING_COLORS)
            {
                Owner.SendSystemMessage(43, false);
                return 0;
            }

            Owner.Record.MandatoryChanges |= (sbyte)CharacterRemodelingEnum.CHARACTER_REMODELING_COLORS;
            Owner.Record.PossibleChanges |= (sbyte)CharacterRemodelingEnum.CHARACTER_REMODELING_COLORS;

            Owner.SendSystemMessage(42, false);

            return 1;
        }
    }

    [ItemId(ItemIdEnum.POTION_DE_CHANGEMENT_DE_VISAGE_13518)]
    public class LookChangePotion : BasePlayerItem
    {
        public LookChangePotion(Character owner, PlayerItemRecord record)
            : base(owner, record)
        {
        }

        public override uint UseItem(int amount = 1, Cell targetCell = null, Character target = null)
        {
            if ((Owner.Record.MandatoryChanges & (sbyte)CharacterRemodelingEnum.CHARACTER_REMODELING_COSMETIC)
                == (sbyte)CharacterRemodelingEnum.CHARACTER_REMODELING_COSMETIC)
            {
                Owner.SendSystemMessage(43, false);
                return 0;
            }

            Owner.Record.MandatoryChanges |= (sbyte)CharacterRemodelingEnum.CHARACTER_REMODELING_COSMETIC;
            Owner.Record.PossibleChanges |= (sbyte)CharacterRemodelingEnum.CHARACTER_REMODELING_COSMETIC;
            Owner.Record.PossibleChanges |= (sbyte)CharacterRemodelingEnum.CHARACTER_REMODELING_COLORS;

            Owner.SendSystemMessage(58, false);

            return 1;
        }
    }

    [ItemId(ItemIdEnum.POTION_DE_CHANGEMENT_DE_SEXE_10862)]
    public class SexChangePotion : BasePlayerItem
    {
        public SexChangePotion(Character owner, PlayerItemRecord record)
            : base(owner, record)
        {
        }

        public override uint UseItem(int amount = 1, Cell targetCell = null, Character target = null)
        {
            if ((Owner.Record.MandatoryChanges & (sbyte)CharacterRemodelingEnum.CHARACTER_REMODELING_GENDER)
                == (sbyte)CharacterRemodelingEnum.CHARACTER_REMODELING_GENDER)
            {
                Owner.SendInformationMessage(TextInformationTypeEnum.TEXT_INFORMATION_POPUP, 43);
                Owner.SendSystemMessage(43, false);
                return 0;
            }

            Owner.Record.MandatoryChanges |= (sbyte)CharacterRemodelingEnum.CHARACTER_REMODELING_GENDER;
            Owner.Record.PossibleChanges |= (sbyte)CharacterRemodelingEnum.CHARACTER_REMODELING_GENDER;
            Owner.Record.PossibleChanges |= (sbyte)CharacterRemodelingEnum.CHARACTER_REMODELING_COSMETIC;
            Owner.Record.PossibleChanges |= (sbyte)CharacterRemodelingEnum.CHARACTER_REMODELING_COLORS;

            Owner.SendSystemMessage(44, false);

            return 1;
        }
    }

    [ItemId(ItemIdEnum.POTION_DE_CHANGEMENT_DE_CLASSE_16147)]
    public class ClassChangePotion : BasePlayerItem
    {
        public ClassChangePotion(Character owner, PlayerItemRecord record)
            : base(owner, record)
        {
        }

        public override uint UseItem(int amount = 1, Cell targetCell = null, Character target = null)
        {
            if ((Owner.Record.MandatoryChanges & (sbyte)CharacterRemodelingEnum.CHARACTER_REMODELING_BREED)
                == (sbyte)CharacterRemodelingEnum.CHARACTER_REMODELING_BREED)
            {
                Owner.SendInformationMessage(TextInformationTypeEnum.TEXT_INFORMATION_POPUP, 43);
                Owner.SendSystemMessage(43, false);
                return 0;
            }

            Owner.Record.MandatoryChanges |= (sbyte)CharacterRemodelingEnum.CHARACTER_REMODELING_BREED;
            Owner.Record.PossibleChanges |= (sbyte)CharacterRemodelingEnum.CHARACTER_REMODELING_BREED;
            Owner.Record.PossibleChanges |= (sbyte)CharacterRemodelingEnum.CHARACTER_REMODELING_GENDER;
            Owner.Record.PossibleChanges |= (sbyte)CharacterRemodelingEnum.CHARACTER_REMODELING_COSMETIC;
            Owner.Record.PossibleChanges |= (sbyte)CharacterRemodelingEnum.CHARACTER_REMODELING_COLORS;

            Owner.SendSystemMessage(63, false);

            if (Owner.WorldAccount.Vip)
                return 0;

            return 1;
        }
    }

    [ItemId(ItemIdEnum.POTION_CAMELEON_30110)]
    public class CameleonMountPotion : BasePlayerItem
    {
        public CameleonMountPotion(Character owner, PlayerItemRecord record)
            : base(owner, record)
        {
        }

        public override uint UseItem(int amount = 1, Cell targetCell = null, Character target = null)
        {
            if (Owner.IsRiding)
            {
                Owner.SendServerMessage("Debes bajarte de tu montura para usar esta poción.", System.Drawing.Color.DarkOrange);
                return 0;
            }

            if (Owner.EquippedMount == null)
            {
                Owner.SendServerMessage("Debes tener una montura para usar esta poción.", System.Drawing.Color.DarkOrange);
                return 0;
            }

            if (Owner.EquippedMount.Behaviors.Contains((int)MountBehaviorEnum.Caméléone))
            {
                Owner.SendServerMessage("Esta montura ya tiene el efecto Camaleón.", System.Drawing.Color.DarkOrange);
                return 0;
            }

            Owner.EquippedMount.AddBehavior(MountBehaviorEnum.Caméléone);
            Owner.EquippedMount.Save(Actors.RolePlay.Mounts.MountManager.Instance.Database);
            Owner.SendServerMessage("Felicitaciones, tu montura ahora es Camaleón.", System.Drawing.Color.DarkOrange);
            return 1;

        }
    }

    [ItemId(10911)]//potion auto piloto(auto pilot)
    public class PotionAutoPiloto : BasePlayerItem
    {
        public PotionAutoPiloto(Character owner, PlayerItemRecord record) : base(owner, record)
        {
        }
        public override uint UseItem(int amount = 1, Cell targetCell = null, Character target = null)
        {
            if (Owner.EquippedMount != null)
            {
                if (!Owner.EquippedMount.Behaviors.Contains((int)10))
                {
                    Owner.SendServerMessage("Felicitaciones, tu montura ahora es tiene la habilidad de ir solita :3.", System.Drawing.Color.DarkOrange);
                    Owner.EquippedMount.Record.Behaviors.Add((int)10);
                    Owner.EquippedMount.Save(MountManager.Instance.Database);
                    Owner.UpdateLook();
                    Owner.EquippedMount.RefreshMount();

                    return 1;
                }
                else
                {

                    switch (Owner.Account.Lang)
                    {
                        case "fr":
                            Owner.OpenPopup("Vous ne pouvez pas utiliser cette potion, votre dragodinde a déjà un pilote automatique !");
                            break;
                        case "es":
                            Owner.OpenPopup("¡No puedes usar esta poción, tu dragopavo ya tiene piloto automático!");
                            break;
                        case "en":
                            Owner.OpenPopup("You can't use this potion, your dragoturkey already has auto pilot!");
                            break;
                        default:
                            Owner.OpenPopup("Você não pode usar esta poção, seu dragossauro já tem auto piloto!");
                            break;
                    }
                    return 0;
                }
            }
            else
            {
                switch (Owner.Account.Lang)
                {
                    case "fr":
                        Owner.OpenPopup("Vous ne pouvez pas utiliser cette potion, vous n'avez pas de dragodinde équippée !");
                        break;
                    case "es":
                        Owner.OpenPopup("¡No puedes usar esta poción, no tienes un dragopavo equipado!");
                        break;
                    case "en":
                        Owner.OpenPopup("You cannot use this potion, you do not have a dragosaur equipped!");
                        break;
                    default:
                        Owner.OpenPopup("Você não pode usar esta poção, você não tem um dragossauro equipado!");
                        break;
                }

                return 0;
            }
        }
    }

    //[ItemId(ItemIdEnum.POTION_REMOVE_EXOTISM)]
    //public class RemoveExotismItemPotion : BasePlayerItem
    //{
    //    public RemoveExotismItemPotion(Character owner, PlayerItemRecord record)
    //        : base(owner, record)
    //    {
    //    }

    //    public override uint UseItem(int amount = 1, Cell targetCell = null, Character target = null)
    //    {
    //        foreach (var equip in Owner.Inventory.GetEquipedItems())
    //        {
    //            if (Owner.Inventory.IsExo(equip, true) != 3)
    //                Owner.Inventory.MoveItem(equip, CharacterInventoryPositionEnum.INVENTORY_POSITION_NOT_EQUIPED, true);
    //            Owner.Record.HasExoPa = false;
    //            Owner.Record.HasExoPm = false;
    //            Owner.Record.HasExoPo = false;
    //            Owner.RefreshStats();
    //        }

    //        Owner.SendServerMessage("Les effets d'exo ont été remis à zéro.", System.Drawing.Color.DarkOrange);
    //        return 1;

    //    }
    //}
}