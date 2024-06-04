import { Component } from '@angular/core';
import { CartService } from './core/services/CartService.service';
import { User } from './features/authen/login.component';
import { UserService } from './core/services/UserService.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent {
  user?: User | null;

  constructor(
    private UserService: UserService,
    private CartService: CartService,
    private router: Router,

  ) {
    this.UserService.user.subscribe((x) => (this.user = x));
    if(this.user==null)
    this.router.navigate(['/login']);
  }

  logout() {
    this.UserService.logout();
  }
  get count() {
    return this.CartService.cartLength;
  }
}
