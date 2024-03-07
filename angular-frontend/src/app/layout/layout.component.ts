import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { UserService } from '../_service/UserService.service';

@Component({
  selector: 'app-layout',
  templateUrl: './layout.component.html',
  styleUrls: ['./layout.component.css']
})
export class LayoutComponent {
  constructor(
      private router: Router,
      private userService: UserService
  ) {
      // redirect to home if already logged in
      if (this.userService.userValue) {
          this.router.navigate(['/']);
      }
  }
}

