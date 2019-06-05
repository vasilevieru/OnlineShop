import { Component, OnInit } from '@angular/core';
import { ProductService } from 'app/product/services/product.service';
import { Product } from 'app/product/models';
import { MaterialGridComponent, NgOnDestroy, PagedResult, PageView, FileDetails, FileService, FilesGroupedByProduct } from '@shared';
import { Observable, Subscription } from 'rxjs';
import { MatDialog } from '@angular/material';
import { ImageStore } from 'app/shared/image-store/image-store';
import { tap, take } from 'rxjs/operators';

@Component({
  selector: 'app-catalog',
  templateUrl: './catalog.component.html',
  styleUrls: ['./catalog.component.scss']
})
export class CatalogComponent implements OnInit{
  

  pageSize = 10;
  products: Product[] = [];
  files: FilesGroupedByProduct<FileDetails>[] = [];
  subscription: Subscription;
  isLoading = true;
  constructor(
    onDestroy$: NgOnDestroy,
    private productService: ProductService,
    private fileService: FileService,
    private imageStore: ImageStore,
    public dialog: MatDialog, ) {
  }

  ngOnInit(): void {
    this.productService.getProductsWithImages()
    .subscribe(res => {
      console.log(res);
      
    })
  }

  private loadImage(id: number, indexGroup: number, indexFile): void {
    const image$ = this.imageStore.getImageUrlById(id);

    this.subscription = image$
      .pipe(
        tap(() => this.subscription = null),
        take(1)
      )
      .subscribe(blob => {
        // this.imageSource = URL.createObjectURL(blob);
        this.files[indexGroup].files[indexFile].imageSource = `url(${URL.createObjectURL(blob)}`;

        this.isLoading = false;
      }, () => {
        this.isLoading = false;
      }
      );
  }

  getFiles() {
    this.fileService.getImagesGroupedByProduct()
      .subscribe(res => {
        this.files = res;
        this.files.forEach((file, indexG) => {
          file.files.forEach((f, indexF) => {
            this.loadImage(f.id, indexG, indexF);
          });
        });
        this.isLoading = false;
      });
  }

  fetchData(vm: PageView): Observable<PagedResult<Product>> {
    return this.productService.getProducts(vm);
  }

}
