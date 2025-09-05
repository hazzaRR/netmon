using System;
using Tmds.DBus.Protocol;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace NetworkManager.DBus;

record DnsManagerProperties
{
    public string Mode { get; set; } = default!;
    public string RcManager { get; set; } = default!;
    public Dictionary<string, VariantValue>[] Configuration { get; set; } = default!;
}
