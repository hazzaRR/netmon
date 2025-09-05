using System;
using Tmds.DBus.Protocol;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace NetworkManager.DBus;

record NetworkManagerProperties
{
    public ObjectPath[] Devices { get; set; } = default!;
    public ObjectPath[] AllDevices { get; set; } = default!;
    public ObjectPath[] Checkpoints { get; set; } = default!;
    public bool NetworkingEnabled { get; set; } = default!;
    public bool WirelessEnabled { get; set; } = default!;
    public bool WirelessHardwareEnabled { get; set; } = default!;
    public bool WwanEnabled { get; set; } = default!;
    public bool WwanHardwareEnabled { get; set; } = default!;
    public bool WimaxEnabled { get; set; } = default!;
    public bool WimaxHardwareEnabled { get; set; } = default!;
    public uint RadioFlags { get; set; } = default!;
    public ObjectPath[] ActiveConnections { get; set; } = default!;
    public ObjectPath PrimaryConnection { get; set; } = default!;
    public string PrimaryConnectionType { get; set; } = default!;
    public uint Metered { get; set; } = default!;
    public ObjectPath ActivatingConnection { get; set; } = default!;
    public bool Startup { get; set; } = default!;
    public string Version { get; set; } = default!;
    public uint[] VersionInfo { get; set; } = default!;
    public uint[] Capabilities { get; set; } = default!;
    public uint State { get; set; } = default!;
    public uint Connectivity { get; set; } = default!;
    public bool ConnectivityCheckAvailable { get; set; } = default!;
    public bool ConnectivityCheckEnabled { get; set; } = default!;
    public string ConnectivityCheckUri { get; set; } = default!;
    public Dictionary<string, VariantValue> GlobalDnsConfiguration { get; set; } = default!;
}