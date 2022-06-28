import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { StoreDevtoolsModule } from '@ngrx/store-devtools';

import { FormsModule } from '@angular/forms';
import { EffectsModule } from '@ngrx/effects';
import { StoreModule } from '@ngrx/store';
import { environment } from 'src/environments/environment';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './pages/credentials/login/login.component';
import { MainGuard } from "./pages/main/main.guard";
import { MainModule } from './pages/main/main.module';
import { UserEffects } from './redux/global.effects';
import { globalReducer } from './redux/global.reducers';
import { FeatureStates } from './state/futureStates';


@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
  ],
  imports: [
    MainModule,
    FormsModule,
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    StoreModule.forRoot([]),
    StoreModule.forFeature(FeatureStates.User, globalReducer),
    EffectsModule.forRoot([UserEffects]),
    StoreDevtoolsModule.instrument({
      name: 'APM Demo App DevTools',
      maxAge: 25,
      logOnly: environment.production
    })
  ],
  providers: [MainGuard],
  bootstrap: [AppComponent]
})
export class AppModule { }
