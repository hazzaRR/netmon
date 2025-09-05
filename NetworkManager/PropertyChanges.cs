using System;
using Tmds.DBus.Protocol;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace NetworkManager.DBus;

class PropertyChanges<TProperties>
{
    public PropertyChanges(TProperties properties, string[] invalidated, string[] changed)
        => (Properties, Invalidated, Changed) = (properties, invalidated, changed);
    public TProperties Properties { get; }
    public string[] Invalidated { get; }
    public string[] Changed { get; }
    public bool HasChanged(string property) => Array.IndexOf(Changed, property) != -1;
    public bool IsInvalidated(string property) => Array.IndexOf(Invalidated, property) != -1;
}