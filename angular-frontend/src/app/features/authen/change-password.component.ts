import { Component, OnInit, OnDestroy,ChangeDetectorRef } from '@angular/core';
import { DataService } from 'src/app/shared/DataService.service';
import { Subscription ,} from 'rxjs';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ChangePasswordRequest } from 'src/app/core/models/User/UserRequest';

@Component({
  selector: 'change-password',
  templateUrl: './change-password.component.html',
})
export class ChangePasswordComponent implements OnInit, OnDestroy {
  confirmPasswordHidden: string = 'password';
  textStatusConfirmPassWord: string = 'i';

  form!: FormGroup;

  email: string;
  private dataSubscription: Subscription

  constructor(
    private dataService: DataService,
    private formBuilder: FormBuilder,
    private cdr: ChangeDetectorRef
  ) {}
  ;

  ngOnInit(): void {
    console.log("vo")
    this.dataSubscription = this.dataService.email.subscribe(message =>{
      this.email=message
      console.log(message)
    });

    this.form = this.formBuilder.group({
      password: ['', [Validators.required,Validators.minLength(4)]],
      confirmPassword: ['', [Validators.required,Validators.minLength(4)]],
    });
  }

  ngOnDestroy(): void {
    this.dataSubscription.unsubscribe();
  }

  get password(){
    return this.form.get('password')
  }

  get confirmPassword(){
    return this.form.get('confirmPassword')
  }


  changeConfirmPasswordStatus() {
    this.confirmPasswordHidden =
      this.confirmPasswordHidden == 'text' ? 'password' : 'text';
    this.textStatusConfirmPassWord =
      this.textStatusConfirmPassWord == 'i' ? 'a' : 'i';
  }

  requestReset() {

    if(!this.form.valid){
      window.alert("data hasn't valid")
      return;
    }
    let password= this.form.value.password;
    let confiimPassword=this.form.value.confirmPassword;
    if(password!=confiimPassword){
      window.alert("password is not match")
      return;
    }

    const requestChangePW : ChangePasswordRequest={
      Email:this.email,
      Password:this.form.value.password,
      ConfirmPassword:this.form.value.confirmPassword
    }
    console.log(requestChangePW)
  }
}
