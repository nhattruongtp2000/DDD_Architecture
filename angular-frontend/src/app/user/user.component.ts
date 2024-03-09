import { Component, OnInit } from '@angular/core';
import { UserService } from '../_service/UserService.service';
import { User } from '../login/login.component';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css']
})
export class UserComponent implements OnInit {
  constructor(private userService: UserService) {

  }
  userModel: User = new User("","","","","");

  ngOnInit(): void {
     const user = this.userService.userValue
     this.userModel = new User(user!.FirstName,user!.LastName,user!.Email,user!.Password,user!.Token)
    console.log(user!.LastName)
    }
}