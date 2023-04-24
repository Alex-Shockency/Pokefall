import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PokemonStatChartsComponent } from './pokemon-stat-charts.component';

describe('PokemonStatChartsComponent', () => {
  let component: PokemonStatChartsComponent;
  let fixture: ComponentFixture<PokemonStatChartsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PokemonStatChartsComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PokemonStatChartsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
