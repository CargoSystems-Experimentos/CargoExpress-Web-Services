namespace ACME.CargoExpress.API.Registration.Domain.Model.Commands;

public record CreateEvidenceCommand(string Link, int TripId);