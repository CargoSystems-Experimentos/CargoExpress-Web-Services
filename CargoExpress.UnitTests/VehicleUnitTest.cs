using ACME.CargoExpress.API.Registration.Domain.Model.Entities;
using ACME.CargoExpress.API.Registration.Domain.Repositories;
using ACME.CargoExpress.API.User.Domain.Model.Aggregates;
using Moq;

namespace CargoExpress.UnitTests;

public class VehicleUnitTest
{
    [Fact]
    public async Task GetAll_Vehicle_Success()
    {
        var entrepreneur = new Entrepreneur("Empresa 1", "987654321", "20123456789", "logo.png", 1, new ACME.CargoExpress.API.IAM.Domain.Model.Aggregates.User("empresa1@mail.com", "contra1234567"));
        var vehicles = new List<Vehicle>
        {
            new Vehicle("Volkswagen", "ABC-123", "ABC-123", 5000f, 35f, 1, entrepreneur),
            new Vehicle("Toyota", "BCD-234", "BCD-234", 6000f, 40f, 2, entrepreneur)
        };

        var vehicleDataMock = new Mock<IVehicleRepository>();
        vehicleDataMock.Setup(x => x.ListAsync()).ReturnsAsync(vehicles);

        var returnedVehicles = await vehicleDataMock.Object.ListAsync();

        vehicleDataMock.Verify(x => x.ListAsync(), Times.Once);
        Assert.Equal(vehicles, returnedVehicles);
        Assert.Equal(2, returnedVehicles.Count());
    }

    [Fact]
    public async Task GetById_Vehicle_Success()
    {
        int validId = 1;
        int invalidId = 0;

        var entrepreneur = new Entrepreneur("Empresa 1", "987654321", "20123456789", "logo.png", 1, new ACME.CargoExpress.API.IAM.Domain.Model.Aggregates.User("empresa1@mail.com", "contra1234567"));
        var vehicle = new Vehicle("Volkswagen", "ABC-123", "ABC-123", 5000f, 35f, validId, entrepreneur);

        var vehicleDataMock = new Mock<IVehicleRepository>();
        vehicleDataMock.Setup(x => x.FindByIdAsync(validId)).ReturnsAsync(vehicle);
        vehicleDataMock.Setup(x => x.FindByIdAsync(invalidId)).ReturnsAsync((Vehicle)null);

        var returnedVehicle = await vehicleDataMock.Object.FindByIdAsync(validId);
        var returnedNullVehicle = await vehicleDataMock.Object.FindByIdAsync(invalidId);

        vehicleDataMock.Verify(x => x.FindByIdAsync(validId), Times.Once);
        vehicleDataMock.Verify(x => x.FindByIdAsync(invalidId), Times.Once);
        Assert.Equal(vehicle, returnedVehicle);
        Assert.Null(returnedNullVehicle);
    }

    [Fact]
    public async Task Add_Vehicle_Success()
    {
        var entrepreneur = new Entrepreneur("Empresa 1", "987654321", "20123456789", "logo.png", 1, new ACME.CargoExpress.API.IAM.Domain.Model.Aggregates.User("empresa1@mail.com", "contra1234567"));
        var vehicle = new Vehicle("Volkswagen", "ABC-123", "ABC-123", 5000f, 35f, 1, entrepreneur);

        var vehicleDataMock = new Mock<IVehicleRepository>();
        vehicleDataMock.Setup(x => x.AddAsync(vehicle)).Returns(Task.CompletedTask);

        await vehicleDataMock.Object.AddAsync(vehicle);

        vehicleDataMock.Verify(x => x.AddAsync(vehicle), Times.Once);
    }

    [Fact]
    public void Update_Vehicle_Success()
    {
        var entrepreneur = new Entrepreneur("Empresa 1", "987654321", "20123456789", "logo.png", 1, new ACME.CargoExpress.API.IAM.Domain.Model.Aggregates.User("empresa1@mail.com", "contra1234567"));
        var vehicle = new Vehicle("Volkswagen", "ABC-123", "ABC-123", 5000f, 35f, 1, entrepreneur);
        var vehicleDataMock = new Mock<IVehicleRepository>();
        vehicleDataMock.Setup(x => x.Update(vehicle));

        vehicle.Model = "Toyota";
        vehicle.TractorPlate = "BCD-234";
        vehicle.MaxLoad = 6000f;
        vehicle.Volume = 40f;

        vehicleDataMock.Object.Update(vehicle);

        vehicleDataMock.Verify(x => x.Update(vehicle), Times.Once);
        Assert.Equal("Toyota", vehicle.Model);
        Assert.Equal("BCD-234", vehicle.TractorPlate);
        Assert.Equal(6000f, vehicle.MaxLoad);
        Assert.Equal(40f, vehicle.Volume);
    }
}