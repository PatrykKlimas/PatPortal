import { createReducer, on } from "@ngrx/store";
import { IComment } from "src/app/common/models/IComment";
import { IPost } from "src/app/common/models/IPost";
import * as MainActions from "./main.actions";

export interface MainState {
    posts: IPost[]
}

const initState: MainState = {
    posts: []
}

export const mainReducer = createReducer(
    initState,
    on(MainActions.initializePostsSuccess, (state, props) => {
        return {
            ...state,
            posts: props.posts
        }
    }),
    on(MainActions.initializePostsFail, (state, props) => {
        console.log(props.error);
        return state;
    }),
    on(MainActions.initializeCommentsSuccess, (state, props) => {
        const post = state.posts.find(p => p.Id === props.postId);

        if (post === undefined)
            return state;

        var newPost: IPost = {
            ...post,
            Comments: props.comments,
            AreCommentsLoaded: true
        }

        let posts = state.posts.map(p => p.Id !== props.postId ? p : newPost);

        return {
            ...state,
            posts: posts
        }

    }),
    on(MainActions.initializeCommentsFail, (state, props) => {
        console.log(props.error);
        return state;
    }),
    on(MainActions.initializePostCommentFail, (state, props) => {
        console.log(props.error);
        return state;
    }),
    on(MainActions.getCommentFail, (state, props) => {
        console.log(props.error);
        return state;
    }),
    on(MainActions.getCommentSuccess, (state, props) => {
        var post = state.posts.find(post => post.Id == props.comment.PostId);
        if (post === undefined)
            return state

        var comments = post?.Comments.filter(c => c.Id !== props.comment.Id);
        comments?.push(props.comment);
        var newPost: IPost = {
            ...post,
            Comments: comments,
            AreCommentsLoaded: true
        }

        return {
            ...state,
            posts: state.posts.map(p => p.Id === newPost?.Id ? newPost : p)
        }
    })
);