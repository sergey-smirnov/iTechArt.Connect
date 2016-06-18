import React, { PropTypes } from 'react'

import AppBar from 'material-ui/AppBar';
import FontIcon from 'material-ui/FontIcon';
import { red800, green800 } from 'material-ui/styles/colors';
import FlatButton from 'material-ui/FlatButton';

const TopBar = ({ authenticated, onLogout }) => (
  <AppBar title="iTechArt.Connect!"
          iconElementRight={<FontIcon className="material-icons" color={authenticated ? green800 : red800}>done</FontIcon>}
          iconElementRight={<FlatButton onClick={onLogout} label='Logout' />} />
);

TopBar.propTypes = {
    authenticated: PropTypes.bool.isRequired,
    onLogout: PropTypes.func.isRequired
}

export default TopBar
