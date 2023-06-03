import { HttpResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Login } from '../model/login';
import { AuthService } from '../service/auth.service';
import { HashService } from '../service/hash.service';
import { StorageService } from '../service/storage.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  user: Login = new Login();
  error: string = '';

  constructor(private authService: AuthService, private storageService: StorageService, private hashService: HashService) { }

  ngOnInit(): void { }

  onSubmit(form: NgForm) {
    const copiedUser: Login = { ...this.user };
    copiedUser.password = this.hashService.hashPassword(copiedUser.password);
    this.authService.login(copiedUser).subscribe(
      (response: HttpResponse<any>) => {
        const token = response.headers.get('Authorization')!;
        this.storageService.processLogin(response.body, token);
      },
      error => {
        this.error = error.error;
      }
    );
  }
}
