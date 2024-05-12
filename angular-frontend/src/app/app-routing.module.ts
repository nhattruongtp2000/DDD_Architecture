import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './features/home/home.component';
import { AuthGuard } from './core/helpers/auth.guard';
import { UserComponent } from './features/user/user.component';
import { UserSettingComponent } from './features/user/usersetting.component';
import { TransactionComponent } from './features/transaction/transaction.component';
import { ListProductsComponent } from './features/list-products/list-products.component';
import { AdminLayoutModule } from './features/admin/admin-layout.module';
import { AuthenModule } from './features/authen/authen.module';
import { PaymentModule } from './features/payment/payment.module';


const routes: Routes = [
  {
    path: '',
    component: HomeComponent,
    canActivate: [AuthGuard],
  },
  { path: 'transaction', component: TransactionComponent },
  { path: 'user', component: UserComponent, canActivate: [AuthGuard] },
  { path: 'usersetting',component: UserSettingComponent,canActivate: [AuthGuard],},
   {path: 'list-products',component:ListProductsComponent},
  { path: '**', redirectTo: '' },
];

@NgModule({
  imports: [
    AdminLayoutModule,
    AuthenModule,
    PaymentModule,
    RouterModule.forRoot(routes)
  ],
  exports: [RouterModule],
})

export class AppRoutingModule {}
