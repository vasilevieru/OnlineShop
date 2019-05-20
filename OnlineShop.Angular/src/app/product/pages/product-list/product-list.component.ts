import { Component, OnInit } from '@angular/core';
import { MaterialGridComponent, NgOnDestroy, PageView, PagedResult, SimpleSnackBarService, SVGIcons } from '@shared';
import { Observable } from 'rxjs';
import { Product } from '@product';
import { ProductService } from 'app/product/services';
import { MatDialog } from '@angular/material';

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

  constructor(
    private productService: ProductService,
    private snackbar: SimpleSnackBarService,
    public dialog: MatDialog,
    onDestroy$: NgOnDestroy) {
    super(onDestroy$, dialog);
  }

  fetchData(vm: PageView): Observable<PagedResult<Product>> {
    return this.productService.getProducts(vm);
  }

  onClickDeleteProduct(id: number): void {

  }

}
