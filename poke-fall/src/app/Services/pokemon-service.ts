import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Pokemon } from '../Entities/pokemon';
import { Evolution } from '../Entities/evolution';
import { SearchResultPokemon } from '../Entities/searchResultPokemon';

@Injectable({
  providedIn: 'root',
})
export class PokemonService {
  private api: string = 'http://localhost:5028/Pokemon';
  constructor(private http: HttpClient) {}

  getAllPokemon(): Observable<Pokemon[]> {
    return this.http.get<Pokemon[]>(this.api);
  }

  searchPokemon(queryString: string): Observable<SearchResultPokemon[]> {
    return this.http.get<SearchResultPokemon[]>(
      this.api + '/search/' + queryString
    );
  }

  getPokemonById(id: number): Observable<Pokemon> {
    return this.http.get<Pokemon>(this.api + '/' + id);
  }

  getEvolutionByChainId(chainId: number): Observable<any> {
    return this.http.get<Evolution[]>(this.api + '/evolution/' + chainId);
  }

  getPokemonForms(id: number, pokedexNumber: number): Observable<any> {
    return this.http.get<SearchResultPokemon[]>(
      this.api + '/forms/' + id + '/' + pokedexNumber
    );
  }

  // getPokemon(queryString: string): Observable<Pokemon> {
  //     return this.http.get<Pokemon[]>(this.api + '/pokemon?q=' + queryString);
  // }
}
