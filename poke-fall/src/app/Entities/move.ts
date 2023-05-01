export interface Move {
  pokemonId: number;
  moveId: number;
  versionId:number;
  levelUpLearnable: boolean;
  levelLearned: number;
  tmLearnable: boolean;
  tutorLearnable: boolean;
  eggMoveLearnable: boolean;
  name: string;
  description: string;
  type: string;
  category: string;
  pp: number;
  power: number;
  accuracy: number;
  contact: boolean;
  affectedProtect: boolean;
  affectedSnatch: boolean;
  affectedMirrorMove: boolean;
}
