import { Component, ElementRef, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Pokemon } from 'src/app/Entities/pokemon';
import { PokemonService } from 'src/app/Services/pokemon-service';
import Chart from 'chart.js/auto';
import { MatAccordion } from '@angular/material/expansion';
import { Evolution } from 'src/app/Entities/evolution';

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
  evolutions: Evolution[] =  [];
  branchingEvolution:boolean =  false;
  showBarChart = true;
  showRadarChart = false;
  audioUrl = '';
  totalStats = 0;
  pokemonLoaded!: Promise<boolean>;

  pokeCry!: HTMLMediaElement;

  constructor(
    private pokemonService: PokemonService,
    private route: ActivatedRoute,
    private router: Router,
  ) { }

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
            this.pokemonService.getEvolutionByChainId(pokemon.evolutionChainId).subscribe((evolutions:Evolution[]) =>{
              this.evolutions = evolutions;
            })
        });
    });
  }

  protected playCry(id: number) {
    this.pokeCry.play();
  }

  navigateToPokemon(pokemonId: number){
    this.router.navigate(
      ['pokemon'],
      { queryParams: { id: pokemonId } }
    )
  }

  toggleOnBar() {
    this.showBarChart = true;
    this.showRadarChart = false;
  }
  toggleOnRadar() {
    this.showRadarChart = true;
    this.showBarChart = false;
  }

  formatName(name: string) {
    return name.toLowerCase();
  }

  padId(id: number): string {
    return String(id).padStart(4, '0');
  }
}
