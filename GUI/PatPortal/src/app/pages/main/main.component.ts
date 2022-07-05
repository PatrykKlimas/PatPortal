import { Component, OnInit } from "@angular/core";
import { Store } from "@ngrx/store";
import { State } from "./redux/main.selectors";
import * as GlobalSelectors from "../../redux/globas.selectors";
import { IUser } from "src/app/common/models/IUser";
import { Router } from "@angular/router";

@Component({
    selector: "pp-main",
    templateUrl: "./main.component.html",
    styleUrls: ["./main.component.css"]
})
export class MainComponent {

    $user = this.store.select(GlobalSelectors.getUser)

    constructor(private store: Store<State>, private router: Router) { }

    navigateToUser(): void {
        this.router.navigate(['main/user']);
    }

    navigateToFriends(): void {

    }
}