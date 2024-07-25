import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AppService {

  private loggedInSubject = new BehaviorSubject<boolean>(false);
  public loggedIn$ = this.loggedInSubject.asObservable();

  constructor() {
    console.log('Hello from AppService constructor!')
  }

  login() {
    this.loggedInSubject.next(true);
  }

  logout() {
    this.loggedInSubject.next(false);
  }

}
