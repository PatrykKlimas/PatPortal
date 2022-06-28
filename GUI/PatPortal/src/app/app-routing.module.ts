import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './pages/credentials/login/login.component';
import { MainComponent } from './pages/main/main.component';
import { MainGuard } from './pages/main/main.guard';

const routes: Routes = [
  // { path: 'home', component: HomeComponent },
  // { path: 'user', component: UserComponent },
  // { path: 'main', component: AppComponent },
  // { path: '', redirectTo: 'main', pathMatch: 'full' },
  // { path: '**', redirectTo: 'main', pathMatch: 'full' }
  //{path: 'friends', component: FriendsComponent},
  { path: 'login', component: LoginComponent },
  { path: 'main', canActivate: [MainGuard], component: MainComponent },
  { path: '', redirectTo: 'main', pathMatch: 'full' },
  { path: '**', redirectTo: 'main', pathMatch: 'full' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
