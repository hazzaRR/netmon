using System;
using Tmds.DBus.Protocol;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace NetworkManager.DBus;
partial class AccessPoint : NetworkManagerObject
{
    private const string __Interface = "org.freedesktop.NetworkManager.AccessPoint";
    public AccessPoint(NetworkManagerService service, ObjectPath path) : base(service, path)
    { }
    public Task<uint> GetFlagsAsync()
        => this.Connection.CallMethodAsync(CreateGetPropertyMessage(__Interface, "Flags"), (Message m, object? s) => ReadMessage_v_u(m, (NetworkManagerObject)s!), this);
    public Task<uint> GetWpaFlagsAsync()
        => this.Connection.CallMethodAsync(CreateGetPropertyMessage(__Interface, "WpaFlags"), (Message m, object? s) => ReadMessage_v_u(m, (NetworkManagerObject)s!), this);
    public Task<uint> GetRsnFlagsAsync()
        => this.Connection.CallMethodAsync(CreateGetPropertyMessage(__Interface, "RsnFlags"), (Message m, object? s) => ReadMessage_v_u(m, (NetworkManagerObject)s!), this);
    public Task<byte[]> GetSsidAsync()
        => this.Connection.CallMethodAsync(CreateGetPropertyMessage(__Interface, "Ssid"), (Message m, object? s) => ReadMessage_v_ay(m, (NetworkManagerObject)s!), this);
    public Task<uint> GetFrequencyAsync()
        => this.Connection.CallMethodAsync(CreateGetPropertyMessage(__Interface, "Frequency"), (Message m, object? s) => ReadMessage_v_u(m, (NetworkManagerObject)s!), this);
    public Task<string> GetHwAddressAsync()
        => this.Connection.CallMethodAsync(CreateGetPropertyMessage(__Interface, "HwAddress"), (Message m, object? s) => ReadMessage_v_s(m, (NetworkManagerObject)s!), this);
    public Task<uint> GetModeAsync()
        => this.Connection.CallMethodAsync(CreateGetPropertyMessage(__Interface, "Mode"), (Message m, object? s) => ReadMessage_v_u(m, (NetworkManagerObject)s!), this);
    public Task<uint> GetMaxBitrateAsync()
        => this.Connection.CallMethodAsync(CreateGetPropertyMessage(__Interface, "MaxBitrate"), (Message m, object? s) => ReadMessage_v_u(m, (NetworkManagerObject)s!), this);
    public Task<byte> GetStrengthAsync()
        => this.Connection.CallMethodAsync(CreateGetPropertyMessage(__Interface, "Strength"), (Message m, object? s) => ReadMessage_v_y(m, (NetworkManagerObject)s!), this);
    public Task<int> GetLastSeenAsync()
        => this.Connection.CallMethodAsync(CreateGetPropertyMessage(__Interface, "LastSeen"), (Message m, object? s) => ReadMessage_v_i(m, (NetworkManagerObject)s!), this);
    public Task<AccessPointProperties> GetPropertiesAsync()
    {
        return this.Connection.CallMethodAsync(CreateGetAllPropertiesMessage(__Interface), (Message m, object? s) => ReadMessage(m, (NetworkManagerObject)s!), this);
        static AccessPointProperties ReadMessage(Message message, NetworkManagerObject _)
        {
            var reader = message.GetBodyReader();
            return ReadProperties(ref reader);
        }
    }
    public ValueTask<IDisposable> WatchPropertiesChangedAsync(Action<Exception?, PropertyChanges<AccessPointProperties>> handler, bool emitOnCapturedContext = true, ObserverFlags flags = ObserverFlags.None)
    {
        return base.WatchPropertiesChangedAsync(__Interface, (Message m, object? s) => ReadMessage(m, (NetworkManagerObject)s!), handler, emitOnCapturedContext, flags);
        static PropertyChanges<AccessPointProperties> ReadMessage(Message message, NetworkManagerObject _)
        {
            var reader = message.GetBodyReader();
            reader.ReadString(); // interface
            List<string> changed = new(), invalidated = new();
            return new PropertyChanges<AccessPointProperties>(ReadProperties(ref reader, changed), ReadInvalidated(ref reader), changed.ToArray());
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
                    case "Flags": invalidated.Add("Flags"); break;
                    case "WpaFlags": invalidated.Add("WpaFlags"); break;
                    case "RsnFlags": invalidated.Add("RsnFlags"); break;
                    case "Ssid": invalidated.Add("Ssid"); break;
                    case "Frequency": invalidated.Add("Frequency"); break;
                    case "HwAddress": invalidated.Add("HwAddress"); break;
                    case "Mode": invalidated.Add("Mode"); break;
                    case "MaxBitrate": invalidated.Add("MaxBitrate"); break;
                    case "Strength": invalidated.Add("Strength"); break;
                    case "LastSeen": invalidated.Add("LastSeen"); break;
                }
            }
            return invalidated?.ToArray() ?? Array.Empty<string>();
        }
    }
    private static AccessPointProperties ReadProperties(ref Reader reader, List<string>? changedList = null)
    {
        var props = new AccessPointProperties();
        ArrayEnd arrayEnd = reader.ReadArrayStart(DBusType.Struct);
        while (reader.HasNext(arrayEnd))
        {
            var property = reader.ReadString();
            switch (property)
            {
                case "Flags":
                    reader.ReadSignature("u"u8);
                    props.Flags = reader.ReadUInt32();
                    changedList?.Add("Flags");
                    break;
                case "WpaFlags":
                    reader.ReadSignature("u"u8);
                    props.WpaFlags = reader.ReadUInt32();
                    changedList?.Add("WpaFlags");
                    break;
                case "RsnFlags":
                    reader.ReadSignature("u"u8);
                    props.RsnFlags = reader.ReadUInt32();
                    changedList?.Add("RsnFlags");
                    break;
                case "Ssid":
                    reader.ReadSignature("ay"u8);
                    props.Ssid = reader.ReadArrayOfByte();
                    changedList?.Add("Ssid");
                    break;
                case "Frequency":
                    reader.ReadSignature("u"u8);
                    props.Frequency = reader.ReadUInt32();
                    changedList?.Add("Frequency");
                    break;
                case "HwAddress":
                    reader.ReadSignature("s"u8);
                    props.HwAddress = reader.ReadString();
                    changedList?.Add("HwAddress");
                    break;
                case "Mode":
                    reader.ReadSignature("u"u8);
                    props.Mode = reader.ReadUInt32();
                    changedList?.Add("Mode");
                    break;
                case "MaxBitrate":
                    reader.ReadSignature("u"u8);
                    props.MaxBitrate = reader.ReadUInt32();
                    changedList?.Add("MaxBitrate");
                    break;
                case "Strength":
                    reader.ReadSignature("y"u8);
                    props.Strength = reader.ReadByte();
                    changedList?.Add("Strength");
                    break;
                case "LastSeen":
                    reader.ReadSignature("i"u8);
                    props.LastSeen = reader.ReadInt32();
                    changedList?.Add("LastSeen");
                    break;
                default:
                    reader.ReadVariantValue();
                    break;
            }
        }
        return props;
    }
}
