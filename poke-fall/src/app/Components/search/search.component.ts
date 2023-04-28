import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Pokemon } from 'src/app/Entities/pokemon';
import { SearchResultPokemon } from 'src/app/Entities/searchResultPokemon';
import { PokemonService } from 'src/app/Services/pokemon-service';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.scss']
})
export class SearchComponent {
  searchString = "";
  searchLoading = false;
  searchResults: SearchResultPokemon[] = [];
  gridDisplay:boolean = false;

  constructor(private pokemonService: PokemonService, private route: ActivatedRoute, private router:Router) {
    this.searchLoading = false;
  }

  ngOnInit() {
    this.route.queryParams.subscribe(params => {
      this.searchString = params['q'];
      this.gridDisplay = JSON.parse(params['gd']);
    });
    this.getPokemon();
  }

  getPokemon(){
    this.searchLoading = true;
   if (this.searchString) {
     //TODO: fix the nidoran case
     this.pokemonService.searchPokemon(this.searchString).subscribe((searchResultPokemon) => {
      //  if(filteredList.length == 1){
      //   this.router.navigate(
      //     ['../pokemon'],
      //     { queryParams: { id: filteredList.pop()?.id } }
      //   )
      //  }
       this.searchResults = searchResultPokemon;
       this.searchLoading = false;
     });
   } else {
     this.searchResults = [];
     this.searchLoading = false;
   }
 }
}
