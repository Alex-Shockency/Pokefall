using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using poke_fall_api.DTO;
using poke_fall_api.Models;
using System.Linq;

namespace poke_fall_api.Controllers;

[ApiController]
[Route("[controller]")]
public class PokemonController : ControllerBase
{

    private readonly PokefallContext _context;

    public PokemonController(PokefallContext context)
    {
        _context = context;
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(IEnumerable<PokemonDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Pokemon>> GetPokemon(int id)
    {
        var Pokemon = await _context.Pokemon.FindAsync(id);

        if (Pokemon == null)
        {
            return NotFound();
        }
        PokemonDTO result = ToDTO(Pokemon);
        return Ok(result);
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<PokemonDTO>), StatusCodes.Status200OK)]
    [EnableCors("_myAllowSpecificOrigins")]
    public IQueryable<Pokemon> GetAllPokemon()
    {
        return _context.Set<Pokemon>();
    }

    // [HttpGet("/search?{queryString}")]
    // [ProducesResponseType(typeof(IEnumerable<PokemonDTO>), StatusCodes.Status200OK)]
    // [ProducesResponseType(StatusCodes.Status404NotFound)]
    // public async Task<ActionResult<IEnumerable<PokemonDTO>>> ListPokemon(string queryString) {
    //     var pokemonQuery = from p in _context.Pokemon
    //                   where p.Type1 == queryString ||
    //                         p.Type2 == queryString
    //                   select p;
    //     List<PokemonDTO> result = ListToDTO(pokemonQuery.OrderBy(p => p.Id).ToList());
    //     return await Task.FromResult(Ok(result));
    // }

    private PokemonDTO ToDTO(Pokemon item)
    {
        return new PokemonDTO
        {
            Id = item.Id,
            PokedexNumber = item.PokedexNumber,
            Name = item.Name,
            Type1 = item.Type1.ToString(),
            Type2 = item.Type2.ToString(),
            Ability1Id = item.Ability1Id,
            Ability2Id = item.Ability2Id,
            HiddenAbilityId = item.HiddenAbilityId,
            Height = item.Height,
            Weight = item.Weight,
            BaseEXPYield = item.BaseEXPYield,
            HP = item.HP,
            Attack = item.Attack,
            Defense = item.Defense,
            SpecAttack = item.SpecAttack,
            SpecDefense = item.SpecDefense,
            Speed = item.Speed,
        };
    }

    private List<PokemonDTO> ListToDTO(List<Pokemon> items)
    {
        return items.Select(p => ToDTO(p)).ToList();
    }



}
