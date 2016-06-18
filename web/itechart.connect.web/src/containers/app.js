'use strict';

import React, { Component } from 'react';
import { Provider } from 'react-redux';
import { createStore, combineReducers, applyMiddleware } from 'redux';

import thunkMiddleware from 'redux-thunk'

import getMuiTheme from 'material-ui/styles/getMuiTheme';
import MuiThemeProvider from 'material-ui/styles/MuiThemeProvider';
import darkBaseTheme from 'material-ui/styles/baseThemes/darkBaseTheme';

import UserLogin from './userLogin.js';
import AppBar from './appBar.js';

import { omReactApp } from '../reducers/reducers.js';

const store = createStore(omReactApp, applyMiddleware(thunkMiddleware));

const muiTheme = getMuiTheme();

export default class App extends Component {
    render() {
        return (
            <MuiThemeProvider muiTheme={muiTheme}>
              <Provider store={store}>
                <div>
                  <AppBar />
                  <UserLogin />
                </div>
              </Provider>
            </MuiThemeProvider>
        );
    }
}
