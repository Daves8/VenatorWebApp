import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Observable } from 'rxjs';
import { getBaseUrl } from '../../main';
import { User } from '../model/user';
import { StorageService } from '../service/storage.service';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css']
})
export class RegistrationComponent implements OnInit {

  user: User = new User();
  error: string = '';

  constructor(private http: HttpClient, private storageService: StorageService) { }

  ngOnInit(): void { }

  onSubmit(form: NgForm) {
    this.registration(this.user).subscribe(
      (response: any) => {
        this.storageService.processLogin(response.user, response.token);
      },
      error => {
        this.error = error.error;
      }
    );
  }

  registration(user: User): Observable<any> {
    return this.http.post(getBaseUrl() + 'auth/registration', user);
  }
}
