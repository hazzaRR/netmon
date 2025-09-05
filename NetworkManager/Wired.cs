using System;
using Tmds.DBus.Protocol;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace NetworkManager.DBus;
partial class Wired : NetworkManagerObject
{
    private const string __Interface = "org.freedesktop.NetworkManager.Device.Wired";
    public Wired(NetworkManagerService service, ObjectPath path) : base(service, path)
    { }
    public Task<string> GetHwAddressAsync()
        => this.Connection.CallMethodAsync(CreateGetPropertyMessage(__Interface, "HwAddress"), (Message m, object? s) => ReadMessage_v_s(m, (NetworkManagerObject)s!), this);
    public Task<string> GetPermHwAddressAsync()
        => this.Connection.CallMethodAsync(CreateGetPropertyMessage(__Interface, "PermHwAddress"), (Message m, object? s) => ReadMessage_v_s(m, (NetworkManagerObject)s!), this);
    public Task<uint> GetSpeedAsync()
        => this.Connection.CallMethodAsync(CreateGetPropertyMessage(__Interface, "Speed"), (Message m, object? s) => ReadMessage_v_u(m, (NetworkManagerObject)s!), this);
    public Task<string[]> GetS390SubchannelsAsync()
        => this.Connection.CallMethodAsync(CreateGetPropertyMessage(__Interface, "S390Subchannels"), (Message m, object? s) => ReadMessage_v_as(m, (NetworkManagerObject)s!), this);
    public Task<bool> GetCarrierAsync()
        => this.Connection.CallMethodAsync(CreateGetPropertyMessage(__Interface, "Carrier"), (Message m, object? s) => ReadMessage_v_b(m, (NetworkManagerObject)s!), this);
    public Task<WiredProperties> GetPropertiesAsync()
    {
        return this.Connection.CallMethodAsync(CreateGetAllPropertiesMessage(__Interface), (Message m, object? s) => ReadMessage(m, (NetworkManagerObject)s!), this);
        static WiredProperties ReadMessage(Message message, NetworkManagerObject _)
        {
            var reader = message.GetBodyReader();
            return ReadProperties(ref reader);
        }
    }
    public ValueTask<IDisposable> WatchPropertiesChangedAsync(Action<Exception?, PropertyChanges<WiredProperties>> handler, bool emitOnCapturedContext = true, ObserverFlags flags = ObserverFlags.None)
    {
        return base.WatchPropertiesChangedAsync(__Interface, (Message m, object? s) => ReadMessage(m, (NetworkManagerObject)s!), handler, emitOnCapturedContext, flags);
        static PropertyChanges<WiredProperties> ReadMessage(Message message, NetworkManagerObject _)
        {
            var reader = message.GetBodyReader();
            reader.ReadString(); // interface
            List<string> changed = new(), invalidated = new();
            return new PropertyChanges<WiredProperties>(ReadProperties(ref reader, changed), ReadInvalidated(ref reader), changed.ToArray());
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
                    case "Speed": invalidated.Add("Speed"); break;
                    case "S390Subchannels": invalidated.Add("S390Subchannels"); break;
                    case "Carrier": invalidated.Add("Carrier"); break;
                }
            }
            return invalidated?.ToArray() ?? Array.Empty<string>();
        }
    }
    private static WiredProperties ReadProperties(ref Reader reader, List<string>? changedList = null)
    {
        var props = new WiredProperties();
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
                case "Speed":
                    reader.ReadSignature("u"u8);
                    props.Speed = reader.ReadUInt32();
                    changedList?.Add("Speed");
                    break;
                case "S390Subchannels":
                    reader.ReadSignature("as"u8);
                    props.S390Subchannels = reader.ReadArrayOfString();
                    changedList?.Add("S390Subchannels");
                    break;
                case "Carrier":
                    reader.ReadSignature("b"u8);
                    props.Carrier = reader.ReadBool();
                    changedList?.Add("Carrier");
                    break;
                default:
                    reader.ReadVariantValue();
                    break;
            }
        }
        return props;
    }
}
