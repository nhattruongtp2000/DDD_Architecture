import { Injectable } from '@angular/core';
import { Subject,BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class DataService {
  private emailSource = new BehaviorSubject('default message');
  email = this.emailSource.asObservable();

  

  sendData(data: any) {
    this.emailSource.next(data);
  }
}