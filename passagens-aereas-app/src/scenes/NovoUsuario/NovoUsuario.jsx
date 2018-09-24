import React from 'react'
import Input from '../../components/generic/Input/Input'
import Alert from  '../../components/generic/Alert/Alert'
import Button from '../../components/generic/Button/Button'
import UsuarioService from '../../services/UsuarioService'
import './NovoUsuario.css'
import Usuario from '../../models/UsuarioModel'
import { Switch, Route, Redirect, Link } from 'react-router-dom'


export default class NovoUsuario extends React.Component{
    constructor() {
        super()

        this.state = this.getInitialState()
        this.handleChange = this.handleChange.bind(this)
        this.onClickCadastrar = this.onClickCadastrar.bind(this)
    }

    getInitialState() {
        return {
            primeiroNome: '',
            ultimoNome: '',
            cpf: '',
            dataNascimento: '',
            login: '',
            senha: ''
        }
    }

    handleChange(event) {
        const target = event.target
        const value = target.value
        const name = target.name
        this.setState({
            [name]: value
        })
    }

    onClickCadastrar() {

        const usuario = new Usuario(
            this.state.primeiroNome,
            this.state.ultimoNome,
            this.state.cpf,
            this.state.dataNascimento,
            this.state.login,
            this.state.senha 
        )

        UsuarioService.cadastrar(usuario)
            .then((result) => {
            })
            .catch((err) => {
                this.setState({
                    error: err.data
                })
            })
    }

    renderError() {
        return this.state.error ? <Alert text={this.state.error} alertType="danger"/> : undefined
    }

    render(){
        return (
            <div className='loginPage'>
                <div className='container-login'>
                    <div className='formLogin'>
                        <h1 className='titulo'></h1>
                        {this.renderError()}
                        <br/>
                            {this.renderError()}
                        <br/>
                        <Input
                            classLabel="hidden"
                            classInput="inputLogin form-control input-sm chat-input"
                            label="Nome"
                            value={this.state.primeiroNome}
                            name="primeiroNome"
                            placeholder="Nome"
                            handleChange={this.handleChange}
                            type="text"
                        />
                        <Input
                            classLabel="hidden"
                            classInput="inputLogin form-control input-sm chat-input"
                            label="Sobrenome"
                            value={this.state.ultimoNome}
                            name="ultimoNome"
                            placeholder="Sobrenome"
                            handleChange={this.handleChange}
                            type="text"
                        />
                        <Input
                            classLabel="hidden"
                            classInput="inputLogin form-control input-sm chat-input"
                            label="CPF"
                            value={this.state.cpf}
                            name="cpf"
                            placeholder="CPF"
                            handleChange={this.handleChange}
                            type="text"
                        />
                        <Input
                            classLabel="hidden"
                            classInput="inputLogin form-control input-sm chat-input"
                            label="Data De Nascimento"
                            value={this.state.dataNascimento}
                            name="dataNascimento"
                            placeholder=""
                            handleChange={this.handleChange}
                            type="date"
                        />
                        <Input
                            classLabel="hidden"
                            classInput="inputLogin form-control input-sm chat-input"
                            label="E-mail"
                            value={this.state.login}
                            name="login"
                            placeholder="Digite seu e-mail"
                            handleChange={this.handleChange}
                            type="text"
                        />
                        <Input
                            classLabel="hidden"
                            classInput="inputLogin form-control input-sm chat-input"
                            label="Senha"
                            value={this.state.senha}
                            name="senha"
                            placeholder="Digite sua senha"
                            handleChange={this.handleChange}
                            type="password"
                        />
                        <br/>
                        <Link to='/login'>
                            <Button type="button"
                                    text="Cadastrar"
                                    onClick={this.onClickCadastrar}/>
                        </Link>
                        <Link to='/login'>
                            <Button type="button"
                                    text="Cancelar"
                            />
                        </Link>
                        
                    </div>
                </div>
            </div>
        )
    }
}