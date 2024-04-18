import { Component, OnInit } from '@angular/core';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { AddProductComponent } from './addproduct.component';
import { ProductService } from '../_service/ProductService.service';
import { ProductModel } from '../_model/Product/ProductModel';
import { AddProductRequest } from '../_model/Product/ProductModel';

@Component({
  selector: 'app-adminproducts',
  templateUrl: './products.component.html',
})
export class AdminProducts implements OnInit {
  constructor(private dialog: MatDialog, private service: ProductService) {}
  products: ProductModel[] = [];
  filterProducts: ProductModel[] = [];

  valueSearch: any;
  async ngOnInit() {
    await this.getAllProducts();
  }

  openDialog(): void {
    const dialogRef: MatDialogRef<AddProductComponent, ProductModel> =
      this.dialog.open(AddProductComponent);
    dialogRef.afterClosed().subscribe((result: any | undefined) => {
      console.log(result?.data);
      if (result?.data != null) {
        const newProduct: AddProductRequest = new AddProductRequest(
          result?.data!.ProductName,
          result?.data!.Description,
          result?.data!.Content,
          result?.data!.Price,
          result?.data!.PhotoReview
        );
        console.log(newProduct)
        this.AddNewProduct(newProduct);
      }
    });
  }
  async getAllProducts() {
    var data = await this.service.GetAllProducts().then((data) => {
      data.forEach((element) => {
        element.PhotoReview =
          'https://localhost:7127/api/Products/image/' + element.PhotoReview ;
      });
      this.products = [...data];
      this.filterProducts = [...data];
    });
  }

  filterOnChange(e: any) {
    this.filterProducts = this.products.filter((x) =>
      x.ProductName.toLocaleLowerCase().includes(
        this.valueSearch.toLocaleLowerCase()
      )
    );
  }

  editItem(item: ProductModel | any) {
    const dialogRef: MatDialogRef<AddProductComponent, ProductModel> =
      this.dialog.open(AddProductComponent);
    dialogRef.componentInstance.dataEdit = item;
    dialogRef.afterClosed().subscribe((result: any | undefined) => {
      console.log(result?.data);
      if (
        this.filterProducts.some((x) => x.ProductId == result?.data.ProductId)
      ) {
        var editProduct = this.products.findIndex(
          (x) => x.ProductId == result?.data.ProductId
        );
        this.products[editProduct] = result?.data;
        this.filterProducts = [...this.products];
        return;
      }
    });
    console.log(item);
  }

  async deleteItem(item: ProductModel | any) {
   await this.service.DeleteProduct(item.ProductId).then(result=>{
    console.log(result)
      if(result=true)
      {
        this.products = this.products.filter((x) => x.ProductId != item.ProductId);
        this.filterProducts = [...this.products];
      }
   })
  }

  removeItems() {
    this.filterProducts = this.filterProducts.filter(
      (x) => x.IsChecked == false
    );
    this.products = [...this.filterProducts];
  }
  checkboxChanged(e: any) {
    console.log(e.target.checked);
    if (e.target.checked === true) {
      this.filterProducts.forEach((x) => (x.IsChecked = true));
    } else {
      this.filterProducts.forEach((x) => (x.IsChecked = false));
    }
  }
 async AddNewProduct(newProduct: AddProductRequest) {
   await this.service.AddNewProduct(newProduct).then(reponse=>{
      if(reponse.data=true){
        this.getAllProducts()
      }
    })
    
  
  }
  
}
