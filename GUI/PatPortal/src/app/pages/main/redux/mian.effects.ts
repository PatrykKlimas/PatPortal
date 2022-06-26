import { Injectable } from "@angular/core";
import { Actions } from "@ngrx/effects";
import { PatPortalHttpService } from "src/app/common/services/patportalhttp.service";

@Injectable()
export class MainEffects{
    constructor(private actions$: Actions, private patPortalHttpService: PatPortalHttpService){}
}