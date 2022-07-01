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
export class MainComponent implements OnInit {

    user: IUser | null = null;


    constructor(private store: Store<State>, private router: Router) { }
    ngOnInit(): void {
        this.store.select(GlobalSelectors.getUser).subscribe(user => this.user = user);
    }

    navigateToUser(): void {
        this.router.navigate(['main/user']);
    }

    navigateToFriends(): void {

    }
}