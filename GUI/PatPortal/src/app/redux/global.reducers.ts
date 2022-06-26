import { createReducer, on } from "@ngrx/store";
import { ICredentails } from "../common/models/ICredentials";
import { IUser } from "../common/models/IUser";
import * as GlobalActions from "./global.actions";

export interface IUserState {
    user: IUser | null,
    credentails: ICredentails,
    errorMessage: string
}

const initState: IUserState = {
    user: null,
    credentails: {
        Login: ''
    },
    errorMessage: ''
}

export const globalReducer = createReducer(
    initState,
    on(GlobalActions.initializeUserSuccess, (state, props) : IUserState => {
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
    }),
    on(GlobalActions.setLogin, (state, props) => {
        return {
            ...state,
            credentails: {
                Login: props.login
            }
        }
    })
);

