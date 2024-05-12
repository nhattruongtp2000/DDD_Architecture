import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { AuthenRoutingModule } from './authen-routing.module';
import { BrowserModule } from '@angular/platform-browser';
import {RouterModule } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { RegisterComponent } from './register.component';
import { LoginComponent } from './login.component';
import { AuthenLayoutComponent } from './authen-layout.component';

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
    ],
    declarations: [
        RegisterComponent,
        LoginComponent,
        AuthenLayoutComponent
    ],
    exports:[]
})
export class AuthenModule { }