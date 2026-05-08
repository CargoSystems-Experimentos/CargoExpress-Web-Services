using ACME.CargoExpress.API.Registration.Domain.Model.Commands;
using ACME.CargoExpress.API.Registration.Domain.Model.Entities;
using ACME.CargoExpress.API.Registration.Domain.Repositories;
using ACME.CargoExpress.API.Registration.Domain.Services;
using ACME.CargoExpress.API.Shared.Domain.Repositories;

namespace ACME.CargoExpress.API.Registration.Application.Internal.CommandServices;

public class DriverCommandService(IDriverRepository driverRepository, IUnitOfWork unitOfWork)
    : IDriverCommandService
{
    public async Task<Driver?> Handle(CreateDriverCommand command)
    {
        var driver = new Driver(command.Name, command.Dni, command.License, command.ContactNumber);
        await driverRepository.AddAsync(driver);
        await unitOfWork.CompleteAsync();
        return driver;
    }
    
    public async Task<Driver?> Handle(UpdateDriverCommand command)
    {
        var driver = await driverRepository.FindByIdAsync(command.DriverId);
        if (driver == null)
        {
            return null;
        }
        //Update the driver information
        driver.Name = command.Name;
        driver.Dni = command.Dni;
        driver.License = command.License;
        driver.ContactNumber = command.ContactNumber;
        
        await unitOfWork.CompleteAsync();
        return driver;
    }
}