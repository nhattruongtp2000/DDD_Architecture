import { Component } from '@angular/core';
import { User } from './features/authen/login.component';
import { UserService } from './core/services/UserService.service';

@Component({ selector: 'app-root', templateUrl: 'app.component.html' })
export class AppComponent {
    user?: User | null;

    constructor(private userService: UserService) {
        this.userService.user.subscribe(x => this.user = x);
    }

    logout() {
        this.userService.logout();
    }
}