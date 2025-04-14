namespace FlightNetworking;

public class ManagerDTO
{
    public string Name { get; set; }
    public string Password { get; set; }

    // Constructor
    public ManagerDTO(string name, string password)
    {
        Name = name;
        Password = password;
    }

    // Override pentru ToString
    public override string ToString()
    {
        return $"Manager{{name='{Name}', password='{Password}'}}";
    }
}