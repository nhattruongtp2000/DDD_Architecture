import { Component, OnInit } from '@angular/core';
import { User } from '../login/login.component';
import { UserService } from '../_service/UserService.service';
@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  user: User | null;

  constructor(private userService: UserService) 
  {
    console.log(this.userService.userValue)
      this.user = this.userService.userValue;
  }

  ngOnInit(): void {
  }
}
