import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class NestedService {

  private counterSubject = new BehaviorSubject<number>(0);
  public counter$ = this.counterSubject.asObservable();

  constructor() {
    console.log('Hello from NestedService constructor!')
  }

  increment() {
    this.counterSubject.next(this.counterSubject.value + 1);
  }

  decrement() {
    this.counterSubject.next(this.counterSubject.value - 1);
  }

  reset() {
    this.counterSubject.next(0);
  }

}
