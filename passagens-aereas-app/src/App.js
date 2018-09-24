import React, {Component} from 'react';
import './App.css';
import LoginForm from './scenes/LoginForm/LoginForm'
import NotFound from './scenes/NotFound/NotFound'
import NovoUsuario from './scenes/NovoUsuario/NovoUsuario'
import Button from './components/generic/Button/Button'
import MinhasReservas from './scenes/MinhasReservas/MinhasReservas'
import {Switch, Route, Redirect, Link} from 'react-router-dom'
import {
    Navbar,
    NavbarToggler,
    NavbarBrand,
    Nav,
    NavItem,
} from 'reactstrap';
import Opcional from "./scenes/Opcional/Opcional";
import Local from "./scenes/Local/Local";
import ClasseVoo from "./scenes/ClasseVoo/ClasseVoo";
import Trecho from "./scenes/Trecho/Trecho";

export default class App extends Component {
    constructor() {
        super()
        this.state = {
            user: 0
        }
        this.atualizarNavBar = this.atualizarNavBar.bind(this)
        this.logOut = this.logOut.bind(this)
    }

    logOut() {
        localStorage.setItem('idUsuario', 0);
        localStorage.setItem('userType', 'none');
        localStorage.setItem('tokenUser', '');
        this.atualizarNavBar()
    }

    componentDidMount(){
        this.atualizarNavBar()
    }

    atualizarNavBar() {
        if (localStorage.getItem('userType') === "Client") {
            this.setState({
                user: 1
            })
        } else {
            if (localStorage.getItem('userType') === "Admin") {
                this.setState({
                    user: 2
                })
            } else {
                this.setState({
                    user: 0
                })
            }
        }
    }

    renderNavbar() {

        if (this.state.user == 0) {
            return ''
        } else {
            if (this.state.user == 1) {
                return (
                    <Navbar color="dark" light expand="md">
                        <NavbarBrand href="/">Airline</NavbarBrand>
                        <NavbarToggler/>
                        <Nav className="ml-auto" navbar>

                            <NavItem>
                                <Link className="App--link" to='/minhasReservas'>
                                    <Button type="button"
                                            text="Minhas Reservas"/>
                                </Link>
                            </NavItem>
                            <NavItem>
                                <Link className="App--link" to='/login'>
                                    <Button type="button"
                                            text="LogOut"
                                            onClick={this.logOut}/>
                                </Link>
                            </NavItem>
                        </Nav>
                    </Navbar>
                )
            } else {
                return (
                    <Navbar color="dark" light expand="md">
                        <NavbarBrand href="/">Airline</NavbarBrand>
                        <NavbarToggler/>
                        <Nav className="ml-auto" navbar>
                            <NavItem>
                                <Link className="App--link" to='/opcionais'>
                                    <Button type="button"
                                            text="Opcionais"
                                            onClick={this.atualizarNavBar}/>
                                </Link>
                            </NavItem>
                            <NavItem>
                                <Link className="App--link" to='/locais'>
                                    <Button type="button"
                                            text="Locais"
                                            onClick={this.atualizarNavBar}/>
                                </Link>
                            </NavItem>
                            <NavItem>
                                <Link className="App--link" to='/classes'>
                                    <Button type="button"
                                            text="Classes"
                                            onClick={this.atualizarNavBar}/>
                                </Link>
                            </NavItem>
                            <NavItem>
                                <Link className="App--link" to='/trechos'>
                                    <Button type="button"
                                            text="Trechos"
                                            onClick={this.atualizarNavBar}/>
                                </Link>
                            </NavItem>
                            <NavItem>
                                <Link className="App--link" to='/minhasReservas'>
                                    <Button type="button"
                                            text="Minhas Reservas"
                                            onClick={this.atualizarNavBar}/>
                                </Link>
                            </NavItem>
                            <NavItem>
                                <Link className="App--link" to='/login'>
                                    <Button type="button"
                                            text="LogOut"
                                            onClick={this.logOut}/>
                                </Link>
                            </NavItem>
                        </Nav>
                    </Navbar>
                )
            }
        }
    }

    render() {
        return (
            <div className="App">
                {this.renderNavbar()}
                <Switch>
                    <Route path="/login"
                           exact atualizarNavBar={this.atualizarNavBar} component={LoginForm}/>
                    <Route path="/"
                           exact atualizarNavBar={this.atualizarNavBar} component={LoginForm}/>
                    <Route path="/opcionais"
                           exact component={Opcional}/>
                    <Route path="/locais"
                           exact component={Local}/>
                    <Route path="/classes"
                           exact component={ClasseVoo}/>
                    <Route path="/trechos"
                           exact component={Trecho}/>
                    <Route path="/novoUsuario"
                           exact component={NovoUsuario}/>
                    <Route path="/minhasReservas"
                           exact component={MinhasReservas}/>
                    <Route path="/404"
                           component={NotFound}/>
                    <Redirect to="/404"/>
                </Switch>
            </div>
        );
    }
}