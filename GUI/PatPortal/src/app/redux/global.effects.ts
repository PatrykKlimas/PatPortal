import { Injectable } from "@angular/core";
import { Actions, createEffect, ofType } from "@ngrx/effects";
import { catchError, map, mergeMap, of, pipe } from "rxjs";
import { PatPortalHttpService } from "../common/services/patportalhttp.service";
import * as GlobalAsctions from "./global.actions";

@Injectable()
export class UserEffects{
    constructor(private actions$: Actions, private patPortalHttpService: PatPortalHttpService){}

    loadUser$ = createEffect(() => {
        return this.actions$
            .pipe(
                ofType(GlobalAsctions.initializeUser),
                mergeMap((props) => this.patPortalHttpService.getUser(props.id)
                    .pipe(
                        map(user => GlobalAsctions.initializeUserSuccess({user})),
                        catchError(error => of(GlobalAsctions.initializeUserFail({ error })))
                    )
            ))
    })
}