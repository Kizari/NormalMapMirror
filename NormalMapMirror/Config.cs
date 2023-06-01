using System.Diagnostics.CodeAnalysis;

namespace NormalMapMirror;

// Ensures trimmer doesn't prevent reflection being used to deserialise
[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)]
public class Config
{
    public Config()
    {
        // Create a default config file if one doesn't exist
        if (!File.Exists(ConfigPath))
        {
            CreateDefaultConfig();
        }

        Deserialize();
    }

    private static string ConfigPath => Path.Combine(Path.GetDirectoryName(Environment.ProcessPath)!, "config.ini");

    public bool XMirror { get; set; } = true;
    public bool YMirror { get; set; } = true;
    public bool ZMirror { get; set; } = true;

    /// Deserialises the contents of the config file into this class instance
    private void Deserialize()
    {
        var lines = File.ReadAllLines(ConfigPath);
        foreach (var line in lines.Where(l => l.Contains('=')))
        {
            var tokens = line.Split('=').Select(t => t.Trim()).ToList();
            foreach (var property in GetType().GetProperties())
            {
                if (tokens[0].Equals(property.Name, StringComparison.OrdinalIgnoreCase))
                {
                    if (bool.TryParse(tokens[1], out var result))
                    {
                        property.SetValue(this, result);
                    }
                }
            }
        }
    }

    /// Generates a default config file and writes it to the config path
    private static void CreateDefaultConfig()
    {
        var lines = new string[4];
        lines[0] = "[Outputs]";
        lines[1] = "XMirror=true";
        lines[2] = "YMirror=true";
        lines[3] = "ZMirror=true";
        File.WriteAllLines(ConfigPath, lines);
    }
}