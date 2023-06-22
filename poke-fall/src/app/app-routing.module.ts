import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { SearchResultsComponent } from './Components/search/search-results/search-results.component';
import { SearchComponent } from './Components/search/search.component';
import { HomeComponent } from './Components/home/home.component';
import { PokemonInfoComponent } from './Components/pokemon-info/pokemon-info.component';
import { AdvancedSearchComponent } from './Components/advanced-search/advanced-search.component';

const routes: Routes = [
  { path: 'search', component: SearchComponent },
  { path: '', component: HomeComponent },
  { path: 'pokemon', component: PokemonInfoComponent },
  { path: 'advancedSearch', component: AdvancedSearchComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
