import React, { PropTypes } from 'react';
import EmployeesList from './employeesList.js';

const Employees = React.createClass({
    propTypes: {
        onEmployeesFetch: PropTypes.func.isRequired,
        employees: PropTypes.array
    },
    componentWillMount: function() {
        this.props.onEmployeesFetch();
    },
    render: function() {
        return (
          <EmployeesList employees={this.props.employees} />
        )
    }
});

export default Employees;
