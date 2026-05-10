using ACME.CargoExpress.API.Registration.Domain.Model.Commands;
using ACME.CargoExpress.API.Registration.Interfaces.REST.Resources;
using ACME.CargoExpress.API.Registration.Domain.Model.Entities;
using ACME.CargoExpress.API.Registration.Domain.Model.ValueObjects;

namespace ACME.CargoExpress.API.Registration.Interfaces.REST.Transform;

public static class CreateOngoingTripCommandFromResourceAssembler
{
    public static CreateOngoingTripCommand ToCommandFromResource(CreateOngoingTripResource resource)
    {
        // Convertir string a enum (si no coincide, usa PENDIENTE por defecto)
        if (!Enum.TryParse<OngoingTripState>(resource.State?.Replace(" ", "_") ?? "PENDIENTE", true, out var state))
        {
            state = OngoingTripState.PENDIENTE;
        }

        return new CreateOngoingTripCommand(state, resource.Latitude, resource.Longitude, resource.Speed, resource.Distance, resource.TripId);
    }
}