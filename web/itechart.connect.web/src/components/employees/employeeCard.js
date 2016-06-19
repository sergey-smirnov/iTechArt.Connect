import React from 'react';
import {GridList, GridTile} from 'material-ui/GridList';
import IconButton from 'material-ui/IconButton';
import Subheader from 'material-ui/Subheader';
import StarBorder from 'material-ui/svg-icons/toggle/star-border';

import Avatar from 'material-ui/Avatar';
import Paper from 'material-ui/Paper';

import {List, ListItem} from 'material-ui/List';
import ContentInbox from 'material-ui/svg-icons/content/inbox';
import ActionGrade from 'material-ui/svg-icons/action/grade';
import ContentSend from 'material-ui/svg-icons/content/send';
import ActionInfo from 'material-ui/svg-icons/action/info';

import CommunicationEmail from 'material-ui/svg-icons/communication/email';

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

const DefaultPhotoUrl = 'http://learngroup.org/assets/images/logos/default_male.jpg';

const EmployeeCard = ({ employee }) => (
    <div className='employee-card'>
      <Paper style={userImageStyle} zDepth={1} circle={true}>
         <img style={imageStyle} src={employee.profile.photoUrl} />
      </Paper>
      <List>
        <ListItem primaryText={employee.profile.firstName + ' ' + employee.profile.lastName}/>
        <ListItem primaryText={employee.department.code}  leftIcon={<ContentSend />} />
        <ListItem primaryText={employee.profile.position} leftIcon={<ActionGrade />} />
        <ListItem primaryText={employee.profile.email}  leftIcon={<CommunicationEmail />} />
      </List>
    </div>
);

export default EmployeeCard;
