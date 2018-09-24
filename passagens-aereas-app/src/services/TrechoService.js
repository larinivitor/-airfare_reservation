
import CONFIG from '../config'
import axios from 'axios'

export default class TrechoService {

    static getTrechos() {
        return  axios.get(
            `${CONFIG.API_URL_BASE}/api/trecho`,
            {
                headers: {
                    Authorization: `Bearer ${localStorage.getItem('tokenUser')}`,
                    'Content-Type': 'application/json'
                }
            }
        )
    }

    static getTrecho(idTrecho) {
        return  axios.get(
            `${CONFIG.API_URL_BASE}/api/trecho/${idTrecho}`,
            {
                headers: {
                    Authorization: `Bearer ${localStorage.getItem('tokenUser')}`,
                    'Content-Type': 'application/json'
                }
            }
        )
    }

    static cadastrar(trecho) {
        return axios.post(
            `${CONFIG.API_URL_BASE}/api/trecho`,
            {
                Nome: trecho.nome,
                IdLocalA: trecho.idLocalOrigem,
                IdLocalB: trecho.idLocalDestino
            },
            {
                headers: {
                    Authorization: `Bearer ${localStorage.getItem('tokenUser')}`,
                    'Content-Type': 'application/json'
                }
            }
        )
    }

    static editar(trecho) {
        return axios.put(
            `${CONFIG.API_URL_BASE}/api/trecho/${trecho.id}`,
            {
                Nome: trecho.nome,
                IdLocalA: trecho.idLocalOrigem,
                IdLocalB: trecho.idLocalDestino
            },
            {
                headers: {
                    Authorization: `Bearer ${localStorage.getItem('tokenUser')}`,
                    'Content-Type': 'application/json'
                }
            }
        )
    }

    static delete(trecho) {
        return axios.delete(
            `${CONFIG.API_URL_BASE}/api/trecho/${trecho.id}`,
            {
                headers: {
                    Authorization: `Bearer ${localStorage.getItem('tokenUser')}`,
                    'Content-Type': 'application/json'
                }
            }
        )
    }
}