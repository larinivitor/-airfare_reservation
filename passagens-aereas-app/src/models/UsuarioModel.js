export default class UsuarioModel{
    constructor(
        primeiroNome,
        ultimoNome,
        cpf,
        dataNascimento,
        login,
        senha
    ){
        this.primeiroNome = primeiroNome
        this.ultimoNome = ultimoNome
        this.cpf = cpf
        this.dataNascimento = dataNascimento
        this.login = login
        this.senha = senha
    }
}