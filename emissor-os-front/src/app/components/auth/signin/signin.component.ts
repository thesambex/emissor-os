import { Component } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { AuthService } from '../../../services/auth/auth-service.service';
import { HttpErrorResponse } from '@angular/common/http';
import { Router } from '@angular/router';
import AuthTokenDTO from '../../../data/dto/AuthToken';
import Constants from '../../../constants';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'auth-signin',
  standalone: true,
  imports: [ReactiveFormsModule, CommonModule],
  templateUrl: './signin.component.html',
  styleUrl: './signin.component.css',
})
export class SignInComponent {
  signInForm = new FormGroup({
    usuario: new FormControl(''),
    senha: new FormControl(''),
  });

  errorMsg?: string;

  constructor(private authService: AuthService, private router: Router) {}

  onSubmit() {
    this.authService
      .signIn(this.signInForm.value.usuario!!, this.signInForm.value.senha!!)
      .subscribe({
        next: (data: AuthTokenDTO) => {
          window.sessionStorage.setItem(Constants.USER_TOKEN, data.token);
          this.router.navigate(['/']);
        },
        error: (error: HttpErrorResponse) => {
          switch (error.status) {
            case 401:
              this.errorMsg = 'Senha incorreta';
              break;
            case 404:
              this.errorMsg = 'Usuario não encontrado';
              break;
            default:
              this.errorMsg = 'Erro desconhecido';
          }
        },
      });
  }
}
