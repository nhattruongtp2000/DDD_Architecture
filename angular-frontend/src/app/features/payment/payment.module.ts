import { NgModule } from "@angular/core";
import { BrowserModule } from "@angular/platform-browser";
import { PaymentRoutingModule } from "./payment-routing.component";
import { PaymentComponent } from "./payment.component";
import { PaymentReturnComponent } from "./payment-return.component";
import { CommonModule } from "@angular/common";
import { RouterModule } from "@angular/router";
import { ReactiveFormsModule } from "@angular/forms";

@NgModule({
    imports:[
        BrowserModule,
        PaymentRoutingModule,
        CommonModule,
        BrowserModule,
        RouterModule,
        ReactiveFormsModule
    ],
    declarations:[
        PaymentComponent,
        PaymentReturnComponent
    ],
    exports:[]
})
export class PaymentModule{}