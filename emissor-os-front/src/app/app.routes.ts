import { Routes } from '@angular/router';
import { SignInComponent } from './components/auth/signin/signin.component';
import { HomeComponent } from './components/home/home.component';
import { SignUpComponent } from './components/auth/sign-up/sign-up.component';

export const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'auth/signin', component: SignInComponent },
  { path: 'auth/signup', component: SignUpComponent },
];
