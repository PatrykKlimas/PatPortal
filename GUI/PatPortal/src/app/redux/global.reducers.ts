import { createReducer, on } from "@ngrx/store";
import { IUser } from "../common/models/IUser";
import * as GlobalActions from "./global.actions";

export interface IUserState {
    user: IUser | null,
    errorMessage: string
}

const initState: IUserState = {
    user: null,
    errorMessage: ''
}

export const globalReducer = createReducer(
    initState,
    on(GlobalActions.initializeUserSuccess, (state, props) : IUserState => {
        console.log(props.user);
        return {
            ...state,
            errorMessage: '',
            user: props.user
        }
    }),
    on(GlobalActions.initializeUserFail, (state, props) => {
        return {
            ...state,
            user: null,
            errorMessage: props.error
        }
    })
);

