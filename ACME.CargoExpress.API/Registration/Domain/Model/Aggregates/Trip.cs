using ACME.CargoExpress.API.Registration.Domain.Model.Commands;
using ACME.CargoExpress.API.Registration.Domain.Model.Entities;
using ACME.CargoExpress.API.Registration.Domain.Model.ValueObjects;
using ACME.CargoExpress.API.User.Domain.Model.Aggregates;
using ACME.CargoExpress.API.User.Domain.Model.Entities;

namespace ACME.CargoExpress.API.Registration.Domain.Model.Aggregates;

public class Trip
{
    public Trip()
    {
        Name = new Name();
        CargoData = new CargoData();
        TripData = new TripData();
        Driver = new Driver();
        Vehicle = new Vehicle();
        Client = new Client();
        Entrepreneur = new Entrepreneur();
    }
    
    
    public Trip(string name, string type, int weight, string loadLocation, DateTime loadDate, string unloadLocation, DateTime unloadDate, 
        int driverId, int vehicleId, int clientId, int entrepreneurId, Driver driver, Vehicle vehicle, Client client, Entrepreneur entrepreneur)
    {
        Name = new Name(name);
        CargoData = new CargoData(type, weight);
        TripData = new TripData(loadLocation, loadDate, unloadLocation, unloadDate);
        DriverId = driverId;
        VehicleId = vehicleId;
        ClientId = clientId;
        EntrepreneurId = entrepreneurId;
        Driver = driver;
        Vehicle = vehicle;
        Client = client;
        Entrepreneur = entrepreneur;
    }
    public Trip(CreateTripCommand command, Driver driver, Vehicle vehicle, Client client, Entrepreneur entrepreneur)
    {
        Name = new Name(command.Name);
        CargoData = new CargoData(command.Type, command.Weight);
        TripData = new TripData(command.LoadLocation, command.LoadDate, command.UnloadLocation, command.UnloadDate);
        Driver = driver;
        Vehicle = vehicle;
        Client = client;
        Entrepreneur = entrepreneur;
    }
    
    public int Id { get; set; }
    public Name Name { get; internal set; }
    public CargoData CargoData { get; internal set; }
    public TripData TripData { get; internal set; }
    
    public Driver Driver { get; internal set; }
    public Vehicle Vehicle { get; internal set; }
    public int DriverId { get; internal set; }
    public int VehicleId { get; internal set; }
    public int ClientId { get; internal set; }
    public int EntrepreneurId { get; internal set; }
    public Client Client { get; internal set; }
    public Entrepreneur Entrepreneur { get; internal set; }
    
    public Expense Expense { get; internal set; }
    public Evidence Evidence { get; internal set; }
    public ICollection<Alert> Alerts { get; internal set; }
    public OngoingTrip OngoingTrip { get; internal set; }
    
}