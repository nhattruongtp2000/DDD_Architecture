import { Component, OnInit } from '@angular/core';
import { OrderInfo } from '../../core/models/User/UserRequest';
import { OrderInfoReturn } from '../../core/models/User/UserRequest';
import { HttpClient } from '@angular/common/http';
import { ActivatedRoute } from '@angular/router';
import { PaymentService } from 'src/app/core/services/PaymentSevice.service';


@Component({
  selector: 'payment-return',
  templateUrl: './payment-return.component.html',
})
export class PaymentReturnComponent implements OnInit {
  dataReturn!: OrderInfoReturn;
  constructor(
    private http: HttpClient,
    private route: ActivatedRoute,
    private paymentService: PaymentService
  ) {}

  ngOnInit(): void {
    this.dataReturn= new OrderInfoReturn();
    var dataReturn2 = new OrderInfoReturn();
    this.route.queryParams.subscribe((params) => {
      // Extract the relevant data from the URL
      const orderId = params['vnp_TxnRef'];
      const vnpayTranId = params['vnp_TransactionNo'];
      const vnp_ResponseCode = params['vnp_ResponseCode'];
      const vnp_TransactionNo = params['vnp_TransactionNo'];
      const vnp_TransactionStatus = params['vnp_TransactionStatus'];
      const vnp_Amount = params['vnp_Amount'];
      const vnp_SecureHash = params['vnp_TxnRef'];
      const TerminalID = params['vnp_TxnRef'];
      const bankCode = params['vnp_BankCode'];
      const transactionInfo = params['vnp_OrderInfo'];

      dataReturn2 = {
        OrderId: orderId,
        OrderName: "Payment merchendise",
        TransactionId: vnpayTranId,
        TransactionInfo: transactionInfo,
        TotalAmount:vnp_Amount, // Convert to number if required
        CurrentCode: "VND",
        TransactionResponseCode: vnp_TransactionStatus,
        Message: "Success",
        TransactionNumber: vnp_TransactionNo,
        Bank: bankCode
      };
      if(vnp_TransactionStatus=='00'){
        this.addPayment(dataReturn2);
       
      }


      // Extract other parameters similarly...
    });
  }

  ngOnLoad(): void {}

  setDataReturn(data: OrderInfoReturn) {
    this.dataReturn = data;
  }

  addPayment(data:OrderInfoReturn){
    this.paymentService.AddPayment(data).then(dataReturn=>{
      if(dataReturn==200){
        this.dataReturn=data
      }
    })
  }
}
