using Sabio.Models;
using Sabio.Models.Domain;
using Sabio.Models.Requests;

namespace Sabio.Services.Interfaces
{
    public interface IPokemonService
    {
        Paged<Pokemon> GetPaginated(int pageIndex, int pageSize);
        int AddPokemon(PokemonAddRequest model);
        Pokemon GetPokemonById(int id);
    }
}