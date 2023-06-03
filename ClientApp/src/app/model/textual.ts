import { BaseEntity } from "./base-entity";
import { User } from "./user";

export abstract class Textual extends BaseEntity {
  text!: string;
  owner!: User;
  isHidden!: boolean;
}
