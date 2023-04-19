import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Pokemon } from 'src/app/Entities/pokemon';
import { PokemonService } from 'src/app/Services/pokemon-service';

@Component({
  selector: 'app-pokemon-info',
  templateUrl: './pokemon-info.component.html',
  styleUrls: ['./pokemon-info.component.scss']
})
export class PokemonInfoComponent {
  pokemonId = 0;
  pokemon!: Pokemon;

  constructor(private pokemonService: PokemonService, private route: ActivatedRoute,) {
  }

  ngOnInit() {
    this.route.queryParams.subscribe(params => {
      this.pokemonId = params['id'];
      this.pokemonService.getPokemonById(this.pokemonId).subscribe(pokemon =>{
        this.pokemon = pokemon
      })
    });
  }

  padId(id: number): string {
    return String(id).padStart(4, '0');
  }

}
