import { Injectable } from '@angular/core';
import { User } from './login.component';
import { LoginResult } from './login.component';

@Injectable({
  providedIn: 'root'
})
export class LoginService {
  url ="https://localhost:7127/api/Authentication/login"
  constructor() { }

  async Login( user : User):Promise<LoginResult>{
    console.log(JSON.stringify(user))
    const  data=await fetch(this.url,{
      method:'POST',
      headers:{
        'Content-Type':'application/json'
      },
      body:JSON.stringify(user)
    });
    return await data.json() ??null;
  }
}
