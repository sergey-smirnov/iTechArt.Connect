import React from 'react';
import MapComponent from '../map/mapComponent.js';

import {List, ListItem} from 'material-ui/List';
import IconButton from 'material-ui/IconButton';
import Subheader from 'material-ui/Subheader';
import StarBorder from 'material-ui/svg-icons/toggle/star-border';

import Avatar from 'material-ui/Avatar';
import Paper from 'material-ui/Paper';

import TextField from 'material-ui/TextField';
import DatePicker from 'material-ui/DatePicker';
import TimePicker from 'material-ui/TimePicker';

const newEvent = {
    name: "New event",
    description: "Just added event",
    dateFrom: "1466233200",
    dateTo: "1466344800",
    createdBy: "Сергей Смирнов",
    geoMarker: {
        lng: 53.900719,
        lat: 27.558903
    }
};

const EventForm = ({event}) => (
    <div className='event-form'>
        <div className='map-container'>
            <MapComponent events={[newEvent]}/>
        </div>
        <div className='form-container'>
            <Paper>
                <div>
                    <TextField hintText="Name of the event" floatingLabelText="Name" fullWidth={true}/><br/>
                    <TextField hintText="Description" floatingLabelText="Description" fullWidth={true}/><br/>
                    <div>
                    <div className='date-block-from'>
                    <DatePicker floatingLabelText="From date"/>
                    <TimePicker format="24hr" hintText="Start time"/>
                    </div>
                    <div>
                    <DatePicker floatingLabelText="To date"/>
                    <TimePicker format="24hr" hintText="End time "/>
                    </div>
                    </div>
                </div>
            </Paper>
        </div>
    </div>
);

export default EventForm;
