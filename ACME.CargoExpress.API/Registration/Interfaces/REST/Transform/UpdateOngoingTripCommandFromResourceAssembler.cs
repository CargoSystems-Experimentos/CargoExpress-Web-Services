using ACME.CargoExpress.API.Registration.Domain.Model.Commands;
using ACME.CargoExpress.API.Registration.Interfaces.REST.Resources;
using ACME.CargoExpress.API.Registration.Domain.Model.ValueObjects;

namespace ACME.CargoExpress.API.Registration.Interfaces.REST.Transform;

public static class UpdateOngoingTripCommandFromResourceAssembler
{
    public static UpdateOngoingTripCommand ToCommandFromResource(UpdateOngoingTripResource resource, int ongoingTripId)
    {
        if (!Enum.TryParse<OngoingTripState>(resource.State?.Replace(" ", "_") ?? "PENDIENTE", true, out var state))
        {
            state = OngoingTripState.PENDIENTE;
        }

        return new UpdateOngoingTripCommand(ongoingTripId, state, resource.Latitude, resource.Longitude, resource.Speed, resource.Distance, resource.TripId);
    }
}