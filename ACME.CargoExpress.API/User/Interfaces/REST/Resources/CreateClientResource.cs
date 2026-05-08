namespace ACME.CargoExpress.API.User.Interfaces.REST.Resources;

public record CreateClientResource(string Name, string Phone, string Ruc, string Address, int UserId);