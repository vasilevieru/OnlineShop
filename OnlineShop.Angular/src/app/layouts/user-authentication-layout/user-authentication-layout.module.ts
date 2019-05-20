import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NavbarComponent, FooterComponent } from '../material-layout';
import { UserAuthenticationLayoutComponent } from './user-authentication-layout.component';
import { RouterModule } from '@angular/router';
import { UserModule } from 'app/user/user.module';
import { MatToolbarModule, MatIconModule, MatButtonModule, MatSidenavModule } from '@angular/material';
import { FlexLayoutModule } from '@angular/flex-layout';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { AuthorizeDirectivesModule } from 'app/directives/authorize-directive.module';

@NgModule({
  declarations: [
    NavbarComponent,
    FooterComponent,
    UserAuthenticationLayoutComponent
  ],
  imports: [
    RouterModule,
    CommonModule,
    UserModule,
    MatSidenavModule,
    MatToolbarModule,
    FlexLayoutModule,
    MatIconModule,
    FontAwesomeModule,
    MatButtonModule,
    AuthorizeDirectivesModule
  ],
  exports: [
    NavbarComponent,
    FooterComponent
  ]
})
export class UserAuthenticationLayoutModule { }
