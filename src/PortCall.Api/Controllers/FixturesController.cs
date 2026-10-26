using Microsoft.AspNetCore.Mvc;
using PortCall.Api.Models;
using PortCall.Api.Services;

namespace PortCall.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class FixturesController(IFixtureRepository repository) : ControllerBase
{
    [HttpGet]
    public ActionResult<IReadOnlyCollection<Fixture>> GetAll() => Ok(repository.GetAll());

    [HttpGet("{id:int}")]
    public ActionResult<Fixture> Get(int id)
    {
        var fixture = repository.Get(id);
        return fixture is null ? NotFound(new { error = "Fixture not found" }) : Ok(fixture);
    }

    [HttpPost]
    public ActionResult<Fixture> Create(CreateFixtureRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Reference) || string.IsNullOrWhiteSpace(request.VesselName)
            || string.IsNullOrWhiteSpace(request.Charterer) || string.IsNullOrWhiteSpace(request.LoadPort)
            || string.IsNullOrWhiteSpace(request.DischargePort) || request.FreightRate <= 0)
            return BadRequest(new { error = "Required fields must be provided and freight rate must be positive" });
        var fixture = repository.Add(new Fixture { Reference=request.Reference.Trim(), VesselName=request.VesselName.Trim(),
            Charterer=request.Charterer.Trim(), LoadPort=request.LoadPort.Trim(), DischargePort=request.DischargePort.Trim(),
            FreightRate=request.FreightRate, Currency=string.IsNullOrWhiteSpace(request.Currency) ? "USD" : request.Currency.ToUpperInvariant() });
        return CreatedAtAction(nameof(Get), new { id = fixture.Id }, fixture);
    }

    [HttpPost("{id:int}/submit")]
    public ActionResult<Fixture> Submit(int id)
    {
        var fixture = repository.Get(id);
        if (fixture is null) return NotFound();
        if (fixture.Status != FixtureStatus.Draft) return Conflict(new { error = "Only draft fixtures can be submitted" });
        fixture.Status = FixtureStatus.PendingApproval;
        repository.Update(fixture);
        return Ok(fixture);
    }

    [HttpPost("{id:int}/decision")]
    public ActionResult<Fixture> Decide(int id, DecisionRequest request)
    {
        var fixture = repository.Get(id);
        if (fixture is null) return NotFound();
        if (fixture.Status != FixtureStatus.PendingApproval) return Conflict(new { error = "Only pending fixtures can receive a decision" });
        fixture.Status = request.Approve ? FixtureStatus.Approved : FixtureStatus.Rejected;
        fixture.DecisionNote = request.Note;
        fixture.DecidedAtUtc = DateTime.UtcNow;
        repository.Update(fixture);
        return Ok(fixture);
    }
}
