import React from 'react'
import '../../../../node_modules/bootstrap/dist/css/bootstrap.min.css'

export default class Alert extends React.Component {
    render() {
        return <div className={`alert alert-${this.props.alertType}`}
            role="alert">
            {this.props.text}
        </div>
    }
}