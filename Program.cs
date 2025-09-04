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

NMConnectivityState connectivity = (NMConnectivityState) await networkManager.GetConnectivityAsync();
Console.WriteLine($"Current connectivity: {connectivity}");

try
{

    ObjectPath wlan0Path = await networkManager.GetDeviceByIpIfaceAsync("wlan0");
    Console.WriteLine($"Found wwan0 device at: {wlan0Path}");
    // You can now use this path to get a device object and interact with it
    var wlan0Device = service.CreateWireless(wlan0Path);

    Console.WriteLine("Requesting a scan for available Wi-Fi networks...");

    await wlan0Device.RequestScanAsync(new Dictionary<string, VariantValue>());

    NM80211Mode deviceMode = (NM80211Mode)await wlan0Device.GetModeAsync();

    Console.WriteLine($"device mode: {deviceMode}");

    await Task.Delay(5000);

    Console.WriteLine("Scan complete. Retrieving access points...");

    ObjectPath[] accessPointsPaths = await wlan0Device.GetAccessPointsAsync();

    if (accessPointsPaths.Length == 0)
    {
        Console.WriteLine("No access points found.");
    }
    else
    {
        Console.WriteLine("Found the following networks:");
        foreach (var apPath in accessPointsPaths)
        {
            var accessPoint = service.CreateAccessPoint(apPath);

            byte[] ssidBytes = await accessPoint.GetSsidAsync();
            string ssid = Encoding.UTF8.GetString(ssidBytes);

            byte strength = await accessPoint.GetStrengthAsync();

            Console.WriteLine($"  SSID: {ssid}, Signal Strength: {strength}%");
        }
    }
    
    if (accessPointsPaths.Length > 0)
    {
        Console.WriteLine("\nSelect a network to connect to:");
        var accessPoints = new List<(string ssid, ObjectPath path)>();

        for (int i = 0; i < accessPointsPaths.Length; i++)
        {
            var ap = service.CreateAccessPoint(accessPointsPaths[i]);
            byte[] ssidBytes = await ap.GetSsidAsync();
            string ssid = Encoding.UTF8.GetString(ssidBytes);
            accessPoints.Add((ssid, accessPointsPaths[i]));
            Console.WriteLine($"  [{i}] {ssid}");
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

            var settingsService = service.CreateSettings("/org/freedesktop/NetworkManager/Settings");
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

    // ObjectPath currentConnections = await networkManager.GetConnectionsAsync();
    // foreach (var conn in currentConnections)
    // {
    //     var connection = service.CreateConnection(conn);

    //     var connSettings = await connection.GetSettingsAsync();
    //     // string ssid = Encoding.UTF8.GetString(ssidBytes);

    //     Console.WriteLine($"Settings: {connSettings}");
    // }
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


Dictionary<string, Dictionary<string, VariantValue>> ConvertToVariantSettings(
    Dictionary<string, IDictionary<string, object>> rawSettings)
{
    var result = new Dictionary<string, Dictionary<string, VariantValue>>();

    foreach (var section in rawSettings)
    {
        var variantSection = new Dictionary<string, VariantValue>();
        foreach (var kvp in section.Value)
        {
            object value = kvp.Value;

            // Handle byte[] for SSID
            if (value is byte[] byteArray)
                variantSection[kvp.Key] = VariantValue.Array(byteArray);
        }
        result[section.Key] = variantSection;
    }

    return result;
}