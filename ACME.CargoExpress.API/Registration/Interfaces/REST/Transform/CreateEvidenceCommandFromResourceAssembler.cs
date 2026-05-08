using ACME.CargoExpress.API.Registration.Domain.Model.Commands;
using ACME.CargoExpress.API.Registration.Interfaces.REST.Resources;

namespace ACME.CargoExpress.API.Registration.Interfaces.REST.Transform;

public static class CreateEvidenceCommandFromResourceAssembler
{
    public static CreateEvidenceCommand ToCommandFromResource(CreateEvidenceResource resource)
    {
        return new CreateEvidenceCommand(resource.Link, resource.TripId);
    }
}