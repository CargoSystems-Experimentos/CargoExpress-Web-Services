using ACME.CargoExpress.API.Registration.Domain.Model.Aggregates;
using ACME.CargoExpress.API.Registration.Domain.Model.Entities;
using ACME.CargoExpress.API.Registration.Domain.Repositories;
using Moq;

namespace CargoExpress.UnitTests;

public class EvidenceUnitTest
{
    [Fact]
    public async Task GetAll_Evidences_Success()
    {
        // Arrange
        var evidences = new List<Evidence> { new Evidence(), new Evidence() };
        var mockEvidenceRepository = new Mock<IEvidenceRepository>();
        mockEvidenceRepository.Setup(repo => repo.ListAsync()).ReturnsAsync(evidences);

        // Act
        var returnedEvidences = await mockEvidenceRepository.Object.ListAsync();

        // Assert
        mockEvidenceRepository.Verify(repo => repo.ListAsync(), Times.Once);
        Assert.Equal(evidences, returnedEvidences);
        Assert.Equal(evidences.Count, returnedEvidences.Count());
    }

    [Fact]
    public async Task GetById_Evidence_Success()
    {
        // Arrange
        int validId = 1;
        int invalidId = 0;
        var evidence = new Evidence("link.com/evidencia.png", 1, new Trip());
        var mockEvidenceRepository = new Mock<IEvidenceRepository>();
        mockEvidenceRepository.Setup(repo => repo.FindByIdAsync(validId)).ReturnsAsync(evidence);
        mockEvidenceRepository.Setup(repo => repo.FindByIdAsync(invalidId)).ReturnsAsync((Evidence)null);
        // Act
        var returnedEvidence = await mockEvidenceRepository.Object.FindByIdAsync(validId);
        var returnedNullEvidence = await mockEvidenceRepository.Object.FindByIdAsync(invalidId);

        // Assert
        mockEvidenceRepository.Verify(repo => repo.FindByIdAsync(validId), Times.Once);
        mockEvidenceRepository.Verify(repo => repo.FindByIdAsync(invalidId), Times.Once);
        Assert.Equal(evidence, returnedEvidence);
        Assert.Null(returnedNullEvidence);
        Assert.Equal(evidence.Link, returnedEvidence.Link);
        Assert.Equal(evidence.TripId, returnedEvidence.TripId);
    }

    [Fact]
    public async Task Add_Evidence_Success()
    {
        // Arrange
        var evidence = new Evidence("link.com/evidencia.png", 1, new Trip());
        var mockEvidenceRepository = new Mock<IEvidenceRepository>();
        mockEvidenceRepository.Setup(repo => repo.AddAsync(evidence)).Returns(Task.CompletedTask);

        // Act
        await mockEvidenceRepository.Object.AddAsync(evidence);

        // Assert
        mockEvidenceRepository.Verify(repo => repo.AddAsync(evidence), Times.Once);
    }

    [Fact]
    public void Update_Evidence_Success()
    {
        // Arrange
        var evidence = new Evidence();
        var mockEvidenceRepository = new Mock<IEvidenceRepository>();
        mockEvidenceRepository.Setup(repo => repo.Update(evidence));

        // Act
        mockEvidenceRepository.Object.Update(evidence);

        // Assert
        mockEvidenceRepository.Verify(repo => repo.Update(evidence), Times.Once);
    }
}