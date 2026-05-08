using ACME.CargoExpress.API.Registration.Domain.Model.Queries;
using ACME.CargoExpress.API.Registration.Domain.Services;
using ACME.CargoExpress.API.Registration.Interfaces.REST.Resources;
using ACME.CargoExpress.API.Registration.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;


namespace ACME.CargoExpress.API.Registration.Interfaces.REST;


[ApiController]
[Route("api/v1/[controller]")]
public class EvidencesController (IEvidenceCommandService evidenceCommandService, IEvidenceQueryService evidenceQueryService)
    : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateEvidence([FromBody] CreateEvidenceResource createEvidenceResource)
    {
        try
        {
            var createEvidenceCommand = CreateEvidenceCommandFromResourceAssembler.ToCommandFromResource(createEvidenceResource);
            var evidence = await evidenceCommandService.Handle(createEvidenceCommand);
            if (evidence is null) return BadRequest();
            var resource = EvidenceResourceFromEntityAssembler.ToResourceFromEntity(evidence);
            return CreatedAtAction(nameof(GetEvidenceById), new { evidenceId = resource.Id }, resource);
        }
        catch (InvalidOperationException e)
        {
            // Handle the case where an Expense with the same TripId already exists
            return BadRequest(new {message = "An Expense with the same TripId already exists."});
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
            return BadRequest(new { message = "An error occurred while creating the evidence.", details = exceptionDetails });
        }
    }
    
    [HttpPut("{evidenceId}")]
    public async Task<IActionResult> UpdateEvidence([FromBody] UpdateEvidenceResource updateEvidenceResource, [FromRoute] int evidenceId)
    {
        try
        {
            var updateEvidenceCommand = UpdateEvidenceCommandFromResourceAssembler.ToCommandFromResource(updateEvidenceResource, evidenceId);
            var evidence = await evidenceCommandService.Handle(updateEvidenceCommand);
            if (evidence is null) return BadRequest();
            var resource = EvidenceResourceFromEntityAssembler.ToResourceFromEntity(evidence);
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
            return BadRequest(new { message = "An error occurred while updating the evidence.", details = exceptionDetails });
        }

    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllEvidences()
    {
        var getAllEvidencesQuery = new GetAllEvidencesQuery();
        var evidences = await evidenceQueryService.Handle(getAllEvidencesQuery);
        var resources = evidences.Select(EvidenceResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }
    
    [HttpGet("{evidenceId}")]
    public async Task<IActionResult> GetEvidenceById([FromRoute] int evidenceId)
    {
        var evidence = await evidenceQueryService.Handle(new GetEvidenceByIdQuery(evidenceId));
        if (evidence == null) return NotFound();
        var resource = EvidenceResourceFromEntityAssembler.ToResourceFromEntity(evidence);
        return Ok(resource);
    }
}