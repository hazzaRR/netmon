using System;
using Tmds.DBus.Protocol;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace NetworkManager.DBus;
enum NM80211Mode : uint
{
    NM_802_11_MODE_UNKNOWN = 0,
    NM_802_11_MODE_ADHOC = 1,
    NM_802_11_MODE_INFRA = 2,
    NM_802_11_MODE_AP = 3
}