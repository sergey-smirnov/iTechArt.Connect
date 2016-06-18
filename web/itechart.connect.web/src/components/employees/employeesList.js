import React, { PropTypes } from 'react';
import EmployeeCard from './employeeCard.js';

var jquery = require('jquery');
var Isotope = require('../../../assets/vendors/isotope.pkgd.js');

const styles = {
  root: {
    display: 'flex',
    flexWrap: 'wrap',
    justifyContent: 'space-around',
  },
  gridList: {
    width: 500,
    height: 500,
    overflowY: 'auto',
    marginBottom: 24,
  },
};

const EmployeesList = React.createClass({
    propTypes: {
        employees: PropTypes.array
    },
    componentDidUpdate: function() {
      var iso = new Isotope( '.users-grid', {
        itemSelector: '.employee-card',
        layoutMode: 'fitRows'
      });

    },
    render: function() {
        let cards = this.props.employees && this.props.employees.map((employee) => {
          return (<EmployeeCard key={employee.id} employee={employee} />)
        });

        return (
          <div className='users-grid'>
              {cards}
          </div>
        )
    }
});

export default EmployeesList;
