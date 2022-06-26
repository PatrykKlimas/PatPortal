import { Component } from '@angular/core';
import { Store } from '@ngrx/store';
import * as GlobalSelectors from "./redux/globas.selectors";
import { State } from './redux/globas.selectors';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent{

  title = 'PatPortal';
  
  user$ = this.store.select(GlobalSelectors.getUser);
  errorMessage$ = this.store.select(GlobalSelectors.getErrorMessage);

  constructor(private store: Store<State>){}

}
