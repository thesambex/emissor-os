import { Component } from '@angular/core';
import {
  FormControl,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { AuthService } from '../../../services/auth/auth-service.service';
import { Router } from '@angular/router';
import CriarUsuarioDTO from '../../../data/dto/CriarUsuario.dto';
import { HttpErrorResponse } from '@angular/common/http';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'auth-sign-up',
  standalone: true,
  imports: [ReactiveFormsModule, CommonModule],
  templateUrl: './sign-up.component.html',
  styleUrl: './sign-up.component.css',
})
export class SignUpComponent {
  signUpForm = new FormGroup({
    nome: new FormControl(''),
    usuario: new FormControl(''),
    senha: new FormControl(''),
    rsenha: new FormControl(''),
  });

  errorMsg?: string;

  constructor(private authService: AuthService, private router: Router) {}

  onSubmit() {
    if (this.signUpForm.value.senha != this.signUpForm.value.rsenha) {
      this.errorMsg = 'As senhas não coincidem';
      return;
    }

    const input = new CriarUsuarioDTO(
      this.signUpForm.value.nome!!,
      this.signUpForm.value.usuario!!,
      this.signUpForm.value.senha!!
    );

    this.authService.signUp(input).subscribe({
      next: (data: CriarUsuarioDTO) => {
        this.router.navigate(['/auth/signin']);
      },
      error: (error: HttpErrorResponse) => {
        switch (error.status) {
          case 409:
            this.errorMsg =
              'Já existe um usuário cadastrado com este nome de usuário';
            break;
          default:
            this.errorMsg = 'Erro desconhecido';
        }
      },
    });
  }
}
