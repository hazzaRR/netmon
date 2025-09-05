using System;
using Tmds.DBus.Protocol;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace NetworkManager.DBus;
enum DeviceState : uint
{
Unknown = 0,
Unmanaged = 10,
Unavailable = 20,
Disconnected = 30,
Prepare = 40,
Config = 50,
NeedAuth = 60,
IpConfig = 70,
IpCheck = 80,
Secondaries = 90,
Activated = 100,
Deactivating = 110,
Failed = 120
}