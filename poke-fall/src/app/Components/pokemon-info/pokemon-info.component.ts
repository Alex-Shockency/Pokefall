import { Component, ElementRef, HostListener, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Pokemon } from 'src/app/Entities/pokemon';
import { PokemonService } from 'src/app/Services/pokemon-service';
import Chart from 'chart.js/auto';
import { MatAccordion } from '@angular/material/expansion';
import { Evolution } from 'src/app/Entities/evolution';
import { Distance, Utilities } from 'src/app/Shared/utilities';
import { PokemonType } from 'src/app/Shared/pokemonTypes';
import { TypeEffectiveness } from 'src/app/Shared/typeEffectiveness';
import { TypeChart } from 'src/app/Shared/typeChart';
import { FormControl } from '@angular/forms';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { Move } from 'src/app/Entities/move';

@Component({
  selector: 'app-pokemon-info',
  templateUrl: './pokemon-info.component.html',
  styleUrls: ['./pokemon-info.component.scss'],
})
export class PokemonInfoComponent {
  @ViewChild(MatAccordion)
  accordion!: MatAccordion;

  enabled = new FormControl(false);

  pokemonId = 0;
  pokemon: Pokemon = {} as Pokemon;
  evolutions: Evolution[] = [];
  branchingEvolution: boolean = false;
  showBarChart = true;
  showRadarChart = false;
  audioUrl = '';
  totalStats = 0;
  pokemonLoaded!: Promise<boolean>;
  pokemonWeightLbs = 0;
  pokemonHeightFeet: Distance = {} as Distance;

  typeEffectiveness: TypeChart = {} as TypeChart;

  pokeCry!: HTMLMediaElement;

  gen7moves: Move[] = [];
  gen8moves: Move[] = [];
  gen9moves: Move[] = [];

  screenHeight = 0;
  screenWidth = 0;

  @HostListener('window:resize', ['$event'])
  onResize() {
    this.screenHeight = window.innerHeight;
    this.screenWidth = window.innerWidth;
  }
  
  dataSource = new MatTableDataSource<Move>();
  displayedColumns: string[] = [
    // "pokemonId",
    // "moveId",
    // "levelUpLearnable",
    'method',
    'levelLearned',
    // "tmLearnable",
    // "tutorLearnable",
    // "eggMoveLearnable",
    'name',
    //"description",
    'type',
    'category',
    'pp',
    'power',
    'accuracy',
    // "contact",
    // "affectedProtect",
    // "affectedSnatch",
    // "affectedMirrorMove",
  ];
  @ViewChild(MatPaginator, { static: false })
  set paginator(value: MatPaginator) {
    this.dataSource.paginator = value;
  }

  @ViewChild(MatSort, { static: false })
  set sort(value: MatSort) {
    this.dataSource.sort = value;
  }

  constructor(
    private pokemonService: PokemonService,
    private route: ActivatedRoute,
    private router: Router,
    protected utilities: Utilities
  ) {
    router.events.subscribe((val) => {
      window.scroll(0,0)
    })
  }

  ngOnInit() {
    this.screenWidth = window.innerWidth;
    this.route.queryParams.subscribe((params) => {
      this.pokemonId = params['id'];
      this.pokemonService
        .getPokemonById(this.pokemonId)
        .subscribe((pokemon) => {
          this.pokemon = pokemon;
          this.pokemonLoaded = Promise.resolve(true);


          this.gen7moves = pokemon.moves.filter(
            (move) => move.versionId == 18
          );
          this.gen8moves = pokemon.moves.filter(
            (move) => move.versionId >= 20 && move.versionId <= 24
          );
          this.gen9moves = pokemon.moves.filter((move) => move.versionId == 25);
          if (this.gen9moves.length != 0) {
            this.dataSource = new MatTableDataSource<Move>(
              this.gen9moves.sort((a, b) => {
                if (a.levelLearned == null) {
                  return 1;
                }
                if (b.levelLearned == null) {
                  return -1;
                }
                return a.levelLearned - b.levelLearned;
              })
            );
          } else if(this.gen8moves.length != 0){
            this.dataSource = new MatTableDataSource<Move>(
              this.gen8moves.sort((a, b) => {
                if (a.levelLearned == null) {
                  return 1;
                }
                if (b.levelLearned == null) {
                  return -1;
                }
                return a.levelLearned - b.levelLearned;
              })
            );
          } else{
            this.dataSource = new MatTableDataSource<Move>(
              this.gen7moves.sort((a, b) => {
                if (a.levelLearned == null) {
                  return 1;
                }
                if (b.levelLearned == null) {
                  return -1;
                }
                return a.levelLearned - b.levelLearned;
              })
            );
          }

          this.pokemonWeightLbs =
            Math.round((pokemon.weight / 10) * 2.20462262185 * 10) / 10;
          this.pokemonHeightFeet = this.utilities.convertMetersToFeetAndInches(
            pokemon.height / 10
          );

          this.typeEffectiveness = this.utilities.getTypeEffectiveness(
            this.utilities.capitalizeFirstLetter(pokemon.type1) as PokemonType,
            this.utilities.capitalizeFirstLetter(pokemon.type2) as PokemonType
          );

          this.pokeCry = document.getElementById(
            'audioElement'
          ) as HTMLMediaElement;

          this.pokeCry.src =
            'https://play.pokemonshowdown.com/audio/cries/' +
            this.pokemon.name.toLocaleLowerCase() +
            '.ogg';

          this.pokemonService
            .getEvolutionByChainId(pokemon.evolutionChainId)
            .subscribe((evolutions: Evolution[]) => {
              this.evolutions = evolutions;
            });
        });
    });
  }
  
  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }

  protected playCry(id: number) {
    this.pokeCry.play();
  }
}
