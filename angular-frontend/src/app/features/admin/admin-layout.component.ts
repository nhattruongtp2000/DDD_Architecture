import { Component, OnInit,HostListener  } from '@angular/core';
import { Router } from '@angular/router';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { RouterModule } from '@angular/router';
@Component({
  selector: 'admin-layout',
  templateUrl: './admin-layout.component.html',
})
export class AdminlayoutComponent implements OnInit {
  ngOnInit() {}
  isOpen: boolean = false;


 
  changeDropDown(){
    this.isOpen = !this.isOpen;
    if(this.isOpen)
    {
      console.log("ok")
    }
  }
}
