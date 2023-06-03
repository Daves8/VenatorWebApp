import { HttpResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { User } from '../model/user';
import { AuthService } from '../service/auth.service';
import { HashService } from '../service/hash.service';
import { StorageService } from '../service/storage.service';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css']
})
export class RegistrationComponent implements OnInit {

  user: User = new User();
  error: string = '';

  constructor(private authService: AuthService, private storageService: StorageService, private hashService: HashService) { }

  ngOnInit(): void { }

  onSubmit(form: NgForm) {
    const copiedUser: User = { ...this.user };
    copiedUser.password = this.hashService.hashPassword(copiedUser.password);
    this.authService.registration(copiedUser).subscribe(
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
