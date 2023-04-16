import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Pokemon } from '../Entities/pokemon';

@Injectable({
    providedIn: 'root'
  })
  export class PokemonService {
    private api: string = 'http://localhost:5028/Pokemon';
    constructor(private http: HttpClient) {}

    getAllPokemon(): Observable<Pokemon[]>{
      return this.http.get<Pokemon[]>(this.api);
    }

    // getPokemon(queryString: string): Observable<Pokemon> {
    //     return this.http.get<Pokemon[]>(this.api + '/pokemon?q=' + queryString);
    // }

  }