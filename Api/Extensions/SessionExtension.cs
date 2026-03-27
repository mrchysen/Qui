using Qui.Api.Extensions;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

namespace Qui.Api.Extensions;

/// <summary>
/// Custom extension for sessions to make json serialization of object to 
/// store it
/// </summary>
public static class SessionExtension
{
    public static void Set<T>(this ISession session, string key, T value) where T : class
    {
        var jsonOptions = new JsonSerializerOptions
        {
            Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
            WriteIndented = true
        };
        string json = JsonSerializer.Serialize<T>(value, jsonOptions);

        session.SetString(key, json);
    }

    public static T Get<T>(this ISession session, string key) where T : new()
    {
        if(!session.Keys.Contains(key))
            return new();

        T result = JsonSerializer.Deserialize<T>(session.Get(key));
        return result == null ? default : result;
    }
}
