import { Component } from '@angular/core';
import {
  FormArray,
  FormBuilder,
  FormControl,
  FormGroup,
  Validators,
} from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-advanced-search',
  templateUrl: './advanced-search.component.html',
  styleUrls: ['./advanced-search.component.scss'],
})
export class AdvancedSearchComponent {
  formCount = 1;
  myForm = this.formBuilder.group({
    pokemonName: '',
    ability: '',
    types: this.formBuilder.control([]),
    stats: this.formBuilder.array([
      this.formBuilder.group({
        statType0: this.formBuilder.control('Total'),
        statMode0: this.formBuilder.control('equal to'),
        statValue0: '',
      }),
    ]),
  });

  constructor(private router: Router, private formBuilder: FormBuilder) {}

  ngOnInit() {}

  typeList: string[] = [
    'Normal',
    'Fire',
    'Water',
    'Electric',
    'Grass',
    'Ice',
    'Fighting',
    'Poison',
    'Ground',
    'Flying',
    'Psychic',
    'Bug',
    'Rock',
    'Ghost',
    'Dragon',
    'Dark',
    'Steel',
    'Fairy',
  ];

  statTypes: string[] = [
    'Total',
    'HP',
    'Attack',
    'Defense',
    'Spec. Attack',
    'Spec. Defense',
    'Speed',
  ];

  statModes: string[] = [
    'equal to',
    'less than',
    'greater than',
    'less than or equal to',
    'greater than or equal to',
  ];

  get stats(): FormArray {
    return this.myForm.get('stats') as FormArray;
  }

  addField() {
    const newFieldGroup = this.formBuilder.group({});
    newFieldGroup.addControl(
      'statType' + this.formCount,
      this.formBuilder.control('')
    );
    newFieldGroup.addControl(
      'statMode' + this.formCount,
      this.formBuilder.control('')
    );
    newFieldGroup.addControl(
      'statValue' + this.formCount,
      this.formBuilder.control('')
    );
    this.stats.push(newFieldGroup);
    this.formCount++;
  }

  onSubmit(): void {
    let queryString = '';
    if (
      this.myForm.value.pokemonName &&
      this.myForm.value.pokemonName.length > 0
    ) {
      queryString += this.myForm.value.pokemonName;
    }
    if (this.myForm.value.ability && this.myForm.value.ability.length > 0) {
      queryString += ' a:' + this.myForm.value.ability;
    }
    if (this.myForm.value.types && this.myForm.value.types.length > 0) {
      this.myForm.value.types.forEach((type) => {
        queryString += ' t:' + type;
      });
    }
    if (this.myForm.value.stats && this.myForm.value.stats.length > 0) {
      let index = 0;
      this.myForm.value.stats.forEach((stat: any) => {
        let statType = stat['statType' + index];
        let statMode = stat['statMode' + index];
        let statValue = stat['statValue' + index];

        if (statType == 'Total' && statValue) {
          if (statMode == 'equal to') {
            queryString += ' bst=' + statValue;
          } else if (statMode == 'less than') {
            queryString += ' bst<' + statValue;
          } else if (statMode == 'greater than') {
            queryString += ' bst>' + statValue;
          } else if (statMode == 'less than or equal to') {
            queryString += ' bst<=' + statValue;
          } else if (statMode == 'greater than or equal to') {
            queryString += ' bst>=' + statValue;
          }
        }

        if (statType == 'HP' && statValue) {
          if (statMode == 'equal to') {
            queryString += ' hp=' + statValue;
          } else if (statMode == 'less than') {
            queryString += ' hp<' + statValue;
          } else if (statMode == 'greater than') {
            queryString += ' hp>' + statValue;
          } else if (statMode == 'less than or equal to') {
            queryString += ' hp<=' + statValue;
          } else if (statMode == 'greater than or equal to') {
            queryString += ' hp>=' + statValue;
          }
        }

        if (statType == 'Attack' && statValue) {
          if (statMode == 'equal to') {
            queryString += ' atk=' + statValue;
          } else if (statMode == 'less than') {
            queryString += ' atk<' + statValue;
          } else if (statMode == 'greater than') {
            queryString += ' atk>' + statValue;
          } else if (statMode == 'less than or equal to') {
            queryString += ' atk<=' + statValue;
          } else if (statMode == 'greater than or equal to') {
            queryString += ' atk>=' + statValue;
          }
        }

        if (statType == 'Defense' && statValue) {
          if (statMode == 'equal to') {
            queryString += ' def=' + statValue;
          } else if (statMode == 'less than') {
            queryString += ' def<' + statValue;
          } else if (statMode == 'greater than') {
            queryString += ' def>' + statValue;
          } else if (statMode == 'less than or equal to') {
            queryString += ' def<=' + statValue;
          } else if (statMode == 'greater than or equal to') {
            queryString += ' def>=' + statValue;
          }
        }

        if (statType == 'Spec. Attack' && statValue) {
          if (statMode == 'equal to') {
            queryString += ' spa=' + statValue;
          } else if (statMode == 'less than') {
            queryString += ' spa<' + statValue;
          } else if (statMode == 'greater than') {
            queryString += ' spa>' + statValue;
          } else if (statMode == 'less than or equal to') {
            queryString += ' spa<=' + statValue;
          } else if (statMode == 'greater than or equal to') {
            queryString += ' spa>=' + statValue;
          }
        }

        if (statType == 'Spec. Defense' && statValue) {
          if (statMode == 'equal to') {
            queryString += ' spdef=' + statValue;
          } else if (statMode == 'less than') {
            queryString += ' spdef<' + statValue;
          } else if (statMode == 'greater than') {
            queryString += ' spdef>' + statValue;
          } else if (statMode == 'less than or equal to') {
            queryString += ' spdef<=' + statValue;
          } else if (statMode == 'greater than or equal to') {
            queryString += ' spdef>=' + statValue;
          }
        }

        if (statType == 'Spec. Speed' && statValue) {
          if (statMode == 'equal to') {
            queryString += ' spd=' + statValue;
          } else if (statMode == 'less than') {
            queryString += ' spd<' + statValue;
          } else if (statMode == 'greater than') {
            queryString += ' spd>' + statValue;
          } else if (statMode == 'less than or equal to') {
            queryString += ' spd<=' + statValue;
          } else if (statMode == 'greater than or equal to') {
            queryString += ' spd>=' + statValue;
          }
        }

        index++;
      });
    }
    this.search(queryString.trim());
  }

  async search(queryString: string): Promise<void> {
    this.router
      .navigate(['../search'], {
        queryParams: { q: queryString.toLocaleLowerCase(), gd: true },
      })
      .then(() => {
        window.location.reload();
      });
  }
}
