import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Observable } from 'rxjs';
import { getBaseUrl } from '../../main';
import { Login } from '../model/login';
import { StorageService } from '../service/storage.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  user: Login = new Login();
  error: string = '';

  constructor(private http: HttpClient, private storageService: StorageService) { }

  ngOnInit(): void { }

  onSubmit(form: NgForm) {
    this.login(this.user).subscribe(
      (response: any) => {
        this.storageService.processLogin(response.user, response.token);
      },
      error => {
        this.error = error.error;
      }
    );
  }

  login(login: Login): Observable<any> {
    return this.http.post(getBaseUrl() + 'auth/login', login);
  }
}
