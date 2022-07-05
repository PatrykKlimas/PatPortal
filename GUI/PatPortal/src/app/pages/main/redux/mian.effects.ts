import { Injectable } from "@angular/core";
import { Actions, createEffect, ofType } from "@ngrx/effects";
import { catchError, map, mergeMap, of } from "rxjs";
import { PatPortalHttpService } from "src/app/common/services/patportalhttp.service";
import * as MainActions from "../redux/main.actions";

@Injectable()
export class MainEffects {
    constructor(private actions$: Actions, private patPortalHttpService: PatPortalHttpService) { }

    $getPosts = createEffect(() => {
        return this.actions$.pipe(
            ofType(MainActions.initializePosts),
            mergeMap(props => this.patPortalHttpService.getPosts(props.userId)
                .pipe(
                    map(posts => MainActions.initializePostsSuccess({ posts: posts })),
                    catchError(error => of(MainActions.initializePostsFail({ error: error })))
                ))
        );
    });

    $getComments = createEffect(() => {
        return this.actions$.pipe(
            ofType(MainActions.initializeComments),
            mergeMap(props => this.patPortalHttpService.getComments(props.postId)
                .pipe(
                    map(comments => MainActions.initializeCommentsSuccess({ comments: comments, postId: props.postId })),
                    catchError(error => of(MainActions.initializeCommentsFail({ error: error })))
                )
            ))
    });
}