import { Injectable } from '@angular/core';
import axios from 'axios';
import { OrderInfo,OrderInfoReturn } from '../models/User/UserRequest';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { LoginRequest } from '../models/User/UserRequest';
import { AddProductRequest,ProductModel } from '../models/Product/ProductModel';
import { Observable } from 'rxjs';

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
        productData.photoReview,
        productData.isChecked
      );
    });
    return products;
  }

  async AddNewProduct(product: AddProductRequest) {
    const request = new FormData();
    request.append('ProductName', product.ProductName);
    request.append('Description', product.Description);
    request.append('Content', product.Content);
    request.append('Price', String(product.Price));
    request.append('PhotoReview', product.PhotoReview);

    return await axios
      .post(this.url + '/add-product', request)
      .then((response) => {
        if ((response.status = 200)) {
          return response.data
        }
      });
  }

  async DeleteProduct(productId: number) {
    return await axios.delete(`${this.url}/${productId}`).then((response) => {
      return response.data
    });
  }
}
