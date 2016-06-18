import { Actions as EmployeesActionsTypes } from '../constants/employees.js';
var xhr = require('xhr-promise-redux');

const URL = "http://localhost:1716/api/users";

const UserActions = {
    EmployeesFetched: function(employees) {
        return {
            type: EmployeesActionsTypes.EMPLOYEES_FETCHED,
            employees
        };
    },

    FetchEmpoyees: function(filter) {
        return (dispatch) => {
            let me = this;

            dispatch(this.RequestEmployees());

            xhr.get(`${URL}`, {
                    responseType: 'json'
                })
                .then(function(response) {
                    dispatch(me.EmployeesFetched(response.body.result));
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
