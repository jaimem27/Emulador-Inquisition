using System;

namespace Stump.DofusProtocol.Enums {
    [Flags]
    public enum TaxCollectorStateEnum {
        STATE_COLLECTING = 0,
        STATE_WAITING_FOR_HELP = 1,
        STATE_FIGHTING = 2
    }
}