import { Component, OnInit } from '@angular/core';
import { User } from '../authen/login.component';
import { UserService } from 'src/app/core/services/UserService.service';
@Component({
  selector: 'home',
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
