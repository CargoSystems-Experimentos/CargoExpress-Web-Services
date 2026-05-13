using ACME.CargoExpress.API.IAM.Domain.Model.Aggregates;
using ACME.CargoExpress.API.IAM.Domain.Repositories;
using Moq;

namespace CargoExpress.UnitTests;

public class UserUnitTest
{
    [Fact]
    public async Task GetAll_User_Success()
    {
        var users = new List<User>
        {
            new User("juan@gmail.com", "contra1234567"),
            new User("pedro@gmail.com", "contra1234567")
        };

        var mockUserRepository = new Mock<IUserRepository>();
        mockUserRepository.Setup(repo => repo.ListAsync().Result).Returns(users);

        var returnedUsers = await mockUserRepository.Object.ListAsync();

        mockUserRepository.Verify(repo => repo.ListAsync(), Times.Once);
        Assert.Equal(users, returnedUsers);
        Assert.Equal(2, returnedUsers.Count());
    }

    [Fact]
    public async Task GetById_User_Success()
    {
        int validId = 1;
        int invalidId = 0;

        var user = new User("juan@gmail.com", "contra1234567");
        var mockUserRepository = new Mock<IUserRepository>();
        mockUserRepository.Setup(repo => repo.FindByIdAsync(validId).Result).Returns(user);
        mockUserRepository.Setup(repo => repo.FindByIdAsync(invalidId).Result).Returns((User)null);

        var returnedUser = await mockUserRepository.Object.FindByIdAsync(validId);
        var returnedNullUser = await mockUserRepository.Object.FindByIdAsync(invalidId);

        mockUserRepository.Verify(repo => repo.FindByIdAsync(validId), Times.Once);
        mockUserRepository.Verify(repo => repo.FindByIdAsync(invalidId), Times.Once);

        Assert.Equal(user, returnedUser);
        Assert.Null(returnedNullUser);
    }

    [Fact]
    public async Task Add_User_Success()
    {
        var user = new User("juan@gmail.com", "contra1234567");
        var mockUserRepository = new Mock<IUserRepository>();
        mockUserRepository.Setup(repo => repo.AddAsync(user)).Returns(Task.CompletedTask);

        await mockUserRepository.Object.AddAsync(user);

        mockUserRepository.Verify(repo => repo.AddAsync(user), Times.Once);
    }
}