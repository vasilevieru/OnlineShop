import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { FileService, SimpleSnackBarService, NgOnDestroy } from '@shared';
import { HttpErrorResponse } from '@angular/common/http';
import { Product, Category, SubCategory } from 'app/product/models';
import { ProductService } from 'app/product/services';
import { map, filter, takeUntil } from 'rxjs/operators';

@Component({
  selector: 'app-product-edit',
  templateUrl: './product-edit.component.html',
  styleUrls: ['./product-edit.component.scss']
})
export class ProductEditComponent implements OnInit {

  file: File;
  form: FormGroup;

  productId: number;
  categories: Category[];
  subCategories: SubCategory[];
  isSubmitted = false;

  constructor(
    private router: Router,
    private productService: ProductService,
    private fileService: FileService,
    private snackbar: SimpleSnackBarService,
    private onDestroy$: NgOnDestroy,
    private route: ActivatedRoute) { }

  ngOnInit() {
    this.createForm();
    this.route.paramMap
      .pipe(
        map(param => Number(param.get('id'))),
        filter(id => !isNaN(id) && id > 0),
        takeUntil(this.onDestroy$)
      ).subscribe(id => {
        this.productId = id;
        this.fillForm(id);
      });
  }

  fillForm(id: number): void {
    this.productService.getProduct(id)
      .subscribe(res => {
        this.form.patchValue(res);
      });
  }

  createForm(): void {
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

    this.fileService.uploadPhoto(this.createFormData())
      .subscribe((uploadedFileDetails) => {
        const product: Product = this.form.getRawValue();
        product.logoId = uploadedFileDetails.id;
        this.updateProduct(product);
      }, (error: HttpErrorResponse) => {
        this.snackbar.openErrorWithResponseMessage('Upload image failed', error);
      });
  }

  handleFileInput(fileList: FileList): void {
    this.file = fileList.item(0);
  }

  private createFormData(): FormData {
    const formData = new FormData();
    formData.append('logo', this.file, this.file.name);
    return formData;
  }

  private updateProduct(product: Product): void {
    this.productService.updateProduct(this.productId, product)
      .subscribe((res) => {
        this.isSubmitted = true;
        this.snackbar.openSuccess('Product updated successfully');
        this.router.navigate(['/products', res.id]);
      }, (error: HttpErrorResponse) => {
        this.snackbar.openErrorWithResponseMessage('Product update failed', error);
      });
  }
}
