using System;
using Tmds.DBus.Protocol;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace NetworkManager.DBus;
record WifiP2PProperties
{
    public string HwAddress { get; set; } = default!;
    public ObjectPath[] Peers { get; set; } = default!;
}