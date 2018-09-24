import React from 'react'
import '../../../../node_modules/bootstrap/dist/css/bootstrap.min.css'

export default class Select extends React.Component {

    renderOptions() {
        return (
            this.props.options.map((category, key) => {
                return (
                    <option
                        key={key}
                        value={category.value}>
                        {category.text}
                    </option>
                )
            })
        )
    }

    render() {
        return <div className="form-group">
                    <label >{this.props.label}</label>
                    <select
                        name={this.props.name}
                        onChange={this.props.handleChange}
                        className="form-control" >
                        {this.renderOptions()}
                    </select>
                </div>
    }
}