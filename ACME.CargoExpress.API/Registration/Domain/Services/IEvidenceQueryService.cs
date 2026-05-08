using ACME.CargoExpress.API.Registration.Domain.Model.Entities;
using ACME.CargoExpress.API.Registration.Domain.Model.Queries;

namespace ACME.CargoExpress.API.Registration.Domain.Services;

public interface IEvidenceQueryService
{
    Task<Evidence?> Handle(GetEvidenceByIdQuery query);
    Task<IEnumerable<Evidence>> Handle(GetAllEvidencesQuery query);
    
}