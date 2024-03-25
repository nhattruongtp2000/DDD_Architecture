import { Component, OnInit } from '@angular/core';
import { FormGroup,FormBuilder,Validators } from '@angular/forms';

@Component({
  selector: 'app-payment',
  templateUrl: './payment.component.html',
  styleUrls: ['./payment.component.css']
})
export class PaymentComponent implements OnInit {
  form !: FormGroup
  constructor(private formBuilder:FormBuilder) { }

  ngOnInit(): void {
    this.form=this.formBuilder.group({
      ProductType: ["", Validators.required],
      TotalAmount: ["", Validators.required],
      Content: ["", Validators.required],
      BankCode: ["", Validators.required],
      Language: ["", Validators.required],
    })
  }

  onSubmit(){
    
  }

}