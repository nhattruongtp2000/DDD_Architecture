import { Component, OnInit } from '@angular/core';
import { FormGroup ,FormBuilder, Validators} from '@angular/forms';
import { LoginRequest } from 'src/app/core/models/User/UserRequest';
import { Router } from '@angular/router';
import { UserService } from 'src/app/core/services/UserService.service';

@Component({
  selector: 'login',
  templateUrl: './login.component.html',
})
export class LoginComponent implements OnInit {
  form!:FormGroup
  userModel: User;

  constructor(private userService:UserService,private formBuilder:FormBuilder
  ,private router:Router ){ 
    this.userModel=new User();
  }

  ngOnInit(): void {
    this.form=this.formBuilder.group({
      email:['ailapronam2010@gmail.com',Validators.required],
      password:['Kiprao123*',Validators.required]
    })
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
          this.router.navigate(['../home'])
        }
      })
    
  }

}

export class User {
  FirstName!:string;
  LastName!:string;
  Email!:string;
  Password!:string;
  PhoneNumber!:string;
  Address!:string;
  Country!:string;
  City!:string;
  BirthDay!:Date;
  Organization!:string;
  Role!:string;
  Department!:string;
  ZipCode!:string;
  ImagePath!:string;
  Token!:string;


  constructor(){
    // this.FirstName=firstName,
    // this.LastName=lastName;
    // this.Email=email;
    // this.Password=password;
    // this.Token=token;
    // this.ImagePath=imagePath ;
    // this.PhoneNumber=phoneNumber,
    // this.Country=country;
    // this.City=city;
    // this.BirthDay=birthday;
    // this.Role=role;
    // this.Department=department;
    // this.ZipCode=zipcode;
    // this.Organization=organization;
  }

  setValue(firstName:string,lastName:string,email:string,password:string,token:string,address:string,imagePath:string){
    this.FirstName=firstName,
    this.LastName=lastName;
    this.Email=email;
    this.Password=password;
    this.Token=token;
    this.Address=address;
    this.ImagePath=(imagePath==null || imagePath=="" )? this.ImagePath:imagePath;
  }

  setImage(image:string){
    this.ImagePath=image
  }
}
export interface LoginResult{
  FirstName:string;
  LastName:string;
  Email:string;
  Password:string;
  Token:string;
}

