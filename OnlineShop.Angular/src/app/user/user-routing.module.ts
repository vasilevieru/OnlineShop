import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { RegistrationComponent, LoginComponent } from './pages';
import { CanDeactivateGuard } from 'app/shared/guards/can-deactivate.guard';
import { UserAuthenticationLayoutComponent } from 'app/layouts/user-authentication-layout/user-authentication-layout.component';

const appRoutes: Routes = [
  {
    path: 'login',
    component: UserAuthenticationLayoutComponent,
    children: [
      {
        path: '',
        component: LoginComponent
      }
    ]
  },
  {
    path: 'registration',
    component: UserAuthenticationLayoutComponent,
    children: [
      {
        path: '',
        component: RegistrationComponent,
        canDeactivate: [CanDeactivateGuard]
      }
    ]
  },
];

@NgModule({
  imports: [RouterModule.forRoot(appRoutes)],
  exports: [RouterModule]
})
export class UserRoutingModule { }
