import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Category, SubCategory, Product, ProductCharacteristics } from 'app/product/models';
import { ProductService } from 'app/product/services';
import { FileService, SimpleSnackBarService } from '@shared';
import { HttpErrorResponse } from '@angular/common/http';
import { Router } from '@angular/router';
import { MatDialog } from '@angular/material';
import {
  AddProductCharacteristicsComponent
} from 'app/product/components/add-product-characteristics/add-product-characteristics.component';

@Component({
  selector: 'app-product-add',
  templateUrl: './product-add.component.html',
  styleUrls: ['./product-add.component.scss']
})
export class ProductAddComponent implements OnInit {

  files: FileList;
  form: FormGroup;

  categories: Category[];
  subCategories: SubCategory[];
  prodCharacteristics: ProductCharacteristics = new ProductCharacteristics();
  isSubmitted = false;
  productId: number;

  constructor(
    private dialog: MatDialog,
    private router: Router,
    private productService: ProductService,
    private fileService: FileService,
    private snackbar: SimpleSnackBarService) { }

  ngOnInit() {
    this.form = new FormGroup({
      name: new FormControl('', [Validators.required, Validators.maxLength(50)]),
      unitPrice: new FormControl('', [Validators.required]),
      category: new FormControl('', Validators.required),
      subCategory: new FormControl(''),
      description: new FormControl(''),
      photo: new FormControl('', Validators.required)
    });
  }

  onSubmit(): void {
    if (!this.form.valid) {
      return;
    }

    const product: Product = this.form.getRawValue();
    this.productService.addProduct(product)
      .subscribe((res) => {
        this.productId = res.id;
        this.addPhotos(res.id);
      }, (error: HttpErrorResponse) => {
        this.snackbar.openErrorWithResponseMessage('Product create failed', error);
      });
  }

  handleFileInput(fileList): void {
    this.files = fileList;
  }

  openCharacteristicsDialog(): void {
    const dialogRef = this.dialog.open(AddProductCharacteristicsComponent, {
      width: '300px'
    });

    dialogRef.afterClosed().subscribe(res => {
      if (res) {
        this.prodCharacteristics = res;
      }
    });
  }

  private createFormData(): FormData {
    const formData = new FormData();
    for (let index = 0; index < this.files.length; index++) {
      formData.append('photos', this.files[index], this.files[index].name);
    }
    return formData;
  }

  private addPhotos(productId: number): void {
    this.fileService.uploadPhoto(productId, this.createFormData())
      .subscribe(() => {
        this.isSubmitted = true;
        this.snackbar.openSuccess('Product created successfully');
        this.router.navigate(['/products', productId]);
      }, (error: HttpErrorResponse) => {
        this.snackbar.openErrorWithResponseMessage('Upload image failed', error);
      });
  }

  private addCharacteristics(prodCharacteristics: ProductCharacteristics): void {
    this.productService.addCharacteristics(this.productId, prodCharacteristics)
      .subscribe(() => {
        this.snackbar.openSuccess('Characteristics added to product');
      }, (error: HttpErrorResponse) => {
        this.snackbar.openErrorWithResponseMessage('Characteristics add failed', error);
      });
  }
}
