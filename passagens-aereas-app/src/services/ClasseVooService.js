
import CONFIG from '../config'
import axios from 'axios'

export default class ClasseVooService {
    
    static getClasses() {
        return  axios.get(
            `${CONFIG.API_URL_BASE}/api/classeVoo`,
            {
                headers: {
                    Authorization: `Bearer ${localStorage.getItem('tokenUser')}`,
                    'Content-Type': 'application/json'
                }
            }
        )
    }


    static getClasseVoo(idClasseVoo) {
        return  axios.get(
            `${CONFIG.API_URL_BASE}/api/classeVoo/${idClasseVoo}`,
            {
                headers: {
                    Authorization: `Bearer ${localStorage.getItem('tokenUser')}`,
                    'Content-Type': 'application/json'
                }
            }
        )
    }
    
    static cadastrar(classeVoo) {
        return axios.post(
            `${CONFIG.API_URL_BASE}/api/classeVoo`,
            {
                Nome: classeVoo.nome,
                ValorFixo: classeVoo.valorFixo,
                ValorMilha: classeVoo.valorMilha
            },
            {
                headers: {
                    Authorization: `Bearer ${localStorage.getItem('tokenUser')}`,
                    'Content-Type': 'application/json'
                }
            }
        )
    }

    static editar(classeVoo) {
        return axios.put(
            `${CONFIG.API_URL_BASE}/api/classeVoo/${classeVoo.id}`,
            {
                Nome: classeVoo.nome,
                ValorFixo: classeVoo.valorFixo,
                ValorMilha: classeVoo.valorMilha
            },
            {
                headers: {
                    Authorization: `Bearer ${localStorage.getItem('tokenUser')}`,
                    'Content-Type': 'application/json'
                }
            }
        )
    }

    static delete(classeVoo) {
        return axios.delete(
            `${CONFIG.API_URL_BASE}/api/classeVoo/${classeVoo.id}`,
            {
                headers: {
                    Authorization: `Bearer ${localStorage.getItem('tokenUser')}`,
                    'Content-Type': 'application/json'
                }
            }
        )
    }
}
