import { Injectable } from "@angular/core";
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot } from "@angular/router";
import { Store } from "@ngrx/store";
import { map, Observable, tap } from "rxjs";
import * as GlobalSelectors from "../../redux/globas.selectors";
import { State } from "../../redux/globas.selectors";


@Injectable({
  providedIn: 'root'
})
export class MainGuard implements CanActivate {

  constructor(private router: Router, private store: Store<State>) {
  }

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean> {

    return this.store.select(GlobalSelectors.getUser)
      .pipe(
        tap(user => {
          if (user === null) this.router.navigate(["/login"])
        }),
        map(user => user != null ? true : false)
      );
  }

}
