using System.Collections.Concurrent;
using PortCall.Api.Models;
namespace PortCall.Api.Services;

public sealed class InMemoryFixtureRepository : IFixtureRepository
{
    private readonly ConcurrentDictionary<int, Fixture> _items = new();
    private int _nextId = 2;
    public InMemoryFixtureRepository()
    {
        _items[1] = new Fixture { Id=1, Reference="FIX-2026-001", VesselName="MV Ocean Pioneer",
            Charterer="Bluewater Chartering", LoadPort="Singapore", DischargePort="Rotterdam",
            FreightRate=18500, Currency="USD", Status=FixtureStatus.PendingApproval };
    }
    public IReadOnlyCollection<Fixture> GetAll() => _items.Values.OrderByDescending(x => x.CreatedAtUtc).ToArray();
    public Fixture? Get(int id) => _items.TryGetValue(id, out var item) ? item : null;
    public Fixture Add(Fixture fixture) { fixture.Id = Interlocked.Increment(ref _nextId); _items[fixture.Id] = fixture; return fixture; }
    public bool Update(Fixture fixture) { if (!_items.ContainsKey(fixture.Id)) return false; _items[fixture.Id] = fixture; return true; }
}
