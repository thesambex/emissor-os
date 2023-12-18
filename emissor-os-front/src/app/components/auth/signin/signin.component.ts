import { Component, inject } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { AuthService } from '../../../services/auth/auth-service.service';
import { HttpErrorResponse } from '@angular/common/http';
import { Router } from '@angular/router';
import AuthTokenDTO from '../../../data/dto/AuthToken';
import Constants from '../../../constants';

@Component({
  selector: 'auth-signin',
  standalone: true,
  imports: [ReactiveFormsModule],
  templateUrl: './signin.component.html',
  styleUrl: './signin.component.css',
})
export class SignInComponent {
  signInForm = new FormGroup({
    usuario: new FormControl(''),
    senha: new FormControl(''),
  });

  constructor(private authService: AuthService, private router: Router) {}

  onSubmit() {
    this.authService
      .signIn(this.signInForm.value.usuario!!, this.signInForm.value.senha!!)
      .subscribe({
        next: (data: AuthTokenDTO) => {
          window.sessionStorage.setItem(Constants.USER_TOKEN, data.token);
          this.router.navigate(['/']);
        },
        error: (error: HttpErrorResponse) => {},
      });
  }
}
