'use strict';

import React, { Component } from 'react';
import { Provider } from 'react-redux';
import { Router, Route, browserHistory } from 'react-router';
import { syncHistoryWithStore, routerReducer } from 'react-router-redux';
import { createStore, combineReducers, applyMiddleware } from 'redux';
import { routerMiddleware, push } from 'react-router-redux'

import thunkMiddleware from 'redux-thunk'

import getMuiTheme from 'material-ui/styles/getMuiTheme';
import MuiThemeProvider from 'material-ui/styles/MuiThemeProvider';
import darkBaseTheme from 'material-ui/styles/baseThemes/darkBaseTheme';

import UserLogin from './userLogin.js';
import AppBar from './appBar.js';
import NavigationMenu from './navigationMenu.js';
import MapContainer from './mapContainer.js';

import { omReactApp } from '../reducers/reducers.js';

// Apply the middleware to the store
const routerM = routerMiddleware(browserHistory);

const store = createStore(omReactApp, applyMiddleware(thunkMiddleware, routerM));
// Create an enhanced history that syncs navigation events with the store
const history = syncHistoryWithStore(browserHistory, store);


const muiTheme = getMuiTheme();

const Page = ({ children }) => (
  <div>
      <NavigationMenu />
      <div className='content-container'>
        <AppBar title="iTechArt.Connect!" showMenuIconButton={false}/>
        {children}
      </div>
  </div>
);

export default class App extends Component {
    render() {
        return (
            <MuiThemeProvider muiTheme={muiTheme}>
              <Provider store={store}>
              <Router history={history}>
                <Route path="/" component={Page}>
                  <Route path="login" component={UserLogin}/>
                  <Route path="map" component={MapContainer}/>
                </Route>
              </Router>
              </Provider>
            </MuiThemeProvider>
        );
    }
}
