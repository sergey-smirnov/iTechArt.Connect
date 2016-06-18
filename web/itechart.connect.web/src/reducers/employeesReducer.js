'use strict';

import lodash from 'lodash';
import React from 'react';
import Immutable from 'immutable';
import { Actions as EmployeesActions } from '../constants/employees.js';

const InitialState = Immutable.Map({
    isFetching: false,
    employees: null
});

export function employees(state = InitialState, action) {
    switch (action.type) {
        case EmployeesActions.REQUEST_EMPLOYEES:
            return state.set('isFetching', true);
        case EmployeesActions.EMPLOYEES_FETCHED:
            return state
                .set('isFetching', false)
                .set('employees', action.employees);
        default:
            return state;
    }
}
