using System;
using Tmds.DBus.Protocol;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace NetworkManager.DBus;
partial class AgentManager : NetworkManagerObject
{
    private const string __Interface = "org.freedesktop.NetworkManager.AgentManager";
    public AgentManager(NetworkManagerService service, ObjectPath path) : base(service, path)
    { }
    public Task RegisterAsync(string identifier)
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
                member: "Register");
            writer.WriteString(identifier);
            return writer.CreateMessage();
        }
    }
    public Task RegisterWithCapabilitiesAsync(string identifier, uint capabilities)
    {
        return this.Connection.CallMethodAsync(CreateMessage());
        MessageBuffer CreateMessage()
        {
            var writer = this.Connection.GetMessageWriter();
            writer.WriteMethodCallHeader(
                destination: Service.Destination,
                path: Path,
                @interface: __Interface,
                signature: "su",
                member: "RegisterWithCapabilities");
            writer.WriteString(identifier);
            writer.WriteUInt32(capabilities);
            return writer.CreateMessage();
        }
    }
    public Task UnregisterAsync()
    {
        return this.Connection.CallMethodAsync(CreateMessage());
        MessageBuffer CreateMessage()
        {
            var writer = this.Connection.GetMessageWriter();
            writer.WriteMethodCallHeader(
                destination: Service.Destination,
                path: Path,
                @interface: __Interface,
                member: "Unregister");
            return writer.CreateMessage();
        }
    }
}
