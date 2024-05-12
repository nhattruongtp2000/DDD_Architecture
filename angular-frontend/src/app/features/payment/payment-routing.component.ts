import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Routes, RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';
import { PaymentComponent } from './payment.component';
import { PaymentReturnComponent } from './payment-return.component';

const routes: Routes = [
  { path: 'payment', component: PaymentComponent },
  { path: 'payment-return', component: PaymentReturnComponent },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class PaymentRoutingModule {}
