import { Component, OnInit } from '@angular/core';
import { FormGroup ,FormBuilder, Validators} from '@angular/forms';
import { UserService } from '../_service/UserService.service';
import { LoginRequest } from '../_model/User/UserRequest';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
})
export class LoginComponent implements OnInit {
  form!:FormGroup
  constructor(private userService:UserService,private formBuilder:FormBuilder
  ,private router:Router ){ }

  ngOnInit(): void {
    this.form=this.formBuilder.group({
      email:['nhattruong@gmai.com',Validators.required],
      password:['Kiprao123*',Validators.required]
    })
  }
   user : User={
    Email:'',
    Password:'',
    Token:"",
    FirstName:"",
    LastName:""
  }

  handleChange(event: any){
    console.log(event)
  }

  onSubmit(){
    const request:LoginRequest={
      Email:this.form.value.email,
      Password:this.form.value.password
    }
      var loginRequest = this.userService.login(request).then( response=>{
        if(response?.FirstName!=null || response?.FirstName!==""){
          this.router.navigate(['../app-home'])
        }
      })
    
  }

}

export class User {
  FirstName:string;
  LastName:string;
  Email:string;
  Password:string;
  Token:string;;
  constructor(firstName:string,lastName:string,email:string,password:string,token:string){
    this.Email=email;
    this.Password=password;
    this.Token=token;
    this.FirstName=firstName;
    this.LastName=lastName;
  }
}
export interface LoginResult{
  FirstName:string;
  LastName:string;
  Email:string;
  Password:string;
  Token:string;
}
