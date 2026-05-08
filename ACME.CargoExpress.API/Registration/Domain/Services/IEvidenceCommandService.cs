using ACME.CargoExpress.API.Registration.Domain.Model.Commands;
using ACME.CargoExpress.API.Registration.Domain.Model.Entities;

namespace ACME.CargoExpress.API.Registration.Domain.Services;

public interface IEvidenceCommandService
{
    Task<Evidence?> Handle(CreateEvidenceCommand createTripCommand);
    Task<Evidence?> Handle(UpdateEvidenceCommand updateTripCommand);
}