using ACME.CargoExpress.API.Registration.Domain.Model.Commands;
using ACME.CargoExpress.API.Registration.Interfaces.REST.Resources;

namespace ACME.CargoExpress.API.Registration.Interfaces.REST.Transform;

public class UpdateEvidenceCommandFromResourceAssembler
{
    public static UpdateEvidenceCommand ToCommandFromResource(UpdateEvidenceResource resource, int evidenceId)
    {
        return new UpdateEvidenceCommand(evidenceId, resource.Link, resource.TripId);
    }
}