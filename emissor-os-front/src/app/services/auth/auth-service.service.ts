import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import Constants from '../../constants';
import AuthTokenDTO from '../../data/dto/AuthToken';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  constructor(private http: HttpClient) {}

  signIn(usuario: string, senha: string): Observable<AuthTokenDTO>  {
    return this.http.post<AuthTokenDTO>(
      `${Constants.ENDPOINT_API}/api/v1/auth/signin`,
      {
        nome_usuario: usuario,
        senha,
      }
    );
  }
}
