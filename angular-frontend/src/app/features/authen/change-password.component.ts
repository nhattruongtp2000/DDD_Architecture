import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'change-password',
  templateUrl: './change-password.component.html',
})
export class ChangePasswordComponent implements OnInit {
  confirmPasswordHidden: string = 'password';
  textStatusConfirmPassWord:string='i'
  constructor() {}
  ngOnInit(): void {}
  changeConfirmPasswordStatus() {
    this.confirmPasswordHidden = this.confirmPasswordHidden== "text"?"password" :"text";
    this.textStatusConfirmPassWord = this.textStatusConfirmPassWord== "i"?"a" :"i";
  }
}
