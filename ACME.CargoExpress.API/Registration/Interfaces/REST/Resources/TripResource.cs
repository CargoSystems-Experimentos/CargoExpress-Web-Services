using ACME.CargoExpress.API.Registration.Domain.Model.ValueObjects;

namespace ACME.CargoExpress.API.Registration.Interfaces.REST.Resources;

public record TripResource(int Id, Name Name, CargoData CargoData, TripData TripData, int DriverId, int VehicleId, int ClientId, int EntrepreneurId);