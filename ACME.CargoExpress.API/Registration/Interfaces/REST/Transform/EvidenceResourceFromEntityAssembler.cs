using ACME.CargoExpress.API.Registration.Domain.Model.Entities;
using ACME.CargoExpress.API.Registration.Interfaces.REST.Resources;

namespace ACME.CargoExpress.API.Registration.Interfaces.REST.Transform;

public class EvidenceResourceFromEntityAssembler
{
    public static EvidenceResource ToResourceFromEntity(Evidence entity)
    {
        return new EvidenceResource(entity.Id, entity.Link, entity.TripId);
    }
}