using ACME.CargoExpress.API.Registration.Domain.Model.Aggregates;

namespace ACME.CargoExpress.API.Registration.Domain.Model.Entities;

public class Vehicle
{
    public Vehicle()
    {
        Model = string.Empty;
        Plate = string.Empty;
        TractorPlate = string.Empty;
        MaxLoad = 0;
        Volume = 0;
        Trips = new List<Trip>();
    }

    public Vehicle(string model, string plate, string tractorPlate, float maxLoad, float volume)
    {
        Model = model;
        Plate = plate;
        TractorPlate = tractorPlate;
        MaxLoad = maxLoad;
        Volume = volume;
        Trips = new List<Trip>();
    }
    
    public int Id { get; set; }
    public string Model { get; set; }
    public string Plate { get; set; }
    public string TractorPlate { get; set; }
    public float MaxLoad { get; set; }
    public float Volume { get; set; }
    
    public ICollection<Trip> Trips { get; }
}