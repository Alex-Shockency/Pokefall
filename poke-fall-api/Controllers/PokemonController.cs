using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using poke_fall_api.DTO;
using poke_fall_api.Models;
using poke_fall_api.Utils;
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

        var Ability1 = await _context.Abilities.FindAsync(Pokemon.Ability1Id);
        var Ability2 = await _context.Abilities.FindAsync(Pokemon.Ability2Id);
        var HiddenAbility = await _context.Abilities.FindAsync(Pokemon.HiddenAbilityId);
        var Moves = _context.PokemonMoves
            .Where(pm => pm.PokemonId == Pokemon.Id && (pm.VersionId >= 18 && pm.VersionId <= 25))
            .Join(
                _context.Moves,
                pm => pm.MoveId,
                move => move.Id,
                (pm, move) =>
                    new FullMoveInfo
                    {
                        PokemonId = pm.PokemonId,
                        MoveId = pm.MoveId,
                        VersionId = pm.VersionId,
                        LevelUpLearnable = pm.LevelUpLearnable,
                        LevelLearned = pm.LevelLearned,
                        TMLearnable = pm.TMLearnable,
                        TutorLearnable = pm.TutorLearnable,
                        EggMoveLearnable = pm.EggMoveLearnable,
                        Name = move.Name,
                        Description = move.Description,
                        Type = move.Type,
                        Category = move.Category,
                        PP = move.PP,
                        Power = move.Power,
                        Accuracy = move.Accuracy,
                        Contact = move.Contact,
                        AffectedProtect = move.AffectedProtect,
                        AffectedSnatch = move.AffectedSnatch,
                        AffectedMirrorMove = move.AffectedMirrorMove,
                    }
            )
            .ToArray();

        PokemonDTO result = ToPokemonDTO(Pokemon, Ability1, Ability2, HiddenAbility, Moves);
        return Ok(result);
    }

    [HttpGet("evolution/{chainId}")]
    [ProducesResponseType(typeof(IEnumerable<EvolutionDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Evolution>> GetEvolutionChain(int chainId)
    {
        List<Pokemon> pokemonList = _context.Pokemon
            .Where(p => p.EvolutionChainId == chainId)
            .OrderBy(p => p.PokedexNumber)
            .ToList();

        var evolutionList = _context.Evolutions.Where(e => e.EvolutionChainId == chainId).ToList();
        List<EvolutionDTO> evolutionDtos = new List<EvolutionDTO>();
        int evoCount = 1;

        foreach (Pokemon pokemon in pokemonList)
        {
            var tempEvolution = evolutionList.Find(
                e => e.EvolvedPokedexNumber == pokemon.PokedexNumber
            );

            if (pokemon.IsBaby)
            {
                if (tempEvolution != null)
                {
                    evolutionDtos.Insert(0, ToEvolutionDTO(tempEvolution, pokemon));
                }
                else
                {
                    evolutionDtos.Insert(0, ToEvolutionDTO(new Evolution(), pokemon));
                }
            }
            else
            {
                if (tempEvolution != null)
                {
                    evolutionDtos.Add(ToEvolutionDTO(tempEvolution, pokemon));
                }
                else
                {
                    evolutionDtos.Add(ToEvolutionDTO(new Evolution(), pokemon));
                }
            }
        }
        var index = 0;
        foreach (EvolutionDTO evo in evolutionDtos)
        {
            evo.Children = new List<EvolutionDTO>();
            //Base Poke
            if (index == 0)
            {
                evo.Children = evolutionDtos.FindAll(
                    evolution => evolution.EvolvedFromPokedexNumber == evo.PokedexNumber
                );
            }
            else
            {
                //Not a form evolution
                if (!(evo.Id >= 10000))
                {
                    evo.Children = evolutionDtos.FindAll(
                         evolution => evolution.EvolvedFromPokedexNumber == evo.PokedexNumber
                     );
                }
            }

            if (evo.Children.Count > 0)
            {
                evoCount++;
            }
            index++;
        }

        if (evolutionDtos.Count == 0)
        {
            return NotFound();
        }

        return Ok(new { evolutions = evolutionDtos, evoCount = evoCount });
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<PokemonDTO>), StatusCodes.Status200OK)]
    [EnableCors("_myAllowSpecificOrigins")]
    public IQueryable<Pokemon> GetAllPokemon()
    {
        return _context.Set<Pokemon>();
    }

    [HttpGet("search/{queryString}")]
    [ProducesResponseType(typeof(IEnumerable<SearchResultPokemonDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<SearchResultPokemonDTO>>> ListPokemon(
        string queryString
    )
    {
        var searchTerms = QueryStringUtils.readQueryString(queryString);

        IQueryable<Pokemon> pokemonQuery = _context.Pokemon.Where(p => p.Id < 10000);

        if (searchTerms.Name.Any())
        {
            if (searchTerms.Name.Count() > 1)
            {
                return await Task.FromResult(Ok(new List<SearchResultPokemonDTO>()));
            }
            pokemonQuery = pokemonQuery.Where(
                p => p.Name.ToLower().Contains(searchTerms.Name.First().ToLower())
            );
        }

        if (searchTerms.Type.Any())
        {
            string firstType = searchTerms.Type.First().ToLower();
            if (searchTerms.Type.Count() == 2)
            {
                searchTerms.Type.Remove(firstType);
                string secondType = searchTerms.Type.First().ToLower();
                pokemonQuery = pokemonQuery.Where(
                    p =>
                        (p.Type1.ToLower() == firstType && p.Type2.ToLower() == secondType)
                        || (p.Type1.ToLower() == secondType && p.Type2.ToLower() == firstType)
                );
            }
            else
            {
                pokemonQuery = pokemonQuery.Where(
                    p => p.Type1.ToLower() == firstType || p.Type2.ToLower() == firstType
                );
            }
        }

        if (searchTerms.Ability.Any())
        {
            List<int> abilityIds = _context.Abilities
                .Where(a => searchTerms.Ability.Contains(a.Name.ToLower()))
                .Select(a => a.Id)
                .ToList();
            pokemonQuery = pokemonQuery.Where(
                p =>
                    abilityIds.Contains(p.Ability1Id)
                    || (p.Ability2Id.HasValue && abilityIds.Contains(p.Ability2Id.Value))
                    || abilityIds.Contains(p.HiddenAbilityId)
            );
        }

        List<Pokemon> queryResult = pokemonQuery.OrderBy(p => p.Id).ToList();
        if (searchTerms.BaseStatTotal != null)
        {
            queryResult = queryResult.FindAll(
                p =>
                    applyBaseStatSearchTerm(
                        searchTerms.BaseStatTotal.Item1,
                        searchTerms.BaseStatTotal.Item2,
                        p
                    )
            );
        }

        if (searchTerms.HP != null)
        {
            queryResult = queryResult.FindAll(
                p => applyIntegerSearchTerm(searchTerms.HP.Item1, searchTerms.HP.Item2, p.HP)
            );
        }

        if (searchTerms.Attack != null)
        {
            queryResult = queryResult.FindAll(
                p =>
                    applyIntegerSearchTerm(
                        searchTerms.Attack.Item1,
                        searchTerms.Attack.Item2,
                        p.Attack
                    )
            );
        }

        if (searchTerms.Defense != null)
        {
            queryResult = queryResult.FindAll(
                p =>
                    applyIntegerSearchTerm(
                        searchTerms.Defense.Item1,
                        searchTerms.Defense.Item2,
                        p.Defense
                    )
            );
        }

        if (searchTerms.SpecialAttack != null)
        {
            queryResult = queryResult.FindAll(
                p =>
                    applyIntegerSearchTerm(
                        searchTerms.SpecialAttack.Item1,
                        searchTerms.SpecialAttack.Item2,
                        p.SpecAttack
                    )
            );
        }

        if (searchTerms.SpecialDefense != null)
        {
            queryResult = queryResult.FindAll(
                p =>
                    applyIntegerSearchTerm(
                        searchTerms.SpecialDefense.Item1,
                        searchTerms.SpecialDefense.Item2,
                        p.SpecDefense
                    )
            );
        }

        if (searchTerms.Speed != null)
        {
            queryResult = queryResult.FindAll(
                p =>
                    applyIntegerSearchTerm(
                        searchTerms.Speed.Item1,
                        searchTerms.Speed.Item2,
                        p.Speed
                    )
            );
        }

        List<SearchResultPokemonDTO> result = ListToSearchResultPokemonDTO(queryResult);

        return await Task.FromResult(Ok(result));
    }

    private PokemonDTO ToPokemonDTO(
        Pokemon item,
        Ability ability1,
        Ability ability2,
        Ability hiddenAbility,
        FullMoveInfo[] moves
    )
    {
        return new PokemonDTO
        {
            Id = item.Id,
            EvolutionChainId = item.EvolutionChainId,
            PokedexNumber = item.PokedexNumber,
            Name = item.Name,
            Type1 = item.Type1.ToString(),
            Type2 = item.Type2.ToString(),
            Ability1 = ability1,
            Ability2 = ability2,
            HiddenAbility = hiddenAbility,
            Moves = moves,
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

    private SearchResultPokemonDTO ToSearchResultPokemonDTO(Pokemon pokemon)
    {
        return new SearchResultPokemonDTO
        {
            Id = pokemon.Id,
            PokedexNumber = pokemon.PokedexNumber,
            Name = pokemon.Name,
            Type1 = pokemon.Type1.ToString(),
            Type2 = pokemon.Type2.ToString(),
        };
    }

    private EvolutionDTO ToEvolutionDTO(
        Evolution evolution,
        Pokemon pokemon,
        List<EvolutionDTO>? children = null
    )
    {
        return new EvolutionDTO
        {
            Id = pokemon.Id,
            Children = children,
            PokedexNumber = pokemon.PokedexNumber,
            EvolvedFromPokedexNumber = evolution.EvolvedFromPokedexNumber,
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

    private List<SearchResultPokemonDTO> ListToSearchResultPokemonDTO(List<Pokemon> results)
    {
        return results.Select(p => ToSearchResultPokemonDTO(p)).ToList();
    }

    private bool applyBaseStatSearchTerm(string op, int searchNumber, Pokemon p)
    {
        int baseStatTotal = new int[]
        {
            p.Attack,
            p.Defense,
            p.SpecAttack,
            p.SpecDefense,
            p.Speed,
            p.HP
        }.Sum();

        return applyIntegerSearchTerm(op, searchNumber, baseStatTotal);
    }

    private Boolean applyIntegerSearchTerm(string op, int searchNumber, int actualNumber)
    {
        switch (op)
        {
            case "<":
                return actualNumber < searchNumber;
            case ">":
                return actualNumber > searchNumber;
            case "<=":
                return actualNumber <= searchNumber;
            case ">=":
                return actualNumber >= searchNumber;
            case "=":
                return actualNumber == searchNumber;
            default:
                throw new ArgumentException("Invalid operator for numerical comparison");
        }
    }
}
