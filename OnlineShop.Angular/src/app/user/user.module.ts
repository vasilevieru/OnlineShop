import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatFormFieldModule, MatCardModule, MatButtonModule, MatInputModule, MatSnackBarModule, MatIconModule } from '@angular/material';
import { ReactiveFormsModule } from '@angular/forms';
import { LoginComponent, RegistrationComponent } from './pages';
import { UserRoutingModule } from './user-routing.module';
import { SharedModule } from '@shared';
import { RouterModule } from '@angular/router';

@NgModule({
  declarations: [
    RegistrationComponent,
    LoginComponent
  ],
  imports: [
    CommonModule,
    RouterModule,
    MatFormFieldModule,
    MatCardModule,
    MatButtonModule,
    ReactiveFormsModule,
    MatInputModule,
    MatSnackBarModule,
    MatIconModule,
    UserRoutingModule,
    SharedModule
  ]
})
export class UserModule { }
