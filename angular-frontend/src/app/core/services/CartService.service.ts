import { Injectable } from '@angular/core';
import axios from 'axios';
import { BehaviorSubject, Observable } from 'rxjs';
import { OrderInfo, OrderInfoReturn } from '../models/User/UserRequest';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { LoginRequest } from '../models/User/UserRequest';
import { ProductModel } from '../models/Product/ProductModel';

@Injectable({
  providedIn: 'root',
})
export class CartService {
  cartItems: ProductModel[] = [];
  constructor() {
    this.cartItems = JSON.parse(localStorage.getItem('items') || '[]');
  }

  addToCart(product: ProductModel) {
    this.cartItems.push(product);
    this.UpdateCartSession();
  }

  removeCart(productId: number) {
    this.cartItems.filter((x) => x.ProductId != productId);
    this.UpdateCartSession();
  }

  get getItems() {
    return this.cartItems;
  }

  get cartLength(){
    return this.cartItems.length;
  }

  UpdateCartSession() {
    localStorage.setItem('items', JSON.stringify(this.cartItems));
  }
}
