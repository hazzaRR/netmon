using System;
using Tmds.DBus.Protocol;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace NetworkManager.DBus;
partial class IP4Config : NetworkManagerObject
{
    private const string __Interface = "org.freedesktop.NetworkManager.IP4Config";
    public IP4Config(NetworkManagerService service, ObjectPath path) : base(service, path)
    { }
    public Task<uint[][]> GetAddressesAsync()
        => this.Connection.CallMethodAsync(CreateGetPropertyMessage(__Interface, "Addresses"), (Message m, object? s) => ReadMessage_v_aau(m, (NetworkManagerObject)s!), this);
    public Task<Dictionary<string, VariantValue>[]> GetAddressDataAsync()
        => this.Connection.CallMethodAsync(CreateGetPropertyMessage(__Interface, "AddressData"), (Message m, object? s) => ReadMessage_v_aaesv(m, (NetworkManagerObject)s!), this);
    public Task<string> GetGatewayAsync()
        => this.Connection.CallMethodAsync(CreateGetPropertyMessage(__Interface, "Gateway"), (Message m, object? s) => ReadMessage_v_s(m, (NetworkManagerObject)s!), this);
    public Task<uint[][]> GetRoutesAsync()
        => this.Connection.CallMethodAsync(CreateGetPropertyMessage(__Interface, "Routes"), (Message m, object? s) => ReadMessage_v_aau(m, (NetworkManagerObject)s!), this);
    public Task<Dictionary<string, VariantValue>[]> GetRouteDataAsync()
        => this.Connection.CallMethodAsync(CreateGetPropertyMessage(__Interface, "RouteData"), (Message m, object? s) => ReadMessage_v_aaesv(m, (NetworkManagerObject)s!), this);
    public Task<Dictionary<string, VariantValue>[]> GetNameserverDataAsync()
        => this.Connection.CallMethodAsync(CreateGetPropertyMessage(__Interface, "NameserverData"), (Message m, object? s) => ReadMessage_v_aaesv(m, (NetworkManagerObject)s!), this);
    public Task<uint[]> GetNameserversAsync()
        => this.Connection.CallMethodAsync(CreateGetPropertyMessage(__Interface, "Nameservers"), (Message m, object? s) => ReadMessage_v_au(m, (NetworkManagerObject)s!), this);
    public Task<string[]> GetDomainsAsync()
        => this.Connection.CallMethodAsync(CreateGetPropertyMessage(__Interface, "Domains"), (Message m, object? s) => ReadMessage_v_as(m, (NetworkManagerObject)s!), this);
    public Task<string[]> GetSearchesAsync()
        => this.Connection.CallMethodAsync(CreateGetPropertyMessage(__Interface, "Searches"), (Message m, object? s) => ReadMessage_v_as(m, (NetworkManagerObject)s!), this);
    public Task<string[]> GetDnsOptionsAsync()
        => this.Connection.CallMethodAsync(CreateGetPropertyMessage(__Interface, "DnsOptions"), (Message m, object? s) => ReadMessage_v_as(m, (NetworkManagerObject)s!), this);
    public Task<int> GetDnsPriorityAsync()
        => this.Connection.CallMethodAsync(CreateGetPropertyMessage(__Interface, "DnsPriority"), (Message m, object? s) => ReadMessage_v_i(m, (NetworkManagerObject)s!), this);
    public Task<string[]> GetWinsServerDataAsync()
        => this.Connection.CallMethodAsync(CreateGetPropertyMessage(__Interface, "WinsServerData"), (Message m, object? s) => ReadMessage_v_as(m, (NetworkManagerObject)s!), this);
    public Task<uint[]> GetWinsServersAsync()
        => this.Connection.CallMethodAsync(CreateGetPropertyMessage(__Interface, "WinsServers"), (Message m, object? s) => ReadMessage_v_au(m, (NetworkManagerObject)s!), this);
    public Task<IP4ConfigProperties> GetPropertiesAsync()
    {
        return this.Connection.CallMethodAsync(CreateGetAllPropertiesMessage(__Interface), (Message m, object? s) => ReadMessage(m, (NetworkManagerObject)s!), this);
        static IP4ConfigProperties ReadMessage(Message message, NetworkManagerObject _)
        {
            var reader = message.GetBodyReader();
            return ReadProperties(ref reader);
        }
    }
    public ValueTask<IDisposable> WatchPropertiesChangedAsync(Action<Exception?, PropertyChanges<IP4ConfigProperties>> handler, bool emitOnCapturedContext = true, ObserverFlags flags = ObserverFlags.None)
    {
        return base.WatchPropertiesChangedAsync(__Interface, (Message m, object? s) => ReadMessage(m, (NetworkManagerObject)s!), handler, emitOnCapturedContext, flags);
        static PropertyChanges<IP4ConfigProperties> ReadMessage(Message message, NetworkManagerObject _)
        {
            var reader = message.GetBodyReader();
            reader.ReadString(); // interface
            List<string> changed = new(), invalidated = new();
            return new PropertyChanges<IP4ConfigProperties>(ReadProperties(ref reader, changed), ReadInvalidated(ref reader), changed.ToArray());
        }
        static string[] ReadInvalidated(ref Reader reader)
        {
            List<string>? invalidated = null;
            ArrayEnd arrayEnd = reader.ReadArrayStart(DBusType.String);
            while (reader.HasNext(arrayEnd))
            {
                invalidated ??= new();
                var property = reader.ReadString();
                switch (property)
                {
                    case "Addresses": invalidated.Add("Addresses"); break;
                    case "AddressData": invalidated.Add("AddressData"); break;
                    case "Gateway": invalidated.Add("Gateway"); break;
                    case "Routes": invalidated.Add("Routes"); break;
                    case "RouteData": invalidated.Add("RouteData"); break;
                    case "NameserverData": invalidated.Add("NameserverData"); break;
                    case "Nameservers": invalidated.Add("Nameservers"); break;
                    case "Domains": invalidated.Add("Domains"); break;
                    case "Searches": invalidated.Add("Searches"); break;
                    case "DnsOptions": invalidated.Add("DnsOptions"); break;
                    case "DnsPriority": invalidated.Add("DnsPriority"); break;
                    case "WinsServerData": invalidated.Add("WinsServerData"); break;
                    case "WinsServers": invalidated.Add("WinsServers"); break;
                }
            }
            return invalidated?.ToArray() ?? Array.Empty<string>();
        }
    }
    private static IP4ConfigProperties ReadProperties(ref Reader reader, List<string>? changedList = null)
    {
        var props = new IP4ConfigProperties();
        ArrayEnd arrayEnd = reader.ReadArrayStart(DBusType.Struct);
        while (reader.HasNext(arrayEnd))
        {
            var property = reader.ReadString();
            switch (property)
            {
                case "Addresses":
                    reader.ReadSignature("aau"u8);
                    props.Addresses = ReadType_aau(ref reader);
                    changedList?.Add("Addresses");
                    break;
                case "AddressData":
                    reader.ReadSignature("aa{sv}"u8);
                    props.AddressData = ReadType_aaesv(ref reader);
                    changedList?.Add("AddressData");
                    break;
                case "Gateway":
                    reader.ReadSignature("s"u8);
                    props.Gateway = reader.ReadString();
                    changedList?.Add("Gateway");
                    break;
                case "Routes":
                    reader.ReadSignature("aau"u8);
                    props.Routes = ReadType_aau(ref reader);
                    changedList?.Add("Routes");
                    break;
                case "RouteData":
                    reader.ReadSignature("aa{sv}"u8);
                    props.RouteData = ReadType_aaesv(ref reader);
                    changedList?.Add("RouteData");
                    break;
                case "NameserverData":
                    reader.ReadSignature("aa{sv}"u8);
                    props.NameserverData = ReadType_aaesv(ref reader);
                    changedList?.Add("NameserverData");
                    break;
                case "Nameservers":
                    reader.ReadSignature("au"u8);
                    props.Nameservers = reader.ReadArrayOfUInt32();
                    changedList?.Add("Nameservers");
                    break;
                case "Domains":
                    reader.ReadSignature("as"u8);
                    props.Domains = reader.ReadArrayOfString();
                    changedList?.Add("Domains");
                    break;
                case "Searches":
                    reader.ReadSignature("as"u8);
                    props.Searches = reader.ReadArrayOfString();
                    changedList?.Add("Searches");
                    break;
                case "DnsOptions":
                    reader.ReadSignature("as"u8);
                    props.DnsOptions = reader.ReadArrayOfString();
                    changedList?.Add("DnsOptions");
                    break;
                case "DnsPriority":
                    reader.ReadSignature("i"u8);
                    props.DnsPriority = reader.ReadInt32();
                    changedList?.Add("DnsPriority");
                    break;
                case "WinsServerData":
                    reader.ReadSignature("as"u8);
                    props.WinsServerData = reader.ReadArrayOfString();
                    changedList?.Add("WinsServerData");
                    break;
                case "WinsServers":
                    reader.ReadSignature("au"u8);
                    props.WinsServers = reader.ReadArrayOfUInt32();
                    changedList?.Add("WinsServers");
                    break;
                default:
                    reader.ReadVariantValue();
                    break;
            }
        }
        return props;
    }
}