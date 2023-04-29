import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { SearchResultsComponent } from './Components/search/search-results/search-results.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MaterialModule } from './Shared/material.module';
import { FormsModule } from '@angular/forms';
import { NgxJsonViewerModule } from 'ngx-json-viewer';
import { SearchBarComponent } from './Components/search/search-bar/search-bar.component';
import { HomeComponent } from './Components/home/home.component';
import { SearchComponent } from './Components/search/search.component';
import { PokemonInfoComponent } from './Components/pokemon-info/pokemon-info.component';
import { PokemonStatChartsComponent } from './Components/pokemon-info/pokemon-stat-charts/pokemon-stat-charts.component';
import { TypeEffectiveness } from './Shared/typeEffectiveness';

@NgModule({
  declarations: [
    AppComponent,
    SearchResultsComponent,
    SearchBarComponent,
    HomeComponent,
    SearchComponent,
    PokemonInfoComponent,
    PokemonStatChartsComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    MaterialModule,
    FormsModule,
    NgxJsonViewerModule,
    BrowserAnimationsModule,
    HttpClientModule,
  ],
  providers: [TypeEffectiveness],
  bootstrap: [AppComponent],
})
export class AppModule {}
