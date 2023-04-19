import { Component, ElementRef, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Pokemon } from 'src/app/Entities/pokemon';
import { PokemonService } from 'src/app/Services/pokemon-service';
import Chart from 'chart.js/auto';
import { MatAccordion } from '@angular/material/expansion';

@Component({
  selector: 'app-pokemon-info',
  templateUrl: './pokemon-info.component.html',
  styleUrls: ['./pokemon-info.component.scss'],
})
export class PokemonInfoComponent {
  @ViewChild(MatAccordion)
  accordion!: MatAccordion;

  pokemonId = 0;
  pokemon!: Pokemon;
  barChart: any;
  radarChart: any;
  showBarChart = true;
  showRadarChart = false;
  audioUrl = '';

  pokeCry!: HTMLMediaElement;

  constructor(
    private pokemonService: PokemonService,
    private route: ActivatedRoute
  ) {}

  ngOnInit() {
    window.scrollTo(0, 0);
    this.route.queryParams.subscribe((params) => {
      this.pokemonId = params['id'];
      this.pokemonService
        .getPokemonById(this.pokemonId)
        .subscribe((pokemon) => {
          this.pokemon = pokemon;
          this.pokeCry = document.getElementById(
            'audioElement'
          ) as HTMLMediaElement;
          this.pokeCry.src =
            'https://play.pokemonshowdown.com/audio/cries/' +
            this.formatName(pokemon.name) +
            '.ogg';
          this.createBarChart();
          this.createRadarChart();
        });
    });
  }

  createRadarChart() {
    this.radarChart = new Chart('radarChart', {
      type: 'radar', //this denotes tha type of chart

      data: {
        // values on X-Axis
        labels: [
          'HP',
          'Attack',
          'Defense',
          'Speed',
          'Sp. Defense',
          'Sp. Attack',
        ],
        datasets: [
          {
            data: [
              this.pokemon.hp,
              this.pokemon.attack,
              this.pokemon.defense,
              this.pokemon.speed,
              this.pokemon.specDefense,
              this.pokemon.specAttack,
            ],
            backgroundColor: ['rgba(0, 137, 123, 0.2)'],
            pointBackgroundColor: [
              'rgba(255, 99, 132 )',
              'rgba(255, 159, 64 )',
              'rgba(255, 205, 86)',
              'rgba(153, 102, 255)',
              'rgba(54, 162, 235)',
              'rgba(75, 192, 192)',
            ],
            pointBorderColor: '#fff',
            pointHoverBackgroundColor: '#fff',
            pointHoverBorderColor: 'rgb(255, 99, 132)',
          },
        ],
      },
      options: {
        plugins: {
          legend: {
            display: false,
          },
        },
        scales: {
          r: {
            min: 0,
            beginAtZero: true,
          },
        },
        aspectRatio: 2.5,
      },
    });
  }

  createBarChart() {
    this.barChart = new Chart('barChart', {
      type: 'bar', //this denotes tha type of chart

      data: {
        // values on X-Axis
        labels: [
          'HP',
          'Attack',
          'Defense',
          'Sp. Attack',
          'Sp. Defense',
          'Speed',
        ],
        datasets: [
          {
            data: [
              this.pokemon.hp,
              this.pokemon.attack,
              this.pokemon.defense,
              this.pokemon.specAttack,
              this.pokemon.specDefense,
              this.pokemon.speed,
            ],
            backgroundColor: [
              'rgba(255, 99, 132 )',
              'rgba(255, 159, 64 )',
              'rgba(255, 205, 86)',
              'rgba(75, 192, 192)',
              'rgba(54, 162, 235)',
              'rgba(153, 102, 255)',
            ],
          },
        ],
      },
      options: {
        plugins: {
          legend: {
            display: false,
          },
        },
        scales: {
          x: {
            max: 255,
            beginAtZero: true,
          },
        },
        indexAxis: 'y',
        aspectRatio: 2.5,
      },
    });
  }
  protected playCry(id: number) {
    this.pokeCry.play();
  }

  toggleOnBar() {
    this.showBarChart = true;
    this.showRadarChart = false;
  }
  toggleOnRadar(){
    this.showRadarChart =true;
    this.showBarChart = false;
  }

  formatName(name: string) {
    return name.toLowerCase();
  }

  padId(id: number): string {
    return String(id).padStart(4, '0');
  }
}
