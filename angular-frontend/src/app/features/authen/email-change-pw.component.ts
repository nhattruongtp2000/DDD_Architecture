import { Component, OnInit,Output,EventEmitter } from '@angular/core';
import { Router } from '@angular/router';
import { FormGroup,Validators,FormBuilder } from '@angular/forms';
import { DataService } from 'src/app/shared/DataService.service';

@Component({
  selector: 'email-change-pw',
  templateUrl: './email-change-pw.component.html',
})
export class EmailChangePasswordCompoent implements OnInit {

  form!:FormGroup

  constructor(
    private router:Router,
    private dataService:DataService,
    private formBuilder:FormBuilder
  ) {

  }

  ngOnInit(): void {
    this.form=this.formBuilder.group({
      email:['',[Validators.required,Validators.email]]
    })
  }

  get email(){
    return this.form.get('email')
  }

  SendInstruction(){  
    if(!this.form.valid){
      window.alert("data hasn't valid")
      return;
    }
    this.dataService.sendData(this.form.value.email)
    this.router.navigate(['/change-password'])
  }
}
