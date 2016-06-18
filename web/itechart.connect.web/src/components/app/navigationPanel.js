import React, {Component, PropTypes} from 'react';

import {List, ListItem, MakeSelectable} from 'material-ui/List';
import Avatar from 'material-ui/Avatar';
import Subheader from 'material-ui/Subheader';


import IconButton from 'material-ui/IconButton';
import RaisedButton from 'material-ui/RaisedButton';

import FlatButton from 'material-ui/FlatButton';
import FontIcon from 'material-ui/FontIcon';

import MapIcon from 'material-ui/svg-icons/maps/map';

import {fullWhite} from 'material-ui/styles/colors';

let SelectableList = MakeSelectable(List);
let pictureSrc = 'https://avatars2.githubusercontent.com/u/1629790?v=3&s=460';

const NavigationPanel = () => (
    <SelectableList>
    </SelectableList>
);

export default NavigationPanel;
