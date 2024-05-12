import { Injectable } from '@angular/core';
import axios from 'axios';
import { BehaviorSubject, Observable } from 'rxjs';
import { RegisterRequest, UpdatePasswordRequest, UserUpdateRequest,UserImageCreateRequest } from '../models/User/UserRequest';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { LoginRequest } from '../models/User/UserRequest';
import { User } from 'src/app/features/authen/login.component';
@Injectable({
  providedIn: 'root'
})
export class UserService{
    url ="https://localhost:7127/api/Authentication"
    urlUser ="https://localhost:7127/api/Users"


    private userSubject: BehaviorSubject<User | null>;
    public user: Observable<User | null>;

    constructor(
      private router: Router,
      private http: HttpClient
    ) {
      this.userSubject = new BehaviorSubject(JSON.parse(localStorage.getItem('user')!));
      this.user = this.userSubject.asObservable();
    }

    public get userValue() {
    return this.userSubject.value;
    }
    public register(request:RegisterRequest ){
        return axios.post(this.url+"/register",request)
    }
    public login(request:LoginRequest)
    {
        return axios.post(this.url+"/login",request).then(response=>{
            if(response.status=200)
            {
                var data=response.data;
                const userData= new User() 
                userData.setValue(data.firstName,data.lastName,data.email,data.password,data.token,data.Address,data.imagePath)
                localStorage.setItem('user',JSON.stringify(userData))
                this.userSubject.next(userData)
                return userData;
            }
            return null;
        })
    }

    public changeLocalUserInfo(data:User){
      const userData= new User()
      userData.setValue(data.FirstName,data.LastName,data.Email,data.Password,data.Token,data.Address,data.ImagePath) 
      localStorage.setItem('user',JSON.stringify(userData))
      this.userSubject.next(userData)
    }
    
    public getUserById( userId : string){
          return axios.post(this.url+userId).then(response=>{
            if(response.status=200)
            {
              var data=response.data;
                const userData= new User()
                userData.setValue(data.FirstName,data.LastName,data.Email,data.Password,data.Token,data.Address,data.ImagePath) 
                return userData;
            }
            return null;
          })
    }

    public UpdateUserByEmail(request: UserUpdateRequest)
    {
      return axios.post(this.urlUser+"/update-user",request).then(response=>{
        if(response.status=200)
        {
          console.log(response)
          var data=response.data.data;
          const userData= new User() 
          userData.setValue(data.firstName,data.lastName,data.email,data.password,data.token,data.address,data.imagePath) 
          localStorage.setItem('user',JSON.stringify(userData))
          this.userSubject.next(userData)
          return userData;
        }
        return null;
      })
    }

    public UpdatePasswordByEmail(request: UpdatePasswordRequest)
    {
      return axios.post(this.urlUser+"/update-password",request).then(response=>{
        if(response.status=200)
        {
          var data=response.data.data;
          const userData= new User() 
          userData.setValue(data.FirstName,data.LastName,data.Email,data.Password,data.Token,data.Address,data.ImagePath) 
        //  localStorage.setItem('user',JSON.stringify(userData))
         // this.userSubject.next(userData)
          return userData;
        }
        return null;
      })
    }
    
    public uploadImage(requestUpload: UserImageCreateRequest) {
      const request = new FormData();
      request.append('ImageFile', requestUpload.ImageFile);
      request.append('Email', requestUpload.Email);
      request.append('Caption', requestUpload.Caption);
      request.append('IsDefault', String(requestUpload.IsDefault));


      return axios.post(this.urlUser+"/upload-user-image",request).then(response=>{
        if(response.status=200)
        {
          var data=response.data.data;
           console.log(data)
          const userData= new User() 
          userData.setValue(data.firstName,data.lastName,data.email,data.password,data.token,data.address,data.imagePath) 
        //  localStorage.setItem('user',JSON.stringify(userData))
         // this.userSubject.next(userData)
          return userData;
        }
        return null;
      })
    }

    logout(){
        localStorage.removeItem('user');
        this.userSubject.next(null);
        this.router.navigate(['/app-login']);
    }
}