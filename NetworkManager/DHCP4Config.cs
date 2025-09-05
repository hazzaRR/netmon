using System;
using Tmds.DBus.Protocol;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace NetworkManager.DBus;
partial class DHCP4Config : NetworkManagerObject
{
    private const string __Interface = "org.freedesktop.NetworkManager.DHCP4Config";
    public DHCP4Config(NetworkManagerService service, ObjectPath path) : base(service, path)
    { }
    public Task<Dictionary<string, VariantValue>> GetOptionsAsync()
        => this.Connection.CallMethodAsync(CreateGetPropertyMessage(__Interface, "Options"), (Message m, object? s) => ReadMessage_v_aesv(m, (NetworkManagerObject)s!), this);
    public Task<DHCP4ConfigProperties> GetPropertiesAsync()
    {
        return this.Connection.CallMethodAsync(CreateGetAllPropertiesMessage(__Interface), (Message m, object? s) => ReadMessage(m, (NetworkManagerObject)s!), this);
        static DHCP4ConfigProperties ReadMessage(Message message, NetworkManagerObject _)
        {
            var reader = message.GetBodyReader();
            return ReadProperties(ref reader);
        }
    }
    public ValueTask<IDisposable> WatchPropertiesChangedAsync(Action<Exception?, PropertyChanges<DHCP4ConfigProperties>> handler, bool emitOnCapturedContext = true, ObserverFlags flags = ObserverFlags.None)
    {
        return base.WatchPropertiesChangedAsync(__Interface, (Message m, object? s) => ReadMessage(m, (NetworkManagerObject)s!), handler, emitOnCapturedContext, flags);
        static PropertyChanges<DHCP4ConfigProperties> ReadMessage(Message message, NetworkManagerObject _)
        {
            var reader = message.GetBodyReader();
            reader.ReadString(); // interface
            List<string> changed = new(), invalidated = new();
            return new PropertyChanges<DHCP4ConfigProperties>(ReadProperties(ref reader, changed), ReadInvalidated(ref reader), changed.ToArray());
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
                    case "Options": invalidated.Add("Options"); break;
                }
            }
            return invalidated?.ToArray() ?? Array.Empty<string>();
        }
    }
    private static DHCP4ConfigProperties ReadProperties(ref Reader reader, List<string>? changedList = null)
    {
        var props = new DHCP4ConfigProperties();
        ArrayEnd arrayEnd = reader.ReadArrayStart(DBusType.Struct);
        while (reader.HasNext(arrayEnd))
        {
            var property = reader.ReadString();
            switch (property)
            {
                case "Options":
                    reader.ReadSignature("a{sv}"u8);
                    props.Options = reader.ReadDictionaryOfStringToVariantValue();
                    changedList?.Add("Options");
                    break;
                default:
                    reader.ReadVariantValue();
                    break;
            }
        }
        return props;
    }
}