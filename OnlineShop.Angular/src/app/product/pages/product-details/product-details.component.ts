import { Component, OnInit } from '@angular/core';
import { Product } from 'app/product/models';
import { ActivatedRoute } from '@angular/router';
import { map, filter, takeUntil, tap, take } from 'rxjs/operators';
import { NgOnDestroy, FileService, FileDetails } from '@shared';
import { ProductService } from 'app/product/services';
import { ImageStore } from 'app/shared/image-store/image-store';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.scss']
})
export class ProductDetailsComponent implements OnInit {

  isLoading = true;
  imageSource: string;
  productId: number;
  files: FileDetails[] = [];
  subscription: Subscription;

  product: Product = new Product();
  isThumbnail = false;
  constructor(private route: ActivatedRoute,
    private imageStore: ImageStore,
    private onDestroy$: NgOnDestroy,
    private productService: ProductService,
    private fileService: FileService) { }

  ngOnInit() {
    this.route.paramMap
      .pipe(
        map(param => Number(param.get('id'))),
        filter(id => !isNaN(id) && id > 0),
        takeUntil(this.onDestroy$)
      ).subscribe(id => {
        this.productId = id;
        this.productService.getProduct(id)
          .subscribe(res => {
            this.product = res;
          });
      });

    this.fileService.getAllProductImages(this.productId)
      .subscribe(res => {
        this.files = res.files;
        this.files.forEach((file, index) => {
          this.loadImage(file.id, index);
        });
        this.isLoading = false;
      });
  }

  addToCart(id: number): void {

  }

  onDeleteProduct(id: number): void {

  }

  private loadImage(id: number, index: number): void {
    this.isLoading = true;

    const image$ = this.imageStore.getImageUrlById(id);

    this.subscription = image$
      .pipe(
        tap(() => this.subscription = null),
        take(1)
      )
      .subscribe(blob => {
        // this.imageSource = URL.createObjectURL(blob);
        this.files[index].imageSource = `url(${URL.createObjectURL(blob)}`;

        this.isLoading = false;
      }, () => {
        this.revokeImageUrl();
        this.isLoading = false;
      }
      );
  }

  private revokeImageUrl() {
    if (this.imageSource) {
      URL.revokeObjectURL(this.imageSource);
      this.imageSource = null;
    }
  }
}
