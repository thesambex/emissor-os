export default class CriarUsuarioDTO {
  public nome: string;
  public nomeUsuario: string;
  public senha: string;

  constructor(nome: string, nomeUsuario: string, senha: string) {
    this.nome = nome;
    this.nomeUsuario = nomeUsuario;
    this.senha = senha;
  }
}
