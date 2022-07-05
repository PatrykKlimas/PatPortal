import { createAction, props } from "@ngrx/store";
import { IComment } from "src/app/common/models/IComment";
import { IPost } from "src/app/common/models/IPost";

const prefix: string = "[Main]"

export const initializePosts = createAction(`${prefix} Get Posts`, props<{userId: string}>());
export const initializePostsFail = createAction(`${prefix} Get Posts Fail`, props<{error: string}>());
export const initializePostsSuccess = createAction(`${prefix} Get Posts Success`, props<{posts: IPost[]}>());

export const initializeComments = createAction(`${prefix} Get Comments`, props<{postId: string}>());
export const initializeCommentsFail = createAction(`${prefix} Get Comments Fail`, props<{error: string}>());
export const initializeCommentsSuccess = createAction(`${prefix} Get Comments Success`, props<{comments: IComment[], postId : string}>());