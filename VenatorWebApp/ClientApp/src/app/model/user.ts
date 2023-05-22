import { Login } from "./login";
import { Role } from "./role";

export class User extends Login {
  id!: number;
  fullName!: string;
  email!: string;
  phoneNumber!: string;
  imageUrl!: string;
  role!: Role;
  goldAmount!: number;
  creationDate!: string;
}
