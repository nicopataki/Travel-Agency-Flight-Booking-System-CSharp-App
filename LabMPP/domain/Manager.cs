namespace LabMPP.domain;

public class Manager: Entity<long>
{
    public string Name { get; set; }
    public string Password { get; set; }

    public Manager(string name, string password)
    {
        Name = name;
        this.Password = password;
    }

    public override string ToString()
    {
        return "Manager: " + Name + '\'' + Password + '\'';
    }
}