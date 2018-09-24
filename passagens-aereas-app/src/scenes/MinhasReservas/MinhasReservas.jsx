import React from 'react'
import {Link} from 'react-router-dom'
import Alert from '../../components/generic/Alert/Alert'
import UsuarioService from '../../services/UsuarioService'
import ClasseVooService from '../../services/ClasseVooService'
import OpcionalService from '../../services/OpcionalService'
import Button from '../../components/generic/Button/Button'
import './MinhasReservas.css'
import Input from '../../components/generic/Input/Input'
import TrechoService from "../../services/TrechoService";
import ReservaModel from "../../models/ReservaModel";

export default class MinhasReservas extends React.Component {
    constructor() {
        super()

        this.deleteReserva = this.deleteReserva.bind(this)
        this.onClickSalvarButton = this.onClickSalvarButton.bind(this)
        this.getValorReserva = this.getValorReserva.bind(this)
        this.handleChange = this.handleChange.bind(this)
        this.handleChangeOpcionais = this.handleChangeOpcionais.bind(this)
        this.state = {
            classesVoo: [],
            reservas: [],
            reserva: {},
            valorTotal: 0,
            opcionais: [],
            opcionaisSelecionados: [],
            trechos: [],
            error: '',
            idTrecho: 0,
            idClasseVoo: 0,
            idOpcionais: []
        }
    }

    handleChange(event) {
        const target = event.target
        const value = target.value
        const name = target.name
        this.setState({
            [name]: value
        })
        this.getValorReserva()
    }

    handleChangeOpcionais(event) {

        let opcionaisSelecionados = this.state.opcionaisSelecionados
        let index

        if (event.target.checked) {
            opcionaisSelecionados.push(+event.target.value)
        } else {
            index = opcionaisSelecionados.indexOf(+event.target.value)
            opcionaisSelecionados.splice(index, 1)
        }

        this.setState({
            [opcionaisSelecionados]: opcionaisSelecionados
        })

        this.getValorReserva()
    }

    componentWillMount() {
        let reservas = []

        UsuarioService
            .getReservas()
            .then((result) => {
                reservas = result.data

                this.setState({
                    reservas: reservas
                })
            })
            .catch((err) => {
                this.setState({
                    error: err.data
                })
            })

        OpcionalService
            .getOpcionais()
            .then((result) => {
                this.setState({
                    opcionais: result.data,
                })
            })
            .catch((err) => {
                this.setState({
                    error: err.data
                })
            })

        TrechoService
            .getTrechos()
            .then((result) => {
                this.setState({
                    trechos: result.data,
                })
            })
            .catch((err) => {
                this.setState({
                    error: err.data
                })
            })

        ClasseVooService
            .getClasses()
            .then((result) => {
                this.setState({
                    classesVoo: result.data,
                })
            })
            .catch((err) => {
                this.setState({
                    error: err.data
                })
            })
    }

    componentDidMount() {
        let reservas = []

        UsuarioService
            .getReservas()
            .then((result) => {
                reservas = result.data

                this.setState({
                    reservas: reservas
                })
            })
            .catch((err) => {
                this.setState({
                    error: err.data
                })
            })

        OpcionalService
            .getOpcionais()
            .then((result) => {
                this.setState({
                    opcionais: result.data,
                })
            })
            .catch((err) => {
                this.setState({
                    error: err.data
                })
            })

        TrechoService
            .getTrechos()
            .then((result) => {
                this.setState({
                    trechos: result.data,
                })
            })
            .catch((err) => {
                this.setState({
                    error: err.data
                })
            })

        ClasseVooService
            .getClasses()
            .then((result) => {
                this.setState({
                    classesVoo: result.data,
                })
            })
            .catch((err) => {
                this.setState({
                    error: err.data
                })
            })
    }

    deleteReserva(reserva) {

        UsuarioService.deleteReserva(reserva)
            .then((result) => {
                this.componentDidMount()
            })
            .catch((err) => {
                this.setState({
                    error: err.data
                })
            })
    }

    onClickSalvarButton() {

        let reserva = new ReservaModel(
            parseInt(this.state.idTrecho),
            parseInt(this.state.idClasseVoo),
            this.state.opcionaisSelecionados
        )

        UsuarioService
            .reservar(reserva)
            .then((result) => {
                this.componentDidMount()
            })
            .catch((err) => {
                this.setState({
                    error: err.data
                })
            })
    }

    getValorReserva() {

        let reserva = new ReservaModel(
            parseInt(this.state.idTrecho),
            parseInt(this.state.idClasseVoo),
            this.state.opcionaisSelecionados
        )

        UsuarioService
            .getValorReserva(reserva)
            .then((result) => {
                this.setState({
                    valorTotal: result.data
                })
            })
            .catch((err) => {
                this.setState({
                    error: err.data
                })
            })
    }

    renderClasses() {
        return this.state.classesVoo.map((classe, key) => {
            return (
                <option value={classe.id}>
                    {classe.nome}
                </option>
            )
        })
    }

    renderTrechos() {
        return this.state.trechos.map((trecho, key) => {
            return (
                <option value={trecho.id}>
                    {trecho.nome}
                </option>
            )
        })
    }


    renderOptions() {
        return this.state.opcionais.map((opcional, key) => {
            return (
                <tr>
                    <td><span>{opcional.nome}</span></td>
                    <td><span>{opcional.descricao}</span></td>
                    <td><span>{opcional.valor}</span></td>
                    <td>
                        <Input
                            classLabel="hidden"
                            classInput="form-control input-sm chat-input selectOpcionais"
                            label=''
                            value={opcional.id}
                            name={opcional.nome}
                            placeholder={opcional.nome}
                            handleChange={this.handleChangeOpcionais}
                            type="checkbox"
                        />
                    </td>
                </tr>
            )
        })
    }

    renderReservas() {

        return this.state.reservas.map((reserva, key) => {
            return (
                <tr>
                    <td><span>{reserva.trecho.nome}</span></td>
                    <td><span>{reserva.classeVoo.nome}</span></td>
                    <td><span>{reserva.valorTotal}</span></td>
                    <td>
                        <Link to='/minhasReservas'>
                            <Button type="button"
                                    buttonClass='btn btn-danger btn-sm float-right'
                                    text="Excluir"
                                    onClick={() => {
                                        this.deleteReserva(reserva)
                                    }}/>
                        </Link>
                    </td>
                </tr>
            )
        })
    }

    renderError() {
        return this.state.error ? <Alert text={this.state.error} alertType="danger"/> : undefined
    }

    render() {

        return (
            <div className="reserva">
                <div className="reservas listaOpcionais">
                    <h2>Nova Reserva</h2>

                    <form>
                        <div className='selectLocais'>
                            <div className="form-group selectLocal">
                                <label className='label1'>Trecho</label>
                                <select className="btn btn-default btn-sm dropdown-toggle" name='idTrecho' value={this.state.idTrecho} onChange={this.handleChange}>
                                    {this.renderTrechos()}
                                </select>
                            </div>
                            <div className="form-group selectLocal">
                                <label className='label1'>Classe</label>
                                <select className="btn btn-default btn-sm dropdown-toggle" name='idClasseVoo' value={this.state.idClasseVoo} onChange={this.handleChange}>
                                    {this.renderClasses()}
                                </select>
                            </div>
                        </div>
                        <div className="form-group">
                            <div className="">
                                <table className="table table-striped">
                                    <thead>
                                    <tr>
                                        <th>Opcional</th>
                                        <th>Descrição</th>
                                        <th>Valor</th>
                                        <th>Adicionar</th>
                                    </tr>
                                    </thead>
                                    <tbody>
                                        {this.renderOptions()}
                                    </tbody>
                                </table>
                            </div>
                        </div>


                        <div className="input-group">
                            <label className='label1'>Valor total da Reserva R$ </label>
                            <span>{this.state.valorTotal}</span>
                        </div>

                        <Link to='/minhasReservas'>
                            <Button type="submit"
                                    buttonClass='btn btn-success btn-sm float-right'
                                    text="Salvar"
                                    onClick={this.onClickSalvarButton}/>
                        </Link>
                    </form>
                    {this.renderError()}


                    <div className="panel panel-default">
                        <br/>
                        <div>
                            <h2>Minhas Reservas</h2>
                            <table className="table table-striped">
                                <thead>
                                <tr>
                                    <th>Trecho</th>
                                    <th>Classe</th>
                                    <th>Valor</th>
                                    <th>Deletar</th>
                                </tr>
                                </thead>
                                <tbody>
                                {this.renderReservas()}
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        )
    }
}