import { Injectable } from '@angular/core';
import * as CryptoJS from 'crypto-js';

@Injectable({
  providedIn: 'root'
})
export class HashService {

  constructor() { }

  hashPassword(password: string): string {
    const hashedPassword = CryptoJS.SHA256(password).toString();
    return hashedPassword;
  }
}
