import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { PokemonService } from 'src/app/Services/pokemon-service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent {

  searchString: string = '';

  constructor(private pokemonService: PokemonService, private router:Router) {
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
