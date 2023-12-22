import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import Constants from '../../constants';
import AuthTokenDTO from '../../data/dto/AuthToken';
import { Observable } from 'rxjs';
import CriarUsuarioDTO from '../../data/dto/CriarUsuario.dto';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  constructor(private http: HttpClient) {}

  signIn(usuario: string, senha: string): Observable<AuthTokenDTO> {
    return this.http.post<AuthTokenDTO>(
      `${Constants.ENDPOINT_API}/api/v1/auth/signin`,
      {
        nome_usuario: usuario,
        senha,
      }
    );
  }

  signUp(dto: CriarUsuarioDTO): Observable<CriarUsuarioDTO> {
    return this.http.post<CriarUsuarioDTO>(
      `${Constants.ENDPOINT_API}/api/v1/auth/signup`,
      {
        nome: dto.nome,
        nome_usuario: dto.nomeUsuario,
        senha: dto.senha,
      }
    );
  }
}
