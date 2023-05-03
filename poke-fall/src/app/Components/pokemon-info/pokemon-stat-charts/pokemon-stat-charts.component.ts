import { Component, Input } from '@angular/core';
import Chart from 'chart.js/auto'
import { Pokemon } from 'src/app/Entities/pokemon';

@Component({
  selector: 'app-pokemon-stat-charts',
  templateUrl: './pokemon-stat-charts.component.html',
  styleUrls: ['./pokemon-stat-charts.component.scss'],
})
export class PokemonStatChartsComponent {
  @Input()
  pokemon: Pokemon = {} as Pokemon;

  barChart: Chart = {} as Chart;
  radarChart: Chart = {} as Chart;
  totalStats = 0;
  showBarChart = true;
  showRadarChart = false;

  ngOnChanges() {
    if (this.pokemon.id){
      this.constructCharts();
    } 
  }

  constructCharts() {
    this.totalStats =
      this.pokemon.hp +
      this.pokemon.attack +
      this.pokemon.defense +
      this.pokemon.specAttack +
      this.pokemon.specDefense +
      this.pokemon.speed;

    this.createBarChart();
    this.createRadarChart();
  }

  createRadarChart() {
    let darkmode = window.matchMedia('(prefers-color-scheme: dark)');
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
        maintainAspectRatio:false,
        responsive:true,
        plugins: {
          legend: {
            display: false,
          },
        },
        scales: {
          r: {
            pointLabels: {
              color: darkmode.matches ? 'white' : 'black',
            },
            min: 0,
            beginAtZero: true,
          },
        },
        aspectRatio: 1.0,
      },
    });
  }

  createBarChart() {
    let darkmode = window.matchMedia('(prefers-color-scheme: dark)');
    this.barChart = new Chart('barChart', {
      type: 'bar', //this denotes tha type of chart

      data: {
        // values on X-Axis
        labels: [
          'HP',
          'Attack',
          'Defense',
          'Sp. Att.',
          'Sp. Def.',
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
        maintainAspectRatio:false,
        responsive:true,
        plugins: {
          legend: {
            display: false,
          },
        },
        scales: {
          x: {
            max: 255,
            beginAtZero: true,
            ticks: {
              color: darkmode.matches ? 'white' : 'black',
            },
          },
          y: {
            ticks: {
              color: darkmode.matches ? 'white' : 'black',
            },
          },
        },
        indexAxis: 'y',
        aspectRatio: 2.0,
      },
    });
  }

  toggleOnBar() {
    this.showBarChart = true;
    this.showRadarChart = false;
  }
  toggleOnRadar() {
    this.showRadarChart = true;
    this.showBarChart = false;
  }
}
