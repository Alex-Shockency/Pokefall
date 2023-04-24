import { Component, Input } from '@angular/core';
import { Router } from '@angular/router';
import { Pokemon } from 'src/app/Entities/pokemon';
import { PokemonService } from 'src/app/Services/pokemon-service';

@Component({
  selector: 'app-search-bar',
  templateUrl: './search-bar.component.html',
  styleUrls: ['./search-bar.component.scss']
})

export class SearchBarComponent {
  @Input() searchString: string = '';

  title = 'poke-fall';
  queryString: string = '';

  constructor(private pokemonService: PokemonService, private router:Router) {
  }
  
  async randomSearch(): Promise<void>{
      this.router.navigate(
        ['../pokemon'],
        { queryParams: { id:  Math.floor(Math.random()*1010) } }
      ).then(()=>{
        window.location.reload();
      });
  }

  async search(): Promise<void> {
   
    this.router.navigate(
      ['../search'],
      { queryParams: { q: this.queryString.toLocaleLowerCase(), gd:true }}
    ).then(()=>{
      if(this.searchString !== this.queryString){
        scroll(0,0)
      }
      window.location.reload();
    });
  }

  async navigateHome(): Promise<void> {
    this.router.navigate(
      ['']
    )
  }

}
