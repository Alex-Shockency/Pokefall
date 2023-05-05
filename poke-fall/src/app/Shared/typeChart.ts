import { PokemonType } from './pokemonTypes';

export type Weakness = {
  type: PokemonType;
  severity: number;
};

export type Resistance = {
  type: PokemonType;
  severity: number;
};

export interface TypeChart {
  typeResistances: Resistance[];
  typeWeaknesses: Weakness[];
  typeImmunities: PokemonType[];
  typeNormals: PokemonType[];
}
