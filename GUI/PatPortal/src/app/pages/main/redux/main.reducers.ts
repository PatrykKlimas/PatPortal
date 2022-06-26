import { createReducer } from "@ngrx/store";
import { IPost } from "src/app/common/models/IPost";

export interface MainState {
    posts: IPost[]
}

const initState: MainState = {
    posts: []
}

export const mainReducer = createReducer(
    initState
);