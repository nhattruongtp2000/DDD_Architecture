import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormControl,Validators  } from '@angular/forms';
import { first } from 'rxjs/operators';
import { Router, ActivatedRoute } from '@angular/router';
import { UserService } from '../_service/UserService.service';
import { RegisterRequest } from '../_model/User/UserRequest';
@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css'],
})
export class RegisterComponent implements OnInit {
  form!: FormGroup; // ! is check null
  constructor(
    private formBuilder: FormBuilder,
    private userService:UserService,
    private route: ActivatedRoute,
    private router:Router
      ) { } 

  ngOnInit(): void {
    this.form = this.formBuilder.group({
      email: ['nhattruong@gmai.com', Validators.required],
      firstName: ['truong', Validators.required],
      lastName: ['nguyen', Validators.required],
      password: ['Kiprao123*', Validators.required],
      confirmPassword: ['Kiprao123*', Validators.required]
  });
  }
  get f() { return this.form.controls; }
  onSubmit(){
    const dataRequest :RegisterRequest={
      Email:this.form.value.email,
      FirstName:this.form.value.firstName,
      LastName:this.form.value.lastName,
      Password:this.form.value.password,
      ConfirmPassword:this.form.value.confirmPassword,
      
    }
    var data = this.userService.register(dataRequest).then(response=>{
      if(response.status==200){
        console.log("successs")
        this.router.navigate(['../app-login'], { relativeTo: this.route });
      }
    }).catch(errors=>console.log('Error',errors))
  }
}
