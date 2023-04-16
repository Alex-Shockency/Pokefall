import { Component, Input, SimpleChanges } from '@angular/core';
import { PageEvent } from '@angular/material/paginator';
import { Output, EventEmitter } from '@angular/core';
import { Pokemon } from 'src/app/Entities/pokemon';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';

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
  @Input() searchLoading: boolean;

  imageUrl = '';
  panelOpenState = false;

  hidePageSize = false;
  showPageSizeOptions = true;
  showFirstLastButtons = true;
  disabled = false;

  ngOnInit() {
    window.scroll({
      top: 0,
      left: 0,
      behavior: 'smooth',
    });
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
}
