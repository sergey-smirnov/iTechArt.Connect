'use strict';

import { createStore, combineReducers } from 'redux';
import { user as UserReducer } from './userReducer';

let reducers = { UserReducer };

export const omReactApp = combineReducers(reducers);
