import { Injectable } from '@angular/core';
import axios from 'axios';
import { OrderInfo, OrderInfoReturn } from '../_model/User/UserRequest';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { LoginRequest } from '../_model/User/UserRequest';
import { ProductModel } from '../_model/Product/ProductModel';

@Injectable({
  providedIn: 'root',
})
export class ProductService {
  url = 'https://localhost:7127/api/Products';

  constructor(private router: Router, http: HttpClient) {}

  public async GetAllProducts() {
    const response = await axios.get(this.url);
    const responseData = response.data.data;
    const products: ProductModel[] = responseData.map((productData: any) => {
      return new ProductModel(
          productData.productId,
          productData.productName,
          productData.description,
          productData.content,
          productData.price,
          productData.PhotoReview
      );
  });
  return products;
  }
}
