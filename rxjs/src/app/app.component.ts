import { Component, OnDestroy, OnInit } from '@angular/core';
import { NameService } from './name.service';
import { ActivatedRoute } from '@angular/router';
import { Observable, Subject, combineLatest, debounceTime, distinctUntilChanged, filter, map, shareReplay, switchMap, takeUntil } from 'rxjs';
import { HttpClient } from '@angular/common/http';

interface AgifyResponse {
  age: number;
  count: number;
  name: string;
}

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit, OnDestroy {
  
  private onDestroy$ = new Subject<void>();

  public firstName$: Observable<string>;
  public lastName$: Observable<string>;
  
  public initials$: Observable<string>;
  public fullName$: Observable<string>;

  public age$: Observable<number>;

  constructor(private nameService: NameService, private route: ActivatedRoute, private httpClient: HttpClient) {
    const firstName$ = this.nameService.firstName$;
    const lastName$ = this.nameService.lastName$;

    this.firstName$ = firstName$;
    this.lastName$ = lastName$;

    const firstInitial$ = firstName$.pipe(
      map(t => t[0] ?? '')
    );

    const lastInitial$ = lastName$.pipe(
      map(t => t[0] ?? '')
    );

    // Combine first and last initials to get initials
    this.initials$ = combineLatest([firstInitial$, lastInitial$]).pipe(
      debounceTime(500),
      map(([firstInitial, lastInitial]) => firstInitial + lastInitial)
    );

    // Combine firstName nad lastName to get full name
    this.fullName$ = combineLatest([firstName$, lastName$]).pipe(
      debounceTime(500),
      map(([firstName, lastName]) => `${firstName} ${lastName}`)
    );

    // Subscribe to fullName and log to console
    this.fullName$.pipe(
      takeUntil(this.onDestroy$)
    ).subscribe(fullName => console.log(fullName));

    const agify$ = firstName$.pipe(
      filter(t => !!t),
      debounceTime(500),
      distinctUntilChanged(),
      switchMap(firstName => this.httpClient.get<AgifyResponse>(`https://api.agify.io/?name=${firstName}`)),
      shareReplay(1)
    );

    this.age$ = agify$.pipe(
      map(t => t.age)
    );
  }

  async ngOnInit() {
    
  }

  async ngOnDestroy() {
    this.onDestroy$.next();
    this.onDestroy$.complete();
  }

  setFirstName(firstName: string) {
    this.nameService.setFirstName(firstName);
  }

  setLastName(lastName: string) {
    this.nameService.setLastName(lastName);
  }

}
