using ACME.CargoExpress.API.Registration.Domain.Model.Aggregates;

namespace ACME.CargoExpress.API.Registration.Domain.Model.Entities;

public class Driver
{
    public Driver()
    {
        Name = string.Empty;
        Dni = string.Empty;
        License = string.Empty;
        ContactNumber = string.Empty;
        Trips = new List<Trip>();
    }

    public Driver(string name, string dni, string license, string contactNumber)
    {
        Name = name;
        Dni = dni;
        License = license;
        ContactNumber = contactNumber;
        Trips = new List<Trip>();
    }
    
    public int Id { get; set; }
    public string Name { get; set; }
    public string Dni { get; set; }
    public string License { get; set; }
    public string ContactNumber { get; set; }
    
    public ICollection<Trip> Trips { get; }
}