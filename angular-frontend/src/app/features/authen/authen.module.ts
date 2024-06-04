import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { AuthenRoutingModule } from './authen-routing.module';
import { BrowserModule } from '@angular/platform-browser';
import {RouterModule } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { RegisterComponent } from './register.component';
import { LoginComponent } from './login.component';
import { AuthenLayoutComponent } from './authen-layout.component';
import { ChangePasswordComponent } from './change-password.component';
import { EmailChangePasswordCompoent } from './email-change-pw.component';

@NgModule({
    imports: [
        CommonModule,
        ReactiveFormsModule,
        AuthenRoutingModule,
        ReactiveFormsModule,
        HttpClientModule,
        RouterModule,
        CommonModule,
        BrowserModule,
        FormsModule
    ],
    declarations: [
        RegisterComponent,
        LoginComponent,
        AuthenLayoutComponent,
        ChangePasswordComponent,
        EmailChangePasswordCompoent
    ],
    exports:[]
})
export class AuthenModule { }