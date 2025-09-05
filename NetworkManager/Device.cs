using System;
using Tmds.DBus.Protocol;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace NetworkManager.DBus;
partial class Device : NetworkManagerObject
{
    private const string __Interface = "org.freedesktop.NetworkManager.Device";
    public Device(NetworkManagerService service, ObjectPath path) : base(service, path)
    { }
    public Task ReapplyAsync(Dictionary<string, Dictionary<string, VariantValue>> connection, ulong versionId, uint flags)
    {
        return this.Connection.CallMethodAsync(CreateMessage());
        MessageBuffer CreateMessage()
        {
            var writer = this.Connection.GetMessageWriter();
            writer.WriteMethodCallHeader(
                destination: Service.Destination,
                path: Path,
                @interface: __Interface,
                signature: "a{sa{sv}}tu",
                member: "Reapply");
            WriteType_aesaesv(ref writer, connection);
            writer.WriteUInt64(versionId);
            writer.WriteUInt32(flags);
            return writer.CreateMessage();
        }
    }
    public Task<(Dictionary<string, Dictionary<string, VariantValue>> Connection, ulong VersionId)> GetAppliedConnectionAsync(uint flags)
    {
        return this.Connection.CallMethodAsync(CreateMessage(), (Message m, object? s) => ReadMessage_aesaesvt(m, (NetworkManagerObject)s!), this);
        MessageBuffer CreateMessage()
        {
            var writer = this.Connection.GetMessageWriter();
            writer.WriteMethodCallHeader(
                destination: Service.Destination,
                path: Path,
                @interface: __Interface,
                signature: "u",
                member: "GetAppliedConnection");
            writer.WriteUInt32(flags);
            return writer.CreateMessage();
        }
    }
    public Task DisconnectAsync()
    {
        return this.Connection.CallMethodAsync(CreateMessage());
        MessageBuffer CreateMessage()
        {
            var writer = this.Connection.GetMessageWriter();
            writer.WriteMethodCallHeader(
                destination: Service.Destination,
                path: Path,
                @interface: __Interface,
                member: "Disconnect");
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
    public ValueTask<IDisposable> WatchStateChangedAsync(Action<Exception?, (uint NewState, uint OldState, uint Reason)> handler, bool emitOnCapturedContext = true, ObserverFlags flags = ObserverFlags.None)
        => base.WatchSignalAsync(Service.Destination, __Interface, Path, "StateChanged", (Message m, object? s) => ReadMessage_uuu(m, (NetworkManagerObject)s!), handler, emitOnCapturedContext, flags);
    public Task SetManagedAsync(bool value)
    {
        return this.Connection.CallMethodAsync(CreateMessage());
        MessageBuffer CreateMessage()
        {
            var writer = this.Connection.GetMessageWriter();
            writer.WriteMethodCallHeader(
                destination: Service.Destination,
                path: Path,
                @interface: "org.freedesktop.DBus.Properties",
                signature: "ssv",
                member: "Set");
            writer.WriteString(__Interface);
            writer.WriteString("Managed");
            writer.WriteSignature("b");
            writer.WriteBool(value);
            return writer.CreateMessage();
        }
    }
    public Task SetAutoconnectAsync(bool value)
    {
        return this.Connection.CallMethodAsync(CreateMessage());
        MessageBuffer CreateMessage()
        {
            var writer = this.Connection.GetMessageWriter();
            writer.WriteMethodCallHeader(
                destination: Service.Destination,
                path: Path,
                @interface: "org.freedesktop.DBus.Properties",
                signature: "ssv",
                member: "Set");
            writer.WriteString(__Interface);
            writer.WriteString("Autoconnect");
            writer.WriteSignature("b");
            writer.WriteBool(value);
            return writer.CreateMessage();
        }
    }
    public Task<string> GetUdiAsync()
        => this.Connection.CallMethodAsync(CreateGetPropertyMessage(__Interface, "Udi"), (Message m, object? s) => ReadMessage_v_s(m, (NetworkManagerObject)s!), this);
    public Task<string> GetPathAsync()
        => this.Connection.CallMethodAsync(CreateGetPropertyMessage(__Interface, "Path"), (Message m, object? s) => ReadMessage_v_s(m, (NetworkManagerObject)s!), this);
    public Task<string> GetInterfaceAsync()
        => this.Connection.CallMethodAsync(CreateGetPropertyMessage(__Interface, "Interface"), (Message m, object? s) => ReadMessage_v_s(m, (NetworkManagerObject)s!), this);
    public Task<string> GetIpInterfaceAsync()
        => this.Connection.CallMethodAsync(CreateGetPropertyMessage(__Interface, "IpInterface"), (Message m, object? s) => ReadMessage_v_s(m, (NetworkManagerObject)s!), this);
    public Task<string> GetDriverAsync()
        => this.Connection.CallMethodAsync(CreateGetPropertyMessage(__Interface, "Driver"), (Message m, object? s) => ReadMessage_v_s(m, (NetworkManagerObject)s!), this);
    public Task<string> GetDriverVersionAsync()
        => this.Connection.CallMethodAsync(CreateGetPropertyMessage(__Interface, "DriverVersion"), (Message m, object? s) => ReadMessage_v_s(m, (NetworkManagerObject)s!), this);
    public Task<string> GetFirmwareVersionAsync()
        => this.Connection.CallMethodAsync(CreateGetPropertyMessage(__Interface, "FirmwareVersion"), (Message m, object? s) => ReadMessage_v_s(m, (NetworkManagerObject)s!), this);
    public Task<uint> GetCapabilitiesAsync()
        => this.Connection.CallMethodAsync(CreateGetPropertyMessage(__Interface, "Capabilities"), (Message m, object? s) => ReadMessage_v_u(m, (NetworkManagerObject)s!), this);
    public Task<uint> GetIp4AddressAsync()
        => this.Connection.CallMethodAsync(CreateGetPropertyMessage(__Interface, "Ip4Address"), (Message m, object? s) => ReadMessage_v_u(m, (NetworkManagerObject)s!), this);
    public Task<uint> GetStateAsync()
        => this.Connection.CallMethodAsync(CreateGetPropertyMessage(__Interface, "State"), (Message m, object? s) => ReadMessage_v_u(m, (NetworkManagerObject)s!), this);
    public Task<(uint, uint)> GetStateReasonAsync()
        => this.Connection.CallMethodAsync(CreateGetPropertyMessage(__Interface, "StateReason"), (Message m, object? s) => ReadMessage_v_ruuz(m, (NetworkManagerObject)s!), this);
    public Task<ObjectPath> GetActiveConnectionAsync()
        => this.Connection.CallMethodAsync(CreateGetPropertyMessage(__Interface, "ActiveConnection"), (Message m, object? s) => ReadMessage_v_o(m, (NetworkManagerObject)s!), this);
    public Task<ObjectPath> GetIp4ConfigAsync()
        => this.Connection.CallMethodAsync(CreateGetPropertyMessage(__Interface, "Ip4Config"), (Message m, object? s) => ReadMessage_v_o(m, (NetworkManagerObject)s!), this);
    public Task<ObjectPath> GetDhcp4ConfigAsync()
        => this.Connection.CallMethodAsync(CreateGetPropertyMessage(__Interface, "Dhcp4Config"), (Message m, object? s) => ReadMessage_v_o(m, (NetworkManagerObject)s!), this);
    public Task<ObjectPath> GetIp6ConfigAsync()
        => this.Connection.CallMethodAsync(CreateGetPropertyMessage(__Interface, "Ip6Config"), (Message m, object? s) => ReadMessage_v_o(m, (NetworkManagerObject)s!), this);
    public Task<ObjectPath> GetDhcp6ConfigAsync()
        => this.Connection.CallMethodAsync(CreateGetPropertyMessage(__Interface, "Dhcp6Config"), (Message m, object? s) => ReadMessage_v_o(m, (NetworkManagerObject)s!), this);
    public Task<bool> GetManagedAsync()
        => this.Connection.CallMethodAsync(CreateGetPropertyMessage(__Interface, "Managed"), (Message m, object? s) => ReadMessage_v_b(m, (NetworkManagerObject)s!), this);
    public Task<bool> GetAutoconnectAsync()
        => this.Connection.CallMethodAsync(CreateGetPropertyMessage(__Interface, "Autoconnect"), (Message m, object? s) => ReadMessage_v_b(m, (NetworkManagerObject)s!), this);
    public Task<bool> GetFirmwareMissingAsync()
        => this.Connection.CallMethodAsync(CreateGetPropertyMessage(__Interface, "FirmwareMissing"), (Message m, object? s) => ReadMessage_v_b(m, (NetworkManagerObject)s!), this);
    public Task<bool> GetNmPluginMissingAsync()
        => this.Connection.CallMethodAsync(CreateGetPropertyMessage(__Interface, "NmPluginMissing"), (Message m, object? s) => ReadMessage_v_b(m, (NetworkManagerObject)s!), this);
    public Task<uint> GetDeviceTypeAsync()
        => this.Connection.CallMethodAsync(CreateGetPropertyMessage(__Interface, "DeviceType"), (Message m, object? s) => ReadMessage_v_u(m, (NetworkManagerObject)s!), this);
    public Task<ObjectPath[]> GetAvailableConnectionsAsync()
        => this.Connection.CallMethodAsync(CreateGetPropertyMessage(__Interface, "AvailableConnections"), (Message m, object? s) => ReadMessage_v_ao(m, (NetworkManagerObject)s!), this);
    public Task<string> GetPhysicalPortIdAsync()
        => this.Connection.CallMethodAsync(CreateGetPropertyMessage(__Interface, "PhysicalPortId"), (Message m, object? s) => ReadMessage_v_s(m, (NetworkManagerObject)s!), this);
    public Task<uint> GetMtuAsync()
        => this.Connection.CallMethodAsync(CreateGetPropertyMessage(__Interface, "Mtu"), (Message m, object? s) => ReadMessage_v_u(m, (NetworkManagerObject)s!), this);
    public Task<uint> GetMeteredAsync()
        => this.Connection.CallMethodAsync(CreateGetPropertyMessage(__Interface, "Metered"), (Message m, object? s) => ReadMessage_v_u(m, (NetworkManagerObject)s!), this);
    public Task<Dictionary<string, VariantValue>[]> GetLldpNeighborsAsync()
        => this.Connection.CallMethodAsync(CreateGetPropertyMessage(__Interface, "LldpNeighbors"), (Message m, object? s) => ReadMessage_v_aaesv(m, (NetworkManagerObject)s!), this);
    public Task<bool> GetRealAsync()
        => this.Connection.CallMethodAsync(CreateGetPropertyMessage(__Interface, "Real"), (Message m, object? s) => ReadMessage_v_b(m, (NetworkManagerObject)s!), this);
    public Task<uint> GetIp4ConnectivityAsync()
        => this.Connection.CallMethodAsync(CreateGetPropertyMessage(__Interface, "Ip4Connectivity"), (Message m, object? s) => ReadMessage_v_u(m, (NetworkManagerObject)s!), this);
    public Task<uint> GetIp6ConnectivityAsync()
        => this.Connection.CallMethodAsync(CreateGetPropertyMessage(__Interface, "Ip6Connectivity"), (Message m, object? s) => ReadMessage_v_u(m, (NetworkManagerObject)s!), this);
    public Task<uint> GetInterfaceFlagsAsync()
        => this.Connection.CallMethodAsync(CreateGetPropertyMessage(__Interface, "InterfaceFlags"), (Message m, object? s) => ReadMessage_v_u(m, (NetworkManagerObject)s!), this);
    public Task<string> GetHwAddressAsync()
        => this.Connection.CallMethodAsync(CreateGetPropertyMessage(__Interface, "HwAddress"), (Message m, object? s) => ReadMessage_v_s(m, (NetworkManagerObject)s!), this);
    public Task<ObjectPath[]> GetPortsAsync()
        => this.Connection.CallMethodAsync(CreateGetPropertyMessage(__Interface, "Ports"), (Message m, object? s) => ReadMessage_v_ao(m, (NetworkManagerObject)s!), this);
    public Task<DeviceProperties> GetPropertiesAsync()
    {
        return this.Connection.CallMethodAsync(CreateGetAllPropertiesMessage(__Interface), (Message m, object? s) => ReadMessage(m, (NetworkManagerObject)s!), this);
        static DeviceProperties ReadMessage(Message message, NetworkManagerObject _)
        {
            var reader = message.GetBodyReader();
            return ReadProperties(ref reader);
        }
    }
    public ValueTask<IDisposable> WatchPropertiesChangedAsync(Action<Exception?, PropertyChanges<DeviceProperties>> handler, bool emitOnCapturedContext = true, ObserverFlags flags = ObserverFlags.None)
    {
        return base.WatchPropertiesChangedAsync(__Interface, (Message m, object? s) => ReadMessage(m, (NetworkManagerObject)s!), handler, emitOnCapturedContext, flags);
        static PropertyChanges<DeviceProperties> ReadMessage(Message message, NetworkManagerObject _)
        {
            var reader = message.GetBodyReader();
            reader.ReadString(); // interface
            List<string> changed = new(), invalidated = new();
            return new PropertyChanges<DeviceProperties>(ReadProperties(ref reader, changed), ReadInvalidated(ref reader), changed.ToArray());
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
                    case "Udi": invalidated.Add("Udi"); break;
                    case "Path": invalidated.Add("Path"); break;
                    case "Interface": invalidated.Add("Interface"); break;
                    case "IpInterface": invalidated.Add("IpInterface"); break;
                    case "Driver": invalidated.Add("Driver"); break;
                    case "DriverVersion": invalidated.Add("DriverVersion"); break;
                    case "FirmwareVersion": invalidated.Add("FirmwareVersion"); break;
                    case "Capabilities": invalidated.Add("Capabilities"); break;
                    case "Ip4Address": invalidated.Add("Ip4Address"); break;
                    case "State": invalidated.Add("State"); break;
                    case "StateReason": invalidated.Add("StateReason"); break;
                    case "ActiveConnection": invalidated.Add("ActiveConnection"); break;
                    case "Ip4Config": invalidated.Add("Ip4Config"); break;
                    case "Dhcp4Config": invalidated.Add("Dhcp4Config"); break;
                    case "Ip6Config": invalidated.Add("Ip6Config"); break;
                    case "Dhcp6Config": invalidated.Add("Dhcp6Config"); break;
                    case "Managed": invalidated.Add("Managed"); break;
                    case "Autoconnect": invalidated.Add("Autoconnect"); break;
                    case "FirmwareMissing": invalidated.Add("FirmwareMissing"); break;
                    case "NmPluginMissing": invalidated.Add("NmPluginMissing"); break;
                    case "DeviceType": invalidated.Add("DeviceType"); break;
                    case "AvailableConnections": invalidated.Add("AvailableConnections"); break;
                    case "PhysicalPortId": invalidated.Add("PhysicalPortId"); break;
                    case "Mtu": invalidated.Add("Mtu"); break;
                    case "Metered": invalidated.Add("Metered"); break;
                    case "LldpNeighbors": invalidated.Add("LldpNeighbors"); break;
                    case "Real": invalidated.Add("Real"); break;
                    case "Ip4Connectivity": invalidated.Add("Ip4Connectivity"); break;
                    case "Ip6Connectivity": invalidated.Add("Ip6Connectivity"); break;
                    case "InterfaceFlags": invalidated.Add("InterfaceFlags"); break;
                    case "HwAddress": invalidated.Add("HwAddress"); break;
                    case "Ports": invalidated.Add("Ports"); break;
                }
            }
            return invalidated?.ToArray() ?? Array.Empty<string>();
        }
    }
    private static DeviceProperties ReadProperties(ref Reader reader, List<string>? changedList = null)
    {
        var props = new DeviceProperties();
        ArrayEnd arrayEnd = reader.ReadArrayStart(DBusType.Struct);
        while (reader.HasNext(arrayEnd))
        {
            var property = reader.ReadString();
            switch (property)
            {
                case "Udi":
                    reader.ReadSignature("s"u8);
                    props.Udi = reader.ReadString();
                    changedList?.Add("Udi");
                    break;
                case "Path":
                    reader.ReadSignature("s"u8);
                    props.Path = reader.ReadString();
                    changedList?.Add("Path");
                    break;
                case "Interface":
                    reader.ReadSignature("s"u8);
                    props.Interface = reader.ReadString();
                    changedList?.Add("Interface");
                    break;
                case "IpInterface":
                    reader.ReadSignature("s"u8);
                    props.IpInterface = reader.ReadString();
                    changedList?.Add("IpInterface");
                    break;
                case "Driver":
                    reader.ReadSignature("s"u8);
                    props.Driver = reader.ReadString();
                    changedList?.Add("Driver");
                    break;
                case "DriverVersion":
                    reader.ReadSignature("s"u8);
                    props.DriverVersion = reader.ReadString();
                    changedList?.Add("DriverVersion");
                    break;
                case "FirmwareVersion":
                    reader.ReadSignature("s"u8);
                    props.FirmwareVersion = reader.ReadString();
                    changedList?.Add("FirmwareVersion");
                    break;
                case "Capabilities":
                    reader.ReadSignature("u"u8);
                    props.Capabilities = reader.ReadUInt32();
                    changedList?.Add("Capabilities");
                    break;
                case "Ip4Address":
                    reader.ReadSignature("u"u8);
                    props.Ip4Address = reader.ReadUInt32();
                    changedList?.Add("Ip4Address");
                    break;
                case "State":
                    reader.ReadSignature("u"u8);
                    props.State = reader.ReadUInt32();
                    changedList?.Add("State");
                    break;
                case "StateReason":
                    reader.ReadSignature("(uu)"u8);
                    props.StateReason = ReadType_ruuz(ref reader);
                    changedList?.Add("StateReason");
                    break;
                case "ActiveConnection":
                    reader.ReadSignature("o"u8);
                    props.ActiveConnection = reader.ReadObjectPath();
                    changedList?.Add("ActiveConnection");
                    break;
                case "Ip4Config":
                    reader.ReadSignature("o"u8);
                    props.Ip4Config = reader.ReadObjectPath();
                    changedList?.Add("Ip4Config");
                    break;
                case "Dhcp4Config":
                    reader.ReadSignature("o"u8);
                    props.Dhcp4Config = reader.ReadObjectPath();
                    changedList?.Add("Dhcp4Config");
                    break;
                case "Ip6Config":
                    reader.ReadSignature("o"u8);
                    props.Ip6Config = reader.ReadObjectPath();
                    changedList?.Add("Ip6Config");
                    break;
                case "Dhcp6Config":
                    reader.ReadSignature("o"u8);
                    props.Dhcp6Config = reader.ReadObjectPath();
                    changedList?.Add("Dhcp6Config");
                    break;
                case "Managed":
                    reader.ReadSignature("b"u8);
                    props.Managed = reader.ReadBool();
                    changedList?.Add("Managed");
                    break;
                case "Autoconnect":
                    reader.ReadSignature("b"u8);
                    props.Autoconnect = reader.ReadBool();
                    changedList?.Add("Autoconnect");
                    break;
                case "FirmwareMissing":
                    reader.ReadSignature("b"u8);
                    props.FirmwareMissing = reader.ReadBool();
                    changedList?.Add("FirmwareMissing");
                    break;
                case "NmPluginMissing":
                    reader.ReadSignature("b"u8);
                    props.NmPluginMissing = reader.ReadBool();
                    changedList?.Add("NmPluginMissing");
                    break;
                case "DeviceType":
                    reader.ReadSignature("u"u8);
                    props.DeviceType = reader.ReadUInt32();
                    changedList?.Add("DeviceType");
                    break;
                case "AvailableConnections":
                    reader.ReadSignature("ao"u8);
                    props.AvailableConnections = reader.ReadArrayOfObjectPath();
                    changedList?.Add("AvailableConnections");
                    break;
                case "PhysicalPortId":
                    reader.ReadSignature("s"u8);
                    props.PhysicalPortId = reader.ReadString();
                    changedList?.Add("PhysicalPortId");
                    break;
                case "Mtu":
                    reader.ReadSignature("u"u8);
                    props.Mtu = reader.ReadUInt32();
                    changedList?.Add("Mtu");
                    break;
                case "Metered":
                    reader.ReadSignature("u"u8);
                    props.Metered = reader.ReadUInt32();
                    changedList?.Add("Metered");
                    break;
                case "LldpNeighbors":
                    reader.ReadSignature("aa{sv}"u8);
                    props.LldpNeighbors = ReadType_aaesv(ref reader);
                    changedList?.Add("LldpNeighbors");
                    break;
                case "Real":
                    reader.ReadSignature("b"u8);
                    props.Real = reader.ReadBool();
                    changedList?.Add("Real");
                    break;
                case "Ip4Connectivity":
                    reader.ReadSignature("u"u8);
                    props.Ip4Connectivity = reader.ReadUInt32();
                    changedList?.Add("Ip4Connectivity");
                    break;
                case "Ip6Connectivity":
                    reader.ReadSignature("u"u8);
                    props.Ip6Connectivity = reader.ReadUInt32();
                    changedList?.Add("Ip6Connectivity");
                    break;
                case "InterfaceFlags":
                    reader.ReadSignature("u"u8);
                    props.InterfaceFlags = reader.ReadUInt32();
                    changedList?.Add("InterfaceFlags");
                    break;
                case "HwAddress":
                    reader.ReadSignature("s"u8);
                    props.HwAddress = reader.ReadString();
                    changedList?.Add("HwAddress");
                    break;
                case "Ports":
                    reader.ReadSignature("ao"u8);
                    props.Ports = reader.ReadArrayOfObjectPath();
                    changedList?.Add("Ports");
                    break;
                default:
                    reader.ReadVariantValue();
                    break;
            }
        }
        return props;
    }
}