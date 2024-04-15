import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AppComponent } from './app.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { HomeComponent } from './home/home.component';
import { AuthGuard } from './_helper/auth.guard';
import { UserComponent } from './user/user.component';
import { UserSettingComponent } from './user/usersetting.component';
import { TransactionComponent } from './transaction/transaction.component';
import { PaymentComponent } from './payment/payment.component';
import { PaymentReturnComponent } from './payment/payment-return.component';
import { AdminlayoutComponent } from './admin/adminlayout.component';
import { AdminDaboardComponent } from './admin-daboard/admin-daboard.component';
import { AdminProducts } from './admin/products.component';
import { AddProductComponent } from './admin/addproduct.component';
import { ListProductsComponent } from './list-products/list-products.component';
const accountModule = () =>
  import('./account/account.module').then((x) => x.AccountModule);
const routes: Routes = [
  {
    path: '',
    component: HomeComponent,
    canActivate: [AuthGuard],
  },
  { path: 'account', loadChildren: accountModule },
  { path: 'app-login', component: LoginComponent },
  { path: 'app-register', component: RegisterComponent },
  { path: 'app-transaction', component: TransactionComponent },
  { path: 'app-user', component: UserComponent, canActivate: [AuthGuard] },
  {
    path: 'app-usersetting',
    component: UserSettingComponent,
    canActivate: [AuthGuard],
  },
  {path:'app-list-products',component:ListProductsComponent},
  { path: 'app-payment', component: PaymentComponent },
  { path: 'app-payment-return', component: PaymentReturnComponent },
  {
    path: "admin", component: AdminlayoutComponent, children: [
      { path: 'app-admin-daboard', component: AdminDaboardComponent, pathMatch: 'full',  },
      { path: 'app-adminproducts', component: AdminProducts, pathMatch: 'full',  },
      { path: 'app-addproduct', component: AddProductComponent, pathMatch: 'full'  },
    
    ]
  },
  // otherwise redirect to home
  { path: '**', redirectTo: '' },
];
@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
