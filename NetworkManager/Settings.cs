using System;
using Tmds.DBus.Protocol;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace NetworkManager.DBus;

partial class Settings : NetworkManagerObject
{
    private const string __Interface = "org.freedesktop.NetworkManager.Settings";
    public Settings(NetworkManagerService service, ObjectPath path) : base(service, path)
    { }
    public Task<ObjectPath[]> ListConnectionsAsync()
    {
        return this.Connection.CallMethodAsync(CreateMessage(), (Message m, object? s) => ReadMessage_ao(m, (NetworkManagerObject)s!), this);
        MessageBuffer CreateMessage()
        {
            var writer = this.Connection.GetMessageWriter();
            writer.WriteMethodCallHeader(
                destination: Service.Destination,
                path: Path,
                @interface: __Interface,
                member: "ListConnections");
            return writer.CreateMessage();
        }
    }
    public Task<ObjectPath> GetConnectionByUuidAsync(string uuid)
    {
        return this.Connection.CallMethodAsync(CreateMessage(), (Message m, object? s) => ReadMessage_o(m, (NetworkManagerObject)s!), this);
        MessageBuffer CreateMessage()
        {
            var writer = this.Connection.GetMessageWriter();
            writer.WriteMethodCallHeader(
                destination: Service.Destination,
                path: Path,
                @interface: __Interface,
                signature: "s",
                member: "GetConnectionByUuid");
            writer.WriteString(uuid);
            return writer.CreateMessage();
        }
    }
    public Task<ObjectPath> AddConnectionAsync(Dictionary<string, Dictionary<string, VariantValue>> connection)
    {
        return this.Connection.CallMethodAsync(CreateMessage(), (Message m, object? s) => ReadMessage_o(m, (NetworkManagerObject)s!), this);
        MessageBuffer CreateMessage()
        {
            var writer = this.Connection.GetMessageWriter();
            writer.WriteMethodCallHeader(
                destination: Service.Destination,
                path: Path,
                @interface: __Interface,
                signature: "a{sa{sv}}",
                member: "AddConnection");
            WriteType_aesaesv(ref writer, connection);
            return writer.CreateMessage();
        }
    }
    public Task<ObjectPath> AddConnectionUnsavedAsync(Dictionary<string, Dictionary<string, VariantValue>> connection)
    {
        return this.Connection.CallMethodAsync(CreateMessage(), (Message m, object? s) => ReadMessage_o(m, (NetworkManagerObject)s!), this);
        MessageBuffer CreateMessage()
        {
            var writer = this.Connection.GetMessageWriter();
            writer.WriteMethodCallHeader(
                destination: Service.Destination,
                path: Path,
                @interface: __Interface,
                signature: "a{sa{sv}}",
                member: "AddConnectionUnsaved");
            WriteType_aesaesv(ref writer, connection);
            return writer.CreateMessage();
        }
    }
    public Task<(ObjectPath Path, Dictionary<string, VariantValue> Result)> AddConnection2Async(Dictionary<string, Dictionary<string, VariantValue>> settings, uint flags, Dictionary<string, VariantValue> args)
    {
        return this.Connection.CallMethodAsync(CreateMessage(), (Message m, object? s) => ReadMessage_oaesv(m, (NetworkManagerObject)s!), this);
        MessageBuffer CreateMessage()
        {
            var writer = this.Connection.GetMessageWriter();
            writer.WriteMethodCallHeader(
                destination: Service.Destination,
                path: Path,
                @interface: __Interface,
                signature: "a{sa{sv}}ua{sv}",
                member: "AddConnection2");
            WriteType_aesaesv(ref writer, settings);
            writer.WriteUInt32(flags);
            writer.WriteDictionary(args);
            return writer.CreateMessage();
        }
    }
    public Task<(bool Status, string[] Failures)> LoadConnectionsAsync(string[] filenames)
    {
        return this.Connection.CallMethodAsync(CreateMessage(), (Message m, object? s) => ReadMessage_bas(m, (NetworkManagerObject)s!), this);
        MessageBuffer CreateMessage()
        {
            var writer = this.Connection.GetMessageWriter();
            writer.WriteMethodCallHeader(
                destination: Service.Destination,
                path: Path,
                @interface: __Interface,
                signature: "as",
                member: "LoadConnections");
            writer.WriteArray(filenames);
            return writer.CreateMessage();
        }
    }
    public Task<bool> ReloadConnectionsAsync()
    {
        return this.Connection.CallMethodAsync(CreateMessage(), (Message m, object? s) => ReadMessage_b(m, (NetworkManagerObject)s!), this);
        MessageBuffer CreateMessage()
        {
            var writer = this.Connection.GetMessageWriter();
            writer.WriteMethodCallHeader(
                destination: Service.Destination,
                path: Path,
                @interface: __Interface,
                member: "ReloadConnections");
            return writer.CreateMessage();
        }
    }
    public Task SaveHostnameAsync(string hostname)
    {
        return this.Connection.CallMethodAsync(CreateMessage());
        MessageBuffer CreateMessage()
        {
            var writer = this.Connection.GetMessageWriter();
            writer.WriteMethodCallHeader(
                destination: Service.Destination,
                path: Path,
                @interface: __Interface,
                signature: "s",
                member: "SaveHostname");
            writer.WriteString(hostname);
            return writer.CreateMessage();
        }
    }
    public ValueTask<IDisposable> WatchNewConnectionAsync(Action<Exception?, ObjectPath> handler, bool emitOnCapturedContext = true, ObserverFlags flags = ObserverFlags.None)
        => base.WatchSignalAsync(Service.Destination, __Interface, Path, "NewConnection", (Message m, object? s) => ReadMessage_o(m, (NetworkManagerObject)s!), handler, emitOnCapturedContext, flags);
    public ValueTask<IDisposable> WatchConnectionRemovedAsync(Action<Exception?, ObjectPath> handler, bool emitOnCapturedContext = true, ObserverFlags flags = ObserverFlags.None)
        => base.WatchSignalAsync(Service.Destination, __Interface, Path, "ConnectionRemoved", (Message m, object? s) => ReadMessage_o(m, (NetworkManagerObject)s!), handler, emitOnCapturedContext, flags);
    public Task<ObjectPath[]> GetConnectionsAsync()
        => this.Connection.CallMethodAsync(CreateGetPropertyMessage(__Interface, "Connections"), (Message m, object? s) => ReadMessage_v_ao(m, (NetworkManagerObject)s!), this);
    public Task<string> GetHostnameAsync()
        => this.Connection.CallMethodAsync(CreateGetPropertyMessage(__Interface, "Hostname"), (Message m, object? s) => ReadMessage_v_s(m, (NetworkManagerObject)s!), this);
    public Task<bool> GetCanModifyAsync()
        => this.Connection.CallMethodAsync(CreateGetPropertyMessage(__Interface, "CanModify"), (Message m, object? s) => ReadMessage_v_b(m, (NetworkManagerObject)s!), this);
    public Task<SettingsProperties> GetPropertiesAsync()
    {
        return this.Connection.CallMethodAsync(CreateGetAllPropertiesMessage(__Interface), (Message m, object? s) => ReadMessage(m, (NetworkManagerObject)s!), this);
        static SettingsProperties ReadMessage(Message message, NetworkManagerObject _)
        {
            var reader = message.GetBodyReader();
            return ReadProperties(ref reader);
        }
    }
    public ValueTask<IDisposable> WatchPropertiesChangedAsync(Action<Exception?, PropertyChanges<SettingsProperties>> handler, bool emitOnCapturedContext = true, ObserverFlags flags = ObserverFlags.None)
    {
        return base.WatchPropertiesChangedAsync(__Interface, (Message m, object? s) => ReadMessage(m, (NetworkManagerObject)s!), handler, emitOnCapturedContext, flags);
        static PropertyChanges<SettingsProperties> ReadMessage(Message message, NetworkManagerObject _)
        {
            var reader = message.GetBodyReader();
            reader.ReadString(); // interface
            List<string> changed = new(), invalidated = new();
            return new PropertyChanges<SettingsProperties>(ReadProperties(ref reader, changed), ReadInvalidated(ref reader), changed.ToArray());
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
                    case "Connections": invalidated.Add("Connections"); break;
                    case "Hostname": invalidated.Add("Hostname"); break;
                    case "CanModify": invalidated.Add("CanModify"); break;
                }
            }
            return invalidated?.ToArray() ?? Array.Empty<string>();
        }
    }
    private static SettingsProperties ReadProperties(ref Reader reader, List<string>? changedList = null)
    {
        var props = new SettingsProperties();
        ArrayEnd arrayEnd = reader.ReadArrayStart(DBusType.Struct);
        while (reader.HasNext(arrayEnd))
        {
            var property = reader.ReadString();
            switch (property)
            {
                case "Connections":
                    reader.ReadSignature("ao"u8);
                    props.Connections = reader.ReadArrayOfObjectPath();
                    changedList?.Add("Connections");
                    break;
                case "Hostname":
                    reader.ReadSignature("s"u8);
                    props.Hostname = reader.ReadString();
                    changedList?.Add("Hostname");
                    break;
                case "CanModify":
                    reader.ReadSignature("b"u8);
                    props.CanModify = reader.ReadBool();
                    changedList?.Add("CanModify");
                    break;
                default:
                    reader.ReadVariantValue();
                    break;
            }
        }
        return props;
    }
}