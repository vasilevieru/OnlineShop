import { Component, OnInit } from '@angular/core';
import { Product } from 'app/product/models';
import { ActivatedRoute } from '@angular/router';
import { map, filter, takeUntil } from 'rxjs/operators';
import { NgOnDestroy, FileService, FileDetails } from '@shared';
import { ProductService } from 'app/product/services';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.scss']
})
export class ProductDetailsComponent implements OnInit {

  isLoading = true;
  productId: number;
  product: Product = new Product();
  files: FileDetails[] = [];
  constructor(private route: ActivatedRoute,
    private onDestroy$: NgOnDestroy,
    private fileService: FileService) { }

  ngOnInit() {
    this.route.paramMap
    .pipe(
      map(param => Number(param.get('id'))),
      filter(id => !isNaN(id) && id > 0),
      takeUntil(this.onDestroy$)
    ).subscribe(id => {
      this.productId = id;
    });

    this.fileService.getAllProductImages(this.productId)
    .subscribe(files => {
      this.files = files;
    });
  }

}
