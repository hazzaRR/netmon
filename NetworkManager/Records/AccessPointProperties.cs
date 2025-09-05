using System;
using Tmds.DBus.Protocol;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace NetworkManager.DBus;
record AccessPointProperties
{
    public uint Flags { get; set; } = default!;
    public uint WpaFlags { get; set; } = default!;
    public uint RsnFlags { get; set; } = default!;
    public byte[] Ssid { get; set; } = default!;
    public uint Frequency { get; set; } = default!;
    public string HwAddress { get; set; } = default!;
    public uint Mode { get; set; } = default!;
    public uint MaxBitrate { get; set; } = default!;
    public byte Strength { get; set; } = default!;
    public int LastSeen { get; set; } = default!;
}