import React, { Component, PropTypes } from 'react';
import { browserHistory } from 'react-router'

import { List, ListItem, MakeSelectable } from 'material-ui/List';
import Avatar from 'material-ui/Avatar';
import Subheader from 'material-ui/Subheader';

import ActionInfo from 'material-ui/svg-icons/action/info';
import IconEvent from 'material-ui/svg-icons/action/event';
import IconAdd from 'material-ui/svg-icons/maps/add-location';
import IconPeople from 'material-ui/svg-icons/social/people';
import IconMap from 'material-ui/svg-icons/maps/map';

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
    <ListItem primaryText="Map" onClick={() => browserHistory.push('/map')} rightIcon={<IconMap />} style={ItemStyle}/>
    <ListItem primaryText="People" onClick={() => browserHistory.push('/users')} rightIcon={<IconPeople />} style={ItemStyle}/>
    <ListItem primaryText="Events" onClick={() => browserHistory.push('/events')} rightIcon={<IconEvent />} style={ItemStyle}/>
    <ListItem primaryText="New Event" onClick={() => browserHistory.push('/addevent')} rightIcon={<IconAdd />} style={ItemStyle}/>
  </List>
);

export default NavigationPanel;
