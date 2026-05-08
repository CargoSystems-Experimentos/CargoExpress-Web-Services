using ACME.CargoExpress.API.Registration.Domain.Model.Aggregates;
using ACME.CargoExpress.API.Registration.Domain.Model.Commands;

namespace ACME.CargoExpress.API.Registration.Domain.Model.Entities;

public class Evidence
{
    public Evidence()
    {
        Link = string.Empty;
        Trip = new Trip();
    }
    
    public Evidence(string link, int tripId, Trip trip)
    {
        Link = link;
        TripId = tripId;
        Trip = trip;
    }
    
    public Evidence(CreateEvidenceCommand command, Trip trip)
    {
        Link = command.Link;
        Trip = trip;
    }
    public int Id { get; set; }
    public string Link { get; set; }
    public int TripId { get; set; }
    public Trip Trip { get; }
}