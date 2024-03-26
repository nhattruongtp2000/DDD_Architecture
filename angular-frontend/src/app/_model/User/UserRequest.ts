export interface RegisterRequest {
  Email: string;
  FirstName: string;
  LastName: string;
  Password: string;
  ConfirmPassword: string;
}

export interface LoginRequest {
  Email: string;
  Password: string;
}

export class OrderInfo {
  OrderId !: string;
  TotalAmount !: number;
  OrderContent !: string;
  CreatedDate !: Date;
  Status !: string;
  PaymentTranId !: number;
  BankCode !: string;
  PayStatus !: string;
  Language !: string;
  constructor(){

  }
  setValue(orderId:string,totalAmount:number,orderContent:string,createdDate:Date,status:string,paymentTranId:number,bankCode:string,payStatus:string,language:string){
    this.OrderId=orderId,
    this.TotalAmount=totalAmount,
    this.OrderContent=orderContent,
    this.CreatedDate=createdDate,
    this.Status=status,
    this.PaymentTranId=paymentTranId,
    this.BankCode=bankCode,
    this.PayStatus=payStatus,
    this.Language=language
  }
}

export interface UserUpdateRequest {
  userUpdate: UserUpdateData;
}

export interface UpdatePasswordRequest {
  Email: string;
  OldPassword: string;
  NewPassword: string;
  ConfirmNewPassword: string;
}

export interface UserUpdateData {
  FirstName: string;
  LastName: string;
  Email: string;
  PhoneNumber: string;
  Address: string;
  Country: string;
  City: string;
  BirthDay: Date;
  Organization: string;
  Role: string;
  Department: string;
  ZipCode: string;
}

export interface AuthenticationResponse {
  FirstName: string;
  LastName: string;
  Email: string;
  Password: string;
  Token: string;
}

export interface User {
  FirstName: string;
  LastName: string;
  Email: string;
  Password: string;
  Token: string;
}
export interface UserImageCreateRequest {
  Email: string;
  Caption: string;
  IsDefault: boolean;
  ImageFile: File;
}
