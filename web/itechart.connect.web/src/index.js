'use strict';

import React from 'react';
import ReactDOM from 'react-dom';

import injectTapEventPlugin from 'react-tap-event-plugin';

import App from './containers/app';

require('../assets/styles/app.less');

// Needed for onTouchTap
// Check this repo:
// https://github.com/zilverline/react-tap-event-plugin
injectTapEventPlugin();
ReactDOM.render(<App />, document.getElementById('root'));
