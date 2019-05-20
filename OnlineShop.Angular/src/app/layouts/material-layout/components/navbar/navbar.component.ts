import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { SVGIcons } from 'app/shared/utils/svg-icons-utils';
import { AuthService, CartService } from '@shared';
import { Router } from '@angular/router';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent implements OnInit {

  @Output() sidenavToggle = new EventEmitter<void>();

  faUserPlus = SVGIcons.faUserPlus;
  faSignIn = SVGIcons.faSignInAlt;
  faSignOut = SVGIcons.faSignOutAlt;
  faShoppingCart = SVGIcons.faShoppingCart;
  faClipboardList = SVGIcons.faClipboardList;

  isAuthenticated = false;
  cartItemsCount: number;

  constructor(
    private authService: AuthService,
    private cartService: CartService,
    protected router: Router) {
    this.isAuthenticated = this.authService.userIsAuthenticated;
  }

  ngOnInit() {
    this.cartService.cartItemsCount.subscribe(cartItemsCount => {
      this.cartItemsCount = cartItemsCount;
    });
  }

  logout(): void {
    this.authService.logout();
    this.router.navigate(['/login']);
  }

  onToggleSidenav(): void {
    this.sidenavToggle.emit();
  }
}
