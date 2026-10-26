namespace PortCall.Api.Models;

public enum FixtureStatus { Draft, PendingApproval, Approved, Rejected }

public sealed class Fixture
{
    public int Id { get; set; }
    public string Reference { get; set; } = "";
    public string VesselName { get; set; } = "";
    public string Charterer { get; set; } = "";
    public string LoadPort { get; set; } = "";
    public string DischargePort { get; set; } = "";
    public decimal FreightRate { get; set; }
    public string Currency { get; set; } = "USD";
    public FixtureStatus Status { get; set; } = FixtureStatus.Draft;
    public string? DecisionNote { get; set; }
    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
    public DateTime? DecidedAtUtc { get; set; }
}

public sealed record CreateFixtureRequest(string Reference, string VesselName, string Charterer,
    string LoadPort, string DischargePort, decimal FreightRate, string Currency);
public sealed record DecisionRequest(bool Approve, string Note);
