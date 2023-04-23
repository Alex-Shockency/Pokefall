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

    [HttpGet("evolution/{chainId}")]
    [ProducesResponseType(typeof(IEnumerable<EvolutionDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Evolution>> GetEvolutionChain(int chainId)
    {
        var pokemonList = _context.Pokemon
            .Where(p => p.EvolutionChainId == chainId)
            .OrderBy(p => p.PokedexNumber)
            .ToList();
        var evolutionList = _context.Evolutions.Where(e => e.EvolutionChainId == chainId).ToList();
        List<EvolutionDTO> evolutionDtos = new List<EvolutionDTO>();
        var index = 0;
        foreach (Pokemon pokemon in pokemonList)
        {
            if (index == 0)
            {
                evolutionDtos.Add(ToEvolutionDTO(new Evolution(), pokemon));
            }
            else
            {
                var tempEvolution = evolutionList.Find(
                    e => e.EvolvedPokedexNumber == pokemon.PokedexNumber
                );
                if (tempEvolution != null)
                {
                    evolutionDtos.Add(ToEvolutionDTO(tempEvolution, pokemon));
                }
            }
            index++;
        }

        if (pokemonList == null)
        {
            return NotFound();
        }
        return Ok(evolutionDtos);
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

    private EvolutionDTO ToEvolutionDTO(Evolution evolution, Pokemon pokemon)
    {
        return new EvolutionDTO
        {
            Id = pokemon.Id,
            PokedexNumber = pokemon.PokedexNumber,
            EvolutionTrigger = evolution.EvolutionTrigger,
            TriggerItem = evolution.TriggerItem,
            MinLevel = evolution.MinLevel,
            Gender = evolution.Gender,
            Location = evolution.Location,
            HeldItem = evolution.HeldItem,
            TimeOfDay = evolution.TimeOfDay,
            KnownMoveId = evolution.KnownMoveId,
            KnownMoveType = evolution.KnownMoveType,
            MinHappiness = evolution.MinHappiness,
            MinBeauty = evolution.MinBeauty,
            MinAffection = evolution.MinAffection,
            RelativePhysicalStats = evolution.RelativePhysicalStats,
            PartyPokedexNumber = evolution.PartyPokedexNumber,
            PartyType = evolution.PartyType,
            TradePokedexNumber = evolution.TradePokedexNumber,
            NeedsOverworldRain = evolution.NeedsOverworldRain,
            TurnUpsideDown = evolution.TurnUpsideDown,
        };
    }

    private List<PokemonDTO> ListToDTO(List<Pokemon> items)
    {
        return items.Select(p => ToDTO(p)).ToList();
    }
}
