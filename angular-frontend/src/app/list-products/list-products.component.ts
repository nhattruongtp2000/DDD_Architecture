import { Component, OnInit } from '@angular/core';
import { ProductModel } from '../_model/Product/ProductModel';
import { ProductService } from '../_service/ProductService.service';

@Component({
  selector: 'app-list-products',
  templateUrl: './list-products.component.html',
  styleUrls: ['./list-products.component.css']
})
export class ListProductsComponent implements OnInit {
  products : ProductModel [] = [];


  constructor(private productService:ProductService) {


   }

  ngOnInit(): void {
    var products = this.productService.GetAllProducts().then(dataReturn=>{
      this.products=[...dataReturn]
    })
  }


}
