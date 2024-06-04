import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Routes, RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';
import { LoginComponent } from './login.component';
import { RegisterComponent } from './register.component';
import { AuthenLayoutComponent } from './authen-layout.component';
import { ChangePasswordComponent } from './change-password.component';
import { EmailChangePasswordCompoent } from './email-change-pw.component';

const routes: Routes = [
  {
    path: '',
    component: AuthenLayoutComponent,
    children: [
      { path: 'login', component: LoginComponent },
      { path: 'register', component: RegisterComponent },
      { path: 'change-password', component: ChangePasswordComponent },
      { path: 'email-change-pw', component: EmailChangePasswordCompoent },
    ],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class AuthenRoutingModule {}