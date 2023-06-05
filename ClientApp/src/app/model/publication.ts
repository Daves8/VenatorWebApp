import { Textual } from "./textual";

export class Publication extends Textual{
  metrics!: string;
  likesCount!: number;
  dislikesCount!: number;
  currentLike!: boolean;
  currentDislike!: boolean;
}
