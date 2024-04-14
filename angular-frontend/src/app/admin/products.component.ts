import { Component, OnInit } from '@angular/core';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { AddProductComponent } from './addproduct.component';
import { ProductService } from '../_service/ProductService.service';
import { ProductModel } from '../_model/Product/ProductModel';
@Component({
  selector: 'app-adminproducts',
  templateUrl: './products.component.html',
})
export class AdminProducts implements OnInit {
  constructor(private dialog: MatDialog, private service: ProductService) { }
  products: ProductModel[] = [];
  filterProducts: ProductModel[] = [];

  valueSearch: any;
  ngOnInit() {
    this.getAllProducts();
    this.products = [...this.products,
    new ProductModel(1, 'Product 1', 'Description 1', 'Content 1', 10, 'photo1.jpg',false),
    new ProductModel(2, 'Product 2', 'Description 2', 'Content 2', 20, 'photo2.jpg',false),
    new ProductModel(3, 'Product 3', 'Description 3', 'Content 3', 30, 'photo3.jpg',false)
    ]
    this.filterProducts = [...this.products]
  }

  openDialog(): void {
    const dialogRef: MatDialogRef<AddProductComponent, ProductModel> = this.dialog.open(AddProductComponent)
    dialogRef.afterClosed().subscribe((result: any | undefined) => {
      console.log(result?.data)
      if (result?.data != null) {


        const newProduct: ProductModel = new ProductModel(
          result?.data!.ProductId,
          result?.data!.ProductName,
          result?.data!.Description,
          result?.data!.Content,
          result?.data!.Price,
          result?.data!.PhotoReview,
          result?.data!.IsChecked // Assuming PhotoReview is part of the result
        );
        this.products.push(newProduct)
        this.filterProducts = [...this.products]
      }
    });



  }
  async getAllProducts() {
    var data = await this.service.GetAllProducts().then(data => {
      return data
    })
  }

  filterOnChange(e: any) {
    this.filterProducts = this.products.filter(x => x.ProductName.toLocaleLowerCase().includes(this.valueSearch.toLocaleLowerCase()))
  }

  editItem(item: ProductModel | any) {
    console.log(this.filterProducts)

    const dialogRef: MatDialogRef<AddProductComponent, ProductModel> = this.dialog.open(AddProductComponent)
    dialogRef.componentInstance.dataEdit = item;
    dialogRef.afterClosed().subscribe((result: any | undefined) => {

      console.log(result?.data)
      if (this.filterProducts.some(x => x.ProductId == result?.data.ProductId)) {
        console.log("vo")
        var editProduct = this.products.findIndex(x => x.ProductId == result?.data.ProductId);
        this.products[editProduct]=result?.data;
        this.filterProducts = [...this.products]
        return;
      }
    });
    console.log(item)
  }

  deleteItem(item: ProductModel | any) {
    this.products = this.products.filter(x => x.ProductId != item.ProductId);
    this.filterProducts = [...this.products]
  }

  removeItems(){
    this.filterProducts = this.filterProducts.filter(x => x.IsChecked ==false);
    this.products = [...this.filterProducts]
  }
  checkboxChanged(e:any){
    console.log(e.target.checked)
      if(e.target.checked===true){
         this.filterProducts.forEach(x=>x.IsChecked=true)
      }else
      {
        this.filterProducts.forEach(x=>x.IsChecked=false)
      }
  }
}
