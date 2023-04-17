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
      this.router.navigate(
        ['../search'],
        { queryParams: { q:  pokemon.name.toLocaleLowerCase(), gd: true } }
      ).then(()=>{
        window.location.reload();
      });
    });
  }

  async search(): Promise<void> {
    this.router.navigate(
      ['../search'],
      { queryParams: { q: this.searchString.toLocaleLowerCase(), gd:true }}
    ).then(()=>{
      window.location.reload();
    });
  }
}
