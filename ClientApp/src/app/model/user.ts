import { Role } from "./enum/role";
import { Login } from "./login";

export class User extends Login {
  fullName!: string;
  email!: string;
  phoneNumber!: string;
  imageUrl!: string;
  role!: Role;
  goldAmount!: number;
}
