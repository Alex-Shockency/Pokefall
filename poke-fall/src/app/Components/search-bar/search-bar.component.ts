import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { Pokemon } from 'src/app/Entities/pokemon';
import { PokemonService } from 'src/app/Services/pokemon-service';

@Component({
  selector: 'app-search-bar',
  templateUrl: './search-bar.component.html',
  styleUrls: ['./search-bar.component.scss']
})

export class SearchBarComponent {
  title = 'poke-fall';
  searchString: string = '';
  searchResults: Pokemon[];
  searchLoading: boolean = false;

  constructor(private pokemonService: PokemonService, private router:Router) {
    this.searchResults = [];
  }
  
  async randomSearch(): Promise<void>{
    this.pokemonService.getPokemonById(Math.floor(Math.random()*1010)).subscribe((pokemon) =>{
      this.searchString = pokemon.name;
      this.router.navigate(
        ['../search'],
        { queryParams: { query: this.searchString.toLocaleLowerCase() } }
      ).then(()=>{
        window.location.reload();
      });
    });
  }

  async search(): Promise<void> {
    this.router.navigate(
      ['../search'],
      { queryParams: { query: this.searchString.toLocaleLowerCase() } }
    ).then(()=>{
      window.location.reload();
    });
    // this.searchLoading = true;
    // if (this.searchString) {
    //   //TODO: fix the nidoran case
    //   this.pokemonService.getAllPokemon().subscribe((pokemons) => {
    //     let filteredList = pokemons.filter((pokemon) =>
    //       pokemon.name.toLocaleLowerCase().includes(this.searchString) && !pokemon.name.toLocaleLowerCase().includes("-")
    //     );
    //     this.searchResults = filteredList.sort((a,b) => a.pokedexNumber - b.pokedexNumber);
    //     this.searchLoading = false;
    //   });
    // } else {
    //   this.searchResults = [];
    //   this.searchLoading = false;
    // }
  }
}
