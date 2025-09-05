using System;
using Tmds.DBus.Protocol;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace NetworkManager.DBus;

record IP6ConfigProperties
{
    public (byte[], uint, byte[])[] Addresses { get; set; } = default!;
    public Dictionary<string, VariantValue>[] AddressData { get; set; } = default!;
    public string Gateway { get; set; } = default!;
    public (byte[], uint, byte[], uint)[] Routes { get; set; } = default!;
    public Dictionary<string, VariantValue>[] RouteData { get; set; } = default!;
    public byte[][] Nameservers { get; set; } = default!;
    public string[] Domains { get; set; } = default!;
    public string[] Searches { get; set; } = default!;
    public string[] DnsOptions { get; set; } = default!;
    public int DnsPriority { get; set; } = default!;
}