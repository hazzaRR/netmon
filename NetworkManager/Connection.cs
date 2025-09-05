using System;
using Tmds.DBus.Protocol;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace NetworkManager.DBus;
partial class Connection : NetworkManagerObject
{
    private const string __Interface = "org.freedesktop.NetworkManager.Settings.Connection";
    public Connection(NetworkManagerService service, ObjectPath path) : base(service, path)
    { }
    public Task UpdateAsync(Dictionary<string, Dictionary<string, VariantValue>> properties)
    {
        return this.Connection.CallMethodAsync(CreateMessage());
        MessageBuffer CreateMessage()
        {
            var writer = this.Connection.GetMessageWriter();
            writer.WriteMethodCallHeader(
                destination: Service.Destination,
                path: Path,
                @interface: __Interface,
                signature: "a{sa{sv}}",
                member: "Update");
            WriteType_aesaesv(ref writer, properties);
            return writer.CreateMessage();
        }
    }
    public Task UpdateUnsavedAsync(Dictionary<string, Dictionary<string, VariantValue>> properties)
    {
        return this.Connection.CallMethodAsync(CreateMessage());
        MessageBuffer CreateMessage()
        {
            var writer = this.Connection.GetMessageWriter();
            writer.WriteMethodCallHeader(
                destination: Service.Destination,
                path: Path,
                @interface: __Interface,
                signature: "a{sa{sv}}",
                member: "UpdateUnsaved");
            WriteType_aesaesv(ref writer, properties);
            return writer.CreateMessage();
        }
    }
    public Task DeleteAsync()
    {
        return this.Connection.CallMethodAsync(CreateMessage());
        MessageBuffer CreateMessage()
        {
            var writer = this.Connection.GetMessageWriter();
            writer.WriteMethodCallHeader(
                destination: Service.Destination,
                path: Path,
                @interface: __Interface,
                member: "Delete");
            return writer.CreateMessage();
        }
    }
    public Task<Dictionary<string, Dictionary<string, VariantValue>>> GetSettingsAsync()
    {
        return this.Connection.CallMethodAsync(CreateMessage(), (Message m, object? s) => ReadMessage_aesaesv(m, (NetworkManagerObject)s!), this);
        MessageBuffer CreateMessage()
        {
            var writer = this.Connection.GetMessageWriter();
            writer.WriteMethodCallHeader(
                destination: Service.Destination,
                path: Path,
                @interface: __Interface,
                member: "GetSettings");
            return writer.CreateMessage();
        }
    }
    public Task<Dictionary<string, Dictionary<string, VariantValue>>> GetSecretsAsync(string settingName)
    {
        return this.Connection.CallMethodAsync(CreateMessage(), (Message m, object? s) => ReadMessage_aesaesv(m, (NetworkManagerObject)s!), this);
        MessageBuffer CreateMessage()
        {
            var writer = this.Connection.GetMessageWriter();
            writer.WriteMethodCallHeader(
                destination: Service.Destination,
                path: Path,
                @interface: __Interface,
                signature: "s",
                member: "GetSecrets");
            writer.WriteString(settingName);
            return writer.CreateMessage();
        }
    }
    public Task ClearSecretsAsync()
    {
        return this.Connection.CallMethodAsync(CreateMessage());
        MessageBuffer CreateMessage()
        {
            var writer = this.Connection.GetMessageWriter();
            writer.WriteMethodCallHeader(
                destination: Service.Destination,
                path: Path,
                @interface: __Interface,
                member: "ClearSecrets");
            return writer.CreateMessage();
        }
    }
    public Task SaveAsync()
    {
        return this.Connection.CallMethodAsync(CreateMessage());
        MessageBuffer CreateMessage()
        {
            var writer = this.Connection.GetMessageWriter();
            writer.WriteMethodCallHeader(
                destination: Service.Destination,
                path: Path,
                @interface: __Interface,
                member: "Save");
            return writer.CreateMessage();
        }
    }
    public Task<Dictionary<string, VariantValue>> Update2Async(Dictionary<string, Dictionary<string, VariantValue>> settings, uint flags, Dictionary<string, VariantValue> args)
    {
        return this.Connection.CallMethodAsync(CreateMessage(), (Message m, object? s) => ReadMessage_aesv(m, (NetworkManagerObject)s!), this);
        MessageBuffer CreateMessage()
        {
            var writer = this.Connection.GetMessageWriter();
            writer.WriteMethodCallHeader(
                destination: Service.Destination,
                path: Path,
                @interface: __Interface,
                signature: "a{sa{sv}}ua{sv}",
                member: "Update2");
            WriteType_aesaesv(ref writer, settings);
            writer.WriteUInt32(flags);
            writer.WriteDictionary(args);
            return writer.CreateMessage();
        }
    }
    public ValueTask<IDisposable> WatchUpdatedAsync(Action<Exception?> handler, bool emitOnCapturedContext = true, ObserverFlags flags = ObserverFlags.None)
        => base.WatchSignalAsync(Service.Destination, __Interface, Path, "Updated", handler, emitOnCapturedContext, flags);
    public ValueTask<IDisposable> WatchRemovedAsync(Action<Exception?> handler, bool emitOnCapturedContext = true, ObserverFlags flags = ObserverFlags.None)
        => base.WatchSignalAsync(Service.Destination, __Interface, Path, "Removed", handler, emitOnCapturedContext, flags);
    public Task<bool> GetUnsavedAsync()
        => this.Connection.CallMethodAsync(CreateGetPropertyMessage(__Interface, "Unsaved"), (Message m, object? s) => ReadMessage_v_b(m, (NetworkManagerObject)s!), this);
    public Task<uint> GetFlagsAsync()
        => this.Connection.CallMethodAsync(CreateGetPropertyMessage(__Interface, "Flags"), (Message m, object? s) => ReadMessage_v_u(m, (NetworkManagerObject)s!), this);
    public Task<string> GetFilenameAsync()
        => this.Connection.CallMethodAsync(CreateGetPropertyMessage(__Interface, "Filename"), (Message m, object? s) => ReadMessage_v_s(m, (NetworkManagerObject)s!), this);
    public Task<ConnectionProperties> GetPropertiesAsync()
    {
        return this.Connection.CallMethodAsync(CreateGetAllPropertiesMessage(__Interface), (Message m, object? s) => ReadMessage(m, (NetworkManagerObject)s!), this);
        static ConnectionProperties ReadMessage(Message message, NetworkManagerObject _)
        {
            var reader = message.GetBodyReader();
            return ReadProperties(ref reader);
        }
    }
    public ValueTask<IDisposable> WatchPropertiesChangedAsync(Action<Exception?, PropertyChanges<ConnectionProperties>> handler, bool emitOnCapturedContext = true, ObserverFlags flags = ObserverFlags.None)
    {
        return base.WatchPropertiesChangedAsync(__Interface, (Message m, object? s) => ReadMessage(m, (NetworkManagerObject)s!), handler, emitOnCapturedContext, flags);
        static PropertyChanges<ConnectionProperties> ReadMessage(Message message, NetworkManagerObject _)
        {
            var reader = message.GetBodyReader();
            reader.ReadString(); // interface
            List<string> changed = new(), invalidated = new();
            return new PropertyChanges<ConnectionProperties>(ReadProperties(ref reader, changed), ReadInvalidated(ref reader), changed.ToArray());
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
                    case "Unsaved": invalidated.Add("Unsaved"); break;
                    case "Flags": invalidated.Add("Flags"); break;
                    case "Filename": invalidated.Add("Filename"); break;
                }
            }
            return invalidated?.ToArray() ?? Array.Empty<string>();
        }
    }
    private static ConnectionProperties ReadProperties(ref Reader reader, List<string>? changedList = null)
    {
        var props = new ConnectionProperties();
        ArrayEnd arrayEnd = reader.ReadArrayStart(DBusType.Struct);
        while (reader.HasNext(arrayEnd))
        {
            var property = reader.ReadString();
            switch (property)
            {
                case "Unsaved":
                    reader.ReadSignature("b"u8);
                    props.Unsaved = reader.ReadBool();
                    changedList?.Add("Unsaved");
                    break;
                case "Flags":
                    reader.ReadSignature("u"u8);
                    props.Flags = reader.ReadUInt32();
                    changedList?.Add("Flags");
                    break;
                case "Filename":
                    reader.ReadSignature("s"u8);
                    props.Filename = reader.ReadString();
                    changedList?.Add("Filename");
                    break;
                default:
                    reader.ReadVariantValue();
                    break;
            }
        }
        return props;
    }
}