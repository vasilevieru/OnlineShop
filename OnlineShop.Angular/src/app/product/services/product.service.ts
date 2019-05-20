import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Product, ProductCharacteristics } from '../models';
import { PageView, PagedResult, FileDetails } from '@shared';

@Injectable({
  providedIn: 'root'
})
export class ProductService {

  constructor(private http: HttpClient) { }

  getProduct(id: number): Observable<Product> {
    return this.http.get<Product>(`api/products/${id}`);
  }

  getProducts(gridParams: PageView): Observable<PagedResult<Product>> {
    const params = gridParams.toSearchParams();
    return this.http.get<PagedResult<Product>>('api/products/paged', { params });
  }

  addProduct(product: Product): Observable<Product> {
    return this.http.post<Product>(`api/products`, product);
  }

  updateProduct(productId: number, product: Product): Observable<Product> {
    return this.http.put<Product>(`api/produts/${productId}`, product);
  }

  addCharacteristics(prodId: number, prodCharacteristics: ProductCharacteristics): Observable<ProductCharacteristics> {
    return this.http.post<ProductCharacteristics>(`api/products/${prodId}/characteristics`, prodCharacteristics);
  }
}
