import { Component, OnInit } from '@angular/core';
import { ProductService } from 'app/product/services/product.service';

@Component({
  selector: 'app-catalog',
  templateUrl: './catalog.component.html',
  styleUrls: ['./catalog.component.scss']
})
export class CatalogComponent implements OnInit {

  constructor(private productService: ProductService) { }

  ngOnInit() {
    // this.productService.getProducts().subscribe(res => {
    // });
  }

}
