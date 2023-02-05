using Sabio.Models;
using Sabio.Models.Domain;
using Sabio.Models.Requests;

namespace Sabio.Services.Interfaces
{
    public interface IPokemonService
    {
        Paged<Pokemon> GetPaginated(int pageIndex, int pageSize, int sortId);
        int AddPokemon(PokemonAddRequest model);
        Pokemon GetPokemonById(int id);
        Paged<Pokemon> SearchPaginated(int pageIndex, int pageSize, string query);
    }
}