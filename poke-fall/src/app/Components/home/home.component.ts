import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { Pokemon } from 'src/app/Entities/pokemon';
import { PokemonService } from 'src/app/Services/pokemon-service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent {
  randomPoke!: Pokemon;
  searchString: string = '';

  constructor(private pokemonService: PokemonService, private router:Router) {
  }

  ngOnInit(){
    this.genRandPoke();
  }

  async randomSearch(): Promise<void>{
      this.router.navigate(
        ['pokemon'],
        { queryParams: { id:  Math.floor(Math.random()*1010)} }
      )
  }  

  async advancedSearch(): Promise<void>{
    this.router.navigate(['advancedSearch'], { queryParams: {} });
  } 

  async search(): Promise<void> {
    this.router.navigate(
      ['search'],
      { queryParams: { q: this.searchString.toLocaleLowerCase(), gd:true }}
    ).then(()=>{
      window.location.reload();
    });
  }

  navigateToPokemon(pokemonId: number){
    this.router.navigate(
      ['pokemon'],
      { queryParams: { id: pokemonId } }
    )
  }

  genRandPoke() {
    this.pokemonService
        .getPokemonById( Math.floor(Math.random() * 1010))
        .subscribe((pokemon) => {
          this.randomPoke = pokemon;
        });
  }
}
