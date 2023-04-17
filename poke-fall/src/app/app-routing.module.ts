import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { SearchResultsComponent } from './Components/search/search-results/search-results.component';
import { SearchComponent } from './Components/search/search.component';

const routes: Routes = [{ path: 'search', component: SearchComponent }];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
