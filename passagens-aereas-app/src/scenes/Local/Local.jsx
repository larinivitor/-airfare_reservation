import React from 'react'
import Input from '../../components/generic/Input/Input'
import Button from '../../components/generic/Button/Button'
import Alert from '../../components/generic/Alert/Alert'
import {Link} from 'react-router-dom'
import LocalService from "../../services/LocalService";
import LocalModel from "../../models/LocalModel";


export default class Local extends React.Component {
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
            titulo: 'Novo Local',
            nome: 'Nome',
            latitude: 0,
            longitude: 0,
            id: 0,
            locais: [],
            error: '',
            modoEdicao: false
        }
    }

    onClickSalvarButton() {
        let local = new LocalModel(
            this.state.nome,
            parseFloat(this.state.latitude),
            parseFloat(this.state.longitude),
            this.state.id
        )

        if (!this.state.modoEdicao) {
            LocalService
                .cadastrar(local)
                .then((result) => {
                    this.componentDidMount()
                })
                .catch((err) => {
                    this.setState({
                        error: err.data
                    })
                })
        } else {
            LocalService
                .editar(local)
                .then((result) => {
                    this.setState({
                        titulo: 'Novo Local',
                        nome: 'Nome',
                        latitude: 0,
                        longitude: 0,
                        id: 0,
                    })
                    this.componentDidMount()
                })
                .catch((err) => {
                    this.setState({
                        error: err.data
                    })
                })
        }
    }

    onClickDeleteButton(local) {

        LocalService
            .delete(local)
            .then((result) => {
                this.componentDidMount()
            })
            .catch((err) => {
                this.setState({
                    error: err.data
                })
            })
    }

    onClickEditButton(local) {

        this.setState({
            titulo: 'Editar Local',
            nome: local.nome,
            latitude: local.latitudeLocal,
            longitude: local.longitudeLocal,
            id: local.id,
            modoEdicao: true
        })
    }

    componentDidMount() {
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

    renderLocais() {

        return this.state.locais.map((local, key) => {
            return (
                <tbody>
                <tr>
                    <td><span>{local.nome}</span></td>
                    <td><span>{local.latitudeLocal}</span></td>
                    <td><span>{local.longitudeLocal}</span></td>
                    <td>
                        <Link to='/locais'>
                            <Button type="button"
                                    buttonClass='btn btn-danger btn-sm float-right'
                                    text="Excluir"
                                    onClick={() => {
                                        this.onClickDeleteButton(local)
                                    }}/>
                        </Link>
                    </td>
                    <td>
                        <Link to='/locais'>
                            <Button type="button"
                                    buttonClass='btn btn-primary btn-sm float-right'
                                    text="Editar"
                                    onClick={() => {
                                        this.onClickEditButton(local)
                                    }}/>
                        </Link>
                    </td>
                </tr>
                </tbody>
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
                            <div className='valoresClasse'>
                                <label>Latitude</label>
                                <Input
                                    classLabel="hidden"
                                    classInput="inputLogin form-control input-sm chat-input"
                                    label="Latitude"
                                    value={this.state.latitude}
                                    name="latitude"
                                    placeholder={this.state.latitude}
                                    handleChange={this.handleChange}
                                    type="text"
                                />
                            </div>
                            <div className='valoresClasse'>
                                <label>Longitude</label>
                                <Input
                                    classLabel="hidden"
                                    classInput="inputLogin form-control input-sm chat-input"
                                    label="Longitude"
                                    value={this.state.longitude}
                                    name="longitude"
                                    placeholder={this.state.longitude}
                                    handleChange={this.handleChange}
                                    type="text"
                                />
                            </div>
                            <br/>
                            <Link to='/locais'>
                                <Button type="button"
                                        buttonClass='btn btn-success btn-sm float-right'
                                        text="Salvar"
                                        onClick={this.onClickSalvarButton}/>
                            </Link>
                        </form>
                        {this.renderError()}

                        <div className='opcionais'><h2>Locais cadastrados</h2>
                            <table className="table table-striped">
                                <thead>
                                <tr>
                                    <th>Nome</th>
                                    <th>Latitude</th>
                                    <th>Longitude</th>
                                    <th></th>
                                    <th></th>
                                </tr>
                                </thead>
                                {this.renderLocais()}
                            </table>
                        </div>
                    </div>
                </div>
            </div>

        )
    }
}