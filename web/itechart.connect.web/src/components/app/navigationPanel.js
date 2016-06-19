import React, { Component, PropTypes } from 'react';
import { browserHistory } from 'react-router'

import { List, ListItem, MakeSelectable } from 'material-ui/List';
import Avatar from 'material-ui/Avatar';
import Subheader from 'material-ui/Subheader';


import ActionInfo from 'material-ui/svg-icons/action/info';

import IconButton from 'material-ui/IconButton';
import RaisedButton from 'material-ui/RaisedButton';

import FlatButton from 'material-ui/FlatButton';
import FontIcon from 'material-ui/FontIcon';

import MapIcon from 'material-ui/svg-icons/maps/map';

import { fullWhite } from 'material-ui/styles/colors';

const ItemStyle = {
  color: 'white'
};

const NavigationPanel = ({currentPage}) => (
  <List className='navigation-panel-list'>
    <ListItem primaryText="Map" onClick={() => browserHistory.push('/map')} rightIcon={<ActionInfo />} style={ItemStyle}/>
    <ListItem primaryText="Users" onClick={() => browserHistory.push('/users')} rightIcon={<ActionInfo />} style={ItemStyle}/>
    <ListItem primaryText="Events" onClick={() => browserHistory.push('/events')} rightIcon={<ActionInfo />} style={ItemStyle}/>
    <ListItem primaryText="New Event" onClick={() => browserHistory.push('/addevent')} rightIcon={<ActionInfo />} style={ItemStyle}/>
  </List>
);

export default NavigationPanel;
