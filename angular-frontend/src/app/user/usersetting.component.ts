import { Component, OnInit } from '@angular/core';
import { UserService } from '../_service/UserService.service';
import { User } from '../login/login.component';
import { FormGroup,FormBuilder,Validators } from '@angular/forms';
import { UpdatePasswordRequest, UserUpdateData, UserUpdateRequest,UserImageCreateRequest } from '../_model/User/UserRequest'; 

class ImageSnippet {
  constructor(public src: string, public file: File) {}
}
@Component({
  selector: 'app-usersetting',
  templateUrl: './usersetting.component.html',
  styleUrls: ['./usersetting.component.css']

})

export class UserSettingComponent implements OnInit {
  form!:FormGroup
  formPassword!:FormGroup
  selectedFile!: ImageSnippet
  constructor(private userService:UserService,private formBuilder:FormBuilder) {
    const user = this.userService.userValue
    this.userModel = new User(user!.FirstName,user!.LastName,user!.Email,user!.Password,user!.Token,user!.ImagePath)
  }
  userModel: User = new User("","","","","","");


  ngOnInit(): void {
    this.form=this.formBuilder.group({
      FirstName:[this.userModel.FirstName,Validators.required],
      LastName:[this.userModel.LastName,Validators.required],
      Email:[this.userModel.Email,Validators.required]
    })
    this.formPassword=this.formBuilder.group({
      OldPassword:["",Validators.required],
      NewPassword:["",Validators.required],
      ConfirmNewPassword:["",Validators.required]
    })
    }

    onSubmit(){
      const requestData: UserUpdateData={
        Email:this.form.value.Email,
        FirstName:this.form.value.FirstName,
        LastName:this.form.value.LastName
      }
      const request:UserUpdateRequest={
        userUpdate:requestData
      }
      var updateRequest= this.userService.UpdateUserByEmail(request).then(user=>{
        console.log(user)
        this.userModel = new User(user!.FirstName,user!.LastName,user!.Email,user!.Password,user!.Token,user!.ImagePath)
      })
    }

    onSubmit2(){
      const user = this.userService.userValue
      const requestUpdate:UpdatePasswordRequest={
        Email:user!.Email,
        OldPassword:this.formPassword.value.OldPassword,
        NewPassword:this.formPassword.value.NewPassword,
        ConfirmNewPassword:this.formPassword.value.ConfirmNewPassword,
      }
      var updateRequest=this.userService.UpdatePasswordByEmail(requestUpdate).then(user=>{
        console.log(user)
      })
    }

    processFile(imageInput: any){
      console.log(imageInput)
      const file: File = imageInput.files[0];
      const reader = new FileReader();
  
      reader.addEventListener('load', (event: any) => {
  
        const user = this.userService.userValue
        const requestUpload:UserImageCreateRequest={
          Email:user!.Email,
          Caption:"",
          IsDefault:this.formPassword.value.NewPassword,
          ImageFile:file,
        }
        this.userService.uploadImage(requestUpload).then(
          (user) => {
              this.userModel = new User(user!.FirstName,user!.LastName,user!.Email,user!.Password,user!.Token,user!.ImagePath)
          });
      });
  
      reader.readAsDataURL(file);
    }
}