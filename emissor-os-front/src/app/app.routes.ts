import { Routes } from '@angular/router';
import { SignInComponent } from './components/auth/signin/signin.component';
import { HomeComponent } from './components/home/home.component';
import { SignUpComponent } from './components/auth/sign-up/sign-up.component';
import { authdGuard } from './guards/authguard.guard';

export const routes: Routes = [
  { path: '', component: HomeComponent, canActivate: [authdGuard] },
  { path: 'auth/signin', component: SignInComponent },
  { path: 'auth/signup', component: SignUpComponent },
];
