import React from 'react'
import Input from '../../components/generic/Input/Input'
import Button from '../../components/generic/Button/Button'
import Alert from '../../components/generic/Alert/Alert'
import './Opcional.css'
import {Link} from 'react-router-dom'
import OpcionalService from "../../services/OpcionalService";
import TextArea from "../../components/generic/TextArea/TextArea";
import OpcionalModel from '../../models/OpcionalModel'


export default class Opcional extends React.Component {
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
            titulo: 'Novo Opcional',
            nome: 'Nome',
            descricao: 'Descrição',
            valor: 0,
            id: 0,
            opcionais: [],
            modoEdicao: false,
            error: ''
        }
    }

    onClickSalvarButton() {
        let opc = new OpcionalModel(
            this.state.nome,
            this.state.descricao,
            this.state.valor,
            this.state.id
        )

        if (!this.state.modoEdicao) {
            OpcionalService
                .cadastrar(opc)
                .then((result) => {
                    this.componentDidMount()
                })
                .catch((err) => {
                    this.setState({
                        error: err.data
                    })
                })
        } else {
            OpcionalService
                .editar(opc)
                .then((result) => {
                    this.componentDidMount()
                })
                .catch((err) => {
                    this.setState({
                        error: err.data
                    })
                })
        }
    }

    onClickDeleteButton(opcional) {

        OpcionalService
            .delete(opcional)
            .then((result) => {
                this.componentDidMount()
            })
            .catch((err) => {
                this.setState({
                    error: err.data
                })
            })
    }

    onClickEditButton(opcional) {

        this.setState({
            titulo: 'Editar Opcional',
            nome: opcional.nome,
            descricao: opcional.descricao,
            valor: opcional.valor,
            id: opcional.id,
            modoEdicao: true
        })
    }

    componentDidMount() {
        OpcionalService
            .getOpcionais()
            .then((result) => {
                this.setState({
                    titulo: 'Novo Opcional',
                    opcionais: result.data,
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

    renderOpcionais() {

        return this.state.opcionais.map((opcional, key) => {
            return (
                <tbody>
                    <tr>
                        <td><span>{opcional.nome}</span></td>
                        <td><span>{opcional.descricao}</span></td>
                        <td><span>{opcional.valor}</span></td>
                        <td>
                            <Link to='/opcionais'>
                                <Button type="button"
                                        buttonClass='btn btn-danger btn-sm float-right'
                                        text="Excluir"
                                        onClick={() => {
                                            this.onClickDeleteButton(opcional)
                                        }}/>
                            </Link>
                        </td>
                        <td>
                            <Link to='/opcionais'>
                                <Button type="button"
                                        buttonClass='btn btn-primary btn-sm float-right'
                                        text="Editar"
                                        onClick={() => {
                                            this.onClickEditButton(opcional)
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
                            <TextArea
                                classLabel="hidden"
                                classInput="inputLogin form-control input-sm chat-input"
                                label="Descrição"
                                value={this.state.descricao}
                                name="descricao"
                                placeholder={this.state.descricao}
                                handleChange={this.handleChange}
                                type="text"
                            />
                            <Input
                                classLabel="hidden"
                                classInput="inputLogin form-control input-sm chat-input"
                                label="Valor"
                                value={this.state.valor}
                                name="valor"
                                placeholder={this.state.valor}
                                handleChange={this.handleChange}
                                type="number"
                            />
                            <br/>
                            <Link to='/opcionais'>
                                <Button type="button"
                                        buttonClass='btn btn-success btn-sm float-right'
                                        text="Salvar"
                                        onClick={this.onClickSalvarButton}/>
                            </Link>
                        </form>
                        {this.renderError()}

                        <div className='opcionais'><h2>Opcionais cadastrados</h2>
                            <table className="table table-striped">
                                <thead>
                                <tr>
                                    <th>Opcional</th>
                                    <th>Descrição</th>
                                    <th>Taxa %</th>
                                    <th></th>
                                    <th></th>
                                </tr>
                                </thead>
                                {this.renderOpcionais()}
                            </table>
                        </div>
                    </div>
                </div>
            </div>

        )
    }
}