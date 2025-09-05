using System;
using Tmds.DBus.Protocol;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace NetworkManager.DBus;
partial class Loopback : NetworkManagerObject
{
    private const string __Interface = "org.freedesktop.NetworkManager.Device.Loopback";
    public Loopback(NetworkManagerService service, ObjectPath path) : base(service, path)
    { }
}
