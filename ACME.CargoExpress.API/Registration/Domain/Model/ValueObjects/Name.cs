namespace ACME.CargoExpress.API.Registration.Domain.Model.ValueObjects;

public record Name(string TripName)
{
    public Name() : this(string.Empty)
    {
    }
}
