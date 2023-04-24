export interface Evolution {
    id: number;
    pokedexNumber: number;
    evolvedFromPokedexNumber: number;
    evolutionTrigger: string;
    triggerItem: string;
    minLevel: string
    gender: string;
    location: string;
    heldItem: string;
    timeOfDay: string;
    knownMoveId: string;
    knownMoveType: string;
    minHappiness: string;
    minBeauty: string;
    minAffection: string;
    relativePhysicalStats: string;
    partyPokedexNumber: string;
    partyType: string;
    tradePokedexNumber: string;
    needsOverworldRain: string;
    turnUpsideDown: string;
}