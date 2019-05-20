import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { CartItem } from '@cart';
import { tap } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class CartService {

  cartItemsCount = new BehaviorSubject<number>(0);
  private count = 0;

  constructor(private http: HttpClient) {
    this.getCartItemsCount().subscribe((count: number) => {
      this.cartItemsCount.next(count);
    });
  }

  getCartItems(): Observable<CartItem[]> {
    return this.http.get<CartItem[]>('api/carts').pipe(
      tap(cartItems => {
        this.count = cartItems.length;
        this.cartItemsCount.next(this.count);
      })
    );
  }

  getCartItemsCount() {
    return this.http.get('api/carts/count');
  }
}
