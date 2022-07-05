import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { FormsModule } from "@angular/forms";
import { RouterModule } from "@angular/router";
import { EffectsModule } from "@ngrx/effects";
import { StoreModule } from "@ngrx/store";
import { CommentComponent } from "src/app/common/Components/Comment/comment.component";
import { InputBoxComponent } from "src/app/common/Components/InputBox/inputBox.Component";
import { PostCommponent } from "src/app/common/Components/Post/post.component";
import { FeatureStates } from "src/app/state/futureStates";
import { HomeComponent } from "./home/home.component";
import { MainComponent } from "./main.component";
import { mainReducer } from "./redux/main.reducers";
import { MainEffects } from "./redux/mian.effects";
import { UserComponent } from "./user/user.component";

@NgModule({
    imports: [
        CommonModule,
        StoreModule.forFeature(FeatureStates.Main, mainReducer),
        EffectsModule.forFeature([MainEffects]),
        RouterModule.forChild([]),
        FormsModule
    ],
    declarations: [
        MainComponent,
        UserComponent,
        HomeComponent,
        PostCommponent,
        CommentComponent,
        InputBoxComponent
    ]
})
export class MainModule { }