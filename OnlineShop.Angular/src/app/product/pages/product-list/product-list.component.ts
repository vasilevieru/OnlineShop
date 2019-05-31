import { Component, OnInit } from '@angular/core';
import { MaterialGridComponent, NgOnDestroy, PageView, PagedResult, SimpleSnackBarService, SVGIcons, FileService } from '@shared';
import { Observable } from 'rxjs';
import { Product } from '@product';
import { ProductService } from 'app/product/services';
import { MatDialog } from '@angular/material';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-product-list',
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.scss']
})
export class ProductListComponent extends MaterialGridComponent {

  pageSize = 10;
  displayedColumns: string[] = ['photo', 'name', 'price', 'category', 'subCategory', 'description', 'actions'];
  faTrashAlt = SVGIcons.faTrashAlt;
  faEdit = SVGIcons.faEdit;
  faPlus = SVGIcons.faPlus;

  constructor(private productService: ProductService,
    private fileService: FileService,
    private snackbar: SimpleSnackBarService,
    public dialog: MatDialog,
    onDestroy$: NgOnDestroy) {
    super(onDestroy$, dialog);
  }

  fetchData(vm: PageView): Observable<PagedResult<Product>> {
    return this.productService.getProducts(vm);
  }

  onClickDeleteProduct(id: number): void {
    this.productService.deleteProduct(id).subscribe(() => {
      this.deleteItem(id, 'id');
      this.snackbar.openSuccess('Product deleted successfully');
    }, (error: HttpErrorResponse) => {
      this.snackbar.openErrorWithResponseMessage('Deletion failed', error);
    });
  }

}
