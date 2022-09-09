using Stump.Server.BaseServer.Database;
using Stump.Server.WorldServer.Database.Ranks;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Npcs;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Ranks;
using System.Collections.Generic;
using System.Drawing;
using System;
using Stump.Server.WorldServer.Database.Items.Templates;
using Stump.Server.WorldServer.Game.Items;

namespace Stump.Server.WorldServer.Database.Npcs.Replies
{
    [Discriminator("RankRewardDialog", typeof(NpcReply), typeof(NpcReplyRecord))]
    class RankRewardReply : NpcReply
    {
        public RankRewardReply(NpcReplyRecord record) : base(record)
        {

        }

        public override bool Execute(Npc npc, Character character)
        {
            if (!base.Execute(npc, character))
                return false;
            var rewards = RankRewardManager.Instance.getRewardsByRank(character.CharacterRankId);
            if (rewards.Count > 0)
            {
                if (character.CharacterRankWin < 10)
                {
                    character.SendServerMessage("Necesitas un mínimo de victoria para reclamar una recompensa. Vuelve conmigo después de derrotar a diez infieles en el campo de batalla.", Color.OrangeRed);
                    return false;
                }
                var now = DateTime.Now;
                if ((character.CharacterRankReward.Month < now.Month || character.CharacterRankReward.Year < now.Year) || character.CharacterRankReward.Day < now.Day)
                {
                    Random rnd = new Random();
                    var selected = rewards[rnd.Next(rewards.Count)].Value;
                    switch (selected.Type)
                    {
                        case "Item":
                            int quantity = 1;

                            if (selected.Optional2 != null && selected.Optional2.Length > 0)
                                quantity = Int32.Parse(selected.Optional2);
                            ItemTemplate template = ItemManager.Instance.TryGetTemplate(Int32.Parse(selected.Optional1));
                            var item = ItemManager.Instance.CreatePlayerItem(character, template, quantity, true);

                            character.Inventory.AddItem(item, true);
                            character.SendServerMessage("Has obtenido: <b>" + template.Name + " (X" + quantity + ")</b>, ¡felicitaciones por tu valentía en DeathMatch! Vuelve a verme mañana.");
                            break;

                        case "Kamas":
                                character.Inventory.AddKamas((ulong)Int32.Parse(selected.Optional1));
                            character.SendServerMessage("Has obtenido: <b>" + Int32.Parse(selected.Optional1) + "</b> kamas, ¡felicitaciones por tu valentía en DeathMatch! Vuelve a verme mañana.");
                            break;

                        case "Exp":
                                character.AddExperience(Int32.Parse(selected.Optional1));
                            character.SendServerMessage("Has obtenido: <b>" + Int32.Parse(selected.Optional1) + "</b> puntos de experencia, fél¡felicitaciones por tu valentía en DeathMatch! Vuelve a verme mañana.");
                            break;
                    }
                    character.CharacterRankReward = DateTime.Now;
                }
                else
                {
                    character.SendServerMessage("Ya has reclamado tu recompensa hoy... vuelve mañana.", Color.OrangeRed);
                }
            }
            else
            {
                character.SendServerMessage("Lo siento, no hay recompensa ahora mismo para tu rango.", Color.OrangeRed);
            }

            character.LeaveDialog();
            return true;
        }
    }
}

