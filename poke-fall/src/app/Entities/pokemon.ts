export interface Pokemon {
    id:number;
    evolutionChainId:number;
    pokedexNumber:number;
    name:string;
    type1:string;
    type2:string;
    ability1Id:number;
    ability2Id:number;
    hiddenAbilityId:number;
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