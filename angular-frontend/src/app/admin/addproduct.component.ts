import { Component,Input,OnInit } from "@angular/core";
import { MatDialogRef } from '@angular/material/dialog';
import { FormGroup,FormBuilder,Validators, Form } from "@angular/forms";
import { ProductModel } from "../_model/Product/ProductModel";


@Component({
    selector: 'app-addproduct',
    templateUrl: './addproduct.component.html',
  })
  export class AddProductComponent implements OnInit {
    form!:FormGroup;
    private _dataEdit :any;

    set dataEdit(value : ProductModel){
      // this.form=this.formBuilder.group({
      //   ProductName:[value.ProductName,Validators.required],
      //   Description:[value.Description,Validators.required],
      //   ProductId:[value.ProductId,Validators.required],
      //   Price:[value.Price,Validators.required],
      // })
      this._dataEdit=value;
    }

    get dataEdit():ProductModel{
      return this._dataEdit
    }

    constructor(private dialogRef:MatDialogRef<AddProductComponent>,
      private formBuilder:FormBuilder
    ){}
    ngOnInit() {
      var dataEdit=this.dataEdit;
      this.form=this.formBuilder.group({
        ProductName:[dataEdit?.ProductName == null || this.dataEdit.ProductName == undefined ?"abc":this.dataEdit.ProductName,Validators.required],
        Description:[dataEdit?.Description,Validators.required],
        ProductId:[dataEdit?.ProductId,Validators.required],
        Price:[dataEdit?.Price,Validators.required],
      })
    }
  
  
    closeDialog(): void {
        this.dialogRef.close();
      }
    
    onSubmit(){
      const productAdded : ProductModel={
        ProductName : this.form.value.ProductName,
        Description : this.form.value.Description,
        ProductId : this.form.value.ProductId,
        Price : this.form.value.Price,
        Content:"",
        PhotoReview:"",
        IsChecked:false
      };
      console.log(productAdded)
      this.dialogRef.close({data:productAdded});
    }
  }

