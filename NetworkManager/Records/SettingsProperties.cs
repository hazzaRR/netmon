using System;
using Tmds.DBus.Protocol;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace NetworkManager.DBus;

record SettingsProperties
{
    public ObjectPath[] Connections { get; set; } = default!;
    public string Hostname { get; set; } = default!;
    public bool CanModify { get; set; } = default!;
}