import { NgModule } from "@angular/core";
import { EffectsModule } from "@ngrx/effects";
import { StoreModule } from "@ngrx/store";
import { FeatureStates } from "src/app/state/futureStates";
import { HomeComponent } from "./home/home.component";
import { MainComponent } from "./main.component";
import { mainReducer } from "./redux/main.reducers";
import { MainEffects } from "./redux/mian.effects";
import { UserComponent } from "./user/user.component";

@NgModule({
    imports: [
        StoreModule.forFeature(FeatureStates.Main, mainReducer),
        EffectsModule.forFeature([MainEffects])
    ],
    declarations: [
        MainComponent,
        UserComponent,
        HomeComponent
    ]
})
export class MainModule{}