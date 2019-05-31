import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppComponent } from './app.component';

import { RouterModule } from '@angular/router';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AppRoutingModule } from './app-routing.module';
import { SharedModule, RefreshTokenInterceptor, JwtInterceptor } from '@shared';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { UserAuthenticationLayoutComponent } from './layouts/user-authentication-layout/user-authentication-layout.component';
import { MaterialLayoutModule } from './layouts/material-layout/material-layout.module';
import { AuthorizeDirectivesModule } from './directives/authorize-directive.module';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';

@NgModule({
  declarations: [
    AppComponent,
    UserAuthenticationLayoutComponent,
  ],
  imports: [
    RouterModule,
    BrowserModule,
    MaterialLayoutModule,
    AppRoutingModule,
    HttpClientModule,
    BrowserAnimationsModule,
    SharedModule,
    NgbModule,
    AuthorizeDirectivesModule
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: RefreshTokenInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },

  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
