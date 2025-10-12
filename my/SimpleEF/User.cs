public class User
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    
    // Конструктор для удобства
    public User() { }
    
    public User(string name, string email)
    {
        Name = name;
        Email = email;
    }
}