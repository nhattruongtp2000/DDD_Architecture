import { Component, OnInit } from '@angular/core';
import { User } from '../authen/login.component';
import { UserService } from 'src/app/core/services/UserService.service';
import { FormGroup, FormBuilder, Validators,ReactiveFormsModule  } from '@angular/forms';
import {
  UpdatePasswordRequest,
  UserUpdateData,
  UserUpdateRequest,
  UserImageCreateRequest,
} from '../../core/models/User/UserRequest';

class ImageSnippet {
  constructor(public src: string, public file: File) {}
}
@Component({
  selector: 'usersetting',
  templateUrl: './usersetting.component.html',
  styleUrls: ['./usersetting.component.css'],
})
export class UserSettingComponent implements OnInit {
  form!: FormGroup;
  formPassword!: FormGroup;
  selectedFile!: ImageSnippet;
  constructor(
    private userService: UserService,
    private formBuilder: FormBuilder
  ) {
    const user = this.userService.userValue;
    console.log(user);
    this.userModel = new User();
    this.userModel.setValue(
      user!.FirstName,
      user!.LastName,
      user!.Email,
      user!.Password,
      user!.Token,
      user!.Address,
      'https://localhost:7127/api/Users/' + user!.ImagePath.slice(0, -4)
    );
  }
  userModel: User;

  ngOnInit(): void {
    this.form = this.formBuilder.group({
      FirstName: [this.userModel.FirstName, Validators.required],
      LastName: [this.userModel.LastName, Validators.required],
      Email: [this.userModel.Email, Validators.required],
      PhoneNumber: [this.userModel.PhoneNumber, Validators.required],
      Country: [this.userModel.Country, Validators.required],
      Address: [this.userModel.Address, Validators.required],
      City: [this.userModel.City, Validators.required],
      BirthDay: [this.userModel.BirthDay, Validators.required],
      Organization: [this.userModel.Organization, Validators.required],
      Role: [this.userModel.Role, Validators.required],
      Department: [this.userModel.Department, Validators.required],
      ZipCode: [this.userModel.ZipCode, Validators.required],
    });
    this.formPassword = this.formBuilder.group({
      OldPassword: ['', Validators.required],
      NewPassword: ['', Validators.required],
      ConfirmNewPassword: ['', Validators.required],
    });
  }

  onSubmit() {
    const requestData: UserUpdateData = {
      Email: this.form.value.Email,
      FirstName: this.form.value.FirstName,
      LastName: this.form.value.LastName,
      PhoneNumber: this.form.value.PhoneNumber,
      Country: this.form.value.Country,
      City: this.form.value.City,
      BirthDay: this.form.value.BirthDay,
      Organization: this.form.value.Organization,
      Role: this.form.value.Role,
      Address: this.form.value.Address,
      Department: this.form.value.Department,
      ZipCode: this.form.value.ZipCode,
    };
    console.log(requestData);
    const request: UserUpdateRequest = {
      userUpdate: requestData,
    };
    var updateRequest = this.userService
      .UpdateUserByEmail(request)
      .then((user) => {
        console.log(user);
        this.userModel = new User();
        this.userModel.setValue(
          user!.FirstName,
          user!.LastName,
          user!.Email,
          user!.Password,
          user!.Token,
          user!.Address,
          'https://localhost:7127/api/Users/' + user!.ImagePath.slice(0, -4)
        );
      });
  }

  onSubmit2() {
    const user = this.userService.userValue;
    const requestUpdate: UpdatePasswordRequest = {
      Email: user!.Email,
      OldPassword: this.formPassword.value.OldPassword,
      NewPassword: this.formPassword.value.NewPassword,
      ConfirmNewPassword: this.formPassword.value.ConfirmNewPassword,
    };
    var updateRequest = this.userService
      .UpdatePasswordByEmail(requestUpdate)
      .then((user) => {
        console.log(user);
      });
  }

  processFile(imageInput: any) {
    const file: File = imageInput.files[0];
    const reader = new FileReader();
    console.log(file);

    reader.addEventListener('load', (event: any) => {
      const user = this.userService.userValue;
      const requestUpload: UserImageCreateRequest = {
        Email: user!.Email,
        Caption: 'true',
        IsDefault: true,
        ImageFile: file,
      };
      var userReturn = this.userService
        .uploadImage(requestUpload)
        .then((user) => {
          this.userModel.setImage('https://localhost:7127/api/Users/' + user!.ImagePath.slice(0, -4))
          console.log(this.userModel)

          this.userService.changeLocalUserInfo(user as any);
        });
    });
    reader.readAsDataURL(file);
  }
}
