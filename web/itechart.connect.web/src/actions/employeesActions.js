import { Actions as EmployeesActionsTypes } from '../constants/employees.js';

var xhr = require('xhr-promise-redux');

const URL = "http://localhost:1716/api/users";

const EmployeesActions = {
    EmployeesFetched: function(employees) {
        return {
            type: EmployeesActionsTypes.EMPLOYEES_FETCHED,
            employees
        };
    },

    FetchEmpoyees: function(filter) {
        return (dispatch, getState) => {
            let me = this;

            dispatch(this.RequestEmployees());
            let departmentCode = getState().user.get('profile').department.code;
            xhr.get(`${URL}?departmentCode=${departmentCode}`, {
                    // xhr.get(`${URL}`, {
                    headers: {
                        'Authorization': 'Bearer ' + getState().user.get('token')
                    },
                    responseType: 'json'
                })
                .then(function(response) {
                    dispatch(me.EmployeesFetched(response.body.result.items /*.slice(50,100)*/ ));
                });
        }
    },

    RequestEmployees: function() {
        return {
            type: EmployeesActionsTypes.REQUEST_EMPLOYEES
        };
    }
};

export default EmployeesActions;
