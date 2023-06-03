import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { getBaseUrl } from '../../main';
import { Login } from '../model/login';
import { User } from '../model/user';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private http: HttpClient) { }

  login(login: Login): Observable<any> {
    return this.http.post(getBaseUrl() + 'auth/login', login, { observe: 'response' });
  }

  registration(user: User): Observable<any> {
    return this.http.post(getBaseUrl() + 'auth/registration', user, { observe: 'response' });
  }
}
