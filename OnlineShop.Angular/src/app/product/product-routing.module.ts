import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { UserAuthenticationLayoutComponent } from './../layouts/user-authentication-layout/user-authentication-layout.component';
import { ProductAddComponent, ProductDetailsComponent, ProductEditComponent, ProductListComponent } from './pages';
import { RoleGuard } from '@shared';
import { CanDeactivateGuard } from 'app/shared/guards/can-deactivate.guard';
import { MaterialLayoutComponent } from 'app/layouts/material-layout/material-layout.component';

const routes: Routes = [
  {
    path: 'products',
    component: UserAuthenticationLayoutComponent,
    children: [
      {
        path: '',
        component: ProductListComponent,
      },
      {
        path: 'new',
        component: ProductAddComponent,
        canDeactivate: [CanDeactivateGuard],
        canActivate: [RoleGuard],
        data: {
          permission: 'Admin'
        }
      },
      {
        path: ':id/edit',
        component: ProductEditComponent,
        canDeactivate: [CanDeactivateGuard],
        canActivate: [RoleGuard],
        data: {
          permission: 'Admin'
        }
      }
    ]
  },
  {
    path: 'products',
    component: MaterialLayoutComponent,
    children: [
      {
        path: ':id',
        component: ProductDetailsComponent
      }
    ]
  }
];

@NgModule({
  imports: [
  RouterModule.forRoot(routes)
  ],
  exports: [
    RouterModule
  ]
})
export class ProductRoutingModule { }
