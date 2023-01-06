import { Component, Input, SimpleChanges } from '@angular/core';
import { PageEvent } from '@angular/material/paginator';
import { Pokemon, PokemonClient } from 'pokenode-ts';
import { Output, EventEmitter } from '@angular/core';

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
export class SearchResultsComponent {
  @Input() searchResults: Pokemon[] = [];
  @Input() searchLoading:boolean;

  imageUrl = '';
  panelOpenState = false;

  hidePageSize = false;
  showPageSizeOptions = true;
  showFirstLastButtons = true;
  disabled = false;

  constructor() {
    this.searchLoading = false;
  }

  padId(id:number): string {
    return String(id).padStart(3, '0');
  }

  capitalizeFirstLetter(word: string): string {
    return word.charAt(0).toUpperCase() + word.slice(1);
  }
}
