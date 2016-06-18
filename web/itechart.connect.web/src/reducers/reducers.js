'use strict';

import { createStore, combineReducers } from 'redux';
import { user } from './userReducer.js';
import { employees } from './employeesReducer.js';

import { routerReducer } from 'react-router-redux';

let reducers = { user, employees, routing: routerReducer };

export const omReactApp = combineReducers(reducers);
