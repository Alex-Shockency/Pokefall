import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { PokemonType } from './pokemonTypes';
import { TypeEffectiveness } from './typeEffectiveness';
import { Resistance, TypeChart, Weakness } from './typeChart';


@Injectable({
  providedIn: 'root',
})
export class Utilities {
  constructor(
    private router: Router,
    private typeEffectiveness: TypeEffectiveness
  ) {}

  padId(id: number): string {
    return String(id).padStart(4, '0');
  }

  capitalizeFirstLetter(word: string): string {
    return word.charAt(0).toUpperCase() + word.slice(1);
  }

  navigateToPokemon(pokemonId: number) {
    this.router.navigate(['pokemon'], { queryParams: { id: pokemonId } });
  }

  genRandPokeId() {
    return Math.floor(Math.random() * 1010);
  }

  getResistances(type1: PokemonType, type2?: PokemonType): PokemonType[] {
    const resistances: PokemonType[] = [];
    const combinedTypes = [type1, type2].filter((type) => type);

    if (combinedTypes.length === 0) {
      throw new Error('At least one type must be provided');
    }

    for (const type of combinedTypes) {
      if (type != undefined) {
        const typeResistance =
          this.typeEffectiveness.getTypeResistances()[type];
        if (typeResistance) {
          resistances.push(...typeResistance);
        }
      }
    }

    return resistances;
  }

  getWeaknesses(type1: PokemonType, type2?: PokemonType): PokemonType[] {
    const weaknesses: PokemonType[] = [];
    const combinedTypes = [type1, type2].filter((type) => type);

    if (combinedTypes.length === 0) {
      throw new Error('At least one type must be provided');
    }

    for (const type of combinedTypes) {
      if (type != undefined) {
        const typeWeakness = this.typeEffectiveness.getTypeWeaknesses()[type];
        if (typeWeakness) {
          weaknesses.push(...typeWeakness);
        }
      }
    }

    return weaknesses;
  }

  getImmunities(type1: PokemonType, type2?: PokemonType): PokemonType[] {
    const immunities: PokemonType[] = [];
    const combinedTypes = [type1, type2].filter((type) => type);

    if (combinedTypes.length === 0) {
      throw new Error('At least one type must be provided');
    }

    for (const type of combinedTypes) {
      if (type != undefined) {
        const typeImmunity = this.typeEffectiveness.getTypeImmunities()[type];
        if (typeImmunity) {
          immunities.push(...typeImmunity);
        }
      }
    }

    return immunities;
  }

  getNormal(type1: PokemonType, type2?: PokemonType): PokemonType[] {
    const normal: PokemonType[] = [];
    const combinedTypes = [type1, type2].filter((type) => type);

    if (combinedTypes.length === 0) {
      throw new Error('At least one type must be provided');
    }

    for (const type of combinedTypes) {
      if (type != undefined) {
        const typeNormal = this.typeEffectiveness.getTypeNormals()[type];
        if (typeNormal) {
          normal.push(...typeNormal);
        }
      }
    }

    return Array.from(new Set(normal));
  }

  getTypeEffectiveness(type1: PokemonType, type2?: PokemonType) {
    let immunities: PokemonType[] = this.getImmunities(type1, type2);
    let weaknesses: PokemonType[] = this.getWeaknesses(type1, type2);
    let resistances: PokemonType[] = this.getResistances(type1, type2);
    let normals: PokemonType[] = this.getNormal(type1, type2);

    let typeWeaknessesSeverity: Weakness[] = [];
    let typeResistanceSeverity: Resistance[] = [];
    let tempNormals: PokemonType[] = structuredClone(normals);

    tempNormals.forEach((normal) => {
      let tempResistance = resistances.find((resist) => resist == normal);
      let tempWeakness = weaknesses.find((weakness) => weakness == normal);
      let tempImmunity = immunities.find((immunity) => immunity == normal);
      if (!tempResistance || !tempWeakness) {
        if (tempResistance) {
          normals.splice(normals.indexOf(tempResistance), 1);
        }
        if (tempWeakness) {
          normals.splice(normals.indexOf(tempWeakness), 1);
        }
      }

      if (tempImmunity) {
        normals.splice(normals.indexOf(tempImmunity), 1);
      }
    });

    weaknesses.forEach((weakness) => {
      let tempResistance = resistances.find((resist) => resist == weakness);
      let tempImmunity = immunities.find((immunity) => immunity == weakness);
      let tempDuplicate = typeWeaknessesSeverity.find(
        (weak) => weak.type == weakness
      );
      if (tempResistance && !tempImmunity) {
        normals.push(weakness);
      } else if (tempDuplicate && !tempImmunity) {
        typeWeaknessesSeverity.splice(
          typeWeaknessesSeverity.indexOf(tempDuplicate),
          1
        );
        typeWeaknessesSeverity.push({ type: weakness, severity: 4 });
      } else if(!tempImmunity){
        typeWeaknessesSeverity.push({ type: weakness, severity: 2 });
      }
    });

    resistances.forEach((resistance) => {
      let tempNormal = normals.find((normal) => normal == resistance);
      let tempImmunity = immunities.find((immunity) => immunity == resistance);
      let tempDuplicate = typeResistanceSeverity.find(
        (resist) => resist.type == resistance
      );
      if (tempDuplicate && !tempImmunity) {
        typeResistanceSeverity.splice(
          typeResistanceSeverity.indexOf(tempDuplicate),
          1
        );
        typeResistanceSeverity.push({ type: resistance, severity: 1 / 4 });
      } else if (!tempNormal && !tempImmunity) {
        typeResistanceSeverity.push({ type: resistance, severity: 1 / 2 });
      }
    });

    let typeChart:TypeChart = {
        typeResistances: typeResistanceSeverity,
        typeWeaknesses: typeWeaknessesSeverity,
        typeImmunities: immunities,
        typeNormals: normals,
    }

    return typeChart;
  }
}
