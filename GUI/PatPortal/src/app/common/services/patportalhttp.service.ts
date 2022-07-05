import { HttpClient, HttpErrorResponse } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { catchError, map, Observable, tap, throwError } from "rxjs";
import { environment } from "src/environments/environment";
import { CommentDto } from "../dtos/CommentDto";
import { PostDto } from "../dtos/PostDto";
import { UserDto } from "../dtos/UserDto";
import { IComment } from "../models/IComment";
import { IPost } from "../models/IPost";
import { IUser } from "../models/IUser";
import { PatPortalMapper } from "./patportalmapper";

@Injectable({
  providedIn: 'root'
})

export class PatPortalHttpService {
  private url: string = environment.patPortalUrl;

  constructor(private http: HttpClient, private mapper: PatPortalMapper) { }

  public getUser(id: string): Observable<IUser> {
    var path = this.url + `user/${id}`;

    return this.http.get<UserDto>(path)
      .pipe(
        map(userDto => this.mapper.CreateUser(userDto)),
        catchError(this.handleError)
      );
  }

  public getPosts(userId: string) : Observable<IPost[]>{
    var path = this.url + `post/${userId}`;

    return this.http.get<PostDto[]>(path)
      .pipe(
        map(posts => posts.map(post => this.mapper.CreatePost(post))),
        catchError(this.handleError)
      );
  }

  public getComments(postId: string) : Observable<IComment[]>{
    var path = this.url + `post/${postId}/comments`;

    return this.http.get<CommentDto[]>(path)
      .pipe(
        map(comments => comments.map(comment => this.mapper.CreateComment(comment))),
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