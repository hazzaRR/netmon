using System;
using Tmds.DBus.Protocol;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace NetworkManager.DBus;
partial class Wireless : NetworkManagerObject
{
    private const string __Interface = "org.freedesktop.NetworkManager.Device.Wireless";
    public Wireless(NetworkManagerService service, ObjectPath path) : base(service, path)
    { }
    public Task<ObjectPath[]> GetAccessPointsAsync()
    {
        return this.Connection.CallMethodAsync(CreateMessage(), (Message m, object? s) => ReadMessage_ao(m, (NetworkManagerObject)s!), this);
        MessageBuffer CreateMessage()
        {
            var writer = this.Connection.GetMessageWriter();
            writer.WriteMethodCallHeader(
                destination: Service.Destination,
                path: Path,
                @interface: __Interface,
                member: "GetAccessPoints");
            return writer.CreateMessage();
        }
    }
    public Task<ObjectPath[]> GetAllAccessPointsAsync()
    {
        return this.Connection.CallMethodAsync(CreateMessage(), (Message m, object? s) => ReadMessage_ao(m, (NetworkManagerObject)s!), this);
        MessageBuffer CreateMessage()
        {
            var writer = this.Connection.GetMessageWriter();
            writer.WriteMethodCallHeader(
                destination: Service.Destination,
                path: Path,
                @interface: __Interface,
                member: "GetAllAccessPoints");
            return writer.CreateMessage();
        }
    }
    public Task RequestScanAsync(Dictionary<string, VariantValue> options)
    {
        return this.Connection.CallMethodAsync(CreateMessage());
        MessageBuffer CreateMessage()
        {
            var writer = this.Connection.GetMessageWriter();
            writer.WriteMethodCallHeader(
                destination: Service.Destination,
                path: Path,
                @interface: __Interface,
                signature: "a{sv}",
                member: "RequestScan");
            writer.WriteDictionary(options);
            return writer.CreateMessage();
        }
    }
    public ValueTask<IDisposable> WatchAccessPointAddedAsync(Action<Exception?, ObjectPath> handler, bool emitOnCapturedContext = true, ObserverFlags flags = ObserverFlags.None)
        => base.WatchSignalAsync(Service.Destination, __Interface, Path, "AccessPointAdded", (Message m, object? s) => ReadMessage_o(m, (NetworkManagerObject)s!), handler, emitOnCapturedContext, flags);
    public ValueTask<IDisposable> WatchAccessPointRemovedAsync(Action<Exception?, ObjectPath> handler, bool emitOnCapturedContext = true, ObserverFlags flags = ObserverFlags.None)
        => base.WatchSignalAsync(Service.Destination, __Interface, Path, "AccessPointRemoved", (Message m, object? s) => ReadMessage_o(m, (NetworkManagerObject)s!), handler, emitOnCapturedContext, flags);
    public Task<string> GetHwAddressAsync()
        => this.Connection.CallMethodAsync(CreateGetPropertyMessage(__Interface, "HwAddress"), (Message m, object? s) => ReadMessage_v_s(m, (NetworkManagerObject)s!), this);
    public Task<string> GetPermHwAddressAsync()
        => this.Connection.CallMethodAsync(CreateGetPropertyMessage(__Interface, "PermHwAddress"), (Message m, object? s) => ReadMessage_v_s(m, (NetworkManagerObject)s!), this);
    public Task<uint> GetModeAsync()
        => this.Connection.CallMethodAsync(CreateGetPropertyMessage(__Interface, "Mode"), (Message m, object? s) => ReadMessage_v_u(m, (NetworkManagerObject)s!), this);
    public Task<uint> GetBitrateAsync()
        => this.Connection.CallMethodAsync(CreateGetPropertyMessage(__Interface, "Bitrate"), (Message m, object? s) => ReadMessage_v_u(m, (NetworkManagerObject)s!), this);
    // public Task<ObjectPath[]> GetAccessPointsAsync()
    //     => this.Connection.CallMethodAsync(CreateGetPropertyMessage(__Interface, "AccessPoints"), (Message m, object? s) => ReadMessage_v_ao(m, (NetworkManagerObject)s!), this);
    public Task<ObjectPath> GetActiveAccessPointAsync()
        => this.Connection.CallMethodAsync(CreateGetPropertyMessage(__Interface, "ActiveAccessPoint"), (Message m, object? s) => ReadMessage_v_o(m, (NetworkManagerObject)s!), this);
    public Task<uint> GetWirelessCapabilitiesAsync()
        => this.Connection.CallMethodAsync(CreateGetPropertyMessage(__Interface, "WirelessCapabilities"), (Message m, object? s) => ReadMessage_v_u(m, (NetworkManagerObject)s!), this);
    public Task<long> GetLastScanAsync()
        => this.Connection.CallMethodAsync(CreateGetPropertyMessage(__Interface, "LastScan"), (Message m, object? s) => ReadMessage_v_x(m, (NetworkManagerObject)s!), this);
    public Task<WirelessProperties> GetPropertiesAsync()
    {
        return this.Connection.CallMethodAsync(CreateGetAllPropertiesMessage(__Interface), (Message m, object? s) => ReadMessage(m, (NetworkManagerObject)s!), this);
        static WirelessProperties ReadMessage(Message message, NetworkManagerObject _)
        {
            var reader = message.GetBodyReader();
            return ReadProperties(ref reader);
        }
    }
    public ValueTask<IDisposable> WatchPropertiesChangedAsync(Action<Exception?, PropertyChanges<WirelessProperties>> handler, bool emitOnCapturedContext = true, ObserverFlags flags = ObserverFlags.None)
    {
        return base.WatchPropertiesChangedAsync(__Interface, (Message m, object? s) => ReadMessage(m, (NetworkManagerObject)s!), handler, emitOnCapturedContext, flags);
        static PropertyChanges<WirelessProperties> ReadMessage(Message message, NetworkManagerObject _)
        {
            var reader = message.GetBodyReader();
            reader.ReadString(); // interface
            List<string> changed = new(), invalidated = new();
            return new PropertyChanges<WirelessProperties>(ReadProperties(ref reader, changed), ReadInvalidated(ref reader), changed.ToArray());
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
                    case "HwAddress": invalidated.Add("HwAddress"); break;
                    case "PermHwAddress": invalidated.Add("PermHwAddress"); break;
                    case "Mode": invalidated.Add("Mode"); break;
                    case "Bitrate": invalidated.Add("Bitrate"); break;
                    case "AccessPoints": invalidated.Add("AccessPoints"); break;
                    case "ActiveAccessPoint": invalidated.Add("ActiveAccessPoint"); break;
                    case "WirelessCapabilities": invalidated.Add("WirelessCapabilities"); break;
                    case "LastScan": invalidated.Add("LastScan"); break;
                }
            }
            return invalidated?.ToArray() ?? Array.Empty<string>();
        }
    }
    private static WirelessProperties ReadProperties(ref Reader reader, List<string>? changedList = null)
    {
        var props = new WirelessProperties();
        ArrayEnd arrayEnd = reader.ReadArrayStart(DBusType.Struct);
        while (reader.HasNext(arrayEnd))
        {
            var property = reader.ReadString();
            switch (property)
            {
                case "HwAddress":
                    reader.ReadSignature("s"u8);
                    props.HwAddress = reader.ReadString();
                    changedList?.Add("HwAddress");
                    break;
                case "PermHwAddress":
                    reader.ReadSignature("s"u8);
                    props.PermHwAddress = reader.ReadString();
                    changedList?.Add("PermHwAddress");
                    break;
                case "Mode":
                    reader.ReadSignature("u"u8);
                    props.Mode = reader.ReadUInt32();
                    changedList?.Add("Mode");
                    break;
                case "Bitrate":
                    reader.ReadSignature("u"u8);
                    props.Bitrate = reader.ReadUInt32();
                    changedList?.Add("Bitrate");
                    break;
                case "AccessPoints":
                    reader.ReadSignature("ao"u8);
                    props.AccessPoints = reader.ReadArrayOfObjectPath();
                    changedList?.Add("AccessPoints");
                    break;
                case "ActiveAccessPoint":
                    reader.ReadSignature("o"u8);
                    props.ActiveAccessPoint = reader.ReadObjectPath();
                    changedList?.Add("ActiveAccessPoint");
                    break;
                case "WirelessCapabilities":
                    reader.ReadSignature("u"u8);
                    props.WirelessCapabilities = reader.ReadUInt32();
                    changedList?.Add("WirelessCapabilities");
                    break;
                case "LastScan":
                    reader.ReadSignature("x"u8);
                    props.LastScan = reader.ReadInt64();
                    changedList?.Add("LastScan");
                    break;
                default:
                    reader.ReadVariantValue();
                    break;
            }
        }
        return props;
    }
}
