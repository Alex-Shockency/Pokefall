import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Pokemon } from 'src/app/Entities/pokemon';
import { PokemonService } from 'src/app/Services/pokemon-service';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.scss']
})
export class SearchComponent {
  searchString = "";
  searchLoading = false;
  searchResults:Pokemon[] = [];
  gridDisplay:boolean = false;

  constructor(private pokemonService: PokemonService, private route: ActivatedRoute,) {
    this.searchLoading = false;
  }

  ngOnInit() {
    this.route.queryParams.subscribe(params => {
      this.searchString = params['q'];
      this.gridDisplay = JSON.parse(params['gd']);
    });
    this.getPokemon();
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
       this.searchLoading = false;
     });
   } else {
     this.searchResults = [];
     this.searchLoading = false;
   }
 }
}
