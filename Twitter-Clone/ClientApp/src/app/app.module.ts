import {BrowserModule} from '@angular/platform-browser';
import {NgModule} from '@angular/core';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {HttpClientModule} from '@angular/common/http';
import {RouterModule} from '@angular/router';
import {BrowserAnimationsModule} from "@angular/platform-browser/animations";

import {AppComponent} from './app.component';
import {NavMenuComponent} from './nav-menu/nav-menu.component';
import {LoginComponent} from './auth/login/login.component';
import {RegisterComponent} from './auth/register/register.component';
import {WelcomeComponent} from './welcome/welcome.component';
import {HomeComponent} from './home/home.component';
import {ControlBarComponent} from '../control-bar/control-bar.component';
import {TweetDisplayComponent} from './tweet-display/tweet-display.component';
import {TimeAgoPipe} from './pipes/time-ago.pipe';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    LoginComponent,
    RegisterComponent,
    WelcomeComponent,
    HomeComponent,
    ControlBarComponent,
    TweetDisplayComponent,
    TimeAgoPipe
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    ReactiveFormsModule,
    BrowserAnimationsModule,
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      {path: '', component: WelcomeComponent, pathMatch: 'full'},
      {path: 'home', component: HomeComponent}
    ])
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
