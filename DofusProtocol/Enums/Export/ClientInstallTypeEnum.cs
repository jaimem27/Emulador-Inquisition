using System;

namespace Stump.DofusProtocol.Enums {
    [Flags]
    public enum ClientInstallTypeEnum {
        CLIENT_INSTALL_UNKNOWN = 0,
        CLIENT_BUNDLE = 1,
        CLIENT_STREAMING = 2
    }
}