using ACME.CargoExpress.API.User.Domain.Repositories;
using ACME.CargoExpress.API.Registration.Domain.Model.Aggregates;
using ACME.CargoExpress.API.Registration.Domain.Model.Commands;
using ACME.CargoExpress.API.Registration.Domain.Model.Entities;
using ACME.CargoExpress.API.Registration.Domain.Repositories;
using ACME.CargoExpress.API.Registration.Domain.Services;
using ACME.CargoExpress.API.Shared.Domain.Repositories;

namespace ACME.CargoExpress.API.Registration.Application.Internal.CommandServices;

public class TripCommandService(
    ITripRepository tripRepository,
    IDriverRepository driverRepository,
    IVehicleRepository vehicleRepository,
    IClientRepository clientRepository,
    IEntrepreneurRepository entrepreneurRepository,
    IUnitOfWork unitOfWork)
    : ITripCommandService
{
    public async Task<Trip?> Handle(CreateTripCommand command)
    {
        var client = await clientRepository.FindByIdAsync(command.ClientId);
        if (client == null)
            throw new ArgumentException("ClientId not found.");

        var entrepreneur = await entrepreneurRepository.FindByIdAsync(command.EntrepreneurId);
        if (entrepreneur == null)
            throw new ArgumentException("EntrepreneurId not found.");

        var driver = await driverRepository.FindByIdAsync(command.DriverId);
        if (driver == null)
            throw new ArgumentException("DriverId not found.");
        if (driver.EntrepreneurId != entrepreneur.Id)
            throw new ArgumentException("Driver does not belong to the given EntrepreneurId.");

        var vehicle = await vehicleRepository.FindByIdAsync(command.VehicleId);
        if (vehicle == null)
            throw new ArgumentException("VehicleId not found.");
        if (vehicle.EntrepreneurId != entrepreneur.Id)
            throw new ArgumentException("Vehicle does not belong to the given EntrepreneurId.");

        var trip = new Trip(command, driver, vehicle, client, entrepreneur);
        await tripRepository.AddAsync(trip);
        await unitOfWork.CompleteAsync();
        return trip;
    }

    public async Task<Trip?> Handle(UpdateTripCommand command)
    {
        var trip = await tripRepository.FindByIdAsync(command.TripId);
        if (trip == null)
            return null;

        var client = await clientRepository.FindByIdAsync(command.ClientId);
        if (client == null)
            throw new ArgumentException("ClientId not found.");

        var entrepreneur = await entrepreneurRepository.FindByIdAsync(command.EntrepreneurId);
        if (entrepreneur == null)
            throw new ArgumentException("EntrepreneurId not found.");

        var driver = await driverRepository.FindByIdAsync(command.DriverId);
        if (driver == null)
            throw new ArgumentException("DriverId not found.");
        if (driver.EntrepreneurId != entrepreneur.Id)
            throw new ArgumentException("Driver does not belong to the given EntrepreneurId.");

        var vehicle = await vehicleRepository.FindByIdAsync(command.VehicleId);
        if (vehicle == null)
            throw new ArgumentException("VehicleId not found.");
        if (vehicle.EntrepreneurId != entrepreneur.Id)
            throw new ArgumentException("Vehicle does not belong to the given EntrepreneurId.");

        trip.Name = new ACME.CargoExpress.API.Registration.Domain.Model.ValueObjects.Name(command.Name);
        trip.CargoData = new ACME.CargoExpress.API.Registration.Domain.Model.ValueObjects.CargoData(command.Type, command.Weight);
        trip.TripData = new ACME.CargoExpress.API.Registration.Domain.Model.ValueObjects.TripData(
            command.LoadLocation,
            command.LoadDate,
            command.UnloadLocation,
            command.UnloadDate);

        trip.DriverId = driver.Id;
        trip.VehicleId = vehicle.Id;
        trip.ClientId = command.ClientId;
        trip.EntrepreneurId = command.EntrepreneurId;

        trip.Driver = driver;
        trip.Vehicle = vehicle;
        trip.Client = client;
        trip.Entrepreneur = entrepreneur;

        await unitOfWork.CompleteAsync();
        return trip;
    }
}