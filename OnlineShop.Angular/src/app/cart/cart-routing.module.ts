import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { CartDetailsComponent } from './pages';
import { RoleGuard } from '@shared';
import { MaterialLayoutComponent } from 'app/layouts/material-layout/material-layout.component';

const routes: Routes = [
  {
    path: 'cart',
    component: MaterialLayoutComponent,
    children: [
      {
        path: '',
        component: CartDetailsComponent,
        canActivate: [RoleGuard],
        data: {
          permission: 'User'
        }
      }
    ]
  },
];

@NgModule({
  imports: [
    RouterModule.forRoot(routes)
  ],
  exports: [
    RouterModule
  ]
})
export class CartRoutingModule { }
