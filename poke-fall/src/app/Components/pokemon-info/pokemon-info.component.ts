import { Component, ElementRef, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Pokemon } from 'src/app/Entities/pokemon';
import { PokemonService } from 'src/app/Services/pokemon-service';
import Chart from 'chart.js/auto';
import { MatAccordion } from '@angular/material/expansion';
import { Evolution } from 'src/app/Entities/evolution';
import { Utilities } from 'src/app/Shared/utilities';
import { PokemonType } from 'src/app/Shared/pokemonTypes';
import { TypeEffectiveness } from 'src/app/Shared/typeEffectiveness';
import { TypeChart } from 'src/app/Shared/typeChart';

@Component({
  selector: 'app-pokemon-info',
  templateUrl: './pokemon-info.component.html',
  styleUrls: ['./pokemon-info.component.scss'],
})
export class PokemonInfoComponent {
  @ViewChild(MatAccordion)
  accordion!: MatAccordion;

  pokemonId = 0;
  pokemon: Pokemon = {} as Pokemon;
  evolutions: Evolution[] = [];
  branchingEvolution: boolean = false;
  showBarChart = true;
  showRadarChart = false;
  audioUrl = '';
  totalStats = 0;
  pokemonLoaded!: Promise<boolean>;

  typeEffectiveness:TypeChart = {} as TypeChart;

  pokeCry!: HTMLMediaElement;

  constructor(
    private pokemonService: PokemonService,
    private route: ActivatedRoute,
    protected utilities: Utilities
  ) {}

  ngOnInit() {
    window.scrollTo(0, 0);
    this.route.queryParams.subscribe((params) => {
      this.pokemonId = params['id'];
      this.pokemonService
        .getPokemonById(this.pokemonId)
        .subscribe((pokemon) => {
          this.pokemon = pokemon;
          this.pokemonLoaded = Promise.resolve(true);

          this.pokeCry = document.getElementById(
            'audioElement'
          ) as HTMLMediaElement;
          // this.pokeCry.src =
          //   'https://play.pokemonshowdown.com/audio/cries/' +
          //   this.formatName(this.pokemon.name) +
          //   '.ogg';

          this.typeEffectiveness =this.utilities.getTypeEffectiveness(
            this.utilities.capitalizeFirstLetter(pokemon.type1) as PokemonType,
            this.utilities.capitalizeFirstLetter(pokemon.type2) as PokemonType
          );

          this.pokemonService
            .getEvolutionByChainId(pokemon.evolutionChainId)
            .subscribe((evolutions: Evolution[]) => {
              this.evolutions = evolutions;
            });
        });
    });
  }

  protected playCry(id: number) {
    this.pokeCry.play();
  }
}
