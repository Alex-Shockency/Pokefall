import { Ability } from "./ability";

export interface Pokemon {
    id:number;
    evolutionChainId:number;
    pokedexNumber:number;
    name:string;
    type1:string;
    type2:string;
    ability1:Ability;
    ability2:Ability;
    hiddenAbility:Ability;
    // Stored as cms and converted later
    height:number;
    // Stored as gs and converted later
    weight:number;
    baseEXPYield:number;
    hp:number;
    attack:number;
    defense:number;
    specAttack:number;
    specDefense:number;
    speed:number;
}