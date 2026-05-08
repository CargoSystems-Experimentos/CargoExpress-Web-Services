using System.Runtime.InteropServices.JavaScript;

namespace ACME.CargoExpress.API.Registration.Domain.Model.ValueObjects;

public record TripData(string LoadLocation, DateTime LoadDate, string UnloadLocation, DateTime UnloadDate)
{
    public TripData() : this(string.Empty, new DateTime(), string.Empty, new DateTime())
    {
    }
}