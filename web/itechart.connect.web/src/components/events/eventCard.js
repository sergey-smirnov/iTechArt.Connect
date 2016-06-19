import React from 'react';
import {GridList, GridTile} from 'material-ui/GridList';
import IconButton from 'material-ui/IconButton';
import Subheader from 'material-ui/Subheader';
import StarBorder from 'material-ui/svg-icons/toggle/star-border';

import Avatar from 'material-ui/Avatar';
import Paper from 'material-ui/Paper';

import {List, ListItem} from 'material-ui/List';

var moment = require('moment');

const userImageStyle = {
    height: 150,
    width: 150,
    display: 'block',
    margin: '0 auto',
    textAlign: 'center',
    overflow: 'hidden'
};

const imageStyle = {
    maxWidth: '100%',
    maxHeight: '100%',
    minWidth: '100%',
    minHeight: '100%'
};

const EventCard = ({ event }) => (
    <div className='event-card card'>
      <img style={imageStyle} src={event.img} />
      <List>
        <ListItem primaryText={event.name}/>
        <ListItem primaryText={event.description}  />
        <ListItem primaryText={moment.unix(event.dateFrom).format('LLLL')}  />
        <ListItem primaryText={moment.unix(event.dateTo).format('LLLL')}  />
      </List>
    </div>
);

export default EventCard;
