namespace WebSchool.Models;

public class WebUser
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public WebUser() { }
    
    public WebUser(string name)
    {
        Name = name;
    }
}