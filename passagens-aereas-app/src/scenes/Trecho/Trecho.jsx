import React from 'react'
import Input from '../../components/generic/Input/Input'
import Button from '../../components/generic/Button/Button'
import Alert from '../../components/generic/Alert/Alert'
import {Link} from 'react-router-dom'
import './Trecho.css'
import TrechoModel from "../../models/TrechoModel";
import TrechoService from "../../services/TrechoService";
import LocalService from "../../services/LocalService";


export default class Trecho extends React.Component {
    constructor() {
        super()

        this.state = this.getInitialState()
        this.onClickSalvarButton = this.onClickSalvarButton.bind(this)
        this.onClickDeleteButton = this.onClickDeleteButton.bind(this)
        this.onClickEditButton = this.onClickEditButton.bind(this)
        this.handleChange = this.handleChange.bind(this)
    }

    getInitialState() {
        return {
            titulo: 'Novo Trecho',
            nome: 'Nome',
            idLocalOrigem: 0,
            idLocalDestino: 0,
            id: 0,
            error: '',
            trechos: [],
            locais: [],
            modoEdicao: false
        }
    }

    onClickSalvarButton() {
        let trecho = new TrechoModel(
            this.state.nome,
            this.state.idLocalOrigem,
            this.state.idLocalDestino,
            this.state.id
        )

        if (!this.state.modoEdicao) {
            TrechoService
                .cadastrar(trecho)
                .then((result) => {
                    this.componentDidMount()
                })
                .catch((err) => {
                    this.setState({
                        error: err.data
                    })
                })
        } else {
            TrechoService
                .editar(trecho)
                .then((result) => {
                    debugger
                    this.componentDidMount()
                })
                .catch((err) => {
                    this.setState({
                        error: err.data
                    })
                })
        }
    }

    onClickDeleteButton(trecho) {

        TrechoService
            .delete(trecho)
            .then((result) => {
                this.componentDidMount()
            })
            .catch((err) => {
                this.setState({
                    error: err.data
                })
            })
    }

    onClickEditButton(trecho) {

        this.setState({
            titulo: 'Editar Trecho',
            nome: trecho.nome,
            idLocalOrigem: trecho.idLocalOrigem,
            idLocalDestino: trecho.idLocalDestino,
            id: trecho.id,
            modoEdicao: true
        })
    }

    componentDidMount() {
        TrechoService
            .getTrechos()
            .then((result) => {
                this.setState({
                    trechos: result.data,
                    modoEdicao: false
                })
            })
            .catch((err) => {
                this.setState({
                    error: err.data
                })
            })

        LocalService
            .getLocais()
            .then((result) => {
                this.setState({
                    locais: result.data,
                    modoEdicao: false
                })
            })
            .catch((err) => {
                this.setState({
                    error: err.data
                })
            })
    }

    handleChange(event) {
        const target = event.target
        const value = target.value
        const name = target.name
        this.setState({
            [name]: value
        })
    }

    renderError() {
        return this.state.error ? <Alert text={this.state.error} alertType="danger"/> : undefined
    }

    renderTrechos() {

        return this.state.trechos.map((trecho, key) => {
            return (
                <tbody>
                <tr>
                    <td><span>{trecho.nome}</span></td>
                    <td><span>{trecho.distancia} Milhas</span></td>
                    <td>
                        <Link to='/trechos'>
                            <Button type="button"
                                    buttonClass='btn btn-danger btn-sm float-right'
                                    text="Excluir"
                                    onClick={() => {
                                        this.onClickDeleteButton(trecho)
                                    }}/>
                        </Link>
                    </td>
                    <td>
                        <Link to='/trechos'>
                            <Button type="button"
                                    buttonClass='btn btn-primary btn-sm float-right'
                                    text="Editar"
                                    onClick={() => {
                                        this.onClickEditButton(trecho)
                                    }}/>
                        </Link>
                    </td>
                </tr>
                </tbody>
            )
        })
    }

    renderLocaisDestino(){
        return this.state.locais.map((local, key) => {
            return (
                <option value={local.id}>
                    {local.nome}
                </option>
            )
        })
    }

    renderLocaisOrigem(){
        return this.state.locais.map((local, key) => {
            return (
                <option value={local.id}>
                    {local.nome}
                </option>
            )
        })
    }

    render() {
        return (
            <div>
                <div className="containerOpcional">
                    <div className="listaOpcionais">
                        <h2>{this.state.titulo}</h2>

                        <form>
                            <Input
                                classLabel="hidden"
                                classInput="inputLogin form-control input-sm chat-input"
                                label="Nome"
                                value={this.state.nome}
                                name="nome"
                                placeholder={this.state.nome}
                                handleChange={this.handleChange}
                                type="text"
                            />
                            <div className='quebra'></div>
                            <div className='selectLocais'>
                                <div className='selectLocal'>
                                    <label className='label1'>Local De Origem</label>
                                    <select className="btn btn-default btn-sm dropdown-toggle" name='idLocalOrigem' value={this.state.idLocalOrigem} onChange={this.handleChange}>
                                        {this.renderLocaisOrigem()}
                                    </select>
                                </div>
                                <div className='selectLocal'>
                                    <label className='label1'>Local De Destino</label>
                                    <select className="btn btn-default btn-sm dropdown-toggle" name='idLocalDestino' value={this.state.idLocalDestino} onChange={this.handleChange}>
                                        {this.renderLocaisDestino()}
                                    </select>
                                </div>
                            </div>
                            <br/>
                            <Link to='/trechos'>
                                <Button type="button"
                                        buttonClass='btn btn-success btn-sm float-right'
                                        text="Salvar"
                                        onClick={this.onClickSalvarButton}/>
                            </Link>
                        </form>
                        {this.renderError()}

                        <div className='opcionais'><h2>Trechos cadastrados</h2>
                            <table className="table table-striped">
                                <thead>
                                <tr>
                                    <th>Trecho</th>
                                    <th>Distância</th>
                                    <th></th>
                                    <th></th>
                                </tr>
                                </thead>
                                {this.renderTrechos()}
                            </table>
                        </div>
                    </div>
                </div>
            </div>

        )
    }
}