using Stump.Core.Collections;
using Stump.Core.Extensions;
using Stump.DofusProtocol.Enums;
using Stump.Server.BaseServer.Database;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Npcs;
using Stump.Server.WorldServer.Game.Effects.Instances;
using Stump.Server.WorldServer.Game.Items;
using System.Linq;

namespace Stump.Server.WorldServer.Database.Npcs.Replies
{
    [Discriminator("traque", typeof(NpcReply), typeof(NpcReplyRecord))]
    public class traque : NpcReply
    {
        private TimedStack<Pair<int, int>> m_pvpSeekHistory = new TimedStack<Pair<int, int>>(60 * 10);

        public traque(NpcReplyRecord record)
            : base(record)
        {
        }

        public override bool Execute(Npc npc, Character character)
        {
            if (!base.Execute(npc, character))
                return false;

            //Check if player already get a contract in the last 10 mins
           

            if (character.AlignmentSide == AlignmentSideEnum.ALIGNMENT_NEUTRAL)
            {
                character.SendServerMessage("Debes tener una alineación para comenzar una cacería.");
                return false;
            }

            if (!character.PvPEnabled)
            {
                character.SendServerMessage("Debes activar tus alas de alineación para comenzar una cacería.");
                return false;
            }

            if (character.Level < 50)
            {
                character.SendServerMessage("Debes tener un nivel mínimo de 50 para poder lanzar una cacería.");
                return false;
            }

            var target = Game.World.Instance.GetCharacters(x => character.CanAgress(x, true) == FighterRefusedReasonEnum.FIGHTER_ACCEPTED).RandomElementOrDefault();

            if (target == null)
            {
                character.SendServerMessage("Aún no hemos podido localizar un objetivo cerca de ti, inténtalo de nuevo en unos minutos.");
                return false;
            }

            foreach (var contract in character.Inventory.GetItems(x => x.Template.Id == (int)ItemIdEnum.ORDRE_DEXECUTION_10085))
                character.Inventory.RemoveItem(contract);

            var item = ItemManager.Instance.CreatePlayerItem(character, (int)ItemIdEnum.ORDRE_DEXECUTION_10085, 25);

            var seekEffect = item.Effects.FirstOrDefault(x => x.EffectId == EffectsEnum.Effect_Seek);

            if (seekEffect != null)
                item.Effects.Remove(seekEffect);

            item.Effects.Add(new EffectString(EffectsEnum.Effect_Seek, target.Name));
            item.Effects.Add(new EffectInteger(EffectsEnum.Effect_Alignment, (short)target.AlignmentSide));
            item.Effects.Add(new EffectInteger(EffectsEnum.Effect_Grade, target.AlignmentGrade));
            item.Effects.Add(new EffectInteger(EffectsEnum.Effect_Level, (short)target.Level));
            item.Effects.Add(new EffectInteger(EffectsEnum.Effect_NonExchangeable_981, 0));

            character.Inventory.AddItem(item);


            character.SendServerMessage($"Ahora tienes al jugador {target.Name} en la caza, se te han asignado 25 pergaminos de investigación para localizar a tu oponente.");

            return true;
        }
    }
}