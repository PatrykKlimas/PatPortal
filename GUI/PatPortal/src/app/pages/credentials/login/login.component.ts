import { Component, OnInit } from "@angular/core";
import { Store } from "@ngrx/store";
import { State } from "../../../redux/globas.selectors";
import * as GlobalActions from "../../../redux/global.actions";
import * as GlobalSelectors from "../../../redux/globas.selectors";
import { Observable, tap } from "rxjs";
import { Router } from "@angular/router";
import { Location } from "@angular/common";

@Component({
    selector: 'pp-login',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit{ 

    userId: string = "afdb21f2-66ad-48a2-a927-83f4d72a5258";
    passwordError: string = "";
    logInError: string = "";
    password: string ="";
    logIn: string ='';

    constructor(private store: Store<State>, private router: Router, private location: Location){}
    ngOnInit(): void {
        this.store.select(GlobalSelectors.getLogin).subscribe(l => this.logIn = l);
        this.store.select(GlobalSelectors.getUser).pipe(
            tap(user => {
                if(user)
                this.router.navigate(['/main']);
            })
        ).subscribe();
    }

    login(): void{
        if(this.logInValidation() && this.passwordValidation()){
            this.store.dispatch(GlobalActions.initializeUser({id: this.userId}));
        }
    }

    passwordValidation(): boolean{
        if(this.password == ""){
            this.passwordError = "Please enter password.";
            return false;
        }

        this.passwordError = "";
        return true;
    }

    logInValidation(): boolean{
        let loginForValidation: string | undefined;
        this.store.select(GlobalSelectors.getLogin).subscribe(
            login => loginForValidation = login
        );

        if(loginForValidation == "" || loginForValidation == undefined){
            return false;
        }

        this.logInError = "";
        return true;
    }

    changeLogin(event: any){
        this.store.dispatch(GlobalActions.setLogin({login: event.target.value}))
    }

    changePassword(event: any){
        this.password = event.target.value;
    }
}