import { Component } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { AuthServiceService } from '../../../services/auth/auth-service.service';

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

  constructor(private authService: AuthServiceService) {}

  onSubmit() {
    this.authService.signIn(
      this.signInForm.value.usuario!!,
      this.signInForm.value.senha!!
    );
  }
}
