using System;
using Tmds.DBus.Protocol;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace NetworkManager.DBus;
partial class NetworkManagerService
{
    public Tmds.DBus.Protocol.Connection Connection { get; }
    public string Destination { get; }
    public NetworkManagerService(Tmds.DBus.Protocol.Connection connection, string destination)
        => (Connection, Destination) = (connection, destination);
    public ObjectManager CreateObjectManager(ObjectPath path) => new ObjectManager(this, path);
    public NetworkManager CreateNetworkManager(ObjectPath path) => new NetworkManager(this, path);
    public DHCP4Config CreateDHCP4Config(ObjectPath path) => new DHCP4Config(this, path);
    public IP4Config CreateIP4Config(ObjectPath path) => new IP4Config(this, path);
    public Active CreateActive(ObjectPath path) => new Active(this, path);
    public AccessPoint CreateAccessPoint(ObjectPath path) => new AccessPoint(this, path);
    public Statistics CreateStatistics(ObjectPath path) => new Statistics(this, path);
    public Device CreateDevice(ObjectPath path) => new Device(this, path);
    public Wired CreateWired(ObjectPath path) => new Wired(this, path);
    public Wireless CreateWireless(ObjectPath path) => new Wireless(this, path);
    public Loopback CreateLoopback(ObjectPath path) => new Loopback(this, path);
    public WifiP2P CreateWifiP2P(ObjectPath path) => new WifiP2P(this, path);
    public AgentManager CreateAgentManager(ObjectPath path) => new AgentManager(this, path);
    public DnsManager CreateDnsManager(ObjectPath path) => new DnsManager(this, path);
    public IP6Config CreateIP6Config(ObjectPath path) => new IP6Config(this, path);
    public Settings CreateSettings(ObjectPath path) => new Settings(this, path);
    public Connection CreateConnection(ObjectPath path) => new Connection(this, path);
}