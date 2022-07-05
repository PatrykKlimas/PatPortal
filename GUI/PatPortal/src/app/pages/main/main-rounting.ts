import { NgModule } from "@angular/core";
import { Routes } from "@angular/router";
import { HomeComponent } from "./home/home.component";
import { MainComponent } from "./main.component";
import { UserComponent } from "./user/user.component";


const routes: Routes = [
    { path: "main", component: MainComponent,
    children: [
      {path: "home", component: HomeComponent},
      {path: "user", component: UserComponent},
      {path: "", redirectTo: "home", pathMatch: "full"},
      { path: '**', redirectTo: 'home', pathMatch: 'full' }
    ]
  },
    // { path: "user", component: UserComponent},
    // { path: '', redirectTo: 'main', pathMatch: 'full' },
    // { path: '**', redirectTo: 'main', pathMatch: 'full' }
  ];
@NgModule()
export class MainRountingModule{}