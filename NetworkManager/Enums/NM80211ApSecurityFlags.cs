using System;
using Tmds.DBus.Protocol;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace NetworkManager.DBus;
using System;

[Flags]
public enum NM80211ApSecurityFlags
{
    /// <summary>
    /// The access point has no special security requirements.
    /// </summary>
    NM_802_11_AP_SEC_NONE = 0x00000000,

    /// <summary>
    /// 40/64-bit WEP is supported for pairwise/unicast encryption.
    /// </summary>
    NM_802_11_AP_SEC_PAIR_WEP40 = 0x00000001,

    /// <summary>
    /// 104/128-bit WEP is supported for pairwise/unicast encryption.
    /// </summary>
    NM_802_11_AP_SEC_PAIR_WEP104 = 0x00000002,

    /// <summary>
    /// TKIP is supported for pairwise/unicast encryption.
    /// </summary>
    NM_802_11_AP_SEC_PAIR_TKIP = 0x00000004,

    /// <summary>
    /// AES/CCMP is supported for pairwise/unicast encryption.
    /// </summary>
    NM_802_11_AP_SEC_PAIR_CCMP = 0x00000008,

    /// <summary>
    /// 40/64-bit WEP is supported for group/broadcast encryption.
    /// </summary>
    NM_802_11_AP_SEC_GROUP_WEP40 = 0x00000010,

    /// <summary>
    /// 104/128-bit WEP is supported for group/broadcast encryption.
    /// </summary>
    NM_802_11_AP_SEC_GROUP_WEP104 = 0x00000020,

    /// <summary>
    /// TKIP is supported for group/broadcast encryption.
    /// </summary>
    NM_802_11_AP_SEC_GROUP_TKIP = 0x00000040,

    /// <summary>
    /// AES/CCMP is supported for group/broadcast encryption.
    /// </summary>
    NM_802_11_AP_SEC_GROUP_CCMP = 0x00000080,

    /// <summary>
    /// WPA/RSN Pre-Shared Key encryption is supported.
    /// </summary>
    NM_802_11_AP_SEC_KEY_MGMT_PSK = 0x00000100,

    /// <summary>
    /// 802.1x authentication and key management is supported.
    /// </summary>
    NM_802_11_AP_SEC_KEY_MGMT_802_1X = 0x00000200
}