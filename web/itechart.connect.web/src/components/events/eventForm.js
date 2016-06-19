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

import FloatingActionButton from 'material-ui/FloatingActionButton';
import ContentAdd from 'material-ui/svg-icons/content/add';
import ContentSave from 'material-ui/svg-icons/content/save';
import RaisedButton from 'material-ui/RaisedButton';

import Dialog from 'material-ui/Dialog';
import FlatButton from 'material-ui/FlatButton';

import EmployeesContainer from '../../containers/employeesContainer.js';

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

const EventForm = React.createClass({
    getInitialState: function(){
       return {open: false}
     },

     handleOpen: function() {
       this.setState({open: true});
     },

     handleClose: function() {
       this.setState({open: false});
     },

    render: function() {
        let event = this.props.event;
        const actions = [
           <FlatButton
             label="Cancel"
             primary={true}
             onTouchTap={this.handleClose}
           />,
           <FlatButton
             label="Submit"
             primary={true}
             keyboardFocused={true}
             onTouchTap={this.handleClose}
           />,
         ];

        return (
            <div className='event-form'>
                <div className='map-container'>
                    <MapComponent events={[]} draggableMarker={true}/>
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
                            <RaisedButton onTouchTap={this.handleOpen} label="Invite people" labelPosition="before" secondary={true} style={{
                                marginTop: '50px'
                            }} icon={<ContentAdd />}/>
                            <Dialog bodyStyle={{width:'800px'}} className='employee-dialog' title="Scrollable Dialog" actions={actions} modal={false} open={this.state.open} onRequestClose={this.handleClose} autoScrollBodyContent={true}>
                                <EmployeesContainer/>
                            </Dialog>
                            <br/>
                            <RaisedButton label="Save" labelPosition="before" primary={true} style={{
                                marginTop: '50px'
                            }} icon={<ContentSave />}/>
                            <br/>
                        </div>
                    </Paper>
                </div>
            </div>
        );
    }
});
/*
const EventForm = ({event}) => (
    <div className='event-form'>
        <div className='map-container'>
            <MapComponent events={[]} draggableMarker={true}/>
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
                    <RaisedButton label="Invite people" labelPosition="before" secondary={true} style={{
                        marginTop: '50px'
                    }} icon={< ContentAdd />}/>
                    <RaisedButton label="Scrollable Dialog" onTouchTap={this.handleOpen}/>
                    <Dialog title="Scrollable Dialog" actions={actions} modal={false} open={this.state.open} onRequestClose={this.handleClose} autoScrollBodyContent={true}>
                        <EmployessComponent/>
                    </Dialog>
                    <br/>
                    <RaisedButton label="Save" labelPosition="before" primary={true} style={{
                        marginTop: '50px'
                    }} icon={< ContentSave />}/>
                    <br/>
                </div>
            </Paper>
        </div>
    </div>
);*/

export default EventForm;
