import React from 'react'
import '../../../../node_modules/bootstrap/dist/css/bootstrap.min.css'

export default class TextArea extends React.Component {

    render() {
        return (
            <div className="form-group">
                <div>
                    <label className={this.props.classLabel} htmlFor="exampleInputEmail1">{this.props.label}</label>
                </div>
                <div>
                    <textarea
                        onChange={this.props.handleChange}
                        value={this.props.value}
                        type={this.props.type}
                        name={this.props.name}
                        placeholder={this.props.placeholder}
                        class={`form-control inputLogin ${this.props.classTextArea}`}
                        id="exampleFormControlTextarea1"
                        rows="3"/>
                </div>
            </div>
        )
    }

}