import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MaterialLayoutComponent } from './material-layout.component';
import { CatalogComponent } from '@product';
import { AuthGuard } from '@shared';

const appRoutes: Routes = [
  {
    path: '',
    component: MaterialLayoutComponent,
    children: [
      {
        path: '',
        component: CatalogComponent
      },
      {
        path: 'catalog',
        component: CatalogComponent,
        canActivate: [AuthGuard]
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(appRoutes)],
  exports: [RouterModule]
})
export class MaterialLayoutRoutingModule { }
