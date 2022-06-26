import { createAction, props } from "@ngrx/store";
import { IUser } from "../common/models/IUser";

export const initializeUser = createAction('[Global] Initialize User', props<{id : string}>());
export const initializeUserFail = createAction('[Global] Initialize User Fail', props<{error: string}>());
export const initializeUserSuccess = createAction('[Global] Initialize User Success', props<{user: IUser}>());

export const setLogin = createAction('[Global] Set Login', props<{login: string}>());
export const getLogin = createAction('[Global] Get Login');