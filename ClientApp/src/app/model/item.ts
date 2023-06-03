import { BaseEntity } from "./base-entity";
import { ItemCategory } from "./enum/item-category";

export class Item extends BaseEntity {
  description!: string;
  category!: ItemCategory;
  price!: number;
  isHidden!: boolean;
  imageUrl!: string;
}
