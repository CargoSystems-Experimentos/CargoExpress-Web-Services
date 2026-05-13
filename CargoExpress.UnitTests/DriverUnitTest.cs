using Moq;
using ACME.CargoExpress.API.Registration.Domain.Model.Entities;
using ACME.CargoExpress.API.Registration.Domain.Repositories;
using ACME.CargoExpress.API.User.Domain.Model.Aggregates;

namespace CargoExpress.UnitTests;

public class DriverUnitTest
{
    [Fact]
    public async Task GetAll_Driver_Success()
    {
        var entrepreneur = new Entrepreneur("Empresa 1", "987654321", "20123456789", "logo.png", 1, new ACME.CargoExpress.API.IAM.Domain.Model.Aggregates.User("empresa1@mail.com", "contra1234567"));
        var drivers = new List<Driver>
        {
            new Driver("Juan Perez", "12345678", "Brevete A1", "955123456", 1, entrepreneur),
            new Driver("Pedro Sanchez", "87654321", "Brevete A2", "955765432", 1, entrepreneur)
        };

        var mockDriverRepository = new Mock<IDriverRepository>();
        mockDriverRepository.Setup(repo => repo.ListAsync()).ReturnsAsync(drivers);

        var returnedDrivers = await mockDriverRepository.Object.ListAsync();

        mockDriverRepository.Verify(repo => repo.ListAsync(), Times.Once);
        Assert.Equal(drivers, returnedDrivers);
        Assert.Equal(2, returnedDrivers.Count());
    }

    [Fact]
    public async Task GetById_Driver_Success()
    {
        int validId = 1;
        int invalidId = 0;

        var entrepreneur = new Entrepreneur("Empresa 1", "987654321", "20123456789", "logo.png", 1, new ACME.CargoExpress.API.IAM.Domain.Model.Aggregates.User("empresa1@mail.com", "contra1234567"));
        var driver = new Driver("Juan Perez", "12345678", "Brevete A1", "955123456", validId, entrepreneur);

        var mockDriverRepository = new Mock<IDriverRepository>();
        mockDriverRepository.Setup(repo => repo.FindByIdAsync(validId)).ReturnsAsync(driver);
        mockDriverRepository.Setup(repo => repo.FindByIdAsync(invalidId)).ReturnsAsync((Driver)null);

        var returnedDriver = await mockDriverRepository.Object.FindByIdAsync(validId);
        var returnedNullDriver = await mockDriverRepository.Object.FindByIdAsync(invalidId);

        mockDriverRepository.Verify(repo => repo.FindByIdAsync(validId), Times.Once);
        mockDriverRepository.Verify(repo => repo.FindByIdAsync(invalidId), Times.Once);
        Assert.Equal(driver, returnedDriver);
        Assert.Null(returnedNullDriver);
    }

    [Fact]
    public async Task Add_Driver_Success()
    {
        var entrepreneur = new Entrepreneur("Empresa 1", "987654321", "20123456789", "logo.png", 1, new ACME.CargoExpress.API.IAM.Domain.Model.Aggregates.User("empresa1@mail.com", "contra1234567"));
        var driver = new Driver("Juan Perez", "12345678", "Brevete A1", "955123456", 1, entrepreneur);

        var mockDriverRepository = new Mock<IDriverRepository>();
        mockDriverRepository.Setup(repo => repo.AddAsync(driver)).Returns(Task.CompletedTask);

        await mockDriverRepository.Object.AddAsync(driver);

        mockDriverRepository.Verify(repo => repo.AddAsync(driver), Times.Once);
    }

    [Fact]
    public void Update_Driver_Success()
    {
        var entrepreneur = new Entrepreneur("Empresa 1", "987654321", "20123456789", "logo.png", 1, new ACME.CargoExpress.API.IAM.Domain.Model.Aggregates.User("empresa1@mail.com", "contra1234567"));
        var driver = new Driver("Juan Perez", "12345678", "Brevete A1", "955123456", 1, entrepreneur);
        var mockDriverRepository = new Mock<IDriverRepository>();
        mockDriverRepository.Setup(repo => repo.Update(driver));

        driver.Name = "Pedro Sanchez";
        driver.Dni = "87654321";
        driver.License = "Brevete A2";
        driver.ContactNumber = "955765432";

        mockDriverRepository.Object.Update(driver);

        mockDriverRepository.Verify(repo => repo.Update(driver), Times.Once);
        Assert.Equal("Pedro Sanchez", driver.Name);
        Assert.Equal("87654321", driver.Dni);
        Assert.Equal("Brevete A2", driver.License);
        Assert.Equal("955765432", driver.ContactNumber);
    }
}