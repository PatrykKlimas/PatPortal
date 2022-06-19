import { createFeatureSelector, createSelector } from "@ngrx/store";
import { IAppState } from "../state/app.state";
import { FeatureStates } from "../state/futureStates"
import { IUserState } from "./global.reducers";

export interface State extends IAppState {
    users: IUserState
}

const getGlobalFutureState = createFeatureSelector<IUserState>(FeatureStates.User);

export const getUser = createSelector(
    getGlobalFutureState,
    state => state.user
)

export const getErrorMessage = createSelector(
    getGlobalFutureState,
    state => state.errorMessage
)