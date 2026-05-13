using ACME.CargoExpress.API.IAM.Domain.Model.Aggregates;
using ACME.CargoExpress.API.User.Domain.Model.Aggregates;
using ACME.CargoExpress.API.User.Domain.Repositories;
using Moq;

namespace CargoExpress.UnitTests;

public class ClientUnitTst
{
    [Fact]
    public async Task GetAll_Client_Success()
    {
        var userClient = new User("juan@gmail.com", "contra1234567");
        var userClientTwo = new User("juan2@gmail.com", "contra1234567");

        var clients = new List<Client>
        {
            new Client("Juan Perez", "986559113", "12345678", 1, userClient),
            new Client("Juan Sanchez", "986559114", "87654321", 2, userClientTwo)
        };

        var mockClientRepository = new Mock<IClientRepository>();
        mockClientRepository.Setup(repo => repo.ListAsync()).ReturnsAsync(clients);

        var returnedClients = await mockClientRepository.Object.ListAsync();

        mockClientRepository.Verify(repo => repo.ListAsync(), Times.Once);
        Assert.Equal(clients, returnedClients);
        Assert.Equal(2, returnedClients.Count());
    }

    [Fact]
    public async Task GetById_Client_Success()
    {
        int validId = 1;
        int invalidId = 0;

        var userClient = new User("juan@gmail.com", "contra1234567");
        var client = new Client("Juan Perez", "986559113", "12345678", validId, userClient);

        var mockClientRepository = new Mock<IClientRepository>();
        mockClientRepository.Setup(repo => repo.FindByIdAsync(validId)).ReturnsAsync(client);
        mockClientRepository.Setup(repo => repo.FindByIdAsync(invalidId)).ReturnsAsync((Client)null);

        var returnedClient = await mockClientRepository.Object.FindByIdAsync(validId);
        var returnedNullClient = await mockClientRepository.Object.FindByIdAsync(invalidId);

        mockClientRepository.Verify(repo => repo.FindByIdAsync(validId), Times.Once);
        mockClientRepository.Verify(repo => repo.FindByIdAsync(invalidId), Times.Once);
        Assert.Equal(client, returnedClient);
        Assert.Null(returnedNullClient);
    }

    [Fact]
    public async Task Add_Client_Success()
    {
        var userClient = new User("juan@gmail.com", "contra1234567");
        var client = new Client("Juan Perez", "986559113", "12345678", 1, userClient);

        var mockClientRepository = new Mock<IClientRepository>();
        mockClientRepository.Setup(repo => repo.AddAsync(client)).Returns(Task.CompletedTask);

        await mockClientRepository.Object.AddAsync(client);

        mockClientRepository.Verify(repo => repo.AddAsync(client), Times.Once);
    }
}