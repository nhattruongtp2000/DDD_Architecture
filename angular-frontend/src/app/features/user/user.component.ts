import { Component, OnInit } from '@angular/core';
import { User } from '../authen/login.component';
import { UserService } from 'src/app/core/services/UserService.service';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css']
})
export class UserComponent implements OnInit {
  constructor(private userService: UserService) {

  }
  userModel: User = new User();

  ngOnInit(): void {
     const user = this.userService.userValue
     this.userModel = new User()
     this.userModel.setValue(user!.FirstName,user!.LastName,user!.Email,user!.Password,user!.Token,user!.Address,user!.ImagePath)
    console.log(user!.LastName)
    }
}