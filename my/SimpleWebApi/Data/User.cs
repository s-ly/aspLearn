namespace SimpleWebApi.Models;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public int? Age { get; set; }

    public User() { }

    public User(string name, string email, int? age = null)
    {
        Name = name;
        Email = email;
        Age = age;
    }
}