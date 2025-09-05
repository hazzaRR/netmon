using System;
using Tmds.DBus.Protocol;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace NetworkManager.DBus;
partial class IP6Config : NetworkManagerObject
{
    private const string __Interface = "org.freedesktop.NetworkManager.IP6Config";
    public IP6Config(NetworkManagerService service, ObjectPath path) : base(service, path)
    { }
    public Task<(byte[], uint, byte[])[]> GetAddressesAsync()
        => this.Connection.CallMethodAsync(CreateGetPropertyMessage(__Interface, "Addresses"), (Message m, object? s) => ReadMessage_v_arayuayz(m, (NetworkManagerObject)s!), this);
    public Task<Dictionary<string, VariantValue>[]> GetAddressDataAsync()
        => this.Connection.CallMethodAsync(CreateGetPropertyMessage(__Interface, "AddressData"), (Message m, object? s) => ReadMessage_v_aaesv(m, (NetworkManagerObject)s!), this);
    public Task<string> GetGatewayAsync()
        => this.Connection.CallMethodAsync(CreateGetPropertyMessage(__Interface, "Gateway"), (Message m, object? s) => ReadMessage_v_s(m, (NetworkManagerObject)s!), this);
    public Task<(byte[], uint, byte[], uint)[]> GetRoutesAsync()
        => this.Connection.CallMethodAsync(CreateGetPropertyMessage(__Interface, "Routes"), (Message m, object? s) => ReadMessage_v_arayuayuz(m, (NetworkManagerObject)s!), this);
    public Task<Dictionary<string, VariantValue>[]> GetRouteDataAsync()
        => this.Connection.CallMethodAsync(CreateGetPropertyMessage(__Interface, "RouteData"), (Message m, object? s) => ReadMessage_v_aaesv(m, (NetworkManagerObject)s!), this);
    public Task<byte[][]> GetNameserversAsync()
        => this.Connection.CallMethodAsync(CreateGetPropertyMessage(__Interface, "Nameservers"), (Message m, object? s) => ReadMessage_v_aay(m, (NetworkManagerObject)s!), this);
    public Task<string[]> GetDomainsAsync()
        => this.Connection.CallMethodAsync(CreateGetPropertyMessage(__Interface, "Domains"), (Message m, object? s) => ReadMessage_v_as(m, (NetworkManagerObject)s!), this);
    public Task<string[]> GetSearchesAsync()
        => this.Connection.CallMethodAsync(CreateGetPropertyMessage(__Interface, "Searches"), (Message m, object? s) => ReadMessage_v_as(m, (NetworkManagerObject)s!), this);
    public Task<string[]> GetDnsOptionsAsync()
        => this.Connection.CallMethodAsync(CreateGetPropertyMessage(__Interface, "DnsOptions"), (Message m, object? s) => ReadMessage_v_as(m, (NetworkManagerObject)s!), this);
    public Task<int> GetDnsPriorityAsync()
        => this.Connection.CallMethodAsync(CreateGetPropertyMessage(__Interface, "DnsPriority"), (Message m, object? s) => ReadMessage_v_i(m, (NetworkManagerObject)s!), this);
    public Task<IP6ConfigProperties> GetPropertiesAsync()
    {
        return this.Connection.CallMethodAsync(CreateGetAllPropertiesMessage(__Interface), (Message m, object? s) => ReadMessage(m, (NetworkManagerObject)s!), this);
        static IP6ConfigProperties ReadMessage(Message message, NetworkManagerObject _)
        {
            var reader = message.GetBodyReader();
            return ReadProperties(ref reader);
        }
    }
    public ValueTask<IDisposable> WatchPropertiesChangedAsync(Action<Exception?, PropertyChanges<IP6ConfigProperties>> handler, bool emitOnCapturedContext = true, ObserverFlags flags = ObserverFlags.None)
    {
        return base.WatchPropertiesChangedAsync(__Interface, (Message m, object? s) => ReadMessage(m, (NetworkManagerObject)s!), handler, emitOnCapturedContext, flags);
        static PropertyChanges<IP6ConfigProperties> ReadMessage(Message message, NetworkManagerObject _)
        {
            var reader = message.GetBodyReader();
            reader.ReadString(); // interface
            List<string> changed = new(), invalidated = new();
            return new PropertyChanges<IP6ConfigProperties>(ReadProperties(ref reader, changed), ReadInvalidated(ref reader), changed.ToArray());
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
                    case "Nameservers": invalidated.Add("Nameservers"); break;
                    case "Domains": invalidated.Add("Domains"); break;
                    case "Searches": invalidated.Add("Searches"); break;
                    case "DnsOptions": invalidated.Add("DnsOptions"); break;
                    case "DnsPriority": invalidated.Add("DnsPriority"); break;
                }
            }
            return invalidated?.ToArray() ?? Array.Empty<string>();
        }
    }
    private static IP6ConfigProperties ReadProperties(ref Reader reader, List<string>? changedList = null)
    {
        var props = new IP6ConfigProperties();
        ArrayEnd arrayEnd = reader.ReadArrayStart(DBusType.Struct);
        while (reader.HasNext(arrayEnd))
        {
            var property = reader.ReadString();
            switch (property)
            {
                case "Addresses":
                    reader.ReadSignature("a(ayuay)"u8);
                    props.Addresses = ReadType_arayuayz(ref reader);
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
                    reader.ReadSignature("a(ayuayu)"u8);
                    props.Routes = ReadType_arayuayuz(ref reader);
                    changedList?.Add("Routes");
                    break;
                case "RouteData":
                    reader.ReadSignature("aa{sv}"u8);
                    props.RouteData = ReadType_aaesv(ref reader);
                    changedList?.Add("RouteData");
                    break;
                case "Nameservers":
                    reader.ReadSignature("aay"u8);
                    props.Nameservers = ReadType_aay(ref reader);
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
                default:
                    reader.ReadVariantValue();
                    break;
            }
        }
        return props;
    }
}

