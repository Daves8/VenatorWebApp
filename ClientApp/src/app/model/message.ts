import { Textual } from "./textual";
import { User } from "./user";

export class Message extends Textual {
  toUser!: User;
}
