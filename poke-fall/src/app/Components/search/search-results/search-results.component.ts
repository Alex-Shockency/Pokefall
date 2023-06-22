import {
  AfterViewInit,
  Component,
  HostListener,
  Input,
  SimpleChanges,
  ViewChild,
} from '@angular/core';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { Output, EventEmitter } from '@angular/core';
import { Pokemon } from 'src/app/Entities/pokemon';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { MatTableDataSource } from '@angular/material/table';
import { MatSort } from '@angular/material/sort';
import { ActivatedRoute, Router } from '@angular/router';
import { PokemonService } from 'src/app/Services/pokemon-service';
import { SearchResultPokemon } from 'src/app/Entities/searchResultPokemon';
import { Utilities } from 'src/app/Shared/utilities';

export interface Tile {
  color: string;
  cols: number;
  rows: number;
  text: string;
}

@Component({
  selector: 'app-search-results',
  templateUrl: './search-results.component.html',
  styleUrls: ['./search-results.component.scss'],
})
export class SearchResultsComponent implements AfterViewInit {
  @Input() searchString: string = '';
  @Input() searchResults: SearchResultPokemon[] = [];
  @Input() searchLoading: boolean;
  @Input() gridDisplay:boolean;

  randomId = this.utilities.genRandPokeId();
  columnNum = 4;
  screenHeight = 0;
  screenWidth = 0;

  dataSource = new MatTableDataSource<SearchResultPokemon>();
  displayedColumns: string[] = [
    'name',
    'type1',
    'type2',
  ];

  @HostListener('window:resize', ['$event'])
  onResize() {
    this.screenHeight = window.innerHeight;
    this.screenWidth = window.innerWidth;
    if (this.screenWidth <= 1920 && this.screenWidth > 1200) {
      this.columnNum = 3;
    } else if (this.screenWidth < 1200 && this.screenWidth > 800) {
      this.columnNum = 2;
    } else if (this.screenWidth < 800) {
      this.columnNum = 1;
    } else {
      this.columnNum = 4;
    }
  }

  @ViewChild(MatPaginator, { static: false })
  set paginator(value: MatPaginator) {
    this.dataSource.paginator = value;
  }

  @ViewChild(MatSort, { static: false })
  set sort(value: MatSort) {
    this.dataSource.sort = value;
  }

  imageUrl = '';
  panelOpenState = false;

  hidePageSize = false;
  showPageSizeOptions = true;
  showFirstLastButtons = true;
  disabled = false;

  ngOnChanges() {
    this.dataSource = new MatTableDataSource<SearchResultPokemon>(this.searchResults);
  }

  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

  constructor(private router:Router,protected utilities:Utilities) {
    this.searchLoading = false;
    this.gridDisplay = true;
    this.onResize();
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }

  setAlternateImage(id: number, dexNum: number) {
    document
      .getElementById(id.toString())
      ?.setAttribute(
        'src',
        'https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/other/official-artwork/' +
          dexNum +
          '.png'
      );
  }

  displayGrid(display: boolean) {
    this.router.navigate(
      ['search'],
      { queryParams: { q: this.searchString, gd: display } }
    )
  }

  async advancedSearch(): Promise<void>{
    this.router.navigate(['advancedSearch'], { queryParams: {} });
  } 

  protected playCry(id: number) {
    let audio: HTMLAudioElement = <HTMLAudioElement>(
      document.getElementById('audio-' + id.toString())
    );
    audio.play();
  }
}
