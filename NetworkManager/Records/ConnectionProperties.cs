namespace NetworkManager.DBus;
record ConnectionProperties
{
    public bool Unsaved { get; set; } = default!;
    public uint Flags { get; set; } = default!;
    public string Filename { get; set; } = default!;
}