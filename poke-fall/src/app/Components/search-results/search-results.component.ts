import { AfterViewInit, Component, Input, SimpleChanges, ViewChild } from '@angular/core';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { Output, EventEmitter } from '@angular/core';
import { Pokemon } from 'src/app/Entities/pokemon';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { MatTableDataSource } from '@angular/material/table';
import { MatSort } from '@angular/material/sort';

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
export class SearchResultsComponent implements AfterViewInit{
  @Input() searchResults: Pokemon[] = [];
  @Input() searchLoading: boolean; 

  displayedColumns: string[] = ['name', 'type1','type2','hp','defense','specAttack','specDefense','speed'];
  dataSource = new MatTableDataSource<Pokemon>();

  @ViewChild(MatPaginator, {static: false})
  set paginator(value: MatPaginator) {
    this.dataSource.paginator = value;
  }

  @ViewChild(MatSort, {static: false})
  set sort(value: MatSort) {
    this.dataSource.sort = value;
  }

  imageUrl = '';
  panelOpenState = false;

  hidePageSize = false;
  showPageSizeOptions = true;
  showFirstLastButtons = true;
  disabled = false;
  gridDisplay = true;

  ngOnChanges(){
    this.dataSource = new MatTableDataSource<Pokemon>(this.searchResults);
  }

  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

  constructor(private http: HttpClient) {
    this.searchLoading = false;
  }

  padId(id: number): string {
    return String(id).padStart(4, '0');
  }

  setAlternateImage(id: number, dexNum:number) {
    document
      .getElementById(id.toString())
      ?.setAttribute(
        'src',
        'https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/other/official-artwork/' + dexNum +
          '.png'
      );
  }

  capitalizeFirstLetter(word: string): string {
    return word.charAt(0).toUpperCase() + word.slice(1);
  }

  displayGrid(display:boolean){
    this.gridDisplay = display;
  }
}
