using ACME.CargoExpress.API.Registration.Domain.Model.Entities;
using ACME.CargoExpress.API.Registration.Domain.Model.Queries;
using ACME.CargoExpress.API.Registration.Domain.Repositories;
using ACME.CargoExpress.API.Registration.Domain.Services;

namespace ACME.CargoExpress.API.Registration.Application.Internal.QueryServices;

public class EvidenceQueryService(IEvidenceRepository evidenceRepository)
    : IEvidenceQueryService
{
    public async Task<Evidence?> Handle(GetEvidenceByIdQuery query)
    {
        return await evidenceRepository.FindByIdAsync(query.EvidenceId);
    }
    
    public async Task<IEnumerable<Evidence>> Handle(GetAllEvidencesQuery query)
    {
        return await evidenceRepository.ListAsync();
    }
}