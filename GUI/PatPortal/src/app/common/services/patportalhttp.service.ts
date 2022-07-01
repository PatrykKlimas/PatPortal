import { HttpClient, HttpErrorResponse } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { catchError, map, Observable, tap, throwError } from "rxjs";
import { environment } from "src/environments/environment";
import { UserDto } from "../dtos/UserDto";
import { IUser } from "../models/IUser";
import { PatPortalMapper } from "./patportalmapper";

@Injectable({
    providedIn: 'root'
})

export class PatPortalHttpService{
    private url: string = environment.patPortalUrl;

    constructor(private http: HttpClient,private mapper: PatPortalMapper){}

    public  getUser(id: string) : Observable<IUser> {
        var path = this.url + `user/${id}`;

        return this.http.get<UserDto>(path)
            .pipe(
                map(userDto => this.mapper.Create(userDto)),
                catchError(this.handleError)
            );
    }

    private handleError(err: HttpErrorResponse): Observable<never> {
        let errorMessage: string;
        
        if (err.error instanceof ErrorEvent) {
          errorMessage = `An error occurred: ${err.error.message}`;
        } else {
          errorMessage = `Backend returned code ${err.status}: ${err.message}`;
        }
        console.error(err);
        return throwError(() => errorMessage);
      }
}