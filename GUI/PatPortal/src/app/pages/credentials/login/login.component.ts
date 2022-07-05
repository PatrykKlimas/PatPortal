import { Component, OnInit } from "@angular/core";
import { Router } from "@angular/router";
import { Store } from "@ngrx/store";
import { tap } from "rxjs";
import * as GlobalActions from "../../../redux/global.actions";
import * as GlobalSelectors from "../../../redux/globas.selectors";
import { State } from "../../../redux/globas.selectors";

@Component({
    selector: 'pp-login',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

    userId: string = "c2debe6b-619d-4595-aadb-c8878718af59";
    passwordError: string = "";
    logInError: string = "";
    password: string = "";
    logIn: string = '';

    constructor(private store: Store<State>, private router: Router) { }
    ngOnInit(): void {
        this.store.select(GlobalSelectors.getLogin).subscribe(l => this.logIn = l);
        this.store.select(GlobalSelectors.getUser).pipe(
            tap(user => {
                if (user)
                    this.router.navigate(['/main']);
            })
        ).subscribe();
    }

    login(): void {
        let validationLogin =this.logInValidation();
        let validationPassword = this.passwordValidation();

        if (validationLogin && validationPassword) {
            this.store.dispatch(GlobalActions.initializeUser({ id: this.userId }));
        }
    }

    passwordValidation(): boolean {

        if (this.password == "") {
            this.passwordError = "Please enter password.";
            return false;
        }
        
        this.passwordError = "";
        return true;
    }

    logInValidation(): boolean {
        if (this.logIn == "" || this.logIn == undefined) {
            this.logInError = "Please enter login";
            return false;
        }

        this.logInError = "";
        return true;
    }

    changeLogin(event: any) {
        this.store.dispatch(GlobalActions.setLogin({ login: event.target.value }))
    }

    changePassword(event: any) {
        this.password = event.target.value;
    }
}