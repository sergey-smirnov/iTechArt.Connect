import React, { PropTypes } from 'react'

import TextField from 'material-ui/TextField';
import RaisedButton from 'material-ui/RaisedButton';
import Paper from 'material-ui/Paper';

import RefreshIndicator from 'material-ui/RefreshIndicator';
import CircularProgress from 'material-ui/CircularProgress';

const style = {
    container: {
        position: 'relative'
    },
    refresh: {
        display: 'inline-block',
        position: 'relative',
    },
};

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
    maxHeight: '100%'
};

const LoginForm = ({ authenticated, isInProgress, onSuccess }) => (
  <div className='login-form'>
    <Paper>
      <Paper style={userImageStyle} zDepth={1} circle={true}>
        {isInProgress ? <CircularProgress size={2.1} /> : <img style={imageStyle} src='https://avatars2.githubusercontent.com/u/1629790?v=3&s=460' />}
      </Paper>
      <br />
      <TextField
         hintText='Enter your domain name'
         fullWidth={true}
         floatingLabelText='Name'
      />
      <br />
      <TextField
          hintText='Password'
          fullWidth={true}
          floatingLabelText='Password'
          type='password'
      />
      <br />
      <div className='form-buttons'>
          <RaisedButton
              onClick={onSuccess}
              className='login-button'
              label='Login'
              labelPosition='before'
              primary={true}
          />
      </div>
    </Paper>
</div>
);

LoginForm.propTypes = {
    authenticated: PropTypes.bool.isRequired,
    isInProgress: PropTypes.bool.isRequired,
    onSuccess: PropTypes.func.isRequired
}

export default LoginForm
