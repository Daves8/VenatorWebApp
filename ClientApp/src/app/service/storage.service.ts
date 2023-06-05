import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { BehaviorSubject } from 'rxjs';
import { User } from '../model/user';

@Injectable({
  providedIn: 'root'
})
export class StorageService {

  private isLogged = new BehaviorSubject<boolean>(false);
  public isLogged$ = this.isLogged.asObservable();

  constructor(private http: HttpClient, private router: Router) {

    this.isLogged.next(localStorage.getItem('token') !== null);
  }

  //checkToken(): boolean { }

  isAuthorized(): boolean {
    return this.isLogged.getValue();
  }

  processLogin(user: User, token: string): void {
    if (token && token.trim() !== '') localStorage.setItem('token', token);
    if (user.id != null && user.id !== 0) localStorage.setItem('user_id', user.id.toString());
    if (user.name && user.name.trim() !== '') localStorage.setItem('user_name', user.name);
    if (user.fullName && user.fullName.trim() !== '') localStorage.setItem('user_full_name', user.fullName);
    if (user.email && user.email.trim() !== '') localStorage.setItem('user_email', user.email);
    if (user.phoneNumber && user.phoneNumber.trim() !== '') localStorage.setItem('user_phone_number', user.phoneNumber);
    if (user.role != null) localStorage.setItem('user_role', user.role.toString());
    this.isLogged.next(true);
    this.router.navigate(['/']);
  }

  processLogout(): void {
    this.isLogged.next(false);
    localStorage.removeItem('token');
    localStorage.removeItem('user_id');
    localStorage.removeItem('user_name');
    localStorage.removeItem('user_full_name');
    localStorage.removeItem('user_email');
    localStorage.removeItem('user_phone_number');
    localStorage.removeItem('user_role');
    this.router.navigate(['/']);
  }

  getToken(): string {
    return localStorage.getItem('token')!;
  }

  getUserId(): number {
    return parseInt(localStorage.getItem('user_id')!);
  }

  getUserName(): string {
    return localStorage.getItem('user_name')!;
  }

  getUserFullName(): string {
    return localStorage.getItem('user_full_name')!;
  }

  getUserEmail(): string {
    return localStorage.getItem('user_email')!;
  }

  getUserPhoneNumber(): string {
    return localStorage.getItem('user_phone_number')!;
  }

  getUserRole(): number {
    return parseInt(localStorage.getItem('user_role')!);
  }
}
