using ACME.CargoExpress.API.Registration.Domain.Model.Queries;
using ACME.CargoExpress.API.Registration.Domain.Services;
using ACME.CargoExpress.API.Registration.Interfaces.REST.Transform;
using ACME.CargoExpress.API.User.Domain.Model.Queries;
using ACME.CargoExpress.API.User.Domain.Services;
using ACME.CargoExpress.API.User.Interfaces.REST.Resources;
using ACME.CargoExpress.API.User.Interfaces.REST.Transform;
using ACME.CargoExpress.API.Shared.Interfaces.ASP.Configuration.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace ACME.CargoExpress.API.User.Interfaces.REST;

[ApiController]
[Route("api/v1/[controller]")]
public class EntrepreneursController (IEntrepreneurQueryService entrepreneurQueryService, IEntrepreneurCommandService entrepreneurCommandService,
    ITripQueryService tripQueryService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateEntrepreneur([FromBody] CreateEntrepreneurResource createEntrepreneurResource)
    {
        if (string.IsNullOrWhiteSpace(createEntrepreneurResource.Name) || createEntrepreneurResource.Name.Length > 100)
            return BadRequest(new { message = "Name is required and max length is 100" });

        if (!createEntrepreneurResource.Phone.IsDigitsWithLength(9))
            return BadRequest(new { message = "Phone must have exactly 9 digits" });

        if (!createEntrepreneurResource.Ruc.IsDigitsWithLength(11))
            return BadRequest(new { message = "Ruc must have exactly 11 digits" });

        if (createEntrepreneurResource.UserId <= 0)
            return BadRequest(new { message = "UserId must be greater than 0" });

        try
        {
            var createEntrepreneurCommand = CreateEntrepreneurCommandFromResourceAssembler.ToCommandFromResource(createEntrepreneurResource);
            var entrepreneur = await entrepreneurCommandService.Handle(createEntrepreneurCommand);
            if (entrepreneur is null) return BadRequest();
            var resource = EntrepreneurResourceFromEntityAssembler.ToResourceFromEntity(entrepreneur);
            return CreatedAtAction(nameof(GetEntrepreneurById), new { entrepreneurId = resource.Id }, resource);
        }
        catch (Exception e)
        {
            var exceptionDetails = new
            {
                e.Message,
                e.StackTrace,
                InnerExceptionMessage = e.InnerException?.Message,
                InnerExceptionStackTrace = e.InnerException?.StackTrace
            };
            Console.WriteLine(exceptionDetails);
            return BadRequest(new { message = "An error occurred while creating the entrepreneur.", details = exceptionDetails });
        }
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllEntrepreneurs()
    {
        var getAllEntrepreneursQuery = new GetAllEntrepreneursQuery();
        var entrepreneurs = await entrepreneurQueryService.Handle(getAllEntrepreneursQuery);
        var resources = entrepreneurs.Select(EntrepreneurResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }
    
    [HttpGet("{entrepreneurId}")]
    public async Task<IActionResult> GetEntrepreneurById([FromRoute] int entrepreneurId)
    {
        var entrepreneur = await entrepreneurQueryService.Handle(new GetEntrepreneurByIdQuery(entrepreneurId));
        if (entrepreneur == null) return NotFound();
        var resource = EntrepreneurResourceFromEntityAssembler.ToResourceFromEntity(entrepreneur);
        return Ok(resource);
    }
    
    [HttpPut("{entrepreneurId}")]
    public async Task<IActionResult> UpdateEntrepreneur([FromBody] UpdateEntrepreneurResource updateEntrepreneurResource, [FromRoute] int entrepreneurId)
    {
        try
        {
            var updateEntrepreneurCommand = UpdateEntrepreneurCommandFromResourceAssembler.ToCommandFromResource(updateEntrepreneurResource, entrepreneurId);
            var entrepreneur = await entrepreneurCommandService.Handle(updateEntrepreneurCommand);
            if (entrepreneur is null) return BadRequest();
            var resource = EntrepreneurResourceFromEntityAssembler.ToResourceFromEntity(entrepreneur);
            return Ok(resource);
        }
        catch (Exception e)
        {
            var exceptionDetails = new
            {
                e.Message,
                e.StackTrace,
                InnerExceptionMessage = e.InnerException?.Message,
                InnerExceptionStackTrace = e.InnerException?.StackTrace
            };
            Console.WriteLine(exceptionDetails);
            return BadRequest(new { message = "An error occurred while updating the entrepreneur.", details = exceptionDetails });
        }
    }

    [HttpGet("{entrepreneurId}/drivers")]
    public async Task<IActionResult> GetDrivers([FromServices] ITripQueryService tripQueryService, int entrepreneurId)
    {
        var drivers = await tripQueryService.Handle(new GetDriversByEntrepreneurIdQuery(entrepreneurId));
        var driverResources = drivers.Select(d => new 
        {
            d.Id,
            d.Name,
            d.Dni,
            d.License,
            d.ContactNumber
        });
        return Ok(driverResources);
    }

    [HttpGet("{entrepreneurId}/vehicles")]
    public async Task<IActionResult> GetVehicles([FromServices] ITripQueryService tripQueryService, int entrepreneurId)
    {
        var vehicles = await tripQueryService.Handle(new GetVehiclesByEntrepreneurIdQuery(entrepreneurId));
        var vehicleResources = vehicles.Select(v => new
        {
            v.Id,
            v.Model,
            v.Plate,
            v.TractorPlate,
            v.MaxLoad,
            v.Volume
        });
        return Ok(vehicleResources);
    }

    [HttpGet("{entrepreneurId}/trips")]
    public async Task<IActionResult> GetTripsByEntrepreneurId([FromRoute] int entrepreneurId)
    {
        var trips = await tripQueryService.Handle(new GetTripsByEntrepreneurIdQuery(entrepreneurId));
        var resources = trips.Select(TripResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }
    
    [HttpGet("{entrepreneurId}/clients")]
    public async Task<IActionResult> GetClientsByEntrepreneurId([FromRoute] int entrepreneurId)
    {
        var clients = await tripQueryService.Handle(new GetClientsByEntrepreneurId(entrepreneurId));
        var resources = clients.Select(ClientResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }
}
