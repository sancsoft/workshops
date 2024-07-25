import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, GuardResult, MaybeAsync, Router, RouterStateSnapshot } from '@angular/router';
import { AppService } from '../app.service';
import { map } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class RequireLoginGuard implements CanActivate {

  constructor(private appService: AppService, private router: Router) {}

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): MaybeAsync<GuardResult> {
    
    return this.appService.loggedIn$.pipe(
      map(loggedIn => loggedIn ? true : this.router.createUrlTree(['/unauthorized']))
    );
  }
  
}
