import React, { PropTypes } from 'react'

import AppBar from 'material-ui/AppBar';
import FontIcon from 'material-ui/FontIcon';
import { red800, green800 } from 'material-ui/styles/colors';
import FlatButton from 'material-ui/FlatButton';

const TopBar = ({ authenticated, onLogout }) => (
  <AppBar title=".Connect!"
          className='appbar'
          style={{position: 'fixed'}}
          iconElementLeft={<img className='appbar-logo' src='https://wiki.itechart-group.com/s/en_GB-1988229788/4731/0a2d13ba65b62df25186f4b87e6c642af1792689.52/_/images/logo/confluence-logo.png'></img>}
          iconElementRight={<FlatButton onClick={onLogout} label='Logout' />} />
);

TopBar.propTypes = {
    authenticated: PropTypes.bool.isRequired,
    onLogout: PropTypes.func.isRequired
}

export default TopBar
