import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './login/login.component';
import { FormsModule } from '@angular/forms';
import { RegisterComponent } from './register/register.component';
import { FormControl, ReactiveFormsModule } from '@angular/forms';
import { HomeComponent } from './home/home.component';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { UserComponent } from './user/user.component';
import { UserSettingComponent } from './user/usersetting.component';
import { TransactionComponent } from './transaction/transaction.component';
import { PaymentComponent } from './payment/payment.component';
import { ListProductsComponent } from './list-products/list-products.component';
import { AdminlayoutComponent } from './admin/adminlayout.component';
import { AdminDaboardComponent } from './admin-daboard/admin-daboard.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatDialogModule } from '@angular/material/dialog';


@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    UserComponent,
    UserSettingComponent,
    TransactionComponent,
    PaymentComponent,
    ListProductsComponent,
    AdminlayoutComponent,
    AdminDaboardComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    CommonModule,
    BrowserAnimationsModule,
    MatDialogModule
  ],
  providers: [


  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
