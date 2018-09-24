import React from 'react'
import './NotFound.css'
import Button from '../../components/generic/Button/Button'
import { Link } from 'react-router-dom'


export default class NotFound extends React.Component {

    render() {
        return (
            <div class="notFound">
                <div class="content content1">
                    <div class="page">
                        <div class="content">
                            <h1>404</h1>
                            <h2>Page not found</h2>
                            <br/>
                            <Link to='/login'>
                                <Button type="button"
                                        text="back to LogIn"
                                        onClick={this.props.logOut}/>
                                    </Link>
                        </div>
                    </div>
                </div>
            </div>
        )
    }

}