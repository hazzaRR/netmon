using System;
using Tmds.DBus.Protocol;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace NetworkManager.DBus;
record WirelessProperties
{
    public string HwAddress { get; set; } = default!;
    public string PermHwAddress { get; set; } = default!;
    public uint Mode { get; set; } = default!;
    public uint Bitrate { get; set; } = default!;
    public ObjectPath[] AccessPoints { get; set; } = default!;
    public ObjectPath ActiveAccessPoint { get; set; } = default!;
    public uint WirelessCapabilities { get; set; } = default!;
    public long LastScan { get; set; } = default!;
}