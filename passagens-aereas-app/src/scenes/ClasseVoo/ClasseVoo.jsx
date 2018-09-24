import React from 'react'
import Input from '../../components/generic/Input/Input'
import Button from '../../components/generic/Button/Button'
import Alert from '../../components/generic/Alert/Alert'
import {Link} from 'react-router-dom'
import ClasseVooModel from "../../models/ClasseVooModel";
import ClasseVooService from "../../services/ClasseVooService";


export default class ClasseVoo extends React.Component {
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
            titulo: 'Nova Classe',
            nome: 'Nome',
            valorFixo: 0,
            valorMilha: 0,
            id: 0,
            classesVoo: [],
            error: '',
            modoEdicao: false
        }
    }

    onClickSalvarButton() {
        let classeVoo = new ClasseVooModel(
            this.state.nome,
            this.state.valorFixo,
            this.state.valorMilha,
            this.state.id
        )

        if (!this.state.modoEdicao) {
            ClasseVooService
                .cadastrar(classeVoo)
                .then((result) => {
                    this.componentDidMount()
                })
                .catch((err) => {
                    this.setState({
                        error: err.data
                    })
                })
        } else {
            ClasseVooService
                .editar(classeVoo)
                .then((result) => {
                    this.setState({
                        titulo: 'Nova Classe',
                        nome: 'Nome',
                        valorFixo: 0,
                        valorMilha: 0,
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

        ClasseVooService
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
            titulo: 'Editar Classe',
            nome: local.nome,
            latitude: local.latitudeLocal,
            longitude: local.longitudeLocal,
            id: local.id,
            modoEdicao: true
        })
    }

    componentDidMount() {
        ClasseVooService
            .getClasses()
            .then((result) => {
                this.setState({
                    classesVoo: result.data,
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

    renderClasses() {

        return this.state.classesVoo.map((classeVoo, key) => {
            return (
                <tbody>
                <tr>
                    <td><span>{classeVoo.nome}</span></td>
                    <td><span>{classeVoo.valorFixo}</span></td>
                    <td><span>{classeVoo.valorMilha}</span></td>
                    <td>
                        <Link to='/classes'>
                            <Button type="button"
                                    buttonClass='btn btn-danger btn-sm float-right'
                                    text="Excluir"
                                    onClick={() => {
                                        this.onClickDeleteButton(classeVoo)
                                    }}/>
                        </Link>
                    </td>
                    <td>
                        <Link to='/classes'>
                            <Button type="button"
                                    buttonClass='btn btn-primary btn-sm float-right'
                                    text="Editar"
                                    onClick={() => {
                                        this.onClickEditButton(classeVoo)
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
                                <label>Valor Fixo</label>
                                <Input
                                    classLabel="hidden"
                                    classInput="inputLogin form-control input-sm chat-input"
                                    label="Latitude"
                                    value={this.state.valorFixo}
                                    name="valorFixo"
                                    placeholder={this.state.valorFixo}
                                    handleChange={this.handleChange}
                                    type="text"
                                />
                            </div>
                            <div className='valoresClasse'>
                                <label>Valor Milha</label>
                                <Input
                                    classLabel="hidden"
                                    classInput="inputLogin inputClasse form-control input-sm chat-input"
                                    label="Longitude"
                                    value={this.state.valorMilha}
                                    name="valorMilha"
                                    placeholder={this.state.valorMilha}
                                    handleChange={this.handleChange}
                                    type="text"
                                />
                            </div>
                            <br/>
                            <Link to='/classes'>
                                <Button type="button"
                                        buttonClass='btn btn-success btn-sm float-right'
                                        text="Salvar"
                                        onClick={this.onClickSalvarButton}/>
                            </Link>
                        </form>
                        {this.renderError()}

                        <div className='opcionais'><h2>Classes cadastradas</h2>
                            <table className="table table-striped">
                                <thead>
                                <tr>
                                    <th>Nome</th>
                                    <th>Valor Fixo</th>
                                    <th>Valor Milha</th>
                                    <th></th>
                                    <th></th>
                                </tr>
                                </thead>
                                {this.renderClasses()}
                            </table>
                        </div>
                    </div>
                </div>
            </div>

        )
    }
}