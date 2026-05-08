using ACME.CargoExpress.API.Registration.Domain.Model.Aggregates;
using ACME.CargoExpress.API.Registration.Domain.Model.Commands;
using ACME.CargoExpress.API.Registration.Domain.Model.ValueObjects;
using ACME.CargoExpress.API.Registration.Domain.Repositories;
using ACME.CargoExpress.API.Registration.Domain.Services;
using ACME.CargoExpress.API.Shared.Domain.Repositories;
using ACME.CargoExpress.API.User.Domain.Repositories;

namespace ACME.CargoExpress.API.Registration.Application.Internal.CommandServices;

public class TripCommandService(ITripRepository tripRepository, 
    IDriverRepository driverRepository, IVehicleRepository vehicleRepository, IClientRepository clientRepository, IEntrepreneurRepository entrepreneurRepository,
    IUnitOfWork unitOfWork)
    : ITripCommandService
{
    public async Task<Trip?> Handle(CreateTripCommand command)
    {
        //Additional validation to check if the driver and vehicle exist
        var driver = await driverRepository.FindByIdAsync(command.DriverId);
        if (driver == null)
        {
            throw new ArgumentException("DriverId not found.");
        }
        
        var vehicle = await vehicleRepository.FindByIdAsync(command.VehicleId);
        if (vehicle == null)
        {
            throw new ArgumentException("VehicleId not found.");
        }
        
        var client = await clientRepository.FindByIdAsync(command.ClientId);
        if (client == null)
        {
            throw new ArgumentException("ClientId not found.");
        }
        
        var entrepreneur = await entrepreneurRepository.FindByIdAsync(command.EntrepreneurId);
        if (entrepreneur == null)
        {
            throw new ArgumentException("EntrepreneurId not found.");
        }
        
        //Create the trip
        var trip = new Trip(command, driver, vehicle, client, entrepreneur);
        try
        { 
            await tripRepository.AddAsync(trip);
            await unitOfWork.CompleteAsync();
            return trip;
        }
        catch (Exception e)
        {
            Console.WriteLine($"An error occurred while creating the trip: {e.Message}");
            return null;
        }
        
    }
    
public async Task<Trip?> Handle(UpdateTripCommand command)
    {
        var driver = await driverRepository.FindByIdAsync(command.DriverId);
        if (driver == null)
        {
            throw new ArgumentException("DriverId not found.");
        }
        
        var vehicle = await vehicleRepository.FindByIdAsync(command.VehicleId);
        if (vehicle == null)
        {
            throw new ArgumentException("VehicleId not found.");
        }
        
        var client = await clientRepository.FindByIdAsync(command.ClientId);
        if (client == null)
        {
            throw new ArgumentException("ClientId not found.");
        }
        
        var entrepreneur = await entrepreneurRepository.FindByIdAsync(command.EntrepreneurId);
        if (entrepreneur == null)
        {
            throw new ArgumentException("EntrepreneurId not found.");
        }
        
        var trip = await tripRepository.FindByIdAsync(command.TripId);
        if (trip == null)
        {
            return null;
        }
        //Update the trip information
        trip.Name = new Name(command.Name);
        trip.CargoData = new CargoData(command.Type, command.Weight);
        trip.TripData = new TripData(command.LoadLocation, command.LoadDate, command.UnloadLocation, command.UnloadDate);
        trip.DriverId = command.DriverId;
        trip.VehicleId = command.VehicleId;
        trip.ClientId = command.ClientId;
        trip.EntrepreneurId = command.EntrepreneurId;
        trip.Vehicle = vehicle;
        trip.Driver = driver;
        trip.Client = client;
        trip.Entrepreneur = entrepreneur;
        
        await unitOfWork.CompleteAsync();
        return trip;
    }
}