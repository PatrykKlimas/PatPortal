import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './pages/credentials/login/login.component';
import { HomeComponent } from './pages/main/home/home.component';
import { MainComponent } from './pages/main/main.component';
import { MainGuard } from './pages/main/main.guard';
import { UserComponent } from './pages/main/user/user.component';

const routes: Routes = [
  { path: 'login', component: LoginComponent },
  {
    path: 'main', canActivate: [MainGuard], component: MainComponent,
    children: [
      { path: "home", component: HomeComponent },
      { path: "user", component: UserComponent },
      { path: "", redirectTo: "home", pathMatch: "full" },
      { path: '**', redirectTo: 'home', pathMatch: 'full' }
    ]
  },
  { path: '', redirectTo: 'main', pathMatch: 'full' },
  { path: '**', redirectTo: 'main', pathMatch: 'full' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
