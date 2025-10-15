public class User
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public int? Age { get; set; }

    // Конструктор для удобства
    public User() { }

    public User(string name, string email)
    {
        Name = name;
        Email = email;
    }

    public User(string name, string email, int? age)
    {
        Name = name;
        Email = email;
        Age = age;
    }
}