using System;
using Tmds.DBus.Protocol;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace NetworkManager.DBus;
record ActiveProperties
{
    public ObjectPath Connection { get; set; } = default!;
    public ObjectPath SpecificObject { get; set; } = default!;
    public string Id { get; set; } = default!;
    public string Uuid { get; set; } = default!;
    public string Type { get; set; } = default!;
    public ObjectPath[] Devices { get; set; } = default!;
    public uint State { get; set; } = default!;
    public uint StateFlags { get; set; } = default!;
    public bool Default { get; set; } = default!;
    public ObjectPath Ip4Config { get; set; } = default!;
    public ObjectPath Dhcp4Config { get; set; } = default!;
    public bool Default6 { get; set; } = default!;
    public ObjectPath Ip6Config { get; set; } = default!;
    public ObjectPath Dhcp6Config { get; set; } = default!;
    public bool Vpn { get; set; } = default!;
    public ObjectPath Controller { get; set; } = default!;
    public ObjectPath Master { get; set; } = default!;
}