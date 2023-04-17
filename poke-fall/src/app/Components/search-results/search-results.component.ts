import {
  AfterViewInit,
  Component,
  Input,
  SimpleChanges,
  ViewChild,
} from '@angular/core';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { Output, EventEmitter } from '@angular/core';
import { Pokemon } from 'src/app/Entities/pokemon';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { MatTableDataSource } from '@angular/material/table';
import { MatSort } from '@angular/material/sort';
import { ActivatedRoute } from '@angular/router';
import { PokemonService } from 'src/app/Services/pokemon-service';

export interface Tile {
  color: string;
  cols: number;
  rows: number;
  text: string;
}

@Component({
  selector: 'app-search-results',
  templateUrl: './search-results.component.html',
  styleUrls: ['./search-results.component.scss'],
})

export class SearchResultsComponent implements AfterViewInit {
  @Input() searchResults: Pokemon[] = [];
  @Input() searchLoading: boolean;

  searchString = "";
  randomId = this.genRandPokeId();
  
  dataSource = new MatTableDataSource<Pokemon>();
  displayedColumns: string[] = [
    'name',
    'type1',
    'type2',
    'hp',
    'defense',
    'specAttack',
    'specDefense',
    'speed',
  ];

  @ViewChild(MatPaginator, { static: false })
  set paginator(value: MatPaginator) {
    this.dataSource.paginator = value;
  }

  @ViewChild(MatSort, { static: false })
  set sort(value: MatSort) {
    this.dataSource.sort = value;
  }

  imageUrl = '';
  panelOpenState = false;

  hidePageSize = false;
  showPageSizeOptions = true;
  showFirstLastButtons = true;
  disabled = false;
  gridDisplay = true;

  ngOnInit() {
    this.route.queryParams.subscribe(params => {
      this.searchString = params['query'];
    });
    this.getPokemon();
  }

  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

  constructor(private pokemonService: PokemonService, private route: ActivatedRoute,) {
    this.searchLoading = false;
  }

  getPokemon(){
     this.searchLoading = true;
    if (this.searchString) {
      //TODO: fix the nidoran case
      this.pokemonService.getAllPokemon().subscribe((pokemons) => {
        let filteredList = pokemons.filter((pokemon) =>
          pokemon.name.toLocaleLowerCase().includes(this.searchString)
        );
        this.searchResults = filteredList.sort((a,b) => a.pokedexNumber - b.pokedexNumber);
        this.dataSource = new MatTableDataSource<Pokemon>(this.searchResults);
        this.searchLoading = false;
      });
    } else {
      this.searchResults = [];
      this.searchLoading = false;
    }
  }

  padId(id: number): string {
    return String(id).padStart(4, '0');
  }

  setAlternateImage(id: number, dexNum: number) {
    document
      .getElementById(id.toString())
      ?.setAttribute(
        'src',
        'https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/other/official-artwork/' +
          dexNum +
          '.png'
      );
  }

  capitalizeFirstLetter(word: string): string {
    return word.charAt(0).toUpperCase() + word.slice(1);
  }

  displayGrid(display: boolean) {
    this.gridDisplay = display;
  }

  genRandPokeId() {
    return Math.floor(Math.random() * 1010);
  }

  protected playCry(id: number) {
    let audio: HTMLAudioElement = <HTMLAudioElement>(
      document.getElementById('audio-' + id.toString())
    );
    audio.play();
  }

  formatName(name: string) {
      return name.toLowerCase();
  }
}
