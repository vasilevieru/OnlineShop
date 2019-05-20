import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CatalogComponent } from './pages/catalog/catalog.component';
import { ProductAddComponent } from './pages/product-add/product-add.component';
import { ProductEditComponent } from './pages/product-edit/product-edit.component';
import { ProductDetailsComponent } from './pages/product-details/product-details.component';
import { RouterModule } from '@angular/router';
import { SharedModule, NgOnDestroy } from '@shared';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import {
  MatFormFieldModule,
  MatInputModule,
  MatButtonModule,
  MatAutocompleteModule,
  MatOptionModule,
  MatTableModule,
  MatPaginatorModule,
  MatIconModule,
  MatSortModule,
  MatDialogModule,
  MatCardModule
} from '@angular/material';
import { ProductRoutingModule } from './product-routing.module';
import { ProductListComponent } from './pages/product-list/product-list.component';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AddProductCharacteristicsComponent } from './components/add-product-characteristics/add-product-characteristics.component';

@NgModule({
  declarations: [
    CatalogComponent,
    ProductAddComponent,
    ProductEditComponent,
    ProductDetailsComponent,
    ProductListComponent,
    AddProductCharacteristicsComponent
  ],
  imports: [
  CommonModule,
    BrowserModule,
    BrowserAnimationsModule,
    RouterModule,
    SharedModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    FormsModule,
    FontAwesomeModule,
    MatAutocompleteModule,
    MatOptionModule,
    ProductRoutingModule,
    MatTableModule,
    MatIconModule,
    MatSortModule,
    MatDialogModule,
    MatPaginatorModule,
    MatCardModule
  ],
  providers: [NgOnDestroy],
  entryComponents: [AddProductCharacteristicsComponent]
})
export class ProductModule { }
