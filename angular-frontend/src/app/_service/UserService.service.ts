import { Injectable } from '@angular/core';
import axios from 'axios';
import { User } from '../login/login.component';
import { BehaviorSubject, Observable } from 'rxjs';
import { RegisterRequest } from '../_model/User/UserRequest';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { LoginRequest } from '../_model/User/UserRequest';

@Injectable({
  providedIn: 'root'
})
export class UserService{
    url ="https://localhost:7127/api/Authentication"

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
                const userData= new User(data.fistname,data.lastName,data.email,data.password,data.token) 
                localStorage.setItem('user',JSON.stringify(userData))
                this.userSubject.next(userData)
                return userData;
            }
            return null;
        })
    }

    public getUserById( userId : string){
          return axios.post(this.url+userId).then(response=>{
            if(response.status=200)
            {
              var data=response.data;
                const userData= new User(data.fistname,data.lastName,data.email,data.password,data.token) 
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