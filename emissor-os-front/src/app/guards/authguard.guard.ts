import { CanActivateFn, Router } from '@angular/router';
import Constants from '../constants';
import { inject } from '@angular/core';
import { jwtDecode } from 'jwt-decode';

export const authdGuard: CanActivateFn = (route, state) => {
  const router = inject(Router);
  const jwt = window.sessionStorage.getItem(Constants.USER_TOKEN)
  console.log(jwt)
  if (jwt == null || jwt?.length == 0) {
    router.navigate(['/auth/signin']);
    return false;
  }

  const token = jwtDecode(jwt);
  console.log(token)
  if (token == null) {
    router.navigate(['/auth/signin']);
    return false;
  }

  if (Date.now() <= token.exp!!) {
    router.navigate(['/auth/signin']);
    return false;
  }

  return true;
};
