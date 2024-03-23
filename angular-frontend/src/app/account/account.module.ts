import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { AccountRoutingModule } from './account-routing.module';
import { HomeComponent } from '../home/home.component';
import { LoginComponent } from '../login/login.component';
import { RegisterComponent } from '../register/register.component';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from '../app-routing.module';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { LayoutComponent } from '../layout/layout.component';
@NgModule({
    imports: [
        CommonModule,
        ReactiveFormsModule,
        AccountRoutingModule,
        ReactiveFormsModule,
        HttpClientModule,
        CommonModule,
    ],
    declarations: [
        LayoutComponent,
        LoginComponent,
        RegisterComponent
    ]
})
export class AccountModule { }