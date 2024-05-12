import { Routes, RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AdminLayoutRoutingModule } from './admin-layout-routing.component';
import { AdminProductsComponent } from './admin-products.component';
import { AdminlayoutComponent } from './admin-layout.component';
import { AdminDaboardComponent } from './admin-daboard.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { AddProductComponent } from './admin-addproduct.component';

const routes :Routes=[

]

@NgModule({
    imports:[
        CommonModule,
        RouterModule,
        AdminLayoutRoutingModule,
        FormsModule,
        BrowserModule,
        ReactiveFormsModule
    ],
    declarations:[
        AdminProductsComponent,
        AdminlayoutComponent,
        AdminDaboardComponent,
        AddProductComponent
    ],
    exports:[

    ],
})
export class AdminLayoutModule{

}