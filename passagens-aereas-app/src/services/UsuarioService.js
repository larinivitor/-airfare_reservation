
import CONFIG from '../config'
import axios from 'axios'

export default class UsuarioService {

    static login(login, senha) {
        return  axios.post(
            `${CONFIG.API_URL_BASE}/api/usuario/login`,
            {
                Login: login,
                Senha: senha
            }
        )
    }

    static cadastrar(usuario) {
        return axios.post(
            `${CONFIG.API_URL_BASE}/api/usuario`,
            {
                PrimeiroNome: usuario.primeiroNome,
                UltimoNome: usuario.ultimoNome,
                CPF: usuario.cpf,
                DataNascimento: usuario.dataNascimento,
                Login: usuario.login,
                Senha: usuario.senha,
                Admin: false
            }
        )
    }

    static editar(usuario) {
        return axios.put(
            `${CONFIG.API_URL_BASE}/api/usuario/${parseInt(localStorage.getItem("idUsuario"))}`,
            {
                PrimeiroNome: usuario.primeiroNome,
                UltimoNome: usuario.ultimoNome,
                CPF: usuario.cpf,
                DataNascimento: usuario.dataNascimento,
                Login: usuario.login,
                Senha: usuario.senha,
                Admin: false
            },
            {
                headers: {
                    Authorization: `Bearer ${localStorage.getItem('tokenUser')}`,
                    'Content-Type': 'application/json'
                }
            }
        )
    }

    static reservar(reserva) {
        debugger
        return axios.post(
            `${CONFIG.API_URL_BASE}/api/usuario/${parseInt(localStorage.getItem("idUsuario"))}/reservar`,
            {
                IdTrecho: reserva.idTrecho,
                IdClasseVoo: reserva.idClasseVoo,
                IdOpcionais: reserva.idOpcionais
            },
            {
                headers: {
                    Authorization: `Bearer ${localStorage.getItem('tokenUser')}`,
                    'Content-Type': 'application/json'
                }
            }
        )
    }

    static getValorReserva(reserva) {
        return axios.post(
            `${CONFIG.API_URL_BASE}/getValorReserva`,
            {
                IdTrecho: reserva.idTrecho,
                IdClasseVoo: reserva.idClasseVoo,
                IdOpcionais: reserva.idOpcionais
            },
            {
                headers: {
                    Authorization: `Bearer ${localStorage.getItem('tokenUser')}`,
                    'Content-Type': 'application/json'
                }
            }
        )
    }

    static getReservas() {
        return axios.get(
            `${CONFIG.API_URL_BASE}/api/usuario/${parseInt(localStorage.getItem("idUsuario"))}/reservas`,
            {
                headers: {
                    Authorization: `Bearer ${localStorage.getItem('tokenUser')}`,
                    'Content-Type': 'application/json'
                }
            }
        )
    }

    static deleteReserva(reserva) {
        return axios.delete(
            `${CONFIG.API_URL_BASE}/api/usuario/${reserva.id}/reserva`,
            {
                headers: {
                    Authorization: `Bearer ${localStorage.getItem('tokenUser')}`,
                    'Content-Type': 'application/json'
                }
            }
        )
    }
}