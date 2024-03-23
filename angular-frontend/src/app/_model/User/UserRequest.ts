export interface RegisterRequest{
    Email:string,
    FirstName:string,
    LastName:string,
    Password:string,
    ConfirmPassword:string
}

export interface LoginRequest{
  Email:string,
  Password:string
}




export interface UserUpdateRequest{
  userUpdate :UserUpdateData
}

export interface UpdatePasswordRequest{
  Email:string,
  OldPassword:string,
  NewPassword:string,
  ConfirmNewPassword:string
}

export interface UserUpdateData{
  FirstName:string,
  LastName:string,
  Email:string
}

export interface AuthenticationResponse{
    FirstName:string;
    LastName:string;
    Email:string;
    Password:string;
    Token:string;
  }
  
  export interface User{
    FirstName:string;
    LastName:string;
    Email:string;
    Password:string;
    Token:string;
  }
    export interface UserImageCreateRequest{
      Email:string;
      Caption:string;
      IsDefault:boolean;
      ImageFile :File;
    }