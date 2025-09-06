using Connection = Tmds.DBus.Protocol.Connection;
using NetworkManager.DBus;
using Tmds.DBus.Protocol;
using System.Text;

string? systemBusAddress = Address.System;
if (systemBusAddress is null)
{
    Console.Write("Can not determine system bus address");
    return 1;
}

Connection connection = new Connection(Address.System!);
await connection.ConnectAsync();
Console.WriteLine("Connected to system bus.");

var service = new NetworkManagerService(connection, "org.freedesktop.NetworkManager");
var networkManager = service.CreateNetworkManager("/org/freedesktop/NetworkManager");

NMConnectivityState connectivity = (NMConnectivityState)await networkManager.GetConnectivityAsync();
Console.WriteLine($"Current connectivity: {connectivity}");

try
{

    ObjectPath wlan0Path = await networkManager.GetDeviceByIpIfaceAsync("wlan0");
    Console.WriteLine($"Found wwan0 device at: {wlan0Path}");
    // You can now use this path to get a device object and interact with it
    var wlan0Device = service.CreateWireless(wlan0Path);

    Console.WriteLine("Requesting a scan for available Wi-Fi networks...");

    await wlan0Device.RequestScanAsync(new Dictionary<string, VariantValue>());

    // NM80211Mode deviceMode = (NM80211Mode)await wlan0Device.GetModeAsync();

    // Console.WriteLine($"device mode: {deviceMode}");

    // await Task.Delay(5000);

    Console.WriteLine("Scan complete. Retrieving access points...");

    Console.WriteLine("Fetching all active connections...");

    var settingsService = service.CreateSettings("/org/freedesktop/NetworkManager/Settings");


// --- NEW CODE SECTION TO MIMIC 'nmcli connection show' ---
Console.WriteLine("\nDisplaying all saved connections (active and inactive):");
ObjectPath[] allSavedConnections = await settingsService.ListConnectionsAsync();

    if (allSavedConnections.Length == 0)
    {
        Console.WriteLine("No saved connections found.");
    }
    else
    {
        Console.WriteLine("NAME                       UUID                                  TYPE            DEVICE");
        Console.WriteLine("-----------------------------------------------------------------------------------------");

        var compatibleConnections = new List<(string Name, string Ssid, string Mode,string Security, ObjectPath ConnectionPath)>();

        foreach (var connPath in allSavedConnections)
        {
            var conn = service.CreateConnection(connPath);
            var settings = await conn.GetSettingsAsync();

            string name = "N/A";
            string uuid = "N/A";
            string type = "N/A";
            string device = "N/A"; // Device is not directly in settings, but can be inferred


            // Get Name, UUID, and Type from the "connection" section
            if (settings.TryGetValue("connection", out var connSection))
            {
                if (connSection.TryGetValue("id", out var idValue) && (DBusType)idValue.Type == DBusType.String)
                {
                    name = idValue.GetString();
                }
                if (connSection.TryGetValue("uuid", out var uuidValue) && (DBusType)uuidValue.Type == DBusType.String)
                {
                    uuid = uuidValue.GetString();
                }
                if (connSection.TryGetValue("type", out var typeValue) && (DBusType)typeValue.Type == DBusType.String)
                {
                    type = typeValue.GetString();
                }

                if (typeValue == "802-11-wireless" && settings.TryGetValue("802-11-wireless", out var wifiSection) &&
                    wifiSection.TryGetValue("ssid", out var ssidValue) &&
                    wifiSection.TryGetValue("mode", out var modeValue) &&
                    wifiSection.TryGetValue("security", out var securityValue))
                {
                    // Console.WriteLine(name);
                    // foreach (string key in wifiSection.Keys)
                    // {
                    //     Console.WriteLine(key);
                    // }
                    string ssid = Encoding.UTF8.GetString(ssidValue.GetArray<byte>());
                    string mode = modeValue.GetString();
                    compatibleConnections.Add((idValue.GetString(), ssid, modeValue.GetString(), securityValue.GetString(), connPath));
                }
            }

            Console.WriteLine($"{name,-25}  {uuid,-36}  {type,-15} {device,-10}");
        }
    
        Console.WriteLine("the following networks you are already connected to:");
        Console.WriteLine("\nSelect a network to connect to:");

        for (int i = 0; i < compatibleConnections.Count; i++)
        {
            var connectionToConnect = compatibleConnections[i];
            Console.WriteLine($"  [{i}] {connectionToConnect.Name}, SSSID: {connectionToConnect.Ssid}, Mode: {connectionToConnect.Mode}, Security: {connectionToConnect.Security}");
        }
        Console.Write("Enter the number of the network to connect to: ");
        if (int.TryParse(Console.ReadLine(), out int selectedIndex) &&
            selectedIndex >= 0 && selectedIndex < compatibleConnections.Count)
        {
            var con = compatibleConnections[selectedIndex];
            var activeConn = await networkManager.ActivateConnectionAsync(con.ConnectionPath, wlan0Path, con.ConnectionPath);
            Console.WriteLine($"Connection activated: {activeConn}");
        }
}
    
    ObjectPath[] accessPointsPaths = await wlan0Device.GetAccessPointsAsync();

    if (accessPointsPaths.Length == 0)
    {
        Console.WriteLine("No access points found.");
    }
    else
    {
        Console.WriteLine("Found the following networks:");
        Console.WriteLine("\nSelect a network to connect to:");
        var accessPoints = new List<(string ssid, ObjectPath path)>();

        for (int i = 0; i < accessPointsPaths.Length; i++)
        {
            var accessPoint = service.CreateAccessPoint(accessPointsPaths[i]);
            byte[] ssidBytes = await accessPoint.GetSsidAsync();
            string ssid = Encoding.UTF8.GetString(ssidBytes);
            byte strength = await accessPoint.GetStrengthAsync();
            accessPoints.Add((ssid, accessPointsPaths[i]));
            Console.WriteLine($"  [{i}] {ssid}, Signal Strength: {strength}%");
        }

        Console.Write("Enter the number of the network to connect to: ");
        if (int.TryParse(Console.ReadLine(), out int selectedIndex) &&
            selectedIndex >= 0 && selectedIndex < accessPoints.Count)
        {
            string selectedSsid = accessPoints[selectedIndex].ssid;
            Console.Write($"Enter password for '{selectedSsid}': ");
            string password = Console.ReadLine() ?? "";

            Console.WriteLine($"Attempting to connect to '{selectedSsid}'...");

            // Create connection settings
            var wifiSettings = new Dictionary<string, Dictionary<string, VariantValue>>
            {
                ["connection"] = new Dictionary<string, VariantValue>
                {
                    ["id"] = $"wifi-{selectedSsid}",
                    ["type"] = "802-11-wireless",
                    ["interface-name"] = "wlan0",
                    ["autoconnect"] = true
                },
                ["802-11-wireless"] = new Dictionary<string, VariantValue>
                {
                    ["ssid"] = VariantValue.Array(Encoding.UTF8.GetBytes(selectedSsid)),
                    ["mode"] = "infrastructure"
                },
                ["802-11-wireless-security"] = new Dictionary<string, VariantValue>
                {
                    ["key-mgmt"] = "wpa-psk",
                    ["psk"] = password
                },
                ["ipv4"] = new Dictionary<string, VariantValue>
                {
                    ["method"] = "auto"
                },
                ["ipv6"] = new Dictionary<string, VariantValue>
                {
                    ["method"] = "ignore"
                }
            };
            
            // (ObjectPath Path, ObjectPath ActiveConnection) activeConn = await networkManager.AddAndActivateConnectionAsync(wifiSettings, wlan0Path, accessPoints[selectedIndex].path);
            ObjectPath wifiConnPath = await settingsService.AddConnectionAsync(wifiSettings);
            var activeConn = await networkManager.ActivateConnectionAsync(wifiConnPath, wlan0Path, accessPoints[selectedIndex].path);
            Console.WriteLine($"Connection activated: {activeConn}");
        }
        else
        {
            Console.WriteLine("Invalid selection.");
        }
    }
}
catch (Exception ex)
{
    // This exception is thrown if the device is not found
    Console.WriteLine($"Error getting device: {ex.Message}");
    Console.WriteLine("Could not find wwan0 or the interface name is incorrect.");
}

Exception? disconnectReason = await connection.DisconnectedAsync();
if (disconnectReason is not null)
{
    Console.WriteLine("The connection was closed:");
    Console.WriteLine(disconnectReason);
    return 1;
}
return 0;