using System;
using Tmds.DBus.Protocol;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace NetworkManager.DBus;
record StatisticsProperties
{
    public uint RefreshRateMs { get; set; } = default!;
    public ulong TxBytes { get; set; } = default!;
    public ulong RxBytes { get; set; } = default!;
}