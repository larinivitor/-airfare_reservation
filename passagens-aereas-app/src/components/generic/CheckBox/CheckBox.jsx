import React from 'react'
import '../../../../node_modules/bootstrap/dist/css/bootstrap.min.css'

export default class CheckBox extends React.Component {

    render() {
        return (
            <div className="form-group">
                <div>
                    <label className={this.props.classLabel} htmlFor="exampleInputEmail1">{this.props.label}</label>
                </div>
                <div>
                    <input
                        onChange={this.props.handleChange}

                        value={this.props.value}
                        type={this.props.type}
                        className={this.props.classInput}
                        name={this.props.name}
                        placeholder={this.props.placeholder} />
                </div>
            </div>
        )
    }

}