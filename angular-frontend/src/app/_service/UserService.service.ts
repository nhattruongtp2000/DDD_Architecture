import { Injectable } from '@angular/core';
import axios from 'axios';
import { User } from '../login/login.component';
import { BehaviorSubject, Observable } from 'rxjs';
import { RegisterRequest, UpdatePasswordRequest, UserUpdateRequest,UserImageCreateRequest } from '../_model/User/UserRequest';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { LoginRequest } from '../_model/User/UserRequest';

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
                const userData= new User(data.firstName,data.lastName,data.email,data.password,data.token,data.imagePath) 
                localStorage.setItem('user',JSON.stringify(userData))
                this.userSubject.next(userData)
                return userData;
            }
            return null;
        })
    }

    public changeLocalUserInfo(data:User){
      const userData= new User(data.FirstName,data.LastName,data.Email,data.Password,data.Token,data.ImagePath) 
      localStorage.setItem('user',JSON.stringify(userData))
      this.userSubject.next(userData)
    }
    
    public getUserById( userId : string){
          return axios.post(this.url+userId).then(response=>{
            if(response.status=200)
            {
              var data=response.data;
                const userData= new User(data.fistname,data.lastName,data.email,data.password,data.token,data.ImagePath) 
                return userData;
            }
            return null;
          })
    }

    public UpdateUserByEmail(request: UserUpdateRequest)
    {
      console.log(this.urlUser+"/update-user")
      return axios.post(this.urlUser+"/update-user",request).then(response=>{
        if(response.status=200)
        {
          console.log(response)
          var data=response.data.data;
          const userData= new User(data.firstName,data.lastName,data.email,data.password,data.token,data.ImagePath) 
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
          const userData= new User(data.firstName,data.lastName,data.email,data.password,data.token,data.ImagePath) 
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
      console.log(request.getAll('ImageFile'))
      console.log(request.getAll('Email'))
      console.log(request.getAll('Caption'))
      console.log(request.getAll('IsDefault'))

      return axios.post(this.urlUser+"/upload-user-image",request).then(response=>{
        if(response.status=200)
        {
          var data2=response;
          var data=response.data.data;
          const userData= new User(data.firstName,data.lastName,data.email,data.password,data.token,data.imagePath) 
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