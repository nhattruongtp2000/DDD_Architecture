import { Component,OnInit } from "@angular/core";
import { MatDialogRef } from '@angular/material/dialog';

@Component({
    selector: 'app-addproduct',
    templateUrl: './addproduct.component.html',
  })
  export class AddProductComponent implements OnInit {
    constructor(private dialogRef:MatDialogRef<AddProductComponent>){}
    ngOnInit() {}
  
  
    closeDialog(): void {
        this.dialogRef.close();
      }
    
      
  }

