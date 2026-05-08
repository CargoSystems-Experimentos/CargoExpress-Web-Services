namespace ACME.CargoExpress.API.Registration.Domain.Model.Commands;

public record UpdateEvidenceCommand(int EvidenceId, string Link, int TripId);