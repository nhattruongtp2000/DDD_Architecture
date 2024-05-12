import { Component } from '@angular/core';
import { CartService } from './core/services/CartService.service';
import { User } from './features/authen/login.component';
import { UserService } from './core/services/UserService.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent {
  user?: User | null;

  constructor(
    private UserService: UserService,
    private CartService: CartService
  ) {
    this.UserService.user.subscribe((x) => (this.user = x));
    console.log(this.user);
  }

  logout() {
    this.UserService.logout();
  }
  get count() {
    return this.CartService.cartLength;
  }
}
