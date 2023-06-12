import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { getBaseUrl } from '../../main';
import { User } from '../model/user';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private http: HttpClient) { }

  getUser(user: User): Observable<any> {
    return this.http.post(getBaseUrl() + 'user/' + user.id, user, { observe: 'response' });
  }

  getCurrentUser(): Observable<any> {
    return this.http.get(getBaseUrl() + 'auth/current-user');
  }

  getUserStatistics(id: number): Observable<any> {
    return this.http.get(getBaseUrl() + 'user/' + id + '/statistics');
  }

  getItemsInInventory(): Observable<any> {
    return this.http.get(getBaseUrl() + 'item/items-in-inventory');
  }

  getUserImage(user: User) {
    const url = getBaseUrl() + "img/user/" + user.name;
    return this.http.get(url, { responseType: 'arraybuffer' });
  }
}
