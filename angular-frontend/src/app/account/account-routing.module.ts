import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Routes, RouterModule } from '@angular/router';
import { AppComponent } from '../app.component';
import { LoginComponent } from '../login/login.component';
import { RegisterComponent } from '../register/register.component';
import { NgModule } from '@angular/core';
import { HomeComponent } from '../home/home.component';
import { LayoutComponent } from '../layout/layout.component';


const routes: Routes = [
  {
      path: '', component: LayoutComponent,
      children: [
          { path: 'app-login', component: LoginComponent },
          { path: 'app-register', component: RegisterComponent }
      ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})

export class AccountRoutingModule {}

