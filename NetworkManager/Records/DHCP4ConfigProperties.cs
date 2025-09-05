using System;
using Tmds.DBus.Protocol;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace NetworkManager.DBus;
record DHCP4ConfigProperties
{
    public Dictionary<string, VariantValue> Options { get; set; } = default!;
}