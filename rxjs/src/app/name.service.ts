import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class NameService {

  private firstNameSubject$ = new BehaviorSubject('');
  private lastNameSubject$ = new BehaviorSubject('');

  public firstName$ = this.firstNameSubject$.asObservable();
  public lastName$ = this.lastNameSubject$.asObservable();

  constructor() { }

  setFirstName(firstName: string) {
    this.firstNameSubject$.next(firstName);
  }

  setLastName(lastName: any) {    
    this.lastNameSubject$.next(lastName);
  }

}
