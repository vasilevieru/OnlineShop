import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MaterialLayoutComponent } from './layouts/material-layout/material-layout.component';
import { CatalogComponent } from '@product';

const appRoutes: Routes = [
  {
    path: '',
    component: MaterialLayoutComponent,
    children: [
      {
        path: '',
        component: CatalogComponent
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(appRoutes)],
  exports: [RouterModule],
  declarations: []
})
export class AppRoutingModule { }
