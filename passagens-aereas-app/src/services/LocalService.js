
import CONFIG from '../config'
import axios from 'axios'

export default class LocalService {

    static getLocais() {
        return  axios.get(
            `${CONFIG.API_URL_BASE}/api/local`,
            {
                headers: {
                    Authorization: `Bearer ${localStorage.getItem('tokenUser')}`,
                    'Content-Type': 'application/json'
                }
            }
        )
    }

    static getLocal(idLocal) {
        return axios.put(
            `${CONFIG.API_URL_BASE}/api/local/${idLocal}`,
            {
                headers: {
                    Authorization: `Bearer ${localStorage.getItem('tokenUser')}`,
                    'Content-Type': 'application/json'
                }
            }
        )
    }

    static cadastrar(local) {
        return axios.post(
            `${CONFIG.API_URL_BASE}/api/local`,
            {
                Nome: local.nome,
                LatitudeLocal: local.latitudeLocal,
                LongitudeLocal: local.longitudeLocal
            },
            {
                headers: {
                    Authorization: `Bearer ${localStorage.getItem('tokenUser')}`,
                    'Content-Type': 'application/json'
                }
            }
        )
    }

    static editar(local) {
        return axios.put(
            `${CONFIG.API_URL_BASE}/api/local/${local.id}`,
            {
                Nome: local.nome,
                LatitudeLocal: local.latitudeLocal,
                LongitudeLocal: local.longitudeLocal
            },
            {
                headers: {
                    Authorization: `Bearer ${localStorage.getItem('tokenUser')}`,
                    'Content-Type': 'application/json'
                }
            }
        )
    }

    static delete(local) {
        return axios.delete(
            `${CONFIG.API_URL_BASE}/api/local/${local.id}`,
            {
                headers: {
                    Authorization: `Bearer ${localStorage.getItem('tokenUser')}`,
                    'Content-Type': 'application/json'
                }
            }
        )
    }
}