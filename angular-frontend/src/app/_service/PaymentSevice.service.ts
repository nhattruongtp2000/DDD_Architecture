import { Injectable } from '@angular/core';
import axios from 'axios';
import { User } from '../login/login.component';
import { BehaviorSubject, Observable } from 'rxjs';
import { OrderInfo, OrderInfoReturn } from '../_model/User/UserRequest';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { LoginRequest } from '../_model/User/UserRequest';

@Injectable({
  providedIn: 'root',
})
export class PaymentService {
  url = 'https://localhost:7127/api/Payments';

  constructor(private router: Router, http: HttpClient) {}
  public SendOrderPayMent(orderInfo:OrderInfo){
        return axios.post(this.url+"/VnPay",orderInfo).then(data=>{
            return data;
        });
  }

  public GetReturnPayment(urlReturn : string){
    return axios.post(this.url+ "/AddPayment").then(data=>{
        return data;
    });
  }
  public AddPayment(data:OrderInfoReturn){
    return axios.post(this.url+ "/AddPayment").then(data=>{
      console.log(data.status)
      return data.status;
  });
  }

}
