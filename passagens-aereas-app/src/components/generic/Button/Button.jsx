import React from 'react'
import '../../../../node_modules/bootstrap/dist/css/bootstrap.min.css'

export default class Button extends React.Component {

    render() {
        return <button
            className={`btn ${this.props.buttonClass}`}
            onClick={this.props.onClick}
            type={this.props.type}>{this.props.text}</button>
    }

}