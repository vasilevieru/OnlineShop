import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlexLayoutModule } from '@angular/flex-layout';
import { MatButtonModule, MatTableModule, MatIconModule } from '@angular/material';
import { SharedModule } from '@shared';
import { CartRoutingModule } from './cart-routing.module';


@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    CartRoutingModule,
    MatButtonModule,
    MatTableModule,
    FlexLayoutModule,
    MatIconModule,
    SharedModule
  ]
})
export class CartModule { }
