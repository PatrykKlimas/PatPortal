import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { StoreDevtoolsModule } from '@ngrx/store-devtools';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { StoreModule } from '@ngrx/store';
import { EffectsModule } from '@ngrx/effects';
import { FeatureStates } from './state/futureStates';
import { globalReducer } from './redux/global.reducers';
import { environment } from 'src/environments/environment';
import { UserEffects } from './redux/global.effects';
import { LoginComponent } from './pages/credentials/login/login.component';
import { FormsModule } from '@angular/forms';
import { UserComponent } from './pages/main/user/user.component';
import { HomeComponent } from './pages/main/home/home.component';
import { MainModule } from './pages/main/main.module';


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
    }),
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
