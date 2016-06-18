import { connect } from 'react-redux';

import Employees from '../components/employees/employees.js';
import EmployeesActions from '../actions/employeesActions.js';

const mapStateToProps = (state) => {
    return {
        employees: state.employees.get('employees'),
        isFetching: state.employees.get('isFetching')
    }
}

const mapDispatchToProps = (dispatch) => {
    return {
        onEmployeesFetch: () => {dispatch(EmployeesActions.FetchEmpoyees())}
    }
}

const EmployeesContainer = connect(
    mapStateToProps,
    mapDispatchToProps
)(Employees)


export default EmployeesContainer;
