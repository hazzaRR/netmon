using System;
using Tmds.DBus.Protocol;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace NetworkManager.DBus;
enum NMConnectivityState : uint
{
    NM_CONNECTIVITY_UNKNOWN = 1,
    NM_CONNECTIVITY_NONE = 2,
    NM_CONNECTIVITY_PORTAL = 3,
    NM_CONNECTIVITY_LIMITED = 4,
    NM_CONNECTIVITY_FULL = 5
}