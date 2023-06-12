import { SafeUrl } from "@angular/platform-browser";
import { Role } from "./enum/role";
import { Login } from "./login";

export class User extends Login {
  fullName!: string;
  email!: string;
  phoneNumber!: string;
  imageUrl!: string;
  imageSafeUrl!: SafeUrl;
  role!: Role;
  goldAmount!: number;
}
