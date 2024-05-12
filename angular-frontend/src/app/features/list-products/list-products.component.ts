import { Component, OnInit } from '@angular/core';
import { CartService } from '../../core/services/CartService.service';
import { ProductModel } from 'src/app/core/models/Product/ProductModel';
import { ProductService } from 'src/app/core/services/ProductService.service';

@Component({
  selector: 'app-list-products',
  templateUrl: './list-products.component.html',
  styleUrls: ['./list-products.component.css'],
})
export class ListProductsComponent implements OnInit {
  products: ProductModel[] = [];

  constructor(
    private productService: ProductService,
    private cartService: CartService
  ) {}

  ngOnInit(): void {
    var products = this.productService.GetAllProducts().then((dataReturn) => {
      dataReturn.forEach((element) => {
        element.PhotoReview =
          'https://localhost:7127/api/Products/image/' + element.PhotoReview;
      });
      this.products = [...dataReturn];
    });
  }

  
  addToCart(product: ProductModel) {
    this.cartService.addToCart(product);
  }

  removeItems(productId:number){
    this.cartService.removeCart(productId)
  }
}
