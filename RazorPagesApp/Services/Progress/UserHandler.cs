using RazorPagesApp.Models;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using RazorPagesApp.Extensions;

namespace RazorPagesApp.Services.Progress;

/// <summary>
/// Простой сервис для сохранения результатов в файлик
/// использовался для тестов
/// </summary>
public class UserHandler : IUserCRUD
{
    public string FilePath { get; set; } = Path.Combine(AppContext.BaseDirectory, "Progress.txt");

    
    public void SaveUser(User user)
    {
        using (StreamWriter sw = new(FilePath, true, System.Text.Encoding.UTF8))
        {
            var jsonOptions = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
                WriteIndented = true
            };

            string json = JsonSerializer.Serialize(user, jsonOptions);

            sw.Write(',');
            sw.WriteLine(json);
        }
    }

    public List<User> GetUsers()
    {
        if (!File.Exists(FilePath))
        {
            return new List<User>();
        }

        string allJson = "[" + File.ReadAllText(FilePath).ReplaceFirst(",","") + "]";

        var jsonOptions = new JsonSerializerOptions
        {
            Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
            WriteIndented = true
        };

        List<User> users = JsonSerializer.Deserialize<List<User>>(allJson, jsonOptions);

        if(users == null)
        {
            return new List<User>();
        }

        return users;
    }

    public void DeleteUser(Guid id)
    {
        throw new NotImplementedException();
    }

    public void DeleteAllUser()
    {
        throw new NotImplementedException();
    }

    public User GetUser(Guid id)
    {
        throw new NotImplementedException();
    }
}
