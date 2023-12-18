import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import Constants from '../../constants';

@Injectable({
  providedIn: 'root',
})
export class AuthServiceService {
  constructor(private http: HttpClient) {}

  signIn(usuario: string, senha: string) {
    this.http.post(`${Constants.ENDPOINT_API}/api/v1/auth/signin`, { nome_usuario: usuario, senha }).subscribe(r => console.log(r));
  }
}
