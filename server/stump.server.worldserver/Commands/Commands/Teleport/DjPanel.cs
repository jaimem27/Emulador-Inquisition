using Stump.Core.Reflection;
using Stump.DofusProtocol.Enums;
using Stump.DofusProtocol.Messages;
using Stump.DofusProtocol.Types;
using Stump.Server.BaseServer.Commands;
using Stump.Server.BaseServer.Network;
using Stump.Server.WorldServer.Commands.Trigger;
using Stump.Server.WorldServer.Database.World;
using Stump.Server.WorldServer.Game;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using Stump.Server.WorldServer.Game.Dialogs.Interactives;
using Stump.Server.WorldServer.Game.Interactives;
using Stump.Server.WorldServer.Game.Maps;
using Stump.Server.WorldServer.Game.Maps.Cells;
using Stump.Server.WorldServer.Handlers.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Stump.Server.WorldServer.Commands.Commands.Teleport
{
    public class DjPanel : CommandBase
    {
        public DjPanel()
        {
            Aliases = new[] { "dj", "DJ" };
            RequiredRole = RoleEnum.GameMaster;
            Description = "Téléportation aux donjons du serveur.";
        }

        public override void Execute(TriggerBase trigger)
        {
            Dictionary<Map, int> destinations = new Dictionary<Map, int>();
            destinations.Add(World.Instance.GetMap(152829952), 437);
            destinations.Add(World.Instance.GetMap(121373185), 464);
            destinations.Add(World.Instance.GetMap(190449664), 437);
            destinations.Add(World.Instance.GetMap(193725440), 520);
            destinations.Add(World.Instance.GetMap(146675712), 491);
            destinations.Add(World.Instance.GetMap(163578368), 456);
            destinations.Add(World.Instance.GetMap(87033344), 417);
            destinations.Add(World.Instance.GetMap(94110720), 443);
            destinations.Add(World.Instance.GetMap(96338946), 480);
            destinations.Add(World.Instance.GetMap(181665792), 552);
            destinations.Add(World.Instance.GetMap(155713536), 505);
            destinations.Add(World.Instance.GetMap(187432960), 258);
            destinations.Add(World.Instance.GetMap(55050240), 284);
            destinations.Add(World.Instance.GetMap(17564931), 536);
            destinations.Add(World.Instance.GetMap(184690945), 534);
            destinations.Add(World.Instance.GetMap(87295489), 422);
            destinations.Add(World.Instance.GetMap(104595969), 472);
            destinations.Add(World.Instance.GetMap(64749568), 478);
            destinations.Add(World.Instance.GetMap(106954752), 464);
            destinations.Add(World.Instance.GetMap(56098816), 490);
            destinations.Add(World.Instance.GetMap(40108544), 476);
            destinations.Add(World.Instance.GetMap(56360960), 228);
            destinations.Add(World.Instance.GetMap(169345024), 464);
            destinations.Add(World.Instance.GetMap(22282240), 376);
            destinations.Add(World.Instance.GetMap(176947200), 326);
            destinations.Add(World.Instance.GetMap(157286400), 375);
            destinations.Add(World.Instance.GetMap(159125512), 311);
            destinations.Add(World.Instance.GetMap(176160768), 449);
            destinations.Add(World.Instance.GetMap(109576705), 256);
            destinations.Add(World.Instance.GetMap(110362624), 221);
            destinations.Add(World.Instance.GetMap(98566657), 509);
            destinations.Add(World.Instance.GetMap(166986752), 533);
            destinations.Add(World.Instance.GetMap(72351744), 465);
            destinations.Add(World.Instance.GetMap(118226944), 508);
            destinations.Add(World.Instance.GetMap(149684224), 421);
            destinations.Add(World.Instance.GetMap(149423104), 422);
            destinations.Add(World.Instance.GetMap(79430145), 254);
            destinations.Add(World.Instance.GetMap(22808576), 456);
            destinations.Add(World.Instance.GetMap(27787264), 379);
            destinations.Add(World.Instance.GetMap(125831681), 464);
            destinations.Add(World.Instance.GetMap(96338948), 312);
            destinations.Add(World.Instance.GetMap(89391104), 246);
            destinations.Add(World.Instance.GetMap(57148161), 25);
            destinations.Add(World.Instance.GetMap(157548544), 522);
            destinations.Add(World.Instance.GetMap(116392448), 464);
            destinations.Add(World.Instance.GetMap(34473474), 464);
            destinations.Add(World.Instance.GetMap(34472450), 408);
            destinations.Add(World.Instance.GetMap(96994817), 423);
            destinations.Add(World.Instance.GetMap(174064128), 521);
            destinations.Add(World.Instance.GetMap(132907008), 533);
            destinations.Add(World.Instance.GetMap(149160960), 424);
            destinations.Add(World.Instance.GetMap(157024256), 359);
            destinations.Add(World.Instance.GetMap(174326272), 491);
            destinations.Add(World.Instance.GetMap(21495808), 210);
            destinations.Add(World.Instance.GetMap(116654593), 491);
            destinations.Add(World.Instance.GetMap(27000832), 535);
            destinations.Add(World.Instance.GetMap(5243139), 336);
            destinations.Add(World.Instance.GetMap(26738688), 505);
            destinations.Add(World.Instance.GetMap(130286592), 516);
            destinations.Add(World.Instance.GetMap(143138823), 515);
            destinations.Add(World.Instance.GetMap(161743872), 504);
            destinations.Add(World.Instance.GetMap(107216896), 554);

            var gameTrigger = trigger as GameTrigger;
            DungsDialog s = new DungsDialog(gameTrigger.Character, destinations);
            s.Open();
        }
    }
}