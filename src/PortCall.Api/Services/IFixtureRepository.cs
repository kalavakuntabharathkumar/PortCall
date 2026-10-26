using PortCall.Api.Models;
namespace PortCall.Api.Services;

public interface IFixtureRepository
{
    IReadOnlyCollection<Fixture> GetAll();
    Fixture? Get(int id);
    Fixture Add(Fixture fixture);
    bool Update(Fixture fixture);
}
