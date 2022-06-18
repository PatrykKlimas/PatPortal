import { Component, OnInit } from '@angular/core';
import { Store } from '@ngrx/store';
import * as GlobalSelectors from "./redux/globa.selectors";
import { State } from './redux/globa.selectors';
import * as GlobalActions from "./redux/global.actions";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit{

  title = 'PatPortal';
  userId: string = "afdb21f2-66ad-48a2-a927-83f4d72a5258";
  user$ = this.store.select(GlobalSelectors.getUser);
  errorMessage = this.store.select(GlobalSelectors.getErrorMessage);

  constructor(private store: Store<State>){}

  ngOnInit(): void {
    this.store.dispatch(GlobalActions.initializeUser({id : this.userId}));
  }
  
}
