using Connection = Tmds.DBus.Protocol.Connection;
using NetworkManager.DBus;
using Tmds.DBus.Protocol;
using System.Text;
using System.Linq;
using System.Net.NetworkInformation;

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
var settingsService = service.CreateSettings("/org/freedesktop/NetworkManager/Settings");

// Check for existing internet connectivity using HttpClient
Console.WriteLine("Checking for internet connectivity...");
bool isOnline = await IsInternetConnected(5, TimeSpan.FromSeconds(5)); // 5 retries, 5-second delay

if (isOnline)
{
    Console.WriteLine("Internet connection is active. Continuing as normal.");
}
try
{
    ObjectPath wlan0Path = await networkManager.GetDeviceByIpIfaceAsync("wlan0");
    Console.WriteLine($"Found wlan0 device at: {wlan0Path}");
    var wlan0Device = service.CreateWireless(wlan0Path);

    Console.WriteLine("Scanning for available Wi-Fi networks...");
    await wlan0Device.RequestScanAsync(new Dictionary<string, VariantValue>());
    await Task.Delay(5000);
    Console.WriteLine("Scan complete. Retrieving access points...");

    var savedConnections = new Dictionary<string, ObjectPath>();
    ObjectPath[] allSavedConnections = await settingsService.ListConnectionsAsync();

    foreach (var connPath in allSavedConnections)
    {
        var conn = service.CreateConnection(connPath);
        var settings = await conn.GetSettingsAsync();

        if (settings.TryGetValue("connection", out var connSection) &&
            connSection.TryGetValue("type", out var typeValue) &&
            typeValue.GetString() == "802-11-wireless" &&
            settings.TryGetValue("802-11-wireless", out var wifiSection) &&
            wifiSection.TryGetValue("ssid", out var ssidValue))
        {
            string ssid = Encoding.UTF8.GetString(ssidValue.GetArray<byte>());
            if (!savedConnections.ContainsKey(ssid))
            {
                savedConnections.Add(ssid, connPath);
            }
        }
    }

    ObjectPath[] availableAccessPointsPaths = await wlan0Device.GetAccessPointsAsync();
    ObjectPath activeApPath = await wlan0Device.GetActiveAccessPointAsync();

    var combinedNetworks = new List<(string Ssid, string Mode, uint Rate, byte Signal, string Bars, string Security, bool IsSaved, bool IsActive, ObjectPath ApPath)>();

    foreach (var apPath in availableAccessPointsPaths)
    {
        var accessPoint = service.CreateAccessPoint(apPath);

        byte[] ssidBytes = await accessPoint.GetSsidAsync();
        string ssid = Encoding.UTF8.GetString(ssidBytes);
        byte strength = await accessPoint.GetStrengthAsync();
        uint rate = await accessPoint.GetMaxBitrateAsync();
        string security = GetSecurityString((NM80211ApSecurityFlags)await accessPoint.GetRsnFlagsAsync() | (NM80211ApSecurityFlags)await accessPoint.GetWpaFlagsAsync() | (NM80211ApSecurityFlags)await accessPoint.GetFlagsAsync());
        // string security = "";

        bool isSaved = savedConnections.ContainsKey(ssid);
        bool isActive = apPath == activeApPath;
        string bars = GetSignalBars(strength);
        
        // Populate the new tuple with the relevant data
        combinedNetworks.Add((ssid, "Infra", rate, strength, bars, security, isSaved, isActive, apPath));
    }
    
    combinedNetworks = combinedNetworks.OrderByDescending(n => n.Signal).ToList();

    // Updated header and formatting to match the new columns
    Console.WriteLine("IN-USE  SSID                   MODE    RATE      SIGNAL  BARS    SECURITY   SAVED");
    Console.WriteLine("-----------------------------------------------------------------------------------");

    for (int i = 0; i < combinedNetworks.Count; i++)
    {
        var net = combinedNetworks[i];
        string inUse = net.IsActive ? "*" : " ";
        string saved = net.IsSaved ? "Yes" : "";
        string formattedSsid = net.Ssid.Length > 20 ? net.Ssid.Substring(0, 17) + "..." : net.Ssid;

        // Updated format string to remove BSSID and Channel
        Console.WriteLine($"[{i}] {inUse}    {formattedSsid,-20} {net.Mode,-7} {net.Rate / 1000,-4}Mbit/s {net.Signal,-6} {net.Bars,-8} {net.Security,-10} {saved,-5}");
    }

    Console.WriteLine("-----------------------------------------------------------------------------------");

    Console.Write("Enter the number of the network to connect to: ");
    if (int.TryParse(Console.ReadLine(), out int selectedIndex) &&
        selectedIndex >= 0 && selectedIndex < combinedNetworks.Count)
    {
        var selectedNetwork = combinedNetworks[selectedIndex];

        if (selectedNetwork.IsSaved)
        {
            Console.WriteLine($"Attempting to activate saved network '{selectedNetwork.Ssid}'...");
            if (savedConnections.TryGetValue(selectedNetwork.Ssid, out var savedConnPath))
            {
                var activeConn = await networkManager.ActivateConnectionAsync(
                    savedConnPath,
                    wlan0Path,
                    selectedNetwork.ApPath
                );
                Console.WriteLine($"Connection activated: {activeConn}");
            }
        }
        else
        {
            Console.Write($"Enter password for '{selectedNetwork.Ssid}': ");
            string password = Console.ReadLine() ?? "";

            Console.WriteLine($"Attempting to connect to new network '{selectedNetwork.Ssid}'...");

            var wifiSettings = new Dictionary<string, Dictionary<string, VariantValue>>
            {
                ["connection"] = new Dictionary<string, VariantValue>
                {
                    ["id"] = $"wifi-{selectedNetwork.Ssid}",
                    ["type"] = "802-11-wireless",
                    ["interface-name"] = "wlan0",
                    ["autoconnect"] = true
                },
                ["802-11-wireless"] = new Dictionary<string, VariantValue>
                {
                    ["ssid"] = VariantValue.Array(Encoding.UTF8.GetBytes(selectedNetwork.Ssid)),
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
            
            ObjectPath wifiConnPath = await settingsService.AddConnectionAsync(wifiSettings);
            var activeConn = await networkManager.ActivateConnectionAsync(
                wifiConnPath,
                wlan0Path,
                selectedNetwork.ApPath
            );
            Console.WriteLine($"Connection activated: {activeConn}");
        }
    }
    else
    {
        Console.WriteLine("Invalid selection.");
    }
}
catch (Exception ex)
{
    Console.WriteLine($"Error: {ex.Message}");
    Console.WriteLine("Could not find wlan0 or an error occurred during connection.");
}

Exception? disconnectReason = await connection.DisconnectedAsync();
if (disconnectReason is not null)
{
    Console.WriteLine("The connection was closed:");
    Console.WriteLine(disconnectReason);
    return 1;
}

return 0;

// Helper functions (kept as they are still needed)
string GetSignalBars(byte strength)
{
    if (strength > 85) return "▂▄▆█";
    if (strength > 70) return "▂▄▆_";
    if (strength > 55) return "▂▄__";
    if (strength > 40) return "▂___";
    return "____";
}

string GetSecurityString(NM80211ApSecurityFlags flags)
{
    var security = new List<string>();

    // Check for WPA2
    if (flags.HasFlag(NM80211ApSecurityFlags.NM_802_11_AP_SEC_PAIR_CCMP) &&
        flags.HasFlag(NM80211ApSecurityFlags.NM_802_11_AP_SEC_KEY_MGMT_PSK))
    {
        security.Add("WPA2");
    }
    // Check for WPA
    else if (flags.HasFlag(NM80211ApSecurityFlags.NM_802_11_AP_SEC_PAIR_TKIP) &&
        flags.HasFlag(NM80211ApSecurityFlags.NM_802_11_AP_SEC_KEY_MGMT_PSK))
    {
        security.Add("WPA");
    }

    // Check for 802.1X
    if (flags.HasFlag(NM80211ApSecurityFlags.NM_802_11_AP_SEC_KEY_MGMT_802_1X))
    {
        security.Add("802.1X");
    }

    // Check for WEP
    if (flags.HasFlag(NM80211ApSecurityFlags.NM_802_11_AP_SEC_PAIR_WEP40) ||
        flags.HasFlag(NM80211ApSecurityFlags.NM_802_11_AP_SEC_PAIR_WEP104))
    {
        security.Add("WEP");
    }

    if (security.Count == 0)
        return "--";

    return string.Join(" ", security);
}

static async Task<bool> IsInternetConnected(int retries, TimeSpan delay)
{
    using (var httpClient = new HttpClient())
    {
        // Set a short timeout to fail quickly on no connection
        httpClient.Timeout = TimeSpan.FromSeconds(5);
        for (int i = 0; i < retries; i++)
        {
            try
            {
                // Send a simple HEAD request to a reliable public server.
                // HEAD is more efficient as it doesn't download the body.
                var response = await httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Head, "http://www.google.com"));
                
                // If the response is successful (200-299), we are online
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
            }
            catch (Exception)
            {
                // Request failed (e.g., no DNS resolution, timeout), continue to the next retry
            }
            await Task.Delay(delay);
        }
    }
    return false;
}