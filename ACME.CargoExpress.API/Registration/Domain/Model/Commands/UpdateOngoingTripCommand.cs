namespace ACME.CargoExpress.API.Registration.Domain.Model.Commands;
using ACME.CargoExpress.API.Registration.Domain.Model.ValueObjects;

public record UpdateOngoingTripCommand(int OngoingTripId, OngoingTripState State, float Latitude, float Longitude, int Speed, int Distance, int TripId);