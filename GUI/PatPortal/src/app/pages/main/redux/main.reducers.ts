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
    on(MainActions.initializeCommentsSuccess, (state, props) =>{
        const post = state.posts.find(p => p.Id === props.postId);

        if(post === undefined)
            return state;

        var newPost : IPost = {
            ...post,
            Comments: props.comments,
            AreCommentsLoaded: true
        }

        let posts = state.posts.map(p => p.Id !== props.postId ? p : newPost);

        return {
            ...state,
            posts : posts
        }

    }),
    on(MainActions.initializeCommentsFail, (state, props) => {
        console.log(props.error);
        return state;
    }),
);