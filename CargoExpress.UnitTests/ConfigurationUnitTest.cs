using ACME.CargoExpress.API.IAM.Domain.Model.Aggregates;
using ACME.CargoExpress.API.User.Domain.Model.Aggregates;
using Moq;

using ACME.CargoExpress.API.User.Domain.Model.Entities;
using ACME.CargoExpress.API.User.Domain.Repositories;

namespace CargoExpress.UnitTests;

public class ConfigurationUnitTest
{
    [Fact]
    public async Task GetAll_Configurations_Success()
    {
        // Arrange
        var configurations = new List<Configuration> { new Configuration(), new Configuration() };
        var mockConfigurationRepository = new Mock<IConfigurationRepository>();
        mockConfigurationRepository.Setup(repo => repo.ListAsync()).ReturnsAsync(configurations);

        // Act
        var returnedConfigurations = await mockConfigurationRepository.Object.ListAsync();

        // Assert
        mockConfigurationRepository.Verify(repo => repo.ListAsync(), Times.Once);
        Assert.Equal(configurations, returnedConfigurations);
        Assert.Equal(configurations.Count, returnedConfigurations.Count());
    }

    [Fact]
    public async Task GetById_Configuration_Success()
    {
        // Arrange
        int validId = 1;
        var configuration = new Configuration(1, "Light", "Grid", true, true, new User());
        var mockConfigurationRepository = new Mock<IConfigurationRepository>();
        mockConfigurationRepository.Setup(repo => repo.FindByIdAsync(validId)).ReturnsAsync(configuration);

        // Act
        var returnedConfiguration = await mockConfigurationRepository.Object.FindByIdAsync(validId);

        // Assert
        mockConfigurationRepository.Verify(repo => repo.FindByIdAsync(validId), Times.Once);
        Assert.Equal(configuration, returnedConfiguration);
        Assert.Equal(configuration.UserId, returnedConfiguration.UserId);
        Assert.Equal(configuration.Theme, returnedConfiguration.Theme);
        Assert.Equal(configuration.View, returnedConfiguration.View);
        Assert.Equal(configuration.AllowDataCollection, returnedConfiguration.AllowDataCollection);
    }

    [Fact]
    public async Task Add_Configuration_Success()
    {
        // Arrange
        var configuration = new Configuration();
        var mockConfigurationRepository = new Mock<IConfigurationRepository>();
        mockConfigurationRepository.Setup(repo => repo.AddAsync(configuration)).Returns(Task.CompletedTask);

        // Act
        await mockConfigurationRepository.Object.AddAsync(configuration);

        // Assert
        mockConfigurationRepository.Verify(repo => repo.AddAsync(configuration), Times.Once);
    }

    [Fact]
    public void Update_Configuration_Success()
    {
        // Arrange
        var configuration = new Configuration();
        var mockConfigurationRepository = new Mock<IConfigurationRepository>();
        mockConfigurationRepository.Setup(repo => repo.Update(configuration));

        // Act
        mockConfigurationRepository.Object.Update(configuration);

        // Assert
        mockConfigurationRepository.Verify(repo => repo.Update(configuration), Times.Once);
    }
}