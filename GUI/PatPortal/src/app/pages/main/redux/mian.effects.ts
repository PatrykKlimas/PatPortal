import { Injectable } from "@angular/core";
import { Actions, createEffect, ofType } from "@ngrx/effects";
import { catchError, concatMap, map, merge, mergeMap, of, switchMap } from "rxjs";
import { PostCommentRequestModel } from "src/app/common/Components/requestModels/postCommentRequestModel";
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

    $postComment = createEffect(() => {
        return this.actions$.pipe(
            ofType(MainActions.initializePostComment),
            switchMap(props => {
                var request: PostCommentRequestModel = {
                    ownerId: props.ownerId,
                    content: props.content
                };
                return this.patPortalHttpService.postComment(props.postId, request)
                    .pipe(
                        map(commentId => MainActions.getComment({ commentId })),
                        catchError(error => of(MainActions.initializePostCommentFail({ error: error })))
                    )
            }
            ))
    });

    $getComment = createEffect(() => {
        return this.actions$.pipe(
            ofType(MainActions.getComment),
            mergeMap(props => this.patPortalHttpService.getComment(props.commentId)
                .pipe(
                    map(comment => MainActions.getCommentSuccess({ comment: comment })),
                    catchError(error => of(MainActions.getCommentFail({ error: error })))
                )
            ))
    });

}