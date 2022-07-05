import { createAction, props } from "@ngrx/store";
import { IUser } from "../common/models/IUser";

const prefix: string = "[Global]";

export const initializeUser = createAction(`${prefix} Initialize User`, props<{id : string}>());
export const initializeUserFail = createAction(`${prefix} Initialize User Fail`, props<{error: string}>());
export const initializeUserSuccess = createAction(`${prefix} Initialize User Success`, props<{user: IUser}>());

export const setLogin = createAction(`${prefix} Set Login`, props<{login: string}>());
export const getLogin = createAction(`${prefix} Get Login`);