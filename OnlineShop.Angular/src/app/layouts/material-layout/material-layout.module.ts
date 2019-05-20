import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MaterialLayoutRoutingModule } from './material-layout-routing.module';
import {
  MatToolbarModule,
  MatSidenavModule,
  MatIconModule,
  MatTreeModule,
  MatListModule,
  MatButtonModule,
  MatBadgeModule
} from '@angular/material';
import { NavbarComponent } from './components/navbar/navbar.component';
import { SidenavComponent } from './components/sidenav/sidenav.component';
import { FooterComponent } from './components/footer/footer.component';
import { MaterialLayoutComponent } from './material-layout.component';
import { FlexLayoutModule } from '@angular/flex-layout';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { RouterModule } from '@angular/router';
import { library } from '@fortawesome/fontawesome-svg-core';
import { fas } from '@fortawesome/free-solid-svg-icons';
import { far } from '@fortawesome/free-regular-svg-icons';
import { fab } from '@fortawesome/free-brands-svg-icons';
import { UserModule } from 'app/user/user.module';
import { ProductModule } from 'app/product/product.module';
import { AuthorizeDirectivesModule } from 'app/directives/authorize-directive.module';

library.add(fas, far, fab);

@NgModule({
  imports: [
    RouterModule,
    CommonModule,
    UserModule,
    ProductModule,
    MaterialLayoutRoutingModule,
    MatToolbarModule,
    MatSidenavModule,
    FlexLayoutModule,
    MatIconModule,
    FontAwesomeModule,
    MatBadgeModule,
    MatTreeModule,
    MatListModule,
    MatButtonModule,
    AuthorizeDirectivesModule,
  ],
  declarations: [
    NavbarComponent,
    SidenavComponent,
    FooterComponent,
    MaterialLayoutComponent
  ],
  exports: [
    NavbarComponent,
    SidenavComponent,
    FooterComponent
  ]
})
export class MaterialLayoutModule { }
