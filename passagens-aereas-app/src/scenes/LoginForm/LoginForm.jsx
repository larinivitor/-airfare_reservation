import React from 'react'
import jwt_decode from 'jwt-decode'
import Input from '../../components/generic/Input/Input'
import Alert from '../../components/generic/Alert/Alert'
import Button from '../../components/generic/Button/Button'
import './LoginForm.css'
import UsuarioService from '../../services/UsuarioService'
import {Link} from 'react-router-dom'


export default class LoginForm extends React.Component {
    constructor(props) {
        super(props)

        this.state = this.getInitialState()
        this.onClickLoginButton = this.onClickLoginButton.bind(this)
        this.handleChange = this.handleChange.bind(this)
    }

    getInitialState() {
        return {
            email: '',
            password: '',
            error: ''
        }
    }

    onClickLoginButton() {

        UsuarioService
            .login(this.state.email, this.state.password)
            .then((result) => {
                localStorage.setItem('tokenUser', result.data.token)
                localStorage.setItem('idUsuario', result.data.id)
                let decoded = jwt_decode(result.data.token);
                localStorage.setItem('userType', (decoded["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"]))
            })
            .catch((err) => {
                this.setState({
                    error: err.data
                })
            })

        /*this.props.atualizarNavBar()*/
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

    render() {
        return (
            <div className='loginPage'>
                <div className='container-login'>
                    <div className='formLogin'>
                        <h1 className='titulo'></h1>
                        <br/>
                        {this.renderError()}
                        <br/>
                        <Input
                            classLabel="hidden"
                            classInput="inputLogin form-control input-sm chat-input"
                            label="E-mail"
                            value={this.state.email}
                            name="email"
                            placeholder="Digite seu e-mail"
                            handleChange={this.handleChange}
                            type="email"
                        />
                        <Input
                            classLabel="hidden"
                            classInput="inputLogin form-control input-sm chat-input"
                            label="Senha"
                            value={this.state.password}
                            name="password"
                            placeholder="Digite sua senha"
                            handleChange={this.handleChange}
                            type="password"
                        />
                        <br/>
                        <Link to='/minhasReservas'>
                            <Button type="button"
                                    text="LogIn"
                                    onClick={() => {
                                        this.onClickLoginButton()
                                    }}/>
                        </Link>
                        <Link to='/novoUsuario'>
                            <Button type="button"
                                    text="Cadastre-se"/>
                        </Link>
                    </div>
                </div>
            </div>
        )
    }
}