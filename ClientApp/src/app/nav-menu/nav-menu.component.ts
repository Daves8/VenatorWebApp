import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { NavigationEnd, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { StorageService } from '../service/storage.service';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {

  isExpanded = false;

  username!: string;
  isAuthorized!: boolean;

  constructor(private storageService: StorageService, private http: HttpClient, private router: Router, private titleService: Title) {
    // checkToken()
    this.storageService.isLogged$.subscribe((value) => {
      this.isAuthorized = value;
      if (this.isAuthorized) {
        const fullname = storageService.getUserFullName() ? ' (' + storageService.getUserFullName() + ')' : '';
        this.username = storageService.getUserName() + fullname;
      } else {
        this.username = '';
      }
    });

    this.router.events.subscribe((event) => {
      if (event instanceof NavigationEnd) {
        switch (event.url.split(/[/?]/)[1]) {

          case 'login':
            this.getNamesConfig().subscribe((config: any) => {
              this.titleService.setTitle(config.site_name + ' – ' + config.page_names.login);
            }); break;
          case 'registration':
            this.getNamesConfig().subscribe((config: any) => {
              this.titleService.setTitle(config.site_name + ' – ' + config.page_names.registration);
            }); break;

          case 'profile':
            this.getNamesConfig().subscribe((config: any) => {
              this.titleService.setTitle(config.site_name + ' – ' + config.page_names.profile);
            }); break;

          case 'store':
            this.getNamesConfig().subscribe((config: any) => {
              this.titleService.setTitle(config.site_name + ' – ' + config.page_names.store);
            }); break;
          case 'item':
            this.getNamesConfig().subscribe((config: any) => {
              this.titleService.setTitle(config.site_name + ' – ' + config.page_names.item);
            }); break;

          case 'news':
            this.getNamesConfig().subscribe((config: any) => {
              this.titleService.setTitle(config.site_name + ' – ' + config.page_names.news);
            }); break;
          case 'forum':
            this.getNamesConfig().subscribe((config: any) => {
              this.titleService.setTitle(config.site_name + ' – ' + config.page_names.forum);
            }); break;
          case 'messages':
            this.getNamesConfig().subscribe((config: any) => {
              this.titleService.setTitle(config.site_name + ' – ' + config.page_names.messages);
            }); break;

          case 'cart':
            this.getNamesConfig().subscribe((config: any) => {
              this.titleService.setTitle(config.site_name + ' – ' + config.page_names.cart);
            }); break;
          case 'recommendations':
            this.getNamesConfig().subscribe((config: any) => {
              this.titleService.setTitle(config.site_name + ' – ' + config.page_names.recommendations);
            }); break;

          default:
            this.getNamesConfig().subscribe((config: any) => {
              this.titleService.setTitle(config.site_name);
            }); break;
        }
      }
    });
  }

  getNamesConfig(): Observable<any> {
    return this.http.get('/assets/names_config.json');
  }

  logout(): void {
    this.storageService.processLogout();
  }

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }
}
