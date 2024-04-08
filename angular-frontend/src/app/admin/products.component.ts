import { Component, OnInit  } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { AddProductComponent } from './addproduct.component';
import { ProductService } from '../_service/ProductService.service';
import { ProductModel } from '../_model/Product/ProductModel';
@Component({
  selector: 'app-adminproducts',
  templateUrl: './products.component.html',
})
export class AdminProducts implements OnInit {
  constructor(private dialog: MatDialog,private service:ProductService) {}
   products: ProductModel[] = [];
  ngOnInit() {
    this.getAllProducts();
  }
  
  openDialog():void{
    this.dialog.open(AddProductComponent)
  }
  async getAllProducts(){
    var data =await this.service.GetAllProducts().then(data=>{
      return data
    })
    console.log(data)
  }
}
