using System.ComponentModel.DataAnnotations;

namespace Qui.Core.Models;

public class User
{
    [Key]
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Sername { get; set; } = string.Empty;
    public string FatherName { get; set; } = string.Empty;
    public Sex Sex { get; set; }
    public int Age { get; set; }
    public UserProgress Progress { get; set; } = new();

    public DateTime GetStartTime() => Progress.AnswerStartDateTime.Count > 0 ? Progress.AnswerStartDateTime[0] : new DateTime(0, 0, 0);
    public string GetFullName() => $"{Sername} {Name} {FatherName}";
    public override string ToString() =>
        $"User[Id:{Id};Name:{Name};Sername:{Sername};FatherName:{FatherName};Sex:{Sex};Age:{Age}]";
}
