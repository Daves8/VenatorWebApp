import { TextualType } from "./enum/textual-type";
import { Publication } from "./publication";
import { Textual } from "./textual";

export class Comment extends Textual {
  parent!: Publication;
  parentType!: TextualType;
  likesCount!: number;
  dislikesCount!: number;
}
