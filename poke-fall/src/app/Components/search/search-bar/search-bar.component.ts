import { Component, HostListener, Input } from '@angular/core';
import { Router } from '@angular/router';
import { Pokemon } from 'src/app/Entities/pokemon';
import { PokemonService } from 'src/app/Services/pokemon-service';
import { Utilities } from 'src/app/Shared/utilities';

@Component({
  selector: 'app-search-bar',
  templateUrl: './search-bar.component.html',
  styleUrls: ['./search-bar.component.scss']
})

export class SearchBarComponent {
  @Input() searchString: string = '';

  title = 'poke-fall';
  queryString: string = '';
  screenHeight = 0;
  screenWidth = 0;
  
  @HostListener('window:resize', ['$event'])
  onResize() {
    this.screenHeight = window.innerHeight;
    this.screenWidth = window.innerWidth;
  }
  constructor(private router:Router, private utilities:Utilities) {
  }
  
  ngOnInit(){
    this.screenWidth = window.innerWidth;
  }

  async randomSearch(): Promise<void>{
      this.router.navigate(
        ['../pokemon'],
        { queryParams: { id:  this.utilities.genRandPokeId() } }
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
