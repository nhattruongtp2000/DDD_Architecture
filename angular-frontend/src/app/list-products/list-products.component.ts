import { Component, OnInit } from '@angular/core';
import { ProductModel } from '../_model/Product/ProductModel';

@Component({
  selector: 'app-list-products',
  templateUrl: './list-products.component.html',
  styleUrls: ['./list-products.component.css']
})
export class ListProductsComponent implements OnInit {
  products : ProductModel [] = [];


  constructor() { }

  ngOnInit(): void {
    this.products = [...this.products,
      new ProductModel(1, 'Kệ Chén Bát Nâng Hạ Garis Inox 304', 'Description 1', 'Content 1', 5.19522222, 'photo1.jpg',false),
      new ProductModel(2, 'Product 2', 'Description 2', 'Content 2', 5.19522222, 'photo2.jpg',false),
      new ProductModel(3, 'Product 3', 'Description 3', 'Content 3', 5.19522222, 'photo3.jpg',false)
      ]
  }
}
