using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NLog;
using Quartz;
using Quartz.Impl;
using Stump.Core.Reflection;
using Stump.DofusProtocol.Enums;
using Stump.Server.BaseServer.Initialization;

namespace Stump.Server.WorldServer.Game.Prisms {
    public class KoHManager : Singleton<KoHManager> {
        private List<int> _confictSubAreas = new List<int> ();
        private int _count = 1;
        protected static readonly Logger logger = LogManager.GetCurrentClassLogger ();

        public IScheduler Scheduler { get; private set; }

        //How it's this manager?
        //First of all the prism need to be weakenend (first figth defeated) or vulnerable (already on KoH)
        //if weakened make a Task to run when date it's already (maybe use Quartz.Net or something)
        //When vulnerable activated
        //  Activate the 2 min counter for character (This it's always running to add new characters on the queue)
        //  When 2 min finished:
        //      Add the enter area characters to the actual fighters on koh
        //      send  UpdateSelfAgressableStatusMessage | status = 20 | probationtime = 0
        //      and send KoHUpdateMessage
        //  Add and action to the SubArea when enter and exit
        //  When Enter:
        //      PrismListUpdate = state vulnerable (5) and info
        //      UpdateSelfAgressableStatusMessage 
        //          if can enter(not died) | status = 23 | probationtime = now - counter time 2min I think or 1:30 idk
        //          if dead status = 21
        //  When Exit:
        //      Delete character from active fighters!, if enter still same

        // METHODS
        [Initialization (InitializationPass.Last)]
        public void Initialize () {
            // First we must get a reference to a scheduler
            ISchedulerFactory sf = new StdSchedulerFactory ();
            Scheduler = sf.GetScheduler ();
            foreach (var prism in PrismManager.Instance.Prisms)
                AddSubAreaInConflictAsync (prism);
            Scheduler.Start ();
        }

        public async void AddSubAreaInConflictAsync (PrismNpc prism) {
            if (_confictSubAreas.Contains (prism.SubArea.Id))
                return;
            Console.WriteLine ($"Prism state : {prism.State}");
            Console.WriteLine ($"Prism map : {prism.Map.Id}");
            switch (prism.State) {
                case PrismStateEnum.PRISM_STATE_VULNERABLE:
                    prism.NextDate = DateTime.Now;
                    try { await Task.Run (() => BeginKoH (prism)); } catch (Exception ex) {

                        logger.Error ("Prisma do mapa" + prism.Map.Id + "Erro no estado vulneravel!");

                        logger.Error (ex.Message);
                    }
                    break;
                case PrismStateEnum.PRISM_STATE_WEAKENED:
                    //else add to Quart.Net to begin Koh
                    try { await Task.Run (() => Instance.AddToQueue (prism)); } catch (Exception ex) {
                        logger.Error ("Prisma do mapa" + prism.Map.Id + "Erro no estado Weakened");
                        logger.Error (ex.Message);
                    }
                    break;
                default:
                    //return. Why we get another? not posible :p
                    return;
            }
            _confictSubAreas.Add (prism.SubArea.Id);
        }

        public async void BeginKoH (PrismNpc prism) {
            //bind the actions etc...
            var test = _count; //because are await so this crashing !
            _count += 1;
            var job = JobBuilder.Create<PrismVulneranility> ()
                .WithIdentity ($"job{test}", "group1")
                .Build ();

            job.JobDataMap.Add ("prism", prism);

            var trigger = (ISimpleTrigger) TriggerBuilder.Create ()
                .WithIdentity ($"trigger{test}", "group1")
                .StartAt (new DateTimeOffset (DateTime.Now.AddSeconds (5))) //Add more time, for example if forced reboot give time to players for reconnect
                .Build ();

            // _count += 1;
            await Task.Run (() => {

                try {
                    Scheduler.ScheduleJob (job, trigger);
                } catch (Exception ex) {
                    logger.Error ("Erro ao começar o begginkoh no prisma " + prism.Map.Id + " Estado:" + prism.State);
                    logger.Error (ex.Message);

                    return false;
                }
                return true;
            });

        }

        public async void AddToQueue (PrismNpc prism) {
            var test = _count; //because are await so this crashing !
            _count += 1;
            var job = JobBuilder.Create<PrismVulneranility> ()
                .WithIdentity ($"job{test}", "group1")
                .Build ();

            job.JobDataMap.Add ("prism", prism);

            var trigger = (ISimpleTrigger) TriggerBuilder.Create ()
                .WithIdentity ($"trigger{test}", "group1")
                .StartAt (prism.NextDate)
                .Build ();

            //  _count += 1;
            // await Task.Run(() => Scheduler.ScheduleJob(job, trigger));
            await Task.Run (() => {

                try {
                    Scheduler.ScheduleJob (job, trigger);
                } catch (Exception ex) {
                    logger.Error ("Erro ao AddToQueue no prisma " + prism.Map.Id + " Estado:" + prism.State);
                    logger.Error (ex.Message);

                }

            });
        }
    }
}