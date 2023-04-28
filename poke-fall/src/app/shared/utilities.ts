import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Pokemon } from '../Entities/pokemon';
import { Evolution } from '../Entities/evolution';
import { SearchResultPokemon } from '../Entities/searchResultPokemon';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root',
})
export class Utilities {
  constructor(private router: Router) {}

  padId(id: number): string {
    return String(id).padStart(4, '0');
  }

  capitalizeFirstLetter(word: string): string {
    return word.charAt(0).toUpperCase() + word.slice(1);
  }

  navigateToPokemon(pokemonId: number) {
    this.router.navigate(['pokemon'], { queryParams: { id: pokemonId } });
  }

  genRandPokeId() {
    return Math.floor(Math.random() * 1010);
  }

  
}
