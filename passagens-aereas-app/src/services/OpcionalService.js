
import CONFIG from '../config'
import axios from 'axios'

export default class OpcionalService {

    static getOpcionais() {
        return  axios.get(
            `${CONFIG.API_URL_BASE}/api/opcional`,
            {
                headers: {
                    Authorization: `Bearer ${localStorage.getItem('tokenUser')}`,
                    'Content-Type': 'application/json'
                }
            }
        )
    }

    static cadastrar(opcional) {
        return axios.post(
            `${CONFIG.API_URL_BASE}/api/opcional`,
            {
                Nome: opcional.nome,
                Descricao: opcional.descricao,
                Valor: (opcional.valor/100)
            },
            {
                headers: {
                    Authorization: `Bearer ${localStorage.getItem('tokenUser')}`,
                    'Content-Type': 'application/json'
                }
            }
        )
    }

    static editar(opcional) {
        return axios.put(
            `${CONFIG.API_URL_BASE}/api/opcional/${opcional.id}`,
            {
                Nome: opcional.nome,
                Descricao: opcional.descricao,
                Valor: (opcional.valor/100)
            },
            {
                headers: {
                    Authorization: `Bearer ${localStorage.getItem('tokenUser')}`,
                    'Content-Type': 'application/json'
                }
            }
        )
    }

    static delete(opcional) {
        return axios.delete(
            `${CONFIG.API_URL_BASE}/api/opcional/${opcional.id}`,
            {
                headers: {
                    Authorization: `Bearer ${localStorage.getItem('tokenUser')}`,
                    'Content-Type': 'application/json'
                }
            }
        )
    }
}