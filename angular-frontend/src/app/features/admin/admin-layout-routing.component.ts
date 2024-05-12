import { Routes,RouterModule } from "@angular/router"
import { AdminlayoutComponent } from "./admin-layout.component"
import { AdminProductsComponent } from "./admin-products.component"
import { AdminDaboardComponent } from "./admin-daboard.component"
import { AddProductComponent } from "./admin-addproduct.component"
import { NgModule } from "@angular/core"

const routes:Routes=[
    {
        path:'admin',
        component:AdminlayoutComponent,
        children:[
            {path:'products',component:AdminProductsComponent},
            {path:'daboard',component:AdminDaboardComponent},
            {path:'add-product',component:AddProductComponent}
        ]
    }

]
@NgModule({
    imports:[RouterModule.forChild(routes)],
    exports:[RouterModule]
})
export class AdminLayoutRoutingModule{}