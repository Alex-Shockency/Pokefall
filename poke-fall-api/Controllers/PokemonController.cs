using Microsoft.AspNetCore.Mvc;
using poke_fall_api.Context;
using poke_fall_api.DTO;
using poke_fall_api.Models;

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
        var Pokemon = await _context.Pokemons.FindAsync(id);

        if (Pokemon == null)
        {
            return NotFound();
        }
        PokemonDTO result = ToDTO(Pokemon);
        return Ok(result);
    }

    private PokemonDTO ToDTO(Pokemon item)
    {
        return new PokemonDTO
        {
            PokemonId = item.PokemonId,
            PokedexNumber = item.PokedexNumber,
            Name = item.Name,
            Type1 = item.Type1.ToString(),
            Type2 = item.Type2.ToString(),
            Ability1Id = item.Ability1Id,
            Ability2Id = item.Ability2Id,
            HiddenAbilityId = item.HiddenAbilityId,
            CatchRate = item.CatchRate,
            GenderRatio = item.GenderRatio,
            EggGroup = item.EggGroup.ToString(),
            HatchStepsMin = item.HatchStepsMin,
            HatchStepMax = item.HatchStepMax,
            Height = item.Height,
            Weight = item.Weight,
            BaseEXPYield = item.BaseEXPYield,
            LevelingRate = item.LevelingRate,
            EVYield = item.EVYield,
            BaseFriendship = item.BaseFriendship,
            BaseStats = item.BaseStats,
            LevelUpMoves = item.LevelUpMoves,
            TMMoves = item.TMMoves,
            EggMoves = item.EggMoves,
            TutorMoves = item.TutorMoves,
            EvolvesTo = item.EvolvesTo,
            EvolvesFrom = item.EvolvesFrom,
            EvolutionDescription = item.EvolutionDescription
        };
    }

    private List<PokemonDTO> ListToDTO(List<Pokemon> items)
    {
        return items.Select(p => ToDTO(p)).ToList();
    }



}
