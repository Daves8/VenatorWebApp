import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { LoginComponent } from './login/login.component';
import { RegistrationComponent } from './registration/registration.component';
import { TestComponent } from './test/test.component';
import { TokenInterceptor } from './service/token-interceptor.service';
import { ProfileComponent } from './profile/profile.component';
import { StoreComponent } from './store/store.component';
import { CartComponent } from './cart/cart.component';
import { RecommendComponent } from './recommend/recommend.component';
import { MessageComponent } from './message/message.component';
import { NewsComponent } from './news/news.component';
import { ForumComponent } from './forum/forum.component';
import { ItemComponent } from './item/item.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    LoginComponent,
    RegistrationComponent,
    ProfileComponent,
    StoreComponent,
    CartComponent,
    RecommendComponent,
    MessageComponent,
    NewsComponent,
    ForumComponent,
    TestComponent,
    ItemComponent,
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'counter', component: CounterComponent },
      { path: 'fetch-data', component: FetchDataComponent },
      { path: 'login', component: LoginComponent },
      { path: 'registration', component: RegistrationComponent },
      { path: 'profile', component: ProfileComponent },
      { path: 'store', component: StoreComponent },
      { path: 'item/:id', component: ItemComponent },
      { path: 'news', component: NewsComponent },
      { path: 'news/:id', component: NewsComponent },
      { path: 'forum', component: ForumComponent },
      { path: 'forum/:id', component: ForumComponent },
      { path: 'message', component: MessageComponent },
      { path: 'message/:id', component: MessageComponent },
      { path: 'cart', component: CartComponent },
      { path: 'recommendations', component: RecommendComponent },
      { path: 'test', component: TestComponent },
    ])
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: TokenInterceptor, multi: true }],
  bootstrap: [AppComponent]
})
export class AppModule { }
