using System;
using Tmds.DBus.Protocol;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace NetworkManager.DBus;
record WiredProperties
{
    public string HwAddress { get; set; } = default!;
    public string PermHwAddress { get; set; } = default!;
    public uint Speed { get; set; } = default!;
    public string[] S390Subchannels { get; set; } = default!;
    public bool Carrier { get; set; } = default!;
}