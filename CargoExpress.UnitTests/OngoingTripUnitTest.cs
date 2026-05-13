using ACME.CargoExpress.API.Registration.Domain.Model.Aggregates;
using ACME.CargoExpress.API.Registration.Domain.Model.Entities;
using ACME.CargoExpress.API.Registration.Domain.Repositories;
using Moq;

namespace CargoExpress.UnitTests;

public class OngoingTripUnitTest
{
    [Fact]
    public async Task GetAll_OngoingTrips_Success()
    {
        var ongoingTrip = new OngoingTrip((float)-11.979, (float)-77.0587, 50, 200, 2, new Trip());
        var ongoingTripTwo = new OngoingTrip((float)-12.94579, (float)77.0587, 80, 150, 3, new Trip());
        var ongoingTrips = new List<OngoingTrip> { ongoingTrip, ongoingTripTwo };

        var mockOngoingTripRepository = new Mock<IOngoingTripRepository>();
        mockOngoingTripRepository.Setup(repo => repo.ListAsync()).ReturnsAsync(ongoingTrips);

        var returnedOngoingTrips = await mockOngoingTripRepository.Object.ListAsync();

        mockOngoingTripRepository.Verify(repo => repo.ListAsync(), Times.Once);
        Assert.Equal(ongoingTrips, returnedOngoingTrips);
        Assert.Equal(2, returnedOngoingTrips.Count());
    }

    [Fact]
    public async Task GetById_OngoingTrip_Success()
    {
        int validId = 1;
        int invalidId = 2;

        var ongoingTrip = new OngoingTrip((float)-11.979, (float)-77.0587, 50, 200, 2, new Trip());
        var mockOngoingTripRepository = new Mock<IOngoingTripRepository>();
        mockOngoingTripRepository.Setup(repo => repo.FindByIdAsync(validId)).ReturnsAsync(ongoingTrip);

        var returnedOngoingTrip = await mockOngoingTripRepository.Object.FindByIdAsync(validId);
        var returnedInvalidOngoingTrip = await mockOngoingTripRepository.Object.FindByIdAsync(invalidId);

        mockOngoingTripRepository.Verify(repo => repo.FindByIdAsync(validId), Times.Once);
        mockOngoingTripRepository.Verify(repo => repo.FindByIdAsync(invalidId), Times.Once);
        Assert.Equal(ongoingTrip, returnedOngoingTrip);
        Assert.Null(returnedInvalidOngoingTrip);
    }

    [Fact]
    public async Task Add_OngoingTrip_Success()
    {
        var ongoingTrip = new OngoingTrip((float)-11.979, (float)-77.0587, 50, 200, 2, new Trip());
        var mockOngoingTripRepository = new Mock<IOngoingTripRepository>();
        mockOngoingTripRepository.Setup(repo => repo.AddAsync(ongoingTrip)).Returns(Task.CompletedTask);

        await mockOngoingTripRepository.Object.AddAsync(ongoingTrip);

        mockOngoingTripRepository.Verify(repo => repo.AddAsync(ongoingTrip), Times.Once);
    }

    [Fact]
    public void Update_OngoingTrip_Success()
    {
        var ongoingTrip = new OngoingTrip((float)-11.979, (float)-77.0587, 50, 200, 2, new Trip());
        var mockOngoingTripRepository = new Mock<IOngoingTripRepository>();
        mockOngoingTripRepository.Setup(repo => repo.Update(ongoingTrip));

        mockOngoingTripRepository.Object.Update(ongoingTrip);

        mockOngoingTripRepository.Verify(repo => repo.Update(ongoingTrip), Times.Once);
        Assert.Equal((float)-11.979, ongoingTrip.Latitude);
        Assert.Equal((float)-77.0587, ongoingTrip.Longitude);
        Assert.Equal(50, ongoingTrip.Speed);
        Assert.Equal(200, ongoingTrip.Distance);
        Assert.Equal(2, ongoingTrip.TripId);
    }
}