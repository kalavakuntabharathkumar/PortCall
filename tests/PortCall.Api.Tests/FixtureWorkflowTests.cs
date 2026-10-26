using PortCall.Api.Models;
using PortCall.Api.Services;
using Xunit;

namespace PortCall.Api.Tests;

public class FixtureWorkflowTests
{
    [Fact]
    public void SeededFixtureStartsPendingApproval()
    {
        var repo = new InMemoryFixtureRepository();
        Assert.Equal(FixtureStatus.PendingApproval, repo.Get(1)!.Status);
    }

    [Fact]
    public void RepositoryAddsAndRetrievesFixture()
    {
        var repo = new InMemoryFixtureRepository();
        var fixture = repo.Add(new Fixture { Reference="FIX-TEST", VesselName="Test Vessel", Charterer="Test Charterer" });
        Assert.True(fixture.Id > 1);
        Assert.Equal("FIX-TEST", repo.Get(fixture.Id)!.Reference);
    }

    [Fact]
    public void DecisionStatusCanBeUpdated()
    {
        var repo = new InMemoryFixtureRepository();
        var fixture = repo.Get(1)!;
        fixture.Status = FixtureStatus.Approved;
        Assert.True(repo.Update(fixture));
        Assert.Equal(FixtureStatus.Approved, repo.Get(1)!.Status);
    }
}
