import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  // { path: 'home', component: HomeComponent },
  // { path: 'user', component: UserComponent },
  // { path: 'main', component: AppComponent },
  // { path: '', redirectTo: 'main', pathMatch: 'full' },
  // { path: '**', redirectTo: 'main', pathMatch: 'full' }
  //{path: 'friends', component: FriendsComponent},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
