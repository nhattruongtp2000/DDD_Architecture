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


const accountModule = () => import('./account/account.module').then(x => x.AccountModule);
const routes: Routes = [
  {
     path: '', component: HomeComponent, canActivate: [AuthGuard] },
     { path: 'account', loadChildren: accountModule },
     { path: 'app-login', component: LoginComponent },
     { path: 'app-register', component: RegisterComponent },
     { path: 'app-transaction', component: TransactionComponent },
     { path: 'app-user', component: UserComponent, canActivate:[AuthGuard]},
     { path: 'app-usersetting', component: UserSettingComponent, canActivate:[AuthGuard]},
     { path: 'app-payment', component: PaymentComponent},


      // otherwise redirect to home
    { path: '**', redirectTo: '' }
];
@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
