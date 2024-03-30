import { Component, OnInit } from '@angular/core';
import { FormGroup,FormBuilder,Validators } from '@angular/forms';
import { OrderInfo } from '../_model/User/UserRequest';
import { PaymentService } from '../_service/PaymentSevice.service';

@Component({
  selector: 'app-payment',
  templateUrl: './payment.component.html',
  styleUrls: ['./payment.component.css']
})
export class PaymentComponent implements OnInit {
  form !: FormGroup
  constructor(private formBuilder:FormBuilder,private paymentService:PaymentService) { }

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
      var orderInfo = new OrderInfo()
      orderInfo.setValue("999999",1000,"okok",new Date(),"1",1,"NCB","1","1")
      this.paymentService.SendOrderPayMent(orderInfo).then(dataReturn=>{
var dataa = dataReturn.data.value.data;
dataa.replace("VNPayReturn","IPN")
        this.paymentService.GetReturnPayment(dataa).then(returnStatus=>{
            console.log(returnStatus)
        })

        document.location.href=dataReturn.data.value.data

      });
  }

}