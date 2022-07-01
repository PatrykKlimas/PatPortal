import { createFeatureSelector, createSelector } from "@ngrx/store";
import { IAppState } from "src/app/state/app.state";
import { MainState } from "./main.reducers";

export interface State extends IAppState {
    main: MainState
}

const getMainFeautureState = createFeatureSelector<MainState>('main');