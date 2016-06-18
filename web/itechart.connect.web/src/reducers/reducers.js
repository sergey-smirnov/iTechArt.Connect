'use strict';

import { createStore, combineReducers } from 'redux';
import { user as UserReducer } from './userReducer';

import { routerReducer } from 'react-router-redux';

let reducers = { UserReducer, routing: routerReducer };

export const omReactApp = combineReducers(reducers);
