import { AsyncPipe } from '@angular/common';
import { Component } from '@angular/core';
import { PageEvent } from '@angular/material/paginator';
import { Pokemon, PokemonClient } from 'pokenode-ts';
import { count } from 'rxjs';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent {
  title = 'poke-fall';
  searchString: string = 'pikachu';
  searchResults: Pokemon[];
  searchLoading:boolean = false;

  constructor() {
    this.searchResults = [];
  }

  ngOnInit() {
    this.search();
  }

  async search(): Promise<void> {
    this.searchLoading = true;
    if (this.searchString) {
      let tempResults: Pokemon[] = [];
      const api = new PokemonClient();
      await api
        .listPokemons(0, 100000)
        .then((data) => {
          let filteredList = data.results.filter((pokemon) =>
            pokemon.name.includes(this.searchString)
          );
          let tasks:Promise<Pokemon>[] = [];
          filteredList.forEach((filteredPokemon) => {
            tasks.push(this.getPokemonDetails(filteredPokemon.name));
          });
          Promise.all(tasks).then(results=>{
            this.searchResults = results;
            this.searchLoading = false;
          })
        })
        .catch((error) => console.error(error));
    } else {
      this.searchResults = [];
      this.searchLoading = false;
    }
  }

  async getPokemonDetails(name: string): Promise<Pokemon> {
    const api = new PokemonClient();
    return api.getPokemonByName(name);
  }
}
