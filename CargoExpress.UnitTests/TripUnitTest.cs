using ACME.CargoExpress.API.IAM.Domain.Model.Aggregates;
using ACME.CargoExpress.API.Registration.Domain.Model.Aggregates;
using ACME.CargoExpress.API.Registration.Domain.Model.Entities;
using ACME.CargoExpress.API.Registration.Domain.Repositories;
using ACME.CargoExpress.API.User.Domain.Model.Aggregates;
using Moq;

namespace CargoExpress.UnitTests;

public class TripUnitTest
{
    [Fact]
    public async Task GetAll_Trip_Success()
    {
        var userClient = new User("juan@gmail.com", "contra123");
        var userEntrepreneur = new User("lucho@gmail.com", "contra123");

        var entrepreneur = new Entrepreneur("Lucho Vega", "986559213", "20000000002", "logo.com/image.jpeg", 1, userEntrepreneur);
        var client = new Client("Juan Perez", "986559113", "20000000001", 1, userClient);
        var driver = new Driver("Juan Perez", "12345678", "Brevete A1", "955123456", 1, entrepreneur);
        var vehicle = new Vehicle("Volkswagen", "A1B-234", "A1B-235", 5000f, 35f, 1, entrepreneur);

        var trips = new List<Trip>
        {
            new Trip("Viaje 1", "Tecnologia", 500,
                "Av. San Borja Sur", new DateTime(2024, 07, 05),
                "Av. San Borja Norte", new DateTime(2024, 07, 06),
                1, 1, 1, 1, "https://example.com/evidence1.jpg", driver, vehicle, client, entrepreneur),

            new Trip("Viaje 2", "Alimentos", 1000,
                "Calle Las Begonias 730", new DateTime(2024, 07, 08),
                "Av. Vicus I-92", new DateTime(2024, 08, 09),
                1, 1, 1, 1, "https://example.com/evidence1.jpg", driver, vehicle, client, entrepreneur),
        };

        var mockTripRepository = new Mock<ITripRepository>();
        mockTripRepository.Setup(repo => repo.ListAsync()).ReturnsAsync(trips);

        var returnedTrips = await mockTripRepository.Object.ListAsync();

        mockTripRepository.Verify(repo => repo.ListAsync(), Times.Once);
        Assert.Equal(trips, returnedTrips);
        Assert.Equal(2, returnedTrips.Count());
    }

    [Fact]
    public async Task GetById_Trip_Success()
    {
        int validId = 1;
        int invalidId = 0;

        var userClient = new User("juan@gmail.com", "contra123");
        var userEntrepreneur = new User("lucho@gmail.com", "contra123");

        var entrepreneur = new Entrepreneur("Lucho Vega", "986559213", "20000000002", "logo.com/image.jpeg", 1, userEntrepreneur);
        var client = new Client("Juan Perez", "986559113", "20000000001", 1, userClient);
        var driver = new Driver("Juan Perez", "12345678", "Brevete A1", "955123456", 1, entrepreneur);
        var vehicle = new Vehicle("Volkswagen", "A1B-234", "A1B-235", 5000f, 35f, 1, entrepreneur);

        var trip = new Trip("Viaje 1", "Tecnologia", 500,
            "Av. San Borja Sur", new DateTime(2024, 07, 05),
            "Av. San Borja Norte", new DateTime(2024, 07, 06),
            1, 1, 1, 1, "https://example.com/evidence.jpg", driver, vehicle, client, entrepreneur);

        var mockTripRepository = new Mock<ITripRepository>();
        mockTripRepository.Setup(repo => repo.FindByIdAsync(validId)).ReturnsAsync(trip);
        mockTripRepository.Setup(repo => repo.FindByIdAsync(invalidId)).ReturnsAsync((Trip)null);

        var returnedTrip = await mockTripRepository.Object.FindByIdAsync(validId);
        var returnedNullTrip = await mockTripRepository.Object.FindByIdAsync(invalidId);

        mockTripRepository.Verify(repo => repo.FindByIdAsync(validId), Times.Once);
        mockTripRepository.Verify(repo => repo.FindByIdAsync(invalidId), Times.Once);
        Assert.Equal(trip, returnedTrip);
        Assert.Null(returnedNullTrip);
    }

    [Fact]
    public async Task Add_Trip_Success()
    {
        var userClient = new User("juan@gmail.com", "contra123");
        var userEntrepreneur = new User("lucho@gmail.com", "contra123");

        var entrepreneur = new Entrepreneur("Lucho Vega", "986559213", "20000000002", "logo.com/image.jpeg", 1, userEntrepreneur);
        var client = new Client("Juan Perez", "986559113", "20000000001", 1, userClient);
        var driver = new Driver("Juan Perez", "12345678", "Brevete A1", "955123456", 1, entrepreneur);
        var vehicle = new Vehicle("Volkswagen", "A1B-234", "A1B-235", 5000f, 35f, 1, entrepreneur);

        var trip = new Trip("Viaje 1", "Tecnologia", 500,
            "Av. San Borja Sur", new DateTime(2024, 07, 05),
            "Av. San Borja Norte", new DateTime(2024, 07, 06),
            1, 1, 1, 1, "https://example.com/evidence.jpg",  driver, vehicle, client, entrepreneur);

        var mockTripRepository = new Mock<ITripRepository>();
        mockTripRepository.Setup(repo => repo.AddAsync(trip)).Returns(Task.CompletedTask);

        await mockTripRepository.Object.AddAsync(trip);

        mockTripRepository.Verify(repo => repo.AddAsync(trip), Times.Once);
    }

    [Fact]
    public void Update_Trip_Success()
    {
        var userClient = new User("juan@gmail.com", "contra123");
        var userEntrepreneur = new User("lucho@gmail.com", "contra123");

        var entrepreneur = new Entrepreneur("Lucho Vega", "986559213", "20000000002", "logo.com/image.jpeg", 1, userEntrepreneur);
        var client = new Client("Juan Perez", "986559113", "20000000001", 1, userClient);
        var driver = new Driver("Juan Perez", "12345678", "Brevete A1", "955123456", 1, entrepreneur);
        var vehicle = new Vehicle("Volkswagen", "A1B-234", "A1B-235", 5000f, 35f, 1, entrepreneur);

        var trip = new Trip("Viaje 1", "Tecnologia", 500,
            "Av. San Borja Sur", new DateTime(2024, 07, 05),
            "Av. San Borja Norte", new DateTime(2024, 07, 06),
            1, 1, 1, 1, "https://example.com/evidence.jpg", driver, vehicle, client, entrepreneur);

        var mockTripRepository = new Mock<ITripRepository>();
        mockTripRepository.Setup(repo => repo.Update(trip));

        mockTripRepository.Object.Update(trip);

        mockTripRepository.Verify(repo => repo.Update(trip), Times.Once);
    }
}