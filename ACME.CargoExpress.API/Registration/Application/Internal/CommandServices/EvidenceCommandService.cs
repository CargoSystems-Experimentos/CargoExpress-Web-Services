using ACME.CargoExpress.API.Registration.Domain.Model.Commands;
using ACME.CargoExpress.API.Registration.Domain.Model.Entities;
using ACME.CargoExpress.API.Registration.Domain.Repositories;
using ACME.CargoExpress.API.Registration.Domain.Services;
using ACME.CargoExpress.API.Shared.Domain.Repositories;

namespace ACME.CargoExpress.API.Registration.Application.Internal.CommandServices;

public class EvidenceCommandService(IEvidenceRepository evidenceRepository ,ITripRepository tripRepository, IUnitOfWork unitOfWork)
    : IEvidenceCommandService
{
    public async Task<Evidence?> Handle(CreateEvidenceCommand command)
    {
        // Additional validation to check if the trip exists
        var trip = await tripRepository.FindByIdAsync(command.TripId);
        if (trip == null)
        {
            throw new ArgumentException("TripId not found.");
        }
        // Check if an Expense with the same TripId already exists
        var existingExpense = await evidenceRepository.FindByTripIdAsync(command.TripId);
        if (existingExpense != null)
        {
            throw new InvalidOperationException("An Evidence with the same TripId already exists.");
        }
        
        var evidence = new Evidence(command, trip);
        await evidenceRepository.AddAsync(evidence);
        await unitOfWork.CompleteAsync();
        return evidence;
    }
    
    public async Task<Evidence?> Handle(UpdateEvidenceCommand command)
    {
        var evidence = await evidenceRepository.FindByIdAsync(command.TripId);
        if (evidence == null)
        {
            return null;
        }
        //Update the evidence information
        evidence.Link = command.Link;
        
        await unitOfWork.CompleteAsync();
        return evidence;
    }
}